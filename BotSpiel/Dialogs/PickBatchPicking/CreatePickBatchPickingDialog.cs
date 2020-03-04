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
//Custom Code Start | Added Code Block 
using BotSpiel.Services.Utilities;
using BotSpiel.DataAccess.Utilities;
//Custom Code End

namespace BotSpiel.Dialogs
{
    public class CreatePickBatchPickingDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreatePickBatchPickingDialogId = "createPickBatchPickingDialog";
        private const string PickBatchPickPromptId = "pickbatchpickPrompt";
        private const string InventoryUnitPromptId = "inventoryunitPrompt";
        private const string BaseUnitQuantityPickedPromptId = "baseunitquantitypickedPrompt";
        private const string PackToHandlingUnitPromptId = "packtohandlingunitPrompt";

        //Custom Code Start | Added Code Block 
        private const string CreateGetPickBatchesDialogId = "createGetPickBatchesDialog";
        //Custom Code End

        private const string DialogKey = nameof(CreatePickBatchPickingDialog);
        private const string DialogKeyOptions = "createPickBatchPickingDialogOptions";
        private const string SearchColumnsKey = "CreatePickBatchPickingDialogSearchColumns";
        private const string SearchTextKey = "CreatePickBatchPickingDialogSearchText";
        private const string EditColumnsKey = "CreatePickBatchPickingDialogEditColumns";
        private const string EditTextKey = "CreatePickBatchPickingDialogEditText";
        private const string SelectedRecordKey = "CreatePickBatchPickingDialogSelectedRecordKey";
        //Custom Code Start | Added Code Block 
        private const string PickSuggestionKey = "CreatePickBatchPickingDialogPickSuggestionKey";
        private const string PickSuggestionQtyKey = "CreatePickBatchPickingDialogPickSuggestionQtyKey";
        private const string IsCompleteInventoryUnitPickKey = "CreatePickBatchPickingDialogIsCompleteInventoryUnitPickKey";
        private const string IsHandlingUnitPickKey = "CreatePickBatchPickingDialogIsHandlingUnitPickKey";
        private const string PickedInventoryUnitKey = "CreatePickBatchPickingDialogPickedInventoryUnit";
        private const string LineQtysPickedKey = "CreatePickBatchPickingDialogLineQtysPickedKey";
        //Custom Code End

        private readonly IPickBatchPickingService _pickbatchpickingService;

        //Custom Code Start | Added Code Block 
        private readonly IPickBatchesService _pickbatchesService;
        private readonly CommonLookUps _commonLookUps;
        private readonly Picking _picking;
        private readonly IInventoryUnitsService _inventoryunitsService;
        private readonly IMoveQueuesService _movequeuesService;
        private readonly IHandlingUnitsService _handlingunitsService;
        private readonly IOutboundOrderLinesInventoryAllocationService _outboundorderlinesinventoryallocationService;
        private readonly IOutboundOrderLinePackingService _outboundorderlinepackingService;
        //Custom Code End


        PickBatchPickingPost _pickbatchpickingPost;

        string[] refine = { "Refine search" };
        string[] exit = { "Exit pickbatchpicking" };
        string[] edit = { "Edit pickbatchpicking" };
        string[] details = { "Display pickbatchpicking" };
        string[] delete = { "Delete pickbatchpicking" };

        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public CreatePickBatchPickingDialog(string id, IPickBatchPickingService pickbatchpickingService, PickBatchPickingPost pickbatchpickingPost, BotSpielUserStateAccessors statePropertyAccessor)
        //Replaced Code Block End
        public CreatePickBatchPickingDialog(string id, IPickBatchPickingService pickbatchpickingService, PickBatchPickingPost pickbatchpickingPost, BotSpielUserStateAccessors statePropertyAccessor
            , IPickBatchesService pickbatchesService
            , CommonLookUps commonLookUps
            , Picking picking
            , IInventoryUnitsService inventoryunitsService
            , IMoveQueuesService movequeuesService
            , IHandlingUnitsService handlingunitsService
            , IOutboundOrderLinesInventoryAllocationService outboundorderlinesinventoryallocationService
            , IOutboundOrderLinePackingService outboundorderlinepackingService
            )
        //Custom Code End
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _pickbatchpickingService = pickbatchpickingService;
            _pickbatchpickingPost = pickbatchpickingPost;

