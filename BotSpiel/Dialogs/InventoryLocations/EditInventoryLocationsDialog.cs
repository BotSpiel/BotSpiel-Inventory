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
    public class EditInventoryLocationsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditInventoryLocationsDialogId = "editInventoryLocationsDialog";

        private const string ChoicePromptId = "choicePrompt";
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

        private const string DialogKey = nameof(EditInventoryLocationsDialog);
        private const string DialogKeyOptions = "editInventoryLocationsDialogOptions";
        private const string SearchColumnsKey = "EditInventoryLocationsDialogSearchColumns";
        private const string SearchTextKey = "EditInventoryLocationsDialogSearchText";
        private const string EditColumnsKey = "EditInventoryLocationsDialogEditColumns";
        private const string EditTextKey = "EditInventoryLocationsDialogEditText";
        private const string SelectedRecordKey = "EditInventoryLocationsDialogSelectedRecordKey";

        private readonly IInventoryLocationsService _inventorylocationsService;
        InventoryLocationsPost _inventorylocationsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inventorylocations" };
        string[] edit = { "Edit inventorylocations" };
        string[] details = { "Display inventorylocations" };
        string[] delete = { "Delete inventorylocations" };

        public EditInventoryLocationsDialog(string id, IInventoryLocationsService inventorylocationsService, InventoryLocationsPost inventorylocationsPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_inventorylocationsService.VerifyInventoryLocationUnique(_inventorylocationsPost.ixInventoryLocation, value))
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

            step.Values[DialogKey] = new InventoryLocationsPost();
            step.Values[DialogKeyOptions] = (InventoryLocationsPost)step.Options;
            step.Values[DialogKey] = _inventorylocationsService.GetPost(((InventoryLocationsPost)step.Options).ixInventoryLocation);
            _inventorylocationsPost = _inventorylocationsService.GetPost(((InventoryLocationsPost)step.Options).ixInventoryLocation);
            step.Values[SelectedRecordKey] = _inventorylocationsPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("InventoryLocations");

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
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "LocationFunction":
					returnResult = await step.PromptAsync(
						LocationFunctionPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a LocationFunction:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectLocationFunctions().Select(ct => ct.sLocationFunction).ToList()),
						},
						cancellationToken);
                    break;
                case "Company":
					returnResult = await step.PromptAsync(
						CompanyPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Company:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectCompanies().Select(ct => ct.sCompany).ToList()),
						},
						cancellationToken);
                    break;
                case "FacilityFloor":
					returnResult = await step.PromptAsync(
						FacilityFloorPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a FacilityFloor:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectFacilityFloors().Select(ct => ct.sFacilityFloor).ToList()),
						},
						cancellationToken);
                    break;
                case "FacilityZone":
					returnResult = await step.PromptAsync(
						FacilityZonePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a FacilityZone:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectFacilityZones().Select(ct => ct.sFacilityZone).ToList()),
						},
						cancellationToken);
                    break;
                case "FacilityWorkArea":
					returnResult = await step.PromptAsync(
						FacilityWorkAreaPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a FacilityWorkArea:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectFacilityWorkAreas().Select(ct => ct.sFacilityWorkArea).ToList()),
						},
						cancellationToken);
                    break;
                case "FacilityAisleFace":
					returnResult = await step.PromptAsync(
						FacilityAisleFacePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a FacilityAisleFace:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectFacilityAisleFaces().Select(ct => ct.sFacilityAisleFace).ToList()),
						},
						cancellationToken);
                    break;
                case "Level":
					returnResult = await step.PromptAsync(
						LevelPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Level:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "Bay":
					returnResult = await step.PromptAsync(
						BayPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Bay:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "Slot":
					returnResult = await step.PromptAsync(
						SlotPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Slot:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "InventoryLocationSize":
					returnResult = await step.PromptAsync(
						InventoryLocationSizePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a InventoryLocationSize:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectInventoryLocationSizes().Select(ct => ct.sInventoryLocationSize).ToList()),
						},
						cancellationToken);
                    break;
                case "Sequence":
					returnResult = await step.PromptAsync(
						SequencePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Sequence:"),
							RetryPrompt = MessageFactory.Text("Please enter an integer."),
						},
						cancellationToken);
                    break;
                case "XOffset":
					returnResult = await step.PromptAsync(
						XOffsetPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a XOffset:"),
							RetryPrompt = MessageFactory.Text("Please enter a number."),
						},
						cancellationToken);
                    break;
                case "XOffsetUnit":
					returnResult = await step.PromptAsync(
						XOffsetUnitPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a XOffsetUnit:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
						},
						cancellationToken);
                    break;
                case "YOffset":
					returnResult = await step.PromptAsync(
						YOffsetPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a YOffset:"),
							RetryPrompt = MessageFactory.Text("Please enter a number."),
						},
						cancellationToken);
                    break;
                case "YOffsetUnit":
					returnResult = await step.PromptAsync(
						YOffsetUnitPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a YOffsetUnit:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
						},
						cancellationToken);
                    break;
                case "ZOffset":
					returnResult = await step.PromptAsync(
						ZOffsetPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a ZOffset:"),
							RetryPrompt = MessageFactory.Text("Please enter a number."),
						},
						cancellationToken);
                    break;
                case "ZOffsetUnit":
					returnResult = await step.PromptAsync(
						ZOffsetUnitPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a ZOffsetUnit:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventorylocationsService.selectUnitsOfMeasurement().Select(ct => ct.sUnitOfMeasurement).ToList()),
						},
						cancellationToken);
                    break;
                case "Latitude":
					returnResult = await step.PromptAsync(
						LatitudePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Latitude:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "Longitude":
					returnResult = await step.PromptAsync(
						LongitudePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Longitude:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "TrackUtilisation":
					returnResult = await step.PromptAsync(
						TrackUtilisationPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a TrackUtilisation:"),
							RetryPrompt = MessageFactory.Text("Please choose a valid option."),
						},
						cancellationToken);
                    break;
                case "UtilisationPercent":
					returnResult = await step.PromptAsync(
						UtilisationPercentPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a UtilisationPercent:"),
							RetryPrompt = MessageFactory.Text("Please enter a number."),
						},
						cancellationToken);
                    break;
                case "QueuedUtilisationPercent":
					returnResult = await step.PromptAsync(
						QueuedUtilisationPercentPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a QueuedUtilisationPercent:"),
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
					var sInventoryLocation = (string)step.Result;
					((InventoryLocationsPost)step.Values[DialogKey]).sInventoryLocation = sInventoryLocation;
                    break;
                case "LocationFunction":
					FoundChoice _LocationFunction = (FoundChoice)step.Result;
					var ixLocationFunction = _inventorylocationsService.selectLocationFunctions().Where(ct => ct.sLocationFunction == _LocationFunction.Value).Select(ct => ct.ixLocationFunction).First();
					((InventoryLocationsPost)step.Values[DialogKey]).ixLocationFunction = ixLocationFunction;
                    break;
                case "Company":
					FoundChoice _Company = (FoundChoice)step.Result;
					var ixCompany = _inventorylocationsService.selectCompanies().Where(ct => ct.sCompany == _Company.Value).Select(ct => ct.ixCompany).First();
					((InventoryLocationsPost)step.Values[DialogKey]).ixCompany = ixCompany;
                    break;
                case "FacilityFloor":
					FoundChoice _FacilityFloor = (FoundChoice)step.Result;
					var ixFacilityFloor = _inventorylocationsService.selectFacilityFloors().Where(ct => ct.sFacilityFloor == _FacilityFloor.Value).Select(ct => ct.ixFacilityFloor).First();
					((InventoryLocationsPost)step.Values[DialogKey]).ixFacilityFloor = ixFacilityFloor;
                    break;
                case "FacilityZone":
					FoundChoice _FacilityZone = (FoundChoice)step.Result;
					var ixFacilityZone = _inventorylocationsService.selectFacilityZones().Where(ct => ct.sFacilityZone == _FacilityZone.Value).Select(ct => ct.ixFacilityZone).First();
					((InventoryLocationsPost)step.Values[DialogKey]).ixFacilityZone = ixFacilityZone;
                    break;
                case "FacilityWorkArea":
					FoundChoice _FacilityWorkArea = (FoundChoice)step.Result;
					var ixFacilityWorkArea = _inventorylocationsService.selectFacilityWorkAreas().Where(ct => ct.sFacilityWorkArea == _FacilityWorkArea.Value).Select(ct => ct.ixFacilityWorkArea).First();
					((InventoryLocationsPost)step.Values[DialogKey]).ixFacilityWorkArea = ixFacilityWorkArea;
                    break;
                case "FacilityAisleFace":
					FoundChoice _FacilityAisleFace = (FoundChoice)step.Result;
					var ixFacilityAisleFace = _inventorylocationsService.selectFacilityAisleFaces().Where(ct => ct.sFacilityAisleFace == _FacilityAisleFace.Value).Select(ct => ct.ixFacilityAisleFace).First();
					((InventoryLocationsPost)step.Values[DialogKey]).ixFacilityAisleFace = ixFacilityAisleFace;
                    break;
                case "Level":
					var sLevel = (string)step.Result;
					((InventoryLocationsPost)step.Values[DialogKey]).sLevel = sLevel;
                    break;
                case "Bay":
					var sBay = (string)step.Result;
					((InventoryLocationsPost)step.Values[DialogKey]).sBay = sBay;
                    break;
                case "Slot":
					var sSlot = (string)step.Result;
					((InventoryLocationsPost)step.Values[DialogKey]).sSlot = sSlot;
                    break;
                case "InventoryLocationSize":
					FoundChoice _InventoryLocationSize = (FoundChoice)step.Result;
					var ixInventoryLocationSize = _inventorylocationsService.selectInventoryLocationSizes().Where(ct => ct.sInventoryLocationSize == _InventoryLocationSize.Value).Select(ct => ct.ixInventoryLocationSize).First();
					((InventoryLocationsPost)step.Values[DialogKey]).ixInventoryLocationSize = ixInventoryLocationSize;
                    break;
                case "Sequence":
					var nSequence = (Int64)step.Result;
					((InventoryLocationsPost)step.Values[DialogKey]).nSequence = nSequence;
                    break;
                case "XOffset":
					var nXOffset = step.Result;
					((InventoryLocationsPost)step.Values[DialogKey]).nXOffset = Convert.ToDouble(nXOffset);
                    break;
                case "XOffsetUnit":
					FoundChoice _XOffsetUnit = (FoundChoice)step.Result;
					var ixXOffsetUnit = _inventorylocationsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _XOffsetUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((InventoryLocationsPost)step.Values[DialogKey]).ixXOffsetUnit = ixXOffsetUnit;
                    break;
                case "YOffset":
					var nYOffset = step.Result;
					((InventoryLocationsPost)step.Values[DialogKey]).nYOffset = Convert.ToDouble(nYOffset);
                    break;
                case "YOffsetUnit":
					FoundChoice _YOffsetUnit = (FoundChoice)step.Result;
					var ixYOffsetUnit = _inventorylocationsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _YOffsetUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((InventoryLocationsPost)step.Values[DialogKey]).ixYOffsetUnit = ixYOffsetUnit;
                    break;
                case "ZOffset":
					var nZOffset = step.Result;
					((InventoryLocationsPost)step.Values[DialogKey]).nZOffset = Convert.ToDouble(nZOffset);
                    break;
                case "ZOffsetUnit":
					FoundChoice _ZOffsetUnit = (FoundChoice)step.Result;
					var ixZOffsetUnit = _inventorylocationsService.selectUnitsOfMeasurement().Where(ct => ct.sUnitOfMeasurement == _ZOffsetUnit.Value).Select(ct => ct.ixUnitOfMeasurement).First();
					((InventoryLocationsPost)step.Values[DialogKey]).ixZOffsetUnit = ixZOffsetUnit;
                    break;
                case "Latitude":
					var sLatitude = (string)step.Result;
					((InventoryLocationsPost)step.Values[DialogKey]).sLatitude = sLatitude;
                    break;
                case "Longitude":
					var sLongitude = (string)step.Result;
					((InventoryLocationsPost)step.Values[DialogKey]).sLongitude = sLongitude;
                    break;
                case "TrackUtilisation":
					var bTrackUtilisation = (bool)step.Result;
					((InventoryLocationsPost)step.Values[DialogKey]).bTrackUtilisation = bTrackUtilisation;
                    break;
                case "UtilisationPercent":
					var nUtilisationPercent = step.Result;
					((InventoryLocationsPost)step.Values[DialogKey]).nUtilisationPercent = Convert.ToDouble(nUtilisationPercent);
                    break;
                case "QueuedUtilisationPercent":
					var nQueuedUtilisationPercent = step.Result;
					((InventoryLocationsPost)step.Values[DialogKey]).nQueuedUtilisationPercent = Convert.ToDouble(nQueuedUtilisationPercent);
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (InventoryLocationsPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


