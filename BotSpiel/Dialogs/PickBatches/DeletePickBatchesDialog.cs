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
    public class DeletePickBatchesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeletePickBatchesDialogId = "deletePickBatchesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeletePickBatchesDialog);
        private const string DialogKeyOptions = "deletePickBatchesDialogOptions";
        private const string SearchColumnsKey = "DeletePickBatchesDialogSearchColumns";
        private const string SearchTextKey = "DeletePickBatchesDialogSearchText";
        private const string EditColumnsKey = "DeletePickBatchesDialogEditColumns";
        private const string EditTextKey = "DeletePickBatchesDialogEditText";
        private const string SelectedRecordKey = "DeletePickBatchesDialogSelectedRecordKey";

        private readonly IPickBatchesService _pickbatchesService;
        PickBatchesPost _pickbatchesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit pickbatches" };
        string[] edit = { "Edit pickbatches" };
        string[] details = { "Display pickbatches" };
        string[] delete = { "Delete pickbatches" };

        public DeletePickBatchesDialog(string id, IPickBatchesService pickbatchesService, PickBatchesPost pickbatchesPost, BotSpielUserStateAccessors statePropertyAccessor)
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


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
              confirmDeletePrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> confirmDeletePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new PickBatchesPost();
            step.Values[DialogKeyOptions] = (PickBatchesPost)step.Options;
            ((PickBatchesPost)step.Values[DialogKey]).ixPickBatch = ((PickBatchesPost)step.Values[DialogKeyOptions]).ixPickBatch;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((PickBatchesPost)step.Options).sPickBatch}:"),
                    RetryPrompt = MessageFactory.Text("Please choose a valid option."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var yesNo = (bool)step.Result;

            if (!yesNo)
            {
                ((PickBatchesPost)step.Values[DialogKey]).ixPickBatch = -1;
            }

            return await step.EndDialogAsync(
                (PickBatchesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


