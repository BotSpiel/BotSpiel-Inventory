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
    public class CreateOutboundCarrierManifestPickupsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateOutboundCarrierManifestPickupsDialogId = "createOutboundCarrierManifestPickupsDialog";
       private const string OutboundCarrierManifestPromptId = "outboundcarriermanifestPrompt";
        private const string StatusPromptId = "statusPrompt";

        private const string DialogKey = nameof(CreateOutboundCarrierManifestPickupsDialog);
        private const string DialogKeyOptions = "createOutboundCarrierManifestPickupsDialogOptions";
        private const string SearchColumnsKey = "CreateOutboundCarrierManifestPickupsDialogSearchColumns";
        private const string SearchTextKey = "CreateOutboundCarrierManifestPickupsDialogSearchText";
        private const string EditColumnsKey = "CreateOutboundCarrierManifestPickupsDialogEditColumns";
        private const string EditTextKey = "CreateOutboundCarrierManifestPickupsDialogEditText";
        private const string SelectedRecordKey = "CreateOutboundCarrierManifestPickupsDialogSelectedRecordKey";

        private readonly IOutboundCarrierManifestPickupsService _outboundcarriermanifestpickupsService;
        OutboundCarrierManifestPickupsPost _outboundcarriermanifestpickupsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundcarriermanifestpickups" };
        string[] edit = { "Edit outboundcarriermanifestpickups" };
        string[] details = { "Display outboundcarriermanifestpickups" };
        string[] delete = { "Delete outboundcarriermanifestpickups" };

        public CreateOutboundCarrierManifestPickupsDialog(string id, IOutboundCarrierManifestPickupsService outboundcarriermanifestpickupsService, OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _outboundcarriermanifestpickupsService = outboundcarriermanifestpickupsService;
            _outboundcarriermanifestpickupsPost = outboundcarriermanifestpickupsPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> outboundcarriermanifestpickupValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_outboundcarriermanifestpickupsService.VerifyOutboundCarrierManifestPickupUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The outboundcarriermanifestpickup {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new ChoicePrompt(OutboundCarrierManifestPromptId));
            AddDialog(new ChoicePrompt(StatusPromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             OutboundCarrierManifestPrompt,
              StatusPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> OutboundCarrierManifestPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new OutboundCarrierManifestPickupsPost();

            return await step.PromptAsync(
                OutboundCarrierManifestPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a OutboundCarrierManifest:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundcarriermanifestpickupsService.selectOutboundCarrierManifests().Select(ct => ct.sOutboundCarrierManifest).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StatusPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _OutboundCarrierManifest = (FoundChoice)step.Result;
            var ixOutboundCarrierManifest = _outboundcarriermanifestpickupsService.selectOutboundCarrierManifests().Where(ct => ct.sOutboundCarrierManifest == _OutboundCarrierManifest.Value).Select(ct => ct.ixOutboundCarrierManifest).First();
            ((OutboundCarrierManifestPickupsPost)step.Values[DialogKey]).ixOutboundCarrierManifest = ixOutboundCarrierManifest;

            return await step.PromptAsync(
                StatusPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Status:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundcarriermanifestpickupsService.selectStatuses().Select(ct => ct.sStatus).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixStatus = (Int64)step.Result;
            ((OutboundCarrierManifestPickupsPost)step.Values[DialogKey]).ixStatus = ixStatus;


            return await step.EndDialogAsync(
                (OutboundCarrierManifestPickupsPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


