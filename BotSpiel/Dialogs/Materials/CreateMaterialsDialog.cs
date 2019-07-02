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
    public class CreateMaterialsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateMaterialsDialogId = "createMaterialsDialog";
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

        private const string DialogKey = nameof(CreateMaterialsDialog);
        private const string DialogKeyOptions = "createMaterialsDialogOptions";
        private const string SearchColumnsKey = "CreateMaterialsDialogSearchColumns";
        private const string SearchTextKey = "CreateMaterialsDialogSearchText";
        private const string EditColumnsKey = "CreateMaterialsDialogEditColumns";
        private const string EditTextKey = "CreateMaterialsDialogEditText";
        private const string SelectedRecordKey = "CreateMaterialsDialogSelectedRecordKey";

        private readonly IMaterialsService _materialsService;
        MaterialsPost _materialsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit materials" };
        string[] edit = { "Edit materials" };
        string[] details = { "Display materials" };
        string[] delete = { "Delete materials" };

        public CreateMaterialsDialog(string id, IMaterialsService materialsService, MaterialsPost materialsPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_materialsService.VerifyMaterialUnique(0L, value))
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


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             MaterialPrompt,
              DescriptionPrompt,
              MaterialTypePrompt,
              BaseUnitPrompt,
              TrackSerialNumberPrompt,
              TrackBatchNumberPrompt,
              TrackExpiryPrompt,
              DensityPrompt,
              DensityUnitPrompt,
              ShelflifePrompt,
              ShelflifeUnitPrompt,
              LengthPrompt,
              LengthUnitPrompt,
              WidthPrompt,
              WidthUnitPrompt,
              HeightPrompt,
              HeightUnitPrompt,
              WeightPrompt,
              WeightUnitPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> MaterialPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new MaterialsPost();

            return await step.PromptAsync(
                MaterialPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Material:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> DescriptionPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sMaterial = (string)step.Result;
            ((MaterialsPost)step.Values[DialogKey]).sMaterial = sMaterial;

            return await step.PromptAsync(
                DescriptionPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Description:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> MaterialTypePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sDescription = (string)step.Result;
            ((MaterialsPost)step.Values[DialogKey]).sDescription = sDescription;

            return await step.PromptAsync(
                MaterialTypePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a MaterialType:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_materialsService.selectMaterialTypes().Select(ct => ct.sMaterialType).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> BaseUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _MaterialType = (FoundChoice)step.Result;
            var ixMaterialType = _materialsService.selectMaterialTypes().Where(ct => ct.sMaterialType == _MaterialType.Value).Select(ct => ct.ixMaterialType).First();
            ((MaterialsPost)step.Values[DialogKey]).ixMaterialType = ixMaterialType;

            return await step.PromptAsync(
                BaseUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a BaseUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_materialsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> TrackSerialNumberPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _BaseUnit = (FoundChoice)step.Result;
            var ixBaseUnit = _materialsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _BaseUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((MaterialsPost)step.Values[DialogKey]).ixBaseUnit = ixBaseUnit;

            return await step.PromptAsync(
                TrackSerialNumberPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a TrackSerialNumber:"),
                    RetryPrompt = MessageFactory.Text("Please choose a valid option."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> TrackBatchNumberPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var bTrackSerialNumber = (bool)step.Result;
            ((MaterialsPost)step.Values[DialogKey]).bTrackSerialNumber = bTrackSerialNumber;

            return await step.PromptAsync(
                TrackBatchNumberPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a TrackBatchNumber:"),
                    RetryPrompt = MessageFactory.Text("Please choose a valid option."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> TrackExpiryPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var bTrackBatchNumber = (bool)step.Result;
            ((MaterialsPost)step.Values[DialogKey]).bTrackBatchNumber = bTrackBatchNumber;

            return await step.PromptAsync(
                TrackExpiryPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a TrackExpiry:"),
                    RetryPrompt = MessageFactory.Text("Please choose a valid option."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> DensityPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var bTrackExpiry = (bool)step.Result;
            ((MaterialsPost)step.Values[DialogKey]).bTrackExpiry = bTrackExpiry;

            return await step.PromptAsync(
                DensityPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Density:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> DensityUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nDensity = step.Result;
            ((MaterialsPost)step.Values[DialogKey]).nDensity = Convert.ToDouble(nDensity);

            return await step.PromptAsync(
                DensityUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a DensityUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_materialsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> ShelflifePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _DensityUnit = (FoundChoice)step.Result;
            var ixDensityUnit = _materialsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _DensityUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((MaterialsPost)step.Values[DialogKey]).ixDensityUnit = ixDensityUnit;

            return await step.PromptAsync(
                ShelflifePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Shelflife:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> ShelflifeUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nShelflife = step.Result;
            ((MaterialsPost)step.Values[DialogKey]).nShelflife = Convert.ToDouble(nShelflife);

            return await step.PromptAsync(
                ShelflifeUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a ShelflifeUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_materialsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> LengthPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _ShelflifeUnit = (FoundChoice)step.Result;
            var ixShelflifeUnit = _materialsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _ShelflifeUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((MaterialsPost)step.Values[DialogKey]).ixShelflifeUnit = ixShelflifeUnit;

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
            ((MaterialsPost)step.Values[DialogKey]).nLength = Convert.ToDouble(nLength);

            return await step.PromptAsync(
                LengthUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a LengthUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_materialsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> WidthPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _LengthUnit = (FoundChoice)step.Result;
            var ixLengthUnit = _materialsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _LengthUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((MaterialsPost)step.Values[DialogKey]).ixLengthUnit = ixLengthUnit;

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
            ((MaterialsPost)step.Values[DialogKey]).nWidth = Convert.ToDouble(nWidth);

            return await step.PromptAsync(
                WidthUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a WidthUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_materialsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> HeightPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _WidthUnit = (FoundChoice)step.Result;
            var ixWidthUnit = _materialsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _WidthUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((MaterialsPost)step.Values[DialogKey]).ixWidthUnit = ixWidthUnit;

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
            ((MaterialsPost)step.Values[DialogKey]).nHeight = Convert.ToDouble(nHeight);

            return await step.PromptAsync(
                HeightUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a HeightUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_materialsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> WeightPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _HeightUnit = (FoundChoice)step.Result;
            var ixHeightUnit = _materialsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _HeightUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((MaterialsPost)step.Values[DialogKey]).ixHeightUnit = ixHeightUnit;

            return await step.PromptAsync(
                WeightPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Weight:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> WeightUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nWeight = step.Result;
            ((MaterialsPost)step.Values[DialogKey]).nWeight = Convert.ToDouble(nWeight);

            return await step.PromptAsync(
                WeightUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a WeightUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_materialsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixWeightUnit = (Int64)step.Result;
            ((MaterialsPost)step.Values[DialogKey]).ixWeightUnit = ixWeightUnit;


            return await step.EndDialogAsync(
                (MaterialsPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


