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
    public class EditFacilityFloorsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditFacilityFloorsDialogId = "editFacilityFloorsDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string FacilityFloorPromptId = "facilityfloorPrompt";

        private const string DialogKey = nameof(EditFacilityFloorsDialog);
        private const string DialogKeyOptions = "editFacilityFloorsDialogOptions";
        private const string SearchColumnsKey = "EditFacilityFloorsDialogSearchColumns";
        private const string SearchTextKey = "EditFacilityFloorsDialogSearchText";
        private const string EditColumnsKey = "EditFacilityFloorsDialogEditColumns";
        private const string EditTextKey = "EditFacilityFloorsDialogEditText";
        private const string SelectedRecordKey = "EditFacilityFloorsDialogSelectedRecordKey";

        private readonly IFacilityFloorsService _facilityfloorsService;
        FacilityFloorsPost _facilityfloorsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilityfloors" };
        string[] edit = { "Edit facilityfloors" };
        string[] details = { "Display facilityfloors" };
        string[] delete = { "Delete facilityfloors" };

        public EditFacilityFloorsDialog(string id, IFacilityFloorsService facilityfloorsService, FacilityFloorsPost facilityfloorsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _facilityfloorsService = facilityfloorsService;
            _facilityfloorsPost = facilityfloorsPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> facilityfloorValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_facilityfloorsService.VerifyFacilityFloorUnique(_facilityfloorsPost.ixFacilityFloor, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The facilityfloor {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(FacilityFloorPromptId, facilityfloorValidator));

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

            step.Values[DialogKey] = new FacilityFloorsPost();
            step.Values[DialogKeyOptions] = (FacilityFloorsPost)step.Options;
            step.Values[DialogKey] = _facilityfloorsService.GetPost(((FacilityFloorsPost)step.Options).ixFacilityFloor);
            _facilityfloorsPost = _facilityfloorsService.GetPost(((FacilityFloorsPost)step.Options).ixFacilityFloor);
            step.Values[SelectedRecordKey] = _facilityfloorsPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("FacilityFloors");

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
                case "FacilityFloor":
					returnResult = await step.PromptAsync(
						FacilityFloorPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a FacilityFloor:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
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
                case "FacilityFloor":
					var sFacilityFloor = (string)step.Result;
					((FacilityFloorsPost)step.Values[DialogKey]).sFacilityFloor = sFacilityFloor;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (FacilityFloorsPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


