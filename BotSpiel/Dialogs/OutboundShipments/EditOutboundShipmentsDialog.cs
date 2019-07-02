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
    public class EditOutboundShipmentsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditOutboundShipmentsDialogId = "editOutboundShipmentsDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string FacilityPromptId = "facilityPrompt";
        private const string CompanyPromptId = "companyPrompt";
        private const string CarrierPromptId = "carrierPrompt";
        private const string CarrierConsignmentNumberPromptId = "carrierconsignmentnumberPrompt";
        private const string StatusPromptId = "statusPrompt";
        private const string AddressPromptId = "addressPrompt";
        private const string OutboundCarrierManifestPromptId = "outboundcarriermanifestPrompt";

        private const string DialogKey = nameof(EditOutboundShipmentsDialog);
        private const string DialogKeyOptions = "editOutboundShipmentsDialogOptions";
        private const string SearchColumnsKey = "EditOutboundShipmentsDialogSearchColumns";
        private const string SearchTextKey = "EditOutboundShipmentsDialogSearchText";
        private const string EditColumnsKey = "EditOutboundShipmentsDialogEditColumns";
        private const string EditTextKey = "EditOutboundShipmentsDialogEditText";
        private const string SelectedRecordKey = "EditOutboundShipmentsDialogSelectedRecordKey";

        private readonly IOutboundShipmentsService _outboundshipmentsService;
        OutboundShipmentsPost _outboundshipmentsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundshipments" };
        string[] edit = { "Edit outboundshipments" };
        string[] details = { "Display outboundshipments" };
        string[] delete = { "Delete outboundshipments" };

        public EditOutboundShipmentsDialog(string id, IOutboundShipmentsService outboundshipmentsService, OutboundShipmentsPost outboundshipmentsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _outboundshipmentsService = outboundshipmentsService;
            _outboundshipmentsPost = outboundshipmentsPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> outboundshipmentValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_outboundshipmentsService.VerifyOutboundShipmentUnique(_outboundshipmentsPost.ixOutboundShipment, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The outboundshipment {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new ChoicePrompt(FacilityPromptId));
            AddDialog(new ChoicePrompt(CompanyPromptId));
            AddDialog(new ChoicePrompt(CarrierPromptId));
            AddDialog(new TextPrompt(CarrierConsignmentNumberPromptId));
            AddDialog(new ChoicePrompt(StatusPromptId));
            AddDialog(new ChoicePrompt(AddressPromptId));
            AddDialog(new ChoicePrompt(OutboundCarrierManifestPromptId));

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

            step.Values[DialogKey] = new OutboundShipmentsPost();
            step.Values[DialogKeyOptions] = (OutboundShipmentsPost)step.Options;
            step.Values[DialogKey] = _outboundshipmentsService.GetPost(((OutboundShipmentsPost)step.Options).ixOutboundShipment);
            _outboundshipmentsPost = _outboundshipmentsService.GetPost(((OutboundShipmentsPost)step.Options).ixOutboundShipment);
            step.Values[SelectedRecordKey] = _outboundshipmentsPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("OutboundShipments");

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
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_outboundshipmentsService.selectFacilities().Select(ct => ct.sFacility).ToList()),
						},
						cancellationToken);
                    break;
                case "Company":
					returnResult = await step.PromptAsync(
						CompanyPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Company:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_outboundshipmentsService.selectCompanies().Select(ct => ct.sCompany).ToList()),
						},
						cancellationToken);
                    break;
                case "Carrier":
					returnResult = await step.PromptAsync(
						CarrierPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Carrier:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_outboundshipmentsService.selectCarriers().Select(ct => ct.sCarrier).ToList()),
						},
						cancellationToken);
                    break;
                case "CarrierConsignmentNumber":
					returnResult = await step.PromptAsync(
						CarrierConsignmentNumberPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a CarrierConsignmentNumber:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
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
							Choices = ChoiceFactory.ToChoices(_outboundshipmentsService.selectStatuses().Select(ct => ct.sStatus).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_outboundshipmentsService.selectAddresses().Select(ct => ct.sAddress).ToList()),
						},
						cancellationToken);
                    break;
                case "OutboundCarrierManifest":
					returnResult = await step.PromptAsync(
						OutboundCarrierManifestPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a OutboundCarrierManifest:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_outboundshipmentsService.selectOutboundCarrierManifests().Select(ct => ct.sOutboundCarrierManifest).ToList()),
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
					FoundChoice _Facility = (FoundChoice)step.Result;
					var ixFacility = _outboundshipmentsService.selectFacilities().Where(ct => ct.sFacility == _Facility.Value).Select(ct => ct.ixFacility).First();
					((OutboundShipmentsPost)step.Values[DialogKey]).ixFacility = ixFacility;
                    break;
                case "Company":
					FoundChoice _Company = (FoundChoice)step.Result;
					var ixCompany = _outboundshipmentsService.selectCompanies().Where(ct => ct.sCompany == _Company.Value).Select(ct => ct.ixCompany).First();
					((OutboundShipmentsPost)step.Values[DialogKey]).ixCompany = ixCompany;
                    break;
                case "Carrier":
					FoundChoice _Carrier = (FoundChoice)step.Result;
					var ixCarrier = _outboundshipmentsService.selectCarriers().Where(ct => ct.sCarrier == _Carrier.Value).Select(ct => ct.ixCarrier).First();
					((OutboundShipmentsPost)step.Values[DialogKey]).ixCarrier = ixCarrier;
                    break;
                case "CarrierConsignmentNumber":
					var sCarrierConsignmentNumber = (string)step.Result;
					((OutboundShipmentsPost)step.Values[DialogKey]).sCarrierConsignmentNumber = sCarrierConsignmentNumber;
                    break;
                case "Status":
					FoundChoice _Status = (FoundChoice)step.Result;
					var ixStatus = _outboundshipmentsService.selectStatuses().Where(ct => ct.sStatus == _Status.Value).Select(ct => ct.ixStatus).First();
					((OutboundShipmentsPost)step.Values[DialogKey]).ixStatus = ixStatus;
                    break;
                case "Address":
					FoundChoice _Address = (FoundChoice)step.Result;
					var ixAddress = _outboundshipmentsService.selectAddresses().Where(ct => ct.sAddress == _Address.Value).Select(ct => ct.ixAddress).First();
					((OutboundShipmentsPost)step.Values[DialogKey]).ixAddress = ixAddress;
                    break;
                case "OutboundCarrierManifest":
					FoundChoice _OutboundCarrierManifest = (FoundChoice)step.Result;
					var ixOutboundCarrierManifest = _outboundshipmentsService.selectOutboundCarrierManifests().Where(ct => ct.sOutboundCarrierManifest == _OutboundCarrierManifest.Value).Select(ct => ct.ixOutboundCarrierManifest).First();
					((OutboundShipmentsPost)step.Values[DialogKey]).ixOutboundCarrierManifest = ixOutboundCarrierManifest;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (OutboundShipmentsPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


