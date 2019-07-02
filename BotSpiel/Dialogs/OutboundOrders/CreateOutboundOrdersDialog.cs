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
    public class CreateOutboundOrdersDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateOutboundOrdersDialogId = "createOutboundOrdersDialog";
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

        private const string DialogKey = nameof(CreateOutboundOrdersDialog);
        private const string DialogKeyOptions = "createOutboundOrdersDialogOptions";
        private const string SearchColumnsKey = "CreateOutboundOrdersDialogSearchColumns";
        private const string SearchTextKey = "CreateOutboundOrdersDialogSearchText";
        private const string EditColumnsKey = "CreateOutboundOrdersDialogEditColumns";
        private const string EditTextKey = "CreateOutboundOrdersDialogEditText";
        private const string SelectedRecordKey = "CreateOutboundOrdersDialogSelectedRecordKey";

        private readonly IOutboundOrdersService _outboundordersService;
        OutboundOrdersPost _outboundordersPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundorders" };
        string[] edit = { "Edit outboundorders" };
        string[] details = { "Display outboundorders" };
        string[] delete = { "Delete outboundorders" };

        public CreateOutboundOrdersDialog(string id, IOutboundOrdersService outboundordersService, OutboundOrdersPost outboundordersPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_outboundordersService.VerifyOutboundOrderUnique(0L, value))
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


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             OutboundOrderPrompt,
              OrderReferencePrompt,
              OutboundOrderTypePrompt,
              FacilityPrompt,
              CompanyPrompt,
              BusinessPartnerPrompt,
              DeliverEarliestPrompt,
              DeliverLatestPrompt,
              CarrierServicePrompt,
              StatusPrompt,
              PickBatchPrompt,
              OutboundShipmentPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> OutboundOrderPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new OutboundOrdersPost();

            return await step.PromptAsync(
                OutboundOrderPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a OutboundOrder:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> OrderReferencePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sOutboundOrder = (string)step.Result;
            ((OutboundOrdersPost)step.Values[DialogKey]).sOutboundOrder = sOutboundOrder;

            return await step.PromptAsync(
                OrderReferencePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a OrderReference:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> OutboundOrderTypePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sOrderReference = (string)step.Result;
            ((OutboundOrdersPost)step.Values[DialogKey]).sOrderReference = sOrderReference;

            return await step.PromptAsync(
                OutboundOrderTypePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a OutboundOrderType:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundordersService.selectOutboundOrderTypes().Select(ct => ct.sOutboundOrderType).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> FacilityPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _OutboundOrderType = (FoundChoice)step.Result;
            var ixOutboundOrderType = _outboundordersService.selectOutboundOrderTypes().Where(ct => ct.sOutboundOrderType == _OutboundOrderType.Value).Select(ct => ct.ixOutboundOrderType).First();
            ((OutboundOrdersPost)step.Values[DialogKey]).ixOutboundOrderType = ixOutboundOrderType;

            return await step.PromptAsync(
                FacilityPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Facility:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundordersService.selectFacilities().Select(ct => ct.sFacility).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CompanyPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Facility = (FoundChoice)step.Result;
            var ixFacility = _outboundordersService.selectFacilities().Where(ct => ct.sFacility == _Facility.Value).Select(ct => ct.ixFacility).First();
            ((OutboundOrdersPost)step.Values[DialogKey]).ixFacility = ixFacility;

            return await step.PromptAsync(
                CompanyPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Company:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundordersService.selectCompanies().Select(ct => ct.sCompany).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> BusinessPartnerPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Company = (FoundChoice)step.Result;
            var ixCompany = _outboundordersService.selectCompanies().Where(ct => ct.sCompany == _Company.Value).Select(ct => ct.ixCompany).First();
            ((OutboundOrdersPost)step.Values[DialogKey]).ixCompany = ixCompany;

            return await step.PromptAsync(
                BusinessPartnerPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a BusinessPartner:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundordersService.selectBusinessPartners().Select(ct => ct.sBusinessPartner).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> DeliverEarliestPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _BusinessPartner = (FoundChoice)step.Result;
            var ixBusinessPartner = _outboundordersService.selectBusinessPartners().Where(ct => ct.sBusinessPartner == _BusinessPartner.Value).Select(ct => ct.ixBusinessPartner).First();
            ((OutboundOrdersPost)step.Values[DialogKey]).ixBusinessPartner = ixBusinessPartner;

            return await step.PromptAsync(
                DeliverEarliestPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a DeliverEarliest:"),
                    RetryPrompt = MessageFactory.Text("Please a date and/or time."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> DeliverLatestPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var dtDeliverEarliest = ((IList<DateTimeResolution>)step.Result).First();
            ((OutboundOrdersPost)step.Values[DialogKey]).dtDeliverEarliest = DateTime.Parse(dtDeliverEarliest.Value);

            return await step.PromptAsync(
                DeliverLatestPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a DeliverLatest:"),
                    RetryPrompt = MessageFactory.Text("Please a date and/or time."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CarrierServicePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var dtDeliverLatest = ((IList<DateTimeResolution>)step.Result).First();
            ((OutboundOrdersPost)step.Values[DialogKey]).dtDeliverLatest = DateTime.Parse(dtDeliverLatest.Value);

            return await step.PromptAsync(
                CarrierServicePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a CarrierService:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundordersService.selectCarrierServices().Select(ct => ct.sCarrierService).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StatusPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _CarrierService = (FoundChoice)step.Result;
            var ixCarrierService = _outboundordersService.selectCarrierServices().Where(ct => ct.sCarrierService == _CarrierService.Value).Select(ct => ct.ixCarrierService).First();
            ((OutboundOrdersPost)step.Values[DialogKey]).ixCarrierService = ixCarrierService;

            return await step.PromptAsync(
                StatusPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Status:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundordersService.selectStatuses().Select(ct => ct.sStatus).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> PickBatchPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Status = (FoundChoice)step.Result;
            var ixStatus = _outboundordersService.selectStatuses().Where(ct => ct.sStatus == _Status.Value).Select(ct => ct.ixStatus).First();
            ((OutboundOrdersPost)step.Values[DialogKey]).ixStatus = ixStatus;

            return await step.PromptAsync(
                PickBatchPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a PickBatch:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundordersService.selectPickBatches().Select(ct => ct.sPickBatch).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> OutboundShipmentPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _PickBatch = (FoundChoice)step.Result;
            var ixPickBatch = _outboundordersService.selectPickBatches().Where(ct => ct.sPickBatch == _PickBatch.Value).Select(ct => ct.ixPickBatch).First();
            ((OutboundOrdersPost)step.Values[DialogKey]).ixPickBatch = ixPickBatch;

            return await step.PromptAsync(
                OutboundShipmentPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a OutboundShipment:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundordersService.selectOutboundShipments().Select(ct => ct.sOutboundShipment).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixOutboundShipment = (Int64)step.Result;
            ((OutboundOrdersPost)step.Values[DialogKey]).ixOutboundShipment = ixOutboundShipment;


            return await step.EndDialogAsync(
                (OutboundOrdersPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


