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
    public class CreateHandlingUnitsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateHandlingUnitsDialogId = "createHandlingUnitsDialog";
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

        private const string DialogKey = nameof(CreateHandlingUnitsDialog);
        private const string DialogKeyOptions = "createHandlingUnitsDialogOptions";
        private const string SearchColumnsKey = "CreateHandlingUnitsDialogSearchColumns";
        private const string SearchTextKey = "CreateHandlingUnitsDialogSearchText";
        private const string EditColumnsKey = "CreateHandlingUnitsDialogEditColumns";
        private const string EditTextKey = "CreateHandlingUnitsDialogEditText";
        private const string SelectedRecordKey = "CreateHandlingUnitsDialogSelectedRecordKey";

        private readonly IHandlingUnitsService _handlingunitsService;
        HandlingUnitsPost _handlingunitsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit handlingunits" };
        string[] edit = { "Edit handlingunits" };
        string[] details = { "Display handlingunits" };
        string[] delete = { "Delete handlingunits" };

        public CreateHandlingUnitsDialog(string id, IHandlingUnitsService handlingunitsService, HandlingUnitsPost handlingunitsPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_handlingunitsService.VerifyHandlingUnitUnique(0L, value))
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


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             HandlingUnitPrompt,
              HandlingUnitTypePrompt,
              ParentHandlingUnitPrompt,
              PackingMaterialPrompt,
              MaterialHandlingUnitConfigurationPrompt,
              LengthPrompt,
              LengthUnitPrompt,
              WidthPrompt,
              WidthUnitPrompt,
              HeightPrompt,
              HeightUnitPrompt,
              WeightPrompt,
              StatusPrompt,
              WeightUnitPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> HandlingUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new HandlingUnitsPost();

            return await step.PromptAsync(
                HandlingUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a HandlingUnit:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> HandlingUnitTypePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sHandlingUnit = (string)step.Result;
            ((HandlingUnitsPost)step.Values[DialogKey]).sHandlingUnit = sHandlingUnit;

            return await step.PromptAsync(
                HandlingUnitTypePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a HandlingUnitType:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_handlingunitsService.selectHandlingUnitTypes().Select(ct => ct.sHandlingUnitType).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> ParentHandlingUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _HandlingUnitType = (FoundChoice)step.Result;
            var ixHandlingUnitType = _handlingunitsService.selectHandlingUnitTypes().Where(ct => ct.sHandlingUnitType == _HandlingUnitType.Value).Select(ct => ct.ixHandlingUnitType).First();
            ((HandlingUnitsPost)step.Values[DialogKey]).ixHandlingUnitType = ixHandlingUnitType;

            return await step.PromptAsync(
                ParentHandlingUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a ParentHandlingUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_handlingunitsService.selectHandlingUnits().Select(ct => ct.sHandlingUnit).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> PackingMaterialPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _ParentHandlingUnit = (FoundChoice)step.Result;
            var ixParentHandlingUnit = _handlingunitsService.selectHandlingUnits().Where(ct => ct.sHandlingUnit == _ParentHandlingUnit.Value).Select(ct => ct.ixHandlingUnit).First();
            ((HandlingUnitsPost)step.Values[DialogKey]).ixParentHandlingUnit = ixParentHandlingUnit;

            return await step.PromptAsync(
                PackingMaterialPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a PackingMaterial:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_handlingunitsService.selectMaterials().Select(ct => ct.sMaterial).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> MaterialHandlingUnitConfigurationPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _PackingMaterial = (FoundChoice)step.Result;
            var ixPackingMaterial = _handlingunitsService.selectMaterials().Where(ct => ct.sMaterial == _PackingMaterial.Value).Select(ct => ct.ixMaterial).First();
            ((HandlingUnitsPost)step.Values[DialogKey]).ixPackingMaterial = ixPackingMaterial;

            return await step.PromptAsync(
                MaterialHandlingUnitConfigurationPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a MaterialHandlingUnitConfiguration:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_handlingunitsService.selectMaterialHandlingUnitConfigurations().Select(ct => ct.sMaterialHandlingUnitConfiguration).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> LengthPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _MaterialHandlingUnitConfiguration = (FoundChoice)step.Result;
            var ixMaterialHandlingUnitConfiguration = _handlingunitsService.selectMaterialHandlingUnitConfigurations().Where(ct => ct.sMaterialHandlingUnitConfiguration == _MaterialHandlingUnitConfiguration.Value).Select(ct => ct.ixMaterialHandlingUnitConfiguration).First();
            ((HandlingUnitsPost)step.Values[DialogKey]).ixMaterialHandlingUnitConfiguration = ixMaterialHandlingUnitConfiguration;

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
            ((HandlingUnitsPost)step.Values[DialogKey]).nLength = Convert.ToDouble(nLength);

            return await step.PromptAsync(
                LengthUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a LengthUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_handlingunitsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> WidthPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _LengthUnit = (FoundChoice)step.Result;
            var ixLengthUnit = _handlingunitsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _LengthUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((HandlingUnitsPost)step.Values[DialogKey]).ixLengthUnit = ixLengthUnit;

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
            ((HandlingUnitsPost)step.Values[DialogKey]).nWidth = Convert.ToDouble(nWidth);

            return await step.PromptAsync(
                WidthUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a WidthUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_handlingunitsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> HeightPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _WidthUnit = (FoundChoice)step.Result;
            var ixWidthUnit = _handlingunitsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _WidthUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((HandlingUnitsPost)step.Values[DialogKey]).ixWidthUnit = ixWidthUnit;

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
            ((HandlingUnitsPost)step.Values[DialogKey]).nHeight = Convert.ToDouble(nHeight);

            return await step.PromptAsync(
                HeightUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a HeightUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_handlingunitsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> WeightPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _HeightUnit = (FoundChoice)step.Result;
            var ixHeightUnit = _handlingunitsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _HeightUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((HandlingUnitsPost)step.Values[DialogKey]).ixHeightUnit = ixHeightUnit;

            return await step.PromptAsync(
                WeightPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Weight:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StatusPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nWeight = step.Result;
            ((HandlingUnitsPost)step.Values[DialogKey]).nWeight = Convert.ToDouble(nWeight);

            return await step.PromptAsync(
                StatusPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Status:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_handlingunitsService.selectStatuses().Select(ct => ct.sStatus).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> WeightUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Status = (FoundChoice)step.Result;
            var ixStatus = _handlingunitsService.selectStatuses().Where(ct => ct.sStatus == _Status.Value).Select(ct => ct.ixStatus).First();
            ((HandlingUnitsPost)step.Values[DialogKey]).ixStatus = ixStatus;

            return await step.PromptAsync(
                WeightUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a WeightUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_handlingunitsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixWeightUnit = (Int64)step.Result;
            ((HandlingUnitsPost)step.Values[DialogKey]).ixWeightUnit = ixWeightUnit;


            return await step.EndDialogAsync(
                (HandlingUnitsPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


