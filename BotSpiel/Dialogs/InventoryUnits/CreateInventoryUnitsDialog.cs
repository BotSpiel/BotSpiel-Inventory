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
    public class CreateInventoryUnitsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateInventoryUnitsDialogId = "createInventoryUnitsDialog";
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

        private const string DialogKey = nameof(CreateInventoryUnitsDialog);
        private const string DialogKeyOptions = "createInventoryUnitsDialogOptions";
        private const string SearchColumnsKey = "CreateInventoryUnitsDialogSearchColumns";
        private const string SearchTextKey = "CreateInventoryUnitsDialogSearchText";
        private const string EditColumnsKey = "CreateInventoryUnitsDialogEditColumns";
        private const string EditTextKey = "CreateInventoryUnitsDialogEditText";
        private const string SelectedRecordKey = "CreateInventoryUnitsDialogSelectedRecordKey";

        private readonly IInventoryUnitsService _inventoryunitsService;
        InventoryUnitsPost _inventoryunitsPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inventoryunits" };
        string[] edit = { "Edit inventoryunits" };
        string[] details = { "Display inventoryunits" };
        string[] delete = { "Delete inventoryunits" };

        public CreateInventoryUnitsDialog(string id, IInventoryUnitsService inventoryunitsService, InventoryUnitsPost inventoryunitsPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_inventoryunitsService.VerifyInventoryUnitUnique(0L, value))
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


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             FacilityPrompt,
              CompanyPrompt,
              MaterialPrompt,
              InventoryStatePrompt,
              HandlingUnitPrompt,
              InventoryLocationPrompt,
              BaseUnitQuantityPrompt,
              SerialNumberPrompt,
              BatchNumberPrompt,
              ExpireAtPrompt,
              StatusPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> FacilityPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new InventoryUnitsPost();

            return await step.PromptAsync(
                FacilityPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Facility:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventoryunitsService.selectFacilities().Select(ct => ct.sFacility).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CompanyPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Facility = (FoundChoice)step.Result;
            var ixFacility = _inventoryunitsService.selectFacilities().Where(ct => ct.sFacility == _Facility.Value).Select(ct => ct.ixFacility).First();
            ((InventoryUnitsPost)step.Values[DialogKey]).ixFacility = ixFacility;

            return await step.PromptAsync(
                CompanyPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Company:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventoryunitsService.selectCompanies().Select(ct => ct.sCompany).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> MaterialPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Company = (FoundChoice)step.Result;
            var ixCompany = _inventoryunitsService.selectCompanies().Where(ct => ct.sCompany == _Company.Value).Select(ct => ct.ixCompany).First();
            ((InventoryUnitsPost)step.Values[DialogKey]).ixCompany = ixCompany;

            return await step.PromptAsync(
                MaterialPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Material:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventoryunitsService.selectMaterials().Select(ct => ct.sMaterial).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> InventoryStatePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Material = (FoundChoice)step.Result;
            var ixMaterial = _inventoryunitsService.selectMaterials().Where(ct => ct.sMaterial == _Material.Value).Select(ct => ct.ixMaterial).First();
            ((InventoryUnitsPost)step.Values[DialogKey]).ixMaterial = ixMaterial;

            return await step.PromptAsync(
                InventoryStatePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a InventoryState:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventoryunitsService.selectInventoryStates().Select(ct => ct.sInventoryState).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> HandlingUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _InventoryState = (FoundChoice)step.Result;
            var ixInventoryState = _inventoryunitsService.selectInventoryStates().Where(ct => ct.sInventoryState == _InventoryState.Value).Select(ct => ct.ixInventoryState).First();
            ((InventoryUnitsPost)step.Values[DialogKey]).ixInventoryState = ixInventoryState;

            return await step.PromptAsync(
                HandlingUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a HandlingUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventoryunitsService.selectHandlingUnits().Select(ct => ct.sHandlingUnit).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> InventoryLocationPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _HandlingUnit = (FoundChoice)step.Result;
            var ixHandlingUnit = _inventoryunitsService.selectHandlingUnits().Where(ct => ct.sHandlingUnit == _HandlingUnit.Value).Select(ct => ct.ixHandlingUnit).First();
            ((InventoryUnitsPost)step.Values[DialogKey]).ixHandlingUnit = ixHandlingUnit;

            return await step.PromptAsync(
                InventoryLocationPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a InventoryLocation:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventoryunitsService.selectInventoryLocations().Select(ct => ct.sInventoryLocation).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> BaseUnitQuantityPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _InventoryLocation = (FoundChoice)step.Result;
            var ixInventoryLocation = _inventoryunitsService.selectInventoryLocations().Where(ct => ct.sInventoryLocation == _InventoryLocation.Value).Select(ct => ct.ixInventoryLocation).First();
            ((InventoryUnitsPost)step.Values[DialogKey]).ixInventoryLocation = ixInventoryLocation;

            return await step.PromptAsync(
                BaseUnitQuantityPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a BaseUnitQuantity:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> SerialNumberPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nBaseUnitQuantity = step.Result;
            ((InventoryUnitsPost)step.Values[DialogKey]).nBaseUnitQuantity = Convert.ToDouble(nBaseUnitQuantity);

            return await step.PromptAsync(
                SerialNumberPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a SerialNumber:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> BatchNumberPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sSerialNumber = (string)step.Result;
            ((InventoryUnitsPost)step.Values[DialogKey]).sSerialNumber = sSerialNumber;

            return await step.PromptAsync(
                BatchNumberPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a BatchNumber:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> ExpireAtPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sBatchNumber = (string)step.Result;
            ((InventoryUnitsPost)step.Values[DialogKey]).sBatchNumber = sBatchNumber;

            return await step.PromptAsync(
                ExpireAtPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a ExpireAt:"),
                    RetryPrompt = MessageFactory.Text("Please a date and/or time."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StatusPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var dtExpireAt = ((IList<DateTimeResolution>)step.Result).First();
            ((InventoryUnitsPost)step.Values[DialogKey]).dtExpireAt = DateTime.Parse(dtExpireAt.Value);

            return await step.PromptAsync(
                StatusPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Status:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inventoryunitsService.selectStatuses().Select(ct => ct.sStatus).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixStatus = (Int64)step.Result;
            ((InventoryUnitsPost)step.Values[DialogKey]).ixStatus = ixStatus;


            return await step.EndDialogAsync(
                (InventoryUnitsPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


