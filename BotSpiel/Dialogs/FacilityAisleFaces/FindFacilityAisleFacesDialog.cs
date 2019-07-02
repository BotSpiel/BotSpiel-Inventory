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
    public class FindFacilityAisleFacesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditFacilityAisleFacesDialogId = "editFacilityAisleFacesDialog";
        private const string DetailsFacilityAisleFacesDialogId = "detailsFacilityAisleFacesDialog";
        private const string DeleteFacilityAisleFacesDialogId = "deleteFacilityAisleFacesDialog";

        private const string FindFacilityAisleFacesDialogId = "findFacilityAisleFacesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindFacilityAisleFacesDialog);
        private const string DialogKeyOptions = "findFacilityAisleFacesDialogOptions";
        private const string SearchColumnsKey = "FindFacilityAisleFacesDialogSearchColumns";
        private const string SearchTextKey = "FindFacilityAisleFacesDialogSearchText";
        private const string EditColumnsKey = "FindFacilityAisleFacesDialogEditColumns";
        private const string EditTextKey = "FindFacilityAisleFacesDialogEditText";
        private const string SelectedRecordKey = "FindFacilityAisleFacesDialogSelectedRecordKey";

        private readonly IFacilityAisleFacesService _facilityaislefacesService;
        FacilityAisleFacesPost _facilityaislefacesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilityaislefaces" };
        string[] edit = { "Edit facilityaislefaces" };
        string[] details = { "Display facilityaislefaces" };
        string[] delete = { "Delete facilityaislefaces" };

        public FindFacilityAisleFacesDialog(string id, IFacilityAisleFacesService facilityaislefacesService, FacilityAisleFacesPost facilityaislefacesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _facilityaislefacesService = facilityaislefacesService;
            _facilityaislefacesPost = facilityaislefacesPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditFacilityAisleFacesDialog(EditFacilityAisleFacesDialogId, _facilityaislefacesService, _facilityaislefacesPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteFacilityAisleFacesDialog(DeleteFacilityAisleFacesDialogId, _facilityaislefacesService, _facilityaislefacesPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new FacilityAisleFacesPost();
            step.Values[SelectedRecordKey] = _facilityaislefacesPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("FacilityAisleFaces");

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
            var facilityaislefacesIndex = _facilityaislefacesService.Index();
            var recordCountTotal = facilityaislefacesIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "FacilityAisleFace":
                    var searchRecordsFacilityAisleFace = facilityaislefacesIndex.Where(o => o.sFacilityAisleFace.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sFacilityAisleFace).Select(o => o.sFacilityAisleFace.ToString());
                    var recordCountFacilityAisleFace = searchRecordsFacilityAisleFace.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} facilityaislefaces. Your search resulted in {recordCountFacilityAisleFace} records. I show the top 15. Please choose a FacilityAisleFace or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsFacilityAisleFace.Take(15).Union(refine).Union(exit).ToList()),
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
            var facilityaislefacesIndex = _facilityaislefacesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit facilityaislefaces"))
            {

                if (selection.Value == "Refine search")
                {
                    ((FacilityAisleFacesPost)step.Values[DialogKey]).ixFacilityAisleFace = 0;
                }
                else if (selection.Value == "Exit facilityaislefaces")
                {
                    ((FacilityAisleFacesPost)step.Values[DialogKey]).ixFacilityAisleFace = -1;
                }
                returnResult = await step.EndDialogAsync(
                (FacilityAisleFacesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _facilityaislefacesService.GetPost(facilityaislefacesIndex.Where(o => o.sFacilityAisleFace == selection.Value).Select(o => o.ixFacilityAisleFace).First());
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
            var facilityaislefacesIndex = _facilityaislefacesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit facilityaislefaces")
            {
                ((FacilityAisleFacesPost)step.Values[DialogKey]).ixFacilityAisleFace = -1;
                returnResult = await step.EndDialogAsync(
                (FacilityAisleFacesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit facilityaislefaces") || (selection.Value == "Display facilityaislefaces") || (selection.Value == "Delete facilityaislefaces"))
            {
                currentBotUserData.ixFacilityAisleFace = ((FacilityAisleFacesPost)step.Values[SelectedRecordKey]).ixFacilityAisleFace;
                switch (selection.Value)
                {
                    case "Edit facilityaislefaces":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditFacilityAisleFacesDialogId, (FacilityAisleFacesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display facilityaislefaces":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete facilityaislefaces":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteFacilityAisleFacesDialogId, (FacilityAisleFacesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (FacilityAisleFacesPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


