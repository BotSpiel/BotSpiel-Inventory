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
    public class EditOutboundCarrierManifestPickupsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditOutboundCarrierManifestPickupsDialogId = "editOutboundCarrierManifestPickupsDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string OutboundCarrierManifestPromptId = "outboundcarriermanifestPrompt";
        private const string StatusPromptId = "statusPrompt";

        private const string DialogKey = nameof(EditOutboundCarrierManifestPickupsDialog);
        private const string DialogKeyOptions = "editOutboundCarrierManifestPickupsDialogOptions";
        private const string SearchColumnsKey = "EditOutboundCarrierManifestPickupsDialogSearchColumns";
        private const string SearchTextKey = "EditOutboundCarrierManifestPickupsDialogSearchText";
        private const string EditColumnsKey = "EditOutboundCarrierManifestPickupsDialogEditColumns";
        private const string EditTextKey = "EditOutboundCarrierManifestPickupsDialogEditText";
        private const string SelectedRecordKey = "EditOutboundCarrierManifestPickupsDialogSelectedRecordKey";

        private readonly IOutboundCarrierManifestPickupsService _outboundcarriermanifestpickupsService;
        OutboundCarrierManifestPickupsPost _outboundcarriermanifestpickupsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundcarriermanifestpickups" };
        string[] edit = { "Edit outboundcarriermanifestpickups" };
        string[] details = { "Display outboundcarriermanifestpickups" };
        string[] delete = { "Delete outboundcarriermanifestpickups" };

        public EditOutboundCarrierManifestPickupsDialog(string id, IOutboundCarrierManifestPickupsService outboundcarriermanifestpickupsService, OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_outboundcarriermanifestpickupsService.VerifyOutboundCarrierManifestPickupUnique(_outboundcarriermanifestpickupsPost.ixOutboundCarrierManifestPickup, value))
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

            AddDialog(new ChoicePrompt(ChoicePromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
              chooseEditColumnPrompt,
              editColumnPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> chooseEditColumnPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            string editColumn = "";
            string editText = "";

            step.Values[DialogKey] = new OutboundCarrierManifestPickupsPost();
            step.Values[DialogKeyOptions] = (OutboundCarrierManifestPickupsPost)step.Options;
            step.Values[DialogKey] = _outboundcarriermanifestpickupsService.GetPost(((OutboundCarrierManifestPickupsPost)step.Options).ixOutboundCarrierManifestPickup);
            _outboundcarriermanifestpickupsPost = _outboundcarriermanifestpickupsService.GetPost(((OutboundCarrierManifestPickupsPost)step.Options).ixOutboundCarrierManifestPickup);
            step.Values[SelectedRecordKey] = _outboundcarriermanifestpickupsPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("OutboundCarrierManifestPickups");

            return await step.PromptAsync(
                ChoicePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Please choose an attribute to change:"),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(entitySearchColumns),
                },
                cancellationToken);
        }



        private async Task<DialogTurnResult> editColumnPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice editColumn = (FoundChoice)step.Result;
            step.Values[EditColumnsKey] = editColumn.Value;
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[EditColumnsKey])
            {
                case "OutboundCarrierManifest":
					returnResult = await step.PromptAsync(
						OutboundCarrierManifestPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a OutboundCarrierManifest:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_outboundcarriermanifestpickupsService.selectOutboundCarrierManifests().Select(ct => ct.sOutboundCarrierManifest).ToList()),
						},
						cancellationToken);
                    break;
                case "Status":
					returnResult = await step.PromptAsync(
						StatusPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Status:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_outboundcarriermanifestpickupsService.selectStatuses().Select(ct => ct.sStatus).ToList()),
						},
						cancellationToken);
                    break;

                default:
                    break;
            }

            return returnResult;
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {

            switch (step.Values[EditColumnsKey])
            {
                case "OutboundCarrierManifest":
					FoundChoice _OutboundCarrierManifest = (FoundChoice)step.Result;
					var ixOutboundCarrierManifest = _outboundcarriermanifestpickupsService.selectOutboundCarrierManifests().Where(ct => ct.sOutboundCarrierManifest == _OutboundCarrierManifest.Value).Select(ct => ct.ixOutboundCarrierManifest).First();
					((OutboundCarrierManifestPickupsPost)step.Values[DialogKey]).ixOutboundCarrierManifest = ixOutboundCarrierManifest;
                    break;
                case "Status":
					FoundChoice _Status = (FoundChoice)step.Result;
					var ixStatus = _outboundcarriermanifestpickupsService.selectStatuses().Where(ct => ct.sStatus == _Status.Value).Select(ct => ct.ixStatus).First();
					((OutboundCarrierManifestPickupsPost)step.Values[DialogKey]).ixStatus = ixStatus;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (OutboundCarrierManifestPickupsPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


