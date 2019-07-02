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
    public class FindOutboundCarrierManifestsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditOutboundCarrierManifestsDialogId = "editOutboundCarrierManifestsDialog";
        private const string DetailsOutboundCarrierManifestsDialogId = "detailsOutboundCarrierManifestsDialog";
        private const string DeleteOutboundCarrierManifestsDialogId = "deleteOutboundCarrierManifestsDialog";

        private const string FindOutboundCarrierManifestsDialogId = "findOutboundCarrierManifestsDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindOutboundCarrierManifestsDialog);
        private const string DialogKeyOptions = "findOutboundCarrierManifestsDialogOptions";
        private const string SearchColumnsKey = "FindOutboundCarrierManifestsDialogSearchColumns";
        private const string SearchTextKey = "FindOutboundCarrierManifestsDialogSearchText";
        private const string EditColumnsKey = "FindOutboundCarrierManifestsDialogEditColumns";
        private const string EditTextKey = "FindOutboundCarrierManifestsDialogEditText";
        private const string SelectedRecordKey = "FindOutboundCarrierManifestsDialogSelectedRecordKey";

        private readonly IOutboundCarrierManifestsService _outboundcarriermanifestsService;
        OutboundCarrierManifestsPost _outboundcarriermanifestsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundcarriermanifests" };
        string[] edit = { "Edit outboundcarriermanifests" };
        string[] details = { "Display outboundcarriermanifests" };
        string[] delete = { "Delete outboundcarriermanifests" };

        public FindOutboundCarrierManifestsDialog(string id, IOutboundCarrierManifestsService outboundcarriermanifestsService, OutboundCarrierManifestsPost outboundcarriermanifestsPost, BotSpielUserStateAccessors statePropertyAccessor)
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

            AddDialog(new EditOutboundCarrierManifestsDialog(EditOutboundCarrierManifestsDialogId, _outboundcarriermanifestsService, _outboundcarriermanifestsPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteOutboundCarrierManifestsDialog(DeleteOutboundCarrierManifestsDialogId, _outboundcarriermanifestsService, _outboundcarriermanifestsPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new OutboundCarrierManifestsPost();
            step.Values[SelectedRecordKey] = _outboundcarriermanifestsPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("OutboundCarrierManifests");

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
            var outboundcarriermanifestsIndex = _outboundcarriermanifestsService.Index();
            var recordCountTotal = outboundcarriermanifestsIndex.Count();
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
            var outboundcarriermanifestsIndex = _outboundcarriermanifestsService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit outboundcarriermanifests"))
            {

                if (selection.Value == "Refine search")
                {
                    ((OutboundCarrierManifestsPost)step.Values[DialogKey]).ixOutboundCarrierManifest = 0;
                }
                else if (selection.Value == "Exit outboundcarriermanifests")
                {
                    ((OutboundCarrierManifestsPost)step.Values[DialogKey]).ixOutboundCarrierManifest = -1;
                }
                returnResult = await step.EndDialogAsync(
                (OutboundCarrierManifestsPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _outboundcarriermanifestsService.GetPost(outboundcarriermanifestsIndex.Where(o => o.sOutboundCarrierManifest == selection.Value).Select(o => o.ixOutboundCarrierManifest).First());
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
            var outboundcarriermanifestsIndex = _outboundcarriermanifestsService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit outboundcarriermanifests")
            {
                ((OutboundCarrierManifestsPost)step.Values[DialogKey]).ixOutboundCarrierManifest = -1;
                returnResult = await step.EndDialogAsync(
                (OutboundCarrierManifestsPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit outboundcarriermanifests") || (selection.Value == "Display outboundcarriermanifests") || (selection.Value == "Delete outboundcarriermanifests"))
            {
                currentBotUserData.ixOutboundCarrierManifest = ((OutboundCarrierManifestsPost)step.Values[SelectedRecordKey]).ixOutboundCarrierManifest;
                switch (selection.Value)
                {
                    case "Edit outboundcarriermanifests":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditOutboundCarrierManifestsDialogId, (OutboundCarrierManifestsPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display outboundcarriermanifests":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete outboundcarriermanifests":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteOutboundCarrierManifestsDialogId, (OutboundCarrierManifestsPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (OutboundCarrierManifestsPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


