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
    public class CreateInboundOrderLinesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateInboundOrderLinesDialogId = "createInboundOrderLinesDialog";
       private const string InboundOrderPromptId = "inboundorderPrompt";
        private const string OrderLineReferencePromptId = "orderlinereferencePrompt";
        private const string MaterialPromptId = "materialPrompt";
        private const string MaterialHandlingUnitConfigurationPromptId = "materialhandlingunitconfigurationPrompt";
        private const string HandlingUnitTypePromptId = "handlingunittypePrompt";
        private const string HandlingUnitQuantityPromptId = "handlingunitquantityPrompt";
        private const string BaseUnitQuantityExpectedPromptId = "baseunitquantityexpectedPrompt";
        private const string BaseUnitQuantityReceivedPromptId = "baseunitquantityreceivedPrompt";
        private const string BatchNumberPromptId = "batchnumberPrompt";
        private const string SerialNumberPromptId = "serialnumberPrompt";
        private const string StatusPromptId = "statusPrompt";

        private const string DialogKey = nameof(CreateInboundOrderLinesDialog);
        private const string DialogKeyOptions = "createInboundOrderLinesDialogOptions";
        private const string SearchColumnsKey = "CreateInboundOrderLinesDialogSearchColumns";
        private const string SearchTextKey = "CreateInboundOrderLinesDialogSearchText";
        private const string EditColumnsKey = "CreateInboundOrderLinesDialogEditColumns";
        private const string EditTextKey = "CreateInboundOrderLinesDialogEditText";
        private const string SelectedRecordKey = "CreateInboundOrderLinesDialogSelectedRecordKey";

        private readonly IInboundOrderLinesService _inboundorderlinesService;
        InboundOrderLinesPost _inboundorderlinesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inboundorderlines" };
        string[] edit = { "Edit inboundorderlines" };
        string[] details = { "Display inboundorderlines" };
        string[] delete = { "Delete inboundorderlines" };

        public CreateInboundOrderLinesDialog(string id, IInboundOrderLinesService inboundorderlinesService, InboundOrderLinesPost inboundorderlinesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _inboundorderlinesService = inboundorderlinesService;
            _inboundorderlinesPost = inboundorderlinesPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> inboundorderlineValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_inboundorderlinesService.VerifyInboundOrderLineUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The inboundorderline {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new ChoicePrompt(InboundOrderPromptId));
            AddDialog(new TextPrompt(OrderLineReferencePromptId));
            AddDialog(new ChoicePrompt(MaterialPromptId));
            AddDialog(new ChoicePrompt(MaterialHandlingUnitConfigurationPromptId));
            AddDialog(new ChoicePrompt(HandlingUnitTypePromptId));
            AddDialog(new NumberPrompt<float>(HandlingUnitQuantityPromptId));
            AddDialog(new NumberPrompt<float>(BaseUnitQuantityExpectedPromptId));
            AddDialog(new NumberPrompt<float>(BaseUnitQuantityReceivedPromptId));
            AddDialog(new TextPrompt(BatchNumberPromptId));
            AddDialog(new TextPrompt(SerialNumberPromptId));
            AddDialog(new ChoicePrompt(StatusPromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             InboundOrderPrompt,
              OrderLineReferencePrompt,
              MaterialPrompt,
              MaterialHandlingUnitConfigurationPrompt,
              HandlingUnitTypePrompt,
              HandlingUnitQuantityPrompt,
              BaseUnitQuantityExpectedPrompt,
              BaseUnitQuantityReceivedPrompt,
              BatchNumberPrompt,
              SerialNumberPrompt,
              StatusPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> InboundOrderPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new InboundOrderLinesPost();

            return await step.PromptAsync(
                InboundOrderPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a InboundOrder:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inboundorderlinesService.selectInboundOrders().Select(ct => ct.sInboundOrder).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> OrderLineReferencePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _InboundOrder = (FoundChoice)step.Result;
            var ixInboundOrder = _inboundorderlinesService.selectInboundOrders().Where(ct => ct.sInboundOrder == _InboundOrder.Value).Select(ct => ct.ixInboundOrder).First();
            ((InboundOrderLinesPost)step.Values[DialogKey]).ixInboundOrder = ixInboundOrder;

            return await step.PromptAsync(
                OrderLineReferencePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a OrderLineReference:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> MaterialPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sOrderLineReference = (string)step.Result;
            ((InboundOrderLinesPost)step.Values[DialogKey]).sOrderLineReference = sOrderLineReference;

            return await step.PromptAsync(
                MaterialPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Material:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inboundorderlinesService.selectMaterials().Select(ct => ct.sMaterial).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> MaterialHandlingUnitConfigurationPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Material = (FoundChoice)step.Result;
            var ixMaterial = _inboundorderlinesService.selectMaterials().Where(ct => ct.sMaterial == _Material.Value).Select(ct => ct.ixMaterial).First();
            ((InboundOrderLinesPost)step.Values[DialogKey]).ixMaterial = ixMaterial;

            return await step.PromptAsync(
                MaterialHandlingUnitConfigurationPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a MaterialHandlingUnitConfiguration:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inboundorderlinesService.selectMaterialHandlingUnitConfigurations().Select(ct => ct.sMaterialHandlingUnitConfiguration).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> HandlingUnitTypePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _MaterialHandlingUnitConfiguration = (FoundChoice)step.Result;
            var ixMaterialHandlingUnitConfiguration = _inboundorderlinesService.selectMaterialHandlingUnitConfigurations().Where(ct => ct.sMaterialHandlingUnitConfiguration == _MaterialHandlingUnitConfiguration.Value).Select(ct => ct.ixMaterialHandlingUnitConfiguration).First();
            ((InboundOrderLinesPost)step.Values[DialogKey]).ixMaterialHandlingUnitConfiguration = ixMaterialHandlingUnitConfiguration;

            return await step.PromptAsync(
                HandlingUnitTypePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a HandlingUnitType:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inboundorderlinesService.selectHandlingUnitTypes().Select(ct => ct.sHandlingUnitType).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> HandlingUnitQuantityPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _HandlingUnitType = (FoundChoice)step.Result;
            var ixHandlingUnitType = _inboundorderlinesService.selectHandlingUnitTypes().Where(ct => ct.sHandlingUnitType == _HandlingUnitType.Value).Select(ct => ct.ixHandlingUnitType).First();
            ((InboundOrderLinesPost)step.Values[DialogKey]).ixHandlingUnitType = ixHandlingUnitType;

            return await step.PromptAsync(
                HandlingUnitQuantityPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a HandlingUnitQuantity:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> BaseUnitQuantityExpectedPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nHandlingUnitQuantity = step.Result;
            ((InboundOrderLinesPost)step.Values[DialogKey]).nHandlingUnitQuantity = Convert.ToDouble(nHandlingUnitQuantity);

            return await step.PromptAsync(
                BaseUnitQuantityExpectedPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a BaseUnitQuantityExpected:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> BaseUnitQuantityReceivedPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nBaseUnitQuantityExpected = step.Result;
            ((InboundOrderLinesPost)step.Values[DialogKey]).nBaseUnitQuantityExpected = Convert.ToDouble(nBaseUnitQuantityExpected);

            return await step.PromptAsync(
                BaseUnitQuantityReceivedPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a BaseUnitQuantityReceived:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> BatchNumberPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nBaseUnitQuantityReceived = step.Result;
            ((InboundOrderLinesPost)step.Values[DialogKey]).nBaseUnitQuantityReceived = Convert.ToDouble(nBaseUnitQuantityReceived);

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
            ((InboundOrderLinesPost)step.Values[DialogKey]).sBatchNumber = sBatchNumber;

            return await step.PromptAsync(
                SerialNumberPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a SerialNumber:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StatusPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sSerialNumber = (string)step.Result;
            ((InboundOrderLinesPost)step.Values[DialogKey]).sSerialNumber = sSerialNumber;

            return await step.PromptAsync(
                StatusPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Status:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_inboundorderlinesService.selectStatuses().Select(ct => ct.sStatus).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixStatus = (Int64)step.Result;
            ((InboundOrderLinesPost)step.Values[DialogKey]).ixStatus = ixStatus;


            return await step.EndDialogAsync(
                (InboundOrderLinesPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


