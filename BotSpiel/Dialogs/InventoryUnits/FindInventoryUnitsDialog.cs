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
    public class FindInventoryUnitsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditInventoryUnitsDialogId = "editInventoryUnitsDialog";
        private const string DetailsInventoryUnitsDialogId = "detailsInventoryUnitsDialog";
        private const string DeleteInventoryUnitsDialogId = "deleteInventoryUnitsDialog";

        private const string FindInventoryUnitsDialogId = "findInventoryUnitsDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindInventoryUnitsDialog);
        private const string DialogKeyOptions = "findInventoryUnitsDialogOptions";
        private const string SearchColumnsKey = "FindInventoryUnitsDialogSearchColumns";
        private const string SearchTextKey = "FindInventoryUnitsDialogSearchText";
        private const string EditColumnsKey = "FindInventoryUnitsDialogEditColumns";
        private const string EditTextKey = "FindInventoryUnitsDialogEditText";
        private const string SelectedRecordKey = "FindInventoryUnitsDialogSelectedRecordKey";

        private readonly IInventoryUnitsService _inventoryunitsService;
        InventoryUnitsPost _inventoryunitsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inventoryunits" };
        string[] edit = { "Edit inventoryunits" };
        string[] details = { "Display inventoryunits" };
        string[] delete = { "Delete inventoryunits" };

        public FindInventoryUnitsDialog(string id, IInventoryUnitsService inventoryunitsService, InventoryUnitsPost inventoryunitsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inventoryunitsService = inventoryunitsService;
            _inventoryunitsPost = inventoryunitsPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditInventoryUnitsDialog(EditInventoryUnitsDialogId, _inventoryunitsService, _inventoryunitsPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteInventoryUnitsDialog(DeleteInventoryUnitsDialogId, _inventoryunitsService, _inventoryunitsPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new InventoryUnitsPost();
            step.Values[SelectedRecordKey] = _inventoryunitsPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("InventoryUnits");

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
            var inventoryunitsIndex = _inventoryunitsService.Index();
            var recordCountTotal = inventoryunitsIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "SerialNumber":
                    var searchRecordsSerialNumber = inventoryunitsIndex.Where(o => o.sSerialNumber.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sInventoryUnit).Select(o => o.sInventoryUnit.ToString());
                    var recordCountSerialNumber = searchRecordsSerialNumber.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} inventoryunits. Your search resulted in {recordCountSerialNumber} records. I show the top 15. Please choose a InventoryUnit or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsSerialNumber.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "BatchNumber":
                    var searchRecordsBatchNumber = inventoryunitsIndex.Where(o => o.sBatchNumber.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sInventoryUnit).Select(o => o.sInventoryUnit.ToString());
                    var recordCountBatchNumber = searchRecordsBatchNumber.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} inventoryunits. Your search resulted in {recordCountBatchNumber} records. I show the top 15. Please choose a InventoryUnit or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsBatchNumber.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;

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
            var inventoryunitsIndex = _inventoryunitsService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit inventoryunits"))
            {

                if (selection.Value == "Refine search")
                {
                    ((InventoryUnitsPost)step.Values[DialogKey]).ixInventoryUnit = 0;
                }
                else if (selection.Value == "Exit inventoryunits")
                {
                    ((InventoryUnitsPost)step.Values[DialogKey]).ixInventoryUnit = -1;
                }
                returnResult = await step.EndDialogAsync(
                (InventoryUnitsPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _inventoryunitsService.GetPost(inventoryunitsIndex.Where(o => o.sInventoryUnit == selection.Value).Select(o => o.ixInventoryUnit).First());
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
            var inventoryunitsIndex = _inventoryunitsService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit inventoryunits")
            {
                ((InventoryUnitsPost)step.Values[DialogKey]).ixInventoryUnit = -1;
                returnResult = await step.EndDialogAsync(
                (InventoryUnitsPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit inventoryunits") || (selection.Value == "Display inventoryunits") || (selection.Value == "Delete inventoryunits"))
            {
                currentBotUserData.ixInventoryUnit = ((InventoryUnitsPost)step.Values[SelectedRecordKey]).ixInventoryUnit;
                switch (selection.Value)
                {
                    case "Edit inventoryunits":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditInventoryUnitsDialogId, (InventoryUnitsPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display inventoryunits":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete inventoryunits":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteInventoryUnitsDialogId, (InventoryUnitsPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (InventoryUnitsPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


