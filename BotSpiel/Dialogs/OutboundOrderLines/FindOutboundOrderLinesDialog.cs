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
    public class FindOutboundOrderLinesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditOutboundOrderLinesDialogId = "editOutboundOrderLinesDialog";
        private const string DetailsOutboundOrderLinesDialogId = "detailsOutboundOrderLinesDialog";
        private const string DeleteOutboundOrderLinesDialogId = "deleteOutboundOrderLinesDialog";

        private const string FindOutboundOrderLinesDialogId = "findOutboundOrderLinesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindOutboundOrderLinesDialog);
        private const string DialogKeyOptions = "findOutboundOrderLinesDialogOptions";
        private const string SearchColumnsKey = "FindOutboundOrderLinesDialogSearchColumns";
        private const string SearchTextKey = "FindOutboundOrderLinesDialogSearchText";
        private const string EditColumnsKey = "FindOutboundOrderLinesDialogEditColumns";
        private const string EditTextKey = "FindOutboundOrderLinesDialogEditText";
        private const string SelectedRecordKey = "FindOutboundOrderLinesDialogSelectedRecordKey";

        private readonly IOutboundOrderLinesService _outboundorderlinesService;
        OutboundOrderLinesPost _outboundorderlinesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundorderlines" };
        string[] edit = { "Edit outboundorderlines" };
        string[] details = { "Display outboundorderlines" };
        string[] delete = { "Delete outboundorderlines" };

        public FindOutboundOrderLinesDialog(string id, IOutboundOrderLinesService outboundorderlinesService, OutboundOrderLinesPost outboundorderlinesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _outboundorderlinesService = outboundorderlinesService;
            _outboundorderlinesPost = outboundorderlinesPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditOutboundOrderLinesDialog(EditOutboundOrderLinesDialogId, _outboundorderlinesService, _outboundorderlinesPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteOutboundOrderLinesDialog(DeleteOutboundOrderLinesDialogId, _outboundorderlinesService, _outboundorderlinesPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new OutboundOrderLinesPost();
            step.Values[SelectedRecordKey] = _outboundorderlinesPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("OutboundOrderLines");

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
            var outboundorderlinesIndex = _outboundorderlinesService.Index();
            var recordCountTotal = outboundorderlinesIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "OrderLineReference":
                    var searchRecordsOrderLineReference = outboundorderlinesIndex.Where(o => o.sOrderLineReference.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sOutboundOrderLine).Select(o => o.sOutboundOrderLine.ToString());
                    var recordCountOrderLineReference = searchRecordsOrderLineReference.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} outboundorderlines. Your search resulted in {recordCountOrderLineReference} records. I show the top 15. Please choose a OutboundOrderLine or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsOrderLineReference.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "BatchNumber":
                    var searchRecordsBatchNumber = outboundorderlinesIndex.Where(o => o.sBatchNumber.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sOutboundOrderLine).Select(o => o.sOutboundOrderLine.ToString());
                    var recordCountBatchNumber = searchRecordsBatchNumber.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} outboundorderlines. Your search resulted in {recordCountBatchNumber} records. I show the top 15. Please choose a OutboundOrderLine or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsBatchNumber.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "SerialNumber":
                    var searchRecordsSerialNumber = outboundorderlinesIndex.Where(o => o.sSerialNumber.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sOutboundOrderLine).Select(o => o.sOutboundOrderLine.ToString());
                    var recordCountSerialNumber = searchRecordsSerialNumber.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} outboundorderlines. Your search resulted in {recordCountSerialNumber} records. I show the top 15. Please choose a OutboundOrderLine or refine the search:"),
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
            var outboundorderlinesIndex = _outboundorderlinesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit outboundorderlines"))
            {

                if (selection.Value == "Refine search")
                {
                    ((OutboundOrderLinesPost)step.Values[DialogKey]).ixOutboundOrderLine = 0;
                }
                else if (selection.Value == "Exit outboundorderlines")
                {
                    ((OutboundOrderLinesPost)step.Values[DialogKey]).ixOutboundOrderLine = -1;
                }
                returnResult = await step.EndDialogAsync(
                (OutboundOrderLinesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _outboundorderlinesService.GetPost(outboundorderlinesIndex.Where(o => o.sOutboundOrderLine == selection.Value).Select(o => o.ixOutboundOrderLine).First());
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
            var outboundorderlinesIndex = _outboundorderlinesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit outboundorderlines")
            {
                ((OutboundOrderLinesPost)step.Values[DialogKey]).ixOutboundOrderLine = -1;
                returnResult = await step.EndDialogAsync(
                (OutboundOrderLinesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit outboundorderlines") || (selection.Value == "Display outboundorderlines") || (selection.Value == "Delete outboundorderlines"))
            {
                currentBotUserData.ixOutboundOrderLine = ((OutboundOrderLinesPost)step.Values[SelectedRecordKey]).ixOutboundOrderLine;
                switch (selection.Value)
                {
                    case "Edit outboundorderlines":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditOutboundOrderLinesDialogId, (OutboundOrderLinesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display outboundorderlines":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete outboundorderlines":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteOutboundOrderLinesDialogId, (OutboundOrderLinesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (OutboundOrderLinesPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


