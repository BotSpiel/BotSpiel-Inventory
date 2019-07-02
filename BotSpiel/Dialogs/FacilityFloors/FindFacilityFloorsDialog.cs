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
    public class FindFacilityFloorsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditFacilityFloorsDialogId = "editFacilityFloorsDialog";
        private const string DetailsFacilityFloorsDialogId = "detailsFacilityFloorsDialog";
        private const string DeleteFacilityFloorsDialogId = "deleteFacilityFloorsDialog";

        private const string FindFacilityFloorsDialogId = "findFacilityFloorsDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindFacilityFloorsDialog);
        private const string DialogKeyOptions = "findFacilityFloorsDialogOptions";
        private const string SearchColumnsKey = "FindFacilityFloorsDialogSearchColumns";
        private const string SearchTextKey = "FindFacilityFloorsDialogSearchText";
        private const string EditColumnsKey = "FindFacilityFloorsDialogEditColumns";
        private const string EditTextKey = "FindFacilityFloorsDialogEditText";
        private const string SelectedRecordKey = "FindFacilityFloorsDialogSelectedRecordKey";

        private readonly IFacilityFloorsService _facilityfloorsService;
        FacilityFloorsPost _facilityfloorsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilityfloors" };
        string[] edit = { "Edit facilityfloors" };
        string[] details = { "Display facilityfloors" };
        string[] delete = { "Delete facilityfloors" };

        public FindFacilityFloorsDialog(string id, IFacilityFloorsService facilityfloorsService, FacilityFloorsPost facilityfloorsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _facilityfloorsService = facilityfloorsService;
            _facilityfloorsPost = facilityfloorsPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditFacilityFloorsDialog(EditFacilityFloorsDialogId, _facilityfloorsService, _facilityfloorsPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteFacilityFloorsDialog(DeleteFacilityFloorsDialogId, _facilityfloorsService, _facilityfloorsPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new FacilityFloorsPost();
            step.Values[SelectedRecordKey] = _facilityfloorsPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("FacilityFloors");

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
            var facilityfloorsIndex = _facilityfloorsService.Index();
            var recordCountTotal = facilityfloorsIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "FacilityFloor":
                    var searchRecordsFacilityFloor = facilityfloorsIndex.Where(o => o.sFacilityFloor.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sFacilityFloor).Select(o => o.sFacilityFloor.ToString());
                    var recordCountFacilityFloor = searchRecordsFacilityFloor.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} facilityfloors. Your search resulted in {recordCountFacilityFloor} records. I show the top 15. Please choose a FacilityFloor or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsFacilityFloor.Take(15).Union(refine).Union(exit).ToList()),
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
            var facilityfloorsIndex = _facilityfloorsService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit facilityfloors"))
            {

                if (selection.Value == "Refine search")
                {
                    ((FacilityFloorsPost)step.Values[DialogKey]).ixFacilityFloor = 0;
                }
                else if (selection.Value == "Exit facilityfloors")
                {
                    ((FacilityFloorsPost)step.Values[DialogKey]).ixFacilityFloor = -1;
                }
                returnResult = await step.EndDialogAsync(
                (FacilityFloorsPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _facilityfloorsService.GetPost(facilityfloorsIndex.Where(o => o.sFacilityFloor == selection.Value).Select(o => o.ixFacilityFloor).First());
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
            var facilityfloorsIndex = _facilityfloorsService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit facilityfloors")
            {
                ((FacilityFloorsPost)step.Values[DialogKey]).ixFacilityFloor = -1;
                returnResult = await step.EndDialogAsync(
                (FacilityFloorsPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit facilityfloors") || (selection.Value == "Display facilityfloors") || (selection.Value == "Delete facilityfloors"))
            {
                currentBotUserData.ixFacilityFloor = ((FacilityFloorsPost)step.Values[SelectedRecordKey]).ixFacilityFloor;
                switch (selection.Value)
                {
                    case "Edit facilityfloors":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditFacilityFloorsDialogId, (FacilityFloorsPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display facilityfloors":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete facilityfloors":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteFacilityFloorsDialogId, (FacilityFloorsPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (FacilityFloorsPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


