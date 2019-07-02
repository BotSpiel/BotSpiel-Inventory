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
    public class EditInventoryLocationSizesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditInventoryLocationSizesDialogId = "editInventoryLocationSizesDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string InventoryLocationSizePromptId = "inventorylocationsizePrompt";
        private const string LengthPromptId = "lengthPrompt";
        private const string LengthUnitPromptId = "lengthunitPrompt";
        private const string WidthPromptId = "widthPrompt";
        private const string WidthUnitPromptId = "widthunitPrompt";
        private const string HeightPromptId = "heightPrompt";
        private const string HeightUnitPromptId = "heightunitPrompt";
        private const string UsableVolumePromptId = "usablevolumePrompt";
        private const string UsableVolumeUnitPromptId = "usablevolumeunitPrompt";

        private const string DialogKey = nameof(EditInventoryLocationSizesDialog);
        private const string DialogKeyOptions = "editInventoryLocationSizesDialogOptions";
        private const string SearchColumnsKey = "EditInventoryLocationSizesDialogSearchColumns";
        private const string SearchTextKey = "EditInventoryLocationSizesDialogSearchText";
        private const string EditColumnsKey = "EditInventoryLocationSizesDialogEditColumns";
        private const string EditTextKey = "EditInventoryLocationSizesDialogEditText";
        private const string SelectedRecordKey = "EditInventoryLocationSizesDialogSelectedRecordKey";

        private readonly IInventoryLocationSizesService _inventorylocationsizesService;
        InventoryLocationSizesPost _inventorylocationsizesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inventorylocationsizes" };
        string[] edit = { "Edit inventorylocationsizes" };
        string[] details = { "Display inventorylocationsizes" };
        string[] delete = { "Delete inventorylocationsizes" };

        public EditInventoryLocationSizesDialog(string id, IInventoryLocationSizesService inventorylocationsizesService, InventoryLocationSizesPost inventorylocationsizesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inventorylocationsizesService = inventorylocationsizesService;
            _inventorylocationsizesPost = inventorylocationsizesPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> inventorylocationsizeValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_inventorylocationsizesService.VerifyInventoryLocationSizeUnique(_inventorylocationsizesPost.ixInventoryLocationSize, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The inventorylocationsize {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(InventoryLocationSizePromptId, inventorylocationsizeValidator));
            AddDialog(new NumberPrompt<float>(LengthPromptId));
            AddDialog(new ChoicePrompt(LengthUnitPromptId));
            AddDialog(new NumberPrompt<float>(WidthPromptId));
            AddDialog(new ChoicePrompt(WidthUnitPromptId));
            AddDialog(new NumberPrompt<float>(HeightPromptId));
            AddDialog(new ChoicePrompt(HeightUnitPromptId));
            AddDialog(new NumberPrompt<float>(UsableVolumePromptId));
            AddDialog(new ChoicePrompt(UsableVolumeUnitPromptId));

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

            step.Values[DialogKey] = new InventoryLocationSizesPost();
            step.Values[DialogKeyOptions] = (InventoryLocationSizesPost)step.Options;
            step.Values[DialogKey] = _inventorylocationsizesService.GetPost(((InventoryLocationSizesPost)step.Options).ixInventoryLocationSize);
            _inventorylocationsizesPost = _inventorylocationsizesService.GetPost(((InventoryLocationSizesPost)step.Options).ixInventoryLocationSize);
            step.Values[SelectedRecordKey] = _inventorylocationsizesPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("InventoryLocationSizes");

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
                case "InventoryLocationSize":
					returnResult = await step.PromptAsync(
						InventoryLocationSizePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a InventoryLocationSize:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "Length":
					returnResult = await step.PromptAsync(
						LengthPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Length:"),
							RetryPrompt = MessageFactory.Text("Please enter a number."),
						},
						cancellationToken);
                    break;
                case "LengthUnit":
					returnResult = await step.PromptAsync(
						LengthUnitPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a LengthUnit:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
						},
						cancellationToken);
                    break;
                case "Width":
					returnResult = await step.PromptAsync(
						WidthPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Width:"),
							RetryPrompt = MessageFactory.Text("Please enter a number."),
						},
						cancellationToken);
                    break;
                case "WidthUnit":
					returnResult = await step.PromptAsync(
						WidthUnitPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a WidthUnit:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
						},
						cancellationToken);
                    break;
                case "Height":
					returnResult = await step.PromptAsync(
						HeightPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Height:"),
							RetryPrompt = MessageFactory.Text("Please enter a number."),
						},
						cancellationToken);
                    break;
                case "HeightUnit":
					returnResult = await step.PromptAsync(
						HeightUnitPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a HeightUnit:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
						},
						cancellationToken);
                    break;
                case "UsableVolume":
					returnResult = await step.PromptAsync(
						UsableVolumePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a UsableVolume:"),
							RetryPrompt = MessageFactory.Text("Please enter a number."),
						},
						cancellationToken);
                    break;
                case "UsableVolumeUnit":
					returnResult = await step.PromptAsync(
						UsableVolumeUnitPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a UsableVolumeUnit:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventorylocationsizesService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
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
                case "InventoryLocationSize":
					var sInventoryLocationSize = (string)step.Result;
					((InventoryLocationSizesPost)step.Values[DialogKey]).sInventoryLocationSize = sInventoryLocationSize;
                    break;
                case "Length":
					var nLength = step.Result;
					((InventoryLocationSizesPost)step.Values[DialogKey]).nLength = Convert.ToDouble(nLength);
                    break;
                case "LengthUnit":
					FoundChoice _LengthUnit = (FoundChoice)step.Result;
					var ixLengthUnit = _inventorylocationsizesService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _LengthUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((InventoryLocationSizesPost)step.Values[DialogKey]).ixLengthUnit = ixLengthUnit;
                    break;
                case "Width":
					var nWidth = step.Result;
					((InventoryLocationSizesPost)step.Values[DialogKey]).nWidth = Convert.ToDouble(nWidth);
                    break;
                case "WidthUnit":
					FoundChoice _WidthUnit = (FoundChoice)step.Result;
					var ixWidthUnit = _inventorylocationsizesService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _WidthUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((InventoryLocationSizesPost)step.Values[DialogKey]).ixWidthUnit = ixWidthUnit;
                    break;
                case "Height":
					var nHeight = step.Result;
					((InventoryLocationSizesPost)step.Values[DialogKey]).nHeight = Convert.ToDouble(nHeight);
                    break;
                case "HeightUnit":
					FoundChoice _HeightUnit = (FoundChoice)step.Result;
					var ixHeightUnit = _inventorylocationsizesService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _HeightUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((InventoryLocationSizesPost)step.Values[DialogKey]).ixHeightUnit = ixHeightUnit;
                    break;
                case "UsableVolume":
					var nUsableVolume = step.Result;
					((InventoryLocationSizesPost)step.Values[DialogKey]).nUsableVolume = Convert.ToDouble(nUsableVolume);
                    break;
                case "UsableVolumeUnit":
					FoundChoice _UsableVolumeUnit = (FoundChoice)step.Result;
					var ixUsableVolumeUnit = _inventorylocationsizesService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _UsableVolumeUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((InventoryLocationSizesPost)step.Values[DialogKey]).ixUsableVolumeUnit = ixUsableVolumeUnit;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (InventoryLocationSizesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


