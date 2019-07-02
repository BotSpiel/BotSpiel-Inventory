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
    public class EditInventoryUnitsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditInventoryUnitsDialogId = "editInventoryUnitsDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string FacilityPromptId = "facilityPrompt";
        private const string CompanyPromptId = "companyPrompt";
        private const string MaterialPromptId = "materialPrompt";
        private const string InventoryStatePromptId = "inventorystatePrompt";
        private const string HandlingUnitPromptId = "handlingunitPrompt";
        private const string InventoryLocationPromptId = "inventorylocationPrompt";
        private const string BaseUnitQuantityPromptId = "baseunitquantityPrompt";
        private const string SerialNumberPromptId = "serialnumberPrompt";
        private const string BatchNumberPromptId = "batchnumberPrompt";
        private const string ExpireAtPromptId = "expireatPrompt";
        private const string StatusPromptId = "statusPrompt";

        private const string DialogKey = nameof(EditInventoryUnitsDialog);
        private const string DialogKeyOptions = "editInventoryUnitsDialogOptions";
        private const string SearchColumnsKey = "EditInventoryUnitsDialogSearchColumns";
        private const string SearchTextKey = "EditInventoryUnitsDialogSearchText";
        private const string EditColumnsKey = "EditInventoryUnitsDialogEditColumns";
        private const string EditTextKey = "EditInventoryUnitsDialogEditText";
        private const string SelectedRecordKey = "EditInventoryUnitsDialogSelectedRecordKey";

        private readonly IInventoryUnitsService _inventoryunitsService;
        InventoryUnitsPost _inventoryunitsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inventoryunits" };
        string[] edit = { "Edit inventoryunits" };
        string[] details = { "Display inventoryunits" };
        string[] delete = { "Delete inventoryunits" };

        public EditInventoryUnitsDialog(string id, IInventoryUnitsService inventoryunitsService, InventoryUnitsPost inventoryunitsPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inventoryunitsService = inventoryunitsService;
            _inventoryunitsPost = inventoryunitsPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> inventoryunitValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_inventoryunitsService.VerifyInventoryUnitUnique(_inventoryunitsPost.ixInventoryUnit, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The inventoryunit {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new ChoicePrompt(FacilityPromptId));
            AddDialog(new ChoicePrompt(CompanyPromptId));
            AddDialog(new ChoicePrompt(MaterialPromptId));
            AddDialog(new ChoicePrompt(InventoryStatePromptId));
            AddDialog(new ChoicePrompt(HandlingUnitPromptId));
            AddDialog(new ChoicePrompt(InventoryLocationPromptId));
            AddDialog(new NumberPrompt<float>(BaseUnitQuantityPromptId));
            AddDialog(new TextPrompt(SerialNumberPromptId));
            AddDialog(new TextPrompt(BatchNumberPromptId));
            AddDialog(new DateTimePrompt(ExpireAtPromptId));
            AddDialog(new ChoicePrompt(StatusPromptId));

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

            step.Values[DialogKey] = new InventoryUnitsPost();
            step.Values[DialogKeyOptions] = (InventoryUnitsPost)step.Options;
            step.Values[DialogKey] = _inventoryunitsService.GetPost(((InventoryUnitsPost)step.Options).ixInventoryUnit);
            _inventoryunitsPost = _inventoryunitsService.GetPost(((InventoryUnitsPost)step.Options).ixInventoryUnit);
            step.Values[SelectedRecordKey] = _inventoryunitsPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("InventoryUnits");

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
                case "Facility":
					returnResult = await step.PromptAsync(
						FacilityPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Facility:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventoryunitsService.selectFacilities().Select(ct => ct.sFacility).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_inventoryunitsService.selectCompanies().Select(ct => ct.sCompany).ToList()),
						},
						cancellationToken);
                    break;
                case "Material":
					returnResult = await step.PromptAsync(
						MaterialPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Material:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventoryunitsService.selectMaterials().Select(ct => ct.sMaterial).ToList()),
						},
						cancellationToken);
                    break;
                case "InventoryState":
					returnResult = await step.PromptAsync(
						InventoryStatePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a InventoryState:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventoryunitsService.selectInventoryStates().Select(ct => ct.sInventoryState).ToList()),
						},
						cancellationToken);
                    break;
                case "HandlingUnit":
					returnResult = await step.PromptAsync(
						HandlingUnitPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a HandlingUnit:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventoryunitsService.selectHandlingUnits().Select(ct => ct.sHandlingUnit).ToList()),
						},
						cancellationToken);
                    break;
                case "InventoryLocation":
					returnResult = await step.PromptAsync(
						InventoryLocationPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a InventoryLocation:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inventoryunitsService.selectInventoryLocations().Select(ct => ct.sInventoryLocation).ToList()),
						},
						cancellationToken);
                    break;
                case "BaseUnitQuantity":
					returnResult = await step.PromptAsync(
						BaseUnitQuantityPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a BaseUnitQuantity:"),
							RetryPrompt = MessageFactory.Text("Please enter a number."),
						},
						cancellationToken);
                    break;
                case "SerialNumber":
					returnResult = await step.PromptAsync(
						SerialNumberPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a SerialNumber:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "BatchNumber":
					returnResult = await step.PromptAsync(
						BatchNumberPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a BatchNumber:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
						},
						cancellationToken);
							break;
                case "ExpireAt":
					returnResult = await step.PromptAsync(
						ExpireAtPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a ExpireAt:"),
							RetryPrompt = MessageFactory.Text("Please a date and/or time."),
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
							Choices = ChoiceFactory.ToChoices(_inventoryunitsService.selectStatuses().Select(ct => ct.sStatus).ToList()),
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
                case "Facility":
					FoundChoice _Facility = (FoundChoice)step.Result;
					var ixFacility = _inventoryunitsService.selectFacilities().Where(ct => ct.sFacility == _Facility.Value).Select(ct => ct.ixFacility).First();
					((InventoryUnitsPost)step.Values[DialogKey]).ixFacility = ixFacility;
                    break;
                case "Company":
					FoundChoice _Company = (FoundChoice)step.Result;
					var ixCompany = _inventoryunitsService.selectCompanies().Where(ct => ct.sCompany == _Company.Value).Select(ct => ct.ixCompany).First();
					((InventoryUnitsPost)step.Values[DialogKey]).ixCompany = ixCompany;
                    break;
                case "Material":
					FoundChoice _Material = (FoundChoice)step.Result;
					var ixMaterial = _inventoryunitsService.selectMaterials().Where(ct => ct.sMaterial == _Material.Value).Select(ct => ct.ixMaterial).First();
					((InventoryUnitsPost)step.Values[DialogKey]).ixMaterial = ixMaterial;
                    break;
                case "InventoryState":
					FoundChoice _InventoryState = (FoundChoice)step.Result;
					var ixInventoryState = _inventoryunitsService.selectInventoryStates().Where(ct => ct.sInventoryState == _InventoryState.Value).Select(ct => ct.ixInventoryState).First();
					((InventoryUnitsPost)step.Values[DialogKey]).ixInventoryState = ixInventoryState;
                    break;
                case "HandlingUnit":
					FoundChoice _HandlingUnit = (FoundChoice)step.Result;
					var ixHandlingUnit = _inventoryunitsService.selectHandlingUnits().Where(ct => ct.sHandlingUnit == _HandlingUnit.Value).Select(ct => ct.ixHandlingUnit).First();
					((InventoryUnitsPost)step.Values[DialogKey]).ixHandlingUnit = ixHandlingUnit;
                    break;
                case "InventoryLocation":
					FoundChoice _InventoryLocation = (FoundChoice)step.Result;
					var ixInventoryLocation = _inventoryunitsService.selectInventoryLocations().Where(ct => ct.sInventoryLocation == _InventoryLocation.Value).Select(ct => ct.ixInventoryLocation).First();
					((InventoryUnitsPost)step.Values[DialogKey]).ixInventoryLocation = ixInventoryLocation;
                    break;
                case "BaseUnitQuantity":
					var nBaseUnitQuantity = step.Result;
					((InventoryUnitsPost)step.Values[DialogKey]).nBaseUnitQuantity = Convert.ToDouble(nBaseUnitQuantity);
                    break;
                case "SerialNumber":
					var sSerialNumber = (string)step.Result;
					((InventoryUnitsPost)step.Values[DialogKey]).sSerialNumber = sSerialNumber;
                    break;
                case "BatchNumber":
					var sBatchNumber = (string)step.Result;
					((InventoryUnitsPost)step.Values[DialogKey]).sBatchNumber = sBatchNumber;
                    break;
                case "ExpireAt":
					var dtExpireAt = ((IList<DateTimeResolution>)step.Result).First();
					((InventoryUnitsPost)step.Values[DialogKey]).dtExpireAt = DateTime.Parse(dtExpireAt.Value);
                    break;
                case "Status":
					FoundChoice _Status = (FoundChoice)step.Result;
					var ixStatus = _inventoryunitsService.selectStatuses().Where(ct => ct.sStatus == _Status.Value).Select(ct => ct.ixStatus).First();
					((InventoryUnitsPost)step.Values[DialogKey]).ixStatus = ixStatus;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (InventoryUnitsPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


