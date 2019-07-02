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
    public class CreateMaterialHandlingUnitConfigurationsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateMaterialHandlingUnitConfigurationsDialogId = "createMaterialHandlingUnitConfigurationsDialog";
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

        private const string DialogKey = nameof(CreateMaterialHandlingUnitConfigurationsDialog);
        private const string DialogKeyOptions = "createMaterialHandlingUnitConfigurationsDialogOptions";
        private const string SearchColumnsKey = "CreateMaterialHandlingUnitConfigurationsDialogSearchColumns";
        private const string SearchTextKey = "CreateMaterialHandlingUnitConfigurationsDialogSearchText";
        private const string EditColumnsKey = "CreateMaterialHandlingUnitConfigurationsDialogEditColumns";
        private const string EditTextKey = "CreateMaterialHandlingUnitConfigurationsDialogEditText";
        private const string SelectedRecordKey = "CreateMaterialHandlingUnitConfigurationsDialogSelectedRecordKey";

        private readonly IMaterialHandlingUnitConfigurationsService _materialhandlingunitconfigurationsService;
        MaterialHandlingUnitConfigurationsPost _materialhandlingunitconfigurationsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit materialhandlingunitconfigurations" };
        string[] edit = { "Edit materialhandlingunitconfigurations" };
        string[] details = { "Display materialhandlingunitconfigurations" };
        string[] delete = { "Delete materialhandlingunitconfigurations" };

        public CreateMaterialHandlingUnitConfigurationsDialog(string id, IMaterialHandlingUnitConfigurationsService materialhandlingunitconfigurationsService, MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurationsPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_materialhandlingunitconfigurationsService.VerifyMaterialHandlingUnitConfigurationUnique(0L, value))
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


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             MaterialPrompt,
              NestingLevelPrompt,
              HandlingUnitTypePrompt,
              QuantityPrompt,
              LengthPrompt,
              LengthUnitPrompt,
              WidthPrompt,
              WidthUnitPrompt,
              HeightPrompt,
              HeightUnitPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> MaterialPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new MaterialHandlingUnitConfigurationsPost();

            return await step.PromptAsync(
                MaterialPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Material:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_materialhandlingunitconfigurationsService.selectMaterials().Select(ct => ct.sMaterial).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> NestingLevelPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Material = (FoundChoice)step.Result;
            var ixMaterial = _materialhandlingunitconfigurationsService.selectMaterials().Where(ct => ct.sMaterial == _Material.Value).Select(ct => ct.ixMaterial).First();
            ((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).ixMaterial = ixMaterial;

            return await step.PromptAsync(
                NestingLevelPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a NestingLevel:"),
                    RetryPrompt = MessageFactory.Text("Please enter an integer."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> HandlingUnitTypePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nNestingLevel = (Int32)step.Result;
            ((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).nNestingLevel = nNestingLevel;

            return await step.PromptAsync(
                HandlingUnitTypePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a HandlingUnitType:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_materialhandlingunitconfigurationsService.selectHandlingUnitTypes().Select(ct => ct.sHandlingUnitType).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> QuantityPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _HandlingUnitType = (FoundChoice)step.Result;
            var ixHandlingUnitType = _materialhandlingunitconfigurationsService.selectHandlingUnitTypes().Where(ct => ct.sHandlingUnitType == _HandlingUnitType.Value).Select(ct => ct.ixHandlingUnitType).First();
            ((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).ixHandlingUnitType = ixHandlingUnitType;

            return await step.PromptAsync(
                QuantityPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Quantity:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> LengthPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nQuantity = step.Result;
            ((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).nQuantity = Convert.ToDouble(nQuantity);

            return await step.PromptAsync(
                LengthPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Length:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> LengthUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nLength = step.Result;
            ((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).nLength = Convert.ToDouble(nLength);

            return await step.PromptAsync(
                LengthUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a LengthUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> WidthPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _LengthUnit = (FoundChoice)step.Result;
            var ixLengthUnit = _materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _LengthUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).ixLengthUnit = ixLengthUnit;

            return await step.PromptAsync(
                WidthPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Width:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> WidthUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nWidth = step.Result;
            ((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).nWidth = Convert.ToDouble(nWidth);

            return await step.PromptAsync(
                WidthUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a WidthUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> HeightPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _WidthUnit = (FoundChoice)step.Result;
            var ixWidthUnit = _materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _WidthUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).ixWidthUnit = ixWidthUnit;

            return await step.PromptAsync(
                HeightPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Height:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> HeightUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nHeight = step.Result;
            ((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).nHeight = Convert.ToDouble(nHeight);

            return await step.PromptAsync(
                HeightUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a HeightUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_materialhandlingunitconfigurationsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixHeightUnit = (Int64)step.Result;
            ((MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey]).ixHeightUnit = ixHeightUnit;


            return await step.EndDialogAsync(
                (MaterialHandlingUnitConfigurationsPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


