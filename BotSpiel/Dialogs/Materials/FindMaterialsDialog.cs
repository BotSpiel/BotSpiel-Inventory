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
    public class FindMaterialsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditMaterialsDialogId = "editMaterialsDialog";
        private const string DetailsMaterialsDialogId = "detailsMaterialsDialog";
        private const string DeleteMaterialsDialogId = "deleteMaterialsDialog";

        private const string FindMaterialsDialogId = "findMaterialsDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(FindMaterialsDialog);
        private const string DialogKeyOptions = "findMaterialsDialogOptions";
        private const string SearchColumnsKey = "FindMaterialsDialogSearchColumns";
        private const string SearchTextKey = "FindMaterialsDialogSearchText";
        private const string EditColumnsKey = "FindMaterialsDialogEditColumns";
        private const string EditTextKey = "FindMaterialsDialogEditText";
        private const string SelectedRecordKey = "FindMaterialsDialogSelectedRecordKey";

        private readonly IMaterialsService _materialsService;
        MaterialsPost _materialsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit materials" };
        string[] edit = { "Edit materials" };
        string[] details = { "Display materials" };
        string[] delete = { "Delete materials" };

        public FindMaterialsDialog(string id, IMaterialsService materialsService, MaterialsPost materialsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _materialsService = materialsService;
            _materialsPost = materialsPost;

            // Define the prompts used in the Dialog.

            AddDialog(new ChoicePrompt(ChoicePromptId));
            AddDialog(new TextPrompt(TextPromptId));
            AddDialog(new DateTimePrompt(DateTimePromptId));
            AddDialog(new ConfirmPrompt(ConfirmPromptId));
            AddDialog(new NumberPrompt<Int32>(NumberPromptIntId));
            AddDialog(new NumberPrompt<Int64>(NumberPromptBigIntId));
            AddDialog(new NumberPrompt<float>(NumberPromptFloatId));

            AddDialog(new EditMaterialsDialog(EditMaterialsDialogId, _materialsService, _materialsPost, _botSpielUserStateAccessors));
            AddDialog(new DeleteMaterialsDialog(DeleteMaterialsDialogId, _materialsService, _materialsPost, _botSpielUserStateAccessors));


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

            step.Values[DialogKey] = new MaterialsPost();
            step.Values[SelectedRecordKey] = _materialsPost;
            step.Values[SearchColumnsKey] = searchColumn;
            step.Values[SearchTextKey] = searchText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.SearchColumnsForEntity("Materials");

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
            var materialsIndex = _materialsService.Index();
            var recordCountTotal = materialsIndex.Count();
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[SearchColumnsKey])
            {
                case "Material":
                    var searchRecordsMaterial = materialsIndex.Where(o => o.sMaterial.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sMaterial).Select(o => o.sMaterial.ToString());
                    var recordCountMaterial = searchRecordsMaterial.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} materials. Your search resulted in {recordCountMaterial} records. I show the top 15. Please choose a Material or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsMaterial.Take(15).Union(refine).Union(exit).ToList()),
                        },
                        cancellationToken);
                    break;
                case "Description":
                    var searchRecordsDescription = materialsIndex.Where(o => o.sDescription.Contains(step.Values[SearchTextKey].ToString())).OrderBy(o => o.sMaterial).Select(o => o.sMaterial.ToString());
                    var recordCountDescription = searchRecordsDescription.Count();
                    returnResult = await step.PromptAsync(
                        ChoicePromptId,
                        new PromptOptions
                        {
                            Prompt = MessageFactory.Text($"There are {recordCountTotal} materials. Your search resulted in {recordCountDescription} records. I show the top 15. Please choose a Material or refine the search:"),
                            RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                            Choices = ChoiceFactory.ToChoices(searchRecordsDescription.Take(15).Union(refine).Union(exit).ToList()),
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
            var materialsIndex = _materialsService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if ((selection.Value == "Refine search") || (selection.Value == "Exit materials"))
            {

                if (selection.Value == "Refine search")
                {
                    ((MaterialsPost)step.Values[DialogKey]).ixMaterial = 0;
                }
                else if (selection.Value == "Exit materials")
                {
                    ((MaterialsPost)step.Values[DialogKey]).ixMaterial = -1;
                }
                returnResult = await step.EndDialogAsync(
                (MaterialsPost)step.Values[DialogKey],
                cancellationToken);
            }
            else
            {
                step.Values[SelectedRecordKey] = _materialsService.GetPost(materialsIndex.Where(o => o.sMaterial == selection.Value).Select(o => o.ixMaterial).First());
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
            var materialsIndex = _materialsService.Index();
            FoundChoice selection = (FoundChoice)step.Result;

            if (selection.Value == "Exit materials")
            {
                ((MaterialsPost)step.Values[DialogKey]).ixMaterial = -1;
                returnResult = await step.EndDialogAsync(
                (MaterialsPost)step.Values[DialogKey],
                cancellationToken);
            }
            else if ((selection.Value == "Edit materials") || (selection.Value == "Display materials") || (selection.Value == "Delete materials"))
            {
                currentBotUserData.ixMaterial = ((MaterialsPost)step.Values[SelectedRecordKey]).ixMaterial;
                switch (selection.Value)
                {
                    case "Edit materials":
                        currentBotUserData.botUserEntityContext.entityIntent = "Edit";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(EditMaterialsDialogId, (MaterialsPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    case "Display materials":
                        currentBotUserData.botUserEntityContext.entityIntent = "Details";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        break;
                    case "Delete materials":
                        currentBotUserData.botUserEntityContext.entityIntent = "Delete";
                        await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
                        await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
                        returnResult = await step.ReplaceDialogAsync(DeleteMaterialsDialogId, (MaterialsPost)step.Values[SelectedRecordKey], cancellationToken);
                        break;
                    default:
                        // We shouldn't get here.
                        break;
                }

                returnResult.Result = (MaterialsPost)step.Values[SelectedRecordKey];
            }
            return returnResult;
        }



    }
}


