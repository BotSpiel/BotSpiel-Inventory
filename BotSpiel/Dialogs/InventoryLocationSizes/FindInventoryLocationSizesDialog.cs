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
    public class FindInventoryLocationSizesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditInventoryLocationSizesDialogId = "editInventoryLocationSizesDialog";
        private const string DetailsInventoryLocationSizesDialogId = "detailsInventoryLocationSizesDialog";
        private const string DeleteInventoryLocationSizesDialogId = "deleteInventoryLocationSizesDialog";

        private const string FindInventoryLocationSizesDialogId = "findInventoryLocationSizesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindInventoryLocationSizesDialog);
        private const string DialogKeyOptions = "findInventoryLocationSizesDialogOptions";
        private const string SearchColumnsKey = "FindInventoryLocationSizesDialogSearchColumns";
        private const string SearchTextKey = "FindInventoryLocationSizesDialogSearchText";
        private const string EditColumnsKey = "FindInventoryLocationSizesDialogEditColumns";
        private const string EditTextKey = "FindInventoryLocationSizesDialogEditText";
        private const string SelectedRecordKey = "FindInventoryLocationSizesDialogSelectedRecordKey";

        private readonly IInventoryLocationSizesService _inventorylocationsizesService;
        InventoryLocationSizesPost _inventorylocationsizesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inventorylocationsizes" };
        string[] edit = { "Edit inventorylocationsizes" };
        string[] details = { "Display inventorylocationsizes" };
        string[] delete = { "Delete inventorylocationsizes" };

        public FindInventoryLocationSizesDialog(string id, IInventoryLocationSizesService inventorylocationsizesService, InventoryLocationSizesPost inventorylocationsizesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inventorylocationsizesService = inventorylocationsizesService;
            _inventorylocationsizesPost = inventorylocationsizesPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditInventoryLocationSizesDialog(EditInventoryLocationSizesDialogId, _inventorylocationsizesService, _inventorylocationsizesPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteInventoryLocationSizesDialog(DeleteInventoryLocationSizesDialogId, _inventorylocationsizesService, _inventorylocationsizesPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new InventoryLocationSizesPost();
            step.Values[SelectedRecordKey] = _inventorylocationsizesPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("InventoryLocationSizes");

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
            var inventorylocationsizesIndex = _inventorylocationsizesService.Index();
            var recordCountTotal = inventorylocationsizesIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "InventoryLocationSize":
                    var searchRecordsInventoryLocationSize = inventorylocationsizesIndex.Where(o => o.sInventoryLocationSize.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sInventoryLocationSize).Select(o => o.sInventoryLocationSize.ToString());
                    var recordCountInventoryLocationSize = searchRecordsInventoryLocationSize.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} inventorylocationsizes. Your search resulted in {recordCountInventoryLocationSize} records. I show the top 15. Please choose a InventoryLocationSize or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsInventoryLocationSize.Take(15).Union(refine).Union(exit).ToList()),
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
            var inventorylocationsizesIndex = _inventorylocationsizesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit inventorylocationsizes"))
            {

                if (selection.Value == "Refine search")
                {
                    ((InventoryLocationSizesPost)step.Values[DialogKey]).ixInventoryLocationSize = 0;
                }
                else if (selection.Value == "Exit inventorylocationsizes")
                {
                    ((InventoryLocationSizesPost)step.Values[DialogKey]).ixInventoryLocationSize = -1;
                }
                returnResult = await step.EndDialogAsync(
                (InventoryLocationSizesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _inventorylocationsizesService.GetPost(inventorylocationsizesIndex.Where(o => o.sInventoryLocationSize == selection.Value).Select(o => o.ixInventoryLocationSize).First());
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
            var inventorylocationsizesIndex = _inventorylocationsizesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit inventorylocationsizes")
            {
                ((InventoryLocationSizesPost)step.Values[DialogKey]).ixInventoryLocationSize = -1;
                returnResult = await step.EndDialogAsync(
                (InventoryLocationSizesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit inventorylocationsizes") || (selection.Value == "Display inventorylocationsizes") || (selection.Value == "Delete inventorylocationsizes"))
            {
                currentBotUserData.ixInventoryLocationSize = ((InventoryLocationSizesPost)step.Values[SelectedRecordKey]).ixInventoryLocationSize;
                switch (selection.Value)
                {
                    case "Edit inventorylocationsizes":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditInventoryLocationSizesDialogId, (InventoryLocationSizesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display inventorylocationsizes":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete inventorylocationsizes":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteInventoryLocationSizesDialogId, (InventoryLocationSizesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (InventoryLocationSizesPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


