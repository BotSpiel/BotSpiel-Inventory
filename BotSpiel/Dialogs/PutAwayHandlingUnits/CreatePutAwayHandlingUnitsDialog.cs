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
using BotSpiel.DataAccess.Utilities;
using BotSpiel.Services.Utilities;
//Custom Code End

namespace BotSpiel.Dialogs
{
    public class CreatePutAwayHandlingUnitsDialog : ComponentDialog
    {
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserData _botUserData;

        private const string CreatePutAwayHandlingUnitsDialogId = "createPutAwayHandlingUnitsDialog";
       private const string PutAwayHandlingUnitPromptId = "putawayhandlingunitPrompt";
        private const string InventoryDropLocationPromptId = "inventorydroplocationPrompt";

        private const string DialogKey = nameof(CreatePutAwayHandlingUnitsDialog);
        private const string DialogKeyOptions = "createPutAwayHandlingUnitsDialogOptions";
        private const string SearchColumnsKey = "CreatePutAwayHandlingUnitsDialogSearchColumns";
        private const string SearchTextKey = "CreatePutAwayHandlingUnitsDialogSearchText";
        private const string EditColumnsKey = "CreatePutAwayHandlingUnitsDialogEditColumns";
        private const string EditTextKey = "CreatePutAwayHandlingUnitsDialogEditText";
        private const string SelectedRecordKey = "CreatePutAwayHandlingUnitsDialogSelectedRecordKey";

        //Custom Code Start | Added Code Block 
        private const string PutAwaySuggestionKey = "CreatePutAwayHandlingUnitsDialogPutAwaySuggestionKey";
        private const string MoveQueuePostDropKey = "CreatePutAwayHandlingUnitsDialogMoveQueuePostDropKey";
        //Custom Code End

        private readonly IPutAwayHandlingUnitsService _putawayhandlingunitsService;
        PutAwayHandlingUnitsPost _putawayhandlingunitsPost;

        //Custom Code Start | Added Code Block 
        private readonly IHandlingUnitsService _handlingunitsService;
        private readonly PutAway _putAway;
        private readonly IInventoryLocationsService _inventorylocationsService;
        private readonly IMoveQueueTypesService _movequeuetypesService;
        private readonly IMoveQueueContextsService _movequeuecontextsService;
        private readonly IInventoryUnitsService _inventoryunitsService;
        private readonly IStatusesService _statusesService;
        private readonly IMoveQueuesService _movequeuesService;

        //Custom Code End

        string[] refine = { "Refine search" };
        string[] exit = { "Exit putawayhandlingunits" };
        string[] edit = { "Edit putawayhandlingunits" };
        string[] details = { "Display putawayhandlingunits" };
        string[] delete = { "Delete putawayhandlingunits" };

        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        //public CreatePutAwayHandlingUnitsDialog(string id, IPutAwayHandlingUnitsService putawayhandlingunitsService, PutAwayHandlingUnitsPost putawayhandlingunitsPost, BotSpielUserStateAccessors statePropertyAccessor)
        //Replaced Code Block End
        public CreatePutAwayHandlingUnitsDialog(string id
            , IPutAwayHandlingUnitsService putawayhandlingunitsService
            , PutAwayHandlingUnitsPost putawayhandlingunitsPost
            , BotSpielUserStateAccessors statePropertyAccessor
            , IHandlingUnitsService handlingunitsService
            , PutAway putAway
            , BotUserData botUserData
            , IInventoryLocationsService inventorylocationsService
            , IMoveQueueTypesService movequeuetypesService
            , IMoveQueueContextsService movequeuecontextsService
            , IInventoryUnitsService inventoryunitsService
            , IStatusesService statusesService
            , IMoveQueuesService movequeuesService
            )

        //Custom Code End
        : base(id)
        {

            InitialDialogId = Id;
            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _putawayhandlingunitsService = putawayhandlingunitsService;
            _putawayhandlingunitsPost = putawayhandlingunitsPost;

            //Custom Code Start | Added Code Block 
            _handlingunitsService = handlingunitsService;
            _putAway = putAway;
            _botUserData = botUserData;
            _inventorylocationsService = inventorylocationsService;
            _movequeuetypesService = movequeuetypesService;
            _movequeuecontextsService = movequeuecontextsService;
            _inventoryunitsService = inventoryunitsService;
            _statusesService = statusesService;
            _movequeuesService = movequeuesService;

            PromptValidator<string> putAwayHandlingUnitValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value.Trim().ToLower();
                if (!_handlingunitsService.IndexDb().Where(x => x.sHandlingUnit.Trim().ToLower() == value).Any())
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The handling unit {value} does not exist. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    return true;
                }
            };

