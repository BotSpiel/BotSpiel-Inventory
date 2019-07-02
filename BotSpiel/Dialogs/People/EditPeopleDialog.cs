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
    public class EditPeopleDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditPeopleDialogId = "editPeopleDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string PersonPromptId = "personPrompt";
        private const string FirstNamePromptId = "firstnamePrompt";
        private const string LastNamePromptId = "lastnamePrompt";
        private const string LanguagePromptId = "languagePrompt";

        private const string DialogKey = nameof(EditPeopleDialog);
        private const string DialogKeyOptions = "editPeopleDialogOptions";
        private const string SearchColumnsKey = "EditPeopleDialogSearchColumns";
        private const string SearchTextKey = "EditPeopleDialogSearchText";
        private const string EditColumnsKey = "EditPeopleDialogEditColumns";
        private const string EditTextKey = "EditPeopleDialogEditText";
        private const string SelectedRecordKey = "EditPeopleDialogSelectedRecordKey";

        private readonly IPeopleService _peopleService;
        PeoplePost _peoplePost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit people" };
        string[] edit = { "Edit people" };
        string[] details = { "Display people" };
        string[] delete = { "Delete people" };

        public EditPeopleDialog(string id, IPeopleService peopleService, PeoplePost peoplePost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _peopleService = peopleService;
            _peoplePost = peoplePost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> personValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_peopleService.VerifyPersonUnique(_peoplePost.ixPerson, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The person {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(PersonPromptId, personValidator));
            AddDialog(new TextPrompt(FirstNamePromptId));
            AddDialog(new TextPrompt(LastNamePromptId));
            AddDialog(new ChoicePrompt(LanguagePromptId));

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

            step.Values[DialogKey] = new PeoplePost();
            step.Values[DialogKeyOptions] = (PeoplePost)step.Options;
            step.Values[DialogKey] = _peopleService.GetPost(((PeoplePost)step.Options).ixPerson);
            _peoplePost = _peopleService.GetPost(((PeoplePost)step.Options).ixPerson);
            step.Values[SelectedRecordKey] = _peoplePost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("People");

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
                case "Person":
					returnResult = await step.PromptAsync(
						PersonPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Person:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "FirstName":
					returnResult = await step.PromptAsync(
						FirstNamePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a FirstName:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "LastName":
					returnResult = await step.PromptAsync(
						LastNamePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a LastName:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "Language":
					returnResult = await step.PromptAsync(
						LanguagePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Language:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_peopleService.selectLanguages().Select(ct => ct.sLanguage).ToList()),
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
                case "Person":
					var sPerson = (string)step.Result;
					((PeoplePost)step.Values[DialogKey]).sPerson = sPerson;
                    break;
                case "FirstName":
					var sFirstName = (string)step.Result;
					((PeoplePost)step.Values[DialogKey]).sFirstName = sFirstName;
                    break;
                case "LastName":
					var sLastName = (string)step.Result;
					((PeoplePost)step.Values[DialogKey]).sLastName = sLastName;
                    break;
                case "Language":
					FoundChoice _Language = (FoundChoice)step.Result;
					var ixLanguage = _peopleService.selectLanguages().Where(ct => ct.sLanguage == _Language.Value).Select(ct => ct.ixLanguage).First();
					((PeoplePost)step.Values[DialogKey]).ixLanguage = ixLanguage;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (PeoplePost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


