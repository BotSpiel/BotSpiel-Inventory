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
    public class EditOutboundOrdersDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditOutboundOrdersDialogId = "editOutboundOrdersDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string OutboundOrderPromptId = "outboundorderPrompt";
        private const string OrderReferencePromptId = "orderreferencePrompt";
        private const string OutboundOrderTypePromptId = "outboundordertypePrompt";
        private const string FacilityPromptId = "facilityPrompt";
        private const string CompanyPromptId = "companyPrompt";
        private const string BusinessPartnerPromptId = "businesspartnerPrompt";
        private const string DeliverEarliestPromptId = "deliverearliestPrompt";
        private const string DeliverLatestPromptId = "deliverlatestPrompt";
        private const string CarrierServicePromptId = "carrierservicePrompt";
        private const string StatusPromptId = "statusPrompt";
        private const string PickBatchPromptId = "pickbatchPrompt";
        private const string OutboundShipmentPromptId = "outboundshipmentPrompt";

        private const string DialogKey = nameof(EditOutboundOrdersDialog);
        private const string DialogKeyOptions = "editOutboundOrdersDialogOptions";
        private const string SearchColumnsKey = "EditOutboundOrdersDialogSearchColumns";
        private const string SearchTextKey = "EditOutboundOrdersDialogSearchText";
        private const string EditColumnsKey = "EditOutboundOrdersDialogEditColumns";
        private const string EditTextKey = "EditOutboundOrdersDialogEditText";
        private const string SelectedRecordKey = "EditOutboundOrdersDialogSelectedRecordKey";

        private readonly IOutboundOrdersService _outboundordersService;
        OutboundOrdersPost _outboundordersPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundorders" };
        string[] edit = { "Edit outboundorders" };
        string[] details = { "Display outboundorders" };
        string[] delete = { "Delete outboundorders" };

        public EditOutboundOrdersDialog(string id, IOutboundOrdersService outboundordersService, OutboundOrdersPost outboundordersPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _outboundordersService = outboundordersService;
            _outboundordersPost = outboundordersPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> outboundorderValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_outboundordersService.VerifyOutboundOrderUnique(_outboundordersPost.ixOutboundOrder, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The outboundorder {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(OutboundOrderPromptId, outboundorderValidator));
            AddDialog(new TextPrompt(OrderReferencePromptId));
            AddDialog(new ChoicePrompt(OutboundOrderTypePromptId));
            AddDialog(new ChoicePrompt(FacilityPromptId));
            AddDialog(new ChoicePrompt(CompanyPromptId));
            AddDialog(new ChoicePrompt(BusinessPartnerPromptId));
            AddDialog(new DateTimePrompt(DeliverEarliestPromptId));
            AddDialog(new DateTimePrompt(DeliverLatestPromptId));
            AddDialog(new ChoicePrompt(CarrierServicePromptId));
            AddDialog(new ChoicePrompt(StatusPromptId));
            AddDialog(new ChoicePrompt(PickBatchPromptId));
            AddDialog(new ChoicePrompt(OutboundShipmentPromptId));

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

            step.Values[DialogKey] = new OutboundOrdersPost();
            step.Values[DialogKeyOptions] = (OutboundOrdersPost)step.Options;
            step.Values[DialogKey] = _outboundordersService.GetPost(((OutboundOrdersPost)step.Options).ixOutboundOrder);
            _outboundordersPost = _outboundordersService.GetPost(((OutboundOrdersPost)step.Options).ixOutboundOrder);
            step.Values[SelectedRecordKey] = _outboundordersPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("OutboundOrders");

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
                case "OutboundOrder":
					returnResult = await step.PromptAsync(
						OutboundOrderPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a OutboundOrder:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "OrderReference":
					returnResult = await step.PromptAsync(
						OrderReferencePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a OrderReference:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "OutboundOrderType":
					returnResult = await step.PromptAsync(
						OutboundOrderTypePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a OutboundOrderType:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_outboundordersService.selectOutboundOrderTypes().Select(ct => ct.sOutboundOrderType).ToList()),
						},
						cancellationToken);
                    break;
                case "Facility":
					returnResult = await step.PromptAsync(
						FacilityPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Facility:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_outboundordersService.selectFacilities().Select(ct => ct.sFacility).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_outboundordersService.selectCompanies().Select(ct => ct.sCompany).ToList()),
						},
						cancellationToken);
                    break;
                case "BusinessPartner":
					returnResult = await step.PromptAsync(
						BusinessPartnerPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a BusinessPartner:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_outboundordersService.selectBusinessPartners().Select(ct => ct.sBusinessPartner).ToList()),
						},
						cancellationToken);
                    break;
                case "DeliverEarliest":
					returnResult = await step.PromptAsync(
						DeliverEarliestPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a DeliverEarliest:"),
							RetryPrompt = MessageFactory.Text("Please a date and/or time."),
						},
						cancellationToken);
                    break;
                case "DeliverLatest":
					returnResult = await step.PromptAsync(
						DeliverLatestPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a DeliverLatest:"),
							RetryPrompt = MessageFactory.Text("Please a date and/or time."),
						},
						cancellationToken);
                    break;
                case "CarrierService":
					returnResult = await step.PromptAsync(
						CarrierServicePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a CarrierService:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_outboundordersService.selectCarrierServices().Select(ct => ct.sCarrierService).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_outboundordersService.selectStatuses().Select(ct => ct.sStatus).ToList()),
						},
						cancellationToken);
                    break;
                case "PickBatch":
					returnResult = await step.PromptAsync(
						PickBatchPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a PickBatch:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_outboundordersService.selectPickBatches().Select(ct => ct.sPickBatch).ToList()),
						},
						cancellationToken);
                    break;
                case "OutboundShipment":
					returnResult = await step.PromptAsync(
						OutboundShipmentPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a OutboundShipment:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_outboundordersService.selectOutboundShipments().Select(ct => ct.sOutboundShipment).ToList()),
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
                case "OutboundOrder":
					var sOutboundOrder = (string)step.Result;
					((OutboundOrdersPost)step.Values[DialogKey]).sOutboundOrder = sOutboundOrder;
                    break;
                case "OrderReference":
					var sOrderReference = (string)step.Result;
					((OutboundOrdersPost)step.Values[DialogKey]).sOrderReference = sOrderReference;
                    break;
                case "OutboundOrderType":
					FoundChoice _OutboundOrderType = (FoundChoice)step.Result;
					var ixOutboundOrderType = _outboundordersService.selectOutboundOrderTypes().Where(ct => ct.sOutboundOrderType == _OutboundOrderType.Value).Select(ct => ct.ixOutboundOrderType).First();
					((OutboundOrdersPost)step.Values[DialogKey]).ixOutboundOrderType = ixOutboundOrderType;
                    break;
                case "Facility":
					FoundChoice _Facility = (FoundChoice)step.Result;
					var ixFacility = _outboundordersService.selectFacilities().Where(ct => ct.sFacility == _Facility.Value).Select(ct => ct.ixFacility).First();
					((OutboundOrdersPost)step.Values[DialogKey]).ixFacility = ixFacility;
                    break;
                case "Company":
					FoundChoice _Company = (FoundChoice)step.Result;
					var ixCompany = _outboundordersService.selectCompanies().Where(ct => ct.sCompany == _Company.Value).Select(ct => ct.ixCompany).First();
					((OutboundOrdersPost)step.Values[DialogKey]).ixCompany = ixCompany;
                    break;
                case "BusinessPartner":
					FoundChoice _BusinessPartner = (FoundChoice)step.Result;
					var ixBusinessPartner = _outboundordersService.selectBusinessPartners().Where(ct => ct.sBusinessPartner == _BusinessPartner.Value).Select(ct => ct.ixBusinessPartner).First();
					((OutboundOrdersPost)step.Values[DialogKey]).ixBusinessPartner = ixBusinessPartner;
                    break;
                case "DeliverEarliest":
					var dtDeliverEarliest = ((IList<DateTimeResolution>)step.Result).First();
					((OutboundOrdersPost)step.Values[DialogKey]).dtDeliverEarliest = DateTime.Parse(dtDeliverEarliest.Value);
                    break;
                case "DeliverLatest":
					var dtDeliverLatest = ((IList<DateTimeResolution>)step.Result).First();
					((OutboundOrdersPost)step.Values[DialogKey]).dtDeliverLatest = DateTime.Parse(dtDeliverLatest.Value);
                    break;
                case "CarrierService":
					FoundChoice _CarrierService = (FoundChoice)step.Result;
					var ixCarrierService = _outboundordersService.selectCarrierServices().Where(ct => ct.sCarrierService == _CarrierService.Value).Select(ct => ct.ixCarrierService).First();
					((OutboundOrdersPost)step.Values[DialogKey]).ixCarrierService = ixCarrierService;
                    break;
                case "Status":
					FoundChoice _Status = (FoundChoice)step.Result;
					var ixStatus = _outboundordersService.selectStatuses().Where(ct => ct.sStatus == _Status.Value).Select(ct => ct.ixStatus).First();
					((OutboundOrdersPost)step.Values[DialogKey]).ixStatus = ixStatus;
                    break;
                case "PickBatch":
					FoundChoice _PickBatch = (FoundChoice)step.Result;
					var ixPickBatch = _outboundordersService.selectPickBatches().Where(ct => ct.sPickBatch == _PickBatch.Value).Select(ct => ct.ixPickBatch).First();
					((OutboundOrdersPost)step.Values[DialogKey]).ixPickBatch = ixPickBatch;
                    break;
                case "OutboundShipment":
					FoundChoice _OutboundShipment = (FoundChoice)step.Result;
					var ixOutboundShipment = _outboundordersService.selectOutboundShipments().Where(ct => ct.sOutboundShipment == _OutboundShipment.Value).Select(ct => ct.ixOutboundShipment).First();
					((OutboundOrdersPost)step.Values[DialogKey]).ixOutboundShipment = ixOutboundShipment;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (OutboundOrdersPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


