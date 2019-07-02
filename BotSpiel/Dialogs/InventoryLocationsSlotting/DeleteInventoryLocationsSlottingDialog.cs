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
    public class DeleteInventoryLocationsSlottingDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteInventoryLocationsSlottingDialogId = "deleteInventoryLocationsSlottingDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteInventoryLocationsSlottingDialog);
        private const string DialogKeyOptions = "deleteInventoryLocationsSlottingDialogOptions";
        private const string SearchColumnsKey = "DeleteInventoryLocationsSlottingDialogSearchColumns";
        private const string SearchTextKey = "DeleteInventoryLocationsSlottingDialogSearchText";
        private const string EditColumnsKey = "DeleteInventoryLocationsSlottingDialogEditColumns";
        private const string EditTextKey = "DeleteInventoryLocationsSlottingDialogEditText";
        private const string SelectedRecordKey = "DeleteInventoryLocationsSlottingDialogSelectedRecordKey";

        private readonly IInventoryLocationsSlottingService _inventorylocationsslottingService;
        InventoryLocationsSlottingPost _inventorylocationsslottingPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inventorylocationsslotting" };
        string[] edit = { "Edit inventorylocationsslotting" };
        string[] details = { "Display inventorylocationsslotting" };
        string[] delete = { "Delete inventorylocationsslotting" };

        public DeleteInventoryLocationsSlottingDialog(string id, IInventoryLocationsSlottingService inventorylocationsslottingService, InventoryLocationsSlottingPost inventorylocationsslottingPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inventorylocationsslottingService = inventorylocationsslottingService;
            _inventorylocationsslottingPost = inventorylocationsslottingPost;

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
            step.Values[DialogKey] = new InventoryLocationsSlottingPost();
            step.Values[DialogKeyOptions] = (InventoryLocationsSlottingPost)step.Options;
            ((InventoryLocationsSlottingPost)step.Values[DialogKey]).ixInventoryLocationSlotting = ((InventoryLocationsSlottingPost)step.Values[DialogKeyOptions]).ixInventoryLocationSlotting;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((InventoryLocationsSlottingPost)step.Options).sInventoryLocationSlotting}:"),
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
                ((InventoryLocationsSlottingPost)step.Values[DialogKey]).ixInventoryLocationSlotting = -1;
            }

            return await step.EndDialogAsync(
                (InventoryLocationsSlottingPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


