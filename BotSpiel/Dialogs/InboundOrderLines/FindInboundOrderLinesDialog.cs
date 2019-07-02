using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;
using BotSpiel.Services;

namespace BotSpiel.Dialogs
{
    public class FindInboundOrderLinesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditInboundOrderLinesDialogId = "editInboundOrderLinesDialog";
        private const string DetailsInboundOrderLinesDialogId = "detailsInboundOrderLinesDialog";
        private const string DeleteInboundOrderLinesDialogId = "deleteInboundOrderLinesDialog";

        private const string FindInboundOrderLinesDialogId = "findInboundOrderLinesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindInboundOrderLinesDialog);
        private const string DialogKeyOptions = "findInboundOrderLinesDialogOptions";
        private const string SearchColumnsKey = "FindInboundOrderLinesDialogSearchColumns";
        private const string SearchTextKey = "FindInboundOrderLinesDialogSearchText";
        private const string EditColumnsKey = "FindInboundOrderLinesDialogEditColumns";
        private const string EditTextKey = "FindInboundOrderLinesDialogEditText";
        private const string SelectedRecordKey = "FindInboundOrderLinesDialogSelectedRecordKey";

        private readonly IInboundOrderLinesService _inboundorderlinesService;
        InboundOrderLinesPost _inboundorderlinesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inboundorderlines" };
        string[] edit = { "Edit inboundorderlines" };
        string[] details = { "Display inboundorderlines" };
        string[] delete = { "Delete inboundorderlines" };

        public FindInboundOrderLinesDialog(string id, IInboundOrderLinesService inboundorderlinesService, InboundOrderLinesPost inboundorderlinesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inboundorderlinesService = inboundorderlinesService;
            _inboundorderlinesPost = inboundorderlinesPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditInboundOrderLinesDialog(EditInboundOrderLinesDialogId, _inboundorderlinesService, _inboundorderlinesPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteInboundOrderLinesDialog(DeleteInboundOrderLinesDialogId, _inboundorderlinesService, _inboundorderlinesPost, _botSpielUserStateAccessors));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             chooseSearchColumnPrompt,
             enterSearchTextPrompt,
             selectFromResultPrompt,
             editDeleteDetailsPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }

        private async Task<DialogTurnResult> chooseSearchColumnPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            string searchColumn = "";
            string searchText = "";

            step.Values[DialogKey] = new InboundOrderLinesPost();
            step.Values[SelectedRecordKey] = _inboundorderlinesPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("InboundOrderLines");

            return await step.PromptAsync(
                ChoicePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Please choose a column to search on:"),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(entitySearchColumns),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> enterSearchTextPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice searchColumn = (FoundChoice)step.Result;
            step.Values[SearchColumnsKey] = searchColumn.Value;

            return await step.PromptAsync(
                TextPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter text to search for in {step.Values[SearchColumnsKey]}:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }


        private async Task<DialogTurnResult> selectFromResultPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[SearchTextKey] = (string)step.Result;
            var inboundorderlinesIndex = _inboundorderlinesService.Index();
            var recordCountTotal = inboundorderlinesIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "OrderLineReference":
                    var searchRecordsOrderLineReference = inboundorderlinesIndex.Where(o => o.sOrderLineReference.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sInboundOrderLine).Select(o => o.sInboundOrderLine.ToString());
                    var recordCountOrderLineReference = searchRecordsOrderLineReference.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} inboundorderlines. Your search resulted in {recordCountOrderLineReference} records. I show the top 15. Please choose a InboundOrderLine or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsOrderLineReference.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "BatchNumber":
                    var searchRecordsBatchNumber = inboundorderlinesIndex.Where(o => o.sBatchNumber.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sInboundOrderLine).Select(o => o.sInboundOrderLine.ToString());
                    var recordCountBatchNumber = searchRecordsBatchNumber.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} inboundorderlines. Your search resulted in {recordCountBatchNumber} records. I show the top 15. Please choose a InboundOrderLine or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsBatchNumber.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "SerialNumber":
                    var searchRecordsSerialNumber = inboundorderlinesIndex.Where(o => o.sSerialNumber.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sInboundOrderLine).Select(o => o.sInboundOrderLine.ToString());
                    var recordCountSerialNumber = searchRecordsSerialNumber.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} inboundorderlines. Your search resulted in {recordCountSerialNumber} records. I show the top 15. Please choose a InboundOrderLine or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsSerialNumber.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;

                default:
                    break;
            }

            return returnResult;
        }



        private async Task<DialogTurnResult> editDeleteDetailsPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            DialogTurnResult returnResult = new DialogTurnResult(0);
            var inboundorderlinesIndex = _inboundorderlinesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit inboundorderlines"))
            {

                if (selection.Value == "Refine search")
                {
                    ((InboundOrderLinesPost)step.Values[DialogKey]).ixInboundOrderLine = 0;
                }
                else if (selection.Value == "Exit inboundorderlines")
                {
                    ((InboundOrderLinesPost)step.Values[DialogKey]).ixInboundOrderLine = -1;
                }
                returnResult = await step.EndDialogAsync(
                (InboundOrderLinesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _inboundorderlinesService.GetPost(inboundorderlinesIndex.Where(o => o.sInboundOrderLine == selection.Value).Select(o => o.ixInboundOrderLine).First());
                returnResult = await step.PromptAsync(
                    ChoicePromptId,
                    new PromptOptions
                    {
                        Prompt = MessageFactory.Text($"Would you like to edit, display or delete {selection.Value}. Please choose an option or exit to continue."),
                        RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                        Choices = ChoiceFactory.ToChoices(edit.Union(details).Union(delete).Union(exit).ToList())
                    },
                    cancellationToken);
            }
            return returnResult;
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            DialogTurnResult returnResult = new DialogTurnResult(0);

            var currentBotUserData = await _botSpielUserStateAccessors.BotUserDataAccessor.GetAsync(step.Context, () => _botUserData);
            var inboundorderlinesIndex = _inboundorderlinesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit inboundorderlines")
            {
                ((InboundOrderLinesPost)step.Values[DialogKey]).ixInboundOrderLine = -1;
                returnResult = await step.EndDialogAsync(
                (InboundOrderLinesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit inboundorderlines") || (selection.Value == "Display inboundorderlines") || (selection.Value == "Delete inboundorderlines"))
            {
                currentBotUserData.ixInboundOrderLine = ((InboundOrderLinesPost)step.Values[SelectedRecordKey]).ixInboundOrderLine;
                switch (selection.Value)
                {
                    case "Edit inboundorderlines":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditInboundOrderLinesDialogId, (InboundOrderLinesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display inboundorderlines":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete inboundorderlines":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteInboundOrderLinesDialogId, (InboundOrderLinesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (InboundOrderLinesPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


