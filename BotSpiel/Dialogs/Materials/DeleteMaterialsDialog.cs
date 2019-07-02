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
    public class DeleteMaterialsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteMaterialsDialogId = "deleteMaterialsDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteMaterialsDialog);
        private const string DialogKeyOptions = "deleteMaterialsDialogOptions";
        private const string SearchColumnsKey = "DeleteMaterialsDialogSearchColumns";
        private const string SearchTextKey = "DeleteMaterialsDialogSearchText";
        private const string EditColumnsKey = "DeleteMaterialsDialogEditColumns";
        private const string EditTextKey = "DeleteMaterialsDialogEditText";
        private const string SelectedRecordKey = "DeleteMaterialsDialogSelectedRecordKey";

        private readonly IMaterialsService _materialsService;
        MaterialsPost _materialsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit materials" };
        string[] edit = { "Edit materials" };
        string[] details = { "Display materials" };
        string[] delete = { "Delete materials" };

        public DeleteMaterialsDialog(string id, IMaterialsService materialsService, MaterialsPost materialsPost, BotSpielUserStateAccessors statePropertyAccessor)
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


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
              confirmDeletePrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> confirmDeletePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new MaterialsPost();
            step.Values[DialogKeyOptions] = (MaterialsPost)step.Options;
            ((MaterialsPost)step.Values[DialogKey]).ixMaterial = ((MaterialsPost)step.Values[DialogKeyOptions]).ixMaterial;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((MaterialsPost)step.Options).sMaterial}:"),
                    RetryPrompt = MessageFactory.Text("Please choose a valid option."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var yesNo = (bool)step.Result;

            if (!yesNo)
            {
                ((MaterialsPost)step.Values[DialogKey]).ixMaterial = -1;
            }

            return await step.EndDialogAsync(
                (MaterialsPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


