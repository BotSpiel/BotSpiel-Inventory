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
    public class EditOutboundCarrierManifestsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditOutboundCarrierManifestsDialogId = "editOutboundCarrierManifestsDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string CarrierPromptId = "carrierPrompt";
        private const string PickupInventoryLocationPromptId = "pickupinventorylocationPrompt";
        private const string ScheduledPickupAtPromptId = "scheduledpickupatPrompt";
        private const string StatusPromptId = "statusPrompt";

        private const string DialogKey = nameof(EditOutboundCarrierManifestsDialog);
        private const string DialogKeyOptions = "editOutboundCarrierManifestsDialogOptions";
        private const string SearchColumnsKey = "EditOutboundCarrierManifestsDialogSearchColumns";
        private const string SearchTextKey = "EditOutboundCarrierManifestsDialogSearchText";
        private const string EditColumnsKey = "EditOutboundCarrierManifestsDialogEditColumns";
        private const string EditTextKey = "EditOutboundCarrierManifestsDialogEditText";
        private const string SelectedRecordKey = "EditOutboundCarrierManifestsDialogSelectedRecordKey";

        private readonly IOutboundCarrierManifestsService _outboundcarriermanifestsService;
        OutboundCarrierManifestsPost _outboundcarriermanifestsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundcarriermanifests" };
        string[] edit = { "Edit outboundcarriermanifests" };
        string[] details = { "Display outboundcarriermanifests" };
        string[] delete = { "Delete outboundcarriermanifests" };

        public EditOutboundCarrierManifestsDialog(string id, IOutboundCarrierManifestsService outboundcarriermanifestsService, OutboundCarrierManifestsPost outboundcarriermanifestsPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_outboundcarriermanifestsService.VerifyOutboundCarrierManifestUnique(_outboundcarriermanifestsPost.ixOutboundCarrierManifest, value))
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

            step.Values[DialogKey] = new OutboundCarrierManifestsPost();
            step.Values[DialogKeyOptions] = (OutboundCarrierManifestsPost)step.Options;
            step.Values[DialogKey] = _outboundcarriermanifestsService.GetPost(((OutboundCarrierManifestsPost)step.Options).ixOutboundCarrierManifest);
            _outboundcarriermanifestsPost = _outboundcarriermanifestsService.GetPost(((OutboundCarrierManifestsPost)step.Options).ixOutboundCarrierManifest);
            step.Values[SelectedRecordKey] = _outboundcarriermanifestsPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("OutboundCarrierManifests");

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
                case "Carrier":
					returnResult = await step.PromptAsync(
						CarrierPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Carrier:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_outboundcarriermanifestsService.selectCarriers().Select(ct => ct.sCarrier).ToList()),
						},
						cancellationToken);
                    break;
                case "PickupInventoryLocation":
					returnResult = await step.PromptAsync(
						PickupInventoryLocationPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a PickupInventoryLocation:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_outboundcarriermanifestsService.selectInventoryLocations().Select(ct => ct.sInventoryLocation).ToList()),
						},
						cancellationToken);
                    break;
                case "ScheduledPickupAt":
					returnResult = await step.PromptAsync(
						ScheduledPickupAtPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a ScheduledPickupAt:"),
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
							Choices = ChoiceFactory.ToChoices(_outboundcarriermanifestsService.selectStatuses().Select(ct => ct.sStatus).ToList()),
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
                case "Carrier":
					FoundChoice _Carrier = (FoundChoice)step.Result;
					var ixCarrier = _outboundcarriermanifestsService.selectCarriers().Where(ct => ct.sCarrier == _Carrier.Value).Select(ct => ct.ixCarrier).First();
					((OutboundCarrierManifestsPost)step.Values[DialogKey]).ixCarrier = ixCarrier;
                    break;
                case "PickupInventoryLocation":
					FoundChoice _PickupInventoryLocation = (FoundChoice)step.Result;
					var ixPickupInventoryLocation = _outboundcarriermanifestsService.selectInventoryLocations().Where(ct => ct.sInventoryLocation == _PickupInventoryLocation.Value).Select(ct => ct.ixInventoryLocation).First();
					((OutboundCarrierManifestsPost)step.Values[DialogKey]).ixPickupInventoryLocation = ixPickupInventoryLocation;
                    break;
                case "ScheduledPickupAt":
					var dtScheduledPickupAt = ((IList<DateTimeResolution>)step.Result).First();
					((OutboundCarrierManifestsPost)step.Values[DialogKey]).dtScheduledPickupAt = DateTime.Parse(dtScheduledPickupAt.Value);
                    break;
                case "Status":
					FoundChoice _Status = (FoundChoice)step.Result;
					var ixStatus = _outboundcarriermanifestsService.selectStatuses().Where(ct => ct.sStatus == _Status.Value).Select(ct => ct.ixStatus).First();
					((OutboundCarrierManifestsPost)step.Values[DialogKey]).ixStatus = ixStatus;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (OutboundCarrierManifestsPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


