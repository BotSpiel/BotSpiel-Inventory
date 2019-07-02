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
    public class EditInventoryLocationsSlottingDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditInventoryLocationsSlottingDialogId = "editInventoryLocationsSlottingDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string InventoryLocationPromptId = "inventorylocationPrompt";
        private const string MaterialPromptId = "materialPrompt";
        private const string MinimumBaseUnitQuantityPromptId = "minimumbaseunitquantityPrompt";
        private const string MaximumBaseUnitQuantityPromptId = "maximumbaseunitquantityPrompt";

        private const string DialogKey = nameof(EditInventoryLocationsSlottingDialog);
        private const string DialogKeyOptions = "editInventoryLocationsSlottingDialogOptions";
        private const string SearchColumnsKey = "EditInventoryLocationsSlottingDialogSearchColumns";
        private const string SearchTextKey = "EditInventoryLocationsSlottingDialogSearchText";
        private const string EditColumnsKey = "EditInventoryLocationsSlottingDialogEditColumns";
        private const string EditTextKey = "EditInventoryLocationsSlottingDialogEditText";
        private const string SelectedRecordKey = "EditInventoryLocationsSlottingDialogSelectedRecordKey";

        private readonly IInventoryLocationsSlottingService _inventorylocationsslottingService;
        InventoryLocationsSlottingPost _inventorylocationsslottingPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inventorylocationsslotting" };
        string[] edit = { "Edit inventorylocationsslotting" };
        string[] details = { "Display inventorylocationsslotting" };
        string[] delete = { "Delete inventorylocationsslotting" };

        public EditInventoryLocationsSlottingDialog(string id, IInventoryLocationsSlottingService inventorylocationsslottingService, InventoryLocationsSlottingPost inventorylocationsslottingPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_inventorylocationsslottingService.VerifyInventoryLocationSlottingUnique(_inventorylocationsslottingPost.ixInventoryLocationSlotting, value))
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

            AddDialog(new ChoicePrompt(ChoicePromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
              chooseEditColumnPrompt,
              editColumnPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> chooseEditColumnPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            string editColumn = "";
            string editText = "";

            step.Values[DialogKey] = new InventoryLocationsSlottingPost();
            step.Values[DialogKeyOptions] = (InventoryLocationsSlottingPost)step.Options;
            step.Values[DialogKey] = _inventorylocationsslottingService.GetPost(((InventoryLocationsSlottingPost)step.Options).ixInventoryLocationSlotting);
            _inventorylocationsslottingPost = _inventorylocationsslottingService.GetPost(((InventoryLocationsSlottingPost)step.Options).ixInventoryLocationSlotting);
            step.Values[SelectedRecordKey] = _inventorylocationsslottingPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("InventoryLocationsSlotting");

            return await step.PromptAsync(
                ChoicePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Please choose an attribute to change:"),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(entitySearchColumns),
                },
                cancellationToken);
        }



        private async Task<DialogTurnResult> editColumnPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice editColumn = (FoundChoice)step.Result;
            step.Values[EditColumnsKey] = editColumn.Value;
            DialogTurnResult returnResult = new DialogTurnResult(0);

            switch (step.Values[EditColumnsKey])
            {
                case "InventoryLocation":
					returnResult = await step.PromptAsync(
						InventoryLocationPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a InventoryLocation:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventorylocationsslottingService.selectInventoryLocations().Select(ct => ct.sInventoryLocation).ToList()),
						},
						cancellationToken);
                    break;
                case "Material":
					returnResult = await step.PromptAsync(
						MaterialPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Material:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventorylocationsslottingService.selectMaterials().Select(ct => ct.sMaterial).ToList()),
						},
						cancellationToken);
                    break;
                case "MinimumBaseUnitQuantity":
					returnResult = await step.PromptAsync(
						MinimumBaseUnitQuantityPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a MinimumBaseUnitQuantity:"),
							RetryPrompt = MessageFactory.Text("Please enter a number."),
						},
						cancellationToken);
                    break;
                case "MaximumBaseUnitQuantity":
					returnResult = await step.PromptAsync(
						MaximumBaseUnitQuantityPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a MaximumBaseUnitQuantity:"),
							RetryPrompt = MessageFactory.Text("Please enter a number."),
						},
						cancellationToken);
                    break;

                default:
                    break;
            }

            return returnResult;
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {

            switch (step.Values[EditColumnsKey])
            {
                case "InventoryLocation":
					FoundChoice _InventoryLocation = (FoundChoice)step.Result;
					var ixInventoryLocation = _inventorylocationsslottingService.selectInventoryLocations().Where(ct => ct.sInventoryLocation == _InventoryLocation.Value).Select(ct => ct.ixInventoryLocation).First();
					((InventoryLocationsSlottingPost)step.Values[DialogKey]).ixInventoryLocation = ixInventoryLocation;
                    break;
                case "Material":
					FoundChoice _Material = (FoundChoice)step.Result;
					var ixMaterial = _inventorylocationsslottingService.selectMaterials().Where(ct => ct.sMaterial == _Material.Value).Select(ct => ct.ixMaterial).First();
					((InventoryLocationsSlottingPost)step.Values[DialogKey]).ixMaterial = ixMaterial;
                    break;
                case "MinimumBaseUnitQuantity":
					var nMinimumBaseUnitQuantity = step.Result;
					((InventoryLocationsSlottingPost)step.Values[DialogKey]).nMinimumBaseUnitQuantity = Convert.ToDouble(nMinimumBaseUnitQuantity);
                    break;
                case "MaximumBaseUnitQuantity":
					var nMaximumBaseUnitQuantity = step.Result;
					((InventoryLocationsSlottingPost)step.Values[DialogKey]).nMaximumBaseUnitQuantity = Convert.ToDouble(nMaximumBaseUnitQuantity);
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (InventoryLocationsSlottingPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


