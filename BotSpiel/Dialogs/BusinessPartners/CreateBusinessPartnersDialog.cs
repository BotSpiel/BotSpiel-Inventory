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
    public class CreateBusinessPartnersDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateBusinessPartnersDialogId = "createBusinessPartnersDialog";
       private const string BusinessPartnerPromptId = "businesspartnerPrompt";
        private const string BusinessPartnerTypePromptId = "businesspartnertypePrompt";
        private const string CompanyPromptId = "companyPrompt";
        private const string AddressPromptId = "addressPrompt";

        private const string DialogKey = nameof(CreateBusinessPartnersDialog);
        private const string DialogKeyOptions = "createBusinessPartnersDialogOptions";
        private const string SearchColumnsKey = "CreateBusinessPartnersDialogSearchColumns";
        private const string SearchTextKey = "CreateBusinessPartnersDialogSearchText";
        private const string EditColumnsKey = "CreateBusinessPartnersDialogEditColumns";
        private const string EditTextKey = "CreateBusinessPartnersDialogEditText";
        private const string SelectedRecordKey = "CreateBusinessPartnersDialogSelectedRecordKey";

        private readonly IBusinessPartnersService _businesspartnersService;
        BusinessPartnersPost _businesspartnersPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit businesspartners" };
        string[] edit = { "Edit businesspartners" };
        string[] details = { "Display businesspartners" };
        string[] delete = { "Delete businesspartners" };

        public CreateBusinessPartnersDialog(string id, IBusinessPartnersService businesspartnersService, BusinessPartnersPost businesspartnersPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_businesspartnersService.VerifyBusinessPartnerUnique(0L, value))
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


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             BusinessPartnerPrompt,
              BusinessPartnerTypePrompt,
              CompanyPrompt,
              AddressPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> BusinessPartnerPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new BusinessPartnersPost();

            return await step.PromptAsync(
                BusinessPartnerPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a BusinessPartner:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> BusinessPartnerTypePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sBusinessPartner = (string)step.Result;
            ((BusinessPartnersPost)step.Values[DialogKey]).sBusinessPartner = sBusinessPartner;

            return await step.PromptAsync(
                BusinessPartnerTypePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a BusinessPartnerType:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_businesspartnersService.selectBusinessPartnerTypes().Select(ct => ct.sBusinessPartnerType).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CompanyPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _BusinessPartnerType = (FoundChoice)step.Result;
            var ixBusinessPartnerType = _businesspartnersService.selectBusinessPartnerTypes().Where(ct => ct.sBusinessPartnerType == _BusinessPartnerType.Value).Select(ct => ct.ixBusinessPartnerType).First();
            ((BusinessPartnersPost)step.Values[DialogKey]).ixBusinessPartnerType = ixBusinessPartnerType;

            return await step.PromptAsync(
                CompanyPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Company:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_businesspartnersService.selectCompanies().Select(ct => ct.sCompany).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> AddressPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Company = (FoundChoice)step.Result;
            var ixCompany = _businesspartnersService.selectCompanies().Where(ct => ct.sCompany == _Company.Value).Select(ct => ct.ixCompany).First();
            ((BusinessPartnersPost)step.Values[DialogKey]).ixCompany = ixCompany;

            return await step.PromptAsync(
                AddressPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Address:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_businesspartnersService.selectAddresses().Select(ct => ct.sAddress).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixAddress = (Int64)step.Result;
            ((BusinessPartnersPost)step.Values[DialogKey]).ixAddress = ixAddress;


            return await step.EndDialogAsync(
                (BusinessPartnersPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


