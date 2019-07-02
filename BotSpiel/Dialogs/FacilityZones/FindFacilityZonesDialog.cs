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
    public class FindFacilityZonesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditFacilityZonesDialogId = "editFacilityZonesDialog";
        private const string DetailsFacilityZonesDialogId = "detailsFacilityZonesDialog";
        private const string DeleteFacilityZonesDialogId = "deleteFacilityZonesDialog";

        private const string FindFacilityZonesDialogId = "findFacilityZonesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindFacilityZonesDialog);
        private const string DialogKeyOptions = "findFacilityZonesDialogOptions";
        private const string SearchColumnsKey = "FindFacilityZonesDialogSearchColumns";
        private const string SearchTextKey = "FindFacilityZonesDialogSearchText";
        private const string EditColumnsKey = "FindFacilityZonesDialogEditColumns";
        private const string EditTextKey = "FindFacilityZonesDialogEditText";
        private const string SelectedRecordKey = "FindFacilityZonesDialogSelectedRecordKey";

        private readonly IFacilityZonesService _facilityzonesService;
        FacilityZonesPost _facilityzonesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilityzones" };
        string[] edit = { "Edit facilityzones" };
        string[] details = { "Display facilityzones" };
        string[] delete = { "Delete facilityzones" };

        public FindFacilityZonesDialog(string id, IFacilityZonesService facilityzonesService, FacilityZonesPost facilityzonesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _facilityzonesService = facilityzonesService;
            _facilityzonesPost = facilityzonesPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditFacilityZonesDialog(EditFacilityZonesDialogId, _facilityzonesService, _facilityzonesPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteFacilityZonesDialog(DeleteFacilityZonesDialogId, _facilityzonesService, _facilityzonesPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new FacilityZonesPost();
            step.Values[SelectedRecordKey] = _facilityzonesPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("FacilityZones");

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
            var facilityzonesIndex = _facilityzonesService.Index();
            var recordCountTotal = facilityzonesIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "FacilityZone":
                    var searchRecordsFacilityZone = facilityzonesIndex.Where(o => o.sFacilityZone.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sFacilityZone).Select(o => o.sFacilityZone.ToString());
                    var recordCountFacilityZone = searchRecordsFacilityZone.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} facilityzones. Your search resulted in {recordCountFacilityZone} records. I show the top 15. Please choose a FacilityZone or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsFacilityZone.Take(15).Union(refine).Union(exit).ToList()),
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
            var facilityzonesIndex = _facilityzonesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit facilityzones"))
            {

                if (selection.Value == "Refine search")
                {
                    ((FacilityZonesPost)step.Values[DialogKey]).ixFacilityZone = 0;
                }
                else if (selection.Value == "Exit facilityzones")
                {
                    ((FacilityZonesPost)step.Values[DialogKey]).ixFacilityZone = -1;
                }
                returnResult = await step.EndDialogAsync(
                (FacilityZonesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _facilityzonesService.GetPost(facilityzonesIndex.Where(o => o.sFacilityZone == selection.Value).Select(o => o.ixFacilityZone).First());
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
            var facilityzonesIndex = _facilityzonesService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit facilityzones")
            {
                ((FacilityZonesPost)step.Values[DialogKey]).ixFacilityZone = -1;
                returnResult = await step.EndDialogAsync(
                (FacilityZonesPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit facilityzones") || (selection.Value == "Display facilityzones") || (selection.Value == "Delete facilityzones"))
            {
                currentBotUserData.ixFacilityZone = ((FacilityZonesPost)step.Values[SelectedRecordKey]).ixFacilityZone;
                switch (selection.Value)
                {
                    case "Edit facilityzones":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditFacilityZonesDialogId, (FacilityZonesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display facilityzones":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete facilityzones":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteFacilityZonesDialogId, (FacilityZonesPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (FacilityZonesPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


