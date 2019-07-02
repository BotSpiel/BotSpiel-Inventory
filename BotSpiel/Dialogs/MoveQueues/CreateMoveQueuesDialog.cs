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
    public class CreateMoveQueuesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateMoveQueuesDialogId = "createMoveQueuesDialog";
       private const string MoveQueueTypePromptId = "movequeuetypePrompt";
        private const string MoveQueueContextPromptId = "movequeuecontextPrompt";
        private const string SourceInventoryUnitPromptId = "sourceinventoryunitPrompt";
        private const string TargetInventoryUnitPromptId = "targetinventoryunitPrompt";
        private const string SourceInventoryLocationPromptId = "sourceinventorylocationPrompt";
        private const string TargetInventoryLocationPromptId = "targetinventorylocationPrompt";
        private const string SourceHandlingUnitPromptId = "sourcehandlingunitPrompt";
        private const string TargetHandlingUnitPromptId = "targethandlingunitPrompt";
        private const string PreferredResourcePromptId = "preferredresourcePrompt";
        private const string BaseUnitQuantityPromptId = "baseunitquantityPrompt";
        private const string StartByPromptId = "startbyPrompt";
        private const string CompleteByPromptId = "completebyPrompt";
        private const string StartedAtPromptId = "startedatPrompt";
        private const string CompletedAtPromptId = "completedatPrompt";
        private const string InboundOrderLinePromptId = "inboundorderlinePrompt";
        private const string OutboundOrderLinePromptId = "outboundorderlinePrompt";
        private const string PickBatchPromptId = "pickbatchPrompt";
        private const string StatusPromptId = "statusPrompt";

        private const string DialogKey = nameof(CreateMoveQueuesDialog);
        private const string DialogKeyOptions = "createMoveQueuesDialogOptions";
        private const string SearchColumnsKey = "CreateMoveQueuesDialogSearchColumns";
        private const string SearchTextKey = "CreateMoveQueuesDialogSearchText";
        private const string EditColumnsKey = "CreateMoveQueuesDialogEditColumns";
        private const string EditTextKey = "CreateMoveQueuesDialogEditText";
        private const string SelectedRecordKey = "CreateMoveQueuesDialogSelectedRecordKey";

        private readonly IMoveQueuesService _movequeuesService;
        MoveQueuesPost _movequeuesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit movequeues" };
        string[] edit = { "Edit movequeues" };
        string[] details = { "Display movequeues" };
        string[] delete = { "Delete movequeues" };

        public CreateMoveQueuesDialog(string id, IMoveQueuesService movequeuesService, MoveQueuesPost movequeuesPost, BotSpielUserStateAccessors statePropertyAccessor)
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _movequeuesService = movequeuesService;
            _movequeuesPost = movequeuesPost;

            // Define the prompts used in the Dialog.
            PromptValidator<string> movequeueValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                if (!_movequeuesService.VerifyMoveQueueUnique(0L, value))
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The movequeue {value} already exists. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

           AddDialog(new ChoicePrompt(MoveQueueTypePromptId));
            AddDialog(new ChoicePrompt(MoveQueueContextPromptId));
            AddDialog(new ChoicePrompt(SourceInventoryUnitPromptId));
            AddDialog(new ChoicePrompt(TargetInventoryUnitPromptId));
            AddDialog(new ChoicePrompt(SourceInventoryLocationPromptId));
            AddDialog(new ChoicePrompt(TargetInventoryLocationPromptId));
            AddDialog(new ChoicePrompt(SourceHandlingUnitPromptId));
            AddDialog(new ChoicePrompt(TargetHandlingUnitPromptId));
            AddDialog(new TextPrompt(PreferredResourcePromptId));
            AddDialog(new NumberPrompt<float>(BaseUnitQuantityPromptId));
            AddDialog(new DateTimePrompt(StartByPromptId));
            AddDialog(new DateTimePrompt(CompleteByPromptId));
            AddDialog(new DateTimePrompt(StartedAtPromptId));
            AddDialog(new DateTimePrompt(CompletedAtPromptId));
            AddDialog(new ChoicePrompt(InboundOrderLinePromptId));
            AddDialog(new ChoicePrompt(OutboundOrderLinePromptId));
            AddDialog(new ChoicePrompt(PickBatchPromptId));
            AddDialog(new ChoicePrompt(StatusPromptId));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             MoveQueueTypePrompt,
              MoveQueueContextPrompt,
              SourceInventoryUnitPrompt,
              TargetInventoryUnitPrompt,
              SourceInventoryLocationPrompt,
              TargetInventoryLocationPrompt,
              SourceHandlingUnitPrompt,
              TargetHandlingUnitPrompt,
              PreferredResourcePrompt,
              BaseUnitQuantityPrompt,
              StartByPrompt,
              CompleteByPrompt,
              StartedAtPrompt,
              CompletedAtPrompt,
              InboundOrderLinePrompt,
              OutboundOrderLinePrompt,
              PickBatchPrompt,
              StatusPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> MoveQueueTypePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new MoveQueuesPost();

            return await step.PromptAsync(
                MoveQueueTypePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a MoveQueueType:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_movequeuesService.selectMoveQueueTypes().Select(ct => ct.sMoveQueueType).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> MoveQueueContextPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _MoveQueueType = (FoundChoice)step.Result;
            var ixMoveQueueType = _movequeuesService.selectMoveQueueTypes().Where(ct => ct.sMoveQueueType == _MoveQueueType.Value).Select(ct => ct.ixMoveQueueType).First();
            ((MoveQueuesPost)step.Values[DialogKey]).ixMoveQueueType = ixMoveQueueType;

            return await step.PromptAsync(
                MoveQueueContextPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a MoveQueueContext:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_movequeuesService.selectMoveQueueContexts().Select(ct => ct.sMoveQueueContext).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> SourceInventoryUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _MoveQueueContext = (FoundChoice)step.Result;
            var ixMoveQueueContext = _movequeuesService.selectMoveQueueContexts().Where(ct => ct.sMoveQueueContext == _MoveQueueContext.Value).Select(ct => ct.ixMoveQueueContext).First();
            ((MoveQueuesPost)step.Values[DialogKey]).ixMoveQueueContext = ixMoveQueueContext;

            return await step.PromptAsync(
                SourceInventoryUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a SourceInventoryUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_movequeuesService.selectInventoryUnits().Select(ct => ct.sInventoryUnit).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> TargetInventoryUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _SourceInventoryUnit = (FoundChoice)step.Result;
            var ixSourceInventoryUnit = _movequeuesService.selectInventoryUnits().Where(ct => ct.sInventoryUnit == _SourceInventoryUnit.Value).Select(ct => ct.ixInventoryUnit).First();
            ((MoveQueuesPost)step.Values[DialogKey]).ixSourceInventoryUnit = ixSourceInventoryUnit;

            return await step.PromptAsync(
                TargetInventoryUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a TargetInventoryUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_movequeuesService.selectInventoryUnits().Select(ct => ct.sInventoryUnit).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> SourceInventoryLocationPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _TargetInventoryUnit = (FoundChoice)step.Result;
            var ixTargetInventoryUnit = _movequeuesService.selectInventoryUnits().Where(ct => ct.sInventoryUnit == _TargetInventoryUnit.Value).Select(ct => ct.ixInventoryUnit).First();
            ((MoveQueuesPost)step.Values[DialogKey]).ixTargetInventoryUnit = ixTargetInventoryUnit;

            return await step.PromptAsync(
                SourceInventoryLocationPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a SourceInventoryLocation:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_movequeuesService.selectInventoryLocations().Select(ct => ct.sInventoryLocation).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> TargetInventoryLocationPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _SourceInventoryLocation = (FoundChoice)step.Result;
            var ixSourceInventoryLocation = _movequeuesService.selectInventoryLocations().Where(ct => ct.sInventoryLocation == _SourceInventoryLocation.Value).Select(ct => ct.ixInventoryLocation).First();
            ((MoveQueuesPost)step.Values[DialogKey]).ixSourceInventoryLocation = ixSourceInventoryLocation;

            return await step.PromptAsync(
                TargetInventoryLocationPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a TargetInventoryLocation:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_movequeuesService.selectInventoryLocations().Select(ct => ct.sInventoryLocation).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> SourceHandlingUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _TargetInventoryLocation = (FoundChoice)step.Result;
            var ixTargetInventoryLocation = _movequeuesService.selectInventoryLocations().Where(ct => ct.sInventoryLocation == _TargetInventoryLocation.Value).Select(ct => ct.ixInventoryLocation).First();
            ((MoveQueuesPost)step.Values[DialogKey]).ixTargetInventoryLocation = ixTargetInventoryLocation;

            return await step.PromptAsync(
                SourceHandlingUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a SourceHandlingUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_movequeuesService.selectHandlingUnits().Select(ct => ct.sHandlingUnit).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> TargetHandlingUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _SourceHandlingUnit = (FoundChoice)step.Result;
            var ixSourceHandlingUnit = _movequeuesService.selectHandlingUnits().Where(ct => ct.sHandlingUnit == _SourceHandlingUnit.Value).Select(ct => ct.ixHandlingUnit).First();
            ((MoveQueuesPost)step.Values[DialogKey]).ixSourceHandlingUnit = ixSourceHandlingUnit;

            return await step.PromptAsync(
                TargetHandlingUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a TargetHandlingUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_movequeuesService.selectHandlingUnits().Select(ct => ct.sHandlingUnit).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> PreferredResourcePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _TargetHandlingUnit = (FoundChoice)step.Result;
            var ixTargetHandlingUnit = _movequeuesService.selectHandlingUnits().Where(ct => ct.sHandlingUnit == _TargetHandlingUnit.Value).Select(ct => ct.ixHandlingUnit).First();
            ((MoveQueuesPost)step.Values[DialogKey]).ixTargetHandlingUnit = ixTargetHandlingUnit;

            return await step.PromptAsync(
                PreferredResourcePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a PreferredResource:"),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> BaseUnitQuantityPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sPreferredResource = (string)step.Result;
            ((MoveQueuesPost)step.Values[DialogKey]).sPreferredResource = sPreferredResource;

            return await step.PromptAsync(
                BaseUnitQuantityPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a BaseUnitQuantity:"),
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StartByPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nBaseUnitQuantity = step.Result;
            ((MoveQueuesPost)step.Values[DialogKey]).nBaseUnitQuantity = Convert.ToDouble(nBaseUnitQuantity);

            return await step.PromptAsync(
                StartByPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a StartBy:"),
                    RetryPrompt = MessageFactory.Text("Please a date and/or time."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CompleteByPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var dtStartBy = ((IList<DateTimeResolution>)step.Result).First();
            ((MoveQueuesPost)step.Values[DialogKey]).dtStartBy = DateTime.Parse(dtStartBy.Value);

            return await step.PromptAsync(
                CompleteByPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a CompleteBy:"),
                    RetryPrompt = MessageFactory.Text("Please a date and/or time."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StartedAtPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var dtCompleteBy = ((IList<DateTimeResolution>)step.Result).First();
            ((MoveQueuesPost)step.Values[DialogKey]).dtCompleteBy = DateTime.Parse(dtCompleteBy.Value);

            return await step.PromptAsync(
                StartedAtPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a StartedAt:"),
                    RetryPrompt = MessageFactory.Text("Please a date and/or time."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> CompletedAtPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var dtStartedAt = ((IList<DateTimeResolution>)step.Result).First();
            ((MoveQueuesPost)step.Values[DialogKey]).dtStartedAt = DateTime.Parse(dtStartedAt.Value);

            return await step.PromptAsync(
                CompletedAtPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a CompletedAt:"),
                    RetryPrompt = MessageFactory.Text("Please a date and/or time."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> InboundOrderLinePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var dtCompletedAt = ((IList<DateTimeResolution>)step.Result).First();
            ((MoveQueuesPost)step.Values[DialogKey]).dtCompletedAt = DateTime.Parse(dtCompletedAt.Value);

            return await step.PromptAsync(
                InboundOrderLinePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a InboundOrderLine:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_movequeuesService.selectInboundOrderLines().Select(ct => ct.sInboundOrderLine).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> OutboundOrderLinePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _InboundOrderLine = (FoundChoice)step.Result;
            var ixInboundOrderLine = _movequeuesService.selectInboundOrderLines().Where(ct => ct.sInboundOrderLine == _InboundOrderLine.Value).Select(ct => ct.ixInboundOrderLine).First();
            ((MoveQueuesPost)step.Values[DialogKey]).ixInboundOrderLine = ixInboundOrderLine;

            return await step.PromptAsync(
                OutboundOrderLinePromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a OutboundOrderLine:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_movequeuesService.selectOutboundOrderLines().Select(ct => ct.sOutboundOrderLine).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> PickBatchPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _OutboundOrderLine = (FoundChoice)step.Result;
            var ixOutboundOrderLine = _movequeuesService.selectOutboundOrderLines().Where(ct => ct.sOutboundOrderLine == _OutboundOrderLine.Value).Select(ct => ct.ixOutboundOrderLine).First();
            ((MoveQueuesPost)step.Values[DialogKey]).ixOutboundOrderLine = ixOutboundOrderLine;

            return await step.PromptAsync(
                PickBatchPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a PickBatch:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_movequeuesService.selectPickBatches().Select(ct => ct.sPickBatch).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> StatusPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            FoundChoice _PickBatch = (FoundChoice)step.Result;
            var ixPickBatch = _movequeuesService.selectPickBatches().Where(ct => ct.sPickBatch == _PickBatch.Value).Select(ct => ct.ixPickBatch).First();
            ((MoveQueuesPost)step.Values[DialogKey]).ixPickBatch = ixPickBatch;

            return await step.PromptAsync(
                StatusPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a Status:"),
                    RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
                    Choices = ChoiceFactory.ToChoices(_movequeuesService.selectStatuses().Select(ct => ct.sStatus).ToList()),
                },
                cancellationToken);
        }

        private static async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var ixStatus = (Int64)step.Result;
            ((MoveQueuesPost)step.Values[DialogKey]).ixStatus = ixStatus;


            return await step.EndDialogAsync(
                (MoveQueuesPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


