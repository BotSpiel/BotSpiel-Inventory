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
    public class FindPickBatchesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditPickBatchesDialogId = "editPickBatchesDialog";
        private const string DetailsPickBatchesDialogId = "detailsPickBatchesDialog";
        private const string DeletePickBatchesDialogId = "deletePickBatchesDialog";

        private const string FindPickBatchesDialogId = "findPickBatchesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindPickBatchesDialog);
        private const string DialogKeyOptions = "findPickBatchesDialogOptions";
        private const string SearchColumnsKey = "FindPickBatchesDialogSearchColumns";
        private const string SearchTextKey = "FindPickBatchesDialogSearchText";
        private const string EditColumnsKey = "FindPickBatchesDialogEditColumns";
        private const string EditTextKey = "FindPickBatchesDialogEditText";
        private const string SelectedRecordKey = "FindPickBatchesDialogSelectedRecordKey";

        private readonly IPickBatchesService _pickbatchesService;
        PickBatchesPost _pickbatchesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit pickbatches" };
        string[] edit = { "Edit pickbatches" };
        string[] details = { "Display pickbatches" };
        string[] delete = { "Delete pickbatches" };

        public FindPickBatchesDialog(string id, IPickBatchesService pickbatchesService, PickBatchesPost pickbatchesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _pickbatchesService = pickbatchesService;
            _pickbatchesPost = pickbatchesPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditPickBatchesDialog(EditPickBatchesDialogId, _pickbatchesService, _pickbatchesPost, _botSpielUserStateAccessors));
            AddDialog(new DeletePickBatchesDialog(DeletePickBatchesDialogId, _pickbatchesService, _pickbatchesPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new PickBatchesPost();
            step.Values[SelectedRecordKey] = _pickbatchesPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("PickBatches");

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
            var pickbatchesIndex = _pickbatchesService.Index();
            var recordCountTotal = pickbatchesIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {

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
            var pickbatchesIndex = _pickbatchesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit pickbatches"))
            {

                if (selection.Value == "Refine search")
                {
                    ((PickBatchesPost)step.Values[DialogKey]).ixPickBatch = 0;
                }
                else if (selection.Value == "Exit pickbatches")
                {
                    ((PickBatchesPost)step.Values[DialogKey]).ixPickBatch = -1;
                }
                returnResult = await step.EndDialogAsync(
                (PickBatchesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _pickbatchesService.GetPost(pickbatchesIndex.Where(o => o.sPickBatch == selection.Value).Select(o => o.ixPickBatch).First());
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
            var pickbatchesIndex = _pickbatchesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit pickbatches")
            {
                ((PickBatchesPost)step.Values[DialogKey]).ixPickBatch = -1;
                returnResult = await step.EndDialogAsync(
                (PickBatchesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit pickbatches") || (selection.Value == "Display pickbatches") || (selection.Value == "Delete pickbatches"))
            {
                currentBotUserData.ixPickBatch = ((PickBatchesPost)step.Values[SelectedRecordKey]).ixPickBatch;
                switch (selection.Value)
                {
                    case "Edit pickbatches":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditPickBatchesDialogId, (PickBatchesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display pickbatches":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete pickbatches":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeletePickBatchesDialogId, (PickBatchesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (PickBatchesPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


