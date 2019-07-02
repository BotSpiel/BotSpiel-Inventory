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
    public class DeleteOutboundOrderLinesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteOutboundOrderLinesDialogId = "deleteOutboundOrderLinesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteOutboundOrderLinesDialog);
        private const string DialogKeyOptions = "deleteOutboundOrderLinesDialogOptions";
        private const string SearchColumnsKey = "DeleteOutboundOrderLinesDialogSearchColumns";
        private const string SearchTextKey = "DeleteOutboundOrderLinesDialogSearchText";
        private const string EditColumnsKey = "DeleteOutboundOrderLinesDialogEditColumns";
        private const string EditTextKey = "DeleteOutboundOrderLinesDialogEditText";
        private const string SelectedRecordKey = "DeleteOutboundOrderLinesDialogSelectedRecordKey";

        private readonly IOutboundOrderLinesService _outboundorderlinesService;
        OutboundOrderLinesPost _outboundorderlinesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundorderlines" };
        string[] edit = { "Edit outboundorderlines" };
        string[] details = { "Display outboundorderlines" };
        string[] delete = { "Delete outboundorderlines" };

        public DeleteOutboundOrderLinesDialog(string id, IOutboundOrderLinesService outboundorderlinesService, OutboundOrderLinesPost outboundorderlinesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _outboundorderlinesService = outboundorderlinesService;
            _outboundorderlinesPost = outboundorderlinesPost;

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
            step.Values[DialogKey] = new OutboundOrderLinesPost();
            step.Values[DialogKeyOptions] = (OutboundOrderLinesPost)step.Options;
            ((OutboundOrderLinesPost)step.Values[DialogKey]).ixOutboundOrderLine = ((OutboundOrderLinesPost)step.Values[DialogKeyOptions]).ixOutboundOrderLine;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((OutboundOrderLinesPost)step.Options).sOutboundOrderLine}:"),
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
                ((OutboundOrderLinesPost)step.Values[DialogKey]).ixOutboundOrderLine = -1;
            }

            return await step.EndDialogAsync(
                (OutboundOrderLinesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


