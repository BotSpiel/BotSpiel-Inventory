using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Recognizers.Text;
using Microsoft.Extensions.Logging;
using BotSpiel.DataAccess.Data;
using BotSpiel.DataAccess.Models;
using BotSpiel.Dialogs;
using BotSpiel.Services;
using BotSpiel.Services.Utilities;
//Custom Code Start | Added Code Block 
using BotSpiel.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Bot.Builder.Dialogs.Choices;
//Custom Code End


namespace BotSpiel
{
    public class BotSpielBot : IBot
    {
        // Messages sent to the user.
        private const string WelcomeMessage = @"";

        // The bot state accessor object. Use this to access specific state properties.
        private readonly BotSpielUserStateAccessors _botSpielUserStateAccessors;
        private readonly BotUserEntityContext _botUserEntityContext;
        readonly BotUserData _botUserData;
        private readonly NavigationEntityData _navigationEntityData;
    
        // Define the IDs for the dialogs in the bot's dialog set.
        private const string RootDialogId = "rootDialog";
        private const string ConfirmPromptId = "confirmDialog";
        //Custom Code Start | Added Code Block 
        private const string ChoicesPromptId = "choicesDialog";
        //Custom Code End
        private const string CreateGetPickBatchesDialogId = "createGetPickBatchesDialog";
        private const string CreatePickBatchPickingDialogId = "createPickBatchPickingDialog";
        private const string CreatePutAwayHandlingUnitsDialogId = "createPutAwayHandlingUnitsDialog";
        private const string CreateSetUpExecutionParametersDialogId = "createSetUpExecutionParametersDialog";
        private const string CreateDropInventoryUnitsDialogId = "createDropInventoryUnitsDialog";
        private readonly IDropInventoryUnitsService _dropinventoryunitsService;
        private readonly IPickBatchPickingService _pickbatchpickingService;
        private readonly IPutAwayHandlingUnitsService _putawayhandlingunitsService;
        private readonly ISetUpExecutionParametersService _setupexecutionparametersService;
        //Custom Code Start | Added Code Block 
        private readonly IHandlingUnitsService _handlingunitsService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IFacilitiesService _facilitiesService;
        private readonly PutAway _putAway;
        private readonly IInventoryLocationsService _inventorylocationsService;
        private readonly ILocationFunctionsService _locationfunctionsService;
        private readonly IMoveQueueTypesService _movequeuetypesService;
        private readonly IMoveQueueContextsService _movequeuecontextsService;
        private readonly IInventoryUnitsService _inventoryunitsService;
        private readonly IStatusesService _statusesService;
        private readonly IMoveQueuesService _movequeuesService;
        private readonly IPickBatchesService _pickbatchesService;
        private readonly CommonLookUps _commonLookUps;
        private readonly Picking _picking;
        private readonly Shipping _shipping;
        private readonly IOutboundOrderLinesInventoryAllocationService _outboundorderlinesinventoryallocationService;
        private readonly IOutboundOrderLinePackingService _outboundorderlinepackingService;
        //Custom Code End

        private readonly ILogger _logger;
        private DialogSet _dialogs;

        ModuleOptions moduleOptions;

        Int64 ixCompany;
        Int64 ixFacility;
        Int64 ixFacilityWorkArea;
        Int64 ixInventoryUnit;
        Int64 ixPickBatchPick;
        Int64 ixPutAwayHandlingUnit;
        Int64 ixSetUpExecutionParameter;
        Int64 ixDropInventoryUnit;
        //Custom Code Start | Added Code Block 
        Int64 ixInventoryLocation;
        //Custom Code End
        DropInventoryUnitsPost _dropinventoryunitsPost;
        PickBatchPickingPost _pickbatchpickingPost;
        PutAwayHandlingUnitsPost _putawayhandlingunitsPost;
        SetUpExecutionParametersPost _setupexecutionparametersPost;

