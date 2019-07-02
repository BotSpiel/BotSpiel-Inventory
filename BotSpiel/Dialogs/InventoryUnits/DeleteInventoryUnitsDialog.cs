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
    public class DeleteInventoryUnitsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteInventoryUnitsDialogId = "deleteInventoryUnitsDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteInventoryUnitsDialog);
        private const string DialogKeyOptions = "deleteInventoryUnitsDialogOptions";
        private const string SearchColumnsKey = "DeleteInventoryUnitsDialogSearchColumns";
        private const string SearchTextKey = "DeleteInventoryUnitsDialogSearchText";
        private const string EditColumnsKey = "DeleteInventoryUnitsDialogEditColumns";
        private const string EditTextKey = "DeleteInventoryUnitsDialogEditText";
        private const string SelectedRecordKey = "DeleteInventoryUnitsDialogSelectedRecordKey";

        private readonly IInventoryUnitsService _inventoryunitsService;
        InventoryUnitsPost _inventoryunitsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inventoryunits" };
        string[] edit = { "Edit inventoryunits" };
        string[] details = { "Display inventoryunits" };
        string[] delete = { "Delete inventoryunits" };

        public DeleteInventoryUnitsDialog(string id, IInventoryUnitsService inventoryunitsService, InventoryUnitsPost inventoryunitsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inventoryunitsService = inventoryunitsService;
            _inventoryunitsPost = inventoryunitsPost;

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
            step.Values[DialogKey] = new InventoryUnitsPost();
            step.Values[DialogKeyOptions] = (InventoryUnitsPost)step.Options;
            ((InventoryUnitsPost)step.Values[DialogKey]).ixInventoryUnit = ((InventoryUnitsPost)step.Values[DialogKeyOptions]).ixInventoryUnit;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((InventoryUnitsPost)step.Options).sInventoryUnit}:"),
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
                ((InventoryUnitsPost)step.Values[DialogKey]).ixInventoryUnit = -1;
            }

            return await step.EndDialogAsync(
                (InventoryUnitsPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


