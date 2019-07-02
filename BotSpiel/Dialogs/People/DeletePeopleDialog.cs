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
    public class DeletePeopleDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeletePeopleDialogId = "deletePeopleDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeletePeopleDialog);
        private const string DialogKeyOptions = "deletePeopleDialogOptions";
        private const string SearchColumnsKey = "DeletePeopleDialogSearchColumns";
        private const string SearchTextKey = "DeletePeopleDialogSearchText";
        private const string EditColumnsKey = "DeletePeopleDialogEditColumns";
        private const string EditTextKey = "DeletePeopleDialogEditText";
        private const string SelectedRecordKey = "DeletePeopleDialogSelectedRecordKey";

        private readonly IPeopleService _peopleService;
        PeoplePost _peoplePost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit people" };
        string[] edit = { "Edit people" };
        string[] details = { "Display people" };
        string[] delete = { "Delete people" };

        public DeletePeopleDialog(string id, IPeopleService peopleService, PeoplePost peoplePost, BotSpielUserStateAccessors statePropertyAccessor)
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
            step.Values[DialogKey] = new PeoplePost();
            step.Values[DialogKeyOptions] = (PeoplePost)step.Options;
            ((PeoplePost)step.Values[DialogKey]).ixPerson = ((PeoplePost)step.Values[DialogKeyOptions]).ixPerson;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((PeoplePost)step.Options).sPerson}:"),
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
                ((PeoplePost)step.Values[DialogKey]).ixPerson = -1;
            }

            return await step.EndDialogAsync(
                (PeoplePost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