        //Custom Code Start | Replaced Code Block
        //Replaced Code Block Start
        // public BotSpielBot(ILoggerFactory loggerFactory, BotSpielUserStateAccessors statePropertyAccessor, BotUserData botUserData, BotUserEntityContext botUserEntityContext, NavigationEntityData navigationEntityData
        //, PutAwayHandlingUnitsPost putawayhandlingunitsPost
        //    , IPutAwayHandlingUnitsService putawayhandlingunitsService
        // )
        //Replaced Code Block End
        public BotSpielBot(ILoggerFactory loggerFactory, BotSpielUserStateAccessors statePropertyAccessor, BotUserData botUserData, BotUserEntityContext botUserEntityContext, NavigationEntityData navigationEntityData
            , DropInventoryUnitsPost dropinventoryunitsPost
            , PickBatchPickingPost pickbatchpickingPost
            , PutAwayHandlingUnitsPost putawayhandlingunitsPost
            , SetUpExecutionParametersPost setupexecutionparametersPost
            , IPutAwayHandlingUnitsService putawayhandlingunitsService
            , ISetUpExecutionParametersService setupexecutionparametersService
            , IHandlingUnitsService handlingunitsService
            , UserManager<ApplicationUser> userManager
            , IFacilitiesService facilitiesService
            , PutAway putAway
            , IInventoryLocationsService inventorylocationsService
            , ILocationFunctionsService locationfunctionsService
            , IMoveQueueTypesService movequeuetypesService
            , IMoveQueueContextsService movequeuecontextsService
            , IInventoryUnitsService inventoryunitsService
            , IStatusesService statusesService
            , IMoveQueuesService movequeuesService
            , IPickBatchesService pickbatchesService
            , CommonLookUps commonLookUps
            , Picking picking
            , Shipping shipping
            , IPickBatchPickingService pickbatchpickingService
            , IOutboundOrderLinesInventoryAllocationService outboundorderlinesinventoryallocationService
            , IOutboundOrderLinePackingService outboundorderlinepackingService
        )
        //Custom Code End

        {
            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<BotSpielBot>();

            _botUserData = botUserData;
            _botUserEntityContext = botUserEntityContext;
            _navigationEntityData = navigationEntityData;

            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

            _dropinventoryunitsPost = dropinventoryunitsPost;
            _pickbatchpickingPost = pickbatchpickingPost;
            _putawayhandlingunitsPost = putawayhandlingunitsPost;
            _setupexecutionparametersPost = setupexecutionparametersPost;
            _putawayhandlingunitsService = putawayhandlingunitsService;
            _setupexecutionparametersService = setupexecutionparametersService;
            //Custom Code Start | Added Code Block 
            _handlingunitsService = handlingunitsService;
            _userManager = userManager;
            _facilitiesService = facilitiesService;
            _putAway = putAway;
            _inventorylocationsService = inventorylocationsService;
            _locationfunctionsService = locationfunctionsService;
            _movequeuetypesService = movequeuetypesService;
            _movequeuecontextsService = movequeuecontextsService;
            _inventoryunitsService = inventoryunitsService;
            _statusesService = statusesService;
            _movequeuesService = movequeuesService;
            _pickbatchesService = pickbatchesService;
            _commonLookUps = commonLookUps;
            _picking = picking;
            _shipping = shipping;
            _pickbatchpickingService = pickbatchpickingService;
            _outboundorderlinesinventoryallocationService = outboundorderlinesinventoryallocationService;
            _outboundorderlinepackingService = outboundorderlinepackingService;
            //Custom Code End			

            // The DialogSet needs a DialogState accessor, it will call it when it has a turn context.
            _dialogs = new DialogSet(statePropertyAccessor.DialogStateAccessor)
                .Add(new RootDialog(RootDialogId, _botUserEntityContext, _navigationEntityData))
                .Add(new CreatePickBatchPickingDialog(CreatePickBatchPickingDialogId, _pickbatchpickingService, _pickbatchpickingPost, _botSpielUserStateAccessors, _pickbatchesService, _commonLookUps, _picking, _inventoryunitsService, _movequeuesService, _handlingunitsService, _outboundorderlinesinventoryallocationService, _outboundorderlinepackingService))
                //Custom Code Start | Replaced Code Block
                //Replaced Code Block Start
                //.Add(new CreatePutAwayHandlingUnitsDialog(CreatePutAwayHandlingUnitsDialogId, _putawayhandlingunitsService, _putawayhandlingunitsPost, _botSpielUserStateAccessors))
                //Replaced Code Block End
                .Add(new CreatePutAwayHandlingUnitsDialog(
                    CreatePutAwayHandlingUnitsDialogId
                    , _putawayhandlingunitsService
                    , _putawayhandlingunitsPost
                    , _botSpielUserStateAccessors
                    , _handlingunitsService
                    , _putAway
                    , _botUserData
                    , _inventorylocationsService
                    , _movequeuetypesService
                    , _movequeuecontextsService
                    , _inventoryunitsService
                    , _statusesService
                    , _movequeuesService
                    ))
                //Custom Code End
                .Add(new CreateSetUpExecutionParametersDialog(CreateSetUpExecutionParametersDialogId, _setupexecutionparametersService, _setupexecutionparametersPost, _botSpielUserStateAccessors))
                .Add(new CreateDropInventoryUnitsDialog(CreateDropInventoryUnitsDialogId, _dropinventoryunitsService, _dropinventoryunitsPost, _botSpielUserStateAccessors, _shipping, _inventorylocationsService, _commonLookUps, _movequeuesService))
                .Add(new ConfirmPrompt(ConfirmPromptId, defaultLocale: Culture.English))
                //Custom Code Start | Added Code Block 
                .Add(new ChoicePrompt(ChoicesPromptId, defaultLocale: Culture.English))
                .Add(new CreateGetPickBatchesDialog(CreateGetPickBatchesDialogId, new GetPickBatchesPost(), _botSpielUserStateAccessors, _pickbatchesService, _commonLookUps));
            //Custom Code End
        }

        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            // use state accessor to extract the didBotWelcomeUser flag
            var didBotWelcomeUser = await _botSpielUserStateAccessors.DidBotWelcomeUser.GetAsync(turnContext, () => false);
            var currentBotUserData = await _botSpielUserStateAccessors.BotUserDataAccessor.GetAsync(turnContext, () => _botUserData);

