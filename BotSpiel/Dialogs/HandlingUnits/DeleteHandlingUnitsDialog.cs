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
    public class DeleteHandlingUnitsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteHandlingUnitsDialogId = "deleteHandlingUnitsDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteHandlingUnitsDialog);
        private const string DialogKeyOptions = "deleteHandlingUnitsDialogOptions";
        private const string SearchColumnsKey = "DeleteHandlingUnitsDialogSearchColumns";
        private const string SearchTextKey = "DeleteHandlingUnitsDialogSearchText";
        private const string EditColumnsKey = "DeleteHandlingUnitsDialogEditColumns";
        private const string EditTextKey = "DeleteHandlingUnitsDialogEditText";
        private const string SelectedRecordKey = "DeleteHandlingUnitsDialogSelectedRecordKey";

        private readonly IHandlingUnitsService _handlingunitsService;
        HandlingUnitsPost _handlingunitsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit handlingunits" };
        string[] edit = { "Edit handlingunits" };
        string[] details = { "Display handlingunits" };
        string[] delete = { "Delete handlingunits" };

        public DeleteHandlingUnitsDialog(string id, IHandlingUnitsService handlingunitsService, HandlingUnitsPost handlingunitsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _handlingunitsService = handlingunitsService;
            _handlingunitsPost = handlingunitsPost;

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
            step.Values[DialogKey] = new HandlingUnitsPost();
            step.Values[DialogKeyOptions] = (HandlingUnitsPost)step.Options;
            ((HandlingUnitsPost)step.Values[DialogKey]).ixHandlingUnit = ((HandlingUnitsPost)step.Values[DialogKeyOptions]).ixHandlingUnit;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((HandlingUnitsPost)step.Options).sHandlingUnit}:"),
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
                ((HandlingUnitsPost)step.Values[DialogKey]).ixHandlingUnit = -1;
            }

            return await step.EndDialogAsync(
                (HandlingUnitsPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


