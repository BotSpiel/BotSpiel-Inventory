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
    public class EditPickBatchesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditPickBatchesDialogId = "editPickBatchesDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string PickBatchTypePromptId = "pickbatchtypePrompt";
        private const string MultiResourcePromptId = "multiresourcePrompt";
        private const string StartByPromptId = "startbyPrompt";
        private const string CompleteByPromptId = "completebyPrompt";
        private const string StatusPromptId = "statusPrompt";

        private const string DialogKey = nameof(EditPickBatchesDialog);
        private const string DialogKeyOptions = "editPickBatchesDialogOptions";
        private const string SearchColumnsKey = "EditPickBatchesDialogSearchColumns";
        private const string SearchTextKey = "EditPickBatchesDialogSearchText";
        private const string EditColumnsKey = "EditPickBatchesDialogEditColumns";
        private const string EditTextKey = "EditPickBatchesDialogEditText";
        private const string SelectedRecordKey = "EditPickBatchesDialogSelectedRecordKey";

        private readonly IPickBatchesService _pickbatchesService;
        PickBatchesPost _pickbatchesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit pickbatches" };
        string[] edit = { "Edit pickbatches" };
        string[] details = { "Display pickbatches" };
        string[] delete = { "Delete pickbatches" };

        public EditPickBatchesDialog(string id, IPickBatchesService pickbatchesService, PickBatchesPost pickbatchesPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_pickbatchesService.VerifyPickBatchUnique(_pickbatchesPost.ixPickBatch, value))
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

            AddDialog(new ChoicePrompt(ChoicePromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
              chooseEditColumnPrompt,
              editColumnPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> chooseEditColumnPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            string editColumn = "";
            string editText = "";

            step.Values[DialogKey] = new PickBatchesPost();
            step.Values[DialogKeyOptions] = (PickBatchesPost)step.Options;
            step.Values[DialogKey] = _pickbatchesService.GetPost(((PickBatchesPost)step.Options).ixPickBatch);
            _pickbatchesPost = _pickbatchesService.GetPost(((PickBatchesPost)step.Options).ixPickBatch);
            step.Values[SelectedRecordKey] = _pickbatchesPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("PickBatches");

            return await step.PromptAsync(
                ChoicePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Please choose an attribute to change:"),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(entitySearchColumns),
                },
                cancellationToken);
        }



        private async Task<DialogTurnResult> editColumnPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice editColumn = (FoundChoice)step.Result;
            step.Values[EditColumnsKey] = editColumn.Value;
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[EditColumnsKey])
            {
                case "PickBatchType":
					returnResult = await step.PromptAsync(
						PickBatchTypePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a PickBatchType:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_pickbatchesService.selectPickBatchTypes().Select(ct => ct.sPickBatchType).ToList()),
						},
						cancellationToken);
                    break;
                case "MultiResource":
					returnResult = await step.PromptAsync(
						MultiResourcePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a MultiResource:"),
							RetryPrompt = MessageFactory.Text("Please choose a valid option."),
						},
						cancellationToken);
                    break;
                case "StartBy":
					returnResult = await step.PromptAsync(
						StartByPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a StartBy:"),
							RetryPrompt = MessageFactory.Text("Please a date and/or time."),
						},
						cancellationToken);
                    break;
                case "CompleteBy":
					returnResult = await step.PromptAsync(
						CompleteByPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a CompleteBy:"),
							RetryPrompt = MessageFactory.Text("Please a date and/or time."),
						},
						cancellationToken);
                    break;
                case "Status":
					returnResult = await step.PromptAsync(
						StatusPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Status:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_pickbatchesService.selectStatuses().Select(ct => ct.sStatus).ToList()),
						},
						cancellationToken);
                    break;

                default:
                    break;
            }

            return returnResult;
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {

            switch (step.Values[EditColumnsKey])
            {
                case "PickBatchType":
					FoundChoice _PickBatchType = (FoundChoice)step.Result;
					var ixPickBatchType = _pickbatchesService.selectPickBatchTypes().Where(ct => ct.sPickBatchType == _PickBatchType.Value).Select(ct => ct.ixPickBatchType).First();
					((PickBatchesPost)step.Values[DialogKey]).ixPickBatchType = ixPickBatchType;
                    break;
                case "MultiResource":
					var bMultiResource = (bool)step.Result;
					((PickBatchesPost)step.Values[DialogKey]).bMultiResource = bMultiResource;
                    break;
                case "StartBy":
					var dtStartBy = ((IList<DateTimeResolution>)step.Result).First();
					((PickBatchesPost)step.Values[DialogKey]).dtStartBy = DateTime.Parse(dtStartBy.Value);
                    break;
                case "CompleteBy":
					var dtCompleteBy = ((IList<DateTimeResolution>)step.Result).First();
					((PickBatchesPost)step.Values[DialogKey]).dtCompleteBy = DateTime.Parse(dtCompleteBy.Value);
                    break;
                case "Status":
					FoundChoice _Status = (FoundChoice)step.Result;
					var ixStatus = _pickbatchesService.selectStatuses().Where(ct => ct.sStatus == _Status.Value).Select(ct => ct.ixStatus).First();
					((PickBatchesPost)step.Values[DialogKey]).ixStatus = ixStatus;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (PickBatchesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


