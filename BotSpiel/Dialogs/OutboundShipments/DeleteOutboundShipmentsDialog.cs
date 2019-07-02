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
    public class DeleteOutboundShipmentsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteOutboundShipmentsDialogId = "deleteOutboundShipmentsDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteOutboundShipmentsDialog);
        private const string DialogKeyOptions = "deleteOutboundShipmentsDialogOptions";
        private const string SearchColumnsKey = "DeleteOutboundShipmentsDialogSearchColumns";
        private const string SearchTextKey = "DeleteOutboundShipmentsDialogSearchText";
        private const string EditColumnsKey = "DeleteOutboundShipmentsDialogEditColumns";
        private const string EditTextKey = "DeleteOutboundShipmentsDialogEditText";
        private const string SelectedRecordKey = "DeleteOutboundShipmentsDialogSelectedRecordKey";

        private readonly IOutboundShipmentsService _outboundshipmentsService;
        OutboundShipmentsPost _outboundshipmentsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundshipments" };
        string[] edit = { "Edit outboundshipments" };
        string[] details = { "Display outboundshipments" };
        string[] delete = { "Delete outboundshipments" };

        public DeleteOutboundShipmentsDialog(string id, IOutboundShipmentsService outboundshipmentsService, OutboundShipmentsPost outboundshipmentsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _outboundshipmentsService = outboundshipmentsService;
            _outboundshipmentsPost = outboundshipmentsPost;

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
            step.Values[DialogKey] = new OutboundShipmentsPost();
            step.Values[DialogKeyOptions] = (OutboundShipmentsPost)step.Options;
            ((OutboundShipmentsPost)step.Values[DialogKey]).ixOutboundShipment = ((OutboundShipmentsPost)step.Values[DialogKeyOptions]).ixOutboundShipment;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((OutboundShipmentsPost)step.Options).sOutboundShipment}:"),
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
                ((OutboundShipmentsPost)step.Values[DialogKey]).ixOutboundShipment = -1;
            }

            return await step.EndDialogAsync(
                (OutboundShipmentsPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


