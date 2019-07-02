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
    public class DeleteMaterialHandlingUnitConfigurationsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteMaterialHandlingUnitConfigurationsDialogId = "deleteMaterialHandlingUnitConfigurationsDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteMaterialHandlingUnitConfigurationsDialog);
        private const string DialogKeyOptions = "deleteMaterialHandlingUnitConfigurationsDialogOptions";
        private const string SearchColumnsKey = "DeleteMaterialHandlingUnitConfigurationsDialogSearchColumns";
        private const string SearchTextKey = "DeleteMaterialHandlingUnitConfigurationsDialogSearchText";
        private const string EditColumnsKey = "DeleteMaterialHandlingUnitConfigurationsDialogEditColumns";
        private const string EditTextKey = "DeleteMaterialHandlingUnitConfigurationsDialogEditText";
        private const string SelectedRecordKey = "DeleteMaterialHandlingUnitConfigurationsDialogSelectedRecordKey";

        private readonly IMaterialHandlingUnitConfigurationsService _materialhandlingunitconfigurationsService;
        MaterialHandlingUnitConfigurationsPost _materialhandlingunitconfigurationsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit materialhandlingunitconfigurations" };
        string[] edit = { "Edit materialhandlingunitconfigurations" };
        string[] details = { "Display materialhandlingunitconfigurations" };
        string[] delete = { "Delete materialhandlingunitconfigurations" };

        public DeleteMaterialHandlingUnitConfigurationsDialog(string id, IMaterialHandlingUnitConfigurationsService materialhandlingunitconfigurationsService, MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurationsPost, BotSpielUserStateAccessors statePropertyAccessor)
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
            step.Values[DialogKey] = new MaterialHandlingUnitConfigurationsPost();
            step.Values[DialogKeyOptions] = (MaterialHandlingUnitConfigurationsPost)step.Options;
            ((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).ixMaterialHandlingUnitConfiguration = ((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKeyOptions]).ixMaterialHandlingUnitConfiguration;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((MaterialHandlingUnitConfigurationsPost)step.Options).sMaterialHandlingUnitConfiguration}:"),
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
                ((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).ixMaterialHandlingUnitConfiguration = -1;
            }

            return await step.EndDialogAsync(
                (MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