            string conCat = "";
            List<string> existInEntities = new List<string>();
            bool DeleteOK = true;

            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                // Establish dialog state from the conversation state.
                DialogContext dc = await _dialogs.CreateContextAsync(turnContext, cancellationToken);

                //Custom Code Start | Added Code Block 
                if (turnContext.Activity.Text.ToLowerInvariant() == "cancel")
                {
                    await dc.CancelAllDialogsAsync(cancellationToken);
                }
                //Custom Code End

                // Continue any current dialog.
                DialogTurnResult dialogTurnResult = await dc.ContinueDialogAsync();

                // Process the result of any complete dialog.
                if (dialogTurnResult.Status is DialogTurnStatus.Complete)
                {
                    switch (dialogTurnResult.Result)
                    {
                        case BotUserEntityContext botUserEntityContext:
                            if (botUserEntityContext.module != "Choose an area")
                            {
                                // Store the results of the root dialog.
                                currentBotUserData.botUserEntityContext = botUserEntityContext;
                                await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                await dc.PromptAsync(ConfirmPromptId, new PromptOptions { Prompt = MessageFactory.Text($"Please confirm that you want to {botUserEntityContext.entityIntent} {botUserEntityContext.entity}. Is that correct?") }, cancellationToken);
                            }
                            else
                            {
                                await turnContext.SendActivityAsync("OK, Let's choose a different area.", cancellationToken: cancellationToken);
                                await dc.BeginDialogAsync(RootDialogId, null, cancellationToken);
                            }
                            break;
                        case bool botYesNo:
                            if (botYesNo)
                            {
                                switch (currentBotUserData.botUserEntityContext.entity)
                                {
                                    case "PickBatchPicking":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                        {
                                            //Custom Code Start | Replaced Code Block
                                            //Replaced Code Block Start
                                            //await dc.BeginDialogAsync(CreatePickBatchPickingDialogId, null, cancellationToken);
                                            //Replaced Code Block End
                                            await dc.BeginDialogAsync(CreatePickBatchPickingDialogId, new PickBatchPickingPost(), cancellationToken);
                                            //Custom Code End                                            
                                        }
                                        break;
                                    case "PutAwayHandlingUnits":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreatePutAwayHandlingUnitsDialogId, null, cancellationToken);
                                            }
                                        break;
                                    case "SetUpExecutionParameters":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                        {
                                            await dc.BeginDialogAsync(CreateSetUpExecutionParametersDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "DropInventoryUnits":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                        {
                                            await dc.BeginDialogAsync(CreateDropInventoryUnitsDialogId, null, cancellationToken);
                                        }
                                        break;
                                    default:
                                        // We shouldn't get here.
                                        break;

                                }
                            }
                            else
                            {
                                await turnContext.SendActivityAsync(MessageFactory.Text("OK, let's try again."), cancellationToken);
                                await dc.BeginDialogAsync(RootDialogId, null, cancellationToken);
                            }
                            break;
                        case GetPickBatchesPost getpickbatchesPost:
                            if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                            {
                                //Custom Code Start | Removed Block 
                                //ixGetPickBatch = await _getpickbatchesService.Create(getpickbatchesPost);
                                //await turnContext.SendActivityAsync(MessageFactory.Text($"The GetPickBatch {ixGetPickBatch} was created"), cancellationToken);
                                //currentBotUserData.ixGetPickBatch = ixGetPickBatch;
                                //Custom Code End			

                                await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                //await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                //Custom Code Start | Added Code Block 
                                //We can now set the pick batch status to started
                                var pickbatch = _pickbatchesService.GetPost(_pickbatchesService.IndexDb().Where(x => x.sPickBatch == getpickbatchesPost.sGetPickBatch).Select(x => x.ixPickBatch).FirstOrDefault());
                                pickbatch.ixStatus = _commonLookUps.getStatuses().Where(x => x.sStatus == "Started").Select(x => x.ixStatus).FirstOrDefault();
                                pickbatch.UserName = dc.Context.Activity.Conversation.Id;
                                await _pickbatchesService.Edit(pickbatch);
                                _pickbatchpickingPost.sPickBatchPick = getpickbatchesPost.sGetPickBatch;
                                await dc.BeginDialogAsync(CreatePickBatchPickingDialogId, _pickbatchpickingPost, cancellationToken);
                                //Custom Code End

                            }
                            break;
                        case PickBatchPickingPost pickbatchpickingPost:
                            if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                            {
                                ixPickBatchPick = await _pickbatchpickingService.Create(pickbatchpickingPost);
                                //Custom Code Start | Removed Block 
                                //await turnContext.SendActivityAsync(MessageFactory.Text($"The PickBatchPick {ixPickBatchPick} was created"), cancellationToken);
                                //Custom Code End			
                                //Custom Code Start | Added Code Block 
                                //We check if the batch is complete 
                                if (!_picking.isPickBatchComplete(pickbatchpickingPost.ixPickBatch))
                                {
                                    currentBotUserData.ixPickBatchPick = ixPickBatchPick;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(CreatePickBatchPickingDialogId, pickbatchpickingPost, cancellationToken);
                                }
                                else
                                {
                                    currentBotUserData.ixPickBatchPick = 0;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    //await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    //We can now set the pick batch status to complete
                                    var pickbatch = _pickbatchesService.GetPost(pickbatchpickingPost.ixPickBatch);
                                    pickbatch.ixStatus = _commonLookUps.getStatuses().Where(x => x.sStatus == "Complete").Select(x => x.ixStatus).FirstOrDefault();
                                    pickbatch.UserName = dc.Context.Activity.Conversation.Id;
                                    await _pickbatchesService.Edit(pickbatch);
                                    //We begin the drop cycle
                                    await dc.BeginDialogAsync(CreateDropInventoryUnitsDialogId, pickbatchpickingPost, cancellationToken);
                                }
                                //Custom Code End
                            }
                            break;
                        case PutAwayHandlingUnitsPost putawayhandlingunitsPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixPutAwayHandlingUnit = await _putawayhandlingunitsService.Create(putawayhandlingunitsPost);
                                    //Custom Code Start | Removed Block 
                                    //await turnContext.SendActivityAsync(MessageFactory.Text($"The PutAwayHandlingUnit {ixPutAwayHandlingUnit} was created"), cancellationToken);
                                    //Custom Code End			
                                    currentBotUserData.ixPutAwayHandlingUnit = ixPutAwayHandlingUnit;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    //Custom Code Start | Replaced Code Block
                                    //Replaced Code Block Start
                                    //await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    //Replaced Code Block End
                                    await dc.BeginDialogAsync(CreatePutAwayHandlingUnitsDialogId, null, cancellationToken);
                                    //Custom Code End
                            }
                            break;
                        case SetUpExecutionParametersPost setupexecutionparametersPost:
                            if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                            {
                                //Custom Code Start | Removed Block 
                                //ixSetUpExecutionParameter = await _setupexecutionparametersService.Create(setupexecutionparametersPost);
                                //await turnContext.SendActivityAsync(MessageFactory.Text($"The SetUpExecutionParameter {ixSetUpExecutionParameter} was created"), cancellationToken);
                                //currentBotUserData.ixSetUpExecutionParameter = ixSetUpExecutionParameter;
                                //Custom Code End			
                                //Custom Code Start | Added Code Block 
                                currentBotUserData.ixFacility = setupexecutionparametersPost.ixFacility;
                                currentBotUserData.ixCompany = setupexecutionparametersPost.ixCompany;
                                currentBotUserData.ixFacilityWorkArea = setupexecutionparametersPost.ixFacilityWorkArea;
                                //We check if the user location exists - if not we create it
                                if (_userManager.Users.Where(x => x.UserName == dc.Context.Activity.Conversation.Id).Any())
                                {
                                    if (!_inventorylocationsService.IndexDb().Where(x => x.sInventoryLocation.ToLower().Trim() == dc.Context.Activity.Conversation.Id.ToLower().Trim()).Any())
                                    {
                                        InventoryLocationsPost inventoryLocationsPost = new InventoryLocationsPost();
                                        inventoryLocationsPost.sInventoryLocation = dc.Context.Activity.Conversation.Id.ToLower().Trim();
                                        inventoryLocationsPost.ixLocationFunction = _locationfunctionsService.IndexDb().Where(x => x.sLocationFunctionCode == "PE").Select(x => x.ixLocationFunction).FirstOrDefault();
                                        inventoryLocationsPost.ixFacility = currentBotUserData.ixFacility;
                                        inventoryLocationsPost.ixFacilityFloor = _inventorylocationsService.FacilityFloorsDb().Select(x => x.ixFacilityFloor).FirstOrDefault();
                                        inventoryLocationsPost.ixFacilityZone = _inventorylocationsService.FacilityZonesDb().Select(x => x.ixFacilityZone).FirstOrDefault();
                                        inventoryLocationsPost.ixFacilityWorkArea = _inventorylocationsService.FacilityWorkAreasDb().Select(x => x.ixFacilityWorkArea).FirstOrDefault();
                                        inventoryLocationsPost.ixFacilityAisleFace = _inventorylocationsService.FacilityAisleFacesDb().Select(x => x.ixFacilityAisleFace).FirstOrDefault();
                                        inventoryLocationsPost.nSequence = 0;
                                        inventoryLocationsPost.bTrackUtilisation = false;
                                        currentBotUserData.ixInventoryLocation = await _inventorylocationsService.Create(inventoryLocationsPost);
                                    }
                                    else
                                    {
                                        currentBotUserData.ixInventoryLocation = _inventorylocationsService.IndexDb().Where(x => x.sInventoryLocation.ToLower().Trim() == dc.Context.Activity.Conversation.Id.ToLower().Trim()).Select(x => x.ixInventoryLocation).FirstOrDefault();
                                        var inventoryLocationsPost = _inventorylocationsService.GetPost(currentBotUserData.ixInventoryLocation);
                                        inventoryLocationsPost.ixFacility = currentBotUserData.ixFacility;
                                        await _inventorylocationsService.Edit(inventoryLocationsPost);
                                    }
                                }
                                //Custom Code End
                                await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                //Custom Code Start | Removed Block 
                                //await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                //Custom Code End			
                                //Custom Code Start | Added Code Block 
                                await turnContext.SendActivityAsync(MessageFactory.Text($"The setup parameters have been updated. Say/type putaway, pick or cancel."), cancellationToken);
                                //Custom Code End
                            }
                            break;
                        case DropInventoryUnitsPost dropinventoryunitsPost:
                            if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                            {
                                //Custom Code Start | Removed Block 
                                //ixDropInventoryUnit = await _dropinventoryunitsService.Create(dropinventoryunitsPost);
                                //await turnContext.SendActivityAsync(MessageFactory.Text($"The DropInventoryUnit {ixDropInventoryUnit} was created"), cancellationToken);
                                //currentBotUserData.ixDropInventoryUnit = ixDropInventoryUnit;
                                //await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                //await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                //await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                //Custom Code End			
                                await dc.BeginDialogAsync(CreateGetPickBatchesDialogId, null, cancellationToken);
                            }
                            break;
                        default:
                            // We shouldn't get here.
                            break;
                    }
                }

