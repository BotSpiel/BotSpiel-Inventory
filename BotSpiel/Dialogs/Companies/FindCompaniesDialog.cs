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
    public class FindCompaniesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditCompaniesDialogId = "editCompaniesDialog";
        private const string DetailsCompaniesDialogId = "detailsCompaniesDialog";
        private const string DeleteCompaniesDialogId = "deleteCompaniesDialog";

        private const string FindCompaniesDialogId = "findCompaniesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindCompaniesDialog);
        private const string DialogKeyOptions = "findCompaniesDialogOptions";
        private const string SearchColumnsKey = "FindCompaniesDialogSearchColumns";
        private const string SearchTextKey = "FindCompaniesDialogSearchText";
        private const string EditColumnsKey = "FindCompaniesDialogEditColumns";
        private const string EditTextKey = "FindCompaniesDialogEditText";
        private const string SelectedRecordKey = "FindCompaniesDialogSelectedRecordKey";

        private readonly ICompaniesService _companiesService;
        CompaniesPost _companiesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit companies" };
        string[] edit = { "Edit companies" };
        string[] details = { "Display companies" };
        string[] delete = { "Delete companies" };

        public FindCompaniesDialog(string id, ICompaniesService companiesService, CompaniesPost companiesPost, BotSpielUserStateAccessors statePropertyAccessor)
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

            AddDialog(new EditCompaniesDialog(EditCompaniesDialogId, _companiesService, _companiesPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteCompaniesDialog(DeleteCompaniesDialogId, _companiesService, _companiesPost, _botSpielUserStateAccessors));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             chooseSearchColumnPrompt,
             enterSearchTextPrompt,
             selectFromResultPrompt,
             editDeleteDetailsPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }

        private async Task<DialogTurnResult> chooseSearchColumnPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            string searchColumn = "";
            string searchText = "";

            step.Values[DialogKey] = new CompaniesPost();
            step.Values[SelectedRecordKey] = _companiesPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("Companies");

            return await step.PromptAsync(
                ChoicePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Please choose a column to search on:"),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(entitySearchColumns),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> enterSearchTextPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice searchColumn = (FoundChoice)step.Result;
            step.Values[SearchColumnsKey] = searchColumn.Value;

            return await step.PromptAsync(
                TextPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter text to search for in {step.Values[SearchColumnsKey]}:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }


        private async Task<DialogTurnResult> selectFromResultPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[SearchTextKey] = (string)step.Result;
            var companiesIndex = _companiesService.Index();
            var recordCountTotal = companiesIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "Company":
                    var searchRecordsCompany = companiesIndex.Where(o => o.sCompany.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sCompany).Select(o => o.sCompany.ToString());
                    var recordCountCompany = searchRecordsCompany.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} companies. Your search resulted in {recordCountCompany} records. I show the top 15. Please choose a Company or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsCompany.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;

                default:
                    break;
            }

            return returnResult;
        }



        private async Task<DialogTurnResult> editDeleteDetailsPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            DialogTurnResult returnResult = new DialogTurnResult(0);
            var companiesIndex = _companiesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit companies"))
            {

                if (selection.Value == "Refine search")
                {
                    ((CompaniesPost)step.Values[DialogKey]).ixCompany = 0;
                }
                else if (selection.Value == "Exit companies")
                {
                    ((CompaniesPost)step.Values[DialogKey]).ixCompany = -1;
                }
                returnResult = await step.EndDialogAsync(
                (CompaniesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _companiesService.GetPost(companiesIndex.Where(o => o.sCompany == selection.Value).Select(o => o.ixCompany).First());
                returnResult = await step.PromptAsync(
                    ChoicePromptId,
                    new PromptOptions
                    {
                        Prompt = MessageFactory.Text($"Would you like to edit, display or delete {selection.Value}. Please choose an option or exit to continue."),
                        RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                        Choices = ChoiceFactory.ToChoices(edit.Union(details).Union(delete).Union(exit).ToList())
                    },
                    cancellationToken);
            }
            return returnResult;
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            DialogTurnResult returnResult = new DialogTurnResult(0);

            var currentBotUserData = await _botSpielUserStateAccessors.BotUserDataAccessor.GetAsync(step.Context, () => _botUserData);
            var companiesIndex = _companiesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit companies")
            {
                ((CompaniesPost)step.Values[DialogKey]).ixCompany = -1;
                returnResult = await step.EndDialogAsync(
                (CompaniesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit companies") || (selection.Value == "Display companies") || (selection.Value == "Delete companies"))
            {
                currentBotUserData.ixCompany = ((CompaniesPost)step.Values[SelectedRecordKey]).ixCompany;
                switch (selection.Value)
                {
                    case "Edit companies":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditCompaniesDialogId, (CompaniesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display companies":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete companies":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteCompaniesDialogId, (CompaniesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (CompaniesPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


