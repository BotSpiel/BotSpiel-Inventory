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
    public class EditFacilityZonesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditFacilityZonesDialogId = "editFacilityZonesDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string FacilityZonePromptId = "facilityzonePrompt";

        private const string DialogKey = nameof(EditFacilityZonesDialog);
        private const string DialogKeyOptions = "editFacilityZonesDialogOptions";
        private const string SearchColumnsKey = "EditFacilityZonesDialogSearchColumns";
        private const string SearchTextKey = "EditFacilityZonesDialogSearchText";
        private const string EditColumnsKey = "EditFacilityZonesDialogEditColumns";
        private const string EditTextKey = "EditFacilityZonesDialogEditText";
        private const string SelectedRecordKey = "EditFacilityZonesDialogSelectedRecordKey";

        private readonly IFacilityZonesService _facilityzonesService;
        FacilityZonesPost _facilityzonesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilityzones" };
        string[] edit = { "Edit facilityzones" };
        string[] details = { "Display facilityzones" };
        string[] delete = { "Delete facilityzones" };

        public EditFacilityZonesDialog(string id, IFacilityZonesService facilityzonesService, FacilityZonesPost facilityzonesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _facilityzonesService = facilityzonesService;
            _facilityzonesPost = facilityzonesPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> facilityzoneValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_facilityzonesService.VerifyFacilityZoneUnique(_facilityzonesPost.ixFacilityZone, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The facilityzone {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(FacilityZonePromptId, facilityzoneValidator));

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

            step.Values[DialogKey] = new FacilityZonesPost();
            step.Values[DialogKeyOptions] = (FacilityZonesPost)step.Options;
            step.Values[DialogKey] = _facilityzonesService.GetPost(((FacilityZonesPost)step.Options).ixFacilityZone);
            _facilityzonesPost = _facilityzonesService.GetPost(((FacilityZonesPost)step.Options).ixFacilityZone);
            step.Values[SelectedRecordKey] = _facilityzonesPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("FacilityZones");

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
                case "FacilityZone":
					returnResult = await step.PromptAsync(
						FacilityZonePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a FacilityZone:"),
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
                case "FacilityZone":
					var sFacilityZone = (string)step.Result;
					((FacilityZonesPost)step.Values[DialogKey]).sFacilityZone = sFacilityZone;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (FacilityZonesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


