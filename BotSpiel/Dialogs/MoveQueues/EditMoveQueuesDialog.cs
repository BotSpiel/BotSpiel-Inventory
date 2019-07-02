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
    public class EditMoveQueuesDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string EditMoveQueuesDialogId = "editMoveQueuesDialog";

        private const string ChoicePromptId = "choicePrompt";
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

        private const string DialogKey = nameof(EditMoveQueuesDialog);
        private const string DialogKeyOptions = "editMoveQueuesDialogOptions";
        private const string SearchColumnsKey = "EditMoveQueuesDialogSearchColumns";
        private const string SearchTextKey = "EditMoveQueuesDialogSearchText";
        private const string EditColumnsKey = "EditMoveQueuesDialogEditColumns";
        private const string EditTextKey = "EditMoveQueuesDialogEditText";
        private const string SelectedRecordKey = "EditMoveQueuesDialogSelectedRecordKey";

        private readonly IMoveQueuesService _movequeuesService;
        MoveQueuesPost _movequeuesPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit movequeues" };
        string[] edit = { "Edit movequeues" };
        string[] details = { "Display movequeues" };
        string[] delete = { "Delete movequeues" };

        public EditMoveQueuesDialog(string id, IMoveQueuesService movequeuesService, MoveQueuesPost movequeuesPost, BotSpielUserStateAccessors statePropertyAccessor)
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
                if (!_movequeuesService.VerifyMoveQueueUnique(_movequeuesPost.ixMoveQueue, value))
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

            step.Values[DialogKey] = new MoveQueuesPost();
            step.Values[DialogKeyOptions] = (MoveQueuesPost)step.Options;
            step.Values[DialogKey] = _movequeuesService.GetPost(((MoveQueuesPost)step.Options).ixMoveQueue);
            _movequeuesPost = _movequeuesService.GetPost(((MoveQueuesPost)step.Options).ixMoveQueue);
            step.Values[SelectedRecordKey] = _movequeuesPost;
            step.Values[EditColumnsKey] = editColumn;
            step.Values[EditTextKey] = editText;

            EntityColumnData _entityColumnData = new EntityColumnData();
            List<string> entitySearchColumns = _entityColumnData.ColumnsForEntity("MoveQueues");

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
                case "MoveQueueType":
					returnResult = await step.PromptAsync(
						MoveQueueTypePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a MoveQueueType:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_movequeuesService.selectMoveQueueTypes().Select(ct => ct.sMoveQueueType).ToList()),
						},
						cancellationToken);
                    break;
                case "MoveQueueContext":
					returnResult = await step.PromptAsync(
						MoveQueueContextPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a MoveQueueContext:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_movequeuesService.selectMoveQueueContexts().Select(ct => ct.sMoveQueueContext).ToList()),
						},
						cancellationToken);
                    break;
                case "SourceInventoryUnit":
					returnResult = await step.PromptAsync(
						SourceInventoryUnitPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a SourceInventoryUnit:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_movequeuesService.selectInventoryUnits().Select(ct => ct.sInventoryUnit).ToList()),
						},
						cancellationToken);
                    break;
                case "TargetInventoryUnit":
					returnResult = await step.PromptAsync(
						TargetInventoryUnitPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a TargetInventoryUnit:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_movequeuesService.selectInventoryUnits().Select(ct => ct.sInventoryUnit).ToList()),
						},
						cancellationToken);
                    break;
                case "SourceInventoryLocation":
					returnResult = await step.PromptAsync(
						SourceInventoryLocationPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a SourceInventoryLocation:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_movequeuesService.selectInventoryLocations().Select(ct => ct.sInventoryLocation).ToList()),
						},
						cancellationToken);
                    break;
                case "TargetInventoryLocation":
					returnResult = await step.PromptAsync(
						TargetInventoryLocationPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a TargetInventoryLocation:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_movequeuesService.selectInventoryLocations().Select(ct => ct.sInventoryLocation).ToList()),
						},
						cancellationToken);
                    break;
                case "SourceHandlingUnit":
					returnResult = await step.PromptAsync(
						SourceHandlingUnitPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a SourceHandlingUnit:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_movequeuesService.selectHandlingUnits().Select(ct => ct.sHandlingUnit).ToList()),
						},
						cancellationToken);
                    break;
                case "TargetHandlingUnit":
					returnResult = await step.PromptAsync(
						TargetHandlingUnitPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a TargetHandlingUnit:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_movequeuesService.selectHandlingUnits().Select(ct => ct.sHandlingUnit).ToList()),
						},
						cancellationToken);
                    break;
                case "PreferredResource":
					returnResult = await step.PromptAsync(
						PreferredResourcePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a PreferredResource:"),
							RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
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
                case "StartBy":
					returnResult = await step.PromptAsync(
						StartByPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a StartBy:"),
							RetryPrompt = MessageFactory.Text("Please a date and/or time."),
						},
						cancellationToken);
                    break;
                case "CompleteBy":
					returnResult = await step.PromptAsync(
						CompleteByPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a CompleteBy:"),
							RetryPrompt = MessageFactory.Text("Please a date and/or time."),
						},
						cancellationToken);
                    break;
                case "StartedAt":
					returnResult = await step.PromptAsync(
						StartedAtPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a StartedAt:"),
							RetryPrompt = MessageFactory.Text("Please a date and/or time."),
						},
						cancellationToken);
                    break;
                case "CompletedAt":
					returnResult = await step.PromptAsync(
						CompletedAtPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a CompletedAt:"),
							RetryPrompt = MessageFactory.Text("Please a date and/or time."),
						},
						cancellationToken);
                    break;
                case "InboundOrderLine":
					returnResult = await step.PromptAsync(
						InboundOrderLinePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a InboundOrderLine:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_movequeuesService.selectInboundOrderLines().Select(ct => ct.sInboundOrderLine).ToList()),
						},
						cancellationToken);
                    break;
                case "OutboundOrderLine":
					returnResult = await step.PromptAsync(
						OutboundOrderLinePromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a OutboundOrderLine:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_movequeuesService.selectOutboundOrderLines().Select(ct => ct.sOutboundOrderLine).ToList()),
						},
						cancellationToken);
                    break;
                case "PickBatch":
					returnResult = await step.PromptAsync(
						PickBatchPromptId,
						new PromptOptions
						{
							Prompt = MessageFactory.Text($"Please enter a PickBatch:"),
							RetryPrompt = MessageFactory.Text("Please choose and option from the list."),
							Choices = ChoiceFactory.ToChoices(_movequeuesService.selectPickBatches().Select(ct => ct.sPickBatch).ToList()),
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
							Choices = ChoiceFactory.ToChoices(_movequeuesService.selectStatuses().Select(ct => ct.sStatus).ToList()),
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
                case "MoveQueueType":
					FoundChoice _MoveQueueType = (FoundChoice)step.Result;
					var ixMoveQueueType = _movequeuesService.selectMoveQueueTypes().Where(ct => ct.sMoveQueueType == _MoveQueueType.Value).Select(ct => ct.ixMoveQueueType).First();
					((MoveQueuesPost)step.Values[DialogKey]).ixMoveQueueType = ixMoveQueueType;
                    break;
                case "MoveQueueContext":
					FoundChoice _MoveQueueContext = (FoundChoice)step.Result;
					var ixMoveQueueContext = _movequeuesService.selectMoveQueueContexts().Where(ct => ct.sMoveQueueContext == _MoveQueueContext.Value).Select(ct => ct.ixMoveQueueContext).First();
					((MoveQueuesPost)step.Values[DialogKey]).ixMoveQueueContext = ixMoveQueueContext;
                    break;
                case "SourceInventoryUnit":
					FoundChoice _SourceInventoryUnit = (FoundChoice)step.Result;
					var ixSourceInventoryUnit = _movequeuesService.selectInventoryUnits().Where(ct => ct.sInventoryUnit == _SourceInventoryUnit.Value).Select(ct => ct.ixInventoryUnit).First();
					((MoveQueuesPost)step.Values[DialogKey]).ixSourceInventoryUnit = ixSourceInventoryUnit;
                    break;
                case "TargetInventoryUnit":
					FoundChoice _TargetInventoryUnit = (FoundChoice)step.Result;
					var ixTargetInventoryUnit = _movequeuesService.selectInventoryUnits().Where(ct => ct.sInventoryUnit == _TargetInventoryUnit.Value).Select(ct => ct.ixInventoryUnit).First();
					((MoveQueuesPost)step.Values[DialogKey]).ixTargetInventoryUnit = ixTargetInventoryUnit;
                    break;
                case "SourceInventoryLocation":
					FoundChoice _SourceInventoryLocation = (FoundChoice)step.Result;
					var ixSourceInventoryLocation = _movequeuesService.selectInventoryLocations().Where(ct => ct.sInventoryLocation == _SourceInventoryLocation.Value).Select(ct => ct.ixInventoryLocation).First();
					((MoveQueuesPost)step.Values[DialogKey]).ixSourceInventoryLocation = ixSourceInventoryLocation;
                    break;
                case "TargetInventoryLocation":
					FoundChoice _TargetInventoryLocation = (FoundChoice)step.Result;
					var ixTargetInventoryLocation = _movequeuesService.selectInventoryLocations().Where(ct => ct.sInventoryLocation == _TargetInventoryLocation.Value).Select(ct => ct.ixInventoryLocation).First();
					((MoveQueuesPost)step.Values[DialogKey]).ixTargetInventoryLocation = ixTargetInventoryLocation;
                    break;
                case "SourceHandlingUnit":
					FoundChoice _SourceHandlingUnit = (FoundChoice)step.Result;
					var ixSourceHandlingUnit = _movequeuesService.selectHandlingUnits().Where(ct => ct.sHandlingUnit == _SourceHandlingUnit.Value).Select(ct => ct.ixHandlingUnit).First();
					((MoveQueuesPost)step.Values[DialogKey]).ixSourceHandlingUnit = ixSourceHandlingUnit;
                    break;
                case "TargetHandlingUnit":
					FoundChoice _TargetHandlingUnit = (FoundChoice)step.Result;
					var ixTargetHandlingUnit = _movequeuesService.selectHandlingUnits().Where(ct => ct.sHandlingUnit == _TargetHandlingUnit.Value).Select(ct => ct.ixHandlingUnit).First();
					((MoveQueuesPost)step.Values[DialogKey]).ixTargetHandlingUnit = ixTargetHandlingUnit;
                    break;
                case "PreferredResource":
					var sPreferredResource = (string)step.Result;
					((MoveQueuesPost)step.Values[DialogKey]).sPreferredResource = sPreferredResource;
                    break;
                case "BaseUnitQuantity":
					var nBaseUnitQuantity = step.Result;
					((MoveQueuesPost)step.Values[DialogKey]).nBaseUnitQuantity = Convert.ToDouble(nBaseUnitQuantity);
                    break;
                case "StartBy":
					var dtStartBy = ((IList<DateTimeResolution>)step.Result).First();
					((MoveQueuesPost)step.Values[DialogKey]).dtStartBy = DateTime.Parse(dtStartBy.Value);
                    break;
                case "CompleteBy":
					var dtCompleteBy = ((IList<DateTimeResolution>)step.Result).First();
					((MoveQueuesPost)step.Values[DialogKey]).dtCompleteBy = DateTime.Parse(dtCompleteBy.Value);
                    break;
                case "StartedAt":
					var dtStartedAt = ((IList<DateTimeResolution>)step.Result).First();
					((MoveQueuesPost)step.Values[DialogKey]).dtStartedAt = DateTime.Parse(dtStartedAt.Value);
                    break;
                case "CompletedAt":
					var dtCompletedAt = ((IList<DateTimeResolution>)step.Result).First();
					((MoveQueuesPost)step.Values[DialogKey]).dtCompletedAt = DateTime.Parse(dtCompletedAt.Value);
                    break;
                case "InboundOrderLine":
					FoundChoice _InboundOrderLine = (FoundChoice)step.Result;
					var ixInboundOrderLine = _movequeuesService.selectInboundOrderLines().Where(ct => ct.sInboundOrderLine == _InboundOrderLine.Value).Select(ct => ct.ixInboundOrderLine).First();
					((MoveQueuesPost)step.Values[DialogKey]).ixInboundOrderLine = ixInboundOrderLine;
                    break;
                case "OutboundOrderLine":
					FoundChoice _OutboundOrderLine = (FoundChoice)step.Result;
					var ixOutboundOrderLine = _movequeuesService.selectOutboundOrderLines().Where(ct => ct.sOutboundOrderLine == _OutboundOrderLine.Value).Select(ct => ct.ixOutboundOrderLine).First();
					((MoveQueuesPost)step.Values[DialogKey]).ixOutboundOrderLine = ixOutboundOrderLine;
                    break;
                case "PickBatch":
					FoundChoice _PickBatch = (FoundChoice)step.Result;
					var ixPickBatch = _movequeuesService.selectPickBatches().Where(ct => ct.sPickBatch == _PickBatch.Value).Select(ct => ct.ixPickBatch).First();
					((MoveQueuesPost)step.Values[DialogKey]).ixPickBatch = ixPickBatch;
                    break;
                case "Status":
					FoundChoice _Status = (FoundChoice)step.Result;
					var ixStatus = _movequeuesService.selectStatuses().Where(ct => ct.sStatus == _Status.Value).Select(ct => ct.ixStatus).First();
					((MoveQueuesPost)step.Values[DialogKey]).ixStatus = ixStatus;
                    break;

                default:
                    break;
            }

            return await step.EndDialogAsync(
                (MoveQueuesPost)step.Values[DialogKey],
                cancellationToken);
        }


    }
}


