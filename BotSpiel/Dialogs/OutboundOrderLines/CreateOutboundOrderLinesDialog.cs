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
    public class CreateOutboundOrderLinesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateOutboundOrderLinesDialogId = "createOutboundOrderLinesDialog";
       private const string OrderLineReferencePromptId = "orderlinereferencePrompt";
        private const string MaterialPromptId = "materialPrompt";
        private const string BatchNumberPromptId = "batchnumberPrompt";
        private const string SerialNumberPromptId = "serialnumberPrompt";
        private const string BaseUnitQuantityOrderedPromptId = "baseunitquantityorderedPrompt";
        private const string BaseUnitQuantityShippedPromptId = "baseunitquantityshippedPrompt";
        private const string StatusPromptId = "statusPrompt";

        private const string DialogKey = nameof(CreateOutboundOrderLinesDialog);
        private const string DialogKeyOptions = "createOutboundOrderLinesDialogOptions";
        private const string SearchColumnsKey = "CreateOutboundOrderLinesDialogSearchColumns";
        private const string SearchTextKey = "CreateOutboundOrderLinesDialogSearchText";
        private const string EditColumnsKey = "CreateOutboundOrderLinesDialogEditColumns";
        private const string EditTextKey = "CreateOutboundOrderLinesDialogEditText";
        private const string SelectedRecordKey = "CreateOutboundOrderLinesDialogSelectedRecordKey";

        private readonly IOutboundOrderLinesService _outboundorderlinesService;
        OutboundOrderLinesPost _outboundorderlinesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit outboundorderlines" };
        string[] edit = { "Edit outboundorderlines" };
        string[] details = { "Display outboundorderlines" };
        string[] delete = { "Delete outboundorderlines" };

        public CreateOutboundOrderLinesDialog(string id, IOutboundOrderLinesService outboundorderlinesService, OutboundOrderLinesPost outboundorderlinesPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_outboundorderlinesService.VerifyOutboundOrderLineUnique(0L, value))
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


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             OrderLineReferencePrompt,
              MaterialPrompt,
              BatchNumberPrompt,
              SerialNumberPrompt,
              BaseUnitQuantityOrderedPrompt,
              BaseUnitQuantityShippedPrompt,
              StatusPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> OrderLineReferencePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new OutboundOrderLinesPost();

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
            ((OutboundOrderLinesPost)step.Values[DialogKey]).sOrderLineReference = sOrderLineReference;

            return await step.PromptAsync(
                MaterialPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Material:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundorderlinesService.selectMaterials().Select(ct => ct.sMaterial).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> BatchNumberPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _Material = (FoundChoice)step.Result;
            var ixMaterial = _outboundorderlinesService.selectMaterials().Where(ct => ct.sMaterial == _Material.Value).Select(ct => ct.ixMaterial).First();
            ((OutboundOrderLinesPost)step.Values[DialogKey]).ixMaterial = ixMaterial;

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
            ((OutboundOrderLinesPost)step.Values[DialogKey]).sBatchNumber = sBatchNumber;

            return await step.PromptAsync(
                SerialNumberPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a SerialNumber:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> BaseUnitQuantityOrderedPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sSerialNumber = (string)step.Result;
            ((OutboundOrderLinesPost)step.Values[DialogKey]).sSerialNumber = sSerialNumber;

            return await step.PromptAsync(
                BaseUnitQuantityOrderedPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a BaseUnitQuantityOrdered:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> BaseUnitQuantityShippedPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nBaseUnitQuantityOrdered = step.Result;
            ((OutboundOrderLinesPost)step.Values[DialogKey]).nBaseUnitQuantityOrdered = Convert.ToDouble(nBaseUnitQuantityOrdered);

            return await step.PromptAsync(
                BaseUnitQuantityShippedPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a BaseUnitQuantityShipped:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StatusPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nBaseUnitQuantityShipped = step.Result;
            ((OutboundOrderLinesPost)step.Values[DialogKey]).nBaseUnitQuantityShipped = Convert.ToDouble(nBaseUnitQuantityShipped);

            return await step.PromptAsync(
                StatusPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Status:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_outboundorderlinesService.selectStatuses().Select(ct => ct.sStatus).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixStatus = (Int64)step.Result;
            ((OutboundOrderLinesPost)step.Values[DialogKey]).ixStatus = ixStatus;


            return await step.EndDialogAsync(
                (OutboundOrderLinesPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


