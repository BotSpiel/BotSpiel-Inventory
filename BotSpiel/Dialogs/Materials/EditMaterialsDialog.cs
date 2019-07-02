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
    public class EditMaterialsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditMaterialsDialogId = "editMaterialsDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string MaterialPromptId = "materialPrompt";
        private const string DescriptionPromptId = "descriptionPrompt";
        private const string MaterialTypePromptId = "materialtypePrompt";
        private const string BaseUnitPromptId = "baseunitPrompt";
        private const string TrackSerialNumberPromptId = "trackserialnumberPrompt";
        private const string TrackBatchNumberPromptId = "trackbatchnumberPrompt";
        private const string TrackExpiryPromptId = "trackexpiryPrompt";
        private const string DensityPromptId = "densityPrompt";
        private const string DensityUnitPromptId = "densityunitPrompt";
        private const string ShelflifePromptId = "shelflifePrompt";
        private const string ShelflifeUnitPromptId = "shelflifeunitPrompt";
        private const string LengthPromptId = "lengthPrompt";
        private const string LengthUnitPromptId = "lengthunitPrompt";
        private const string WidthPromptId = "widthPrompt";
        private const string WidthUnitPromptId = "widthunitPrompt";
        private const string HeightPromptId = "heightPrompt";
        private const string HeightUnitPromptId = "heightunitPrompt";
        private const string WeightPromptId = "weightPrompt";
        private const string WeightUnitPromptId = "weightunitPrompt";

        private const string DialogKey = nameof(EditMaterialsDialog);
        private const string DialogKeyOptions = "editMaterialsDialogOptions";
        private const string SearchColumnsKey = "EditMaterialsDialogSearchColumns";
        private const string SearchTextKey = "EditMaterialsDialogSearchText";
        private const string EditColumnsKey = "EditMaterialsDialogEditColumns";
        private const string EditTextKey = "EditMaterialsDialogEditText";
        private const string SelectedRecordKey = "EditMaterialsDialogSelectedRecordKey";

        private readonly IMaterialsService _materialsService;
        MaterialsPost _materialsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit materials" };
        string[] edit = { "Edit materials" };
        string[] details = { "Display materials" };
        string[] delete = { "Delete materials" };

        public EditMaterialsDialog(string id, IMaterialsService materialsService, MaterialsPost materialsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _materialsService = materialsService;
            _materialsPost = materialsPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> materialValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_materialsService.VerifyMaterialUnique(_materialsPost.ixMaterial, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The material {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(MaterialPromptId, materialValidator));
            AddDialog(new TextPrompt(DescriptionPromptId));
            AddDialog(new ChoicePrompt(MaterialTypePromptId));
            AddDialog(new ChoicePrompt(BaseUnitPromptId));
            AddDialog(new ConfirmPrompt(TrackSerialNumberPromptId));
            AddDialog(new ConfirmPrompt(TrackBatchNumberPromptId));
            AddDialog(new ConfirmPrompt(TrackExpiryPromptId));
            AddDialog(new NumberPrompt<float>(DensityPromptId));
            AddDialog(new ChoicePrompt(DensityUnitPromptId));
            AddDialog(new NumberPrompt<float>(ShelflifePromptId));
            AddDialog(new ChoicePrompt(ShelflifeUnitPromptId));
            AddDialog(new NumberPrompt<float>(LengthPromptId));
            AddDialog(new ChoicePrompt(LengthUnitPromptId));
            AddDialog(new NumberPrompt<float>(WidthPromptId));
            AddDialog(new ChoicePrompt(WidthUnitPromptId));
            AddDialog(new NumberPrompt<float>(HeightPromptId));
            AddDialog(new ChoicePrompt(HeightUnitPromptId));
            AddDialog(new NumberPrompt<float>(WeightPromptId));
            AddDialog(new ChoicePrompt(WeightUnitPromptId));

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

            step.Values[DialogKey] = new MaterialsPost();
            step.Values[DialogKeyOptions] = (MaterialsPost)step.Options;
            step.Values[DialogKey] = _materialsService.GetPost(((MaterialsPost)step.Options).ixMaterial);
            _materialsPost = _materialsService.GetPost(((MaterialsPost)step.Options).ixMaterial);
            step.Values[SelectedRecordKey] = _materialsPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("Materials");

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
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "Description":
					returnResult = await step.PromptAsync(
						DescriptionPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Description:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "MaterialType":
					returnResult = await step.PromptAsync(
						MaterialTypePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a MaterialType:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_materialsService.selectMaterialTypes().Select(ct => ct.sMaterialType).ToList()),
						},
						cancellationToken);
                    break;
                case "BaseUnit":
					returnResult = await step.PromptAsync(
						BaseUnitPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a BaseUnit:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_materialsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
						},
						cancellationToken);
                    break;
                case "TrackSerialNumber":
					returnResult = await step.PromptAsync(
						TrackSerialNumberPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a TrackSerialNumber:"),
							RetryPrompt = MessageFactory.Text("Please choose a valid option."),
						},
						cancellationToken);
                    break;
                case "TrackBatchNumber":
					returnResult = await step.PromptAsync(
						TrackBatchNumberPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a TrackBatchNumber:"),
							RetryPrompt = MessageFactory.Text("Please choose a valid option."),
						},
						cancellationToken);
                    break;
                case "TrackExpiry":
					returnResult = await step.PromptAsync(
						TrackExpiryPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a TrackExpiry:"),
							RetryPrompt = MessageFactory.Text("Please choose a valid option."),
						},
						cancellationToken);
                    break;
                case "Density":
					returnResult = await step.PromptAsync(
						DensityPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Density:"),
							RetryPrompt = MessageFactory.Text("Please enter a number."),
						},
						cancellationToken);
                    break;
                case "DensityUnit":
					returnResult = await step.PromptAsync(
						DensityUnitPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a DensityUnit:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_materialsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
						},
						cancellationToken);
                    break;
                case "Shelflife":
					returnResult = await step.PromptAsync(
						ShelflifePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Shelflife:"),
							RetryPrompt = MessageFactory.Text("Please enter a number."),
						},
						cancellationToken);
                    break;
                case "ShelflifeUnit":
					returnResult = await step.PromptAsync(
						ShelflifeUnitPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a ShelflifeUnit:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_materialsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_materialsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_materialsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_materialsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
						},
						cancellationToken);
                    break;
                case "Weight":
					returnResult = await step.PromptAsync(
						WeightPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Weight:"),
							RetryPrompt = MessageFactory.Text("Please enter a number."),
						},
						cancellationToken);
                    break;
                case "WeightUnit":
					returnResult = await step.PromptAsync(
						WeightUnitPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a WeightUnit:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_materialsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
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
					var sMaterial = (string)step.Result;
					((MaterialsPost)step.Values[DialogKey]).sMaterial = sMaterial;
                    break;
                case "Description":
					var sDescription = (string)step.Result;
					((MaterialsPost)step.Values[DialogKey]).sDescription = sDescription;
                    break;
                case "MaterialType":
					FoundChoice _MaterialType = (FoundChoice)step.Result;
					var ixMaterialType = _materialsService.selectMaterialTypes().Where(ct => ct.sMaterialType == _MaterialType.Value).Select(ct => ct.ixMaterialType).First();
					((MaterialsPost)step.Values[DialogKey]).ixMaterialType = ixMaterialType;
                    break;
                case "BaseUnit":
					FoundChoice _BaseUnit = (FoundChoice)step.Result;
					var ixBaseUnit = _materialsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _BaseUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((MaterialsPost)step.Values[DialogKey]).ixBaseUnit = ixBaseUnit;
                    break;
                case "TrackSerialNumber":
					var bTrackSerialNumber = (bool)step.Result;
					((MaterialsPost)step.Values[DialogKey]).bTrackSerialNumber = bTrackSerialNumber;
                    break;
                case "TrackBatchNumber":
					var bTrackBatchNumber = (bool)step.Result;
					((MaterialsPost)step.Values[DialogKey]).bTrackBatchNumber = bTrackBatchNumber;
                    break;
                case "TrackExpiry":
					var bTrackExpiry = (bool)step.Result;
					((MaterialsPost)step.Values[DialogKey]).bTrackExpiry = bTrackExpiry;
                    break;
                case "Density":
					var nDensity = step.Result;
					((MaterialsPost)step.Values[DialogKey]).nDensity = Convert.ToDouble(nDensity);
                    break;
                case "DensityUnit":
					FoundChoice _DensityUnit = (FoundChoice)step.Result;
					var ixDensityUnit = _materialsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _DensityUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((MaterialsPost)step.Values[DialogKey]).ixDensityUnit = ixDensityUnit;
                    break;
                case "Shelflife":
					var nShelflife = step.Result;
					((MaterialsPost)step.Values[DialogKey]).nShelflife = Convert.ToDouble(nShelflife);
                    break;
                case "ShelflifeUnit":
					FoundChoice _ShelflifeUnit = (FoundChoice)step.Result;
					var ixShelflifeUnit = _materialsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _ShelflifeUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((MaterialsPost)step.Values[DialogKey]).ixShelflifeUnit = ixShelflifeUnit;
                    break;
                case "Length":
					var nLength = step.Result;
					((MaterialsPost)step.Values[DialogKey]).nLength = Convert.ToDouble(nLength);
                    break;
                case "LengthUnit":
					FoundChoice _LengthUnit = (FoundChoice)step.Result;
					var ixLengthUnit = _materialsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _LengthUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((MaterialsPost)step.Values[DialogKey]).ixLengthUnit = ixLengthUnit;
                    break;
                case "Width":
					var nWidth = step.Result;
					((MaterialsPost)step.Values[DialogKey]).nWidth = Convert.ToDouble(nWidth);
                    break;
                case "WidthUnit":
					FoundChoice _WidthUnit = (FoundChoice)step.Result;
					var ixWidthUnit = _materialsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _WidthUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((MaterialsPost)step.Values[DialogKey]).ixWidthUnit = ixWidthUnit;
                    break;
                case "Height":
					var nHeight = step.Result;
					((MaterialsPost)step.Values[DialogKey]).nHeight = Convert.ToDouble(nHeight);
                    break;
                case "HeightUnit":
					FoundChoice _HeightUnit = (FoundChoice)step.Result;
					var ixHeightUnit = _materialsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _HeightUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((MaterialsPost)step.Values[DialogKey]).ixHeightUnit = ixHeightUnit;
                    break;
                case "Weight":
					var nWeight = step.Result;
					((MaterialsPost)step.Values[DialogKey]).nWeight = Convert.ToDouble(nWeight);
                    break;
                case "WeightUnit":
					FoundChoice _WeightUnit = (FoundChoice)step.Result;
					var ixWeightUnit = _materialsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _WeightUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((MaterialsPost)step.Values[DialogKey]).ixWeightUnit = ixWeightUnit;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (MaterialsPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


