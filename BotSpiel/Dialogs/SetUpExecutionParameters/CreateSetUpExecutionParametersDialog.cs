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
    public class CreateSetUpExecutionParametersDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateSetUpExecutionParametersDialogId = "createSetUpExecutionParametersDialog";
       private const string FacilityPromptId = "facilityPrompt";
        private const string CompanyPromptId = "companyPrompt";
        private const string FacilityWorkAreaPromptId = "facilityworkareaPrompt";

        private const string DialogKey = nameof(CreateSetUpExecutionParametersDialog);
        private const string DialogKeyOptions = "createSetUpExecutionParametersDialogOptions";
        private const string SearchColumnsKey = "CreateSetUpExecutionParametersDialogSearchColumns";
        private const string SearchTextKey = "CreateSetUpExecutionParametersDialogSearchText";
        private const string EditColumnsKey = "CreateSetUpExecutionParametersDialogEditColumns";
        private const string EditTextKey = "CreateSetUpExecutionParametersDialogEditText";
        private const string SelectedRecordKey = "CreateSetUpExecutionParametersDialogSelectedRecordKey";

        private readonly ISetUpExecutionParametersService _setupexecutionparametersService;
        SetUpExecutionParametersPost _setupexecutionparametersPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit setupexecutionparameters" };
        string[] edit = { "Edit setupexecutionparameters" };
        string[] details = { "Display setupexecutionparameters" };
        string[] delete = { "Delete setupexecutionparameters" };

        public CreateSetUpExecutionParametersDialog(string id, ISetUpExecutionParametersService setupexecutionparametersService, SetUpExecutionParametersPost setupexecutionparametersPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _setupexecutionparametersService = setupexecutionparametersService;
            _setupexecutionparametersPost = setupexecutionparametersPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> setupexecutionparameterValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_setupexecutionparametersService.VerifySetUpExecutionParameterUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The setupexecutionparameter {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new ChoicePrompt(FacilityPromptId));
            AddDialog(new ChoicePrompt(CompanyPromptId));
            AddDialog(new ChoicePrompt(FacilityWorkAreaPromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             FacilityPrompt,
              CompanyPrompt,
              FacilityWorkAreaPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> FacilityPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new SetUpExecutionParametersPost();

            return await step.PromptAsync(
                FacilityPromptId,
                new PromptOptions
                {
                    //Custom Code Start | Replaced Code Block
                    //Replaced Code Block Start
                    //Prompt = MessageFactory.Text($"Please enter a Facility:"),
                    //Replaced Code Block End
                    Prompt = MessageFactory.Text($"Let's setup your organization defaults first.{Environment.NewLine} Which facility are you working in?{Environment.NewLine}"),
                    //Custom Code End
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(_setupexecutionparametersService.selectFacilities().Select(ct => ct.sFacility).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CompanyPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Facility = (FoundChoice)step.Result;
            var ixFacility = _setupexecutionparametersService.selectFacilities().Where(ct => ct.sFacility == _Facility.Value).Select(ct => ct.ixFacility).First();
            ((SetUpExecutionParametersPost)step.Values[DialogKey]).ixFacility = ixFacility;

            return await step.PromptAsync(
                CompanyPromptId,
                new PromptOptions
                {
                    //Custom Code Start | Replaced Code Block
                    //Replaced Code Block Start
                    //Prompt = MessageFactory.Text($"Please enter a Company:"),
                    //Replaced Code Block End
                    Prompt = MessageFactory.Text($"Which company are you working in?{Environment.NewLine}"),
                    //Custom Code End
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(_setupexecutionparametersService.selectCompanies().Select(ct => ct.sCompany).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> FacilityWorkAreaPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Company = (FoundChoice)step.Result;
            var ixCompany = _setupexecutionparametersService.selectCompanies().Where(ct => ct.sCompany == _Company.Value).Select(ct => ct.ixCompany).First();
            ((SetUpExecutionParametersPost)step.Values[DialogKey]).ixCompany = ixCompany;

            return await step.PromptAsync(
                FacilityWorkAreaPromptId,
                new PromptOptions
                {

                    //Custom Code Start | Replaced Code Block
                    //Replaced Code Block Start
                    //Prompt = MessageFactory.Text($"Please enter a FacilityWorkArea:"),
                    //Replaced Code Block End
                    Prompt = MessageFactory.Text($"Which work area are you working in?{Environment.NewLine}"),
                    //Custom Code End
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(_setupexecutionparametersService.selectFacilityWorkAreas().Select(ct => ct.sFacilityWorkArea).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _FacilityWorkArea = (FoundChoice)step.Result;
            var ixFacilityWorkArea = _setupexecutionparametersService.selectFacilityWorkAreas().Where(ct => ct.sFacilityWorkArea == _FacilityWorkArea.Value).Select(ct => ct.ixFacilityWorkArea).First();
            ((SetUpExecutionParametersPost)step.Values[DialogKey]).ixFacilityWorkArea = ixFacilityWorkArea;

            return await step.EndDialogAsync(
                (SetUpExecutionParametersPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


