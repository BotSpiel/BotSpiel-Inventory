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
    public class FindPeopleDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditPeopleDialogId = "editPeopleDialog";
        private const string DetailsPeopleDialogId = "detailsPeopleDialog";
        private const string DeletePeopleDialogId = "deletePeopleDialog";

        private const string FindPeopleDialogId = "findPeopleDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindPeopleDialog);
        private const string DialogKeyOptions = "findPeopleDialogOptions";
        private const string SearchColumnsKey = "FindPeopleDialogSearchColumns";
        private const string SearchTextKey = "FindPeopleDialogSearchText";
        private const string EditColumnsKey = "FindPeopleDialogEditColumns";
        private const string EditTextKey = "FindPeopleDialogEditText";
        private const string SelectedRecordKey = "FindPeopleDialogSelectedRecordKey";

        private readonly IPeopleService _peopleService;
        PeoplePost _peoplePost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit people" };
        string[] edit = { "Edit people" };
        string[] details = { "Display people" };
        string[] delete = { "Delete people" };

        public FindPeopleDialog(string id, IPeopleService peopleService, PeoplePost peoplePost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _peopleService = peopleService;
            _peoplePost = peoplePost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditPeopleDialog(EditPeopleDialogId, _peopleService, _peoplePost, _botSpielUserStateAccessors));
            AddDialog(new DeletePeopleDialog(DeletePeopleDialogId, _peopleService, _peoplePost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new PeoplePost();
            step.Values[SelectedRecordKey] = _peoplePost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("People");

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
            var peopleIndex = _peopleService.Index();
            var recordCountTotal = peopleIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "Person":
                    var searchRecordsPerson = peopleIndex.Where(o => o.sPerson.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sPerson).Select(o => o.sPerson.ToString());
                    var recordCountPerson = searchRecordsPerson.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} people. Your search resulted in {recordCountPerson} records. I show the top 15. Please choose a Person or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsPerson.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "FirstName":
                    var searchRecordsFirstName = peopleIndex.Where(o => o.sFirstName.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sPerson).Select(o => o.sPerson.ToString());
                    var recordCountFirstName = searchRecordsFirstName.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} people. Your search resulted in {recordCountFirstName} records. I show the top 15. Please choose a Person or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsFirstName.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "LastName":
                    var searchRecordsLastName = peopleIndex.Where(o => o.sLastName.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sPerson).Select(o => o.sPerson.ToString());
                    var recordCountLastName = searchRecordsLastName.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} people. Your search resulted in {recordCountLastName} records. I show the top 15. Please choose a Person or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsLastName.Take(15).Union(refine).Union(exit).ToList()),
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
            var peopleIndex = _peopleService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit people"))
            {

                if (selection.Value == "Refine search")
                {
                    ((PeoplePost)step.Values[DialogKey]).ixPerson = 0;
                }
                else if (selection.Value == "Exit people")
                {
                    ((PeoplePost)step.Values[DialogKey]).ixPerson = -1;
                }
                returnResult = await step.EndDialogAsync(
                (PeoplePost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _peopleService.GetPost(peopleIndex.Where(o => o.sPerson == selection.Value).Select(o => o.ixPerson).First());
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
            var peopleIndex = _peopleService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit people")
            {
                ((PeoplePost)step.Values[DialogKey]).ixPerson = -1;
                returnResult = await step.EndDialogAsync(
                (PeoplePost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit people") || (selection.Value == "Display people") || (selection.Value == "Delete people"))
            {
                currentBotUserData.ixPerson = ((PeoplePost)step.Values[SelectedRecordKey]).ixPerson;
                switch (selection.Value)
                {
                    case "Edit people":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditPeopleDialogId, (PeoplePost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display people":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete people":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeletePeopleDialogId, (PeoplePost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (PeoplePost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


