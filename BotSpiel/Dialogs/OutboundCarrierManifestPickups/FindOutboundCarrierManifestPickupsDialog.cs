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
    public class FindOutboundCarrierManifestPickupsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditOutboundCarrierManifestPickupsDialogId = "editOutboundCarrierManifestPickupsDialog";
        private const string DetailsOutboundCarrierManifestPickupsDialogId = "detailsOutboundCarrierManifestPickupsDialog";
        private const string DeleteOutboundCarrierManifestPickupsDialogId = "deleteOutboundCarrierManifestPickupsDialog";

        private const string FindOutboundCarrierManifestPickupsDialogId = "findOutboundCarrierManifestPickupsDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindOutboundCarrierManifestPickupsDialog);
        private const string DialogKeyOptions = "findOutboundCarrierManifestPickupsDialogOptions";
        private const string SearchColumnsKey = "FindOutboundCarrierManifestPickupsDialogSearchColumns";
        private const string SearchTextKey = "FindOutboundCarrierManifestPickupsDialogSearchText";
        private const string EditColumnsKey = "FindOutboundCarrierManifestPickupsDialogEditColumns";
        private const string EditTextKey = "FindOutboundCarrierManifestPickupsDialogEditText";
        private const string SelectedRecordKey = "FindOutboundCarrierManifestPickupsDialogSelectedRecordKey";

        private readonly IOutboundCarrierManifestPickupsService _outboundcarriermanifestpickupsService;
        OutboundCarrierManifestPickupsPost _outboundcarriermanifestpickupsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundcarriermanifestpickups" };
        string[] edit = { "Edit outboundcarriermanifestpickups" };
        string[] details = { "Display outboundcarriermanifestpickups" };
        string[] delete = { "Delete outboundcarriermanifestpickups" };

        public FindOutboundCarrierManifestPickupsDialog(string id, IOutboundCarrierManifestPickupsService outboundcarriermanifestpickupsService, OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost, BotSpielUserStateAccessors statePropertyAccessor)
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

            AddDialog(new EditOutboundCarrierManifestPickupsDialog(EditOutboundCarrierManifestPickupsDialogId, _outboundcarriermanifestpickupsService, _outboundcarriermanifestpickupsPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteOutboundCarrierManifestPickupsDialog(DeleteOutboundCarrierManifestPickupsDialogId, _outboundcarriermanifestpickupsService, _outboundcarriermanifestpickupsPost, _botSpielUserStateAccessors));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             chooseSearchColumnPrompt,
             enterSearchTextPrompt,
             selectFromResultPrompt,
             editDeleteDetailsPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }

        private async Task<DialogTurnResult> chooseSearchColumnPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            string searchColumn = "";
            string searchText = "";

            step.Values[DialogKey] = new OutboundCarrierManifestPickupsPost();
            step.Values[SelectedRecordKey] = _outboundcarriermanifestpickupsPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("OutboundCarrierManifestPickups");

            return await step.PromptAsync(
                ChoicePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Please choose a column to search on:"),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(entitySearchColumns),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> enterSearchTextPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice searchColumn = (FoundChoice)step.Result;
            step.Values[SearchColumnsKey] = searchColumn.Value;

            return await step.PromptAsync(
                TextPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter text to search for in {step.Values[SearchColumnsKey]}:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }


        private async Task<DialogTurnResult> selectFromResultPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[SearchTextKey] = (string)step.Result;
            var outboundcarriermanifestpickupsIndex = _outboundcarriermanifestpickupsService.Index();
            var recordCountTotal = outboundcarriermanifestpickupsIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {

                default:
                    break;
            }

            return returnResult;
        }



        private async Task<DialogTurnResult> editDeleteDetailsPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            DialogTurnResult returnResult = new DialogTurnResult(0);
            var outboundcarriermanifestpickupsIndex = _outboundcarriermanifestpickupsService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit outboundcarriermanifestpickups"))
            {

                if (selection.Value == "Refine search")
                {
                    ((OutboundCarrierManifestPickupsPost)step.Values[DialogKey]).ixOutboundCarrierManifestPickup = 0;
                }
                else if (selection.Value == "Exit outboundcarriermanifestpickups")
                {
                    ((OutboundCarrierManifestPickupsPost)step.Values[DialogKey]).ixOutboundCarrierManifestPickup = -1;
                }
                returnResult = await step.EndDialogAsync(
                (OutboundCarrierManifestPickupsPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _outboundcarriermanifestpickupsService.GetPost(outboundcarriermanifestpickupsIndex.Where(o => o.sOutboundCarrierManifestPickup == selection.Value).Select(o => o.ixOutboundCarrierManifestPickup).First());
                returnResult = await step.PromptAsync(
                    ChoicePromptId,
                    new PromptOptions
                    {
                        Prompt = MessageFactory.Text($"Would you like to edit, display or delete {selection.Value}. Please choose an option or exit to continue."),
                        RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                        Choices = ChoiceFactory.ToChoices(edit.Union(details).Union(delete).Union(exit).ToList())
                    },
                    cancellationToken);
            }
            return returnResult;
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            DialogTurnResult returnResult = new DialogTurnResult(0);

            var currentBotUserData = await _botSpielUserStateAccessors.BotUserDataAccessor.GetAsync(step.Context, () => _botUserData);
            var outboundcarriermanifestpickupsIndex = _outboundcarriermanifestpickupsService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit outboundcarriermanifestpickups")
            {
                ((OutboundCarrierManifestPickupsPost)step.Values[DialogKey]).ixOutboundCarrierManifestPickup = -1;
                returnResult = await step.EndDialogAsync(
                (OutboundCarrierManifestPickupsPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit outboundcarriermanifestpickups") || (selection.Value == "Display outboundcarriermanifestpickups") || (selection.Value == "Delete outboundcarriermanifestpickups"))
            {
                currentBotUserData.ixOutboundCarrierManifestPickup = ((OutboundCarrierManifestPickupsPost)step.Values[SelectedRecordKey]).ixOutboundCarrierManifestPickup;
                switch (selection.Value)
                {
                    case "Edit outboundcarriermanifestpickups":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditOutboundCarrierManifestPickupsDialogId, (OutboundCarrierManifestPickupsPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display outboundcarriermanifestpickups":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete outboundcarriermanifestpickups":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteOutboundCarrierManifestPickupsDialogId, (OutboundCarrierManifestPickupsPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (OutboundCarrierManifestPickupsPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


