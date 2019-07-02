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
    public class DeleteInboundOrdersDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteInboundOrdersDialogId = "deleteInboundOrdersDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteInboundOrdersDialog);
        private const string DialogKeyOptions = "deleteInboundOrdersDialogOptions";
        private const string SearchColumnsKey = "DeleteInboundOrdersDialogSearchColumns";
        private const string SearchTextKey = "DeleteInboundOrdersDialogSearchText";
        private const string EditColumnsKey = "DeleteInboundOrdersDialogEditColumns";
        private const string EditTextKey = "DeleteInboundOrdersDialogEditText";
        private const string SelectedRecordKey = "DeleteInboundOrdersDialogSelectedRecordKey";

        private readonly IInboundOrdersService _inboundordersService;
        InboundOrdersPost _inboundordersPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inboundorders" };
        string[] edit = { "Edit inboundorders" };
        string[] details = { "Display inboundorders" };
        string[] delete = { "Delete inboundorders" };

        public DeleteInboundOrdersDialog(string id, IInboundOrdersService inboundordersService, InboundOrdersPost inboundordersPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inboundordersService = inboundordersService;
            _inboundordersPost = inboundordersPost;

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
            step.Values[DialogKey] = new InboundOrdersPost();
            step.Values[DialogKeyOptions] = (InboundOrdersPost)step.Options;
            ((InboundOrdersPost)step.Values[DialogKey]).ixInboundOrder = ((InboundOrdersPost)step.Values[DialogKeyOptions]).ixInboundOrder;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((InboundOrdersPost)step.Options).sInboundOrder}:"),
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
                ((InboundOrdersPost)step.Values[DialogKey]).ixInboundOrder = -1;
            }

            return await step.EndDialogAsync(
                (InboundOrdersPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


