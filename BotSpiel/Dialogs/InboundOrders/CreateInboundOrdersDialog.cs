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
    public class CreateInboundOrdersDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateInboundOrdersDialogId = "createInboundOrdersDialog";
       private const string OrderReferencePromptId = "orderreferencePrompt";
        private const string InboundOrderTypePromptId = "inboundordertypePrompt";
        private const string FacilityPromptId = "facilityPrompt";
        private const string CompanyPromptId = "companyPrompt";
        private const string BusinessPartnerPromptId = "businesspartnerPrompt";
        private const string ExpectedAtPromptId = "expectedatPrompt";
        private const string StatusPromptId = "statusPrompt";

        private const string DialogKey = nameof(CreateInboundOrdersDialog);
        private const string DialogKeyOptions = "createInboundOrdersDialogOptions";
        private const string SearchColumnsKey = "CreateInboundOrdersDialogSearchColumns";
        private const string SearchTextKey = "CreateInboundOrdersDialogSearchText";
        private const string EditColumnsKey = "CreateInboundOrdersDialogEditColumns";
        private const string EditTextKey = "CreateInboundOrdersDialogEditText";
        private const string SelectedRecordKey = "CreateInboundOrdersDialogSelectedRecordKey";

        private readonly IInboundOrdersService _inboundordersService;
        InboundOrdersPost _inboundordersPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inboundorders" };
        string[] edit = { "Edit inboundorders" };
        string[] details = { "Display inboundorders" };
        string[] delete = { "Delete inboundorders" };

        public CreateInboundOrdersDialog(string id, IInboundOrdersService inboundordersService, InboundOrdersPost inboundordersPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inboundordersService = inboundordersService;
            _inboundordersPost = inboundordersPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> inboundorderValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_inboundordersService.VerifyInboundOrderUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The inboundorder {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(OrderReferencePromptId));
            AddDialog(new ChoicePrompt(InboundOrderTypePromptId));
            AddDialog(new ChoicePrompt(FacilityPromptId));
            AddDialog(new ChoicePrompt(CompanyPromptId));
            AddDialog(new ChoicePrompt(BusinessPartnerPromptId));
            AddDialog(new DateTimePrompt(ExpectedAtPromptId));
            AddDialog(new ChoicePrompt(StatusPromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             OrderReferencePrompt,
              InboundOrderTypePrompt,
              FacilityPrompt,
              CompanyPrompt,
              BusinessPartnerPrompt,
              ExpectedAtPrompt,
              StatusPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> OrderReferencePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new InboundOrdersPost();

            return await step.PromptAsync(
                OrderReferencePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a OrderReference:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> InboundOrderTypePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sOrderReference = (string)step.Result;
            ((InboundOrdersPost)step.Values[DialogKey]).sOrderReference = sOrderReference;

            return await step.PromptAsync(
                InboundOrderTypePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a InboundOrderType:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inboundordersService.selectInboundOrderTypes().Select(ct => ct.sInboundOrderType).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> FacilityPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _InboundOrderType = (FoundChoice)step.Result;
            var ixInboundOrderType = _inboundordersService.selectInboundOrderTypes().Where(ct => ct.sInboundOrderType == _InboundOrderType.Value).Select(ct => ct.ixInboundOrderType).First();
            ((InboundOrdersPost)step.Values[DialogKey]).ixInboundOrderType = ixInboundOrderType;

            return await step.PromptAsync(
                FacilityPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Facility:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inboundordersService.selectFacilities().Select(ct => ct.sFacility).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CompanyPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Facility = (FoundChoice)step.Result;
            var ixFacility = _inboundordersService.selectFacilities().Where(ct => ct.sFacility == _Facility.Value).Select(ct => ct.ixFacility).First();
            ((InboundOrdersPost)step.Values[DialogKey]).ixFacility = ixFacility;

            return await step.PromptAsync(
                CompanyPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Company:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inboundordersService.selectCompanies().Select(ct => ct.sCompany).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> BusinessPartnerPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Company = (FoundChoice)step.Result;
            var ixCompany = _inboundordersService.selectCompanies().Where(ct => ct.sCompany == _Company.Value).Select(ct => ct.ixCompany).First();
            ((InboundOrdersPost)step.Values[DialogKey]).ixCompany = ixCompany;

            return await step.PromptAsync(
                BusinessPartnerPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a BusinessPartner:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inboundordersService.selectBusinessPartners().Select(ct => ct.sBusinessPartner).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> ExpectedAtPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _BusinessPartner = (FoundChoice)step.Result;
            var ixBusinessPartner = _inboundordersService.selectBusinessPartners().Where(ct => ct.sBusinessPartner == _BusinessPartner.Value).Select(ct => ct.ixBusinessPartner).First();
            ((InboundOrdersPost)step.Values[DialogKey]).ixBusinessPartner = ixBusinessPartner;

            return await step.PromptAsync(
                ExpectedAtPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a ExpectedAt:"),
                    RetryPrompt = MessageFactory.Text("Please a date and/or time."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StatusPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var dtExpectedAt = ((IList<DateTimeResolution>)step.Result).First();
            ((InboundOrdersPost)step.Values[DialogKey]).dtExpectedAt = DateTime.Parse(dtExpectedAt.Value);

            return await step.PromptAsync(
                StatusPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Status:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inboundordersService.selectStatuses().Select(ct => ct.sStatus).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixStatus = (Int64)step.Result;
            ((InboundOrdersPost)step.Values[DialogKey]).ixStatus = ixStatus;


            return await step.EndDialogAsync(
                (InboundOrdersPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


