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
    public class DeleteOutboundCarrierManifestsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteOutboundCarrierManifestsDialogId = "deleteOutboundCarrierManifestsDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteOutboundCarrierManifestsDialog);
        private const string DialogKeyOptions = "deleteOutboundCarrierManifestsDialogOptions";
        private const string SearchColumnsKey = "DeleteOutboundCarrierManifestsDialogSearchColumns";
        private const string SearchTextKey = "DeleteOutboundCarrierManifestsDialogSearchText";
        private const string EditColumnsKey = "DeleteOutboundCarrierManifestsDialogEditColumns";
        private const string EditTextKey = "DeleteOutboundCarrierManifestsDialogEditText";
        private const string SelectedRecordKey = "DeleteOutboundCarrierManifestsDialogSelectedRecordKey";

        private readonly IOutboundCarrierManifestsService _outboundcarriermanifestsService;
        OutboundCarrierManifestsPost _outboundcarriermanifestsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundcarriermanifests" };
        string[] edit = { "Edit outboundcarriermanifests" };
        string[] details = { "Display outboundcarriermanifests" };
        string[] delete = { "Delete outboundcarriermanifests" };

        public DeleteOutboundCarrierManifestsDialog(string id, IOutboundCarrierManifestsService outboundcarriermanifestsService, OutboundCarrierManifestsPost outboundcarriermanifestsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _outboundcarriermanifestsService = outboundcarriermanifestsService;
            _outboundcarriermanifestsPost = outboundcarriermanifestsPost;

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
            step.Values[DialogKey] = new OutboundCarrierManifestsPost();
            step.Values[DialogKeyOptions] = (OutboundCarrierManifestsPost)step.Options;
            ((OutboundCarrierManifestsPost)step.Values[DialogKey]).ixOutboundCarrierManifest = ((OutboundCarrierManifestsPost)step.Values[DialogKeyOptions]).ixOutboundCarrierManifest;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((OutboundCarrierManifestsPost)step.Options).sOutboundCarrierManifest}:"),
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
                ((OutboundCarrierManifestsPost)step.Values[DialogKey]).ixOutboundCarrierManifest = -1;
            }

            return await step.EndDialogAsync(
                (OutboundCarrierManifestsPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


