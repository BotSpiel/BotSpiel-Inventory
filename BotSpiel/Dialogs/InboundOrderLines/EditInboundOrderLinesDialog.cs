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
    public class EditInboundOrderLinesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditInboundOrderLinesDialogId = "editInboundOrderLinesDialog";

        private const string ChoicePromptId = "choicePrompt";
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

        private const string DialogKey = nameof(EditInboundOrderLinesDialog);
        private const string DialogKeyOptions = "editInboundOrderLinesDialogOptions";
        private const string SearchColumnsKey = "EditInboundOrderLinesDialogSearchColumns";
        private const string SearchTextKey = "EditInboundOrderLinesDialogSearchText";
        private const string EditColumnsKey = "EditInboundOrderLinesDialogEditColumns";
        private const string EditTextKey = "EditInboundOrderLinesDialogEditText";
        private const string SelectedRecordKey = "EditInboundOrderLinesDialogSelectedRecordKey";

        private readonly IInboundOrderLinesService _inboundorderlinesService;
        InboundOrderLinesPost _inboundorderlinesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit inboundorderlines" };
        string[] edit = { "Edit inboundorderlines" };
        string[] details = { "Display inboundorderlines" };
        string[] delete = { "Delete inboundorderlines" };

        public EditInboundOrderLinesDialog(string id, IInboundOrderLinesService inboundorderlinesService, InboundOrderLinesPost inboundorderlinesPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_inboundorderlinesService.VerifyInboundOrderLineUnique(_inboundorderlinesPost.ixInboundOrderLine, value))
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

            step.Values[DialogKey] = new InboundOrderLinesPost();
            step.Values[DialogKeyOptions] = (InboundOrderLinesPost)step.Options;
            step.Values[DialogKey] = _inboundorderlinesService.GetPost(((InboundOrderLinesPost)step.Options).ixInboundOrderLine);
            _inboundorderlinesPost = _inboundorderlinesService.GetPost(((InboundOrderLinesPost)step.Options).ixInboundOrderLine);
            step.Values[SelectedRecordKey] = _inboundorderlinesPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("InboundOrderLines");

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
                case "InboundOrder":
					returnResult = await step.PromptAsync(
						InboundOrderPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a InboundOrder:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inboundorderlinesService.selectInboundOrders().Select(ct => ct.sInboundOrder).ToList()),
						},
						cancellationToken);
                    break;
                case "OrderLineReference":
					returnResult = await step.PromptAsync(
						OrderLineReferencePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a OrderLineReference:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
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
							Choices = ChoiceFactory.ToChoices(_inboundorderlinesService.selectMaterials().Select(ct => ct.sMaterial).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_inboundorderlinesService.selectMaterialHandlingUnitConfigurations().Select(ct => ct.sMaterialHandlingUnitConfiguration).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_inboundorderlinesService.selectHandlingUnitTypes().Select(ct => ct.sHandlingUnitType).ToList()),
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
                case "BaseUnitQuantityExpected":
					returnResult = await step.PromptAsync(
						BaseUnitQuantityExpectedPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a BaseUnitQuantityExpected:"),
							RetryPrompt = MessageFactory.Text("Please enter a number."),
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
                case "Status":
					returnResult = await step.PromptAsync(
						StatusPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a Status:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_inboundorderlinesService.selectStatuses().Select(ct => ct.sStatus).ToList()),
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
                case "InboundOrder":
					FoundChoice _InboundOrder = (FoundChoice)step.Result;
					var ixInboundOrder = _inboundorderlinesService.selectInboundOrders().Where(ct => ct.sInboundOrder == _InboundOrder.Value).Select(ct => ct.ixInboundOrder).First();
					((InboundOrderLinesPost)step.Values[DialogKey]).ixInboundOrder = ixInboundOrder;
                    break;
                case "OrderLineReference":
					var sOrderLineReference = (string)step.Result;
					((InboundOrderLinesPost)step.Values[DialogKey]).sOrderLineReference = sOrderLineReference;
                    break;
                case "Material":
					FoundChoice _Material = (FoundChoice)step.Result;
					var ixMaterial = _inboundorderlinesService.selectMaterials().Where(ct => ct.sMaterial == _Material.Value).Select(ct => ct.ixMaterial).First();
					((InboundOrderLinesPost)step.Values[DialogKey]).ixMaterial = ixMaterial;
                    break;
                case "MaterialHandlingUnitConfiguration":
					FoundChoice _MaterialHandlingUnitConfiguration = (FoundChoice)step.Result;
					var ixMaterialHandlingUnitConfiguration = _inboundorderlinesService.selectMaterialHandlingUnitConfigurations().Where(ct => ct.sMaterialHandlingUnitConfiguration == _MaterialHandlingUnitConfiguration.Value).Select(ct => ct.ixMaterialHandlingUnitConfiguration).First();
					((InboundOrderLinesPost)step.Values[DialogKey]).ixMaterialHandlingUnitConfiguration = ixMaterialHandlingUnitConfiguration;
                    break;
                case "HandlingUnitType":
					FoundChoice _HandlingUnitType = (FoundChoice)step.Result;
					var ixHandlingUnitType = _inboundorderlinesService.selectHandlingUnitTypes().Where(ct => ct.sHandlingUnitType == _HandlingUnitType.Value).Select(ct => ct.ixHandlingUnitType).First();
					((InboundOrderLinesPost)step.Values[DialogKey]).ixHandlingUnitType = ixHandlingUnitType;
                    break;
                case "HandlingUnitQuantity":
					var nHandlingUnitQuantity = step.Result;
					((InboundOrderLinesPost)step.Values[DialogKey]).nHandlingUnitQuantity = Convert.ToDouble(nHandlingUnitQuantity);
                    break;
                case "BaseUnitQuantityExpected":
					var nBaseUnitQuantityExpected = step.Result;
					((InboundOrderLinesPost)step.Values[DialogKey]).nBaseUnitQuantityExpected = Convert.ToDouble(nBaseUnitQuantityExpected);
                    break;
                case "BaseUnitQuantityReceived":
					var nBaseUnitQuantityReceived = step.Result;
					((InboundOrderLinesPost)step.Values[DialogKey]).nBaseUnitQuantityReceived = Convert.ToDouble(nBaseUnitQuantityReceived);
                    break;
                case "BatchNumber":
					var sBatchNumber = (string)step.Result;
					((InboundOrderLinesPost)step.Values[DialogKey]).sBatchNumber = sBatchNumber;
                    break;
                case "SerialNumber":
					var sSerialNumber = (string)step.Result;
					((InboundOrderLinesPost)step.Values[DialogKey]).sSerialNumber = sSerialNumber;
                    break;
                case "Status":
					FoundChoice _Status = (FoundChoice)step.Result;
					var ixStatus = _inboundorderlinesService.selectStatuses().Where(ct => ct.sStatus == _Status.Value).Select(ct => ct.ixStatus).First();
					((InboundOrderLinesPost)step.Values[DialogKey]).ixStatus = ixStatus;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (InboundOrderLinesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


