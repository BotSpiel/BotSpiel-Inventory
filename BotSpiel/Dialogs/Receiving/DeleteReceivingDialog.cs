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
    public class DeleteReceivingDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteReceivingDialogId = "deleteReceivingDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteReceivingDialog);
        private const string DialogKeyOptions = "deleteReceivingDialogOptions";
        private const string SearchColumnsKey = "DeleteReceivingDialogSearchColumns";
        private const string SearchTextKey = "DeleteReceivingDialogSearchText";
        private const string EditColumnsKey = "DeleteReceivingDialogEditColumns";
        private const string EditTextKey = "DeleteReceivingDialogEditText";
        private const string SelectedRecordKey = "DeleteReceivingDialogSelectedRecordKey";

        private readonly IReceivingService _receivingService;
        ReceivingPost _receivingPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit receiving" };
        string[] edit = { "Edit receiving" };
        string[] details = { "Display receiving" };
        string[] delete = { "Delete receiving" };

        public DeleteReceivingDialog(string id, IReceivingService receivingService, ReceivingPost receivingPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _receivingService = receivingService;
            _receivingPost = receivingPost;

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
            step.Values[DialogKey] = new ReceivingPost();
            step.Values[DialogKeyOptions] = (ReceivingPost)step.Options;
            ((ReceivingPost)step.Values[DialogKey]).ixReceipt = ((ReceivingPost)step.Values[DialogKeyOptions]).ixReceipt;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((ReceivingPost)step.Options).sReceipt}:"),
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
                ((ReceivingPost)step.Values[DialogKey]).ixReceipt = -1;
            }

            return await step.EndDialogAsync(
                (ReceivingPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


