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
    public class CreatePickBatchesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreatePickBatchesDialogId = "createPickBatchesDialog";
       private const string PickBatchTypePromptId = "pickbatchtypePrompt";
        private const string MultiResourcePromptId = "multiresourcePrompt";
        private const string StartByPromptId = "startbyPrompt";
        private const string CompleteByPromptId = "completebyPrompt";
        private const string StatusPromptId = "statusPrompt";

        private const string DialogKey = nameof(CreatePickBatchesDialog);
        private const string DialogKeyOptions = "createPickBatchesDialogOptions";
        private const string SearchColumnsKey = "CreatePickBatchesDialogSearchColumns";
        private const string SearchTextKey = "CreatePickBatchesDialogSearchText";
        private const string EditColumnsKey = "CreatePickBatchesDialogEditColumns";
        private const string EditTextKey = "CreatePickBatchesDialogEditText";
        private const string SelectedRecordKey = "CreatePickBatchesDialogSelectedRecordKey";

        private readonly IPickBatchesService _pickbatchesService;
        PickBatchesPost _pickbatchesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit pickbatches" };
        string[] edit = { "Edit pickbatches" };
        string[] details = { "Display pickbatches" };
        string[] delete = { "Delete pickbatches" };

        public CreatePickBatchesDialog(string id, IPickBatchesService pickbatchesService, PickBatchesPost pickbatchesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _pickbatchesService = pickbatchesService;
            _pickbatchesPost = pickbatchesPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> pickbatchValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_pickbatchesService.VerifyPickBatchUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The pickbatch {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new ChoicePrompt(PickBatchTypePromptId));
            AddDialog(new ConfirmPrompt(MultiResourcePromptId));
            AddDialog(new DateTimePrompt(StartByPromptId));
            AddDialog(new DateTimePrompt(CompleteByPromptId));
            AddDialog(new ChoicePrompt(StatusPromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             PickBatchTypePrompt,
              MultiResourcePrompt,
              StartByPrompt,
              CompleteByPrompt,
              StatusPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> PickBatchTypePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new PickBatchesPost();

            return await step.PromptAsync(
                PickBatchTypePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a PickBatchType:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_pickbatchesService.selectPickBatchTypes().Select(ct => ct.sPickBatchType).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> MultiResourcePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _PickBatchType = (FoundChoice)step.Result;
            var ixPickBatchType = _pickbatchesService.selectPickBatchTypes().Where(ct => ct.sPickBatchType == _PickBatchType.Value).Select(ct => ct.ixPickBatchType).First();
            ((PickBatchesPost)step.Values[DialogKey]).ixPickBatchType = ixPickBatchType;

            return await step.PromptAsync(
                MultiResourcePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a MultiResource:"),
                    RetryPrompt = MessageFactory.Text("Please choose a valid option."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StartByPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var bMultiResource = (bool)step.Result;
            ((PickBatchesPost)step.Values[DialogKey]).bMultiResource = bMultiResource;

            return await step.PromptAsync(
                StartByPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a StartBy:"),
                    RetryPrompt = MessageFactory.Text("Please a date and/or time."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CompleteByPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var dtStartBy = ((IList<DateTimeResolution>)step.Result).First();
            ((PickBatchesPost)step.Values[DialogKey]).dtStartBy = DateTime.Parse(dtStartBy.Value);

            return await step.PromptAsync(
                CompleteByPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a CompleteBy:"),
                    RetryPrompt = MessageFactory.Text("Please a date and/or time."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StatusPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var dtCompleteBy = ((IList<DateTimeResolution>)step.Result).First();
            ((PickBatchesPost)step.Values[DialogKey]).dtCompleteBy = DateTime.Parse(dtCompleteBy.Value);

            return await step.PromptAsync(
                StatusPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Status:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_pickbatchesService.selectStatuses().Select(ct => ct.sStatus).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixStatus = (Int64)step.Result;
            ((PickBatchesPost)step.Values[DialogKey]).ixStatus = ixStatus;


            return await step.EndDialogAsync(
                (PickBatchesPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


