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
    public class FindFacilitiesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditFacilitiesDialogId = "editFacilitiesDialog";
        private const string DetailsFacilitiesDialogId = "detailsFacilitiesDialog";
        private const string DeleteFacilitiesDialogId = "deleteFacilitiesDialog";

        private const string FindFacilitiesDialogId = "findFacilitiesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindFacilitiesDialog);
        private const string DialogKeyOptions = "findFacilitiesDialogOptions";
        private const string SearchColumnsKey = "FindFacilitiesDialogSearchColumns";
        private const string SearchTextKey = "FindFacilitiesDialogSearchText";
        private const string EditColumnsKey = "FindFacilitiesDialogEditColumns";
        private const string EditTextKey = "FindFacilitiesDialogEditText";
        private const string SelectedRecordKey = "FindFacilitiesDialogSelectedRecordKey";

        private readonly IFacilitiesService _facilitiesService;
        FacilitiesPost _facilitiesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilities" };
        string[] edit = { "Edit facilities" };
        string[] details = { "Display facilities" };
        string[] delete = { "Delete facilities" };

        public FindFacilitiesDialog(string id, IFacilitiesService facilitiesService, FacilitiesPost facilitiesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _facilitiesService = facilitiesService;
            _facilitiesPost = facilitiesPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditFacilitiesDialog(EditFacilitiesDialogId, _facilitiesService, _facilitiesPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteFacilitiesDialog(DeleteFacilitiesDialogId, _facilitiesService, _facilitiesPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new FacilitiesPost();
            step.Values[SelectedRecordKey] = _facilitiesPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("Facilities");

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
            var facilitiesIndex = _facilitiesService.Index();
            var recordCountTotal = facilitiesIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "Facility":
                    var searchRecordsFacility = facilitiesIndex.Where(o => o.sFacility.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sFacility).Select(o => o.sFacility.ToString());
                    var recordCountFacility = searchRecordsFacility.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} facilities. Your search resulted in {recordCountFacility} records. I show the top 15. Please choose a Facility or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsFacility.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "Latitude":
                    var searchRecordsLatitude = facilitiesIndex.Where(o => o.sLatitude.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sFacility).Select(o => o.sFacility.ToString());
                    var recordCountLatitude = searchRecordsLatitude.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} facilities. Your search resulted in {recordCountLatitude} records. I show the top 15. Please choose a Facility or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsLatitude.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "Longitude":
                    var searchRecordsLongitude = facilitiesIndex.Where(o => o.sLongitude.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sFacility).Select(o => o.sFacility.ToString());
                    var recordCountLongitude = searchRecordsLongitude.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} facilities. Your search resulted in {recordCountLongitude} records. I show the top 15. Please choose a Facility or refine the search:"),
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
            var facilitiesIndex = _facilitiesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit facilities"))
            {

                if (selection.Value == "Refine search")
                {
                    ((FacilitiesPost)step.Values[DialogKey]).ixFacility = 0;
                }
                else if (selection.Value == "Exit facilities")
                {
                    ((FacilitiesPost)step.Values[DialogKey]).ixFacility = -1;
                }
                returnResult = await step.EndDialogAsync(
                (FacilitiesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _facilitiesService.GetPost(facilitiesIndex.Where(o => o.sFacility == selection.Value).Select(o => o.ixFacility).First());
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
            var facilitiesIndex = _facilitiesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit facilities")
            {
                ((FacilitiesPost)step.Values[DialogKey]).ixFacility = -1;
                returnResult = await step.EndDialogAsync(
                (FacilitiesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit facilities") || (selection.Value == "Display facilities") || (selection.Value == "Delete facilities"))
            {
                currentBotUserData.ixFacility = ((FacilitiesPost)step.Values[SelectedRecordKey]).ixFacility;
                switch (selection.Value)
                {
                    case "Edit facilities":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditFacilitiesDialogId, (FacilitiesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display facilities":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete facilities":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteFacilitiesDialogId, (FacilitiesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (FacilitiesPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


