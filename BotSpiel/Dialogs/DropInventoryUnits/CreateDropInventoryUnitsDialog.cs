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
//Custom Code End

namespace BotSpiel.Dialogs
{
    public class CreateDropInventoryUnitsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreateDropInventoryUnitsDialogId = "createDropInventoryUnitsDialog";
       private const string DropInventoryUnitPromptId = "dropinventoryunitPrompt";

        private const string DialogKey = nameof(CreateDropInventoryUnitsDialog);
        private const string DialogKeyOptions = "createDropInventoryUnitsDialogOptions";
        private const string SearchColumnsKey = "CreateDropInventoryUnitsDialogSearchColumns";
        private const string SearchTextKey = "CreateDropInventoryUnitsDialogSearchText";
        private const string EditColumnsKey = "CreateDropInventoryUnitsDialogEditColumns";
        private const string EditTextKey = "CreateDropInventoryUnitsDialogEditText";
        private const string SelectedRecordKey = "CreateDropInventoryUnitsDialogSelectedRecordKey";

        private readonly IDropInventoryUnitsService _dropinventoryunitsService;
        DropInventoryUnitsPost _dropinventoryunitsPost;

        //Custom Code Start | Added Code Block 
        private readonly Shipping _shipping;
        private readonly IInventoryLocationsService _inventorylocationsService;
        private readonly CommonLookUps _commonLookUps;
        private readonly IMoveQueuesService _movequeuesService;
        //Custom Code End

        string[] refine = { "Refine search" };
        string[] exit = { "Exit dropinventoryunits" };
        string[] edit = { "Edit dropinventoryunits" };
        string[] details = { "Display dropinventoryunits" };
        string[] delete = { "Delete dropinventoryunits" };

        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public CreateDropInventoryUnitsDialog(string id, IDropInventoryUnitsService dropinventoryunitsService, DropInventoryUnitsPost dropinventoryunitsPost, BotSpielUserStateAccessors statePropertyAccessor)
        //Replaced Code Block End
        public CreateDropInventoryUnitsDialog(string id, IDropInventoryUnitsService dropinventoryunitsService, DropInventoryUnitsPost dropinventoryunitsPost, BotSpielUserStateAccessors statePropertyAccessor, Shipping shipping, IInventoryLocationsService inventorylocationsService, CommonLookUps commonLookUps, IMoveQueuesService movequeuesService)
        //Custom Code End
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _dropinventoryunitsService = dropinventoryunitsService;
            _dropinventoryunitsPost = dropinventoryunitsPost;

            //Custom Code Start | Added Code Block 
            _shipping = shipping;
            _inventorylocationsService = inventorylocationsService;
            _commonLookUps = commonLookUps;
            _movequeuesService = movequeuesService;
            //Custom Code End

            //Custom Code Start | Added Code Block 
            PromptValidator<string> inventoryDropLocationValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value.Trim().ToLower();
                var currentBotUserData = await _botSpielUserStateAccessors.BotUserDataAccessor.GetAsync(promptContext.Context, () => _botUserData);