            //Custom Code Start | Added Code Block 
            _pickbatchesService = pickbatchesService;
            _commonLookUps = commonLookUps;
            _picking = picking;
            _inventoryunitsService = inventoryunitsService;
            _movequeuesService = movequeuesService;
            _handlingunitsService = handlingunitsService;
            _outboundorderlinesinventoryallocationService = outboundorderlinesinventoryallocationService;
            _outboundorderlinepackingService = outboundorderlinepackingService;

            PromptValidator<string> pickBatchPickValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value.Trim().ToLower();
                if (!_pickbatchesService.IndexDb().Where(x => x.sPickBatch.Trim().ToLower() == value && x.ixStatus != _commonLookUps.getStatuses().Where(s => s.sStatus == "Complete").Select(s => s.ixStatus).FirstOrDefault()).Any())
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The pick batch {value} does not exist or is already complete. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    if (_pickbatchesService.IndexDb().Where(x => x.sPickBatch.Trim().ToLower() == value && x.ixStatus == _commonLookUps.getStatuses().Where(s => s.sStatus == "Started").Select(s => s.ixStatus).FirstOrDefault() && !x.bMultiResource).Any())
                    {
                        await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The pick batch {value} has already been started and is flagged for a single picker. Please enter a different value or exit."), cancellationToken);
                        return false;
                    }
                    else if (_pickbatchesService.IndexDb().Where(x => x.sPickBatch.Trim().ToLower() == value && x.ixStatus == _commonLookUps.getStatuses().Where(s => s.sStatus == "Inactive").Select(s => s.ixStatus).FirstOrDefault()).Any())
                    {
                        await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The pick batch {value} has not been activated. Please activate or enter a different value or exit."), cancellationToken);
                        return false;
                    }
                    return true;
                }
            };

            PromptValidator<float> baseUnitQuantityPickedValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value;
                var currentBotUserData = await _botSpielUserStateAccessors.BotUserDataAccessor.GetAsync(promptContext.Context, () => _botUserData);

                if (value > currentBotUserData.pickSuggestion.Item2)
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The quantity picked {value.ToString()} must be less than or equal to the quantity suggested {currentBotUserData.pickSuggestion.Item2.ToString()}. Please enter a different value or exit."), cancellationToken);
                    return false;

                }
                else
                {
                    return true;
                }
            };

            //Custom Code End



            // Define the prompts used in the Dialog.
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //AddDialog(new TextPrompt(PickBatchPickPromptId));
            //Replaced Code Block End
            AddDialog(new TextPrompt(PickBatchPickPromptId, pickBatchPickValidator));
            //Custom Code End
            AddDialog(new ChoicePrompt(InventoryUnitPromptId));
            AddDialog(new NumberPrompt<float>(BaseUnitQuantityPickedPromptId, baseUnitQuantityPickedValidator));
            AddDialog(new TextPrompt(PackToHandlingUnitPromptId));

            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
                //PickBatchPickPrompt,
                //Custom Code Start | Removed Block 
                //InventoryUnitPrompt,			
                //Custom Code End			
                BaseUnitQuantityPickedPrompt,
                PackToHandlingUnitPrompt,
                donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));
        }
        private async Task<DialogTurnResult> PickBatchPickPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new PickBatchPickingPost();

            return await step.PromptAsync(
                PickBatchPickPromptId,
                new PromptOptions
                {
                    //Custom Code Start | Replaced Code Block
                    //Replaced Code Block Start
                    //Prompt = MessageFactory.Text($"Please enter a PickBatchPick:"),
                    //Replaced Code Block End
                    Prompt = MessageFactory.Text($"Please enter a Pick Batch to pick:"),
                    //Custom Code End
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> InventoryUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await step.PromptAsync(
                InventoryUnitPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Please enter a InventoryUnit:"),
                    RetryPrompt = MessageFactory.Text("Please choose an option from the list."),
                    Choices = ChoiceFactory.ToChoices(_pickbatchpickingService.selectInventoryUnits().Select(ct => ct.sInventoryUnit).ToList()),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> BaseUnitQuantityPickedPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            //Custom Code Start | Removed Block 
            //FoundChoice _InventoryUnit = (FoundChoice)step.Result;
            //var ixInventoryUnit = _pickbatchpickingService.selectInventoryUnits().Where(ct => ct.sInventoryUnit == _InventoryUnit.Value).Select(ct => ct.ixInventoryUnit).First();
            //((PickBatchPickingPost)step.Values[DialogKey]).ixInventoryUnit = ixInventoryUnit;
            //Custom Code End

            //if (!(((PickBatchPickingPost)step.Options).ixPickBatch > 0))
            //{
            //    return await step.BeginDialogAsync(CreateGetPickBatchesDialogId, null, cancellationToken);
            //    //var sPickBatchPick = (string)step.Result;
            //    //((PickBatchPickingPost)step.Values[DialogKey]).sPickBatchPick = sPickBatchPick;
            //}
            //else
            //{
            step.Values[DialogKey] = new PickBatchPickingPost();
            ((PickBatchPickingPost)step.Values[DialogKey]).sPickBatchPick = ((PickBatchPickingPost)step.Options).sPickBatchPick;
            //}

            //Custom Code Start | Added Code Block 
            var sPickBatchPick = ((PickBatchPickingPost)step.Options).sPickBatchPick;
            //((PickBatchPickingPost)step.Values[DialogKey]).sPickBatchPick = sPickBatchPick;

            var ixPickBatch = _pickbatchesService.IndexDb().Where(x => x.sPickBatch.Trim().ToLower() == sPickBatchPick.Trim().ToLower()).Select(x => x.ixPickBatch).FirstOrDefault();
            ((PickBatchPickingPost)step.Values[DialogKey]).ixPickBatch = ixPickBatch;
            //Now we get the pick suggestion
            var currentBotUserData = await _botSpielUserStateAccessors.BotUserDataAccessor.GetAsync(step.Context, () => _botUserData);
            var pickSuggestion = _picking.getPickSuggestion(ixPickBatch, currentBotUserData);
            currentBotUserData.pickSuggestion = pickSuggestion;
            await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
            await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);

            var pickSuggestionText = "";
            if (pickSuggestion.Item1 > 0 && pickSuggestion.Item2 > 0)
            {
                var inventoryUnit = _inventoryunitsService.Get(pickSuggestion.Item1);
                pickSuggestionText = $@"Please pick inventory unit:
Inventory Location: {inventoryUnit.InventoryLocations.sInventoryLocation}
Handling Unit: {inventoryUnit.HandlingUnits.sHandlingUnit}
Material: {inventoryUnit.Materials.sMaterial}
Pick Quantity: {pickSuggestion.Item2}
and confirm the quantity picked.
";
                ((PickBatchPickingPost)step.Values[DialogKey]).ixInventoryUnit = inventoryUnit.ixInventoryUnit;
                step.Values[PickSuggestionKey] = inventoryUnit;
                step.Values[PickSuggestionQtyKey] = pickSuggestion.Item2;
                //We update the queued qty on the iu
                var inventoryUnitPickedFrom = _inventoryunitsService.GetPost(inventoryUnit.ixInventoryUnit);
                inventoryUnitPickedFrom.nBaseUnitQuantityQueued += pickSuggestion.Item2;
                inventoryUnitPickedFrom.UserName = step.Context.Activity.Conversation.Id;
                await _inventoryunitsService.Edit(inventoryUnitPickedFrom, _commonLookUps.getInventoryUnitTransactionContext().Where(x => x.sInventoryUnitTransactionContext == "Inventory Adjustment").Select(x => x.ixInventoryUnitTransactionContext).FirstOrDefault());
            }
            else
            {
                pickSuggestionText = "I cannot find an inventory unit to pick, please exit and choose a different batch or correct the inventory.";
            }

            //Custom Code End

            return await step.PromptAsync(
                BaseUnitQuantityPickedPromptId,
                new PromptOptions
                {
                    //Custom Code Start | Replaced Code Block
                    //Replaced Code Block Start
                    //Prompt = MessageFactory.Text($"Please enter a BaseUnitQuantityPicked:"),
                    //Replaced Code Block End
                    Prompt = MessageFactory.Text($"{pickSuggestionText}"),
                    //Custom Code End
                    RetryPrompt = MessageFactory.Text("Please enter a number."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> PackToHandlingUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var nBaseUnitQuantityPicked = step.Result;
            ((PickBatchPickingPost)step.Values[DialogKey]).nBaseUnitQuantityPicked = Convert.ToDouble(nBaseUnitQuantityPicked);

            //Custom Code Start | Added Code Block 
            step.Values[IsCompleteInventoryUnitPickKey] = _picking.isCompleteInventoryUnitPick((InventoryUnits)step.Values[PickSuggestionKey], Convert.ToDouble(nBaseUnitQuantityPicked));
            step.Values[IsHandlingUnitPickKey] = _picking.isHandlingUnitPick((InventoryUnits)step.Values[PickSuggestionKey], Convert.ToDouble(nBaseUnitQuantityPicked));

            //We adjust the queued qty down
            var inventoryUnitPickedFrom = _inventoryunitsService.GetPost(((InventoryUnits)step.Values[PickSuggestionKey]).ixInventoryUnit);
            inventoryUnitPickedFrom.nBaseUnitQuantityQueued -= (double)step.Values[PickSuggestionQtyKey];
            inventoryUnitPickedFrom.UserName = step.Context.Activity.Conversation.Id;
            await _inventoryunitsService.Edit(inventoryUnitPickedFrom, _commonLookUps.getInventoryUnitTransactionContext().Where(x => x.sInventoryUnitTransactionContext == "Inventory Adjustment").Select(x => x.ixInventoryUnitTransactionContext).FirstOrDefault());

            var currentBotUserData = await _botSpielUserStateAccessors.BotUserDataAccessor.GetAsync(step.Context, () => _botUserData);
            //If we pick complete inventory unit we move it to the picker else we adjust and create/consolidate and then move to the picker
            if ((bool)step.Values[IsCompleteInventoryUnitPickKey])
            {
                //We now create and execute the move queue for the pickup
                step.Values[PickedInventoryUnitKey] = ((InventoryUnits)step.Values[PickSuggestionKey]).ixInventoryUnit;
                MoveQueuesPost moveQueuePost = new MoveQueuesPost();
                if (_picking.isHandlingUnitPick((InventoryUnits)step.Values[PickSuggestionKey], Convert.ToDouble(nBaseUnitQuantityPicked)))
                {
                    moveQueuePost.ixMoveQueueType = _commonLookUps.getMoveQueueTypes().Where(x => x.sMoveQueueType == "Unit Pickup - Consolidated Drop").Select(x => x.ixMoveQueueType).FirstOrDefault();
                }
                else
                {
                    moveQueuePost.ixMoveQueueType = _commonLookUps.getMoveQueueTypes().Where(x => x.sMoveQueueType == "Consolidated Pickup - Consolidated Drop").Select(x => x.ixMoveQueueType).FirstOrDefault();
                }
                moveQueuePost.ixMoveQueueContext = _commonLookUps.getMoveQueueContexts().Where(x => x.sMoveQueueContext == "Picking").Select(x => x.ixMoveQueueContext).FirstOrDefault();
                moveQueuePost.ixSourceInventoryUnit = ((InventoryUnits)step.Values[PickSuggestionKey]).ixInventoryUnit;
                moveQueuePost.ixTargetInventoryUnit = ((InventoryUnits)step.Values[PickSuggestionKey]).ixInventoryUnit;
                moveQueuePost.ixSourceInventoryLocation = ((InventoryUnits)step.Values[PickSuggestionKey]).ixInventoryLocation;
                moveQueuePost.ixTargetInventoryLocation = currentBotUserData.ixInventoryLocation;
                moveQueuePost.ixSourceHandlingUnit = ((InventoryUnits)step.Values[PickSuggestionKey]).ixHandlingUnit > 0 ? ((InventoryUnits)step.Values[PickSuggestionKey]).ixHandlingUnit : null;
                moveQueuePost.ixTargetHandlingUnit = ((InventoryUnits)step.Values[PickSuggestionKey]).ixHandlingUnit > 0 ? ((InventoryUnits)step.Values[PickSuggestionKey]).ixHandlingUnit : null;
                moveQueuePost.sPreferredResource = step.Context.Activity.Conversation.Id;
                moveQueuePost.nBaseUnitQuantity = Convert.ToDouble(nBaseUnitQuantityPicked);
                moveQueuePost.dtStartedAt = DateTime.Now;
                moveQueuePost.ixPickBatch = ((PickBatchPickingPost)step.Values[DialogKey]).ixPickBatch;
                moveQueuePost.ixStatus = _commonLookUps.getStatuses().Where(x => x.sStatus == "Active").Select(x => x.ixStatus).FirstOrDefault();
                moveQueuePost.UserName = step.Context.Activity.Conversation.Id;
                var ixMoveQueue = await _movequeuesService.Create(moveQueuePost);
                //We now complete the move queue for the pickup
                var moveQueuePickUp = _movequeuesService.GetPost(ixMoveQueue);
                moveQueuePickUp.dtCompletedAt = DateTime.Now;
                moveQueuePickUp.ixStatus = _commonLookUps.getStatuses().Where(x => x.sStatus == "Complete").Select(x => x.ixStatus).FirstOrDefault();
                moveQueuePickUp.UserName = moveQueuePost.UserName;
                await _movequeuesService.Edit(moveQueuePickUp);
            }
            else
            {
                //Now we adjust the iu remaining down
                inventoryUnitPickedFrom.nBaseUnitQuantity -= Convert.ToDouble(nBaseUnitQuantityPicked);
                inventoryUnitPickedFrom.UserName = step.Context.Activity.Conversation.Id;
                await _inventoryunitsService.Edit(inventoryUnitPickedFrom, _commonLookUps.getInventoryUnitTransactionContext().Where(x => x.sInventoryUnitTransactionContext == "Inventory Adjustment").Select(x => x.ixInventoryUnitTransactionContext).FirstOrDefault());

                //Now we either create or edit an iu for the qty picked
                inventoryUnitPickedFrom.nBaseUnitQuantity = Convert.ToDouble(nBaseUnitQuantityPicked);
                inventoryUnitPickedFrom.UserName = step.Context.Activity.Conversation.Id;
                inventoryUnitPickedFrom.ixInventoryLocation = currentBotUserData.ixInventoryLocation;

                if (_inventoryunitsService.IndexDb().Where(x =>
                                x.ixFacility == inventoryUnitPickedFrom.ixFacility &&
                                x.ixCompany == inventoryUnitPickedFrom.ixCompany &&
                                x.ixMaterial == inventoryUnitPickedFrom.ixMaterial &&
                                x.ixInventoryState == inventoryUnitPickedFrom.ixInventoryState &&
                                x.ixHandlingUnit == inventoryUnitPickedFrom.ixHandlingUnit &&
                                x.ixInventoryLocation == inventoryUnitPickedFrom.ixInventoryLocation &&
                                x.sBatchNumber == inventoryUnitPickedFrom.sBatchNumber &&
                                x.dtExpireAt == inventoryUnitPickedFrom.dtExpireAt && x.ixStatus == 5
                                ).Select(x => x.ixInventoryUnit).Any()
                                )
                {
                    //We edit the iu
                    inventoryUnitPickedFrom.ixInventoryUnit = _inventoryunitsService.IndexDb().Where(x =>
                    x.ixFacility == inventoryUnitPickedFrom.ixFacility &&
                    x.ixCompany == inventoryUnitPickedFrom.ixCompany &&
                    x.ixMaterial == inventoryUnitPickedFrom.ixMaterial &&
                    x.ixInventoryState == inventoryUnitPickedFrom.ixInventoryState &&
                    x.ixHandlingUnit == inventoryUnitPickedFrom.ixHandlingUnit &&
                    x.ixInventoryLocation == inventoryUnitPickedFrom.ixInventoryLocation &&
                    x.sBatchNumber == inventoryUnitPickedFrom.sBatchNumber &&
                    x.dtExpireAt == inventoryUnitPickedFrom.dtExpireAt && x.ixStatus == 5
                    ).Select(x => x.ixInventoryUnit).FirstOrDefault();
                    inventoryUnitPickedFrom.nBaseUnitQuantity = _inventoryunitsService.GetPost(inventoryUnitPickedFrom.ixInventoryUnit).nBaseUnitQuantity + Convert.ToDouble(nBaseUnitQuantityPicked);
                    await _inventoryunitsService.Edit(inventoryUnitPickedFrom, _commonLookUps.getInventoryUnitTransactionContext().Where(x => x.sInventoryUnitTransactionContext == "Inventory Adjustment").Select(x => x.ixInventoryUnitTransactionContext).FirstOrDefault());
                    step.Values[PickedInventoryUnitKey] = inventoryUnitPickedFrom.ixInventoryUnit;
                }
                else
                {
                    //We create an iu
                    step.Values[PickedInventoryUnitKey] = await _inventoryunitsService.Create(inventoryUnitPickedFrom, _commonLookUps.getInventoryUnitTransactionContext().Where(x => x.sInventoryUnitTransactionContext == "Inventory Adjustment").Select(x => x.ixInventoryUnitTransactionContext).FirstOrDefault());
                }
            }

            //We now allocate the picked qty to open allocation lines

            step.Values[LineQtysPickedKey] = new List<Tuple<Int64, double>>();

            var pickedQtyNotAllocated = Convert.ToDouble(nBaseUnitQuantityPicked);
            _outboundorderlinesinventoryallocationService.IndexDbPost().Where(x => (x.nBaseUnitQuantityAllocated - x.nBaseUnitQuantityPicked) > 0).OrderBy(x => (x.nBaseUnitQuantityAllocated - x.nBaseUnitQuantityPicked) > 0).ToList()
                .Join(_picking.getOrderLinesInBatchForMaterial(((PickBatchPickingPost)step.Values[DialogKey]).ixPickBatch, ((InventoryUnits)step.Values[PickSuggestionKey]).ixMaterial), ol => ol.ixOutboundOrderLine, olb => olb, (ol, olb) => new { Ol = ol, Olb = olb }).ToList()
                .ForEach(y =>
                {
                    if ((y.Ol.nBaseUnitQuantityAllocated - y.Ol.nBaseUnitQuantityPicked) <= pickedQtyNotAllocated && pickedQtyNotAllocated > 0)
                    {
                        ((List<Tuple<Int64, double>>)step.Values[LineQtysPickedKey]).Add(new Tuple<long, double>(y.Ol.ixOutboundOrderLine, y.Ol.nBaseUnitQuantityAllocated - y.Ol.nBaseUnitQuantityPicked));
                        y.Ol.nBaseUnitQuantityPicked += y.Ol.nBaseUnitQuantityAllocated - y.Ol.nBaseUnitQuantityPicked;
                        pickedQtyNotAllocated -= y.Ol.nBaseUnitQuantityAllocated - y.Ol.nBaseUnitQuantityPicked;
                        y.Ol.UserName = step.Context.Activity.Conversation.Id;
                        _outboundorderlinesinventoryallocationService.Edit(y.Ol);
                    }
                    else if ((y.Ol.nBaseUnitQuantityAllocated - y.Ol.nBaseUnitQuantityPicked) > pickedQtyNotAllocated && pickedQtyNotAllocated > 0)
                    {
                        ((List<Tuple<Int64, double>>)step.Values[LineQtysPickedKey]).Add(new Tuple<long, double>(y.Ol.ixOutboundOrderLine, pickedQtyNotAllocated));
                        y.Ol.nBaseUnitQuantityPicked += pickedQtyNotAllocated;
                        pickedQtyNotAllocated -= pickedQtyNotAllocated;
                        y.Ol.UserName = step.Context.Activity.Conversation.Id;
                        _outboundorderlinesinventoryallocationService.Edit(y.Ol);

                    }
                }
                );

            //Custom Code End

            //We have to figure out whether this is a pick of a complete Handling Unit.
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //return await step.PromptAsync(
            //    PackToHandlingUnitPromptId,
            //    new PromptOptions
            //    {
            //        Prompt = MessageFactory.Text($"Please enter a PackToHandlingUnit:"),
            //        RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
            //    },
            //    cancellationToken);
            //Replaced Code Block End
            if ((bool)step.Values[IsHandlingUnitPickKey])
            {
                ((PickBatchPickingPost)step.Values[DialogKey]).sPackToHandlingUnit = ((InventoryUnits)step.Values[PickSuggestionKey]).HandlingUnits.sHandlingUnit;
                ((PickBatchPickingPost)step.Values[DialogKey]).ixHandlingUnit = ((InventoryUnits)step.Values[PickSuggestionKey]).HandlingUnits.ixHandlingUnit;

                //We create the packing record
                var orderLinesToPack = ((List<Tuple<Int64, double>>)step.Values[LineQtysPickedKey]).GroupBy(x => x.Item1).Select(x => new { ixOutboundOrderLine = x.Key, Total = x.Sum(s => s.Item2) }).ToList();

                orderLinesToPack.ForEach(x =>
                    {
                        //We check if there is an existing pack record we can use
                        if (
                                _outboundorderlinepackingService.IndexDb().Where(p =>
                                p.ixHandlingUnit == ((PickBatchPickingPost)step.Values[DialogKey]).ixHandlingUnit &&
                                p.ixOutboundOrderLine == x.ixOutboundOrderLine
                                ).Any()
                            )
                        {
                            var outboundorderlinepack = _outboundorderlinepackingService.GetPost(_outboundorderlinepackingService.IndexDb().Where(p =>
                               p.ixHandlingUnit == ((PickBatchPickingPost)step.Values[DialogKey]).ixHandlingUnit &&
                               p.ixOutboundOrderLine == x.ixOutboundOrderLine
                                ).Select(p => p.ixOutboundOrderLinePack).FirstOrDefault()
                                );
                            outboundorderlinepack.nBaseUnitQuantityPacked += x.Total;
                            outboundorderlinepack.UserName = step.Context.Activity.Conversation.Id;
                            _outboundorderlinepackingService.Edit(outboundorderlinepack);
                        }
                        else
                        {
                            var outboundorderlinepack = new OutboundOrderLinePackingPost();
                            outboundorderlinepack.ixOutboundOrderLine = x.ixOutboundOrderLine;
                            outboundorderlinepack.ixHandlingUnit = ((PickBatchPickingPost)step.Values[DialogKey]).ixHandlingUnit;
                            outboundorderlinepack.nBaseUnitQuantityPacked = x.Total;
                            outboundorderlinepack.ixStatus = _commonLookUps.getStatuses().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault();
                            outboundorderlinepack.UserName = step.Context.Activity.Conversation.Id;
                            _outboundorderlinepackingService.Create(outboundorderlinepack);
                        }
                    }
                    );

                return await step.EndDialogAsync(
                    (PickBatchPickingPost)step.Values[DialogKey],
                    cancellationToken);
            }
            else
            {
                return await step.PromptAsync(
                    PackToHandlingUnitPromptId,
                    new PromptOptions
                    {
                        //Custom Code Start | Replaced Code Block
                        //Replaced Code Block Start
                        //Prompt = MessageFactory.Text($"Please enter a PackToHandlingUnit:"),
                        //Replaced Code Block End
                        Prompt = MessageFactory.Text($"Please enter/scan the Pack To Handling Unit:"),
                        //Custom Code End
                        RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                    },
                    cancellationToken);
            }
            //Custom Code End


        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sPackToHandlingUnit = (string)step.Result;
            ((PickBatchPickingPost)step.Values[DialogKey]).sPackToHandlingUnit = sPackToHandlingUnit;

            //Custom Code Start | Added Code Block 
            //If the handling unit does not exist we create it

            long ixHandlingUnit = 0;

            if (!_movequeuesService.HandlingUnitsDb().Where(x => x.sHandlingUnit.Trim() == sPackToHandlingUnit.Trim()).Any())
            {
                HandlingUnitsPost handlingUnitsPost = new HandlingUnitsPost();
                handlingUnitsPost.sHandlingUnit = sPackToHandlingUnit.Trim();
                handlingUnitsPost.ixHandlingUnitType = _commonLookUps.getHandlingUnitTypes().Where(x => x.sHandlingUnitType == "Carton").Select(x => x.ixHandlingUnitType).FirstOrDefault();
                handlingUnitsPost.UserName = step.Context.Activity.Conversation.Id;
                ixHandlingUnit = _handlingunitsService.Create(handlingUnitsPost).Result;
            }
            else
            {
                ixHandlingUnit = _movequeuesService.HandlingUnitsDb().Where(x => x.sHandlingUnit == sPackToHandlingUnit.Trim()).Select(x => x.ixHandlingUnit).FirstOrDefault();
            }
            ((PickBatchPickingPost)step.Values[DialogKey]).ixHandlingUnit = ixHandlingUnit;

            var inventoryUnitPicked = _inventoryunitsService.GetPost((long)step.Values[PickedInventoryUnitKey]);
            inventoryUnitPicked.ixHandlingUnit = ixHandlingUnit;
            inventoryUnitPicked.UserName = step.Context.Activity.Conversation.Id;
            await _inventoryunitsService.Edit(inventoryUnitPicked, _commonLookUps.getInventoryUnitTransactionContext().Where(x => x.sInventoryUnitTransactionContext == "Inventory Adjustment").Select(x => x.ixInventoryUnitTransactionContext).FirstOrDefault());

            //We create the packing record
            var orderLinesToPack = ((List<Tuple<Int64, double>>)step.Values[LineQtysPickedKey]).GroupBy(x => x.Item1).Select(x => new { ixOutboundOrderLine = x.Key, Total = x.Sum(s => s.Item2) }).ToList();

            orderLinesToPack.ForEach(x =>
            {
                //We check if there is an existing pack record we can use
                if (
                        _outboundorderlinepackingService.IndexDb().Where(p =>
                        p.ixHandlingUnit == ((PickBatchPickingPost)step.Values[DialogKey]).ixHandlingUnit &&
                        p.ixOutboundOrderLine == x.ixOutboundOrderLine
                        ).Any()
                    )
                {
                    var outboundorderlinepack = _outboundorderlinepackingService.GetPost(_outboundorderlinepackingService.IndexDb().Where(p =>
                       p.ixHandlingUnit == ((PickBatchPickingPost)step.Values[DialogKey]).ixHandlingUnit &&
                       p.ixOutboundOrderLine == x.ixOutboundOrderLine
                        ).Select(p => p.ixOutboundOrderLinePack).FirstOrDefault()
                        );
                    outboundorderlinepack.nBaseUnitQuantityPacked += x.Total;
                    outboundorderlinepack.UserName = step.Context.Activity.Conversation.Id;
                    _outboundorderlinepackingService.Edit(outboundorderlinepack);
                }
                else
                {
                    var outboundorderlinepack = new OutboundOrderLinePackingPost();
                    outboundorderlinepack.ixOutboundOrderLine = x.ixOutboundOrderLine;
                    outboundorderlinepack.ixHandlingUnit = ((PickBatchPickingPost)step.Values[DialogKey]).ixHandlingUnit;
                    outboundorderlinepack.nBaseUnitQuantityPacked = x.Total;
                    outboundorderlinepack.ixStatus = _commonLookUps.getStatuses().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault();
                    outboundorderlinepack.UserName = step.Context.Activity.Conversation.Id;
                    _outboundorderlinepackingService.Create(outboundorderlinepack);
                }
            }
                );

            //Custom Code End

            return await step.EndDialogAsync(
                (PickBatchPickingPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


