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
    public class CreatePeopleDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreatePeopleDialogId = "createPeopleDialog";
       private const string PersonPromptId = "personPrompt";
        private const string FirstNamePromptId = "firstnamePrompt";
        private const string LastNamePromptId = "lastnamePrompt";
        private const string LanguagePromptId = "languagePrompt";

        private const string DialogKey = nameof(CreatePeopleDialog);
        private const string DialogKeyOptions = "createPeopleDialogOptions";
        private const string SearchColumnsKey = "CreatePeopleDialogSearchColumns";
        private const string SearchTextKey = "CreatePeopleDialogSearchText";
        private const string EditColumnsKey = "CreatePeopleDialogEditColumns";
        private const string EditTextKey = "CreatePeopleDialogEditText";
        private const string SelectedRecordKey = "CreatePeopleDialogSelectedRecordKey";

        private readonly IPeopleService _peopleService;
        PeoplePost _peoplePost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit people" };
        string[] edit = { "Edit people" };
        string[] details = { "Display people" };
        string[] delete = { "Delete people" };

        public CreatePeopleDialog(string id, IPeopleService peopleService, PeoplePost peoplePost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_peopleService.VerifyPersonUnique(0L, value))
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


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             PersonPrompt,
              FirstNamePrompt,
              LastNamePrompt,
              LanguagePrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> PersonPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new PeoplePost();

            return await step.PromptAsync(
                PersonPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Person:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> FirstNamePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sPerson = (string)step.Result;
            ((PeoplePost)step.Values[DialogKey]).sPerson = sPerson;

            return await step.PromptAsync(
                FirstNamePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a FirstName:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> LastNamePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sFirstName = (string)step.Result;
            ((PeoplePost)step.Values[DialogKey]).sFirstName = sFirstName;

            return await step.PromptAsync(
                LastNamePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a LastName:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> LanguagePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sLastName = (string)step.Result;
            ((PeoplePost)step.Values[DialogKey]).sLastName = sLastName;

            return await step.PromptAsync(
                LanguagePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Language:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_peopleService.selectLanguages().Select(ct => ct.sLanguage).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixLanguage = (Int64)step.Result;
            ((PeoplePost)step.Values[DialogKey]).ixLanguage = ixLanguage;


            return await step.EndDialogAsync(
                (PeoplePost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


