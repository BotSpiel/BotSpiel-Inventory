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
    public class EditReceivingDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditReceivingDialogId = "editReceivingDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string InventoryLocationPromptId = "inventorylocationPrompt";
        private const string InboundOrderPromptId = "inboundorderPrompt";
        private const string HandlingUnitPromptId = "handlingunitPrompt";
        private const string MaterialPromptId = "materialPrompt";
        private const string MaterialHandlingUnitConfigurationPromptId = "materialhandlingunitconfigurationPrompt";
        private const string HandlingUnitTypePromptId = "handlingunittypePrompt";
        private const string HandlingUnitQuantityPromptId = "handlingunitquantityPrompt";
        private const string BatchNumberPromptId = "batchnumberPrompt";
        private const string SerialNumberPromptId = "serialnumberPrompt";
        private const string BaseUnitQuantityReceivedPromptId = "baseunitquantityreceivedPrompt";
        private const string StatusPromptId = "statusPrompt";

        private const string DialogKey = nameof(EditReceivingDialog);
        private const string DialogKeyOptions = "editReceivingDialogOptions";
        private const string SearchColumnsKey = "EditReceivingDialogSearchColumns";
        private const string SearchTextKey = "EditReceivingDialogSearchText";
        private const string EditColumnsKey = "EditReceivingDialogEditColumns";
        private const string EditTextKey = "EditReceivingDialogEditText";
        private const string SelectedRecordKey = "EditReceivingDialogSelectedRecordKey";

        private readonly IReceivingService _receivingService;
        ReceivingPost _receivingPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit receiving" };
        string[] edit = { "Edit receiving" };
        string[] details = { "Display receiving" };
        string[] delete = { "Delete receiving" };

        public EditReceivingDialog(string id, IReceivingService receivingService, ReceivingPost receivingPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _receivingService = receivingService;
            _receivingPost = receivingPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> receiptValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_receivingService.VerifyReceiptUnique(_receivingPost.ixReceipt, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The receipt {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new ChoicePrompt(InventoryLocationPromptId));
            AddDialog(new ChoicePrompt(InboundOrderPromptId));
            AddDialog(new ChoicePrompt(HandlingUnitPromptId));
            AddDialog(new ChoicePrompt(MaterialPromptId));
            AddDialog(new ChoicePrompt(MaterialHandlingUnitConfigurationPromptId));
            AddDialog(new ChoicePrompt(HandlingUnitTypePromptId));
            AddDialog(new NumberPrompt<float>(HandlingUnitQuantityPromptId));
            AddDialog(new TextPrompt(BatchNumberPromptId));
            AddDialog(new TextPrompt(SerialNumberPromptId));
            AddDialog(new NumberPrompt<float>(BaseUnitQuantityReceivedPromptId));
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

            step.Values[DialogKey] = new ReceivingPost();
            step.Values[DialogKeyOptions] = (ReceivingPost)step.Options;
            step.Values[DialogKey] = _receivingService.GetPost(((ReceivingPost)step.Options).ixReceipt);
            _receivingPost = _receivingService.GetPost(((ReceivingPost)step.Options).ixReceipt);
            step.Values[SelectedRecordKey] = _receivingPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("Receiving");

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
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_receivingService.selectInventoryLocations().Select(ct => ct.sInventoryLocation).ToList()),
						},
						cancellationToken);
                    break;
                case "InboundOrder":
					returnResult = await step.PromptAsync(
						InboundOrderPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a InboundOrder:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_receivingService.selectInboundOrders().Select(ct => ct.sInboundOrder).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_receivingService.selectHandlingUnits().Select(ct => ct.sHandlingUnit).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_receivingService.selectMaterials().Select(ct => ct.sMaterial).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_receivingService.selectMaterialHandlingUnitConfigurations().Select(ct => ct.sMaterialHandlingUnitConfiguration).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_receivingService.selectHandlingUnitTypes().Select(ct => ct.sHandlingUnitType).ToList()),
						},
						cancellationToken);
                    break;
                case "HandlingUnitQuantity":
					returnResult = await step.PromptAsync(
						HandlingUnitQuantityPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a HandlingUnitQuantity:"),
							RetryPrompt = MessageFactory.Text("Please enter a number."),
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
                case "BaseUnitQuantityReceived":
					returnResult = await step.PromptAsync(
						BaseUnitQuantityReceivedPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a BaseUnitQuantityReceived:"),
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
							Choices = ChoiceFactory.ToChoices(_receivingService.selectStatuses().Select(ct => ct.sStatus).ToList()),
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
					FoundChoice _InventoryLocation = (FoundChoice)step.Result;
					var ixInventoryLocation = _receivingService.selectInventoryLocations().Where(ct => ct.sInventoryLocation == _InventoryLocation.Value).Select(ct => ct.ixInventoryLocation).First();
					((ReceivingPost)step.Values[DialogKey]).ixInventoryLocation = ixInventoryLocation;
                    break;
                case "InboundOrder":
					FoundChoice _InboundOrder = (FoundChoice)step.Result;
					var ixInboundOrder = _receivingService.selectInboundOrders().Where(ct => ct.sInboundOrder == _InboundOrder.Value).Select(ct => ct.ixInboundOrder).First();
					((ReceivingPost)step.Values[DialogKey]).ixInboundOrder = ixInboundOrder;
                    break;
                case "HandlingUnit":
					FoundChoice _HandlingUnit = (FoundChoice)step.Result;
					var ixHandlingUnit = _receivingService.selectHandlingUnits().Where(ct => ct.sHandlingUnit == _HandlingUnit.Value).Select(ct => ct.ixHandlingUnit).First();
					((ReceivingPost)step.Values[DialogKey]).ixHandlingUnit = ixHandlingUnit;
                    break;
                case "Material":
					FoundChoice _Material = (FoundChoice)step.Result;
					var ixMaterial = _receivingService.selectMaterials().Where(ct => ct.sMaterial == _Material.Value).Select(ct => ct.ixMaterial).First();
					((ReceivingPost)step.Values[DialogKey]).ixMaterial = ixMaterial;
                    break;
                case "MaterialHandlingUnitConfiguration":
					FoundChoice _MaterialHandlingUnitConfiguration = (FoundChoice)step.Result;
					var ixMaterialHandlingUnitConfiguration = _receivingService.selectMaterialHandlingUnitConfigurations().Where(ct => ct.sMaterialHandlingUnitConfiguration == _MaterialHandlingUnitConfiguration.Value).Select(ct => ct.ixMaterialHandlingUnitConfiguration).First();
					((ReceivingPost)step.Values[DialogKey]).ixMaterialHandlingUnitConfiguration = ixMaterialHandlingUnitConfiguration;
                    break;
                case "HandlingUnitType":
					FoundChoice _HandlingUnitType = (FoundChoice)step.Result;
					var ixHandlingUnitType = _receivingService.selectHandlingUnitTypes().Where(ct => ct.sHandlingUnitType == _HandlingUnitType.Value).Select(ct => ct.ixHandlingUnitType).First();
					((ReceivingPost)step.Values[DialogKey]).ixHandlingUnitType = ixHandlingUnitType;
                    break;
                case "HandlingUnitQuantity":
					var nHandlingUnitQuantity = step.Result;
					((ReceivingPost)step.Values[DialogKey]).nHandlingUnitQuantity = Convert.ToDouble(nHandlingUnitQuantity);
                    break;
                case "BatchNumber":
					var sBatchNumber = (string)step.Result;
					((ReceivingPost)step.Values[DialogKey]).sBatchNumber = sBatchNumber;
                    break;
                case "SerialNumber":
					var sSerialNumber = (string)step.Result;
					((ReceivingPost)step.Values[DialogKey]).sSerialNumber = sSerialNumber;
                    break;
                case "BaseUnitQuantityReceived":
					var nBaseUnitQuantityReceived = step.Result;
					((ReceivingPost)step.Values[DialogKey]).nBaseUnitQuantityReceived = Convert.ToDouble(nBaseUnitQuantityReceived);
                    break;
                case "Status":
					FoundChoice _Status = (FoundChoice)step.Result;
					var ixStatus = _receivingService.selectStatuses().Where(ct => ct.sStatus == _Status.Value).Select(ct => ct.ixStatus).First();
					((ReceivingPost)step.Values[DialogKey]).ixStatus = ixStatus;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (ReceivingPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