            PromptValidator<string> inventoryDropLocationValidator = async (promptContext, cancellationToken) =>
            {
                var value = promptContext.Recognized.Value.Trim().ToLower();
                var currentBotUserData = await _botSpielUserStateAccessors.BotUserDataAccessor.GetAsync(promptContext.Context, () => _botUserData);

                if (!_inventorylocationsService.IndexDb().Where(x => x.sInventoryLocation.Trim().ToLower() == value && x.ixFacility == currentBotUserData.ixFacility).Any() && currentBotUserData.sPutAwaySuggestion == "No Suggestion")
                {
                    await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The inventory location {value} does not exist in the facility. Please enter a different value or exit."), cancellationToken);
                    return false;
                }
                else
                {
                    if (currentBotUserData.sPutAwaySuggestion.Trim().ToLower() != value)
                    {
                        await promptContext.Context.SendActivityAsync(MessageFactory.Text($"The inventory location {value} does not match the suggested location {currentBotUserData.sPutAwaySuggestion}. Please enter a different value or exit."), cancellationToken);
                        return false;
                    }
                    return true;
                }
            };

            //Custom Code End


            // Define the prompts used in the Dialog.
            //Custom Code Start | Replaced Code Block
            //Replaced Code Block Start
            //AddDialog(new TextPrompt(PutAwayHandlingUnitPromptId));
            //Replaced Code Block End
            AddDialog(new TextPrompt(PutAwayHandlingUnitPromptId, putAwayHandlingUnitValidator));
            //Custom Code End

            AddDialog(new TextPrompt(InventoryDropLocationPromptId, inventoryDropLocationValidator));


