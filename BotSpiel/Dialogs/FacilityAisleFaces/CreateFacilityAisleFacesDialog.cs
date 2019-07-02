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
    public class CreateFacilityAisleFacesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateFacilityAisleFacesDialogId = "createFacilityAisleFacesDialog";
       private const string FacilityAisleFacePromptId = "facilityaislefacePrompt";
        private const string FacilityFloorPromptId = "facilityfloorPrompt";
        private const string SequencePromptId = "sequencePrompt";
        private const string BaySequenceTypePromptId = "baysequencetypePrompt";
        private const string PairedAisleFacePromptId = "pairedaislefacePrompt";
        private const string LogicalOrientationPromptId = "logicalorientationPrompt";
        private const string AisleFaceStorageTypePromptId = "aislefacestoragetypePrompt";
        private const string XOffsetPromptId = "xoffsetPrompt";
        private const string XOffsetUnitPromptId = "xoffsetunitPrompt";
        private const string YOffsetPromptId = "yoffsetPrompt";
        private const string YOffsetUnitPromptId = "yoffsetunitPrompt";
        private const string LevelsPromptId = "levelsPrompt";
        private const string DefaultNumberOfBaysPromptId = "defaultnumberofbaysPrompt";
        private const string DefaultNumberOfSlotsInBayPromptId = "defaultnumberofslotsinbayPrompt";
        private const string DefaultFacilityZonePromptId = "defaultfacilityzonePrompt";
        private const string DefaultLocationFunctionPromptId = "defaultlocationfunctionPrompt";
        private const string DefaultInventoryLocationSizePromptId = "defaultinventorylocationsizePrompt";

        private const string DialogKey = nameof(CreateFacilityAisleFacesDialog);
        private const string DialogKeyOptions = "createFacilityAisleFacesDialogOptions";
        private const string SearchColumnsKey = "CreateFacilityAisleFacesDialogSearchColumns";
        private const string SearchTextKey = "CreateFacilityAisleFacesDialogSearchText";
        private const string EditColumnsKey = "CreateFacilityAisleFacesDialogEditColumns";
        private const string EditTextKey = "CreateFacilityAisleFacesDialogEditText";
        private const string SelectedRecordKey = "CreateFacilityAisleFacesDialogSelectedRecordKey";

        private readonly IFacilityAisleFacesService _facilityaislefacesService;
        FacilityAisleFacesPost _facilityaislefacesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit facilityaislefaces" };
        string[] edit = { "Edit facilityaislefaces" };
        string[] details = { "Display facilityaislefaces" };
        string[] delete = { "Delete facilityaislefaces" };

        public CreateFacilityAisleFacesDialog(string id, IFacilityAisleFacesService facilityaislefacesService, FacilityAisleFacesPost facilityaislefacesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _facilityaislefacesService = facilityaislefacesService;
            _facilityaislefacesPost = facilityaislefacesPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> facilityaislefaceValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_facilityaislefacesService.VerifyFacilityAisleFaceUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The facilityaisleface {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(FacilityAisleFacePromptId, facilityaislefaceValidator));
            AddDialog(new ChoicePrompt(FacilityFloorPromptId));
            AddDialog(new NumberPrompt<Int64>(SequencePromptId));
            AddDialog(new ChoicePrompt(BaySequenceTypePromptId));
            AddDialog(new ChoicePrompt(PairedAisleFacePromptId));
            AddDialog(new ChoicePrompt(LogicalOrientationPromptId));
            AddDialog(new ChoicePrompt(AisleFaceStorageTypePromptId));
            AddDialog(new NumberPrompt<float>(XOffsetPromptId));
            AddDialog(new ChoicePrompt(XOffsetUnitPromptId));
            AddDialog(new NumberPrompt<float>(YOffsetPromptId));
            AddDialog(new ChoicePrompt(YOffsetUnitPromptId));
            AddDialog(new NumberPrompt<Int32>(LevelsPromptId));
            AddDialog(new NumberPrompt<Int32>(DefaultNumberOfBaysPromptId));
            AddDialog(new NumberPrompt<Int32>(DefaultNumberOfSlotsInBayPromptId));
            AddDialog(new ChoicePrompt(DefaultFacilityZonePromptId));
            AddDialog(new ChoicePrompt(DefaultLocationFunctionPromptId));
            AddDialog(new ChoicePrompt(DefaultInventoryLocationSizePromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             FacilityAisleFacePrompt,
              FacilityFloorPrompt,
              SequencePrompt,
              BaySequenceTypePrompt,
              PairedAisleFacePrompt,
              LogicalOrientationPrompt,
              AisleFaceStorageTypePrompt,
              XOffsetPrompt,
              XOffsetUnitPrompt,
              YOffsetPrompt,
              YOffsetUnitPrompt,
              LevelsPrompt,
              DefaultNumberOfBaysPrompt,
              DefaultNumberOfSlotsInBayPrompt,
              DefaultFacilityZonePrompt,
              DefaultLocationFunctionPrompt,
              DefaultInventoryLocationSizePrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> FacilityAisleFacePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new FacilityAisleFacesPost();

            return await step.PromptAsync(
                FacilityAisleFacePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a FacilityAisleFace:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> FacilityFloorPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sFacilityAisleFace = (string)step.Result;
            ((FacilityAisleFacesPost)step.Values[DialogKey]).sFacilityAisleFace = sFacilityAisleFace;

            return await step.PromptAsync(
                FacilityFloorPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a FacilityFloor:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_facilityaislefacesService.selectFacilityFloors().Select(ct => ct.sFacilityFloor).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> SequencePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _FacilityFloor = (FoundChoice)step.Result;
            var ixFacilityFloor = _facilityaislefacesService.selectFacilityFloors().Where(ct => ct.sFacilityFloor == _FacilityFloor.Value).Select(ct => ct.ixFacilityFloor).First();
            ((FacilityAisleFacesPost)step.Values[DialogKey]).ixFacilityFloor = ixFacilityFloor;

            return await step.PromptAsync(
                SequencePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Sequence:"),
                    RetryPrompt = MessageFactory.Text("Please enter an integer."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> BaySequenceTypePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nSequence = (Int64)step.Result;
            ((FacilityAisleFacesPost)step.Values[DialogKey]).nSequence = nSequence;

            return await step.PromptAsync(
                BaySequenceTypePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a BaySequenceType:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_facilityaislefacesService.selectBaySequenceTypes().Select(ct => ct.sBaySequenceType).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> PairedAisleFacePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _BaySequenceType = (FoundChoice)step.Result;
            var ixBaySequenceType = _facilityaislefacesService.selectBaySequenceTypes().Where(ct => ct.sBaySequenceType == _BaySequenceType.Value).Select(ct => ct.ixBaySequenceType).First();
            ((FacilityAisleFacesPost)step.Values[DialogKey]).ixBaySequenceType = ixBaySequenceType;

            return await step.PromptAsync(
                PairedAisleFacePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a PairedAisleFace:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_facilityaislefacesService.selectFacilityAisleFaces().Select(ct => ct.sFacilityAisleFace).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> LogicalOrientationPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _PairedAisleFace = (FoundChoice)step.Result;
            var ixPairedAisleFace = _facilityaislefacesService.selectFacilityAisleFaces().Where(ct => ct.sFacilityAisleFace == _PairedAisleFace.Value).Select(ct => ct.ixFacilityAisleFace).First();
            ((FacilityAisleFacesPost)step.Values[DialogKey]).ixPairedAisleFace = ixPairedAisleFace;

            return await step.PromptAsync(
                LogicalOrientationPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a LogicalOrientation:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_facilityaislefacesService.selectLogicalOrientations().Select(ct => ct.sLogicalOrientation).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> AisleFaceStorageTypePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _LogicalOrientation = (FoundChoice)step.Result;
            var ixLogicalOrientation = _facilityaislefacesService.selectLogicalOrientations().Where(ct => ct.sLogicalOrientation == _LogicalOrientation.Value).Select(ct => ct.ixLogicalOrientation).First();
            ((FacilityAisleFacesPost)step.Values[DialogKey]).ixLogicalOrientation = ixLogicalOrientation;

            return await step.PromptAsync(
                AisleFaceStorageTypePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a AisleFaceStorageType:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_facilityaislefacesService.selectAisleFaceStorageTypes().Select(ct => ct.sAisleFaceStorageType).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> XOffsetPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _AisleFaceStorageType = (FoundChoice)step.Result;
            var ixAisleFaceStorageType = _facilityaislefacesService.selectAisleFaceStorageTypes().Where(ct => ct.sAisleFaceStorageType == _AisleFaceStorageType.Value).Select(ct => ct.ixAisleFaceStorageType).First();
            ((FacilityAisleFacesPost)step.Values[DialogKey]).ixAisleFaceStorageType = ixAisleFaceStorageType;

            return await step.PromptAsync(
                XOffsetPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a XOffset:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> XOffsetUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nXOffset = step.Result;
            ((FacilityAisleFacesPost)step.Values[DialogKey]).nXOffset = Convert.ToDouble(nXOffset);

            return await step.PromptAsync(
                XOffsetUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a XOffsetUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_facilityaislefacesService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> YOffsetPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _XOffsetUnit = (FoundChoice)step.Result;
            var ixXOffsetUnit = _facilityaislefacesService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _XOffsetUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((FacilityAisleFacesPost)step.Values[DialogKey]).ixXOffsetUnit = ixXOffsetUnit;

            return await step.PromptAsync(
                YOffsetPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a YOffset:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> YOffsetUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nYOffset = step.Result;
            ((FacilityAisleFacesPost)step.Values[DialogKey]).nYOffset = Convert.ToDouble(nYOffset);

            return await step.PromptAsync(
                YOffsetUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a YOffsetUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_facilityaislefacesService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> LevelsPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _YOffsetUnit = (FoundChoice)step.Result;
            var ixYOffsetUnit = _facilityaislefacesService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _YOffsetUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((FacilityAisleFacesPost)step.Values[DialogKey]).ixYOffsetUnit = ixYOffsetUnit;

            return await step.PromptAsync(
                LevelsPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Levels:"),
                    RetryPrompt = MessageFactory.Text("Please enter an integer."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> DefaultNumberOfBaysPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nLevels = (Int32)step.Result;
            ((FacilityAisleFacesPost)step.Values[DialogKey]).nLevels = nLevels;

            return await step.PromptAsync(
                DefaultNumberOfBaysPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a DefaultNumberOfBays:"),
                    RetryPrompt = MessageFactory.Text("Please enter an integer."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> DefaultNumberOfSlotsInBayPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nDefaultNumberOfBays = (Int32)step.Result;
            ((FacilityAisleFacesPost)step.Values[DialogKey]).nDefaultNumberOfBays = nDefaultNumberOfBays;

            return await step.PromptAsync(
                DefaultNumberOfSlotsInBayPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a DefaultNumberOfSlotsInBay:"),
                    RetryPrompt = MessageFactory.Text("Please enter an integer."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> DefaultFacilityZonePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nDefaultNumberOfSlotsInBay = (Int32)step.Result;
            ((FacilityAisleFacesPost)step.Values[DialogKey]).nDefaultNumberOfSlotsInBay = nDefaultNumberOfSlotsInBay;

            return await step.PromptAsync(
                DefaultFacilityZonePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a DefaultFacilityZone:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_facilityaislefacesService.selectFacilityZones().Select(ct => ct.sFacilityZone).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> DefaultLocationFunctionPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _DefaultFacilityZone = (FoundChoice)step.Result;
            var ixDefaultFacilityZone = _facilityaislefacesService.selectFacilityZones().Where(ct => ct.sFacilityZone == _DefaultFacilityZone.Value).Select(ct => ct.ixFacilityZone).First();
            ((FacilityAisleFacesPost)step.Values[DialogKey]).ixDefaultFacilityZone = ixDefaultFacilityZone;

            return await step.PromptAsync(
                DefaultLocationFunctionPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a DefaultLocationFunction:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_facilityaislefacesService.selectLocationFunctions().Select(ct => ct.sLocationFunction).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> DefaultInventoryLocationSizePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _DefaultLocationFunction = (FoundChoice)step.Result;
            var ixDefaultLocationFunction = _facilityaislefacesService.selectLocationFunctions().Where(ct => ct.sLocationFunction == _DefaultLocationFunction.Value).Select(ct => ct.ixLocationFunction).First();
            ((FacilityAisleFacesPost)step.Values[DialogKey]).ixDefaultLocationFunction = ixDefaultLocationFunction;

            return await step.PromptAsync(
                DefaultInventoryLocationSizePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a DefaultInventoryLocationSize:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_facilityaislefacesService.selectInventoryLocationSizes().Select(ct => ct.sInventoryLocationSize).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixDefaultInventoryLocationSize = (Int64)step.Result;
            ((FacilityAisleFacesPost)step.Values[DialogKey]).ixDefaultInventoryLocationSize = ixDefaultInventoryLocationSize;


            return await step.EndDialogAsync(
                (FacilityAisleFacesPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


