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
    public class FindAddressesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditAddressesDialogId = "editAddressesDialog";
        private const string DetailsAddressesDialogId = "detailsAddressesDialog";
        private const string DeleteAddressesDialogId = "deleteAddressesDialog";

        private const string FindAddressesDialogId = "findAddressesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindAddressesDialog);
        private const string DialogKeyOptions = "findAddressesDialogOptions";
        private const string SearchColumnsKey = "FindAddressesDialogSearchColumns";
        private const string SearchTextKey = "FindAddressesDialogSearchText";
        private const string EditColumnsKey = "FindAddressesDialogEditColumns";
        private const string EditTextKey = "FindAddressesDialogEditText";
        private const string SelectedRecordKey = "FindAddressesDialogSelectedRecordKey";

        private readonly IAddressesService _addressesService;
        AddressesPost _addressesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit addresses" };
        string[] edit = { "Edit addresses" };
        string[] details = { "Display addresses" };
        string[] delete = { "Delete addresses" };

        public FindAddressesDialog(string id, IAddressesService addressesService, AddressesPost addressesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _addressesService = addressesService;
            _addressesPost = addressesPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditAddressesDialog(EditAddressesDialogId, _addressesService, _addressesPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteAddressesDialog(DeleteAddressesDialogId, _addressesService, _addressesPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new AddressesPost();
            step.Values[SelectedRecordKey] = _addressesPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("Addresses");

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
            var addressesIndex = _addressesService.Index();
            var recordCountTotal = addressesIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "StreetAndNumberOrPostOfficeBoxOne":
                    var searchRecordsStreetAndNumberOrPostOfficeBoxOne = addressesIndex.Where(o => o.sStreetAndNumberOrPostOfficeBoxOne.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sAddress).Select(o => o.sAddress.ToString());
                    var recordCountStreetAndNumberOrPostOfficeBoxOne = searchRecordsStreetAndNumberOrPostOfficeBoxOne.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} addresses. Your search resulted in {recordCountStreetAndNumberOrPostOfficeBoxOne} records. I show the top 15. Please choose a Address or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsStreetAndNumberOrPostOfficeBoxOne.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "StreetAndNumberOrPostOfficeBoxTwo":
                    var searchRecordsStreetAndNumberOrPostOfficeBoxTwo = addressesIndex.Where(o => o.sStreetAndNumberOrPostOfficeBoxTwo.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sAddress).Select(o => o.sAddress.ToString());
                    var recordCountStreetAndNumberOrPostOfficeBoxTwo = searchRecordsStreetAndNumberOrPostOfficeBoxTwo.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} addresses. Your search resulted in {recordCountStreetAndNumberOrPostOfficeBoxTwo} records. I show the top 15. Please choose a Address or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsStreetAndNumberOrPostOfficeBoxTwo.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "StreetAndNumberOrPostOfficeBoxThree":
                    var searchRecordsStreetAndNumberOrPostOfficeBoxThree = addressesIndex.Where(o => o.sStreetAndNumberOrPostOfficeBoxThree.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sAddress).Select(o => o.sAddress.ToString());
                    var recordCountStreetAndNumberOrPostOfficeBoxThree = searchRecordsStreetAndNumberOrPostOfficeBoxThree.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} addresses. Your search resulted in {recordCountStreetAndNumberOrPostOfficeBoxThree} records. I show the top 15. Please choose a Address or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsStreetAndNumberOrPostOfficeBoxThree.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "CityOrSuburb":
                    var searchRecordsCityOrSuburb = addressesIndex.Where(o => o.sCityOrSuburb.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sAddress).Select(o => o.sAddress.ToString());
                    var recordCountCityOrSuburb = searchRecordsCityOrSuburb.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} addresses. Your search resulted in {recordCountCityOrSuburb} records. I show the top 15. Please choose a Address or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsCityOrSuburb.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "ZipOrPostCode":
                    var searchRecordsZipOrPostCode = addressesIndex.Where(o => o.sZipOrPostCode.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sAddress).Select(o => o.sAddress.ToString());
                    var recordCountZipOrPostCode = searchRecordsZipOrPostCode.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} addresses. Your search resulted in {recordCountZipOrPostCode} records. I show the top 15. Please choose a Address or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsZipOrPostCode.Take(15).Union(refine).Union(exit).ToList()),
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
            var addressesIndex = _addressesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit addresses"))
            {

                if (selection.Value == "Refine search")
                {
                    ((AddressesPost)step.Values[DialogKey]).ixAddress = 0;
                }
                else if (selection.Value == "Exit addresses")
                {
                    ((AddressesPost)step.Values[DialogKey]).ixAddress = -1;
                }
                returnResult = await step.EndDialogAsync(
                (AddressesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _addressesService.GetPost(addressesIndex.Where(o => o.sAddress == selection.Value).Select(o => o.ixAddress).First());
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
            var addressesIndex = _addressesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit addresses")
            {
                ((AddressesPost)step.Values[DialogKey]).ixAddress = -1;
                returnResult = await step.EndDialogAsync(
                (AddressesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit addresses") || (selection.Value == "Display addresses") || (selection.Value == "Delete addresses"))
            {
                currentBotUserData.ixAddress = ((AddressesPost)step.Values[SelectedRecordKey]).ixAddress;
                switch (selection.Value)
                {
                    case "Edit addresses":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditAddressesDialogId, (AddressesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display addresses":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete addresses":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteAddressesDialogId, (AddressesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (AddressesPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


