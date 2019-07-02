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
    public class EditHandlingUnitsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditHandlingUnitsDialogId = "editHandlingUnitsDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string HandlingUnitPromptId = "handlingunitPrompt";
        private const string HandlingUnitTypePromptId = "handlingunittypePrompt";
        private const string ParentHandlingUnitPromptId = "parenthandlingunitPrompt";
        private const string PackingMaterialPromptId = "packingmaterialPrompt";
        private const string MaterialHandlingUnitConfigurationPromptId = "materialhandlingunitconfigurationPrompt";
        private const string LengthPromptId = "lengthPrompt";
        private const string LengthUnitPromptId = "lengthunitPrompt";
        private const string WidthPromptId = "widthPrompt";
        private const string WidthUnitPromptId = "widthunitPrompt";
        private const string HeightPromptId = "heightPrompt";
        private const string HeightUnitPromptId = "heightunitPrompt";
        private const string WeightPromptId = "weightPrompt";
        private const string StatusPromptId = "statusPrompt";
        private const string WeightUnitPromptId = "weightunitPrompt";

        private const string DialogKey = nameof(EditHandlingUnitsDialog);
        private const string DialogKeyOptions = "editHandlingUnitsDialogOptions";
        private const string SearchColumnsKey = "EditHandlingUnitsDialogSearchColumns";
        private const string SearchTextKey = "EditHandlingUnitsDialogSearchText";
        private const string EditColumnsKey = "EditHandlingUnitsDialogEditColumns";
        private const string EditTextKey = "EditHandlingUnitsDialogEditText";
        private const string SelectedRecordKey = "EditHandlingUnitsDialogSelectedRecordKey";

        private readonly IHandlingUnitsService _handlingunitsService;
        HandlingUnitsPost _handlingunitsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit handlingunits" };
        string[] edit = { "Edit handlingunits" };
        string[] details = { "Display handlingunits" };
        string[] delete = { "Delete handlingunits" };

        public EditHandlingUnitsDialog(string id, IHandlingUnitsService handlingunitsService, HandlingUnitsPost handlingunitsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _handlingunitsService = handlingunitsService;
            _handlingunitsPost = handlingunitsPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> handlingunitValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_handlingunitsService.VerifyHandlingUnitUnique(_handlingunitsPost.ixHandlingUnit, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The handlingunit {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(HandlingUnitPromptId, handlingunitValidator));
            AddDialog(new ChoicePrompt(HandlingUnitTypePromptId));
            AddDialog(new ChoicePrompt(ParentHandlingUnitPromptId));
            AddDialog(new ChoicePrompt(PackingMaterialPromptId));
            AddDialog(new ChoicePrompt(MaterialHandlingUnitConfigurationPromptId));
            AddDialog(new NumberPrompt<float>(LengthPromptId));
            AddDialog(new ChoicePrompt(LengthUnitPromptId));
            AddDialog(new NumberPrompt<float>(WidthPromptId));
            AddDialog(new ChoicePrompt(WidthUnitPromptId));
            AddDialog(new NumberPrompt<float>(HeightPromptId));
            AddDialog(new ChoicePrompt(HeightUnitPromptId));
            AddDialog(new NumberPrompt<float>(WeightPromptId));
            AddDialog(new ChoicePrompt(StatusPromptId));
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

            step.Values[DialogKey] = new HandlingUnitsPost();
            step.Values[DialogKeyOptions] = (HandlingUnitsPost)step.Options;
            step.Values[DialogKey] = _handlingunitsService.GetPost(((HandlingUnitsPost)step.Options).ixHandlingUnit);
            _handlingunitsPost = _handlingunitsService.GetPost(((HandlingUnitsPost)step.Options).ixHandlingUnit);
            step.Values[SelectedRecordKey] = _handlingunitsPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("HandlingUnits");

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
                case "HandlingUnit":
					returnResult = await step.PromptAsync(
						HandlingUnitPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a HandlingUnit:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
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
							Choices = ChoiceFactory.ToChoices(_handlingunitsService.selectHandlingUnitTypes().Select(ct => ct.sHandlingUnitType).ToList()),
						},
						cancellationToken);
                    break;
                case "ParentHandlingUnit":
					returnResult = await step.PromptAsync(
						ParentHandlingUnitPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a ParentHandlingUnit:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_handlingunitsService.selectHandlingUnits().Select(ct => ct.sHandlingUnit).ToList()),
						},
						cancellationToken);
                    break;
                case "PackingMaterial":
					returnResult = await step.PromptAsync(
						PackingMaterialPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a PackingMaterial:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_handlingunitsService.selectMaterials().Select(ct => ct.sMaterial).ToList()),
						},
						cancellationToken);
                    break;
                case "MaterialHandlingUnitConfiguration":
					returnResult = await step.PromptAsync(
						MaterialHandlingUnitConfigurationPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a MaterialHandlingUnitConfiguration:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_handlingunitsService.selectMaterialHandlingUnitConfigurations().Select(ct => ct.sMaterialHandlingUnitConfiguration).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_handlingunitsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_handlingunitsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_handlingunitsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
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
                case "Status":
					returnResult = await step.PromptAsync(
						StatusPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Status:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_handlingunitsService.selectStatuses().Select(ct => ct.sStatus).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_handlingunitsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
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
                case "HandlingUnit":
					var sHandlingUnit = (string)step.Result;
					((HandlingUnitsPost)step.Values[DialogKey]).sHandlingUnit = sHandlingUnit;
                    break;
                case "HandlingUnitType":
					FoundChoice _HandlingUnitType = (FoundChoice)step.Result;
					var ixHandlingUnitType = _handlingunitsService.selectHandlingUnitTypes().Where(ct => ct.sHandlingUnitType == _HandlingUnitType.Value).Select(ct => ct.ixHandlingUnitType).First();
					((HandlingUnitsPost)step.Values[DialogKey]).ixHandlingUnitType = ixHandlingUnitType;
                    break;
                case "ParentHandlingUnit":
					FoundChoice _ParentHandlingUnit = (FoundChoice)step.Result;
					var ixParentHandlingUnit = _handlingunitsService.selectHandlingUnits().Where(ct => ct.sHandlingUnit == _ParentHandlingUnit.Value).Select(ct => ct.ixHandlingUnit).First();
					((HandlingUnitsPost)step.Values[DialogKey]).ixParentHandlingUnit = ixParentHandlingUnit;
                    break;
                case "PackingMaterial":
					FoundChoice _PackingMaterial = (FoundChoice)step.Result;
					var ixPackingMaterial = _handlingunitsService.selectMaterials().Where(ct => ct.sMaterial == _PackingMaterial.Value).Select(ct => ct.ixMaterial).First();
					((HandlingUnitsPost)step.Values[DialogKey]).ixPackingMaterial = ixPackingMaterial;
                    break;
                case "MaterialHandlingUnitConfiguration":
					FoundChoice _MaterialHandlingUnitConfiguration = (FoundChoice)step.Result;
					var ixMaterialHandlingUnitConfiguration = _handlingunitsService.selectMaterialHandlingUnitConfigurations().Where(ct => ct.sMaterialHandlingUnitConfiguration == _MaterialHandlingUnitConfiguration.Value).Select(ct => ct.ixMaterialHandlingUnitConfiguration).First();
					((HandlingUnitsPost)step.Values[DialogKey]).ixMaterialHandlingUnitConfiguration = ixMaterialHandlingUnitConfiguration;
                    break;
                case "Length":
					var nLength = step.Result;
					((HandlingUnitsPost)step.Values[DialogKey]).nLength = Convert.ToDouble(nLength);
                    break;
                case "LengthUnit":
					FoundChoice _LengthUnit = (FoundChoice)step.Result;
					var ixLengthUnit = _handlingunitsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _LengthUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((HandlingUnitsPost)step.Values[DialogKey]).ixLengthUnit = ixLengthUnit;
                    break;
                case "Width":
					var nWidth = step.Result;
					((HandlingUnitsPost)step.Values[DialogKey]).nWidth = Convert.ToDouble(nWidth);
                    break;
                case "WidthUnit":
					FoundChoice _WidthUnit = (FoundChoice)step.Result;
					var ixWidthUnit = _handlingunitsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _WidthUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((HandlingUnitsPost)step.Values[DialogKey]).ixWidthUnit = ixWidthUnit;
                    break;
                case "Height":
					var nHeight = step.Result;
					((HandlingUnitsPost)step.Values[DialogKey]).nHeight = Convert.ToDouble(nHeight);
                    break;
                case "HeightUnit":
					FoundChoice _HeightUnit = (FoundChoice)step.Result;
					var ixHeightUnit = _handlingunitsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _HeightUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((HandlingUnitsPost)step.Values[DialogKey]).ixHeightUnit = ixHeightUnit;
                    break;
                case "Weight":
					var nWeight = step.Result;
					((HandlingUnitsPost)step.Values[DialogKey]).nWeight = Convert.ToDouble(nWeight);
                    break;
                case "Status":
					FoundChoice _Status = (FoundChoice)step.Result;
					var ixStatus = _handlingunitsService.selectStatuses().Where(ct => ct.sStatus == _Status.Value).Select(ct => ct.ixStatus).First();
					((HandlingUnitsPost)step.Values[DialogKey]).ixStatus = ixStatus;
                    break;
                case "WeightUnit":
					FoundChoice _WeightUnit = (FoundChoice)step.Result;
					var ixWeightUnit = _handlingunitsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _WeightUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((HandlingUnitsPost)step.Values[DialogKey]).ixWeightUnit = ixWeightUnit;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (HandlingUnitsPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


