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
    public class DeleteOutboundCarrierManifestPickupsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteOutboundCarrierManifestPickupsDialogId = "deleteOutboundCarrierManifestPickupsDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteOutboundCarrierManifestPickupsDialog);
        private const string DialogKeyOptions = "deleteOutboundCarrierManifestPickupsDialogOptions";
        private const string SearchColumnsKey = "DeleteOutboundCarrierManifestPickupsDialogSearchColumns";
        private const string SearchTextKey = "DeleteOutboundCarrierManifestPickupsDialogSearchText";
        private const string EditColumnsKey = "DeleteOutboundCarrierManifestPickupsDialogEditColumns";
        private const string EditTextKey = "DeleteOutboundCarrierManifestPickupsDialogEditText";
        private const string SelectedRecordKey = "DeleteOutboundCarrierManifestPickupsDialogSelectedRecordKey";

        private readonly IOutboundCarrierManifestPickupsService _outboundcarriermanifestpickupsService;
        OutboundCarrierManifestPickupsPost _outboundcarriermanifestpickupsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundcarriermanifestpickups" };
        string[] edit = { "Edit outboundcarriermanifestpickups" };
        string[] details = { "Display outboundcarriermanifestpickups" };
        string[] delete = { "Delete outboundcarriermanifestpickups" };

        public DeleteOutboundCarrierManifestPickupsDialog(string id, IOutboundCarrierManifestPickupsService outboundcarriermanifestpickupsService, OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _outboundcarriermanifestpickupsService = outboundcarriermanifestpickupsService;
            _outboundcarriermanifestpickupsPost = outboundcarriermanifestpickupsPost;

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
            step.Values[DialogKey] = new OutboundCarrierManifestPickupsPost();
            step.Values[DialogKeyOptions] = (OutboundCarrierManifestPickupsPost)step.Options;
            ((OutboundCarrierManifestPickupsPost)step.Values[DialogKey]).ixOutboundCarrierManifestPickup = ((OutboundCarrierManifestPickupsPost)step.Values[DialogKeyOptions]).ixOutboundCarrierManifestPickup;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((OutboundCarrierManifestPickupsPost)step.Options).sOutboundCarrierManifestPickup}:"),
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
                ((OutboundCarrierManifestPickupsPost)step.Values[DialogKey]).ixOutboundCarrierManifestPickup = -1;
            }

            return await step.EndDialogAsync(
                (OutboundCarrierManifestPickupsPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


