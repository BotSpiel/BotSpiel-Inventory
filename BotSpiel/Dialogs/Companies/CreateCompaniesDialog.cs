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
    public class CreateCompaniesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateCompaniesDialogId = "createCompaniesDialog";
       private const string CompanyPromptId = "companyPrompt";

        private const string DialogKey = nameof(CreateCompaniesDialog);
        private const string DialogKeyOptions = "createCompaniesDialogOptions";
        private const string SearchColumnsKey = "CreateCompaniesDialogSearchColumns";
        private const string SearchTextKey = "CreateCompaniesDialogSearchText";
        private const string EditColumnsKey = "CreateCompaniesDialogEditColumns";
        private const string EditTextKey = "CreateCompaniesDialogEditText";
        private const string SelectedRecordKey = "CreateCompaniesDialogSelectedRecordKey";

        private readonly ICompaniesService _companiesService;
        CompaniesPost _companiesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit companies" };
        string[] edit = { "Edit companies" };
        string[] details = { "Display companies" };
        string[] delete = { "Delete companies" };

        public CreateCompaniesDialog(string id, ICompaniesService companiesService, CompaniesPost companiesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _companiesService = companiesService;
            _companiesPost = companiesPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> companyValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_companiesService.VerifyCompanyUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The company {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(CompanyPromptId, companyValidator));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             CompanyPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> CompanyPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new CompaniesPost();

            return await step.PromptAsync(
                CompanyPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Company:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sCompany = (string)step.Result;
            ((CompaniesPost)step.Values[DialogKey]).sCompany = sCompany;


            return await step.EndDialogAsync(
                (CompaniesPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


