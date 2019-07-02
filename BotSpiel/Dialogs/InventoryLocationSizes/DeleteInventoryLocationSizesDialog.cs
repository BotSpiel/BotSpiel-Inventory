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
    public class DeleteInventoryLocationSizesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteInventoryLocationSizesDialogId = "deleteInventoryLocationSizesDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteInventoryLocationSizesDialog);
        private const string DialogKeyOptions = "deleteInventoryLocationSizesDialogOptions";
        private const string SearchColumnsKey = "DeleteInventoryLocationSizesDialogSearchColumns";
        private const string SearchTextKey = "DeleteInventoryLocationSizesDialogSearchText";
        private const string EditColumnsKey = "DeleteInventoryLocationSizesDialogEditColumns";
        private const string EditTextKey = "DeleteInventoryLocationSizesDialogEditText";
        private const string SelectedRecordKey = "DeleteInventoryLocationSizesDialogSelectedRecordKey";

        private readonly IInventoryLocationSizesService _inventorylocationsizesService;
        InventoryLocationSizesPost _inventorylocationsizesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inventorylocationsizes" };
        string[] edit = { "Edit inventorylocationsizes" };
        string[] details = { "Display inventorylocationsizes" };
        string[] delete = { "Delete inventorylocationsizes" };

        public DeleteInventoryLocationSizesDialog(string id, IInventoryLocationSizesService inventorylocationsizesService, InventoryLocationSizesPost inventorylocationsizesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inventorylocationsizesService = inventorylocationsizesService;
            _inventorylocationsizesPost = inventorylocationsizesPost;

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
            step.Values[DialogKey] = new InventoryLocationSizesPost();
            step.Values[DialogKeyOptions] = (InventoryLocationSizesPost)step.Options;
            ((InventoryLocationSizesPost)step.Values[DialogKey]).ixInventoryLocationSize = ((InventoryLocationSizesPost)step.Values[DialogKeyOptions]).ixInventoryLocationSize;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((InventoryLocationSizesPost)step.Options).sInventoryLocationSize}:"),
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
                ((InventoryLocationSizesPost)step.Values[DialogKey]).ixInventoryLocationSize = -1;
            }

            return await step.EndDialogAsync(
                (InventoryLocationSizesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


