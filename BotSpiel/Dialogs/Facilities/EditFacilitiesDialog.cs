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
    public class EditFacilitiesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditFacilitiesDialogId = "editFacilitiesDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string FacilityPromptId = "facilityPrompt";
        private const string AddressPromptId = "addressPrompt";
        private const string LatitudePromptId = "latitudePrompt";
        private const string LongitudePromptId = "longitudePrompt";

        private const string DialogKey = nameof(EditFacilitiesDialog);
        private const string DialogKeyOptions = "editFacilitiesDialogOptions";
        private const string SearchColumnsKey = "EditFacilitiesDialogSearchColumns";
        private const string SearchTextKey = "EditFacilitiesDialogSearchText";
        private const string EditColumnsKey = "EditFacilitiesDialogEditColumns";
        private const string EditTextKey = "EditFacilitiesDialogEditText";
        private const string SelectedRecordKey = "EditFacilitiesDialogSelectedRecordKey";

        private readonly IFacilitiesService _facilitiesService;
        FacilitiesPost _facilitiesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilities" };
        string[] edit = { "Edit facilities" };
        string[] details = { "Display facilities" };
        string[] delete = { "Delete facilities" };

        public EditFacilitiesDialog(string id, IFacilitiesService facilitiesService, FacilitiesPost facilitiesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _facilitiesService = facilitiesService;
            _facilitiesPost = facilitiesPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> facilityValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_facilitiesService.VerifyFacilityUnique(_facilitiesPost.ixFacility, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The facility {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(FacilityPromptId, facilityValidator));
            AddDialog(new ChoicePrompt(AddressPromptId));
            AddDialog(new TextPrompt(LatitudePromptId));
            AddDialog(new TextPrompt(LongitudePromptId));

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

            step.Values[DialogKey] = new FacilitiesPost();
            step.Values[DialogKeyOptions] = (FacilitiesPost)step.Options;
            step.Values[DialogKey] = _facilitiesService.GetPost(((FacilitiesPost)step.Options).ixFacility);
            _facilitiesPost = _facilitiesService.GetPost(((FacilitiesPost)step.Options).ixFacility);
            step.Values[SelectedRecordKey] = _facilitiesPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("Facilities");

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
                case "Facility":
					returnResult = await step.PromptAsync(
						FacilityPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Facility:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "Address":
					returnResult = await step.PromptAsync(
						AddressPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Address:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_facilitiesService.selectAddresses().Select(ct => ct.sAddress).ToList()),
						},
						cancellationToken);
                    break;
                case "Latitude":
					returnResult = await step.PromptAsync(
						LatitudePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Latitude:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "Longitude":
					returnResult = await step.PromptAsync(
						LongitudePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Longitude:"),
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
                case "Facility":
					var sFacility = (string)step.Result;
					((FacilitiesPost)step.Values[DialogKey]).sFacility = sFacility;
                    break;
                case "Address":
					FoundChoice _Address = (FoundChoice)step.Result;
					var ixAddress = _facilitiesService.selectAddresses().Where(ct => ct.sAddress == _Address.Value).Select(ct => ct.ixAddress).First();
					((FacilitiesPost)step.Values[DialogKey]).ixAddress = ixAddress;
                    break;
                case "Latitude":
					var sLatitude = (string)step.Result;
					((FacilitiesPost)step.Values[DialogKey]).sLatitude = sLatitude;
                    break;
                case "Longitude":
					var sLongitude = (string)step.Result;
					((FacilitiesPost)step.Values[DialogKey]).sLongitude = sLongitude;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (FacilitiesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


