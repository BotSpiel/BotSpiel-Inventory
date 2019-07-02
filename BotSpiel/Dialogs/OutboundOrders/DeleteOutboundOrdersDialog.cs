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
    public class DeleteOutboundOrdersDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteOutboundOrdersDialogId = "deleteOutboundOrdersDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteOutboundOrdersDialog);
        private const string DialogKeyOptions = "deleteOutboundOrdersDialogOptions";
        private const string SearchColumnsKey = "DeleteOutboundOrdersDialogSearchColumns";
        private const string SearchTextKey = "DeleteOutboundOrdersDialogSearchText";
        private const string EditColumnsKey = "DeleteOutboundOrdersDialogEditColumns";
        private const string EditTextKey = "DeleteOutboundOrdersDialogEditText";
        private const string SelectedRecordKey = "DeleteOutboundOrdersDialogSelectedRecordKey";

        private readonly IOutboundOrdersService _outboundordersService;
        OutboundOrdersPost _outboundordersPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundorders" };
        string[] edit = { "Edit outboundorders" };
        string[] details = { "Display outboundorders" };
        string[] delete = { "Delete outboundorders" };

        public DeleteOutboundOrdersDialog(string id, IOutboundOrdersService outboundordersService, OutboundOrdersPost outboundordersPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _outboundordersService = outboundordersService;
            _outboundordersPost = outboundordersPost;

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
            step.Values[DialogKey] = new OutboundOrdersPost();
            step.Values[DialogKeyOptions] = (OutboundOrdersPost)step.Options;
            ((OutboundOrdersPost)step.Values[DialogKey]).ixOutboundOrder = ((OutboundOrdersPost)step.Values[DialogKeyOptions]).ixOutboundOrder;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((OutboundOrdersPost)step.Options).sOutboundOrder}:"),
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
                ((OutboundOrdersPost)step.Values[DialogKey]).ixOutboundOrder = -1;
            }

            return await step.EndDialogAsync(
                (OutboundOrdersPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


