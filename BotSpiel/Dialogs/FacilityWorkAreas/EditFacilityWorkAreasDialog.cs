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
    public class EditFacilityWorkAreasDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditFacilityWorkAreasDialogId = "editFacilityWorkAreasDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string FacilityWorkAreaPromptId = "facilityworkareaPrompt";

        private const string DialogKey = nameof(EditFacilityWorkAreasDialog);
        private const string DialogKeyOptions = "editFacilityWorkAreasDialogOptions";
        private const string SearchColumnsKey = "EditFacilityWorkAreasDialogSearchColumns";
        private const string SearchTextKey = "EditFacilityWorkAreasDialogSearchText";
        private const string EditColumnsKey = "EditFacilityWorkAreasDialogEditColumns";
        private const string EditTextKey = "EditFacilityWorkAreasDialogEditText";
        private const string SelectedRecordKey = "EditFacilityWorkAreasDialogSelectedRecordKey";

        private readonly IFacilityWorkAreasService _facilityworkareasService;
        FacilityWorkAreasPost _facilityworkareasPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilityworkareas" };
        string[] edit = { "Edit facilityworkareas" };
        string[] details = { "Display facilityworkareas" };
        string[] delete = { "Delete facilityworkareas" };

        public EditFacilityWorkAreasDialog(string id, IFacilityWorkAreasService facilityworkareasService, FacilityWorkAreasPost facilityworkareasPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _facilityworkareasService = facilityworkareasService;
            _facilityworkareasPost = facilityworkareasPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> facilityworkareaValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_facilityworkareasService.VerifyFacilityWorkAreaUnique(_facilityworkareasPost.ixFacilityWorkArea, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The facilityworkarea {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(FacilityWorkAreaPromptId, facilityworkareaValidator));

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

            step.Values[DialogKey] = new FacilityWorkAreasPost();
            step.Values[DialogKeyOptions] = (FacilityWorkAreasPost)step.Options;
            step.Values[DialogKey] = _facilityworkareasService.GetPost(((FacilityWorkAreasPost)step.Options).ixFacilityWorkArea);
            _facilityworkareasPost = _facilityworkareasService.GetPost(((FacilityWorkAreasPost)step.Options).ixFacilityWorkArea);
            step.Values[SelectedRecordKey] = _facilityworkareasPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("FacilityWorkAreas");

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
                case "FacilityWorkArea":
					returnResult = await step.PromptAsync(
						FacilityWorkAreaPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a FacilityWorkArea:"),
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
                case "FacilityWorkArea":
					var sFacilityWorkArea = (string)step.Result;
					((FacilityWorkAreasPost)step.Values[DialogKey]).sFacilityWorkArea = sFacilityWorkArea;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (FacilityWorkAreasPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


