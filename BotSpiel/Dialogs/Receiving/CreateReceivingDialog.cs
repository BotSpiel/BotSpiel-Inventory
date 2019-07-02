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
    public class CreateReceivingDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateReceivingDialogId = "createReceivingDialog";
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

        private const string DialogKey = nameof(CreateReceivingDialog);
        private const string DialogKeyOptions = "createReceivingDialogOptions";
        private const string SearchColumnsKey = "CreateReceivingDialogSearchColumns";
        private const string SearchTextKey = "CreateReceivingDialogSearchText";
        private const string EditColumnsKey = "CreateReceivingDialogEditColumns";
        private const string EditTextKey = "CreateReceivingDialogEditText";
        private const string SelectedRecordKey = "CreateReceivingDialogSelectedRecordKey";

        private readonly IReceivingService _receivingService;
        ReceivingPost _receivingPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit receiving" };
        string[] edit = { "Edit receiving" };
        string[] details = { "Display receiving" };
        string[] delete = { "Delete receiving" };

        public CreateReceivingDialog(string id, IReceivingService receivingService, ReceivingPost receivingPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_receivingService.VerifyReceiptUnique(0L, value))
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


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             InventoryLocationPrompt,
              InboundOrderPrompt,
              HandlingUnitPrompt,
              MaterialPrompt,
              MaterialHandlingUnitConfigurationPrompt,
              HandlingUnitTypePrompt,
              HandlingUnitQuantityPrompt,
              BatchNumberPrompt,
              SerialNumberPrompt,
              BaseUnitQuantityReceivedPrompt,
              StatusPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> InventoryLocationPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new ReceivingPost();

            return await step.PromptAsync(
                InventoryLocationPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a InventoryLocation:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_receivingService.selectInventoryLocations().Select(ct => ct.sInventoryLocation).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> InboundOrderPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _InventoryLocation = (FoundChoice)step.Result;
            var ixInventoryLocation = _receivingService.selectInventoryLocations().Where(ct => ct.sInventoryLocation == _InventoryLocation.Value).Select(ct => ct.ixInventoryLocation).First();
            ((ReceivingPost)step.Values[DialogKey]).ixInventoryLocation = ixInventoryLocation;

            return await step.PromptAsync(
                InboundOrderPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a InboundOrder:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_receivingService.selectInboundOrders().Select(ct => ct.sInboundOrder).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> HandlingUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _InboundOrder = (FoundChoice)step.Result;
            var ixInboundOrder = _receivingService.selectInboundOrders().Where(ct => ct.sInboundOrder == _InboundOrder.Value).Select(ct => ct.ixInboundOrder).First();
            ((ReceivingPost)step.Values[DialogKey]).ixInboundOrder = ixInboundOrder;

            return await step.PromptAsync(
                HandlingUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a HandlingUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_receivingService.selectHandlingUnits().Select(ct => ct.sHandlingUnit).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> MaterialPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _HandlingUnit = (FoundChoice)step.Result;
            var ixHandlingUnit = _receivingService.selectHandlingUnits().Where(ct => ct.sHandlingUnit == _HandlingUnit.Value).Select(ct => ct.ixHandlingUnit).First();
            ((ReceivingPost)step.Values[DialogKey]).ixHandlingUnit = ixHandlingUnit;

            return await step.PromptAsync(
                MaterialPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Material:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_receivingService.selectMaterials().Select(ct => ct.sMaterial).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> MaterialHandlingUnitConfigurationPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Material = (FoundChoice)step.Result;
            var ixMaterial = _receivingService.selectMaterials().Where(ct => ct.sMaterial == _Material.Value).Select(ct => ct.ixMaterial).First();
            ((ReceivingPost)step.Values[DialogKey]).ixMaterial = ixMaterial;

            return await step.PromptAsync(
                MaterialHandlingUnitConfigurationPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a MaterialHandlingUnitConfiguration:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_receivingService.selectMaterialHandlingUnitConfigurations().Select(ct => ct.sMaterialHandlingUnitConfiguration).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> HandlingUnitTypePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _MaterialHandlingUnitConfiguration = (FoundChoice)step.Result;
            var ixMaterialHandlingUnitConfiguration = _receivingService.selectMaterialHandlingUnitConfigurations().Where(ct => ct.sMaterialHandlingUnitConfiguration == _MaterialHandlingUnitConfiguration.Value).Select(ct => ct.ixMaterialHandlingUnitConfiguration).First();
            ((ReceivingPost)step.Values[DialogKey]).ixMaterialHandlingUnitConfiguration = ixMaterialHandlingUnitConfiguration;

            return await step.PromptAsync(
                HandlingUnitTypePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a HandlingUnitType:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_receivingService.selectHandlingUnitTypes().Select(ct => ct.sHandlingUnitType).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> HandlingUnitQuantityPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _HandlingUnitType = (FoundChoice)step.Result;
            var ixHandlingUnitType = _receivingService.selectHandlingUnitTypes().Where(ct => ct.sHandlingUnitType == _HandlingUnitType.Value).Select(ct => ct.ixHandlingUnitType).First();
            ((ReceivingPost)step.Values[DialogKey]).ixHandlingUnitType = ixHandlingUnitType;

            return await step.PromptAsync(
                HandlingUnitQuantityPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a HandlingUnitQuantity:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> BatchNumberPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nHandlingUnitQuantity = step.Result;
            ((ReceivingPost)step.Values[DialogKey]).nHandlingUnitQuantity = Convert.ToDouble(nHandlingUnitQuantity);

            return await step.PromptAsync(
                BatchNumberPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a BatchNumber:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> SerialNumberPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sBatchNumber = (string)step.Result;
            ((ReceivingPost)step.Values[DialogKey]).sBatchNumber = sBatchNumber;

            return await step.PromptAsync(
                SerialNumberPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a SerialNumber:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> BaseUnitQuantityReceivedPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sSerialNumber = (string)step.Result;
            ((ReceivingPost)step.Values[DialogKey]).sSerialNumber = sSerialNumber;

            return await step.PromptAsync(
                BaseUnitQuantityReceivedPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a BaseUnitQuantityReceived:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StatusPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nBaseUnitQuantityReceived = step.Result;
            ((ReceivingPost)step.Values[DialogKey]).nBaseUnitQuantityReceived = Convert.ToDouble(nBaseUnitQuantityReceived);

            return await step.PromptAsync(
                StatusPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Status:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_receivingService.selectStatuses().Select(ct => ct.sStatus).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixStatus = (Int64)step.Result;
            ((ReceivingPost)step.Values[DialogKey]).ixStatus = ixStatus;


            return await step.EndDialogAsync(
                (ReceivingPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


