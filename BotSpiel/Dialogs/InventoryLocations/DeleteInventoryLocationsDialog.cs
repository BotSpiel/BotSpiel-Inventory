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
    public class DeleteInventoryLocationsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string DeleteInventoryLocationsDialogId = "deleteInventoryLocationsDialog";

        private const string ChoicePromptId = "choicePrompt";
        private const string TextPromptId = "textPrompt";
        private const string DateTimePromptId = "datetimePrompt";
        private const string ConfirmPromptId = "confirmPrompt";
        private const string NumberPromptIntId = "numberIntPrompt";
        private const string NumberPromptBigIntId = "numberBigIntPrompt";
        private const string NumberPromptFloatId = "numberFloatPrompt";

        private const string DialogKey = nameof(DeleteInventoryLocationsDialog);
        private const string DialogKeyOptions = "deleteInventoryLocationsDialogOptions";
        private const string SearchColumnsKey = "DeleteInventoryLocationsDialogSearchColumns";
        private const string SearchTextKey = "DeleteInventoryLocationsDialogSearchText";
        private const string EditColumnsKey = "DeleteInventoryLocationsDialogEditColumns";
        private const string EditTextKey = "DeleteInventoryLocationsDialogEditText";
        private const string SelectedRecordKey = "DeleteInventoryLocationsDialogSelectedRecordKey";

        private readonly IInventoryLocationsService _inventorylocationsService;
        InventoryLocationsPost _inventorylocationsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inventorylocations" };
        string[] edit = { "Edit inventorylocations" };
        string[] details = { "Display inventorylocations" };
        string[] delete = { "Delete inventorylocations" };

        public DeleteInventoryLocationsDialog(string id, IInventoryLocationsService inventorylocationsService, InventoryLocationsPost inventorylocationsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inventorylocationsService = inventorylocationsService;
            _inventorylocationsPost = inventorylocationsPost;

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
            step.Values[DialogKey] = new InventoryLocationsPost();
            step.Values[DialogKeyOptions] = (InventoryLocationsPost)step.Options;
            ((InventoryLocationsPost)step.Values[DialogKey]).ixInventoryLocation = ((InventoryLocationsPost)step.Values[DialogKeyOptions]).ixInventoryLocation;

            return await step.PromptAsync(
                ConfirmPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Are you sure you want to delete {((InventoryLocationsPost)step.Options).sInventoryLocation}:"),
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
                ((InventoryLocationsPost)step.Values[DialogKey]).ixInventoryLocation = -1;
            }

            return await step.EndDialogAsync(
                (InventoryLocationsPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