                // Proactively send a welcome message to a personal chat the first time
                // (and only the first time) a user initiates a personal chat.
                //Custom Code Start | Replaced Code Block
                //Replaced Code Block Start
                //if (didBotWelcomeUser == false)
                //Replaced Code Block End
                if (true == false)
                //Custom Code End
                {
                    // Update user state flag to reflect bot handled first user interaction.
                    await _botSpielUserStateAccessors.DidBotWelcomeUser.SetAsync(turnContext, true);
                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);

                    // the channel should sends the user name in the 'From' object
                    var userName = turnContext.Activity.From.Name;

                    // We give the user the opportunity to say or request something using natural language and funnel through recognizer
                    await turnContext.SendActivityAsync($"What would like to do? You can say things like ... or help me.", cancellationToken: cancellationToken);
                }
                //Custom Code Start | Added Code Block 
                else if (!currentBotUserData.bIsInitialSetUpParametersSet)
                {
                    currentBotUserData.bIsInitialSetUpParametersSet = true;
                    currentBotUserData.botUserEntityContext.module = "Execution";
                    currentBotUserData.botUserEntityContext.entity = "SetUpExecutionParameters";
                    currentBotUserData.botUserEntityContext.entityIntent = "Create";
                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                    await dc.BeginDialogAsync(CreateSetUpExecutionParametersDialogId, null, cancellationToken);
                }
                //Custom Code End
                else if ((dialogTurnResult.Status is DialogTurnStatus.Empty) || turnContext.Activity.Text.ToLowerInvariant() == "putaway" || turnContext.Activity.Text.ToLowerInvariant() == "cancel")
                {

                    var text = turnContext.Activity.Text.ToLowerInvariant();

                    // Now attempt to infer the context (NLP)
                    // Placeholder for code to added

                    switch (text)
                    {
                        //Custom Code Start | Removed Block 
                        //case "help me":
                        //    await turnContext.SendActivityAsync($"You said: {text}.", cancellationToken: cancellationToken);
                        //    break;
                        //Custom Code End		
                        //Custom Code Start | Added Code Block 
                        case "putaway":
                            currentBotUserData.botUserEntityContext.module = "Execution";
                            currentBotUserData.botUserEntityContext.entity = "PutAwayHandlingUnits";
                            currentBotUserData.botUserEntityContext.entityIntent = "Create";
                            await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                            await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                            await dc.BeginDialogAsync(CreatePutAwayHandlingUnitsDialogId, null, cancellationToken);
                            break;
                        case "pick":
                            currentBotUserData.botUserEntityContext.module = "Execution";
                            currentBotUserData.botUserEntityContext.entity = "PickBatchPicking";
                            currentBotUserData.botUserEntityContext.entityIntent = "Create";
                            await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                            await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                            //await dc.BeginDialogAsync(CreatePickBatchPickingDialogId, new PickBatchPickingPost(), cancellationToken);
                            await dc.BeginDialogAsync(CreateGetPickBatchesDialogId, null, cancellationToken);
                            break;
                        case "cancel":
                            await dc.CancelAllDialogsAsync(cancellationToken);
                            break;
                        //Custom Code End                        
                        default:
                            if (dc.ActiveDialog == null && (dialogTurnResult.Status is DialogTurnStatus.Complete || dialogTurnResult.Status is DialogTurnStatus.Empty || dialogTurnResult.Status is DialogTurnStatus.Cancelled))
                            {
                                await turnContext.SendActivityAsync("I do not understand, let's try something different.", cancellationToken: cancellationToken);
                                await dc.BeginDialogAsync(RootDialogId, null, cancellationToken);
                            }
                            break;
                    }
                }
            }
            else if (turnContext.Activity.Type == ActivityTypes.ConversationUpdate)
            {
                if (turnContext.Activity.MembersAdded.Any())
                {
                    // Iterate over all new members added to the conversation
                    foreach (var member in turnContext.Activity.MembersAdded)
                    {
                        if (member.Id != turnContext.Activity.Recipient.Id)
                        {
                            await turnContext.SendActivityAsync($"Hi there - {member.Name}. {WelcomeMessage}", cancellationToken: cancellationToken);
                        }
                    }
                }
            }
            else
            {
                // Default behaviour for all other type of activities.
                await turnContext.SendActivityAsync($"{turnContext.Activity.Type} activity detected");
            }

            // save any state changes made to your state objects.
            await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
            await _botSpielUserStateAccessors.ConversationState.SaveChangesAsync(turnContext);

        }

    }
}