                if (!_inventorylocationsService.IndexDb().Where(x => x.sInventoryLocation.Trim().ToLower() == value && x.ixFacility == currentBotUserData.ixFacility).Any())
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The inventory location {value} does not exist in the facility. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                //else
                //{
                //    if (currentBotUserData.sPutAwaySuggestion.Trim().ToLower() != value)
                //    {
                //        await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The inventory location {value} does not match the suggested location {currentBotUserData.sPutAwaySuggestion}. Please enter a different value or exit."), cancellationToken);
                //        return false;
                //    }
                //    return true;
                //}
                return true;
            };

            //Custom Code End

            // Define the prompts used in the Dialog.
            AddDialog(new TextPrompt(DropInventoryUnitPromptId, inventoryDropLocationValidator));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             DropInventoryUnitPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> DropInventoryUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new DropInventoryUnitsPost();

            //Custom Code Start | Added Code Block 
            var promptString = "";
            // We start by getting the suggested drop location  
            var ixPickupInventoryLocation = _shipping.getDropLocationForPickBatch(((PickBatchPickingPost)step.Options).ixPickBatch);

            if (ixPickupInventoryLocation > 0)
            {
                promptString = $"The suggested drop location is {_inventorylocationsService.GetPost(ixPickupInventoryLocation).sInventoryLocation}.{Environment.NewLine} Please confirm the drop location.";
            }
            else
            {
                promptString = $"Please confirm the drop location.";
            }
            //Custom Code End


            return await step.PromptAsync(
                DropInventoryUnitPromptId,
                new PromptOptions
                {
                    //Custom Code Start | Replaced Code Block
                    //Replaced Code Block Start
                    //Prompt = MessageFactory.Text($"Please enter a DropInventoryUnit:"),
                    //Replaced Code Block End
                    Prompt = MessageFactory.Text(promptString),
                    //Custom Code End
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sDropInventoryUnit = (string)step.Result;
            ((DropInventoryUnitsPost)step.Values[DialogKey]).sDropInventoryUnit = sDropInventoryUnit;

            //Custom Code Start | Added Code Block 
            var currentBotUserData = await _botSpielUserStateAccessors.BotUserDataAccessor.GetAsync(step.Context, () => _botUserData);
            var ixInventoryLocationDrop = _inventorylocationsService.IndexDb().Where(x => x.sInventoryLocation.Trim().ToLower() == sDropInventoryUnit.Trim().ToLower() && x.ixFacility == currentBotUserData.ixFacility).Select(x => x.ixInventoryLocation).FirstOrDefault();

            //We drop all the inventory on the user
            //We now create and execute the move queue for the drop
            List<Int64> moveQueues = new List<long>();

            var handlingUnitsToDrop = _movequeuesService.InventoryUnitsDb().Where(x =>
                        x.ixInventoryLocation == currentBotUserData.ixInventoryLocation &&
                        x.nBaseUnitQuantity > 0 &&
                        x.ixStatus == _commonLookUps.getStatuses().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault() &&
                        x.ixHandlingUnit > 0
                        ).Select(x => x.ixHandlingUnit).Distinct().ToList();

            if (handlingUnitsToDrop.Count() > 0)
            {
                var moveQueueType = _commonLookUps.getMoveQueueTypes().Where(mqt => mqt.sMoveQueueType == "Consolidated Pickup - Consolidated Drop").Select(mqt => mqt.ixMoveQueueType).FirstOrDefault();
                var moveQueueContext = _commonLookUps.getMoveQueueContexts().Where(mqc => mqc.sMoveQueueContext == "Picking").Select(mqc => mqc.ixMoveQueueContext).FirstOrDefault();
                var statusActive = _commonLookUps.getStatuses().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault();
                var statusComplete = _commonLookUps.getStatuses().Where(s => s.sStatus == "Complete").Select(s => s.ixStatus).FirstOrDefault();
                handlingUnitsToDrop.ForEach(x =>
                    {
                        var moveQueuePostDrop = new MoveQueuesPost();
                        moveQueuePostDrop.ixMoveQueueType = moveQueueType;
                        moveQueuePostDrop.ixMoveQueueContext = moveQueueContext;
                        moveQueuePostDrop.ixSourceInventoryLocation = currentBotUserData.ixInventoryLocation;
                        moveQueuePostDrop.ixTargetInventoryLocation = ixInventoryLocationDrop;
                        moveQueuePostDrop.ixSourceHandlingUnit = x;
                        moveQueuePostDrop.ixTargetHandlingUnit = x;
                        moveQueuePostDrop.sPreferredResource = step.Context.Activity.Conversation.Id;
                        moveQueuePostDrop.nBaseUnitQuantity = 0;
                        moveQueuePostDrop.dtStartedAt = DateTime.Now;
                        moveQueuePostDrop.ixStatus = statusActive;
                        moveQueuePostDrop.UserName = step.Context.Activity.Conversation.Id;
                        moveQueues.Add(_movequeuesService.Create(moveQueuePostDrop).Result);
                    }
                    );
                moveQueues.ForEach(x =>
                    {
                        var moveQueueDrop = _movequeuesService.GetPost(x);
                        moveQueueDrop.dtCompletedAt = DateTime.Now;
                        moveQueueDrop.ixStatus = statusComplete;
                        moveQueueDrop.UserName = step.Context.Activity.Conversation.Id;
                        _movequeuesService.Edit(moveQueueDrop);
                    }
                    );
            }

            //Custom Code End


            return await step.EndDialogAsync(
                (DropInventoryUnitsPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


