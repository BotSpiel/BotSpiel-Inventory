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
    public class FindMaterialHandlingUnitConfigurationsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditMaterialHandlingUnitConfigurationsDialogId = "editMaterialHandlingUnitConfigurationsDialog";
        private const string DetailsMaterialHandlingUnitConfigurationsDialogId = "detailsMaterialHandlingUnitConfigurationsDialog";
        private const string DeleteMaterialHandlingUnitConfigurationsDialogId = "deleteMaterialHandlingUnitConfigurationsDialog";

        private const string FindMaterialHandlingUnitConfigurationsDialogId = "findMaterialHandlingUnitConfigurationsDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindMaterialHandlingUnitConfigurationsDialog);
        private const string DialogKeyOptions = "findMaterialHandlingUnitConfigurationsDialogOptions";
        private const string SearchColumnsKey = "FindMaterialHandlingUnitConfigurationsDialogSearchColumns";
        private const string SearchTextKey = "FindMaterialHandlingUnitConfigurationsDialogSearchText";
        private const string EditColumnsKey = "FindMaterialHandlingUnitConfigurationsDialogEditColumns";
        private const string EditTextKey = "FindMaterialHandlingUnitConfigurationsDialogEditText";
        private const string SelectedRecordKey = "FindMaterialHandlingUnitConfigurationsDialogSelectedRecordKey";

        private readonly IMaterialHandlingUnitConfigurationsService _materialhandlingunitconfigurationsService;
        MaterialHandlingUnitConfigurationsPost _materialhandlingunitconfigurationsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit materialhandlingunitconfigurations" };
        string[] edit = { "Edit materialhandlingunitconfigurations" };
        string[] details = { "Display materialhandlingunitconfigurations" };
        string[] delete = { "Delete materialhandlingunitconfigurations" };

        public FindMaterialHandlingUnitConfigurationsDialog(string id, IMaterialHandlingUnitConfigurationsService materialhandlingunitconfigurationsService, MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurationsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _materialhandlingunitconfigurationsService = materialhandlingunitconfigurationsService;
            _materialhandlingunitconfigurationsPost = materialhandlingunitconfigurationsPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditMaterialHandlingUnitConfigurationsDialog(EditMaterialHandlingUnitConfigurationsDialogId, _materialhandlingunitconfigurationsService, _materialhandlingunitconfigurationsPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteMaterialHandlingUnitConfigurationsDialog(DeleteMaterialHandlingUnitConfigurationsDialogId, _materialhandlingunitconfigurationsService, _materialhandlingunitconfigurationsPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new MaterialHandlingUnitConfigurationsPost();
            step.Values[SelectedRecordKey] = _materialhandlingunitconfigurationsPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("MaterialHandlingUnitConfigurations");

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
            var materialhandlingunitconfigurationsIndex = _materialhandlingunitconfigurationsService.Index();
            var recordCountTotal = materialhandlingunitconfigurationsIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {

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
            var materialhandlingunitconfigurationsIndex = _materialhandlingunitconfigurationsService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit materialhandlingunitconfigurations"))
            {

                if (selection.Value == "Refine search")
                {
                    ((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).ixMaterialHandlingUnitConfiguration = 0;
                }
                else if (selection.Value == "Exit materialhandlingunitconfigurations")
                {
                    ((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).ixMaterialHandlingUnitConfiguration = -1;
                }
                returnResult = await step.EndDialogAsync(
                (MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _materialhandlingunitconfigurationsService.GetPost(materialhandlingunitconfigurationsIndex.Where(o => o.sMaterialHandlingUnitConfiguration == selection.Value).Select(o => o.ixMaterialHandlingUnitConfiguration).First());
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
            var materialhandlingunitconfigurationsIndex = _materialhandlingunitconfigurationsService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit materialhandlingunitconfigurations")
            {
                ((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).ixMaterialHandlingUnitConfiguration = -1;
                returnResult = await step.EndDialogAsync(
                (MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit materialhandlingunitconfigurations") || (selection.Value == "Display materialhandlingunitconfigurations") || (selection.Value == "Delete materialhandlingunitconfigurations"))
            {
                currentBotUserData.ixMaterialHandlingUnitConfiguration = ((MaterialHandlingUnitConfigurationsPost)step.Values[SelectedRecordKey]).ixMaterialHandlingUnitConfiguration;
                switch (selection.Value)
                {
                    case "Edit materialhandlingunitconfigurations":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditMaterialHandlingUnitConfigurationsDialogId, (MaterialHandlingUnitConfigurationsPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display materialhandlingunitconfigurations":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete materialhandlingunitconfigurations":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteMaterialHandlingUnitConfigurationsDialogId, (MaterialHandlingUnitConfigurationsPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (MaterialHandlingUnitConfigurationsPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


