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
    public class FindCarriersDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditCarriersDialogId = "editCarriersDialog";
        private const string DetailsCarriersDialogId = "detailsCarriersDialog";
        private const string DeleteCarriersDialogId = "deleteCarriersDialog";

        private const string FindCarriersDialogId = "findCarriersDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindCarriersDialog);
        private const string DialogKeyOptions = "findCarriersDialogOptions";
        private const string SearchColumnsKey = "FindCarriersDialogSearchColumns";
        private const string SearchTextKey = "FindCarriersDialogSearchText";
        private const string EditColumnsKey = "FindCarriersDialogEditColumns";
        private const string EditTextKey = "FindCarriersDialogEditText";
        private const string SelectedRecordKey = "FindCarriersDialogSelectedRecordKey";

        private readonly ICarriersService _carriersService;
        CarriersPost _carriersPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit carriers" };
        string[] edit = { "Edit carriers" };
        string[] details = { "Display carriers" };
        string[] delete = { "Delete carriers" };

        public FindCarriersDialog(string id, ICarriersService carriersService, CarriersPost carriersPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _carriersService = carriersService;
            _carriersPost = carriersPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditCarriersDialog(EditCarriersDialogId, _carriersService, _carriersPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteCarriersDialog(DeleteCarriersDialogId, _carriersService, _carriersPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new CarriersPost();
            step.Values[SelectedRecordKey] = _carriersPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("Carriers");

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
            var carriersIndex = _carriersService.Index();
            var recordCountTotal = carriersIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "Carrier":
                    var searchRecordsCarrier = carriersIndex.Where(o => o.sCarrier.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sCarrier).Select(o => o.sCarrier.ToString());
                    var recordCountCarrier = searchRecordsCarrier.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} carriers. Your search resulted in {recordCountCarrier} records. I show the top 15. Please choose a Carrier or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsCarrier.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "StandardCarrierAlphaCode":
                    var searchRecordsStandardCarrierAlphaCode = carriersIndex.Where(o => o.sStandardCarrierAlphaCode.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sCarrier).Select(o => o.sCarrier.ToString());
                    var recordCountStandardCarrierAlphaCode = searchRecordsStandardCarrierAlphaCode.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} carriers. Your search resulted in {recordCountStandardCarrierAlphaCode} records. I show the top 15. Please choose a Carrier or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsStandardCarrierAlphaCode.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "CarrierConsignmentNumberPrefix":
                    var searchRecordsCarrierConsignmentNumberPrefix = carriersIndex.Where(o => o.sCarrierConsignmentNumberPrefix.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sCarrier).Select(o => o.sCarrier.ToString());
                    var recordCountCarrierConsignmentNumberPrefix = searchRecordsCarrierConsignmentNumberPrefix.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} carriers. Your search resulted in {recordCountCarrierConsignmentNumberPrefix} records. I show the top 15. Please choose a Carrier or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsCarrierConsignmentNumberPrefix.Take(15).Union(refine).Union(exit).ToList()),
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
            var carriersIndex = _carriersService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit carriers"))
            {

                if (selection.Value == "Refine search")
                {
                    ((CarriersPost)step.Values[DialogKey]).ixCarrier = 0;
                }
                else if (selection.Value == "Exit carriers")
                {
                    ((CarriersPost)step.Values[DialogKey]).ixCarrier = -1;
                }
                returnResult = await step.EndDialogAsync(
                (CarriersPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _carriersService.GetPost(carriersIndex.Where(o => o.sCarrier == selection.Value).Select(o => o.ixCarrier).First());
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
            var carriersIndex = _carriersService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit carriers")
            {
                ((CarriersPost)step.Values[DialogKey]).ixCarrier = -1;
                returnResult = await step.EndDialogAsync(
                (CarriersPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit carriers") || (selection.Value == "Display carriers") || (selection.Value == "Delete carriers"))
            {
                currentBotUserData.ixCarrier = ((CarriersPost)step.Values[SelectedRecordKey]).ixCarrier;
                switch (selection.Value)
                {
                    case "Edit carriers":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditCarriersDialogId, (CarriersPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display carriers":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete carriers":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteCarriersDialogId, (CarriersPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (CarriersPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


