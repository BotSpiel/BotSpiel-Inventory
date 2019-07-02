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
    public class CreateInventoryLocationsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateInventoryLocationsDialogId = "createInventoryLocationsDialog";
       private const string InventoryLocationPromptId = "inventorylocationPrompt";
        private const string LocationFunctionPromptId = "locationfunctionPrompt";
        private const string CompanyPromptId = "companyPrompt";
        private const string FacilityFloorPromptId = "facilityfloorPrompt";
        private const string FacilityZonePromptId = "facilityzonePrompt";
        private const string FacilityWorkAreaPromptId = "facilityworkareaPrompt";
        private const string FacilityAisleFacePromptId = "facilityaislefacePrompt";
        private const string LevelPromptId = "levelPrompt";
        private const string BayPromptId = "bayPrompt";
        private const string SlotPromptId = "slotPrompt";
        private const string InventoryLocationSizePromptId = "inventorylocationsizePrompt";
        private const string SequencePromptId = "sequencePrompt";
        private const string XOffsetPromptId = "xoffsetPrompt";
        private const string XOffsetUnitPromptId = "xoffsetunitPrompt";
        private const string YOffsetPromptId = "yoffsetPrompt";
        private const string YOffsetUnitPromptId = "yoffsetunitPrompt";
        private const string ZOffsetPromptId = "zoffsetPrompt";
        private const string ZOffsetUnitPromptId = "zoffsetunitPrompt";
        private const string LatitudePromptId = "latitudePrompt";
        private const string LongitudePromptId = "longitudePrompt";
        private const string TrackUtilisationPromptId = "trackutilisationPrompt";
        private const string UtilisationPercentPromptId = "utilisationpercentPrompt";
        private const string QueuedUtilisationPercentPromptId = "queuedutilisationpercentPrompt";

        private const string DialogKey = nameof(CreateInventoryLocationsDialog);
        private const string DialogKeyOptions = "createInventoryLocationsDialogOptions";
        private const string SearchColumnsKey = "CreateInventoryLocationsDialogSearchColumns";
        private const string SearchTextKey = "CreateInventoryLocationsDialogSearchText";
        private const string EditColumnsKey = "CreateInventoryLocationsDialogEditColumns";
        private const string EditTextKey = "CreateInventoryLocationsDialogEditText";
        private const string SelectedRecordKey = "CreateInventoryLocationsDialogSelectedRecordKey";

        private readonly IInventoryLocationsService _inventorylocationsService;
        InventoryLocationsPost _inventorylocationsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inventorylocations" };
        string[] edit = { "Edit inventorylocations" };
        string[] details = { "Display inventorylocations" };
        string[] delete = { "Delete inventorylocations" };

        public CreateInventoryLocationsDialog(string id, IInventoryLocationsService inventorylocationsService, InventoryLocationsPost inventorylocationsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inventorylocationsService = inventorylocationsService;
            _inventorylocationsPost = inventorylocationsPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> inventorylocationValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_inventorylocationsService.VerifyInventoryLocationUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The inventorylocation {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(InventoryLocationPromptId, inventorylocationValidator));
            AddDialog(new ChoicePrompt(LocationFunctionPromptId));
            AddDialog(new ChoicePrompt(CompanyPromptId));
            AddDialog(new ChoicePrompt(FacilityFloorPromptId));
            AddDialog(new ChoicePrompt(FacilityZonePromptId));
            AddDialog(new ChoicePrompt(FacilityWorkAreaPromptId));
            AddDialog(new ChoicePrompt(FacilityAisleFacePromptId));
            AddDialog(new TextPrompt(LevelPromptId));
            AddDialog(new TextPrompt(BayPromptId));
            AddDialog(new TextPrompt(SlotPromptId));
            AddDialog(new ChoicePrompt(InventoryLocationSizePromptId));
            AddDialog(new NumberPrompt<Int64>(SequencePromptId));
            AddDialog(new NumberPrompt<float>(XOffsetPromptId));
            AddDialog(new ChoicePrompt(XOffsetUnitPromptId));
            AddDialog(new NumberPrompt<float>(YOffsetPromptId));
            AddDialog(new ChoicePrompt(YOffsetUnitPromptId));
            AddDialog(new NumberPrompt<float>(ZOffsetPromptId));
            AddDialog(new ChoicePrompt(ZOffsetUnitPromptId));
            AddDialog(new TextPrompt(LatitudePromptId));
            AddDialog(new TextPrompt(LongitudePromptId));
            AddDialog(new ConfirmPrompt(TrackUtilisationPromptId));
            AddDialog(new NumberPrompt<float>(UtilisationPercentPromptId));
            AddDialog(new NumberPrompt<float>(QueuedUtilisationPercentPromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             InventoryLocationPrompt,
              LocationFunctionPrompt,
              CompanyPrompt,
              FacilityFloorPrompt,
              FacilityZonePrompt,
              FacilityWorkAreaPrompt,
              FacilityAisleFacePrompt,
              LevelPrompt,
              BayPrompt,
              SlotPrompt,
              InventoryLocationSizePrompt,
              SequencePrompt,
              XOffsetPrompt,
              XOffsetUnitPrompt,
              YOffsetPrompt,
              YOffsetUnitPrompt,
              ZOffsetPrompt,
              ZOffsetUnitPrompt,
              LatitudePrompt,
              LongitudePrompt,
              TrackUtilisationPrompt,
              UtilisationPercentPrompt,
              QueuedUtilisationPercentPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> InventoryLocationPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new InventoryLocationsPost();

            return await step.PromptAsync(
                InventoryLocationPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a InventoryLocation:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> LocationFunctionPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sInventoryLocation = (string)step.Result;
            ((InventoryLocationsPost)step.Values[DialogKey]).sInventoryLocation = sInventoryLocation;

            return await step.PromptAsync(
                LocationFunctionPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a LocationFunction:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectLocationFunctions().Select(ct => ct.sLocationFunction).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CompanyPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _LocationFunction = (FoundChoice)step.Result;
            var ixLocationFunction = _inventorylocationsService.selectLocationFunctions().Where(ct => ct.sLocationFunction == _LocationFunction.Value).Select(ct => ct.ixLocationFunction).First();
            ((InventoryLocationsPost)step.Values[DialogKey]).ixLocationFunction = ixLocationFunction;

            return await step.PromptAsync(
                CompanyPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Company:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectCompanies().Select(ct => ct.sCompany).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> FacilityFloorPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Company = (FoundChoice)step.Result;
            var ixCompany = _inventorylocationsService.selectCompanies().Where(ct => ct.sCompany == _Company.Value).Select(ct => ct.ixCompany).First();
            ((InventoryLocationsPost)step.Values[DialogKey]).ixCompany = ixCompany;

            return await step.PromptAsync(
                FacilityFloorPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a FacilityFloor:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectFacilityFloors().Select(ct => ct.sFacilityFloor).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> FacilityZonePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _FacilityFloor = (FoundChoice)step.Result;
            var ixFacilityFloor = _inventorylocationsService.selectFacilityFloors().Where(ct => ct.sFacilityFloor == _FacilityFloor.Value).Select(ct => ct.ixFacilityFloor).First();
            ((InventoryLocationsPost)step.Values[DialogKey]).ixFacilityFloor = ixFacilityFloor;

            return await step.PromptAsync(
                FacilityZonePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a FacilityZone:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectFacilityZones().Select(ct => ct.sFacilityZone).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> FacilityWorkAreaPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _FacilityZone = (FoundChoice)step.Result;
            var ixFacilityZone = _inventorylocationsService.selectFacilityZones().Where(ct => ct.sFacilityZone == _FacilityZone.Value).Select(ct => ct.ixFacilityZone).First();
            ((InventoryLocationsPost)step.Values[DialogKey]).ixFacilityZone = ixFacilityZone;

            return await step.PromptAsync(
                FacilityWorkAreaPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a FacilityWorkArea:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectFacilityWorkAreas().Select(ct => ct.sFacilityWorkArea).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> FacilityAisleFacePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _FacilityWorkArea = (FoundChoice)step.Result;
            var ixFacilityWorkArea = _inventorylocationsService.selectFacilityWorkAreas().Where(ct => ct.sFacilityWorkArea == _FacilityWorkArea.Value).Select(ct => ct.ixFacilityWorkArea).First();
            ((InventoryLocationsPost)step.Values[DialogKey]).ixFacilityWorkArea = ixFacilityWorkArea;

            return await step.PromptAsync(
                FacilityAisleFacePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a FacilityAisleFace:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectFacilityAisleFaces().Select(ct => ct.sFacilityAisleFace).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> LevelPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _FacilityAisleFace = (FoundChoice)step.Result;
            var ixFacilityAisleFace = _inventorylocationsService.selectFacilityAisleFaces().Where(ct => ct.sFacilityAisleFace == _FacilityAisleFace.Value).Select(ct => ct.ixFacilityAisleFace).First();
            ((InventoryLocationsPost)step.Values[DialogKey]).ixFacilityAisleFace = ixFacilityAisleFace;

            return await step.PromptAsync(
                LevelPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Level:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> BayPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sLevel = (string)step.Result;
            ((InventoryLocationsPost)step.Values[DialogKey]).sLevel = sLevel;

            return await step.PromptAsync(
                BayPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Bay:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> SlotPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sBay = (string)step.Result;
            ((InventoryLocationsPost)step.Values[DialogKey]).sBay = sBay;

            return await step.PromptAsync(
                SlotPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Slot:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> InventoryLocationSizePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sSlot = (string)step.Result;
            ((InventoryLocationsPost)step.Values[DialogKey]).sSlot = sSlot;

            return await step.PromptAsync(
                InventoryLocationSizePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a InventoryLocationSize:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectInventoryLocationSizes().Select(ct => ct.sInventoryLocationSize).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> SequencePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _InventoryLocationSize = (FoundChoice)step.Result;
            var ixInventoryLocationSize = _inventorylocationsService.selectInventoryLocationSizes().Where(ct => ct.sInventoryLocationSize == _InventoryLocationSize.Value).Select(ct => ct.ixInventoryLocationSize).First();
            ((InventoryLocationsPost)step.Values[DialogKey]).ixInventoryLocationSize = ixInventoryLocationSize;

            return await step.PromptAsync(
                SequencePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Sequence:"),
                    RetryPrompt = MessageFactory.Text("Please enter an integer."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> XOffsetPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nSequence = (Int64)step.Result;
            ((InventoryLocationsPost)step.Values[DialogKey]).nSequence = nSequence;

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
            ((InventoryLocationsPost)step.Values[DialogKey]).nXOffset = Convert.ToDouble(nXOffset);

            return await step.PromptAsync(
                XOffsetUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a XOffsetUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> YOffsetPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _XOffsetUnit = (FoundChoice)step.Result;
            var ixXOffsetUnit = _inventorylocationsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _XOffsetUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((InventoryLocationsPost)step.Values[DialogKey]).ixXOffsetUnit = ixXOffsetUnit;

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
            ((InventoryLocationsPost)step.Values[DialogKey]).nYOffset = Convert.ToDouble(nYOffset);

            return await step.PromptAsync(
                YOffsetUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a YOffsetUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> ZOffsetPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _YOffsetUnit = (FoundChoice)step.Result;
            var ixYOffsetUnit = _inventorylocationsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _YOffsetUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((InventoryLocationsPost)step.Values[DialogKey]).ixYOffsetUnit = ixYOffsetUnit;

            return await step.PromptAsync(
                ZOffsetPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a ZOffset:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> ZOffsetUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nZOffset = step.Result;
            ((InventoryLocationsPost)step.Values[DialogKey]).nZOffset = Convert.ToDouble(nZOffset);

            return await step.PromptAsync(
                ZOffsetUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a ZOffsetUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> LatitudePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _ZOffsetUnit = (FoundChoice)step.Result;
            var ixZOffsetUnit = _inventorylocationsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _ZOffsetUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
            ((InventoryLocationsPost)step.Values[DialogKey]).ixZOffsetUnit = ixZOffsetUnit;

            return await step.PromptAsync(
                LatitudePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Latitude:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> LongitudePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sLatitude = (string)step.Result;
            ((InventoryLocationsPost)step.Values[DialogKey]).sLatitude = sLatitude;

            return await step.PromptAsync(
                LongitudePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Longitude:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> TrackUtilisationPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sLongitude = (string)step.Result;
            ((InventoryLocationsPost)step.Values[DialogKey]).sLongitude = sLongitude;

            return await step.PromptAsync(
                TrackUtilisationPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a TrackUtilisation:"),
                    RetryPrompt = MessageFactory.Text("Please choose a valid option."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> UtilisationPercentPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var bTrackUtilisation = (bool)step.Result;
            ((InventoryLocationsPost)step.Values[DialogKey]).bTrackUtilisation = bTrackUtilisation;

            return await step.PromptAsync(
                UtilisationPercentPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a UtilisationPercent:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> QueuedUtilisationPercentPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nUtilisationPercent = step.Result;
            ((InventoryLocationsPost)step.Values[DialogKey]).nUtilisationPercent = Convert.ToDouble(nUtilisationPercent);

            return await step.PromptAsync(
                QueuedUtilisationPercentPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a QueuedUtilisationPercent:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nQueuedUtilisationPercent = (Double)step.Result;
            ((InventoryLocationsPost)step.Values[DialogKey]).nQueuedUtilisationPercent = nQueuedUtilisationPercent;


            return await step.EndDialogAsync(
                (InventoryLocationsPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