            // Define the conversation flow for the Dialog.
            WaterfallStep[] steps = new WaterfallStep[]
            {
             PutAwayHandlingUnitPrompt,
              InventoryDropLocationPrompt,
              donePrompt,
            };
            AddDialog(new WaterfallDialog(Id, steps));


        }
        private async Task<DialogTurnResult> PutAwayHandlingUnitPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            step.Values[DialogKey] = new PutAwayHandlingUnitsPost();
            //Custom Code Start | Added Code Block 
            step.Values[MoveQueuePostDropKey] = new MoveQueuesPost();
            //Custom Code End

            return await step.PromptAsync(
                PutAwayHandlingUnitPromptId,
                new PromptOptions
                {
                    //Custom Code Start | Replaced Code Block
                    //Replaced Code Block Start
                    //Prompt = MessageFactory.Text($"Please enter a PutAwayHandlingUnit:"),
                    //Replaced Code Block End
                    Prompt = MessageFactory.Text($"Please enter/scan a Handling Unit to Put Away:"),
                    //Custom Code End
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> InventoryDropLocationPrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sPutAwayHandlingUnit = (string)step.Result;
            ((PutAwayHandlingUnitsPost)step.Values[DialogKey]).sPutAwayHandlingUnit = sPutAwayHandlingUnit;

            //Custom Code Start | Added Code Block 
            var currentBotUserData = await _botSpielUserStateAccessors.BotUserDataAccessor.GetAsync(step.Context, () => _botUserData);
            var ixHandlingUnit = _handlingunitsService.IndexDb().Where(x => x.sHandlingUnit.Trim().ToLower() == sPutAwayHandlingUnit).Select(x => x.ixHandlingUnit).FirstOrDefault();
            ((PutAwayHandlingUnitsPost)step.Values[DialogKey]).ixHandlingUnit = ixHandlingUnit;
            //We now create and execute the move queue for the pickup
            MoveQueuesPost moveQueuePost = new MoveQueuesPost();
            moveQueuePost.ixMoveQueueType = _movequeuetypesService.IndexDb().Where(x => x.sMoveQueueType == "Consolidated Pickup - Consolidated Drop").Select(x => x.ixMoveQueueType).FirstOrDefault();
            moveQueuePost.ixMoveQueueContext = _movequeuecontextsService.IndexDb().Where(x => x.sMoveQueueContext == "Putaway").Select(x => x.ixMoveQueueContext).FirstOrDefault();
            moveQueuePost.ixSourceInventoryLocation = _inventoryunitsService.IndexDbPost().Where(x => x.ixHandlingUnit == ixHandlingUnit).Select(x => x.ixInventoryLocation).FirstOrDefault();
            moveQueuePost.ixTargetInventoryLocation = currentBotUserData.ixInventoryLocation;
            moveQueuePost.ixSourceHandlingUnit = ixHandlingUnit;
            moveQueuePost.ixTargetHandlingUnit = ixHandlingUnit;
            moveQueuePost.sPreferredResource = step.Context.Activity.Conversation.Id;
            moveQueuePost.nBaseUnitQuantity = 0;
            moveQueuePost.dtStartedAt = DateTime.Now;
            moveQueuePost.ixStatus = _statusesService.IndexDb().Where(x => x.sStatus == "Active").Select(x => x.ixStatus).FirstOrDefault();
            moveQueuePost.UserName = step.Context.Activity.Conversation.Id;
            var ixMoveQueue = await _movequeuesService.Create(moveQueuePost);
            //We now complete the move queue for the pickup
            var moveQueuePickUp = _movequeuesService.GetPost(ixMoveQueue);
            moveQueuePickUp.dtCompletedAt = DateTime.Now;
            moveQueuePickUp.ixStatus = _statusesService.IndexDb().Where(x => x.sStatus == "Complete").Select(x => x.ixStatus).FirstOrDefault();
            moveQueuePickUp.UserName = moveQueuePost.UserName;
            await _movequeuesService.Edit(moveQueuePickUp);

            var ixInventoryLocationSuggestion = _putAway.getPutAwaySuggestion(ixHandlingUnit, currentBotUserData.ixCompany, currentBotUserData.ixFacility, currentBotUserData.ixInventoryLocation);
            string sPutAwaySuggestion = "";
            if (_inventorylocationsService.IndexDb().Where(x => x.ixInventoryLocation == ixInventoryLocationSuggestion).Any())
            {
                sPutAwaySuggestion = _inventorylocationsService.IndexDb().Where(x => x.ixInventoryLocation == ixInventoryLocationSuggestion).Select(x => x.sInventoryLocation).FirstOrDefault();
                //We now create the move queue for the drop
                MoveQueuesPost moveQueuePostDrop = new MoveQueuesPost();
                moveQueuePostDrop.ixMoveQueueType = moveQueuePost.ixMoveQueueType;
                moveQueuePostDrop.ixMoveQueueContext = moveQueuePost.ixMoveQueueContext;
                moveQueuePostDrop.ixSourceInventoryLocation = currentBotUserData.ixInventoryLocation;
                moveQueuePostDrop.ixTargetInventoryLocation = ixInventoryLocationSuggestion;
                moveQueuePostDrop.ixSourceHandlingUnit = ixHandlingUnit;
                moveQueuePostDrop.ixTargetHandlingUnit = ixHandlingUnit;
                moveQueuePostDrop.sPreferredResource = step.Context.Activity.Conversation.Id;
                moveQueuePostDrop.nBaseUnitQuantity = 0;
                moveQueuePostDrop.dtStartedAt = DateTime.Now;
                moveQueuePostDrop.ixStatus = _statusesService.IndexDb().Where(x => x.sStatus == "Active").Select(x => x.ixStatus).FirstOrDefault();
                moveQueuePostDrop.UserName = step.Context.Activity.Conversation.Id;
                var ixMoveQueueDrop = await _movequeuesService.Create(moveQueuePostDrop);
                step.Values[MoveQueuePostDropKey] = moveQueuePostDrop;
            }
            else
            {
                sPutAwaySuggestion = "No Suggestion";
            }
            step.Values[PutAwaySuggestionKey] = sPutAwaySuggestion;
            currentBotUserData.sPutAwaySuggestion = sPutAwaySuggestion;
            await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(step.Context, currentBotUserData, cancellationToken);
            await _botSpielUserStateAccessors.UserState.SaveChangesAsync(step.Context);
            //Custom Code End

            return await step.PromptAsync(
                InventoryDropLocationPromptId,
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"The suggested drop location is {sPutAwaySuggestion}.{Environment.NewLine} Please enter/scan the Inventory Drop Location."),
                    RetryPrompt = MessageFactory.Text("I didn't understand. Please try again."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> donePrompt(
            WaterfallStepContext step,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var sInventoryDropLocation = (string)step.Result;
            ((PutAwayHandlingUnitsPost)step.Values[DialogKey]).sInventoryDropLocation = sInventoryDropLocation;


            if (step.Values[PutAwaySuggestionKey].ToString() == sInventoryDropLocation) //We confirm the original move queue
            {
                var moveQueueDrop = _movequeuesService.GetPost(((MoveQueuesPost)step.Values[MoveQueuePostDropKey]).ixMoveQueue);
                moveQueueDrop.dtCompletedAt = DateTime.Now;
                moveQueueDrop.ixStatus = _statusesService.IndexDb().Where(x => x.sStatus == "Complete").Select(x => x.ixStatus).FirstOrDefault();
                moveQueueDrop.UserName = step.Context.Activity.Conversation.Id;
                await _movequeuesService.Edit(moveQueueDrop);
                ((PutAwayHandlingUnitsPost)step.Values[DialogKey]).ixInventoryLocation = moveQueueDrop.ixTargetInventoryLocation ?? 0;
                ((PutAwayHandlingUnitsPost)step.Values[DialogKey]).UserName = step.Context.Activity.Conversation.Id;
            }
            else
            {
                if (step.Values[PutAwaySuggestionKey].ToString() == "No Suggestion") //We must create the move queue from scratch and confirm (Implement later)
                {

                }
                //else //We must edit the move queue from scratch and confirm (Implement later)
                //{

                //}

            }



            return await step.EndDialogAsync(
                (PutAwayHandlingUnitsPost)step.Values[DialogKey],
                cancellationToken);
        }



    }
}


