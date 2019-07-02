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
    public class EditCompaniesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditCompaniesDialogId = "editCompaniesDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string CompanyPromptId = "companyPrompt";

        private const string DialogKey = nameof(EditCompaniesDialog);
        private const string DialogKeyOptions = "editCompaniesDialogOptions";
        private const string SearchColumnsKey = "EditCompaniesDialogSearchColumns";
        private const string SearchTextKey = "EditCompaniesDialogSearchText";
        private const string EditColumnsKey = "EditCompaniesDialogEditColumns";
        private const string EditTextKey = "EditCompaniesDialogEditText";
        private const string SelectedRecordKey = "EditCompaniesDialogSelectedRecordKey";

        private readonly ICompaniesService _companiesService;
        CompaniesPost _companiesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit companies" };
        string[] edit = { "Edit companies" };
        string[] details = { "Display companies" };
        string[] delete = { "Delete companies" };

        public EditCompaniesDialog(string id, ICompaniesService companiesService, CompaniesPost companiesPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_companiesService.VerifyCompanyUnique(_companiesPost.ixCompany, value))
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

            step.Values[DialogKey] = new CompaniesPost();
            step.Values[DialogKeyOptions] = (CompaniesPost)step.Options;
            step.Values[DialogKey] = _companiesService.GetPost(((CompaniesPost)step.Options).ixCompany);
            _companiesPost = _companiesService.GetPost(((CompaniesPost)step.Options).ixCompany);
            step.Values[SelectedRecordKey] = _companiesPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("Companies");

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
                case "Company":
					returnResult = await step.PromptAsync(
						CompanyPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Company:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
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
                case "Company":
					var sCompany = (string)step.Result;
					((CompaniesPost)step.Values[DialogKey]).sCompany = sCompany;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (CompaniesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


