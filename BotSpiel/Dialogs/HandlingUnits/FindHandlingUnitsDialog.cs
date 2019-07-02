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
    public class FindHandlingUnitsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditHandlingUnitsDialogId = "editHandlingUnitsDialog";
        private const string DetailsHandlingUnitsDialogId = "detailsHandlingUnitsDialog";
        private const string DeleteHandlingUnitsDialogId = "deleteHandlingUnitsDialog";

        private const string FindHandlingUnitsDialogId = "findHandlingUnitsDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindHandlingUnitsDialog);
        private const string DialogKeyOptions = "findHandlingUnitsDialogOptions";
        private const string SearchColumnsKey = "FindHandlingUnitsDialogSearchColumns";
        private const string SearchTextKey = "FindHandlingUnitsDialogSearchText";
        private const string EditColumnsKey = "FindHandlingUnitsDialogEditColumns";
        private const string EditTextKey = "FindHandlingUnitsDialogEditText";
        private const string SelectedRecordKey = "FindHandlingUnitsDialogSelectedRecordKey";

        private readonly IHandlingUnitsService _handlingunitsService;
        HandlingUnitsPost _handlingunitsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit handlingunits" };
        string[] edit = { "Edit handlingunits" };
        string[] details = { "Display handlingunits" };
        string[] delete = { "Delete handlingunits" };

        public FindHandlingUnitsDialog(string id, IHandlingUnitsService handlingunitsService, HandlingUnitsPost handlingunitsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _handlingunitsService = handlingunitsService;
            _handlingunitsPost = handlingunitsPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditHandlingUnitsDialog(EditHandlingUnitsDialogId, _handlingunitsService, _handlingunitsPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteHandlingUnitsDialog(DeleteHandlingUnitsDialogId, _handlingunitsService, _handlingunitsPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new HandlingUnitsPost();
            step.Values[SelectedRecordKey] = _handlingunitsPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("HandlingUnits");

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
            var handlingunitsIndex = _handlingunitsService.Index();
            var recordCountTotal = handlingunitsIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "HandlingUnit":
                    var searchRecordsHandlingUnit = handlingunitsIndex.Where(o => o.sHandlingUnit.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sHandlingUnit).Select(o => o.sHandlingUnit.ToString());
                    var recordCountHandlingUnit = searchRecordsHandlingUnit.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} handlingunits. Your search resulted in {recordCountHandlingUnit} records. I show the top 15. Please choose a HandlingUnit or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsHandlingUnit.Take(15).Union(refine).Union(exit).ToList()),
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
            var handlingunitsIndex = _handlingunitsService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit handlingunits"))
            {

                if (selection.Value == "Refine search")
                {
                    ((HandlingUnitsPost)step.Values[DialogKey]).ixHandlingUnit = 0;
                }
                else if (selection.Value == "Exit handlingunits")
                {
                    ((HandlingUnitsPost)step.Values[DialogKey]).ixHandlingUnit = -1;
                }
                returnResult = await step.EndDialogAsync(
                (HandlingUnitsPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _handlingunitsService.GetPost(handlingunitsIndex.Where(o => o.sHandlingUnit == selection.Value).Select(o => o.ixHandlingUnit).First());
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
            var handlingunitsIndex = _handlingunitsService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit handlingunits")
            {
                ((HandlingUnitsPost)step.Values[DialogKey]).ixHandlingUnit = -1;
                returnResult = await step.EndDialogAsync(
                (HandlingUnitsPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit handlingunits") || (selection.Value == "Display handlingunits") || (selection.Value == "Delete handlingunits"))
            {
                currentBotUserData.ixHandlingUnit = ((HandlingUnitsPost)step.Values[SelectedRecordKey]).ixHandlingUnit;
                switch (selection.Value)
                {
                    case "Edit handlingunits":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditHandlingUnitsDialogId, (HandlingUnitsPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display handlingunits":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete handlingunits":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteHandlingUnitsDialogId, (HandlingUnitsPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (HandlingUnitsPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


