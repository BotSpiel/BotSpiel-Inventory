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
    public class FindMoveQueuesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditMoveQueuesDialogId = "editMoveQueuesDialog";
        private const string DetailsMoveQueuesDialogId = "detailsMoveQueuesDialog";
        private const string DeleteMoveQueuesDialogId = "deleteMoveQueuesDialog";

        private const string FindMoveQueuesDialogId = "findMoveQueuesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindMoveQueuesDialog);
        private const string DialogKeyOptions = "findMoveQueuesDialogOptions";
        private const string SearchColumnsKey = "FindMoveQueuesDialogSearchColumns";
        private const string SearchTextKey = "FindMoveQueuesDialogSearchText";
        private const string EditColumnsKey = "FindMoveQueuesDialogEditColumns";
        private const string EditTextKey = "FindMoveQueuesDialogEditText";
        private const string SelectedRecordKey = "FindMoveQueuesDialogSelectedRecordKey";

        private readonly IMoveQueuesService _movequeuesService;
        MoveQueuesPost _movequeuesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit movequeues" };
        string[] edit = { "Edit movequeues" };
        string[] details = { "Display movequeues" };
        string[] delete = { "Delete movequeues" };

        public FindMoveQueuesDialog(string id, IMoveQueuesService movequeuesService, MoveQueuesPost movequeuesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _movequeuesService = movequeuesService;
            _movequeuesPost = movequeuesPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditMoveQueuesDialog(EditMoveQueuesDialogId, _movequeuesService, _movequeuesPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteMoveQueuesDialog(DeleteMoveQueuesDialogId, _movequeuesService, _movequeuesPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new MoveQueuesPost();
            step.Values[SelectedRecordKey] = _movequeuesPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("MoveQueues");

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
            var movequeuesIndex = _movequeuesService.Index();
            var recordCountTotal = movequeuesIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "PreferredResource":
                    var searchRecordsPreferredResource = movequeuesIndex.Where(o => o.sPreferredResource.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sMoveQueue).Select(o => o.sMoveQueue.ToString());
                    var recordCountPreferredResource = searchRecordsPreferredResource.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} movequeues. Your search resulted in {recordCountPreferredResource} records. I show the top 15. Please choose a MoveQueue or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsPreferredResource.Take(15).Union(refine).Union(exit).ToList()),
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
            var movequeuesIndex = _movequeuesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit movequeues"))
            {

                if (selection.Value == "Refine search")
                {
                    ((MoveQueuesPost)step.Values[DialogKey]).ixMoveQueue = 0;
                }
                else if (selection.Value == "Exit movequeues")
                {
                    ((MoveQueuesPost)step.Values[DialogKey]).ixMoveQueue = -1;
                }
                returnResult = await step.EndDialogAsync(
                (MoveQueuesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _movequeuesService.GetPost(movequeuesIndex.Where(o => o.sMoveQueue == selection.Value).Select(o => o.ixMoveQueue).First());
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
            var movequeuesIndex = _movequeuesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit movequeues")
            {
                ((MoveQueuesPost)step.Values[DialogKey]).ixMoveQueue = -1;
                returnResult = await step.EndDialogAsync(
                (MoveQueuesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit movequeues") || (selection.Value == "Display movequeues") || (selection.Value == "Delete movequeues"))
            {
                currentBotUserData.ixMoveQueue = ((MoveQueuesPost)step.Values[SelectedRecordKey]).ixMoveQueue;
                switch (selection.Value)
                {
                    case "Edit movequeues":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditMoveQueuesDialogId, (MoveQueuesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display movequeues":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete movequeues":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteMoveQueuesDialogId, (MoveQueuesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (MoveQueuesPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


