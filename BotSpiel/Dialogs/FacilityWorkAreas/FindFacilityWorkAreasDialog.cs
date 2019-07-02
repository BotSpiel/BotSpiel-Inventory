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
    public class FindFacilityWorkAreasDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditFacilityWorkAreasDialogId = "editFacilityWorkAreasDialog";
        private const string DetailsFacilityWorkAreasDialogId = "detailsFacilityWorkAreasDialog";
        private const string DeleteFacilityWorkAreasDialogId = "deleteFacilityWorkAreasDialog";

        private const string FindFacilityWorkAreasDialogId = "findFacilityWorkAreasDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindFacilityWorkAreasDialog);
        private const string DialogKeyOptions = "findFacilityWorkAreasDialogOptions";
        private const string SearchColumnsKey = "FindFacilityWorkAreasDialogSearchColumns";
        private const string SearchTextKey = "FindFacilityWorkAreasDialogSearchText";
        private const string EditColumnsKey = "FindFacilityWorkAreasDialogEditColumns";
        private const string EditTextKey = "FindFacilityWorkAreasDialogEditText";
        private const string SelectedRecordKey = "FindFacilityWorkAreasDialogSelectedRecordKey";

        private readonly IFacilityWorkAreasService _facilityworkareasService;
        FacilityWorkAreasPost _facilityworkareasPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilityworkareas" };
        string[] edit = { "Edit facilityworkareas" };
        string[] details = { "Display facilityworkareas" };
        string[] delete = { "Delete facilityworkareas" };

        public FindFacilityWorkAreasDialog(string id, IFacilityWorkAreasService facilityworkareasService, FacilityWorkAreasPost facilityworkareasPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _facilityworkareasService = facilityworkareasService;
            _facilityworkareasPost = facilityworkareasPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditFacilityWorkAreasDialog(EditFacilityWorkAreasDialogId, _facilityworkareasService, _facilityworkareasPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteFacilityWorkAreasDialog(DeleteFacilityWorkAreasDialogId, _facilityworkareasService, _facilityworkareasPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new FacilityWorkAreasPost();
            step.Values[SelectedRecordKey] = _facilityworkareasPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("FacilityWorkAreas");

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
            var facilityworkareasIndex = _facilityworkareasService.Index();
            var recordCountTotal = facilityworkareasIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "FacilityWorkArea":
                    var searchRecordsFacilityWorkArea = facilityworkareasIndex.Where(o => o.sFacilityWorkArea.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sFacilityWorkArea).Select(o => o.sFacilityWorkArea.ToString());
                    var recordCountFacilityWorkArea = searchRecordsFacilityWorkArea.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} facilityworkareas. Your search resulted in {recordCountFacilityWorkArea} records. I show the top 15. Please choose a FacilityWorkArea or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsFacilityWorkArea.Take(15).Union(refine).Union(exit).ToList()),
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
            var facilityworkareasIndex = _facilityworkareasService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit facilityworkareas"))
            {

                if (selection.Value == "Refine search")
                {
                    ((FacilityWorkAreasPost)step.Values[DialogKey]).ixFacilityWorkArea = 0;
                }
                else if (selection.Value == "Exit facilityworkareas")
                {
                    ((FacilityWorkAreasPost)step.Values[DialogKey]).ixFacilityWorkArea = -1;
                }
                returnResult = await step.EndDialogAsync(
                (FacilityWorkAreasPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _facilityworkareasService.GetPost(facilityworkareasIndex.Where(o => o.sFacilityWorkArea == selection.Value).Select(o => o.ixFacilityWorkArea).First());
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
            var facilityworkareasIndex = _facilityworkareasService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit facilityworkareas")
            {
                ((FacilityWorkAreasPost)step.Values[DialogKey]).ixFacilityWorkArea = -1;
                returnResult = await step.EndDialogAsync(
                (FacilityWorkAreasPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit facilityworkareas") || (selection.Value == "Display facilityworkareas") || (selection.Value == "Delete facilityworkareas"))
            {
                currentBotUserData.ixFacilityWorkArea = ((FacilityWorkAreasPost)step.Values[SelectedRecordKey]).ixFacilityWorkArea;
                switch (selection.Value)
                {
                    case "Edit facilityworkareas":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditFacilityWorkAreasDialogId, (FacilityWorkAreasPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display facilityworkareas":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete facilityworkareas":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteFacilityWorkAreasDialogId, (FacilityWorkAreasPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (FacilityWorkAreasPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


