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
    public class DeleteMoveQueuesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteMoveQueuesDialogId = "deleteMoveQueuesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteMoveQueuesDialog);
        private const string DialogKeyOptions = "deleteMoveQueuesDialogOptions";
        private const string SearchColumnsKey = "DeleteMoveQueuesDialogSearchColumns";
        private const string SearchTextKey = "DeleteMoveQueuesDialogSearchText";
        private const string EditColumnsKey = "DeleteMoveQueuesDialogEditColumns";
        private const string EditTextKey = "DeleteMoveQueuesDialogEditText";
        private const string SelectedRecordKey = "DeleteMoveQueuesDialogSelectedRecordKey";

        private readonly IMoveQueuesService _movequeuesService;
        MoveQueuesPost _movequeuesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit movequeues" };
        string[] edit = { "Edit movequeues" };
        string[] details = { "Display movequeues" };
        string[] delete = { "Delete movequeues" };

        public DeleteMoveQueuesDialog(string id, IMoveQueuesService movequeuesService, MoveQueuesPost movequeuesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _movequeuesService = movequeuesService;
            _movequeuesPost = movequeuesPost;

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
            step.Values[DialogKey] = new MoveQueuesPost();
            step.Values[DialogKeyOptions] = (MoveQueuesPost)step.Options;
            ((MoveQueuesPost)step.Values[DialogKey]).ixMoveQueue = ((MoveQueuesPost)step.Values[DialogKeyOptions]).ixMoveQueue;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((MoveQueuesPost)step.Options).sMoveQueue}:"),
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
                ((MoveQueuesPost)step.Values[DialogKey]).ixMoveQueue = -1;
            }

            return await step.EndDialogAsync(
                (MoveQueuesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


