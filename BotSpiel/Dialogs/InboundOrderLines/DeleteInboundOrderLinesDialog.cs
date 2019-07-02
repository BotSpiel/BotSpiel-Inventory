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
    public class DeleteInboundOrderLinesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteInboundOrderLinesDialogId = "deleteInboundOrderLinesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteInboundOrderLinesDialog);
        private const string DialogKeyOptions = "deleteInboundOrderLinesDialogOptions";
        private const string SearchColumnsKey = "DeleteInboundOrderLinesDialogSearchColumns";
        private const string SearchTextKey = "DeleteInboundOrderLinesDialogSearchText";
        private const string EditColumnsKey = "DeleteInboundOrderLinesDialogEditColumns";
        private const string EditTextKey = "DeleteInboundOrderLinesDialogEditText";
        private const string SelectedRecordKey = "DeleteInboundOrderLinesDialogSelectedRecordKey";

        private readonly IInboundOrderLinesService _inboundorderlinesService;
        InboundOrderLinesPost _inboundorderlinesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inboundorderlines" };
        string[] edit = { "Edit inboundorderlines" };
        string[] details = { "Display inboundorderlines" };
        string[] delete = { "Delete inboundorderlines" };

        public DeleteInboundOrderLinesDialog(string id, IInboundOrderLinesService inboundorderlinesService, InboundOrderLinesPost inboundorderlinesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inboundorderlinesService = inboundorderlinesService;
            _inboundorderlinesPost = inboundorderlinesPost;

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
            step.Values[DialogKey] = new InboundOrderLinesPost();
            step.Values[DialogKeyOptions] = (InboundOrderLinesPost)step.Options;
            ((InboundOrderLinesPost)step.Values[DialogKey]).ixInboundOrderLine = ((InboundOrderLinesPost)step.Values[DialogKeyOptions]).ixInboundOrderLine;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((InboundOrderLinesPost)step.Options).sInboundOrderLine}:"),
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
                ((InboundOrderLinesPost)step.Values[DialogKey]).ixInboundOrderLine = -1;
            }

            return await step.EndDialogAsync(
                (InboundOrderLinesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


