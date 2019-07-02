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
    public class CreateInventoryLocationsSlottingDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateInventoryLocationsSlottingDialogId = "createInventoryLocationsSlottingDialog";
       private const string InventoryLocationPromptId = "inventorylocationPrompt";
        private const string MaterialPromptId = "materialPrompt";
        private const string MinimumBaseUnitQuantityPromptId = "minimumbaseunitquantityPrompt";
        private const string MaximumBaseUnitQuantityPromptId = "maximumbaseunitquantityPrompt";

        private const string DialogKey = nameof(CreateInventoryLocationsSlottingDialog);
        private const string DialogKeyOptions = "createInventoryLocationsSlottingDialogOptions";
        private const string SearchColumnsKey = "CreateInventoryLocationsSlottingDialogSearchColumns";
        private const string SearchTextKey = "CreateInventoryLocationsSlottingDialogSearchText";
        private const string EditColumnsKey = "CreateInventoryLocationsSlottingDialogEditColumns";
        private const string EditTextKey = "CreateInventoryLocationsSlottingDialogEditText";
        private const string SelectedRecordKey = "CreateInventoryLocationsSlottingDialogSelectedRecordKey";

        private readonly IInventoryLocationsSlottingService _inventorylocationsslottingService;
        InventoryLocationsSlottingPost _inventorylocationsslottingPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inventorylocationsslotting" };
        string[] edit = { "Edit inventorylocationsslotting" };
        string[] details = { "Display inventorylocationsslotting" };
        string[] delete = { "Delete inventorylocationsslotting" };

        public CreateInventoryLocationsSlottingDialog(string id, IInventoryLocationsSlottingService inventorylocationsslottingService, InventoryLocationsSlottingPost inventorylocationsslottingPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inventorylocationsslottingService = inventorylocationsslottingService;
            _inventorylocationsslottingPost = inventorylocationsslottingPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> inventorylocationslottingValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_inventorylocationsslottingService.VerifyInventoryLocationSlottingUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The inventorylocationslotting {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new ChoicePrompt(InventoryLocationPromptId));
            AddDialog(new ChoicePrompt(MaterialPromptId));
            AddDialog(new NumberPrompt<float>(MinimumBaseUnitQuantityPromptId));
            AddDialog(new NumberPrompt<float>(MaximumBaseUnitQuantityPromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             InventoryLocationPrompt,
              MaterialPrompt,
              MinimumBaseUnitQuantityPrompt,
              MaximumBaseUnitQuantityPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> InventoryLocationPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new InventoryLocationsSlottingPost();

            return await step.PromptAsync(
                InventoryLocationPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a InventoryLocation:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventorylocationsslottingService.selectInventoryLocations().Select(ct => ct.sInventoryLocation).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> MaterialPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _InventoryLocation = (FoundChoice)step.Result;
            var ixInventoryLocation = _inventorylocationsslottingService.selectInventoryLocations().Where(ct => ct.sInventoryLocation == _InventoryLocation.Value).Select(ct => ct.ixInventoryLocation).First();
            ((InventoryLocationsSlottingPost)step.Values[DialogKey]).ixInventoryLocation = ixInventoryLocation;

            return await step.PromptAsync(
                MaterialPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Material:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventorylocationsslottingService.selectMaterials().Select(ct => ct.sMaterial).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> MinimumBaseUnitQuantityPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Material = (FoundChoice)step.Result;
            var ixMaterial = _inventorylocationsslottingService.selectMaterials().Where(ct => ct.sMaterial == _Material.Value).Select(ct => ct.ixMaterial).First();
            ((InventoryLocationsSlottingPost)step.Values[DialogKey]).ixMaterial = ixMaterial;

            return await step.PromptAsync(
                MinimumBaseUnitQuantityPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a MinimumBaseUnitQuantity:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> MaximumBaseUnitQuantityPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nMinimumBaseUnitQuantity = step.Result;
            ((InventoryLocationsSlottingPost)step.Values[DialogKey]).nMinimumBaseUnitQuantity = Convert.ToDouble(nMinimumBaseUnitQuantity);

            return await step.PromptAsync(
                MaximumBaseUnitQuantityPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a MaximumBaseUnitQuantity:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nMaximumBaseUnitQuantity = (Double)step.Result;
            ((InventoryLocationsSlottingPost)step.Values[DialogKey]).nMaximumBaseUnitQuantity = nMaximumBaseUnitQuantity;


            return await step.EndDialogAsync(
                (InventoryLocationsSlottingPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


