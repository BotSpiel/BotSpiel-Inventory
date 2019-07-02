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
    public class DeleteCompaniesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteCompaniesDialogId = "deleteCompaniesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteCompaniesDialog);
        private const string DialogKeyOptions = "deleteCompaniesDialogOptions";
        private const string SearchColumnsKey = "DeleteCompaniesDialogSearchColumns";
        private const string SearchTextKey = "DeleteCompaniesDialogSearchText";
        private const string EditColumnsKey = "DeleteCompaniesDialogEditColumns";
        private const string EditTextKey = "DeleteCompaniesDialogEditText";
        private const string SelectedRecordKey = "DeleteCompaniesDialogSelectedRecordKey";

        private readonly ICompaniesService _companiesService;
        CompaniesPost _companiesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit companies" };
        string[] edit = { "Edit companies" };
        string[] details = { "Display companies" };
        string[] delete = { "Delete companies" };

        public DeleteCompaniesDialog(string id, ICompaniesService companiesService, CompaniesPost companiesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _companiesService = companiesService;
            _companiesPost = companiesPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
              confirmDeletePrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> confirmDeletePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new CompaniesPost();
            step.Values[DialogKeyOptions] = (CompaniesPost)step.Options;
            ((CompaniesPost)step.Values[DialogKey]).ixCompany = ((CompaniesPost)step.Values[DialogKeyOptions]).ixCompany;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((CompaniesPost)step.Options).sCompany}:"),
                    RetryPrompt = MessageFactory.Text("Please choose a valid option."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var yesNo = (bool)step.Result;

            if (!yesNo)
            {
                ((CompaniesPost)step.Values[DialogKey]).ixCompany = -1;
            }

            return await step.EndDialogAsync(
                (CompaniesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


