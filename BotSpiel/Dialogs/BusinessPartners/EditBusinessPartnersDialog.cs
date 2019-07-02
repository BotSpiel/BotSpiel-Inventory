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
    public class EditBusinessPartnersDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditBusinessPartnersDialogId = "editBusinessPartnersDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string BusinessPartnerPromptId = "businesspartnerPrompt";
        private const string BusinessPartnerTypePromptId = "businesspartnertypePrompt";
        private const string CompanyPromptId = "companyPrompt";
        private const string AddressPromptId = "addressPrompt";

        private const string DialogKey = nameof(EditBusinessPartnersDialog);
        private const string DialogKeyOptions = "editBusinessPartnersDialogOptions";
        private const string SearchColumnsKey = "EditBusinessPartnersDialogSearchColumns";
        private const string SearchTextKey = "EditBusinessPartnersDialogSearchText";
        private const string EditColumnsKey = "EditBusinessPartnersDialogEditColumns";
        private const string EditTextKey = "EditBusinessPartnersDialogEditText";
        private const string SelectedRecordKey = "EditBusinessPartnersDialogSelectedRecordKey";

        private readonly IBusinessPartnersService _businesspartnersService;
        BusinessPartnersPost _businesspartnersPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit businesspartners" };
        string[] edit = { "Edit businesspartners" };
        string[] details = { "Display businesspartners" };
        string[] delete = { "Delete businesspartners" };

        public EditBusinessPartnersDialog(string id, IBusinessPartnersService businesspartnersService, BusinessPartnersPost businesspartnersPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _businesspartnersService = businesspartnersService;
            _businesspartnersPost = businesspartnersPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> businesspartnerValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_businesspartnersService.VerifyBusinessPartnerUnique(_businesspartnersPost.ixBusinessPartner, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The businesspartner {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(BusinessPartnerPromptId, businesspartnerValidator));
            AddDialog(new ChoicePrompt(BusinessPartnerTypePromptId));
            AddDialog(new ChoicePrompt(CompanyPromptId));
            AddDialog(new ChoicePrompt(AddressPromptId));

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

            step.Values[DialogKey] = new BusinessPartnersPost();
            step.Values[DialogKeyOptions] = (BusinessPartnersPost)step.Options;
            step.Values[DialogKey] = _businesspartnersService.GetPost(((BusinessPartnersPost)step.Options).ixBusinessPartner);
            _businesspartnersPost = _businesspartnersService.GetPost(((BusinessPartnersPost)step.Options).ixBusinessPartner);
            step.Values[SelectedRecordKey] = _businesspartnersPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("BusinessPartners");

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
                case "BusinessPartner":
					returnResult = await step.PromptAsync(
						BusinessPartnerPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a BusinessPartner:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "BusinessPartnerType":
					returnResult = await step.PromptAsync(
						BusinessPartnerTypePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a BusinessPartnerType:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_businesspartnersService.selectBusinessPartnerTypes().Select(ct => ct.sBusinessPartnerType).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_businesspartnersService.selectCompanies().Select(ct => ct.sCompany).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_businesspartnersService.selectAddresses().Select(ct => ct.sAddress).ToList()),
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
                case "BusinessPartner":
					var sBusinessPartner = (string)step.Result;
					((BusinessPartnersPost)step.Values[DialogKey]).sBusinessPartner = sBusinessPartner;
                    break;
                case "BusinessPartnerType":
					FoundChoice _BusinessPartnerType = (FoundChoice)step.Result;
					var ixBusinessPartnerType = _businesspartnersService.selectBusinessPartnerTypes().Where(ct => ct.sBusinessPartnerType == _BusinessPartnerType.Value).Select(ct => ct.ixBusinessPartnerType).First();
					((BusinessPartnersPost)step.Values[DialogKey]).ixBusinessPartnerType = ixBusinessPartnerType;
                    break;
                case "Company":
					FoundChoice _Company = (FoundChoice)step.Result;
					var ixCompany = _businesspartnersService.selectCompanies().Where(ct => ct.sCompany == _Company.Value).Select(ct => ct.ixCompany).First();
					((BusinessPartnersPost)step.Values[DialogKey]).ixCompany = ixCompany;
                    break;
                case "Address":
					FoundChoice _Address = (FoundChoice)step.Result;
					var ixAddress = _businesspartnersService.selectAddresses().Where(ct => ct.sAddress == _Address.Value).Select(ct => ct.ixAddress).First();
					((BusinessPartnersPost)step.Values[DialogKey]).ixAddress = ixAddress;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (BusinessPartnersPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


