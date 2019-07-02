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
    public class FindInventoryLocationsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditInventoryLocationsDialogId = "editInventoryLocationsDialog";
        private const string DetailsInventoryLocationsDialogId = "detailsInventoryLocationsDialog";
        private const string DeleteInventoryLocationsDialogId = "deleteInventoryLocationsDialog";

        private const string FindInventoryLocationsDialogId = "findInventoryLocationsDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindInventoryLocationsDialog);
        private const string DialogKeyOptions = "findInventoryLocationsDialogOptions";
        private const string SearchColumnsKey = "FindInventoryLocationsDialogSearchColumns";
        private const string SearchTextKey = "FindInventoryLocationsDialogSearchText";
        private const string EditColumnsKey = "FindInventoryLocationsDialogEditColumns";
        private const string EditTextKey = "FindInventoryLocationsDialogEditText";
        private const string SelectedRecordKey = "FindInventoryLocationsDialogSelectedRecordKey";

        private readonly IInventoryLocationsService _inventorylocationsService;
        InventoryLocationsPost _inventorylocationsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inventorylocations" };
        string[] edit = { "Edit inventorylocations" };
        string[] details = { "Display inventorylocations" };
        string[] delete = { "Delete inventorylocations" };

        public FindInventoryLocationsDialog(string id, IInventoryLocationsService inventorylocationsService, InventoryLocationsPost inventorylocationsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inventorylocationsService = inventorylocationsService;
            _inventorylocationsPost = inventorylocationsPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditInventoryLocationsDialog(EditInventoryLocationsDialogId, _inventorylocationsService, _inventorylocationsPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteInventoryLocationsDialog(DeleteInventoryLocationsDialogId, _inventorylocationsService, _inventorylocationsPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new InventoryLocationsPost();
            step.Values[SelectedRecordKey] = _inventorylocationsPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("InventoryLocations");

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
            var inventorylocationsIndex = _inventorylocationsService.Index();
            var recordCountTotal = inventorylocationsIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "InventoryLocation":
                    var searchRecordsInventoryLocation = inventorylocationsIndex.Where(o => o.sInventoryLocation.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sInventoryLocation).Select(o => o.sInventoryLocation.ToString());
                    var recordCountInventoryLocation = searchRecordsInventoryLocation.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} inventorylocations. Your search resulted in {recordCountInventoryLocation} records. I show the top 15. Please choose a InventoryLocation or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsInventoryLocation.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "Level":
                    var searchRecordsLevel = inventorylocationsIndex.Where(o => o.sLevel.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sInventoryLocation).Select(o => o.sInventoryLocation.ToString());
                    var recordCountLevel = searchRecordsLevel.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} inventorylocations. Your search resulted in {recordCountLevel} records. I show the top 15. Please choose a InventoryLocation or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsLevel.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "Bay":
                    var searchRecordsBay = inventorylocationsIndex.Where(o => o.sBay.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sInventoryLocation).Select(o => o.sInventoryLocation.ToString());
                    var recordCountBay = searchRecordsBay.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} inventorylocations. Your search resulted in {recordCountBay} records. I show the top 15. Please choose a InventoryLocation or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsBay.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "Slot":
                    var searchRecordsSlot = inventorylocationsIndex.Where(o => o.sSlot.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sInventoryLocation).Select(o => o.sInventoryLocation.ToString());
                    var recordCountSlot = searchRecordsSlot.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} inventorylocations. Your search resulted in {recordCountSlot} records. I show the top 15. Please choose a InventoryLocation or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsSlot.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "Latitude":
                    var searchRecordsLatitude = inventorylocationsIndex.Where(o => o.sLatitude.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sInventoryLocation).Select(o => o.sInventoryLocation.ToString());
                    var recordCountLatitude = searchRecordsLatitude.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} inventorylocations. Your search resulted in {recordCountLatitude} records. I show the top 15. Please choose a InventoryLocation or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsLatitude.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "Longitude":
                    var searchRecordsLongitude = inventorylocationsIndex.Where(o => o.sLongitude.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sInventoryLocation).Select(o => o.sInventoryLocation.ToString());
                    var recordCountLongitude = searchRecordsLongitude.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} inventorylocations. Your search resulted in {recordCountLongitude} records. I show the top 15. Please choose a InventoryLocation or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsLongitude.Take(15).Union(refine).Union(exit).ToList()),
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
            var inventorylocationsIndex = _inventorylocationsService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit inventorylocations"))
            {

                if (selection.Value == "Refine search")
                {
                    ((InventoryLocationsPost)step.Values[DialogKey]).ixInventoryLocation = 0;
                }
                else if (selection.Value == "Exit inventorylocations")
                {
                    ((InventoryLocationsPost)step.Values[DialogKey]).ixInventoryLocation = -1;
                }
                returnResult = await step.EndDialogAsync(
                (InventoryLocationsPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _inventorylocationsService.GetPost(inventorylocationsIndex.Where(o => o.sInventoryLocation == selection.Value).Select(o => o.ixInventoryLocation).First());
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
            var inventorylocationsIndex = _inventorylocationsService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit inventorylocations")
            {
                ((InventoryLocationsPost)step.Values[DialogKey]).ixInventoryLocation = -1;
                returnResult = await step.EndDialogAsync(
                (InventoryLocationsPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit inventorylocations") || (selection.Value == "Display inventorylocations") || (selection.Value == "Delete inventorylocations"))
            {
                currentBotUserData.ixInventoryLocation = ((InventoryLocationsPost)step.Values[SelectedRecordKey]).ixInventoryLocation;
                switch (selection.Value)
                {
                    case "Edit inventorylocations":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditInventoryLocationsDialogId, (InventoryLocationsPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display inventorylocations":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete inventorylocations":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteInventoryLocationsDialogId, (InventoryLocationsPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (InventoryLocationsPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


