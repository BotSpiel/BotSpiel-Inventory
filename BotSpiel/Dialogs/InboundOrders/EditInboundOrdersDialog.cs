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
    public class EditInboundOrdersDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditInboundOrdersDialogId = "editInboundOrdersDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string OrderReferencePromptId = "orderreferencePrompt";
        private const string InboundOrderTypePromptId = "inboundordertypePrompt";
        private const string FacilityPromptId = "facilityPrompt";
        private const string CompanyPromptId = "companyPrompt";
        private const string BusinessPartnerPromptId = "businesspartnerPrompt";
        private const string ExpectedAtPromptId = "expectedatPrompt";
        private const string StatusPromptId = "statusPrompt";

        private const string DialogKey = nameof(EditInboundOrdersDialog);
        private const string DialogKeyOptions = "editInboundOrdersDialogOptions";
        private const string SearchColumnsKey = "EditInboundOrdersDialogSearchColumns";
        private const string SearchTextKey = "EditInboundOrdersDialogSearchText";
        private const string EditColumnsKey = "EditInboundOrdersDialogEditColumns";
        private const string EditTextKey = "EditInboundOrdersDialogEditText";
        private const string SelectedRecordKey = "EditInboundOrdersDialogSelectedRecordKey";

        private readonly IInboundOrdersService _inboundordersService;
        InboundOrdersPost _inboundordersPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inboundorders" };
        string[] edit = { "Edit inboundorders" };
        string[] details = { "Display inboundorders" };
        string[] delete = { "Delete inboundorders" };

        public EditInboundOrdersDialog(string id, IInboundOrdersService inboundordersService, InboundOrdersPost inboundordersPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_inboundordersService.VerifyInboundOrderUnique(_inboundordersPost.ixInboundOrder, value))
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

            step.Values[DialogKey] = new InboundOrdersPost();
            step.Values[DialogKeyOptions] = (InboundOrdersPost)step.Options;
            step.Values[DialogKey] = _inboundordersService.GetPost(((InboundOrdersPost)step.Options).ixInboundOrder);
            _inboundordersPost = _inboundordersService.GetPost(((InboundOrdersPost)step.Options).ixInboundOrder);
            step.Values[SelectedRecordKey] = _inboundordersPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("InboundOrders");

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
                case "InboundOrderType":
					returnResult = await step.PromptAsync(
						InboundOrderTypePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a InboundOrderType:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inboundordersService.selectInboundOrderTypes().Select(ct => ct.sInboundOrderType).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_inboundordersService.selectFacilities().Select(ct => ct.sFacility).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_inboundordersService.selectCompanies().Select(ct => ct.sCompany).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_inboundordersService.selectBusinessPartners().Select(ct => ct.sBusinessPartner).ToList()),
						},
						cancellationToken);
                    break;
                case "ExpectedAt":
					returnResult = await step.PromptAsync(
						ExpectedAtPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a ExpectedAt:"),
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
							Choices = ChoiceFactory.ToChoices(_inboundordersService.selectStatuses().Select(ct => ct.sStatus).ToList()),
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
                case "OrderReference":
					var sOrderReference = (string)step.Result;
					((InboundOrdersPost)step.Values[DialogKey]).sOrderReference = sOrderReference;
                    break;
                case "InboundOrderType":
					FoundChoice _InboundOrderType = (FoundChoice)step.Result;
					var ixInboundOrderType = _inboundordersService.selectInboundOrderTypes().Where(ct => ct.sInboundOrderType == _InboundOrderType.Value).Select(ct => ct.ixInboundOrderType).First();
					((InboundOrdersPost)step.Values[DialogKey]).ixInboundOrderType = ixInboundOrderType;
                    break;
                case "Facility":
					FoundChoice _Facility = (FoundChoice)step.Result;
					var ixFacility = _inboundordersService.selectFacilities().Where(ct => ct.sFacility == _Facility.Value).Select(ct => ct.ixFacility).First();
					((InboundOrdersPost)step.Values[DialogKey]).ixFacility = ixFacility;
                    break;
                case "Company":
					FoundChoice _Company = (FoundChoice)step.Result;
					var ixCompany = _inboundordersService.selectCompanies().Where(ct => ct.sCompany == _Company.Value).Select(ct => ct.ixCompany).First();
					((InboundOrdersPost)step.Values[DialogKey]).ixCompany = ixCompany;
                    break;
                case "BusinessPartner":
					FoundChoice _BusinessPartner = (FoundChoice)step.Result;
					var ixBusinessPartner = _inboundordersService.selectBusinessPartners().Where(ct => ct.sBusinessPartner == _BusinessPartner.Value).Select(ct => ct.ixBusinessPartner).First();
					((InboundOrdersPost)step.Values[DialogKey]).ixBusinessPartner = ixBusinessPartner;
                    break;
                case "ExpectedAt":
					var dtExpectedAt = ((IList<DateTimeResolution>)step.Result).First();
					((InboundOrdersPost)step.Values[DialogKey]).dtExpectedAt = DateTime.Parse(dtExpectedAt.Value);
                    break;
                case "Status":
					FoundChoice _Status = (FoundChoice)step.Result;
					var ixStatus = _inboundordersService.selectStatuses().Where(ct => ct.sStatus == _Status.Value).Select(ct => ct.ixStatus).First();
					((InboundOrdersPost)step.Values[DialogKey]).ixStatus = ixStatus;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (InboundOrdersPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


