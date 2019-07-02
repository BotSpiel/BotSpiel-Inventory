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
    public class CreateFacilitiesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateFacilitiesDialogId = "createFacilitiesDialog";
       private const string FacilityPromptId = "facilityPrompt";
        private const string AddressPromptId = "addressPrompt";
        private const string LatitudePromptId = "latitudePrompt";
        private const string LongitudePromptId = "longitudePrompt";

        private const string DialogKey = nameof(CreateFacilitiesDialog);
        private const string DialogKeyOptions = "createFacilitiesDialogOptions";
        private const string SearchColumnsKey = "CreateFacilitiesDialogSearchColumns";
        private const string SearchTextKey = "CreateFacilitiesDialogSearchText";
        private const string EditColumnsKey = "CreateFacilitiesDialogEditColumns";
        private const string EditTextKey = "CreateFacilitiesDialogEditText";
        private const string SelectedRecordKey = "CreateFacilitiesDialogSelectedRecordKey";

        private readonly IFacilitiesService _facilitiesService;
        FacilitiesPost _facilitiesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilities" };
        string[] edit = { "Edit facilities" };
        string[] details = { "Display facilities" };
        string[] delete = { "Delete facilities" };

        public CreateFacilitiesDialog(string id, IFacilitiesService facilitiesService, FacilitiesPost facilitiesPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_facilitiesService.VerifyFacilityUnique(0L, value))
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


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             FacilityPrompt,
              AddressPrompt,
              LatitudePrompt,
              LongitudePrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> FacilityPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new FacilitiesPost();

            return await step.PromptAsync(
                FacilityPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Facility:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> AddressPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sFacility = (string)step.Result;
            ((FacilitiesPost)step.Values[DialogKey]).sFacility = sFacility;

            return await step.PromptAsync(
                AddressPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Address:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_facilitiesService.selectAddresses().Select(ct => ct.sAddress).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> LatitudePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Address = (FoundChoice)step.Result;
            var ixAddress = _facilitiesService.selectAddresses().Where(ct => ct.sAddress == _Address.Value).Select(ct => ct.ixAddress).First();
            ((FacilitiesPost)step.Values[DialogKey]).ixAddress = ixAddress;

            return await step.PromptAsync(
                LatitudePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Latitude:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> LongitudePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sLatitude = (string)step.Result;
            ((FacilitiesPost)step.Values[DialogKey]).sLatitude = sLatitude;

            return await step.PromptAsync(
                LongitudePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Longitude:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sLongitude = (string)step.Result;
            ((FacilitiesPost)step.Values[DialogKey]).sLongitude = sLongitude;


            return await step.EndDialogAsync(
                (FacilitiesPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


