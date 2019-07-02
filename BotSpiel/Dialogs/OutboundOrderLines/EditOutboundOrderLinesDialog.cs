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
    public class EditOutboundOrderLinesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditOutboundOrderLinesDialogId = "editOutboundOrderLinesDialog";

        private const string ChoicePromptId = "choicePrompt";
       private const string OrderLineReferencePromptId = "orderlinereferencePrompt";
        private const string MaterialPromptId = "materialPrompt";
        private const string BatchNumberPromptId = "batchnumberPrompt";
        private const string SerialNumberPromptId = "serialnumberPrompt";
        private const string BaseUnitQuantityOrderedPromptId = "baseunitquantityorderedPrompt";
        private const string BaseUnitQuantityShippedPromptId = "baseunitquantityshippedPrompt";
        private const string StatusPromptId = "statusPrompt";

        private const string DialogKey = nameof(EditOutboundOrderLinesDialog);
        private const string DialogKeyOptions = "editOutboundOrderLinesDialogOptions";
        private const string SearchColumnsKey = "EditOutboundOrderLinesDialogSearchColumns";
        private const string SearchTextKey = "EditOutboundOrderLinesDialogSearchText";
        private const string EditColumnsKey = "EditOutboundOrderLinesDialogEditColumns";
        private const string EditTextKey = "EditOutboundOrderLinesDialogEditText";
        private const string SelectedRecordKey = "EditOutboundOrderLinesDialogSelectedRecordKey";

        private readonly IOutboundOrderLinesService _outboundorderlinesService;
        OutboundOrderLinesPost _outboundorderlinesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundorderlines" };
        string[] edit = { "Edit outboundorderlines" };
        string[] details = { "Display outboundorderlines" };
        string[] delete = { "Delete outboundorderlines" };

        public EditOutboundOrderLinesDialog(string id, IOutboundOrderLinesService outboundorderlinesService, OutboundOrderLinesPost outboundorderlinesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _outboundorderlinesService = outboundorderlinesService;
            _outboundorderlinesPost = outboundorderlinesPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> outboundorderlineValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_outboundorderlinesService.VerifyOutboundOrderLineUnique(_outboundorderlinesPost.ixOutboundOrderLine, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The outboundorderline {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new TextPrompt(OrderLineReferencePromptId));
            AddDialog(new ChoicePrompt(MaterialPromptId));
            AddDialog(new TextPrompt(BatchNumberPromptId));
            AddDialog(new TextPrompt(SerialNumberPromptId));
            AddDialog(new NumberPrompt<float>(BaseUnitQuantityOrderedPromptId));
            AddDialog(new NumberPrompt<float>(BaseUnitQuantityShippedPromptId));
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

            step.Values[DialogKey] = new OutboundOrderLinesPost();
            step.Values[DialogKeyOptions] = (OutboundOrderLinesPost)step.Options;
            step.Values[DialogKey] = _outboundorderlinesService.GetPost(((OutboundOrderLinesPost)step.Options).ixOutboundOrderLine);
            _outboundorderlinesPost = _outboundorderlinesService.GetPost(((OutboundOrderLinesPost)step.Options).ixOutboundOrderLine);
            step.Values[SelectedRecordKey] = _outboundorderlinesPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("OutboundOrderLines");

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
							Choices = ChoiceFactory.ToChoices(_outboundorderlinesService.selectMaterials().Select(ct => ct.sMaterial).ToList()),
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
                case "BaseUnitQuantityOrdered":
					returnResult = await step.PromptAsync(
						BaseUnitQuantityOrderedPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a BaseUnitQuantityOrdered:"),
							RetryPrompt = MessageFactory.Text("Please enter a number."),
						},
						cancellationToken);
                    break;
                case "BaseUnitQuantityShipped":
					returnResult = await step.PromptAsync(
						BaseUnitQuantityShippedPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a BaseUnitQuantityShipped:"),
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
							Choices = ChoiceFactory.ToChoices(_outboundorderlinesService.selectStatuses().Select(ct => ct.sStatus).ToList()),
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
                case "OrderLineReference":
					var sOrderLineReference = (string)step.Result;
					((OutboundOrderLinesPost)step.Values[DialogKey]).sOrderLineReference = sOrderLineReference;
                    break;
                case "Material":
					FoundChoice _Material = (FoundChoice)step.Result;
					var ixMaterial = _outboundorderlinesService.selectMaterials().Where(ct => ct.sMaterial == _Material.Value).Select(ct => ct.ixMaterial).First();
					((OutboundOrderLinesPost)step.Values[DialogKey]).ixMaterial = ixMaterial;
                    break;
                case "BatchNumber":
					var sBatchNumber = (string)step.Result;
					((OutboundOrderLinesPost)step.Values[DialogKey]).sBatchNumber = sBatchNumber;
                    break;
                case "SerialNumber":
					var sSerialNumber = (string)step.Result;
					((OutboundOrderLinesPost)step.Values[DialogKey]).sSerialNumber = sSerialNumber;
                    break;
                case "BaseUnitQuantityOrdered":
					var nBaseUnitQuantityOrdered = step.Result;
					((OutboundOrderLinesPost)step.Values[DialogKey]).nBaseUnitQuantityOrdered = Convert.ToDouble(nBaseUnitQuantityOrdered);
                    break;
                case "BaseUnitQuantityShipped":
					var nBaseUnitQuantityShipped = step.Result;
					((OutboundOrderLinesPost)step.Values[DialogKey]).nBaseUnitQuantityShipped = Convert.ToDouble(nBaseUnitQuantityShipped);
                    break;
                case "Status":
					FoundChoice _Status = (FoundChoice)step.Result;
					var ixStatus = _outboundorderlinesService.selectStatuses().Where(ct => ct.sStatus == _Status.Value).Select(ct => ct.ixStatus).First();
					((OutboundOrderLinesPost)step.Values[DialogKey]).ixStatus = ixStatus;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (OutboundOrderLinesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


