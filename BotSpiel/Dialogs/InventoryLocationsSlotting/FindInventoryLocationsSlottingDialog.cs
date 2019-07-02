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
    public class FindInventoryLocationsSlottingDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditInventoryLocationsSlottingDialogId = "editInventoryLocationsSlottingDialog";
        private const string DetailsInventoryLocationsSlottingDialogId = "detailsInventoryLocationsSlottingDialog";
        private const string DeleteInventoryLocationsSlottingDialogId = "deleteInventoryLocationsSlottingDialog";

        private const string FindInventoryLocationsSlottingDialogId = "findInventoryLocationsSlottingDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindInventoryLocationsSlottingDialog);
        private const string DialogKeyOptions = "findInventoryLocationsSlottingDialogOptions";
        private const string SearchColumnsKey = "FindInventoryLocationsSlottingDialogSearchColumns";
        private const string SearchTextKey = "FindInventoryLocationsSlottingDialogSearchText";
        private const string EditColumnsKey = "FindInventoryLocationsSlottingDialogEditColumns";
        private const string EditTextKey = "FindInventoryLocationsSlottingDialogEditText";
        private const string SelectedRecordKey = "FindInventoryLocationsSlottingDialogSelectedRecordKey";

        private readonly IInventoryLocationsSlottingService _inventorylocationsslottingService;
        InventoryLocationsSlottingPost _inventorylocationsslottingPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inventorylocationsslotting" };
        string[] edit = { "Edit inventorylocationsslotting" };
        string[] details = { "Display inventorylocationsslotting" };
        string[] delete = { "Delete inventorylocationsslotting" };

        public FindInventoryLocationsSlottingDialog(string id, IInventoryLocationsSlottingService inventorylocationsslottingService, InventoryLocationsSlottingPost inventorylocationsslottingPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inventorylocationsslottingService = inventorylocationsslottingService;
            _inventorylocationsslottingPost = inventorylocationsslottingPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditInventoryLocationsSlottingDialog(EditInventoryLocationsSlottingDialogId, _inventorylocationsslottingService, _inventorylocationsslottingPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteInventoryLocationsSlottingDialog(DeleteInventoryLocationsSlottingDialogId, _inventorylocationsslottingService, _inventorylocationsslottingPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new InventoryLocationsSlottingPost();
            step.Values[SelectedRecordKey] = _inventorylocationsslottingPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("InventoryLocationsSlotting");

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
            var inventorylocationsslottingIndex = _inventorylocationsslottingService.Index();
            var recordCountTotal = inventorylocationsslottingIndex.Count();
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
            var inventorylocationsslottingIndex = _inventorylocationsslottingService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit inventorylocationsslotting"))
            {

                if (selection.Value == "Refine search")
                {
                    ((InventoryLocationsSlottingPost)step.Values[DialogKey]).ixInventoryLocationSlotting = 0;
                }
                else if (selection.Value == "Exit inventorylocationsslotting")
                {
                    ((InventoryLocationsSlottingPost)step.Values[DialogKey]).ixInventoryLocationSlotting = -1;
                }
                returnResult = await step.EndDialogAsync(
                (InventoryLocationsSlottingPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _inventorylocationsslottingService.GetPost(inventorylocationsslottingIndex.Where(o => o.sInventoryLocationSlotting == selection.Value).Select(o => o.ixInventoryLocationSlotting).First());
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
            var inventorylocationsslottingIndex = _inventorylocationsslottingService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit inventorylocationsslotting")
            {
                ((InventoryLocationsSlottingPost)step.Values[DialogKey]).ixInventoryLocationSlotting = -1;
                returnResult = await step.EndDialogAsync(
                (InventoryLocationsSlottingPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit inventorylocationsslotting") || (selection.Value == "Display inventorylocationsslotting") || (selection.Value == "Delete inventorylocationsslotting"))
            {
                currentBotUserData.ixInventoryLocationSlotting = ((InventoryLocationsSlottingPost)step.Values[SelectedRecordKey]).ixInventoryLocationSlotting;
                switch (selection.Value)
                {
                    case "Edit inventorylocationsslotting":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditInventoryLocationsSlottingDialogId, (InventoryLocationsSlottingPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display inventorylocationsslotting":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete inventorylocationsslotting":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteInventoryLocationsSlottingDialogId, (InventoryLocationsSlottingPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (InventoryLocationsSlottingPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


