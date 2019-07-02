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
    public class EditMaterialHandlingUnitConfigurationsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditMaterialHandlingUnitConfigurationsDialogId = "editMaterialHandlingUnitConfigurationsDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string MaterialPromptId = "materialPrompt";
        private const string NestingLevelPromptId = "nestinglevelPrompt";
        private const string HandlingUnitTypePromptId = "handlingunittypePrompt";
        private const string QuantityPromptId = "quantityPrompt";
        private const string LengthPromptId = "lengthPrompt";
        private const string LengthUnitPromptId = "lengthunitPrompt";
        private const string WidthPromptId = "widthPrompt";
        private const string WidthUnitPromptId = "widthunitPrompt";
        private const string HeightPromptId = "heightPrompt";
        private const string HeightUnitPromptId = "heightunitPrompt";

        private const string DialogKey = nameof(EditMaterialHandlingUnitConfigurationsDialog);
        private const string DialogKeyOptions = "editMaterialHandlingUnitConfigurationsDialogOptions";
        private const string SearchColumnsKey = "EditMaterialHandlingUnitConfigurationsDialogSearchColumns";
        private const string SearchTextKey = "EditMaterialHandlingUnitConfigurationsDialogSearchText";
        private const string EditColumnsKey = "EditMaterialHandlingUnitConfigurationsDialogEditColumns";
        private const string EditTextKey = "EditMaterialHandlingUnitConfigurationsDialogEditText";
        private const string SelectedRecordKey = "EditMaterialHandlingUnitConfigurationsDialogSelectedRecordKey";

        private readonly IMaterialHandlingUnitConfigurationsService _materialhandlingunitconfigurationsService;
        MaterialHandlingUnitConfigurationsPost _materialhandlingunitconfigurationsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit materialhandlingunitconfigurations" };
        string[] edit = { "Edit materialhandlingunitconfigurations" };
        string[] details = { "Display materialhandlingunitconfigurations" };
        string[] delete = { "Delete materialhandlingunitconfigurations" };

        public EditMaterialHandlingUnitConfigurationsDialog(string id, IMaterialHandlingUnitConfigurationsService materialhandlingunitconfigurationsService, MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurationsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _materialhandlingunitconfigurationsService = materialhandlingunitconfigurationsService;
            _materialhandlingunitconfigurationsPost = materialhandlingunitconfigurationsPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> materialhandlingunitconfigurationValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_materialhandlingunitconfigurationsService.VerifyMaterialHandlingUnitConfigurationUnique(_materialhandlingunitconfigurationsPost.ixMaterialHandlingUnitConfiguration, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The materialhandlingunitconfiguration {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new ChoicePrompt(MaterialPromptId));
            AddDialog(new NumberPrompt<Int32>(NestingLevelPromptId));
            AddDialog(new ChoicePrompt(HandlingUnitTypePromptId));
            AddDialog(new NumberPrompt<float>(QuantityPromptId));
            AddDialog(new NumberPrompt<float>(LengthPromptId));
            AddDialog(new ChoicePrompt(LengthUnitPromptId));
            AddDialog(new NumberPrompt<float>(WidthPromptId));
            AddDialog(new ChoicePrompt(WidthUnitPromptId));
            AddDialog(new NumberPrompt<float>(HeightPromptId));
            AddDialog(new ChoicePrompt(HeightUnitPromptId));

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

            step.Values[DialogKey] = new MaterialHandlingUnitConfigurationsPost();
            step.Values[DialogKeyOptions] = (MaterialHandlingUnitConfigurationsPost)step.Options;
            step.Values[DialogKey] = _materialhandlingunitconfigurationsService.GetPost(((MaterialHandlingUnitConfigurationsPost)step.Options).ixMaterialHandlingUnitConfiguration);
            _materialhandlingunitconfigurationsPost = _materialhandlingunitconfigurationsService.GetPost(((MaterialHandlingUnitConfigurationsPost)step.Options).ixMaterialHandlingUnitConfiguration);
            step.Values[SelectedRecordKey] = _materialhandlingunitconfigurationsPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("MaterialHandlingUnitConfigurations");

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
                case "Material":
					returnResult = await step.PromptAsync(
						MaterialPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Material:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_materialhandlingunitconfigurationsService.selectMaterials().Select(ct => ct.sMaterial).ToList()),
						},
						cancellationToken);
                    break;
                case "NestingLevel":
					returnResult = await step.PromptAsync(
						NestingLevelPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a NestingLevel:"),
							RetryPrompt = MessageFactory.Text("Please enter an integer."),
						},
						cancellationToken);
                    break;
                case "HandlingUnitType":
					returnResult = await step.PromptAsync(
						HandlingUnitTypePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a HandlingUnitType:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_materialhandlingunitconfigurationsService.selectHandlingUnitTypes().Select(ct => ct.sHandlingUnitType).ToList()),
						},
						cancellationToken);
                    break;
                case "Quantity":
					returnResult = await step.PromptAsync(
						QuantityPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Quantity:"),
							RetryPrompt = MessageFactory.Text("Please enter a number."),
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
							Choices = ChoiceFactory.ToChoices(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
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
                case "Material":
					FoundChoice _Material = (FoundChoice)step.Result;
					var ixMaterial = _materialhandlingunitconfigurationsService.selectMaterials().Where(ct => ct.sMaterial == _Material.Value).Select(ct => ct.ixMaterial).First();
					((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).ixMaterial = ixMaterial;
                    break;
                case "NestingLevel":
					var nNestingLevel = (Int32)step.Result;
					((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).nNestingLevel = nNestingLevel;
                    break;
                case "HandlingUnitType":
					FoundChoice _HandlingUnitType = (FoundChoice)step.Result;
					var ixHandlingUnitType = _materialhandlingunitconfigurationsService.selectHandlingUnitTypes().Where(ct => ct.sHandlingUnitType == _HandlingUnitType.Value).Select(ct => ct.ixHandlingUnitType).First();
					((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).ixHandlingUnitType = ixHandlingUnitType;
                    break;
                case "Quantity":
					var nQuantity = step.Result;
					((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).nQuantity = Convert.ToDouble(nQuantity);
                    break;
                case "Length":
					var nLength = step.Result;
					((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).nLength = Convert.ToDouble(nLength);
                    break;
                case "LengthUnit":
					FoundChoice _LengthUnit = (FoundChoice)step.Result;
					var ixLengthUnit = _materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _LengthUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).ixLengthUnit = ixLengthUnit;
                    break;
                case "Width":
					var nWidth = step.Result;
					((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).nWidth = Convert.ToDouble(nWidth);
                    break;
                case "WidthUnit":
					FoundChoice _WidthUnit = (FoundChoice)step.Result;
					var ixWidthUnit = _materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _WidthUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).ixWidthUnit = ixWidthUnit;
                    break;
                case "Height":
					var nHeight = step.Result;
					((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).nHeight = Convert.ToDouble(nHeight);
                    break;
                case "HeightUnit":
					FoundChoice _HeightUnit = (FoundChoice)step.Result;
					var ixHeightUnit = _materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _HeightUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).ixHeightUnit = ixHeightUnit;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


