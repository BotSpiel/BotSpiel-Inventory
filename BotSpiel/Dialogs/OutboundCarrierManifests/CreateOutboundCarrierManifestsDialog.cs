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
    public class CreateOutboundCarrierManifestsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateOutboundCarrierManifestsDialogId = "createOutboundCarrierManifestsDialog";
       private const string CarrierPromptId = "carrierPrompt";
        private const string PickupInventoryLocationPromptId = "pickupinventorylocationPrompt";
        private const string ScheduledPickupAtPromptId = "scheduledpickupatPrompt";
        private const string StatusPromptId = "statusPrompt";

        private const string DialogKey = nameof(CreateOutboundCarrierManifestsDialog);
        private const string DialogKeyOptions = "createOutboundCarrierManifestsDialogOptions";
        private const string SearchColumnsKey = "CreateOutboundCarrierManifestsDialogSearchColumns";
        private const string SearchTextKey = "CreateOutboundCarrierManifestsDialogSearchText";
        private const string EditColumnsKey = "CreateOutboundCarrierManifestsDialogEditColumns";
        private const string EditTextKey = "CreateOutboundCarrierManifestsDialogEditText";
        private const string SelectedRecordKey = "CreateOutboundCarrierManifestsDialogSelectedRecordKey";

        private readonly IOutboundCarrierManifestsService _outboundcarriermanifestsService;
        OutboundCarrierManifestsPost _outboundcarriermanifestsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundcarriermanifests" };
        string[] edit = { "Edit outboundcarriermanifests" };
        string[] details = { "Display outboundcarriermanifests" };
        string[] delete = { "Delete outboundcarriermanifests" };

        public CreateOutboundCarrierManifestsDialog(string id, IOutboundCarrierManifestsService outboundcarriermanifestsService, OutboundCarrierManifestsPost outboundcarriermanifestsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _outboundcarriermanifestsService = outboundcarriermanifestsService;
            _outboundcarriermanifestsPost = outboundcarriermanifestsPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> outboundcarriermanifestValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_outboundcarriermanifestsService.VerifyOutboundCarrierManifestUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The outboundcarriermanifest {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new ChoicePrompt(CarrierPromptId));
            AddDialog(new ChoicePrompt(PickupInventoryLocationPromptId));
            AddDialog(new DateTimePrompt(ScheduledPickupAtPromptId));
            AddDialog(new ChoicePrompt(StatusPromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             CarrierPrompt,
              PickupInventoryLocationPrompt,
              ScheduledPickupAtPrompt,
              StatusPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> CarrierPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new OutboundCarrierManifestsPost();

            return await step.PromptAsync(
                CarrierPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Carrier:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundcarriermanifestsService.selectCarriers().Select(ct => ct.sCarrier).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> PickupInventoryLocationPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Carrier = (FoundChoice)step.Result;
            var ixCarrier = _outboundcarriermanifestsService.selectCarriers().Where(ct => ct.sCarrier == _Carrier.Value).Select(ct => ct.ixCarrier).First();
            ((OutboundCarrierManifestsPost)step.Values[DialogKey]).ixCarrier = ixCarrier;

            return await step.PromptAsync(
                PickupInventoryLocationPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a PickupInventoryLocation:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundcarriermanifestsService.selectInventoryLocations().Select(ct => ct.sInventoryLocation).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> ScheduledPickupAtPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _PickupInventoryLocation = (FoundChoice)step.Result;
            var ixPickupInventoryLocation = _outboundcarriermanifestsService.selectInventoryLocations().Where(ct => ct.sInventoryLocation == _PickupInventoryLocation.Value).Select(ct => ct.ixInventoryLocation).First();
            ((OutboundCarrierManifestsPost)step.Values[DialogKey]).ixPickupInventoryLocation = ixPickupInventoryLocation;

            return await step.PromptAsync(
                ScheduledPickupAtPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a ScheduledPickupAt:"),
                    RetryPrompt = MessageFactory.Text("Please a date and/or time."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StatusPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var dtScheduledPickupAt = ((IList<DateTimeResolution>)step.Result).First();
            ((OutboundCarrierManifestsPost)step.Values[DialogKey]).dtScheduledPickupAt = DateTime.Parse(dtScheduledPickupAt.Value);

            return await step.PromptAsync(
                StatusPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Status:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundcarriermanifestsService.selectStatuses().Select(ct => ct.sStatus).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixStatus = (Int64)step.Result;
            ((OutboundCarrierManifestsPost)step.Values[DialogKey]).ixStatus = ixStatus;


            return await step.EndDialogAsync(
                (OutboundCarrierManifestsPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


