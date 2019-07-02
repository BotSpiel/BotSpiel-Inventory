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
    public class FindReceivingDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditReceivingDialogId = "editReceivingDialog";
        private const string DetailsReceivingDialogId = "detailsReceivingDialog";
        private const string DeleteReceivingDialogId = "deleteReceivingDialog";

        private const string FindReceivingDialogId = "findReceivingDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindReceivingDialog);
        private const string DialogKeyOptions = "findReceivingDialogOptions";
        private const string SearchColumnsKey = "FindReceivingDialogSearchColumns";
        private const string SearchTextKey = "FindReceivingDialogSearchText";
        private const string EditColumnsKey = "FindReceivingDialogEditColumns";
        private const string EditTextKey = "FindReceivingDialogEditText";
        private const string SelectedRecordKey = "FindReceivingDialogSelectedRecordKey";

        private readonly IReceivingService _receivingService;
        ReceivingPost _receivingPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit receiving" };
        string[] edit = { "Edit receiving" };
        string[] details = { "Display receiving" };
        string[] delete = { "Delete receiving" };

        public FindReceivingDialog(string id, IReceivingService receivingService, ReceivingPost receivingPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _receivingService = receivingService;
            _receivingPost = receivingPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditReceivingDialog(EditReceivingDialogId, _receivingService, _receivingPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteReceivingDialog(DeleteReceivingDialogId, _receivingService, _receivingPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new ReceivingPost();
            step.Values[SelectedRecordKey] = _receivingPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("Receiving");

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
            var receivingIndex = _receivingService.Index();
            var recordCountTotal = receivingIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "BatchNumber":
                    var searchRecordsBatchNumber = receivingIndex.Where(o => o.sBatchNumber.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sReceipt).Select(o => o.sReceipt.ToString());
                    var recordCountBatchNumber = searchRecordsBatchNumber.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} receiving. Your search resulted in {recordCountBatchNumber} records. I show the top 15. Please choose a Receipt or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsBatchNumber.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "SerialNumber":
                    var searchRecordsSerialNumber = receivingIndex.Where(o => o.sSerialNumber.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sReceipt).Select(o => o.sReceipt.ToString());
                    var recordCountSerialNumber = searchRecordsSerialNumber.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} receiving. Your search resulted in {recordCountSerialNumber} records. I show the top 15. Please choose a Receipt or refine the search:"),
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
            var receivingIndex = _receivingService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit receiving"))
            {

                if (selection.Value == "Refine search")
                {
                    ((ReceivingPost)step.Values[DialogKey]).ixReceipt = 0;
                }
                else if (selection.Value == "Exit receiving")
                {
                    ((ReceivingPost)step.Values[DialogKey]).ixReceipt = -1;
                }
                returnResult = await step.EndDialogAsync(
                (ReceivingPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _receivingService.GetPost(receivingIndex.Where(o => o.sReceipt == selection.Value).Select(o => o.ixReceipt).First());
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
            var receivingIndex = _receivingService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit receiving")
            {
                ((ReceivingPost)step.Values[DialogKey]).ixReceipt = -1;
                returnResult = await step.EndDialogAsync(
                (ReceivingPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit receiving") || (selection.Value == "Display receiving") || (selection.Value == "Delete receiving"))
            {
                currentBotUserData.ixReceipt = ((ReceivingPost)step.Values[SelectedRecordKey]).ixReceipt;
                switch (selection.Value)
                {
                    case "Edit receiving":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditReceivingDialogId, (ReceivingPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display receiving":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete receiving":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteReceivingDialogId, (ReceivingPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (ReceivingPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


