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

namespace BotSpiel
{
    public class BotSpielBot : IBot
    {
        //Custom Code Start | Added Code Block
        private readonly IInventoryUnitTransactionContextsService _inventoryunittransactioncontextsService;
        //Custom Code End

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
       private const string CreatePeopleDialogId = "createPeopleDialog";
        private const string DeletePeopleDialogId = "deletePeopleDialog";
        private const string EditPeopleDialogId = "editPeopleDialog";
        private const string FindPeopleDialogId = "findPeopleDialog";
        private const string CreateAddressesDialogId = "createAddressesDialog";
        private const string DeleteAddressesDialogId = "deleteAddressesDialog";
        private const string EditAddressesDialogId = "editAddressesDialog";
        private const string FindAddressesDialogId = "findAddressesDialog";
        private const string CreateCompaniesDialogId = "createCompaniesDialog";
        private const string DeleteCompaniesDialogId = "deleteCompaniesDialog";
        private const string EditCompaniesDialogId = "editCompaniesDialog";
        private const string FindCompaniesDialogId = "findCompaniesDialog";
        private const string CreateFacilitiesDialogId = "createFacilitiesDialog";
        private const string DeleteFacilitiesDialogId = "deleteFacilitiesDialog";
        private const string EditFacilitiesDialogId = "editFacilitiesDialog";
        private const string FindFacilitiesDialogId = "findFacilitiesDialog";
        private const string CreateFacilityZonesDialogId = "createFacilityZonesDialog";
        private const string DeleteFacilityZonesDialogId = "deleteFacilityZonesDialog";
        private const string EditFacilityZonesDialogId = "editFacilityZonesDialog";
        private const string FindFacilityZonesDialogId = "findFacilityZonesDialog";
        private const string CreateFacilityWorkAreasDialogId = "createFacilityWorkAreasDialog";
        private const string DeleteFacilityWorkAreasDialogId = "deleteFacilityWorkAreasDialog";
        private const string EditFacilityWorkAreasDialogId = "editFacilityWorkAreasDialog";
        private const string FindFacilityWorkAreasDialogId = "findFacilityWorkAreasDialog";
        private const string CreateFacilityFloorsDialogId = "createFacilityFloorsDialog";
        private const string DeleteFacilityFloorsDialogId = "deleteFacilityFloorsDialog";
        private const string EditFacilityFloorsDialogId = "editFacilityFloorsDialog";
        private const string FindFacilityFloorsDialogId = "findFacilityFloorsDialog";
        private const string CreateFacilityAisleFacesDialogId = "createFacilityAisleFacesDialog";
        private const string DeleteFacilityAisleFacesDialogId = "deleteFacilityAisleFacesDialog";
        private const string EditFacilityAisleFacesDialogId = "editFacilityAisleFacesDialog";
        private const string FindFacilityAisleFacesDialogId = "findFacilityAisleFacesDialog";
        private const string CreateInventoryLocationSizesDialogId = "createInventoryLocationSizesDialog";
        private const string DeleteInventoryLocationSizesDialogId = "deleteInventoryLocationSizesDialog";
        private const string EditInventoryLocationSizesDialogId = "editInventoryLocationSizesDialog";
        private const string FindInventoryLocationSizesDialogId = "findInventoryLocationSizesDialog";
        private const string CreateMaterialsDialogId = "createMaterialsDialog";
        private const string DeleteMaterialsDialogId = "deleteMaterialsDialog";
        private const string EditMaterialsDialogId = "editMaterialsDialog";
        private const string FindMaterialsDialogId = "findMaterialsDialog";
        private const string CreateInventoryUnitsDialogId = "createInventoryUnitsDialog";
        private const string DeleteInventoryUnitsDialogId = "deleteInventoryUnitsDialog";
        private const string EditInventoryUnitsDialogId = "editInventoryUnitsDialog";
        private const string FindInventoryUnitsDialogId = "findInventoryUnitsDialog";
        private const string CreateHandlingUnitsDialogId = "createHandlingUnitsDialog";
        private const string DeleteHandlingUnitsDialogId = "deleteHandlingUnitsDialog";
        private const string EditHandlingUnitsDialogId = "editHandlingUnitsDialog";
        private const string FindHandlingUnitsDialogId = "findHandlingUnitsDialog";
        private const string CreateInventoryLocationsDialogId = "createInventoryLocationsDialog";
        private const string DeleteInventoryLocationsDialogId = "deleteInventoryLocationsDialog";
        private const string EditInventoryLocationsDialogId = "editInventoryLocationsDialog";
        private const string FindInventoryLocationsDialogId = "findInventoryLocationsDialog";
        private const string CreateMoveQueuesDialogId = "createMoveQueuesDialog";
        private const string DeleteMoveQueuesDialogId = "deleteMoveQueuesDialog";
        private const string EditMoveQueuesDialogId = "editMoveQueuesDialog";
        private const string FindMoveQueuesDialogId = "findMoveQueuesDialog";
        private const string CreateMaterialHandlingUnitConfigurationsDialogId = "createMaterialHandlingUnitConfigurationsDialog";
        private const string DeleteMaterialHandlingUnitConfigurationsDialogId = "deleteMaterialHandlingUnitConfigurationsDialog";
        private const string EditMaterialHandlingUnitConfigurationsDialogId = "editMaterialHandlingUnitConfigurationsDialog";
        private const string FindMaterialHandlingUnitConfigurationsDialogId = "findMaterialHandlingUnitConfigurationsDialog";
        private const string CreateBusinessPartnersDialogId = "createBusinessPartnersDialog";
        private const string DeleteBusinessPartnersDialogId = "deleteBusinessPartnersDialog";
        private const string EditBusinessPartnersDialogId = "editBusinessPartnersDialog";
        private const string FindBusinessPartnersDialogId = "findBusinessPartnersDialog";
        private const string CreateInboundOrdersDialogId = "createInboundOrdersDialog";
        private const string DeleteInboundOrdersDialogId = "deleteInboundOrdersDialog";
        private const string EditInboundOrdersDialogId = "editInboundOrdersDialog";
        private const string FindInboundOrdersDialogId = "findInboundOrdersDialog";
        private const string CreateInboundOrderLinesDialogId = "createInboundOrderLinesDialog";
        private const string DeleteInboundOrderLinesDialogId = "deleteInboundOrderLinesDialog";
        private const string EditInboundOrderLinesDialogId = "editInboundOrderLinesDialog";
        private const string FindInboundOrderLinesDialogId = "findInboundOrderLinesDialog";
        private const string CreateCarriersDialogId = "createCarriersDialog";
        private const string DeleteCarriersDialogId = "deleteCarriersDialog";
        private const string EditCarriersDialogId = "editCarriersDialog";
        private const string FindCarriersDialogId = "findCarriersDialog";
        private const string CreateCarrierServicesDialogId = "createCarrierServicesDialog";
        private const string DeleteCarrierServicesDialogId = "deleteCarrierServicesDialog";
        private const string EditCarrierServicesDialogId = "editCarrierServicesDialog";
        private const string FindCarrierServicesDialogId = "findCarrierServicesDialog";
        private const string CreateOutboundOrdersDialogId = "createOutboundOrdersDialog";
        private const string DeleteOutboundOrdersDialogId = "deleteOutboundOrdersDialog";
        private const string EditOutboundOrdersDialogId = "editOutboundOrdersDialog";
        private const string FindOutboundOrdersDialogId = "findOutboundOrdersDialog";
        private const string CreateOutboundShipmentsDialogId = "createOutboundShipmentsDialog";
        private const string DeleteOutboundShipmentsDialogId = "deleteOutboundShipmentsDialog";
        private const string EditOutboundShipmentsDialogId = "editOutboundShipmentsDialog";
        private const string FindOutboundShipmentsDialogId = "findOutboundShipmentsDialog";
        private const string CreateOutboundCarrierManifestsDialogId = "createOutboundCarrierManifestsDialog";
        private const string DeleteOutboundCarrierManifestsDialogId = "deleteOutboundCarrierManifestsDialog";
        private const string EditOutboundCarrierManifestsDialogId = "editOutboundCarrierManifestsDialog";
        private const string FindOutboundCarrierManifestsDialogId = "findOutboundCarrierManifestsDialog";
        private const string CreateOutboundOrderLinesDialogId = "createOutboundOrderLinesDialog";
        private const string DeleteOutboundOrderLinesDialogId = "deleteOutboundOrderLinesDialog";
        private const string EditOutboundOrderLinesDialogId = "editOutboundOrderLinesDialog";
        private const string FindOutboundOrderLinesDialogId = "findOutboundOrderLinesDialog";
        private const string CreatePickBatchesDialogId = "createPickBatchesDialog";
        private const string DeletePickBatchesDialogId = "deletePickBatchesDialog";
        private const string EditPickBatchesDialogId = "editPickBatchesDialog";
        private const string FindPickBatchesDialogId = "findPickBatchesDialog";
        private const string CreateReceivingDialogId = "createReceivingDialog";
        private const string DeleteReceivingDialogId = "deleteReceivingDialog";
        private const string EditReceivingDialogId = "editReceivingDialog";
        private const string FindReceivingDialogId = "findReceivingDialog";
        private const string CreateOutboundCarrierManifestPickupsDialogId = "createOutboundCarrierManifestPickupsDialog";
        private const string DeleteOutboundCarrierManifestPickupsDialogId = "deleteOutboundCarrierManifestPickupsDialog";
        private const string EditOutboundCarrierManifestPickupsDialogId = "editOutboundCarrierManifestPickupsDialog";
        private const string FindOutboundCarrierManifestPickupsDialogId = "findOutboundCarrierManifestPickupsDialog";
        private const string CreateInventoryLocationsSlottingDialogId = "createInventoryLocationsSlottingDialog";
        private const string DeleteInventoryLocationsSlottingDialogId = "deleteInventoryLocationsSlottingDialog";
        private const string EditInventoryLocationsSlottingDialogId = "editInventoryLocationsSlottingDialog";
        private const string FindInventoryLocationsSlottingDialogId = "findInventoryLocationsSlottingDialog";
       private readonly IAddressesService _addressesService;
        private readonly IBusinessPartnersService _businesspartnersService;
        private readonly ICarriersService _carriersService;
        private readonly ICarrierServicesService _carrierservicesService;
        private readonly ICompaniesService _companiesService;
        private readonly IFacilitiesService _facilitiesService;
        private readonly IFacilityAisleFacesService _facilityaislefacesService;
        private readonly IFacilityFloorsService _facilityfloorsService;
        private readonly IFacilityWorkAreasService _facilityworkareasService;
        private readonly IFacilityZonesService _facilityzonesService;
        private readonly IHandlingUnitsService _handlingunitsService;
        private readonly IInboundOrderLinesService _inboundorderlinesService;
        private readonly IInboundOrdersService _inboundordersService;
        private readonly IInventoryLocationsService _inventorylocationsService;
        private readonly IInventoryLocationSizesService _inventorylocationsizesService;
        private readonly IInventoryLocationsSlottingService _inventorylocationsslottingService;
        private readonly IInventoryUnitsService _inventoryunitsService;
        private readonly IMaterialHandlingUnitConfigurationsService _materialhandlingunitconfigurationsService;
        private readonly IMaterialsService _materialsService;
        private readonly IMoveQueuesService _movequeuesService;
        private readonly IOutboundCarrierManifestPickupsService _outboundcarriermanifestpickupsService;
        private readonly IOutboundCarrierManifestsService _outboundcarriermanifestsService;
        private readonly IOutboundOrderLinesService _outboundorderlinesService;
        private readonly IOutboundOrdersService _outboundordersService;
        private readonly IOutboundShipmentsService _outboundshipmentsService;
        private readonly IPeopleService _peopleService;
        private readonly IPickBatchesService _pickbatchesService;
        private readonly IReceivingService _receivingService;


        private readonly ILogger _logger;
        private DialogSet _dialogs;

        ModuleOptions moduleOptions;

	Int64  ixCountry;
		Int64  ixLocationFunction;
		Int64  ixStatus;
		Int64  ixLanguage;
		Int64  ixPerson;
		Int64  ixAddress;
		Int64  ixStateOrProvince;
		Int64  ixCompany;
		Int64  ixFacility;
		Int64  ixFacilityZone;
		Int64  ixFacilityWorkArea;
		Int64  ixFacilityFloor;
		Int64  ixFacilityAisleFace;
		Int64  ixBaySequenceType;
		Int64  ixPairedAisleFace;
		Int64  ixLogicalOrientation;
		Int64  ixAisleFaceStorageType;
		Int64  ixXOffsetUnit;
		Int64  ixYOffsetUnit;
		Int64  ixZOffsetUnit;
		Int64  ixDefaultFacilityZone;
		Int64  ixDefaultLocationFunction;
		Int64  ixInventoryLocationSize;
		Int64  ixLengthUnit;
		Int64  ixWidthUnit;
		Int64  ixHeightUnit;
		Int64  ixUsableVolumeUnit;
		Int64  ixDefaultInventoryLocationSize;
		Int64  ixMaterial;
		Int64  ixMaterialType;
		Int64  ixBaseUnit;
		Int64  ixDensityUnit;
		Int64  ixShelflifeUnit;
		Int64  ixInventoryUnit;
		Int64  ixInventoryState;
		Int64  ixHandlingUnit;
		Int64  ixHandlingUnitType;
		Int64  ixParentHandlingUnit;
		Int64  ixWeightUnit;
		Int64  ixInventoryLocation;
		Int64  ixMoveQueueContext;
		Int64  ixMoveQueue;
		Int64  ixMoveQueueType;
		Int64  ixSourceInventoryLocation;
		Int64  ixTargetInventoryLocation;
		Int64  ixSourceInventoryUnit;
		Int64  ixTargetInventoryUnit;
		Int64  ixSourceHandlingUnit;
		Int64  ixTargetHandlingUnit;
		Int64  ixMaterialHandlingUnitConfiguration;
		Int64  ixBusinessPartnerType;
		Int64  ixBusinessPartner;
		Int64  ixInboundOrderType;
		Int64  ixInboundOrder;
		Int64  ixInboundOrderLine;
		Int64  ixOutboundOrderType;
		Int64  ixCarrier;
		Int64  ixCarrierType;
		Int64  ixCarrierService;
		Int64  ixOutboundOrder;
		Int64  ixOutboundShipment;
		Int64  ixOutboundCarrierManifest;
		Int64  ixPickupInventoryLocation;
		Int64  ixOutboundOrderLine;
		Int64  ixPickBatchType;
		Int64  ixPickBatch;
		Int64  ixReceipt;
		Int64  ixOutboundCarrierManifestPickup;
		Int64  ixInventoryLocationSlotting;
		Int64  ixPackingMaterial;
       AddressesPost _addressesPost;
        BusinessPartnersPost _businesspartnersPost;
        CarriersPost _carriersPost;
        CarrierServicesPost _carrierservicesPost;
        CompaniesPost _companiesPost;
        FacilitiesPost _facilitiesPost;
        FacilityAisleFacesPost _facilityaislefacesPost;
        FacilityFloorsPost _facilityfloorsPost;
        FacilityWorkAreasPost _facilityworkareasPost;
        FacilityZonesPost _facilityzonesPost;
        HandlingUnitsPost _handlingunitsPost;
        InboundOrderLinesPost _inboundorderlinesPost;
        InboundOrdersPost _inboundordersPost;
        InventoryLocationsPost _inventorylocationsPost;
        InventoryLocationSizesPost _inventorylocationsizesPost;
        InventoryLocationsSlottingPost _inventorylocationsslottingPost;
        InventoryUnitsPost _inventoryunitsPost;
        MaterialHandlingUnitConfigurationsPost _materialhandlingunitconfigurationsPost;
        MaterialsPost _materialsPost;
        MoveQueuesPost _movequeuesPost;
        OutboundCarrierManifestPickupsPost _outboundcarriermanifestpickupsPost;
        OutboundCarrierManifestsPost _outboundcarriermanifestsPost;
        OutboundOrderLinesPost _outboundorderlinesPost;
        OutboundOrdersPost _outboundordersPost;
        OutboundShipmentsPost _outboundshipmentsPost;
        PeoplePost _peoplePost;
        PickBatchesPost _pickbatchesPost;
        ReceivingPost _receivingPost;


        public BotSpielBot(ILoggerFactory loggerFactory, BotSpielUserStateAccessors statePropertyAccessor, BotUserData botUserData, BotUserEntityContext botUserEntityContext, NavigationEntityData navigationEntityData
       ,AddressesPost addressesPost
        ,BusinessPartnersPost businesspartnersPost
        ,CarriersPost carriersPost
        ,CarrierServicesPost carrierservicesPost
        ,CompaniesPost companiesPost
        ,FacilitiesPost facilitiesPost
        ,FacilityAisleFacesPost facilityaislefacesPost
        ,FacilityFloorsPost facilityfloorsPost
        ,FacilityWorkAreasPost facilityworkareasPost
        ,FacilityZonesPost facilityzonesPost
        ,HandlingUnitsPost handlingunitsPost
        ,InboundOrderLinesPost inboundorderlinesPost
        ,InboundOrdersPost inboundordersPost
        ,InventoryLocationsPost inventorylocationsPost
        ,InventoryLocationSizesPost inventorylocationsizesPost
        ,InventoryLocationsSlottingPost inventorylocationsslottingPost
        ,InventoryUnitsPost inventoryunitsPost
        ,MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurationsPost
        ,MaterialsPost materialsPost
        ,MoveQueuesPost movequeuesPost
        ,OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost
        ,OutboundCarrierManifestsPost outboundcarriermanifestsPost
        ,OutboundOrderLinesPost outboundorderlinesPost
        ,OutboundOrdersPost outboundordersPost
        ,OutboundShipmentsPost outboundshipmentsPost
        ,PeoplePost peoplePost
        ,PickBatchesPost pickbatchesPost
        ,ReceivingPost receivingPost
           ,IAddressesService addressesService
            ,IBusinessPartnersService businesspartnersService
            ,ICarriersService carriersService
            ,ICarrierServicesService carrierservicesService
            ,ICompaniesService companiesService
            ,IFacilitiesService facilitiesService
            ,IFacilityAisleFacesService facilityaislefacesService
            ,IFacilityFloorsService facilityfloorsService
            ,IFacilityWorkAreasService facilityworkareasService
            ,IFacilityZonesService facilityzonesService
            ,IHandlingUnitsService handlingunitsService
            ,IInboundOrderLinesService inboundorderlinesService
            ,IInboundOrdersService inboundordersService
            ,IInventoryLocationsService inventorylocationsService
            ,IInventoryLocationSizesService inventorylocationsizesService
            ,IInventoryLocationsSlottingService inventorylocationsslottingService
            ,IInventoryUnitsService inventoryunitsService
            ,IMaterialHandlingUnitConfigurationsService materialhandlingunitconfigurationsService
            ,IMaterialsService materialsService
            ,IMoveQueuesService movequeuesService
            ,IOutboundCarrierManifestPickupsService outboundcarriermanifestpickupsService
            ,IOutboundCarrierManifestsService outboundcarriermanifestsService
            ,IOutboundOrderLinesService outboundorderlinesService
            ,IOutboundOrdersService outboundordersService
            ,IOutboundShipmentsService outboundshipmentsService
            ,IPeopleService peopleService
            ,IPickBatchesService pickbatchesService
            ,IReceivingService receivingService
            //Custom Code Start | Added Code Block
            , IInventoryUnitTransactionContextsService inventoryunittransactioncontextsService
            //Custom Code End
        )
        {
            //Custom Code Start | Added Code Block
            _inventoryunittransactioncontextsService = inventoryunittransactioncontextsService;
            //Custom Code End

            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<BotSpielBot>();

            _botUserData = botUserData;
            _botUserEntityContext = botUserEntityContext;
            _navigationEntityData = navigationEntityData;

            _botSpielUserStateAccessors = statePropertyAccessor ?? throw new System.ArgumentNullException("state accessor can't be null");

       _addressesPost = addressesPost;
        _businesspartnersPost = businesspartnersPost;
        _carriersPost = carriersPost;
        _carrierservicesPost = carrierservicesPost;
        _companiesPost = companiesPost;
        _facilitiesPost = facilitiesPost;
        _facilityaislefacesPost = facilityaislefacesPost;
        _facilityfloorsPost = facilityfloorsPost;
        _facilityworkareasPost = facilityworkareasPost;
        _facilityzonesPost = facilityzonesPost;
        _handlingunitsPost = handlingunitsPost;
        _inboundorderlinesPost = inboundorderlinesPost;
        _inboundordersPost = inboundordersPost;
        _inventorylocationsPost = inventorylocationsPost;
        _inventorylocationsizesPost = inventorylocationsizesPost;
        _inventorylocationsslottingPost = inventorylocationsslottingPost;
        _inventoryunitsPost = inventoryunitsPost;
        _materialhandlingunitconfigurationsPost = materialhandlingunitconfigurationsPost;
        _materialsPost = materialsPost;
        _movequeuesPost = movequeuesPost;
        _outboundcarriermanifestpickupsPost = outboundcarriermanifestpickupsPost;
        _outboundcarriermanifestsPost = outboundcarriermanifestsPost;
        _outboundorderlinesPost = outboundorderlinesPost;
        _outboundordersPost = outboundordersPost;
        _outboundshipmentsPost = outboundshipmentsPost;
        _peoplePost = peoplePost;
        _pickbatchesPost = pickbatchesPost;
        _receivingPost = receivingPost;
           _addressesService = addressesService;
            _businesspartnersService = businesspartnersService;
            _carriersService = carriersService;
            _carrierservicesService = carrierservicesService;
            _companiesService = companiesService;
            _facilitiesService = facilitiesService;
            _facilityaislefacesService = facilityaislefacesService;
            _facilityfloorsService = facilityfloorsService;
            _facilityworkareasService = facilityworkareasService;
            _facilityzonesService = facilityzonesService;
            _handlingunitsService = handlingunitsService;
            _inboundorderlinesService = inboundorderlinesService;
            _inboundordersService = inboundordersService;
            _inventorylocationsService = inventorylocationsService;
            _inventorylocationsizesService = inventorylocationsizesService;
            _inventorylocationsslottingService = inventorylocationsslottingService;
            _inventoryunitsService = inventoryunitsService;
            _materialhandlingunitconfigurationsService = materialhandlingunitconfigurationsService;
            _materialsService = materialsService;
            _movequeuesService = movequeuesService;
            _outboundcarriermanifestpickupsService = outboundcarriermanifestpickupsService;
            _outboundcarriermanifestsService = outboundcarriermanifestsService;
            _outboundorderlinesService = outboundorderlinesService;
            _outboundordersService = outboundordersService;
            _outboundshipmentsService = outboundshipmentsService;
            _peopleService = peopleService;
            _pickbatchesService = pickbatchesService;
            _receivingService = receivingService;


            // The DialogSet needs a DialogState accessor, it will call it when it has a turn context.
            _dialogs = new DialogSet(statePropertyAccessor.DialogStateAccessor)
                .Add(new RootDialog(RootDialogId, _botUserEntityContext, _navigationEntityData))
               .Add(new CreatePeopleDialog(CreatePeopleDialogId, _peopleService, _peoplePost, _botSpielUserStateAccessors))
                .Add(new DeletePeopleDialog(DeletePeopleDialogId, _peopleService, _peoplePost, _botSpielUserStateAccessors))
                .Add(new EditPeopleDialog(EditPeopleDialogId, _peopleService, _peoplePost, _botSpielUserStateAccessors))
                .Add(new FindPeopleDialog(FindPeopleDialogId, _peopleService, _peoplePost, _botSpielUserStateAccessors))
                .Add(new CreateAddressesDialog(CreateAddressesDialogId, _addressesService, _addressesPost, _botSpielUserStateAccessors))
                .Add(new DeleteAddressesDialog(DeleteAddressesDialogId, _addressesService, _addressesPost, _botSpielUserStateAccessors))
                .Add(new EditAddressesDialog(EditAddressesDialogId, _addressesService, _addressesPost, _botSpielUserStateAccessors))
                .Add(new FindAddressesDialog(FindAddressesDialogId, _addressesService, _addressesPost, _botSpielUserStateAccessors))
                .Add(new CreateCompaniesDialog(CreateCompaniesDialogId, _companiesService, _companiesPost, _botSpielUserStateAccessors))
                .Add(new DeleteCompaniesDialog(DeleteCompaniesDialogId, _companiesService, _companiesPost, _botSpielUserStateAccessors))
                .Add(new EditCompaniesDialog(EditCompaniesDialogId, _companiesService, _companiesPost, _botSpielUserStateAccessors))
                .Add(new FindCompaniesDialog(FindCompaniesDialogId, _companiesService, _companiesPost, _botSpielUserStateAccessors))
                .Add(new CreateFacilitiesDialog(CreateFacilitiesDialogId, _facilitiesService, _facilitiesPost, _botSpielUserStateAccessors))
                .Add(new DeleteFacilitiesDialog(DeleteFacilitiesDialogId, _facilitiesService, _facilitiesPost, _botSpielUserStateAccessors))
                .Add(new EditFacilitiesDialog(EditFacilitiesDialogId, _facilitiesService, _facilitiesPost, _botSpielUserStateAccessors))
                .Add(new FindFacilitiesDialog(FindFacilitiesDialogId, _facilitiesService, _facilitiesPost, _botSpielUserStateAccessors))
                .Add(new CreateFacilityZonesDialog(CreateFacilityZonesDialogId, _facilityzonesService, _facilityzonesPost, _botSpielUserStateAccessors))
                .Add(new DeleteFacilityZonesDialog(DeleteFacilityZonesDialogId, _facilityzonesService, _facilityzonesPost, _botSpielUserStateAccessors))
                .Add(new EditFacilityZonesDialog(EditFacilityZonesDialogId, _facilityzonesService, _facilityzonesPost, _botSpielUserStateAccessors))
                .Add(new FindFacilityZonesDialog(FindFacilityZonesDialogId, _facilityzonesService, _facilityzonesPost, _botSpielUserStateAccessors))
                .Add(new CreateFacilityWorkAreasDialog(CreateFacilityWorkAreasDialogId, _facilityworkareasService, _facilityworkareasPost, _botSpielUserStateAccessors))
                .Add(new DeleteFacilityWorkAreasDialog(DeleteFacilityWorkAreasDialogId, _facilityworkareasService, _facilityworkareasPost, _botSpielUserStateAccessors))
                .Add(new EditFacilityWorkAreasDialog(EditFacilityWorkAreasDialogId, _facilityworkareasService, _facilityworkareasPost, _botSpielUserStateAccessors))
                .Add(new FindFacilityWorkAreasDialog(FindFacilityWorkAreasDialogId, _facilityworkareasService, _facilityworkareasPost, _botSpielUserStateAccessors))
                .Add(new CreateFacilityFloorsDialog(CreateFacilityFloorsDialogId, _facilityfloorsService, _facilityfloorsPost, _botSpielUserStateAccessors))
                .Add(new DeleteFacilityFloorsDialog(DeleteFacilityFloorsDialogId, _facilityfloorsService, _facilityfloorsPost, _botSpielUserStateAccessors))
                .Add(new EditFacilityFloorsDialog(EditFacilityFloorsDialogId, _facilityfloorsService, _facilityfloorsPost, _botSpielUserStateAccessors))
                .Add(new FindFacilityFloorsDialog(FindFacilityFloorsDialogId, _facilityfloorsService, _facilityfloorsPost, _botSpielUserStateAccessors))
                .Add(new CreateFacilityAisleFacesDialog(CreateFacilityAisleFacesDialogId, _facilityaislefacesService, _facilityaislefacesPost, _botSpielUserStateAccessors))
                .Add(new DeleteFacilityAisleFacesDialog(DeleteFacilityAisleFacesDialogId, _facilityaislefacesService, _facilityaislefacesPost, _botSpielUserStateAccessors))
                .Add(new EditFacilityAisleFacesDialog(EditFacilityAisleFacesDialogId, _facilityaislefacesService, _facilityaislefacesPost, _botSpielUserStateAccessors))
                .Add(new FindFacilityAisleFacesDialog(FindFacilityAisleFacesDialogId, _facilityaislefacesService, _facilityaislefacesPost, _botSpielUserStateAccessors))
                .Add(new CreateInventoryLocationSizesDialog(CreateInventoryLocationSizesDialogId, _inventorylocationsizesService, _inventorylocationsizesPost, _botSpielUserStateAccessors))
                .Add(new DeleteInventoryLocationSizesDialog(DeleteInventoryLocationSizesDialogId, _inventorylocationsizesService, _inventorylocationsizesPost, _botSpielUserStateAccessors))
                .Add(new EditInventoryLocationSizesDialog(EditInventoryLocationSizesDialogId, _inventorylocationsizesService, _inventorylocationsizesPost, _botSpielUserStateAccessors))
                .Add(new FindInventoryLocationSizesDialog(FindInventoryLocationSizesDialogId, _inventorylocationsizesService, _inventorylocationsizesPost, _botSpielUserStateAccessors))
                .Add(new CreateMaterialsDialog(CreateMaterialsDialogId, _materialsService, _materialsPost, _botSpielUserStateAccessors))
                .Add(new DeleteMaterialsDialog(DeleteMaterialsDialogId, _materialsService, _materialsPost, _botSpielUserStateAccessors))
                .Add(new EditMaterialsDialog(EditMaterialsDialogId, _materialsService, _materialsPost, _botSpielUserStateAccessors))
                .Add(new FindMaterialsDialog(FindMaterialsDialogId, _materialsService, _materialsPost, _botSpielUserStateAccessors))
                .Add(new CreateInventoryUnitsDialog(CreateInventoryUnitsDialogId, _inventoryunitsService, _inventoryunitsPost, _botSpielUserStateAccessors))
                .Add(new DeleteInventoryUnitsDialog(DeleteInventoryUnitsDialogId, _inventoryunitsService, _inventoryunitsPost, _botSpielUserStateAccessors))
                .Add(new EditInventoryUnitsDialog(EditInventoryUnitsDialogId, _inventoryunitsService, _inventoryunitsPost, _botSpielUserStateAccessors))
                .Add(new FindInventoryUnitsDialog(FindInventoryUnitsDialogId, _inventoryunitsService, _inventoryunitsPost, _botSpielUserStateAccessors))
                .Add(new CreateHandlingUnitsDialog(CreateHandlingUnitsDialogId, _handlingunitsService, _handlingunitsPost, _botSpielUserStateAccessors))
                .Add(new DeleteHandlingUnitsDialog(DeleteHandlingUnitsDialogId, _handlingunitsService, _handlingunitsPost, _botSpielUserStateAccessors))
                .Add(new EditHandlingUnitsDialog(EditHandlingUnitsDialogId, _handlingunitsService, _handlingunitsPost, _botSpielUserStateAccessors))
                .Add(new FindHandlingUnitsDialog(FindHandlingUnitsDialogId, _handlingunitsService, _handlingunitsPost, _botSpielUserStateAccessors))
                .Add(new CreateInventoryLocationsDialog(CreateInventoryLocationsDialogId, _inventorylocationsService, _inventorylocationsPost, _botSpielUserStateAccessors))
                .Add(new DeleteInventoryLocationsDialog(DeleteInventoryLocationsDialogId, _inventorylocationsService, _inventorylocationsPost, _botSpielUserStateAccessors))
                .Add(new EditInventoryLocationsDialog(EditInventoryLocationsDialogId, _inventorylocationsService, _inventorylocationsPost, _botSpielUserStateAccessors))
                .Add(new FindInventoryLocationsDialog(FindInventoryLocationsDialogId, _inventorylocationsService, _inventorylocationsPost, _botSpielUserStateAccessors))
                .Add(new CreateMoveQueuesDialog(CreateMoveQueuesDialogId, _movequeuesService, _movequeuesPost, _botSpielUserStateAccessors))
                .Add(new DeleteMoveQueuesDialog(DeleteMoveQueuesDialogId, _movequeuesService, _movequeuesPost, _botSpielUserStateAccessors))
                .Add(new EditMoveQueuesDialog(EditMoveQueuesDialogId, _movequeuesService, _movequeuesPost, _botSpielUserStateAccessors))
                .Add(new FindMoveQueuesDialog(FindMoveQueuesDialogId, _movequeuesService, _movequeuesPost, _botSpielUserStateAccessors))
                .Add(new CreateMaterialHandlingUnitConfigurationsDialog(CreateMaterialHandlingUnitConfigurationsDialogId, _materialhandlingunitconfigurationsService, _materialhandlingunitconfigurationsPost, _botSpielUserStateAccessors))
                .Add(new DeleteMaterialHandlingUnitConfigurationsDialog(DeleteMaterialHandlingUnitConfigurationsDialogId, _materialhandlingunitconfigurationsService, _materialhandlingunitconfigurationsPost, _botSpielUserStateAccessors))
                .Add(new EditMaterialHandlingUnitConfigurationsDialog(EditMaterialHandlingUnitConfigurationsDialogId, _materialhandlingunitconfigurationsService, _materialhandlingunitconfigurationsPost, _botSpielUserStateAccessors))
                .Add(new FindMaterialHandlingUnitConfigurationsDialog(FindMaterialHandlingUnitConfigurationsDialogId, _materialhandlingunitconfigurationsService, _materialhandlingunitconfigurationsPost, _botSpielUserStateAccessors))
                .Add(new CreateBusinessPartnersDialog(CreateBusinessPartnersDialogId, _businesspartnersService, _businesspartnersPost, _botSpielUserStateAccessors))
                .Add(new DeleteBusinessPartnersDialog(DeleteBusinessPartnersDialogId, _businesspartnersService, _businesspartnersPost, _botSpielUserStateAccessors))
                .Add(new EditBusinessPartnersDialog(EditBusinessPartnersDialogId, _businesspartnersService, _businesspartnersPost, _botSpielUserStateAccessors))
                .Add(new FindBusinessPartnersDialog(FindBusinessPartnersDialogId, _businesspartnersService, _businesspartnersPost, _botSpielUserStateAccessors))
                .Add(new CreateInboundOrdersDialog(CreateInboundOrdersDialogId, _inboundordersService, _inboundordersPost, _botSpielUserStateAccessors))
                .Add(new DeleteInboundOrdersDialog(DeleteInboundOrdersDialogId, _inboundordersService, _inboundordersPost, _botSpielUserStateAccessors))
                .Add(new EditInboundOrdersDialog(EditInboundOrdersDialogId, _inboundordersService, _inboundordersPost, _botSpielUserStateAccessors))
                .Add(new FindInboundOrdersDialog(FindInboundOrdersDialogId, _inboundordersService, _inboundordersPost, _botSpielUserStateAccessors))
                .Add(new CreateInboundOrderLinesDialog(CreateInboundOrderLinesDialogId, _inboundorderlinesService, _inboundorderlinesPost, _botSpielUserStateAccessors))
                .Add(new DeleteInboundOrderLinesDialog(DeleteInboundOrderLinesDialogId, _inboundorderlinesService, _inboundorderlinesPost, _botSpielUserStateAccessors))
                .Add(new EditInboundOrderLinesDialog(EditInboundOrderLinesDialogId, _inboundorderlinesService, _inboundorderlinesPost, _botSpielUserStateAccessors))
                .Add(new FindInboundOrderLinesDialog(FindInboundOrderLinesDialogId, _inboundorderlinesService, _inboundorderlinesPost, _botSpielUserStateAccessors))
                .Add(new CreateCarriersDialog(CreateCarriersDialogId, _carriersService, _carriersPost, _botSpielUserStateAccessors))
                .Add(new DeleteCarriersDialog(DeleteCarriersDialogId, _carriersService, _carriersPost, _botSpielUserStateAccessors))
                .Add(new EditCarriersDialog(EditCarriersDialogId, _carriersService, _carriersPost, _botSpielUserStateAccessors))
                .Add(new FindCarriersDialog(FindCarriersDialogId, _carriersService, _carriersPost, _botSpielUserStateAccessors))
                .Add(new CreateCarrierServicesDialog(CreateCarrierServicesDialogId, _carrierservicesService, _carrierservicesPost, _botSpielUserStateAccessors))
                .Add(new DeleteCarrierServicesDialog(DeleteCarrierServicesDialogId, _carrierservicesService, _carrierservicesPost, _botSpielUserStateAccessors))
                .Add(new EditCarrierServicesDialog(EditCarrierServicesDialogId, _carrierservicesService, _carrierservicesPost, _botSpielUserStateAccessors))
                .Add(new FindCarrierServicesDialog(FindCarrierServicesDialogId, _carrierservicesService, _carrierservicesPost, _botSpielUserStateAccessors))
                .Add(new CreateOutboundOrdersDialog(CreateOutboundOrdersDialogId, _outboundordersService, _outboundordersPost, _botSpielUserStateAccessors))
                .Add(new DeleteOutboundOrdersDialog(DeleteOutboundOrdersDialogId, _outboundordersService, _outboundordersPost, _botSpielUserStateAccessors))
                .Add(new EditOutboundOrdersDialog(EditOutboundOrdersDialogId, _outboundordersService, _outboundordersPost, _botSpielUserStateAccessors))
                .Add(new FindOutboundOrdersDialog(FindOutboundOrdersDialogId, _outboundordersService, _outboundordersPost, _botSpielUserStateAccessors))
                .Add(new CreateOutboundShipmentsDialog(CreateOutboundShipmentsDialogId, _outboundshipmentsService, _outboundshipmentsPost, _botSpielUserStateAccessors))
                .Add(new DeleteOutboundShipmentsDialog(DeleteOutboundShipmentsDialogId, _outboundshipmentsService, _outboundshipmentsPost, _botSpielUserStateAccessors))
                .Add(new EditOutboundShipmentsDialog(EditOutboundShipmentsDialogId, _outboundshipmentsService, _outboundshipmentsPost, _botSpielUserStateAccessors))
                .Add(new FindOutboundShipmentsDialog(FindOutboundShipmentsDialogId, _outboundshipmentsService, _outboundshipmentsPost, _botSpielUserStateAccessors))
                .Add(new CreateOutboundCarrierManifestsDialog(CreateOutboundCarrierManifestsDialogId, _outboundcarriermanifestsService, _outboundcarriermanifestsPost, _botSpielUserStateAccessors))
                .Add(new DeleteOutboundCarrierManifestsDialog(DeleteOutboundCarrierManifestsDialogId, _outboundcarriermanifestsService, _outboundcarriermanifestsPost, _botSpielUserStateAccessors))
                .Add(new EditOutboundCarrierManifestsDialog(EditOutboundCarrierManifestsDialogId, _outboundcarriermanifestsService, _outboundcarriermanifestsPost, _botSpielUserStateAccessors))
                .Add(new FindOutboundCarrierManifestsDialog(FindOutboundCarrierManifestsDialogId, _outboundcarriermanifestsService, _outboundcarriermanifestsPost, _botSpielUserStateAccessors))
                .Add(new CreateOutboundOrderLinesDialog(CreateOutboundOrderLinesDialogId, _outboundorderlinesService, _outboundorderlinesPost, _botSpielUserStateAccessors))
                .Add(new DeleteOutboundOrderLinesDialog(DeleteOutboundOrderLinesDialogId, _outboundorderlinesService, _outboundorderlinesPost, _botSpielUserStateAccessors))
                .Add(new EditOutboundOrderLinesDialog(EditOutboundOrderLinesDialogId, _outboundorderlinesService, _outboundorderlinesPost, _botSpielUserStateAccessors))
                .Add(new FindOutboundOrderLinesDialog(FindOutboundOrderLinesDialogId, _outboundorderlinesService, _outboundorderlinesPost, _botSpielUserStateAccessors))
                .Add(new CreatePickBatchesDialog(CreatePickBatchesDialogId, _pickbatchesService, _pickbatchesPost, _botSpielUserStateAccessors))
                .Add(new DeletePickBatchesDialog(DeletePickBatchesDialogId, _pickbatchesService, _pickbatchesPost, _botSpielUserStateAccessors))
                .Add(new EditPickBatchesDialog(EditPickBatchesDialogId, _pickbatchesService, _pickbatchesPost, _botSpielUserStateAccessors))
                .Add(new FindPickBatchesDialog(FindPickBatchesDialogId, _pickbatchesService, _pickbatchesPost, _botSpielUserStateAccessors))
                .Add(new CreateReceivingDialog(CreateReceivingDialogId, _receivingService, _receivingPost, _botSpielUserStateAccessors))
                .Add(new DeleteReceivingDialog(DeleteReceivingDialogId, _receivingService, _receivingPost, _botSpielUserStateAccessors))
                .Add(new EditReceivingDialog(EditReceivingDialogId, _receivingService, _receivingPost, _botSpielUserStateAccessors))
                .Add(new FindReceivingDialog(FindReceivingDialogId, _receivingService, _receivingPost, _botSpielUserStateAccessors))
                .Add(new CreateOutboundCarrierManifestPickupsDialog(CreateOutboundCarrierManifestPickupsDialogId, _outboundcarriermanifestpickupsService, _outboundcarriermanifestpickupsPost, _botSpielUserStateAccessors))
                .Add(new DeleteOutboundCarrierManifestPickupsDialog(DeleteOutboundCarrierManifestPickupsDialogId, _outboundcarriermanifestpickupsService, _outboundcarriermanifestpickupsPost, _botSpielUserStateAccessors))
                .Add(new EditOutboundCarrierManifestPickupsDialog(EditOutboundCarrierManifestPickupsDialogId, _outboundcarriermanifestpickupsService, _outboundcarriermanifestpickupsPost, _botSpielUserStateAccessors))
                .Add(new FindOutboundCarrierManifestPickupsDialog(FindOutboundCarrierManifestPickupsDialogId, _outboundcarriermanifestpickupsService, _outboundcarriermanifestpickupsPost, _botSpielUserStateAccessors))
                .Add(new CreateInventoryLocationsSlottingDialog(CreateInventoryLocationsSlottingDialogId, _inventorylocationsslottingService, _inventorylocationsslottingPost, _botSpielUserStateAccessors))
                .Add(new DeleteInventoryLocationsSlottingDialog(DeleteInventoryLocationsSlottingDialogId, _inventorylocationsslottingService, _inventorylocationsslottingPost, _botSpielUserStateAccessors))
                .Add(new EditInventoryLocationsSlottingDialog(EditInventoryLocationsSlottingDialogId, _inventorylocationsslottingService, _inventorylocationsslottingPost, _botSpielUserStateAccessors))
                .Add(new FindInventoryLocationsSlottingDialog(FindInventoryLocationsSlottingDialogId, _inventorylocationsslottingService, _inventorylocationsslottingPost, _botSpielUserStateAccessors))
                .Add(new ConfirmPrompt(ConfirmPromptId, defaultLocale: Culture.English));
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
                                    case "People":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreatePeopleDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixPerson > 0)
                                            {
                                                await dc.BeginDialogAsync(EditPeopleDialogId, _peopleService.GetPost(currentBotUserData.ixPerson), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no people selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindPeopleDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixPerson > 0)
                                            {
                                                _peoplePost = _peopleService.GetPost(currentBotUserData.ixPerson);
                                                existInEntities = _peopleService.VerifyPersonDeleteOK(_peoplePost.ixPerson, _peoplePost.sPerson);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeletePeopleDialogId, _peoplePost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The Person {_peoplePost.sPerson} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the Person, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no people selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindPeopleDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixPerson > 0)
                                            {
                                                _peoplePost = _peopleService.GetPost(currentBotUserData.ixPerson);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for Person ID {currentBotUserData.ixPerson}: {System.Environment.NewLine}  Person: {_peoplePost.sPerson.ToString() + System.Environment.NewLine}  FirstName: {_peoplePost.sFirstName.ToString() + System.Environment.NewLine}  LastName: {_peoplePost.sLastName.ToString() + System.Environment.NewLine}  Language: {_peoplePost.ixLanguage.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no people selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindPeopleDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindPeopleDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "Addresses":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateAddressesDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixAddress > 0)
                                            {
                                                await dc.BeginDialogAsync(EditAddressesDialogId, _addressesService.GetPost(currentBotUserData.ixAddress), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no addresses selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindAddressesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixAddress > 0)
                                            {
                                                _addressesPost = _addressesService.GetPost(currentBotUserData.ixAddress);
                                                existInEntities = _addressesService.VerifyAddressDeleteOK(_addressesPost.ixAddress, _addressesPost.sAddress);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteAddressesDialogId, _addressesPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The Address {_addressesPost.sAddress} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the Address, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no addresses selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindAddressesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixAddress > 0)
                                            {
                                                _addressesPost = _addressesService.GetPost(currentBotUserData.ixAddress);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for Address ID {currentBotUserData.ixAddress}: {System.Environment.NewLine}  Address: {_addressesPost.sAddress.ToString() + System.Environment.NewLine}  StreetAndNumberOrPostOfficeBoxOne: {_addressesPost.sStreetAndNumberOrPostOfficeBoxOne.ToString() + System.Environment.NewLine}  StreetAndNumberOrPostOfficeBoxTwo: {_addressesPost.sStreetAndNumberOrPostOfficeBoxTwo.ToString() + System.Environment.NewLine}  StreetAndNumberOrPostOfficeBoxThree: {_addressesPost.sStreetAndNumberOrPostOfficeBoxThree.ToString() + System.Environment.NewLine}  CityOrSuburb: {_addressesPost.sCityOrSuburb.ToString() + System.Environment.NewLine}  ZipOrPostCode: {_addressesPost.sZipOrPostCode.ToString() + System.Environment.NewLine}  StateOrProvince: {_addressesPost.ixStateOrProvince.ToString() + System.Environment.NewLine}  Country: {_addressesPost.ixCountry.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no addresses selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindAddressesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindAddressesDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "Companies":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateCompaniesDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixCompany > 0)
                                            {
                                                await dc.BeginDialogAsync(EditCompaniesDialogId, _companiesService.GetPost(currentBotUserData.ixCompany), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no companies selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindCompaniesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixCompany > 0)
                                            {
                                                _companiesPost = _companiesService.GetPost(currentBotUserData.ixCompany);
                                                existInEntities = _companiesService.VerifyCompanyDeleteOK(_companiesPost.ixCompany, _companiesPost.sCompany);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteCompaniesDialogId, _companiesPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The Company {_companiesPost.sCompany} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the Company, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no companies selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindCompaniesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixCompany > 0)
                                            {
                                                _companiesPost = _companiesService.GetPost(currentBotUserData.ixCompany);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for Company ID {currentBotUserData.ixCompany}: {System.Environment.NewLine}  Company: {_companiesPost.sCompany.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no companies selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindCompaniesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindCompaniesDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "Facilities":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateFacilitiesDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixFacility > 0)
                                            {
                                                await dc.BeginDialogAsync(EditFacilitiesDialogId, _facilitiesService.GetPost(currentBotUserData.ixFacility), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no facilities selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindFacilitiesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixFacility > 0)
                                            {
                                                _facilitiesPost = _facilitiesService.GetPost(currentBotUserData.ixFacility);
                                                existInEntities = _facilitiesService.VerifyFacilityDeleteOK(_facilitiesPost.ixFacility, _facilitiesPost.sFacility);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteFacilitiesDialogId, _facilitiesPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The Facility {_facilitiesPost.sFacility} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the Facility, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no facilities selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindFacilitiesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixFacility > 0)
                                            {
                                                _facilitiesPost = _facilitiesService.GetPost(currentBotUserData.ixFacility);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for Facility ID {currentBotUserData.ixFacility}: {System.Environment.NewLine}  Facility: {_facilitiesPost.sFacility.ToString() + System.Environment.NewLine}  Address: {_facilitiesPost.ixAddress.ToString() + System.Environment.NewLine}  Latitude: {_facilitiesPost.sLatitude.ToString() + System.Environment.NewLine}  Longitude: {_facilitiesPost.sLongitude.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no facilities selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindFacilitiesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindFacilitiesDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "FacilityZones":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateFacilityZonesDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixFacilityZone > 0)
                                            {
                                                await dc.BeginDialogAsync(EditFacilityZonesDialogId, _facilityzonesService.GetPost(currentBotUserData.ixFacilityZone), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no facilityzones selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindFacilityZonesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixFacilityZone > 0)
                                            {
                                                _facilityzonesPost = _facilityzonesService.GetPost(currentBotUserData.ixFacilityZone);
                                                existInEntities = _facilityzonesService.VerifyFacilityZoneDeleteOK(_facilityzonesPost.ixFacilityZone, _facilityzonesPost.sFacilityZone);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteFacilityZonesDialogId, _facilityzonesPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The FacilityZone {_facilityzonesPost.sFacilityZone} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the FacilityZone, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no facilityzones selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindFacilityZonesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixFacilityZone > 0)
                                            {
                                                _facilityzonesPost = _facilityzonesService.GetPost(currentBotUserData.ixFacilityZone);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for FacilityZone ID {currentBotUserData.ixFacilityZone}: {System.Environment.NewLine}  FacilityZone: {_facilityzonesPost.sFacilityZone.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no facilityzones selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindFacilityZonesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindFacilityZonesDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "FacilityWorkAreas":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateFacilityWorkAreasDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixFacilityWorkArea > 0)
                                            {
                                                await dc.BeginDialogAsync(EditFacilityWorkAreasDialogId, _facilityworkareasService.GetPost(currentBotUserData.ixFacilityWorkArea), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no facilityworkareas selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindFacilityWorkAreasDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixFacilityWorkArea > 0)
                                            {
                                                _facilityworkareasPost = _facilityworkareasService.GetPost(currentBotUserData.ixFacilityWorkArea);
                                                existInEntities = _facilityworkareasService.VerifyFacilityWorkAreaDeleteOK(_facilityworkareasPost.ixFacilityWorkArea, _facilityworkareasPost.sFacilityWorkArea);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteFacilityWorkAreasDialogId, _facilityworkareasPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The FacilityWorkArea {_facilityworkareasPost.sFacilityWorkArea} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the FacilityWorkArea, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no facilityworkareas selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindFacilityWorkAreasDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixFacilityWorkArea > 0)
                                            {
                                                _facilityworkareasPost = _facilityworkareasService.GetPost(currentBotUserData.ixFacilityWorkArea);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for FacilityWorkArea ID {currentBotUserData.ixFacilityWorkArea}: {System.Environment.NewLine}  FacilityWorkArea: {_facilityworkareasPost.sFacilityWorkArea.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no facilityworkareas selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindFacilityWorkAreasDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindFacilityWorkAreasDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "FacilityFloors":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateFacilityFloorsDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixFacilityFloor > 0)
                                            {
                                                await dc.BeginDialogAsync(EditFacilityFloorsDialogId, _facilityfloorsService.GetPost(currentBotUserData.ixFacilityFloor), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no facilityfloors selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindFacilityFloorsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixFacilityFloor > 0)
                                            {
                                                _facilityfloorsPost = _facilityfloorsService.GetPost(currentBotUserData.ixFacilityFloor);
                                                existInEntities = _facilityfloorsService.VerifyFacilityFloorDeleteOK(_facilityfloorsPost.ixFacilityFloor, _facilityfloorsPost.sFacilityFloor);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteFacilityFloorsDialogId, _facilityfloorsPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The FacilityFloor {_facilityfloorsPost.sFacilityFloor} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the FacilityFloor, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no facilityfloors selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindFacilityFloorsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixFacilityFloor > 0)
                                            {
                                                _facilityfloorsPost = _facilityfloorsService.GetPost(currentBotUserData.ixFacilityFloor);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for FacilityFloor ID {currentBotUserData.ixFacilityFloor}: {System.Environment.NewLine}  FacilityFloor: {_facilityfloorsPost.sFacilityFloor.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no facilityfloors selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindFacilityFloorsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindFacilityFloorsDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "FacilityAisleFaces":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateFacilityAisleFacesDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixFacilityAisleFace > 0)
                                            {
                                                await dc.BeginDialogAsync(EditFacilityAisleFacesDialogId, _facilityaislefacesService.GetPost(currentBotUserData.ixFacilityAisleFace), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no facilityaislefaces selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindFacilityAisleFacesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixFacilityAisleFace > 0)
                                            {
                                                _facilityaislefacesPost = _facilityaislefacesService.GetPost(currentBotUserData.ixFacilityAisleFace);
                                                existInEntities = _facilityaislefacesService.VerifyFacilityAisleFaceDeleteOK(_facilityaislefacesPost.ixFacilityAisleFace, _facilityaislefacesPost.sFacilityAisleFace);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteFacilityAisleFacesDialogId, _facilityaislefacesPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The FacilityAisleFace {_facilityaislefacesPost.sFacilityAisleFace} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the FacilityAisleFace, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no facilityaislefaces selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindFacilityAisleFacesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixFacilityAisleFace > 0)
                                            {
                                                _facilityaislefacesPost = _facilityaislefacesService.GetPost(currentBotUserData.ixFacilityAisleFace);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for FacilityAisleFace ID {currentBotUserData.ixFacilityAisleFace}: {System.Environment.NewLine}  FacilityAisleFace: {_facilityaislefacesPost.sFacilityAisleFace.ToString() + System.Environment.NewLine}  FacilityFloor: {_facilityaislefacesPost.ixFacilityFloor.ToString() + System.Environment.NewLine}  Sequence: {_facilityaislefacesPost.nSequence.ToString() + System.Environment.NewLine}  BaySequenceType: {_facilityaislefacesPost.ixBaySequenceType.ToString() + System.Environment.NewLine}  PairedAisleFace: {_facilityaislefacesPost.ixPairedAisleFace.ToString() + System.Environment.NewLine}  LogicalOrientation: {_facilityaislefacesPost.ixLogicalOrientation.ToString() + System.Environment.NewLine}  AisleFaceStorageType: {_facilityaislefacesPost.ixAisleFaceStorageType.ToString() + System.Environment.NewLine}  XOffset: {_facilityaislefacesPost.nXOffset.ToString() + System.Environment.NewLine}  XOffsetUnit: {_facilityaislefacesPost.ixXOffsetUnit.ToString() + System.Environment.NewLine}  YOffset: {_facilityaislefacesPost.nYOffset.ToString() + System.Environment.NewLine}  YOffsetUnit: {_facilityaislefacesPost.ixYOffsetUnit.ToString() + System.Environment.NewLine}  Levels: {_facilityaislefacesPost.nLevels.ToString() + System.Environment.NewLine}  DefaultNumberOfBays: {_facilityaislefacesPost.nDefaultNumberOfBays.ToString() + System.Environment.NewLine}  DefaultNumberOfSlotsInBay: {_facilityaislefacesPost.nDefaultNumberOfSlotsInBay.ToString() + System.Environment.NewLine}  DefaultFacilityZone: {_facilityaislefacesPost.ixDefaultFacilityZone.ToString() + System.Environment.NewLine}  DefaultLocationFunction: {_facilityaislefacesPost.ixDefaultLocationFunction.ToString() + System.Environment.NewLine}  DefaultInventoryLocationSize: {_facilityaislefacesPost.ixDefaultInventoryLocationSize.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no facilityaislefaces selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindFacilityAisleFacesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindFacilityAisleFacesDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "InventoryLocationSizes":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateInventoryLocationSizesDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixInventoryLocationSize > 0)
                                            {
                                                await dc.BeginDialogAsync(EditInventoryLocationSizesDialogId, _inventorylocationsizesService.GetPost(currentBotUserData.ixInventoryLocationSize), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no inventorylocationsizes selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindInventoryLocationSizesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixInventoryLocationSize > 0)
                                            {
                                                _inventorylocationsizesPost = _inventorylocationsizesService.GetPost(currentBotUserData.ixInventoryLocationSize);
                                                existInEntities = _inventorylocationsizesService.VerifyInventoryLocationSizeDeleteOK(_inventorylocationsizesPost.ixInventoryLocationSize, _inventorylocationsizesPost.sInventoryLocationSize);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteInventoryLocationSizesDialogId, _inventorylocationsizesPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The InventoryLocationSize {_inventorylocationsizesPost.sInventoryLocationSize} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the InventoryLocationSize, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no inventorylocationsizes selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindInventoryLocationSizesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixInventoryLocationSize > 0)
                                            {
                                                _inventorylocationsizesPost = _inventorylocationsizesService.GetPost(currentBotUserData.ixInventoryLocationSize);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for InventoryLocationSize ID {currentBotUserData.ixInventoryLocationSize}: {System.Environment.NewLine}  InventoryLocationSize: {_inventorylocationsizesPost.sInventoryLocationSize.ToString() + System.Environment.NewLine}  Length: {_inventorylocationsizesPost.nLength.ToString() + System.Environment.NewLine}  LengthUnit: {_inventorylocationsizesPost.ixLengthUnit.ToString() + System.Environment.NewLine}  Width: {_inventorylocationsizesPost.nWidth.ToString() + System.Environment.NewLine}  WidthUnit: {_inventorylocationsizesPost.ixWidthUnit.ToString() + System.Environment.NewLine}  Height: {_inventorylocationsizesPost.nHeight.ToString() + System.Environment.NewLine}  HeightUnit: {_inventorylocationsizesPost.ixHeightUnit.ToString() + System.Environment.NewLine}  UsableVolume: {_inventorylocationsizesPost.nUsableVolume.ToString() + System.Environment.NewLine}  UsableVolumeUnit: {_inventorylocationsizesPost.ixUsableVolumeUnit.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no inventorylocationsizes selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindInventoryLocationSizesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindInventoryLocationSizesDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "Materials":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateMaterialsDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixMaterial > 0)
                                            {
                                                await dc.BeginDialogAsync(EditMaterialsDialogId, _materialsService.GetPost(currentBotUserData.ixMaterial), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no materials selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindMaterialsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixMaterial > 0)
                                            {
                                                _materialsPost = _materialsService.GetPost(currentBotUserData.ixMaterial);
                                                existInEntities = _materialsService.VerifyMaterialDeleteOK(_materialsPost.ixMaterial, _materialsPost.sMaterial);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteMaterialsDialogId, _materialsPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The Material {_materialsPost.sMaterial} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the Material, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no materials selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindMaterialsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixMaterial > 0)
                                            {
                                                _materialsPost = _materialsService.GetPost(currentBotUserData.ixMaterial);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for Material ID {currentBotUserData.ixMaterial}: {System.Environment.NewLine}  Material: {_materialsPost.sMaterial.ToString() + System.Environment.NewLine}  Description: {_materialsPost.sDescription.ToString() + System.Environment.NewLine}  MaterialType: {_materialsPost.ixMaterialType.ToString() + System.Environment.NewLine}  BaseUnit: {_materialsPost.ixBaseUnit.ToString() + System.Environment.NewLine}  TrackSerialNumber: {_materialsPost.bTrackSerialNumber.ToString() + System.Environment.NewLine}  TrackBatchNumber: {_materialsPost.bTrackBatchNumber.ToString() + System.Environment.NewLine}  TrackExpiry: {_materialsPost.bTrackExpiry.ToString() + System.Environment.NewLine}  Density: {_materialsPost.nDensity.ToString() + System.Environment.NewLine}  DensityUnit: {_materialsPost.ixDensityUnit.ToString() + System.Environment.NewLine}  Shelflife: {_materialsPost.nShelflife.ToString() + System.Environment.NewLine}  ShelflifeUnit: {_materialsPost.ixShelflifeUnit.ToString() + System.Environment.NewLine}  Length: {_materialsPost.nLength.ToString() + System.Environment.NewLine}  LengthUnit: {_materialsPost.ixLengthUnit.ToString() + System.Environment.NewLine}  Width: {_materialsPost.nWidth.ToString() + System.Environment.NewLine}  WidthUnit: {_materialsPost.ixWidthUnit.ToString() + System.Environment.NewLine}  Height: {_materialsPost.nHeight.ToString() + System.Environment.NewLine}  HeightUnit: {_materialsPost.ixHeightUnit.ToString() + System.Environment.NewLine}  Weight: {_materialsPost.nWeight.ToString() + System.Environment.NewLine}  WeightUnit: {_materialsPost.ixWeightUnit.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no materials selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindMaterialsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindMaterialsDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "InventoryUnits":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateInventoryUnitsDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixInventoryUnit > 0)
                                            {
                                                await dc.BeginDialogAsync(EditInventoryUnitsDialogId, _inventoryunitsService.GetPost(currentBotUserData.ixInventoryUnit), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no inventoryunits selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindInventoryUnitsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixInventoryUnit > 0)
                                            {
                                                _inventoryunitsPost = _inventoryunitsService.GetPost(currentBotUserData.ixInventoryUnit);
                                                existInEntities = _inventoryunitsService.VerifyInventoryUnitDeleteOK(_inventoryunitsPost.ixInventoryUnit, _inventoryunitsPost.sInventoryUnit);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteInventoryUnitsDialogId, _inventoryunitsPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The InventoryUnit {_inventoryunitsPost.sInventoryUnit} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the InventoryUnit, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no inventoryunits selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindInventoryUnitsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixInventoryUnit > 0)
                                            {
                                                _inventoryunitsPost = _inventoryunitsService.GetPost(currentBotUserData.ixInventoryUnit);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for InventoryUnit ID {currentBotUserData.ixInventoryUnit}: {System.Environment.NewLine}  InventoryUnit: {_inventoryunitsPost.sInventoryUnit.ToString() + System.Environment.NewLine}  Facility: {_inventoryunitsPost.ixFacility.ToString() + System.Environment.NewLine}  Company: {_inventoryunitsPost.ixCompany.ToString() + System.Environment.NewLine}  Material: {_inventoryunitsPost.ixMaterial.ToString() + System.Environment.NewLine}  InventoryState: {_inventoryunitsPost.ixInventoryState.ToString() + System.Environment.NewLine}  HandlingUnit: {_inventoryunitsPost.ixHandlingUnit.ToString() + System.Environment.NewLine}  InventoryLocation: {_inventoryunitsPost.ixInventoryLocation.ToString() + System.Environment.NewLine}  BaseUnitQuantity: {_inventoryunitsPost.nBaseUnitQuantity.ToString() + System.Environment.NewLine}  SerialNumber: {_inventoryunitsPost.sSerialNumber.ToString() + System.Environment.NewLine}  BatchNumber: {_inventoryunitsPost.sBatchNumber.ToString() + System.Environment.NewLine}  ExpireAt: {_inventoryunitsPost.dtExpireAt.ToString() + System.Environment.NewLine}  Status: {_inventoryunitsPost.ixStatus.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no inventoryunits selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindInventoryUnitsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindInventoryUnitsDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "HandlingUnits":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateHandlingUnitsDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixHandlingUnit > 0)
                                            {
                                                await dc.BeginDialogAsync(EditHandlingUnitsDialogId, _handlingunitsService.GetPost(currentBotUserData.ixHandlingUnit), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no handlingunits selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindHandlingUnitsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixHandlingUnit > 0)
                                            {
                                                _handlingunitsPost = _handlingunitsService.GetPost(currentBotUserData.ixHandlingUnit);
                                                existInEntities = _handlingunitsService.VerifyHandlingUnitDeleteOK(_handlingunitsPost.ixHandlingUnit, _handlingunitsPost.sHandlingUnit);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteHandlingUnitsDialogId, _handlingunitsPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The HandlingUnit {_handlingunitsPost.sHandlingUnit} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the HandlingUnit, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no handlingunits selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindHandlingUnitsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixHandlingUnit > 0)
                                            {
                                                _handlingunitsPost = _handlingunitsService.GetPost(currentBotUserData.ixHandlingUnit);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for HandlingUnit ID {currentBotUserData.ixHandlingUnit}: {System.Environment.NewLine}  HandlingUnit: {_handlingunitsPost.sHandlingUnit.ToString() + System.Environment.NewLine}  HandlingUnitType: {_handlingunitsPost.ixHandlingUnitType.ToString() + System.Environment.NewLine}  ParentHandlingUnit: {_handlingunitsPost.ixParentHandlingUnit.ToString() + System.Environment.NewLine}  PackingMaterial: {_handlingunitsPost.ixPackingMaterial.ToString() + System.Environment.NewLine}  MaterialHandlingUnitConfiguration: {_handlingunitsPost.ixMaterialHandlingUnitConfiguration.ToString() + System.Environment.NewLine}  Length: {_handlingunitsPost.nLength.ToString() + System.Environment.NewLine}  LengthUnit: {_handlingunitsPost.ixLengthUnit.ToString() + System.Environment.NewLine}  Width: {_handlingunitsPost.nWidth.ToString() + System.Environment.NewLine}  WidthUnit: {_handlingunitsPost.ixWidthUnit.ToString() + System.Environment.NewLine}  Height: {_handlingunitsPost.nHeight.ToString() + System.Environment.NewLine}  HeightUnit: {_handlingunitsPost.ixHeightUnit.ToString() + System.Environment.NewLine}  Weight: {_handlingunitsPost.nWeight.ToString() + System.Environment.NewLine}  Status: {_handlingunitsPost.ixStatus.ToString() + System.Environment.NewLine}  WeightUnit: {_handlingunitsPost.ixWeightUnit.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no handlingunits selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindHandlingUnitsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindHandlingUnitsDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "InventoryLocations":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateInventoryLocationsDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixInventoryLocation > 0)
                                            {
                                                await dc.BeginDialogAsync(EditInventoryLocationsDialogId, _inventorylocationsService.GetPost(currentBotUserData.ixInventoryLocation), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no inventorylocations selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindInventoryLocationsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixInventoryLocation > 0)
                                            {
                                                _inventorylocationsPost = _inventorylocationsService.GetPost(currentBotUserData.ixInventoryLocation);
                                                existInEntities = _inventorylocationsService.VerifyInventoryLocationDeleteOK(_inventorylocationsPost.ixInventoryLocation, _inventorylocationsPost.sInventoryLocation);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteInventoryLocationsDialogId, _inventorylocationsPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The InventoryLocation {_inventorylocationsPost.sInventoryLocation} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the InventoryLocation, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no inventorylocations selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindInventoryLocationsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixInventoryLocation > 0)
                                            {
                                                _inventorylocationsPost = _inventorylocationsService.GetPost(currentBotUserData.ixInventoryLocation);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for InventoryLocation ID {currentBotUserData.ixInventoryLocation}: {System.Environment.NewLine}  InventoryLocation: {_inventorylocationsPost.sInventoryLocation.ToString() + System.Environment.NewLine}  LocationFunction: {_inventorylocationsPost.ixLocationFunction.ToString() + System.Environment.NewLine}  Company: {_inventorylocationsPost.ixCompany.ToString() + System.Environment.NewLine}  FacilityFloor: {_inventorylocationsPost.ixFacilityFloor.ToString() + System.Environment.NewLine}  FacilityZone: {_inventorylocationsPost.ixFacilityZone.ToString() + System.Environment.NewLine}  FacilityWorkArea: {_inventorylocationsPost.ixFacilityWorkArea.ToString() + System.Environment.NewLine}  FacilityAisleFace: {_inventorylocationsPost.ixFacilityAisleFace.ToString() + System.Environment.NewLine}  Level: {_inventorylocationsPost.sLevel.ToString() + System.Environment.NewLine}  Bay: {_inventorylocationsPost.sBay.ToString() + System.Environment.NewLine}  Slot: {_inventorylocationsPost.sSlot.ToString() + System.Environment.NewLine}  InventoryLocationSize: {_inventorylocationsPost.ixInventoryLocationSize.ToString() + System.Environment.NewLine}  Sequence: {_inventorylocationsPost.nSequence.ToString() + System.Environment.NewLine}  XOffset: {_inventorylocationsPost.nXOffset.ToString() + System.Environment.NewLine}  XOffsetUnit: {_inventorylocationsPost.ixXOffsetUnit.ToString() + System.Environment.NewLine}  YOffset: {_inventorylocationsPost.nYOffset.ToString() + System.Environment.NewLine}  YOffsetUnit: {_inventorylocationsPost.ixYOffsetUnit.ToString() + System.Environment.NewLine}  ZOffset: {_inventorylocationsPost.nZOffset.ToString() + System.Environment.NewLine}  ZOffsetUnit: {_inventorylocationsPost.ixZOffsetUnit.ToString() + System.Environment.NewLine}  Latitude: {_inventorylocationsPost.sLatitude.ToString() + System.Environment.NewLine}  Longitude: {_inventorylocationsPost.sLongitude.ToString() + System.Environment.NewLine}  TrackUtilisation: {_inventorylocationsPost.bTrackUtilisation.ToString() + System.Environment.NewLine}  UtilisationPercent: {_inventorylocationsPost.nUtilisationPercent.ToString() + System.Environment.NewLine}  QueuedUtilisationPercent: {_inventorylocationsPost.nQueuedUtilisationPercent.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no inventorylocations selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindInventoryLocationsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindInventoryLocationsDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "MoveQueues":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateMoveQueuesDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixMoveQueue > 0)
                                            {
                                                await dc.BeginDialogAsync(EditMoveQueuesDialogId, _movequeuesService.GetPost(currentBotUserData.ixMoveQueue), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no movequeues selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindMoveQueuesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixMoveQueue > 0)
                                            {
                                                _movequeuesPost = _movequeuesService.GetPost(currentBotUserData.ixMoveQueue);
                                                existInEntities = _movequeuesService.VerifyMoveQueueDeleteOK(_movequeuesPost.ixMoveQueue, _movequeuesPost.sMoveQueue);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteMoveQueuesDialogId, _movequeuesPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The MoveQueue {_movequeuesPost.sMoveQueue} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the MoveQueue, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no movequeues selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindMoveQueuesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixMoveQueue > 0)
                                            {
                                                _movequeuesPost = _movequeuesService.GetPost(currentBotUserData.ixMoveQueue);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for MoveQueue ID {currentBotUserData.ixMoveQueue}: {System.Environment.NewLine}  MoveQueue: {_movequeuesPost.sMoveQueue.ToString() + System.Environment.NewLine}  MoveQueueType: {_movequeuesPost.ixMoveQueueType.ToString() + System.Environment.NewLine}  MoveQueueContext: {_movequeuesPost.ixMoveQueueContext.ToString() + System.Environment.NewLine}  SourceInventoryUnit: {_movequeuesPost.ixSourceInventoryUnit.ToString() + System.Environment.NewLine}  TargetInventoryUnit: {_movequeuesPost.ixTargetInventoryUnit.ToString() + System.Environment.NewLine}  SourceInventoryLocation: {_movequeuesPost.ixSourceInventoryLocation.ToString() + System.Environment.NewLine}  TargetInventoryLocation: {_movequeuesPost.ixTargetInventoryLocation.ToString() + System.Environment.NewLine}  SourceHandlingUnit: {_movequeuesPost.ixSourceHandlingUnit.ToString() + System.Environment.NewLine}  TargetHandlingUnit: {_movequeuesPost.ixTargetHandlingUnit.ToString() + System.Environment.NewLine}  PreferredResource: {_movequeuesPost.sPreferredResource.ToString() + System.Environment.NewLine}  BaseUnitQuantity: {_movequeuesPost.nBaseUnitQuantity.ToString() + System.Environment.NewLine}  StartBy: {_movequeuesPost.dtStartBy.ToString() + System.Environment.NewLine}  CompleteBy: {_movequeuesPost.dtCompleteBy.ToString() + System.Environment.NewLine}  StartedAt: {_movequeuesPost.dtStartedAt.ToString() + System.Environment.NewLine}  CompletedAt: {_movequeuesPost.dtCompletedAt.ToString() + System.Environment.NewLine}  InboundOrderLine: {_movequeuesPost.ixInboundOrderLine.ToString() + System.Environment.NewLine}  OutboundOrderLine: {_movequeuesPost.ixOutboundOrderLine.ToString() + System.Environment.NewLine}  PickBatch: {_movequeuesPost.ixPickBatch.ToString() + System.Environment.NewLine}  Status: {_movequeuesPost.ixStatus.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no movequeues selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindMoveQueuesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindMoveQueuesDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "MaterialHandlingUnitConfigurations":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateMaterialHandlingUnitConfigurationsDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixMaterialHandlingUnitConfiguration > 0)
                                            {
                                                await dc.BeginDialogAsync(EditMaterialHandlingUnitConfigurationsDialogId, _materialhandlingunitconfigurationsService.GetPost(currentBotUserData.ixMaterialHandlingUnitConfiguration), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no materialhandlingunitconfigurations selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindMaterialHandlingUnitConfigurationsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixMaterialHandlingUnitConfiguration > 0)
                                            {
                                                _materialhandlingunitconfigurationsPost = _materialhandlingunitconfigurationsService.GetPost(currentBotUserData.ixMaterialHandlingUnitConfiguration);
                                                existInEntities = _materialhandlingunitconfigurationsService.VerifyMaterialHandlingUnitConfigurationDeleteOK(_materialhandlingunitconfigurationsPost.ixMaterialHandlingUnitConfiguration, _materialhandlingunitconfigurationsPost.sMaterialHandlingUnitConfiguration);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteMaterialHandlingUnitConfigurationsDialogId, _materialhandlingunitconfigurationsPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The MaterialHandlingUnitConfiguration {_materialhandlingunitconfigurationsPost.sMaterialHandlingUnitConfiguration} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the MaterialHandlingUnitConfiguration, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no materialhandlingunitconfigurations selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindMaterialHandlingUnitConfigurationsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixMaterialHandlingUnitConfiguration > 0)
                                            {
                                                _materialhandlingunitconfigurationsPost = _materialhandlingunitconfigurationsService.GetPost(currentBotUserData.ixMaterialHandlingUnitConfiguration);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for MaterialHandlingUnitConfiguration ID {currentBotUserData.ixMaterialHandlingUnitConfiguration}: {System.Environment.NewLine}  MaterialHandlingUnitConfiguration: {_materialhandlingunitconfigurationsPost.sMaterialHandlingUnitConfiguration.ToString() + System.Environment.NewLine}  Material: {_materialhandlingunitconfigurationsPost.ixMaterial.ToString() + System.Environment.NewLine}  NestingLevel: {_materialhandlingunitconfigurationsPost.nNestingLevel.ToString() + System.Environment.NewLine}  HandlingUnitType: {_materialhandlingunitconfigurationsPost.ixHandlingUnitType.ToString() + System.Environment.NewLine}  Quantity: {_materialhandlingunitconfigurationsPost.nQuantity.ToString() + System.Environment.NewLine}  Length: {_materialhandlingunitconfigurationsPost.nLength.ToString() + System.Environment.NewLine}  LengthUnit: {_materialhandlingunitconfigurationsPost.ixLengthUnit.ToString() + System.Environment.NewLine}  Width: {_materialhandlingunitconfigurationsPost.nWidth.ToString() + System.Environment.NewLine}  WidthUnit: {_materialhandlingunitconfigurationsPost.ixWidthUnit.ToString() + System.Environment.NewLine}  Height: {_materialhandlingunitconfigurationsPost.nHeight.ToString() + System.Environment.NewLine}  HeightUnit: {_materialhandlingunitconfigurationsPost.ixHeightUnit.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no materialhandlingunitconfigurations selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindMaterialHandlingUnitConfigurationsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindMaterialHandlingUnitConfigurationsDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "BusinessPartners":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateBusinessPartnersDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixBusinessPartner > 0)
                                            {
                                                await dc.BeginDialogAsync(EditBusinessPartnersDialogId, _businesspartnersService.GetPost(currentBotUserData.ixBusinessPartner), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no businesspartners selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindBusinessPartnersDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixBusinessPartner > 0)
                                            {
                                                _businesspartnersPost = _businesspartnersService.GetPost(currentBotUserData.ixBusinessPartner);
                                                existInEntities = _businesspartnersService.VerifyBusinessPartnerDeleteOK(_businesspartnersPost.ixBusinessPartner, _businesspartnersPost.sBusinessPartner);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteBusinessPartnersDialogId, _businesspartnersPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The BusinessPartner {_businesspartnersPost.sBusinessPartner} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the BusinessPartner, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no businesspartners selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindBusinessPartnersDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixBusinessPartner > 0)
                                            {
                                                _businesspartnersPost = _businesspartnersService.GetPost(currentBotUserData.ixBusinessPartner);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for BusinessPartner ID {currentBotUserData.ixBusinessPartner}: {System.Environment.NewLine}  BusinessPartner: {_businesspartnersPost.sBusinessPartner.ToString() + System.Environment.NewLine}  BusinessPartnerType: {_businesspartnersPost.ixBusinessPartnerType.ToString() + System.Environment.NewLine}  Company: {_businesspartnersPost.ixCompany.ToString() + System.Environment.NewLine}  Address: {_businesspartnersPost.ixAddress.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no businesspartners selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindBusinessPartnersDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindBusinessPartnersDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "InboundOrders":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateInboundOrdersDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixInboundOrder > 0)
                                            {
                                                await dc.BeginDialogAsync(EditInboundOrdersDialogId, _inboundordersService.GetPost(currentBotUserData.ixInboundOrder), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no inboundorders selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindInboundOrdersDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixInboundOrder > 0)
                                            {
                                                _inboundordersPost = _inboundordersService.GetPost(currentBotUserData.ixInboundOrder);
                                                existInEntities = _inboundordersService.VerifyInboundOrderDeleteOK(_inboundordersPost.ixInboundOrder, _inboundordersPost.sInboundOrder);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteInboundOrdersDialogId, _inboundordersPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The InboundOrder {_inboundordersPost.sInboundOrder} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the InboundOrder, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no inboundorders selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindInboundOrdersDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixInboundOrder > 0)
                                            {
                                                _inboundordersPost = _inboundordersService.GetPost(currentBotUserData.ixInboundOrder);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for InboundOrder ID {currentBotUserData.ixInboundOrder}: {System.Environment.NewLine}  InboundOrder: {_inboundordersPost.sInboundOrder.ToString() + System.Environment.NewLine}  OrderReference: {_inboundordersPost.sOrderReference.ToString() + System.Environment.NewLine}  InboundOrderType: {_inboundordersPost.ixInboundOrderType.ToString() + System.Environment.NewLine}  Facility: {_inboundordersPost.ixFacility.ToString() + System.Environment.NewLine}  Company: {_inboundordersPost.ixCompany.ToString() + System.Environment.NewLine}  BusinessPartner: {_inboundordersPost.ixBusinessPartner.ToString() + System.Environment.NewLine}  ExpectedAt: {_inboundordersPost.dtExpectedAt.ToString() + System.Environment.NewLine}  Status: {_inboundordersPost.ixStatus.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no inboundorders selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindInboundOrdersDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindInboundOrdersDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "InboundOrderLines":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateInboundOrderLinesDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixInboundOrderLine > 0)
                                            {
                                                await dc.BeginDialogAsync(EditInboundOrderLinesDialogId, _inboundorderlinesService.GetPost(currentBotUserData.ixInboundOrderLine), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no inboundorderlines selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindInboundOrderLinesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixInboundOrderLine > 0)
                                            {
                                                _inboundorderlinesPost = _inboundorderlinesService.GetPost(currentBotUserData.ixInboundOrderLine);
                                                existInEntities = _inboundorderlinesService.VerifyInboundOrderLineDeleteOK(_inboundorderlinesPost.ixInboundOrderLine, _inboundorderlinesPost.sInboundOrderLine);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteInboundOrderLinesDialogId, _inboundorderlinesPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The InboundOrderLine {_inboundorderlinesPost.sInboundOrderLine} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the InboundOrderLine, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no inboundorderlines selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindInboundOrderLinesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixInboundOrderLine > 0)
                                            {
                                                _inboundorderlinesPost = _inboundorderlinesService.GetPost(currentBotUserData.ixInboundOrderLine);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for InboundOrderLine ID {currentBotUserData.ixInboundOrderLine}: {System.Environment.NewLine}  InboundOrderLine: {_inboundorderlinesPost.sInboundOrderLine.ToString() + System.Environment.NewLine}  InboundOrder: {_inboundorderlinesPost.ixInboundOrder.ToString() + System.Environment.NewLine}  OrderLineReference: {_inboundorderlinesPost.sOrderLineReference.ToString() + System.Environment.NewLine}  Material: {_inboundorderlinesPost.ixMaterial.ToString() + System.Environment.NewLine}  MaterialHandlingUnitConfiguration: {_inboundorderlinesPost.ixMaterialHandlingUnitConfiguration.ToString() + System.Environment.NewLine}  HandlingUnitType: {_inboundorderlinesPost.ixHandlingUnitType.ToString() + System.Environment.NewLine}  HandlingUnitQuantity: {_inboundorderlinesPost.nHandlingUnitQuantity.ToString() + System.Environment.NewLine}  BaseUnitQuantityExpected: {_inboundorderlinesPost.nBaseUnitQuantityExpected.ToString() + System.Environment.NewLine}  BaseUnitQuantityReceived: {_inboundorderlinesPost.nBaseUnitQuantityReceived.ToString() + System.Environment.NewLine}  BatchNumber: {_inboundorderlinesPost.sBatchNumber.ToString() + System.Environment.NewLine}  SerialNumber: {_inboundorderlinesPost.sSerialNumber.ToString() + System.Environment.NewLine}  Status: {_inboundorderlinesPost.ixStatus.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no inboundorderlines selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindInboundOrderLinesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindInboundOrderLinesDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "Carriers":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateCarriersDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixCarrier > 0)
                                            {
                                                await dc.BeginDialogAsync(EditCarriersDialogId, _carriersService.GetPost(currentBotUserData.ixCarrier), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no carriers selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindCarriersDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixCarrier > 0)
                                            {
                                                _carriersPost = _carriersService.GetPost(currentBotUserData.ixCarrier);
                                                existInEntities = _carriersService.VerifyCarrierDeleteOK(_carriersPost.ixCarrier, _carriersPost.sCarrier);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteCarriersDialogId, _carriersPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The Carrier {_carriersPost.sCarrier} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the Carrier, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no carriers selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindCarriersDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixCarrier > 0)
                                            {
                                                _carriersPost = _carriersService.GetPost(currentBotUserData.ixCarrier);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for Carrier ID {currentBotUserData.ixCarrier}: {System.Environment.NewLine}  Carrier: {_carriersPost.sCarrier.ToString() + System.Environment.NewLine}  CarrierType: {_carriersPost.ixCarrierType.ToString() + System.Environment.NewLine}  StandardCarrierAlphaCode: {_carriersPost.sStandardCarrierAlphaCode.ToString() + System.Environment.NewLine}  CarrierConsignmentNumberPrefix: {_carriersPost.sCarrierConsignmentNumberPrefix.ToString() + System.Environment.NewLine}  CarrierConsignmentNumberStart: {_carriersPost.nCarrierConsignmentNumberStart.ToString() + System.Environment.NewLine}  CarrierConsignmentNumberLastUsed: {_carriersPost.nCarrierConsignmentNumberLastUsed.ToString() + System.Environment.NewLine}  ScheduledPickupTime: {_carriersPost.dtScheduledPickupTime.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no carriers selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindCarriersDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindCarriersDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "CarrierServices":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateCarrierServicesDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixCarrierService > 0)
                                            {
                                                await dc.BeginDialogAsync(EditCarrierServicesDialogId, _carrierservicesService.GetPost(currentBotUserData.ixCarrierService), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no carrierservices selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindCarrierServicesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixCarrierService > 0)
                                            {
                                                _carrierservicesPost = _carrierservicesService.GetPost(currentBotUserData.ixCarrierService);
                                                existInEntities = _carrierservicesService.VerifyCarrierServiceDeleteOK(_carrierservicesPost.ixCarrierService, _carrierservicesPost.sCarrierService);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteCarrierServicesDialogId, _carrierservicesPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The CarrierService {_carrierservicesPost.sCarrierService} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the CarrierService, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no carrierservices selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindCarrierServicesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixCarrierService > 0)
                                            {
                                                _carrierservicesPost = _carrierservicesService.GetPost(currentBotUserData.ixCarrierService);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for CarrierService ID {currentBotUserData.ixCarrierService}: {System.Environment.NewLine}  CarrierService: {_carrierservicesPost.sCarrierService.ToString() + System.Environment.NewLine}  Carrier: {_carrierservicesPost.ixCarrier.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no carrierservices selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindCarrierServicesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindCarrierServicesDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "OutboundOrders":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateOutboundOrdersDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixOutboundOrder > 0)
                                            {
                                                await dc.BeginDialogAsync(EditOutboundOrdersDialogId, _outboundordersService.GetPost(currentBotUserData.ixOutboundOrder), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no outboundorders selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindOutboundOrdersDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixOutboundOrder > 0)
                                            {
                                                _outboundordersPost = _outboundordersService.GetPost(currentBotUserData.ixOutboundOrder);
                                                existInEntities = _outboundordersService.VerifyOutboundOrderDeleteOK(_outboundordersPost.ixOutboundOrder, _outboundordersPost.sOutboundOrder);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteOutboundOrdersDialogId, _outboundordersPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The OutboundOrder {_outboundordersPost.sOutboundOrder} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the OutboundOrder, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no outboundorders selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindOutboundOrdersDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixOutboundOrder > 0)
                                            {
                                                _outboundordersPost = _outboundordersService.GetPost(currentBotUserData.ixOutboundOrder);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for OutboundOrder ID {currentBotUserData.ixOutboundOrder}: {System.Environment.NewLine}  OutboundOrder: {_outboundordersPost.sOutboundOrder.ToString() + System.Environment.NewLine}  OrderReference: {_outboundordersPost.sOrderReference.ToString() + System.Environment.NewLine}  OutboundOrderType: {_outboundordersPost.ixOutboundOrderType.ToString() + System.Environment.NewLine}  Facility: {_outboundordersPost.ixFacility.ToString() + System.Environment.NewLine}  Company: {_outboundordersPost.ixCompany.ToString() + System.Environment.NewLine}  BusinessPartner: {_outboundordersPost.ixBusinessPartner.ToString() + System.Environment.NewLine}  DeliverEarliest: {_outboundordersPost.dtDeliverEarliest.ToString() + System.Environment.NewLine}  DeliverLatest: {_outboundordersPost.dtDeliverLatest.ToString() + System.Environment.NewLine}  CarrierService: {_outboundordersPost.ixCarrierService.ToString() + System.Environment.NewLine}  Status: {_outboundordersPost.ixStatus.ToString() + System.Environment.NewLine}  PickBatch: {_outboundordersPost.ixPickBatch.ToString() + System.Environment.NewLine}  OutboundShipment: {_outboundordersPost.ixOutboundShipment.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no outboundorders selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindOutboundOrdersDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindOutboundOrdersDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "OutboundShipments":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateOutboundShipmentsDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixOutboundShipment > 0)
                                            {
                                                await dc.BeginDialogAsync(EditOutboundShipmentsDialogId, _outboundshipmentsService.GetPost(currentBotUserData.ixOutboundShipment), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no outboundshipments selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindOutboundShipmentsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixOutboundShipment > 0)
                                            {
                                                _outboundshipmentsPost = _outboundshipmentsService.GetPost(currentBotUserData.ixOutboundShipment);
                                                existInEntities = _outboundshipmentsService.VerifyOutboundShipmentDeleteOK(_outboundshipmentsPost.ixOutboundShipment, _outboundshipmentsPost.sOutboundShipment);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteOutboundShipmentsDialogId, _outboundshipmentsPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The OutboundShipment {_outboundshipmentsPost.sOutboundShipment} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the OutboundShipment, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no outboundshipments selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindOutboundShipmentsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixOutboundShipment > 0)
                                            {
                                                _outboundshipmentsPost = _outboundshipmentsService.GetPost(currentBotUserData.ixOutboundShipment);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for OutboundShipment ID {currentBotUserData.ixOutboundShipment}: {System.Environment.NewLine}  OutboundShipment: {_outboundshipmentsPost.sOutboundShipment.ToString() + System.Environment.NewLine}  Facility: {_outboundshipmentsPost.ixFacility.ToString() + System.Environment.NewLine}  Company: {_outboundshipmentsPost.ixCompany.ToString() + System.Environment.NewLine}  Carrier: {_outboundshipmentsPost.ixCarrier.ToString() + System.Environment.NewLine}  CarrierConsignmentNumber: {_outboundshipmentsPost.sCarrierConsignmentNumber.ToString() + System.Environment.NewLine}  Status: {_outboundshipmentsPost.ixStatus.ToString() + System.Environment.NewLine}  Address: {_outboundshipmentsPost.ixAddress.ToString() + System.Environment.NewLine}  OutboundCarrierManifest: {_outboundshipmentsPost.ixOutboundCarrierManifest.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no outboundshipments selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindOutboundShipmentsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindOutboundShipmentsDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "OutboundCarrierManifests":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateOutboundCarrierManifestsDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixOutboundCarrierManifest > 0)
                                            {
                                                await dc.BeginDialogAsync(EditOutboundCarrierManifestsDialogId, _outboundcarriermanifestsService.GetPost(currentBotUserData.ixOutboundCarrierManifest), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no outboundcarriermanifests selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindOutboundCarrierManifestsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixOutboundCarrierManifest > 0)
                                            {
                                                _outboundcarriermanifestsPost = _outboundcarriermanifestsService.GetPost(currentBotUserData.ixOutboundCarrierManifest);
                                                existInEntities = _outboundcarriermanifestsService.VerifyOutboundCarrierManifestDeleteOK(_outboundcarriermanifestsPost.ixOutboundCarrierManifest, _outboundcarriermanifestsPost.sOutboundCarrierManifest);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteOutboundCarrierManifestsDialogId, _outboundcarriermanifestsPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The OutboundCarrierManifest {_outboundcarriermanifestsPost.sOutboundCarrierManifest} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the OutboundCarrierManifest, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no outboundcarriermanifests selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindOutboundCarrierManifestsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixOutboundCarrierManifest > 0)
                                            {
                                                _outboundcarriermanifestsPost = _outboundcarriermanifestsService.GetPost(currentBotUserData.ixOutboundCarrierManifest);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for OutboundCarrierManifest ID {currentBotUserData.ixOutboundCarrierManifest}: {System.Environment.NewLine}  OutboundCarrierManifest: {_outboundcarriermanifestsPost.sOutboundCarrierManifest.ToString() + System.Environment.NewLine}  Carrier: {_outboundcarriermanifestsPost.ixCarrier.ToString() + System.Environment.NewLine}  PickupInventoryLocation: {_outboundcarriermanifestsPost.ixPickupInventoryLocation.ToString() + System.Environment.NewLine}  ScheduledPickupAt: {_outboundcarriermanifestsPost.dtScheduledPickupAt.ToString() + System.Environment.NewLine}  Status: {_outboundcarriermanifestsPost.ixStatus.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no outboundcarriermanifests selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindOutboundCarrierManifestsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindOutboundCarrierManifestsDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "OutboundOrderLines":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateOutboundOrderLinesDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixOutboundOrderLine > 0)
                                            {
                                                await dc.BeginDialogAsync(EditOutboundOrderLinesDialogId, _outboundorderlinesService.GetPost(currentBotUserData.ixOutboundOrderLine), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no outboundorderlines selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindOutboundOrderLinesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixOutboundOrderLine > 0)
                                            {
                                                _outboundorderlinesPost = _outboundorderlinesService.GetPost(currentBotUserData.ixOutboundOrderLine);
                                                existInEntities = _outboundorderlinesService.VerifyOutboundOrderLineDeleteOK(_outboundorderlinesPost.ixOutboundOrderLine, _outboundorderlinesPost.sOutboundOrderLine);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteOutboundOrderLinesDialogId, _outboundorderlinesPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The OutboundOrderLine {_outboundorderlinesPost.sOutboundOrderLine} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the OutboundOrderLine, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no outboundorderlines selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindOutboundOrderLinesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixOutboundOrderLine > 0)
                                            {
                                                _outboundorderlinesPost = _outboundorderlinesService.GetPost(currentBotUserData.ixOutboundOrderLine);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for OutboundOrderLine ID {currentBotUserData.ixOutboundOrderLine}: {System.Environment.NewLine}  OutboundOrderLine: {_outboundorderlinesPost.sOutboundOrderLine.ToString() + System.Environment.NewLine}  OrderLineReference: {_outboundorderlinesPost.sOrderLineReference.ToString() + System.Environment.NewLine}  Material: {_outboundorderlinesPost.ixMaterial.ToString() + System.Environment.NewLine}  BatchNumber: {_outboundorderlinesPost.sBatchNumber.ToString() + System.Environment.NewLine}  SerialNumber: {_outboundorderlinesPost.sSerialNumber.ToString() + System.Environment.NewLine}  BaseUnitQuantityOrdered: {_outboundorderlinesPost.nBaseUnitQuantityOrdered.ToString() + System.Environment.NewLine}  BaseUnitQuantityShipped: {_outboundorderlinesPost.nBaseUnitQuantityShipped.ToString() + System.Environment.NewLine}  Status: {_outboundorderlinesPost.ixStatus.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no outboundorderlines selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindOutboundOrderLinesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindOutboundOrderLinesDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "PickBatches":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreatePickBatchesDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixPickBatch > 0)
                                            {
                                                await dc.BeginDialogAsync(EditPickBatchesDialogId, _pickbatchesService.GetPost(currentBotUserData.ixPickBatch), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no pickbatches selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindPickBatchesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixPickBatch > 0)
                                            {
                                                _pickbatchesPost = _pickbatchesService.GetPost(currentBotUserData.ixPickBatch);
                                                existInEntities = _pickbatchesService.VerifyPickBatchDeleteOK(_pickbatchesPost.ixPickBatch, _pickbatchesPost.sPickBatch);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeletePickBatchesDialogId, _pickbatchesPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The PickBatch {_pickbatchesPost.sPickBatch} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the PickBatch, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no pickbatches selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindPickBatchesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixPickBatch > 0)
                                            {
                                                _pickbatchesPost = _pickbatchesService.GetPost(currentBotUserData.ixPickBatch);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for PickBatch ID {currentBotUserData.ixPickBatch}: {System.Environment.NewLine}  PickBatch: {_pickbatchesPost.sPickBatch.ToString() + System.Environment.NewLine}  PickBatchType: {_pickbatchesPost.ixPickBatchType.ToString() + System.Environment.NewLine}  MultiResource: {_pickbatchesPost.bMultiResource.ToString() + System.Environment.NewLine}  StartBy: {_pickbatchesPost.dtStartBy.ToString() + System.Environment.NewLine}  CompleteBy: {_pickbatchesPost.dtCompleteBy.ToString() + System.Environment.NewLine}  Status: {_pickbatchesPost.ixStatus.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no pickbatches selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindPickBatchesDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindPickBatchesDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "Receiving":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateReceivingDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixReceipt > 0)
                                            {
                                                await dc.BeginDialogAsync(EditReceivingDialogId, _receivingService.GetPost(currentBotUserData.ixReceipt), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no receiving selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindReceivingDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixReceipt > 0)
                                            {
                                                _receivingPost = _receivingService.GetPost(currentBotUserData.ixReceipt);
                                                existInEntities = _receivingService.VerifyReceiptDeleteOK(_receivingPost.ixReceipt, _receivingPost.sReceipt);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteReceivingDialogId, _receivingPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The Receipt {_receivingPost.sReceipt} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the Receipt, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no receiving selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindReceivingDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixReceipt > 0)
                                            {
                                                _receivingPost = _receivingService.GetPost(currentBotUserData.ixReceipt);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for Receipt ID {currentBotUserData.ixReceipt}: {System.Environment.NewLine}  Receipt: {_receivingPost.sReceipt.ToString() + System.Environment.NewLine}  InventoryLocation: {_receivingPost.ixInventoryLocation.ToString() + System.Environment.NewLine}  InboundOrder: {_receivingPost.ixInboundOrder.ToString() + System.Environment.NewLine}  HandlingUnit: {_receivingPost.ixHandlingUnit.ToString() + System.Environment.NewLine}  Material: {_receivingPost.ixMaterial.ToString() + System.Environment.NewLine}  MaterialHandlingUnitConfiguration: {_receivingPost.ixMaterialHandlingUnitConfiguration.ToString() + System.Environment.NewLine}  HandlingUnitType: {_receivingPost.ixHandlingUnitType.ToString() + System.Environment.NewLine}  HandlingUnitQuantity: {_receivingPost.nHandlingUnitQuantity.ToString() + System.Environment.NewLine}  BatchNumber: {_receivingPost.sBatchNumber.ToString() + System.Environment.NewLine}  SerialNumber: {_receivingPost.sSerialNumber.ToString() + System.Environment.NewLine}  BaseUnitQuantityReceived: {_receivingPost.nBaseUnitQuantityReceived.ToString() + System.Environment.NewLine}  Status: {_receivingPost.ixStatus.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no receiving selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindReceivingDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindReceivingDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "OutboundCarrierManifestPickups":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateOutboundCarrierManifestPickupsDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixOutboundCarrierManifestPickup > 0)
                                            {
                                                await dc.BeginDialogAsync(EditOutboundCarrierManifestPickupsDialogId, _outboundcarriermanifestpickupsService.GetPost(currentBotUserData.ixOutboundCarrierManifestPickup), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no outboundcarriermanifestpickups selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindOutboundCarrierManifestPickupsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixOutboundCarrierManifestPickup > 0)
                                            {
                                                _outboundcarriermanifestpickupsPost = _outboundcarriermanifestpickupsService.GetPost(currentBotUserData.ixOutboundCarrierManifestPickup);
                                                existInEntities = _outboundcarriermanifestpickupsService.VerifyOutboundCarrierManifestPickupDeleteOK(_outboundcarriermanifestpickupsPost.ixOutboundCarrierManifestPickup, _outboundcarriermanifestpickupsPost.sOutboundCarrierManifestPickup);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteOutboundCarrierManifestPickupsDialogId, _outboundcarriermanifestpickupsPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The OutboundCarrierManifestPickup {_outboundcarriermanifestpickupsPost.sOutboundCarrierManifestPickup} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the OutboundCarrierManifestPickup, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no outboundcarriermanifestpickups selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindOutboundCarrierManifestPickupsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixOutboundCarrierManifestPickup > 0)
                                            {
                                                _outboundcarriermanifestpickupsPost = _outboundcarriermanifestpickupsService.GetPost(currentBotUserData.ixOutboundCarrierManifestPickup);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for OutboundCarrierManifestPickup ID {currentBotUserData.ixOutboundCarrierManifestPickup}: {System.Environment.NewLine}  OutboundCarrierManifestPickup: {_outboundcarriermanifestpickupsPost.sOutboundCarrierManifestPickup.ToString() + System.Environment.NewLine}  OutboundCarrierManifest: {_outboundcarriermanifestpickupsPost.ixOutboundCarrierManifest.ToString() + System.Environment.NewLine}  Status: {_outboundcarriermanifestpickupsPost.ixStatus.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no outboundcarriermanifestpickups selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindOutboundCarrierManifestPickupsDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindOutboundCarrierManifestPickupsDialogId, null, cancellationToken);
                                        }
                                        break;
                                    case "InventoryLocationsSlotting":
                                        if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                            {
                                                await dc.BeginDialogAsync(CreateInventoryLocationsSlottingDialogId, null, cancellationToken);
                                            }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                        {
                                            if (currentBotUserData.ixInventoryLocationSlotting > 0)
                                            {
                                                await dc.BeginDialogAsync(EditInventoryLocationsSlottingDialogId, _inventorylocationsslottingService.GetPost(currentBotUserData.ixInventoryLocationSlotting), cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no inventorylocationsslotting selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindInventoryLocationsSlottingDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                        {
                                            if (currentBotUserData.ixInventoryLocationSlotting > 0)
                                            {
                                                _inventorylocationsslottingPost = _inventorylocationsslottingService.GetPost(currentBotUserData.ixInventoryLocationSlotting);
                                                existInEntities = _inventorylocationsslottingService.VerifyInventoryLocationSlottingDeleteOK(_inventorylocationsslottingPost.ixInventoryLocationSlotting, _inventorylocationsslottingPost.sInventoryLocationSlotting);
                                                if (existInEntities.Any()) { DeleteOK = false; } else { DeleteOK = true; }
                                                if (DeleteOK)
                                                {
                                                    await dc.BeginDialogAsync(DeleteInventoryLocationsSlottingDialogId, _inventorylocationsslottingPost, cancellationToken);
                                                }
                                                else
                                                {
                                                    conCat = $"The InventoryLocationSlotting {_inventorylocationsslottingPost.sInventoryLocationSlotting} cannot be deleted. It is referenced by the following entities:{System.Environment.NewLine}";
                                                    foreach(var entity in existInEntities)
                                                    {
                                                        conCat += (String.Join(" ", StringSplitters.SplitCamelCase(entity.ToString())) + System.Environment.NewLine);
                                                    }
                                                    conCat += "If you want to delete the InventoryLocationSlotting, delete the dependent references first.";
                                                    await turnContext.SendActivityAsync(MessageFactory.Text(conCat), cancellationToken);
                                                }
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no inventorylocationsslotting selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindInventoryLocationsSlottingDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                        {
                                            if (currentBotUserData.ixInventoryLocationSlotting > 0)
                                            {
                                                _inventorylocationsslottingPost = _inventorylocationsslottingService.GetPost(currentBotUserData.ixInventoryLocationSlotting);
                                                await turnContext.SendActivityAsync(MessageFactory.Text($"The details for InventoryLocationSlotting ID {currentBotUserData.ixInventoryLocationSlotting}: {System.Environment.NewLine}  InventoryLocationSlotting: {_inventorylocationsslottingPost.sInventoryLocationSlotting.ToString() + System.Environment.NewLine}  InventoryLocation: {_inventorylocationsslottingPost.ixInventoryLocation.ToString() + System.Environment.NewLine}  Material: {_inventorylocationsslottingPost.ixMaterial.ToString() + System.Environment.NewLine}  MinimumBaseUnitQuantity: {_inventorylocationsslottingPost.nMinimumBaseUnitQuantity.ToString() + System.Environment.NewLine}  MaximumBaseUnitQuantity: {_inventorylocationsslottingPost.nMaximumBaseUnitQuantity.ToString() + System.Environment.NewLine}"), cancellationToken);
                                                await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                            }
                                            else
                                            {
                                                await turnContext.SendActivityAsync(MessageFactory.Text("You have no inventorylocationsslotting selected. Let's find one first."), cancellationToken);
                                                await dc.BeginDialogAsync(FindInventoryLocationsSlottingDialogId, null, cancellationToken);
                                            }
                                        }
                                        else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                        {
                                            await dc.BeginDialogAsync(FindInventoryLocationsSlottingDialogId, null, cancellationToken);
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
                        case PeoplePost peoplePost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixPerson = await _peopleService.Create(peoplePost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The Person {ixPerson} was created"), cancellationToken);
                                    currentBotUserData.ixPerson = ixPerson;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _peopleService.Edit(peoplePost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The Person {peoplePost.ixPerson} was changed"), cancellationToken);
                                    ixPerson = peoplePost.ixPerson;
                                    currentBotUserData.ixPerson = peoplePost.ixPerson;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (peoplePost.ixPerson > 0)
                                        {
                                            _peoplePost = _peopleService.GetPost(peoplePost.ixPerson);
                                            await _peopleService.Delete(_peoplePost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The Person {_peoplePost.sPerson} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _peoplePost = _peopleService.GetPost(peoplePost.ixPerson);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for Person ID {currentBotUserData.ixPerson}: {System.Environment.NewLine}  Person: {_peoplePost.sPerson.ToString() + System.Environment.NewLine}  FirstName: {_peoplePost.sFirstName.ToString() + System.Environment.NewLine}  LastName: {_peoplePost.sLastName.ToString() + System.Environment.NewLine}  Language: {_peoplePost.ixLanguage.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (peoplePost.ixPerson > 0)
                                    {
                                        ixPerson = peoplePost.ixPerson;
                                    }
                                    else if (peoplePost.ixPerson == 0)
                                    {
                                        await dc.BeginDialogAsync(FindPeopleDialogId, null, cancellationToken);
                                    }
                                    else if (peoplePost.ixPerson < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case AddressesPost addressesPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixAddress = await _addressesService.Create(addressesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The Address {ixAddress} was created"), cancellationToken);
                                    currentBotUserData.ixAddress = ixAddress;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _addressesService.Edit(addressesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The Address {addressesPost.ixAddress} was changed"), cancellationToken);
                                    ixAddress = addressesPost.ixAddress;
                                    currentBotUserData.ixAddress = addressesPost.ixAddress;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (addressesPost.ixAddress > 0)
                                        {
                                            _addressesPost = _addressesService.GetPost(addressesPost.ixAddress);
                                            await _addressesService.Delete(_addressesPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The Address {_addressesPost.sAddress} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _addressesPost = _addressesService.GetPost(addressesPost.ixAddress);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for Address ID {currentBotUserData.ixAddress}: {System.Environment.NewLine}  Address: {_addressesPost.sAddress.ToString() + System.Environment.NewLine}  StreetAndNumberOrPostOfficeBoxOne: {_addressesPost.sStreetAndNumberOrPostOfficeBoxOne.ToString() + System.Environment.NewLine}  StreetAndNumberOrPostOfficeBoxTwo: {_addressesPost.sStreetAndNumberOrPostOfficeBoxTwo.ToString() + System.Environment.NewLine}  StreetAndNumberOrPostOfficeBoxThree: {_addressesPost.sStreetAndNumberOrPostOfficeBoxThree.ToString() + System.Environment.NewLine}  CityOrSuburb: {_addressesPost.sCityOrSuburb.ToString() + System.Environment.NewLine}  ZipOrPostCode: {_addressesPost.sZipOrPostCode.ToString() + System.Environment.NewLine}  StateOrProvince: {_addressesPost.ixStateOrProvince.ToString() + System.Environment.NewLine}  Country: {_addressesPost.ixCountry.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (addressesPost.ixAddress > 0)
                                    {
                                        ixAddress = addressesPost.ixAddress;
                                    }
                                    else if (addressesPost.ixAddress == 0)
                                    {
                                        await dc.BeginDialogAsync(FindAddressesDialogId, null, cancellationToken);
                                    }
                                    else if (addressesPost.ixAddress < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case CompaniesPost companiesPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixCompany = await _companiesService.Create(companiesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The Company {ixCompany} was created"), cancellationToken);
                                    currentBotUserData.ixCompany = ixCompany;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _companiesService.Edit(companiesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The Company {companiesPost.ixCompany} was changed"), cancellationToken);
                                    ixCompany = companiesPost.ixCompany;
                                    currentBotUserData.ixCompany = companiesPost.ixCompany;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (companiesPost.ixCompany > 0)
                                        {
                                            _companiesPost = _companiesService.GetPost(companiesPost.ixCompany);
                                            await _companiesService.Delete(_companiesPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The Company {_companiesPost.sCompany} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _companiesPost = _companiesService.GetPost(companiesPost.ixCompany);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for Company ID {currentBotUserData.ixCompany}: {System.Environment.NewLine}  Company: {_companiesPost.sCompany.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (companiesPost.ixCompany > 0)
                                    {
                                        ixCompany = companiesPost.ixCompany;
                                    }
                                    else if (companiesPost.ixCompany == 0)
                                    {
                                        await dc.BeginDialogAsync(FindCompaniesDialogId, null, cancellationToken);
                                    }
                                    else if (companiesPost.ixCompany < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case FacilitiesPost facilitiesPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixFacility = await _facilitiesService.Create(facilitiesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The Facility {ixFacility} was created"), cancellationToken);
                                    currentBotUserData.ixFacility = ixFacility;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _facilitiesService.Edit(facilitiesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The Facility {facilitiesPost.ixFacility} was changed"), cancellationToken);
                                    ixFacility = facilitiesPost.ixFacility;
                                    currentBotUserData.ixFacility = facilitiesPost.ixFacility;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (facilitiesPost.ixFacility > 0)
                                        {
                                            _facilitiesPost = _facilitiesService.GetPost(facilitiesPost.ixFacility);
                                            await _facilitiesService.Delete(_facilitiesPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The Facility {_facilitiesPost.sFacility} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _facilitiesPost = _facilitiesService.GetPost(facilitiesPost.ixFacility);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for Facility ID {currentBotUserData.ixFacility}: {System.Environment.NewLine}  Facility: {_facilitiesPost.sFacility.ToString() + System.Environment.NewLine}  Address: {_facilitiesPost.ixAddress.ToString() + System.Environment.NewLine}  Latitude: {_facilitiesPost.sLatitude.ToString() + System.Environment.NewLine}  Longitude: {_facilitiesPost.sLongitude.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (facilitiesPost.ixFacility > 0)
                                    {
                                        ixFacility = facilitiesPost.ixFacility;
                                    }
                                    else if (facilitiesPost.ixFacility == 0)
                                    {
                                        await dc.BeginDialogAsync(FindFacilitiesDialogId, null, cancellationToken);
                                    }
                                    else if (facilitiesPost.ixFacility < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case FacilityZonesPost facilityzonesPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixFacilityZone = await _facilityzonesService.Create(facilityzonesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The FacilityZone {ixFacilityZone} was created"), cancellationToken);
                                    currentBotUserData.ixFacilityZone = ixFacilityZone;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _facilityzonesService.Edit(facilityzonesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The FacilityZone {facilityzonesPost.ixFacilityZone} was changed"), cancellationToken);
                                    ixFacilityZone = facilityzonesPost.ixFacilityZone;
                                    currentBotUserData.ixFacilityZone = facilityzonesPost.ixFacilityZone;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (facilityzonesPost.ixFacilityZone > 0)
                                        {
                                            _facilityzonesPost = _facilityzonesService.GetPost(facilityzonesPost.ixFacilityZone);
                                            await _facilityzonesService.Delete(_facilityzonesPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The FacilityZone {_facilityzonesPost.sFacilityZone} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _facilityzonesPost = _facilityzonesService.GetPost(facilityzonesPost.ixFacilityZone);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for FacilityZone ID {currentBotUserData.ixFacilityZone}: {System.Environment.NewLine}  FacilityZone: {_facilityzonesPost.sFacilityZone.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (facilityzonesPost.ixFacilityZone > 0)
                                    {
                                        ixFacilityZone = facilityzonesPost.ixFacilityZone;
                                    }
                                    else if (facilityzonesPost.ixFacilityZone == 0)
                                    {
                                        await dc.BeginDialogAsync(FindFacilityZonesDialogId, null, cancellationToken);
                                    }
                                    else if (facilityzonesPost.ixFacilityZone < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case FacilityWorkAreasPost facilityworkareasPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixFacilityWorkArea = await _facilityworkareasService.Create(facilityworkareasPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The FacilityWorkArea {ixFacilityWorkArea} was created"), cancellationToken);
                                    currentBotUserData.ixFacilityWorkArea = ixFacilityWorkArea;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _facilityworkareasService.Edit(facilityworkareasPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The FacilityWorkArea {facilityworkareasPost.ixFacilityWorkArea} was changed"), cancellationToken);
                                    ixFacilityWorkArea = facilityworkareasPost.ixFacilityWorkArea;
                                    currentBotUserData.ixFacilityWorkArea = facilityworkareasPost.ixFacilityWorkArea;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (facilityworkareasPost.ixFacilityWorkArea > 0)
                                        {
                                            _facilityworkareasPost = _facilityworkareasService.GetPost(facilityworkareasPost.ixFacilityWorkArea);
                                            await _facilityworkareasService.Delete(_facilityworkareasPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The FacilityWorkArea {_facilityworkareasPost.sFacilityWorkArea} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _facilityworkareasPost = _facilityworkareasService.GetPost(facilityworkareasPost.ixFacilityWorkArea);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for FacilityWorkArea ID {currentBotUserData.ixFacilityWorkArea}: {System.Environment.NewLine}  FacilityWorkArea: {_facilityworkareasPost.sFacilityWorkArea.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (facilityworkareasPost.ixFacilityWorkArea > 0)
                                    {
                                        ixFacilityWorkArea = facilityworkareasPost.ixFacilityWorkArea;
                                    }
                                    else if (facilityworkareasPost.ixFacilityWorkArea == 0)
                                    {
                                        await dc.BeginDialogAsync(FindFacilityWorkAreasDialogId, null, cancellationToken);
                                    }
                                    else if (facilityworkareasPost.ixFacilityWorkArea < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case FacilityFloorsPost facilityfloorsPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixFacilityFloor = await _facilityfloorsService.Create(facilityfloorsPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The FacilityFloor {ixFacilityFloor} was created"), cancellationToken);
                                    currentBotUserData.ixFacilityFloor = ixFacilityFloor;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _facilityfloorsService.Edit(facilityfloorsPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The FacilityFloor {facilityfloorsPost.ixFacilityFloor} was changed"), cancellationToken);
                                    ixFacilityFloor = facilityfloorsPost.ixFacilityFloor;
                                    currentBotUserData.ixFacilityFloor = facilityfloorsPost.ixFacilityFloor;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (facilityfloorsPost.ixFacilityFloor > 0)
                                        {
                                            _facilityfloorsPost = _facilityfloorsService.GetPost(facilityfloorsPost.ixFacilityFloor);
                                            await _facilityfloorsService.Delete(_facilityfloorsPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The FacilityFloor {_facilityfloorsPost.sFacilityFloor} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _facilityfloorsPost = _facilityfloorsService.GetPost(facilityfloorsPost.ixFacilityFloor);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for FacilityFloor ID {currentBotUserData.ixFacilityFloor}: {System.Environment.NewLine}  FacilityFloor: {_facilityfloorsPost.sFacilityFloor.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (facilityfloorsPost.ixFacilityFloor > 0)
                                    {
                                        ixFacilityFloor = facilityfloorsPost.ixFacilityFloor;
                                    }
                                    else if (facilityfloorsPost.ixFacilityFloor == 0)
                                    {
                                        await dc.BeginDialogAsync(FindFacilityFloorsDialogId, null, cancellationToken);
                                    }
                                    else if (facilityfloorsPost.ixFacilityFloor < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case FacilityAisleFacesPost facilityaislefacesPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixFacilityAisleFace = await _facilityaislefacesService.Create(facilityaislefacesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The FacilityAisleFace {ixFacilityAisleFace} was created"), cancellationToken);
                                    currentBotUserData.ixFacilityAisleFace = ixFacilityAisleFace;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _facilityaislefacesService.Edit(facilityaislefacesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The FacilityAisleFace {facilityaislefacesPost.ixFacilityAisleFace} was changed"), cancellationToken);
                                    ixFacilityAisleFace = facilityaislefacesPost.ixFacilityAisleFace;
                                    currentBotUserData.ixFacilityAisleFace = facilityaislefacesPost.ixFacilityAisleFace;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (facilityaislefacesPost.ixFacilityAisleFace > 0)
                                        {
                                            _facilityaislefacesPost = _facilityaislefacesService.GetPost(facilityaislefacesPost.ixFacilityAisleFace);
                                            await _facilityaislefacesService.Delete(_facilityaislefacesPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The FacilityAisleFace {_facilityaislefacesPost.sFacilityAisleFace} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _facilityaislefacesPost = _facilityaislefacesService.GetPost(facilityaislefacesPost.ixFacilityAisleFace);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for FacilityAisleFace ID {currentBotUserData.ixFacilityAisleFace}: {System.Environment.NewLine}  FacilityAisleFace: {_facilityaislefacesPost.sFacilityAisleFace.ToString() + System.Environment.NewLine}  FacilityFloor: {_facilityaislefacesPost.ixFacilityFloor.ToString() + System.Environment.NewLine}  Sequence: {_facilityaislefacesPost.nSequence.ToString() + System.Environment.NewLine}  BaySequenceType: {_facilityaislefacesPost.ixBaySequenceType.ToString() + System.Environment.NewLine}  PairedAisleFace: {_facilityaislefacesPost.ixPairedAisleFace.ToString() + System.Environment.NewLine}  LogicalOrientation: {_facilityaislefacesPost.ixLogicalOrientation.ToString() + System.Environment.NewLine}  AisleFaceStorageType: {_facilityaislefacesPost.ixAisleFaceStorageType.ToString() + System.Environment.NewLine}  XOffset: {_facilityaislefacesPost.nXOffset.ToString() + System.Environment.NewLine}  XOffsetUnit: {_facilityaislefacesPost.ixXOffsetUnit.ToString() + System.Environment.NewLine}  YOffset: {_facilityaislefacesPost.nYOffset.ToString() + System.Environment.NewLine}  YOffsetUnit: {_facilityaislefacesPost.ixYOffsetUnit.ToString() + System.Environment.NewLine}  Levels: {_facilityaislefacesPost.nLevels.ToString() + System.Environment.NewLine}  DefaultNumberOfBays: {_facilityaislefacesPost.nDefaultNumberOfBays.ToString() + System.Environment.NewLine}  DefaultNumberOfSlotsInBay: {_facilityaislefacesPost.nDefaultNumberOfSlotsInBay.ToString() + System.Environment.NewLine}  DefaultFacilityZone: {_facilityaislefacesPost.ixDefaultFacilityZone.ToString() + System.Environment.NewLine}  DefaultLocationFunction: {_facilityaislefacesPost.ixDefaultLocationFunction.ToString() + System.Environment.NewLine}  DefaultInventoryLocationSize: {_facilityaislefacesPost.ixDefaultInventoryLocationSize.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (facilityaislefacesPost.ixFacilityAisleFace > 0)
                                    {
                                        ixFacilityAisleFace = facilityaislefacesPost.ixFacilityAisleFace;
                                    }
                                    else if (facilityaislefacesPost.ixFacilityAisleFace == 0)
                                    {
                                        await dc.BeginDialogAsync(FindFacilityAisleFacesDialogId, null, cancellationToken);
                                    }
                                    else if (facilityaislefacesPost.ixFacilityAisleFace < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case InventoryLocationSizesPost inventorylocationsizesPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixInventoryLocationSize = await _inventorylocationsizesService.Create(inventorylocationsizesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The InventoryLocationSize {ixInventoryLocationSize} was created"), cancellationToken);
                                    currentBotUserData.ixInventoryLocationSize = ixInventoryLocationSize;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _inventorylocationsizesService.Edit(inventorylocationsizesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The InventoryLocationSize {inventorylocationsizesPost.ixInventoryLocationSize} was changed"), cancellationToken);
                                    ixInventoryLocationSize = inventorylocationsizesPost.ixInventoryLocationSize;
                                    currentBotUserData.ixInventoryLocationSize = inventorylocationsizesPost.ixInventoryLocationSize;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (inventorylocationsizesPost.ixInventoryLocationSize > 0)
                                        {
                                            _inventorylocationsizesPost = _inventorylocationsizesService.GetPost(inventorylocationsizesPost.ixInventoryLocationSize);
                                            await _inventorylocationsizesService.Delete(_inventorylocationsizesPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The InventoryLocationSize {_inventorylocationsizesPost.sInventoryLocationSize} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _inventorylocationsizesPost = _inventorylocationsizesService.GetPost(inventorylocationsizesPost.ixInventoryLocationSize);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for InventoryLocationSize ID {currentBotUserData.ixInventoryLocationSize}: {System.Environment.NewLine}  InventoryLocationSize: {_inventorylocationsizesPost.sInventoryLocationSize.ToString() + System.Environment.NewLine}  Length: {_inventorylocationsizesPost.nLength.ToString() + System.Environment.NewLine}  LengthUnit: {_inventorylocationsizesPost.ixLengthUnit.ToString() + System.Environment.NewLine}  Width: {_inventorylocationsizesPost.nWidth.ToString() + System.Environment.NewLine}  WidthUnit: {_inventorylocationsizesPost.ixWidthUnit.ToString() + System.Environment.NewLine}  Height: {_inventorylocationsizesPost.nHeight.ToString() + System.Environment.NewLine}  HeightUnit: {_inventorylocationsizesPost.ixHeightUnit.ToString() + System.Environment.NewLine}  UsableVolume: {_inventorylocationsizesPost.nUsableVolume.ToString() + System.Environment.NewLine}  UsableVolumeUnit: {_inventorylocationsizesPost.ixUsableVolumeUnit.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (inventorylocationsizesPost.ixInventoryLocationSize > 0)
                                    {
                                        ixInventoryLocationSize = inventorylocationsizesPost.ixInventoryLocationSize;
                                    }
                                    else if (inventorylocationsizesPost.ixInventoryLocationSize == 0)
                                    {
                                        await dc.BeginDialogAsync(FindInventoryLocationSizesDialogId, null, cancellationToken);
                                    }
                                    else if (inventorylocationsizesPost.ixInventoryLocationSize < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case MaterialsPost materialsPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixMaterial = await _materialsService.Create(materialsPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The Material {ixMaterial} was created"), cancellationToken);
                                    currentBotUserData.ixMaterial = ixMaterial;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _materialsService.Edit(materialsPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The Material {materialsPost.ixMaterial} was changed"), cancellationToken);
                                    ixMaterial = materialsPost.ixMaterial;
                                    currentBotUserData.ixMaterial = materialsPost.ixMaterial;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (materialsPost.ixMaterial > 0)
                                        {
                                            _materialsPost = _materialsService.GetPost(materialsPost.ixMaterial);
                                            await _materialsService.Delete(_materialsPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The Material {_materialsPost.sMaterial} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _materialsPost = _materialsService.GetPost(materialsPost.ixMaterial);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for Material ID {currentBotUserData.ixMaterial}: {System.Environment.NewLine}  Material: {_materialsPost.sMaterial.ToString() + System.Environment.NewLine}  Description: {_materialsPost.sDescription.ToString() + System.Environment.NewLine}  MaterialType: {_materialsPost.ixMaterialType.ToString() + System.Environment.NewLine}  BaseUnit: {_materialsPost.ixBaseUnit.ToString() + System.Environment.NewLine}  TrackSerialNumber: {_materialsPost.bTrackSerialNumber.ToString() + System.Environment.NewLine}  TrackBatchNumber: {_materialsPost.bTrackBatchNumber.ToString() + System.Environment.NewLine}  TrackExpiry: {_materialsPost.bTrackExpiry.ToString() + System.Environment.NewLine}  Density: {_materialsPost.nDensity.ToString() + System.Environment.NewLine}  DensityUnit: {_materialsPost.ixDensityUnit.ToString() + System.Environment.NewLine}  Shelflife: {_materialsPost.nShelflife.ToString() + System.Environment.NewLine}  ShelflifeUnit: {_materialsPost.ixShelflifeUnit.ToString() + System.Environment.NewLine}  Length: {_materialsPost.nLength.ToString() + System.Environment.NewLine}  LengthUnit: {_materialsPost.ixLengthUnit.ToString() + System.Environment.NewLine}  Width: {_materialsPost.nWidth.ToString() + System.Environment.NewLine}  WidthUnit: {_materialsPost.ixWidthUnit.ToString() + System.Environment.NewLine}  Height: {_materialsPost.nHeight.ToString() + System.Environment.NewLine}  HeightUnit: {_materialsPost.ixHeightUnit.ToString() + System.Environment.NewLine}  Weight: {_materialsPost.nWeight.ToString() + System.Environment.NewLine}  WeightUnit: {_materialsPost.ixWeightUnit.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (materialsPost.ixMaterial > 0)
                                    {
                                        ixMaterial = materialsPost.ixMaterial;
                                    }
                                    else if (materialsPost.ixMaterial == 0)
                                    {
                                        await dc.BeginDialogAsync(FindMaterialsDialogId, null, cancellationToken);
                                    }
                                    else if (materialsPost.ixMaterial < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case InventoryUnitsPost inventoryunitsPost:
                            //Custom Code Start | Added Code Block
                            var ixInventoryUnitTransactionContext = _inventoryunittransactioncontextsService.Index().Where(x => x.sInventoryUnitTransactionContext == "Inventory Adjustment").Select(x => x.ixInventoryUnitTransactionContext).FirstOrDefault();
                            //Custom Code End
                            if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    //Custom Code Start | Replaced Code Block
                                    //Replaced Code Block Start
                                    //ixInventoryUnit = await _inventoryunitsService.Create(inventoryunitsPost);
                                    //Replaced Code Block End
                                    ixInventoryUnit = await _inventoryunitsService.Create(inventoryunitsPost, ixInventoryUnitTransactionContext);
                                    //Custom Code End
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The InventoryUnit {ixInventoryUnit} was created"), cancellationToken);
                                    currentBotUserData.ixInventoryUnit = ixInventoryUnit;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {

                                    //Custom Code Start | Replaced Code Block
                                    //Replaced Code Block Start
                                    //await _inventoryunitsService.Edit(inventoryunitsPost);
                                    //Replaced Code Block End
                                    await _inventoryunitsService.Edit(inventoryunitsPost, ixInventoryUnitTransactionContext);
                                    //Custom Code End
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The InventoryUnit {inventoryunitsPost.ixInventoryUnit} was changed"), cancellationToken);
                                    ixInventoryUnit = inventoryunitsPost.ixInventoryUnit;
                                    currentBotUserData.ixInventoryUnit = inventoryunitsPost.ixInventoryUnit;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (inventoryunitsPost.ixInventoryUnit > 0)
                                        {
                                            _inventoryunitsPost = _inventoryunitsService.GetPost(inventoryunitsPost.ixInventoryUnit);
                                            await _inventoryunitsService.Delete(_inventoryunitsPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The InventoryUnit {_inventoryunitsPost.sInventoryUnit} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _inventoryunitsPost = _inventoryunitsService.GetPost(inventoryunitsPost.ixInventoryUnit);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for InventoryUnit ID {currentBotUserData.ixInventoryUnit}: {System.Environment.NewLine}  InventoryUnit: {_inventoryunitsPost.sInventoryUnit.ToString() + System.Environment.NewLine}  Facility: {_inventoryunitsPost.ixFacility.ToString() + System.Environment.NewLine}  Company: {_inventoryunitsPost.ixCompany.ToString() + System.Environment.NewLine}  Material: {_inventoryunitsPost.ixMaterial.ToString() + System.Environment.NewLine}  InventoryState: {_inventoryunitsPost.ixInventoryState.ToString() + System.Environment.NewLine}  HandlingUnit: {_inventoryunitsPost.ixHandlingUnit.ToString() + System.Environment.NewLine}  InventoryLocation: {_inventoryunitsPost.ixInventoryLocation.ToString() + System.Environment.NewLine}  BaseUnitQuantity: {_inventoryunitsPost.nBaseUnitQuantity.ToString() + System.Environment.NewLine}  SerialNumber: {_inventoryunitsPost.sSerialNumber.ToString() + System.Environment.NewLine}  BatchNumber: {_inventoryunitsPost.sBatchNumber.ToString() + System.Environment.NewLine}  ExpireAt: {_inventoryunitsPost.dtExpireAt.ToString() + System.Environment.NewLine}  Status: {_inventoryunitsPost.ixStatus.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (inventoryunitsPost.ixInventoryUnit > 0)
                                    {
                                        ixInventoryUnit = inventoryunitsPost.ixInventoryUnit;
                                    }
                                    else if (inventoryunitsPost.ixInventoryUnit == 0)
                                    {
                                        await dc.BeginDialogAsync(FindInventoryUnitsDialogId, null, cancellationToken);
                                    }
                                    else if (inventoryunitsPost.ixInventoryUnit < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case HandlingUnitsPost handlingunitsPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixHandlingUnit = await _handlingunitsService.Create(handlingunitsPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The HandlingUnit {ixHandlingUnit} was created"), cancellationToken);
                                    currentBotUserData.ixHandlingUnit = ixHandlingUnit;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _handlingunitsService.Edit(handlingunitsPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The HandlingUnit {handlingunitsPost.ixHandlingUnit} was changed"), cancellationToken);
                                    ixHandlingUnit = handlingunitsPost.ixHandlingUnit;
                                    currentBotUserData.ixHandlingUnit = handlingunitsPost.ixHandlingUnit;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (handlingunitsPost.ixHandlingUnit > 0)
                                        {
                                            _handlingunitsPost = _handlingunitsService.GetPost(handlingunitsPost.ixHandlingUnit);
                                            await _handlingunitsService.Delete(_handlingunitsPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The HandlingUnit {_handlingunitsPost.sHandlingUnit} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _handlingunitsPost = _handlingunitsService.GetPost(handlingunitsPost.ixHandlingUnit);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for HandlingUnit ID {currentBotUserData.ixHandlingUnit}: {System.Environment.NewLine}  HandlingUnit: {_handlingunitsPost.sHandlingUnit.ToString() + System.Environment.NewLine}  HandlingUnitType: {_handlingunitsPost.ixHandlingUnitType.ToString() + System.Environment.NewLine}  ParentHandlingUnit: {_handlingunitsPost.ixParentHandlingUnit.ToString() + System.Environment.NewLine}  PackingMaterial: {_handlingunitsPost.ixPackingMaterial.ToString() + System.Environment.NewLine}  MaterialHandlingUnitConfiguration: {_handlingunitsPost.ixMaterialHandlingUnitConfiguration.ToString() + System.Environment.NewLine}  Length: {_handlingunitsPost.nLength.ToString() + System.Environment.NewLine}  LengthUnit: {_handlingunitsPost.ixLengthUnit.ToString() + System.Environment.NewLine}  Width: {_handlingunitsPost.nWidth.ToString() + System.Environment.NewLine}  WidthUnit: {_handlingunitsPost.ixWidthUnit.ToString() + System.Environment.NewLine}  Height: {_handlingunitsPost.nHeight.ToString() + System.Environment.NewLine}  HeightUnit: {_handlingunitsPost.ixHeightUnit.ToString() + System.Environment.NewLine}  Weight: {_handlingunitsPost.nWeight.ToString() + System.Environment.NewLine}  Status: {_handlingunitsPost.ixStatus.ToString() + System.Environment.NewLine}  WeightUnit: {_handlingunitsPost.ixWeightUnit.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (handlingunitsPost.ixHandlingUnit > 0)
                                    {
                                        ixHandlingUnit = handlingunitsPost.ixHandlingUnit;
                                    }
                                    else if (handlingunitsPost.ixHandlingUnit == 0)
                                    {
                                        await dc.BeginDialogAsync(FindHandlingUnitsDialogId, null, cancellationToken);
                                    }
                                    else if (handlingunitsPost.ixHandlingUnit < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case InventoryLocationsPost inventorylocationsPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixInventoryLocation = await _inventorylocationsService.Create(inventorylocationsPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The InventoryLocation {ixInventoryLocation} was created"), cancellationToken);
                                    currentBotUserData.ixInventoryLocation = ixInventoryLocation;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _inventorylocationsService.Edit(inventorylocationsPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The InventoryLocation {inventorylocationsPost.ixInventoryLocation} was changed"), cancellationToken);
                                    ixInventoryLocation = inventorylocationsPost.ixInventoryLocation;
                                    currentBotUserData.ixInventoryLocation = inventorylocationsPost.ixInventoryLocation;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (inventorylocationsPost.ixInventoryLocation > 0)
                                        {
                                            _inventorylocationsPost = _inventorylocationsService.GetPost(inventorylocationsPost.ixInventoryLocation);
                                            await _inventorylocationsService.Delete(_inventorylocationsPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The InventoryLocation {_inventorylocationsPost.sInventoryLocation} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _inventorylocationsPost = _inventorylocationsService.GetPost(inventorylocationsPost.ixInventoryLocation);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for InventoryLocation ID {currentBotUserData.ixInventoryLocation}: {System.Environment.NewLine}  InventoryLocation: {_inventorylocationsPost.sInventoryLocation.ToString() + System.Environment.NewLine}  LocationFunction: {_inventorylocationsPost.ixLocationFunction.ToString() + System.Environment.NewLine}  Company: {_inventorylocationsPost.ixCompany.ToString() + System.Environment.NewLine}  FacilityFloor: {_inventorylocationsPost.ixFacilityFloor.ToString() + System.Environment.NewLine}  FacilityZone: {_inventorylocationsPost.ixFacilityZone.ToString() + System.Environment.NewLine}  FacilityWorkArea: {_inventorylocationsPost.ixFacilityWorkArea.ToString() + System.Environment.NewLine}  FacilityAisleFace: {_inventorylocationsPost.ixFacilityAisleFace.ToString() + System.Environment.NewLine}  Level: {_inventorylocationsPost.sLevel.ToString() + System.Environment.NewLine}  Bay: {_inventorylocationsPost.sBay.ToString() + System.Environment.NewLine}  Slot: {_inventorylocationsPost.sSlot.ToString() + System.Environment.NewLine}  InventoryLocationSize: {_inventorylocationsPost.ixInventoryLocationSize.ToString() + System.Environment.NewLine}  Sequence: {_inventorylocationsPost.nSequence.ToString() + System.Environment.NewLine}  XOffset: {_inventorylocationsPost.nXOffset.ToString() + System.Environment.NewLine}  XOffsetUnit: {_inventorylocationsPost.ixXOffsetUnit.ToString() + System.Environment.NewLine}  YOffset: {_inventorylocationsPost.nYOffset.ToString() + System.Environment.NewLine}  YOffsetUnit: {_inventorylocationsPost.ixYOffsetUnit.ToString() + System.Environment.NewLine}  ZOffset: {_inventorylocationsPost.nZOffset.ToString() + System.Environment.NewLine}  ZOffsetUnit: {_inventorylocationsPost.ixZOffsetUnit.ToString() + System.Environment.NewLine}  Latitude: {_inventorylocationsPost.sLatitude.ToString() + System.Environment.NewLine}  Longitude: {_inventorylocationsPost.sLongitude.ToString() + System.Environment.NewLine}  TrackUtilisation: {_inventorylocationsPost.bTrackUtilisation.ToString() + System.Environment.NewLine}  UtilisationPercent: {_inventorylocationsPost.nUtilisationPercent.ToString() + System.Environment.NewLine}  QueuedUtilisationPercent: {_inventorylocationsPost.nQueuedUtilisationPercent.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (inventorylocationsPost.ixInventoryLocation > 0)
                                    {
                                        ixInventoryLocation = inventorylocationsPost.ixInventoryLocation;
                                    }
                                    else if (inventorylocationsPost.ixInventoryLocation == 0)
                                    {
                                        await dc.BeginDialogAsync(FindInventoryLocationsDialogId, null, cancellationToken);
                                    }
                                    else if (inventorylocationsPost.ixInventoryLocation < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case MoveQueuesPost movequeuesPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixMoveQueue = await _movequeuesService.Create(movequeuesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The MoveQueue {ixMoveQueue} was created"), cancellationToken);
                                    currentBotUserData.ixMoveQueue = ixMoveQueue;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _movequeuesService.Edit(movequeuesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The MoveQueue {movequeuesPost.ixMoveQueue} was changed"), cancellationToken);
                                    ixMoveQueue = movequeuesPost.ixMoveQueue;
                                    currentBotUserData.ixMoveQueue = movequeuesPost.ixMoveQueue;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (movequeuesPost.ixMoveQueue > 0)
                                        {
                                            _movequeuesPost = _movequeuesService.GetPost(movequeuesPost.ixMoveQueue);
                                            await _movequeuesService.Delete(_movequeuesPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The MoveQueue {_movequeuesPost.sMoveQueue} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _movequeuesPost = _movequeuesService.GetPost(movequeuesPost.ixMoveQueue);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for MoveQueue ID {currentBotUserData.ixMoveQueue}: {System.Environment.NewLine}  MoveQueue: {_movequeuesPost.sMoveQueue.ToString() + System.Environment.NewLine}  MoveQueueType: {_movequeuesPost.ixMoveQueueType.ToString() + System.Environment.NewLine}  MoveQueueContext: {_movequeuesPost.ixMoveQueueContext.ToString() + System.Environment.NewLine}  SourceInventoryUnit: {_movequeuesPost.ixSourceInventoryUnit.ToString() + System.Environment.NewLine}  TargetInventoryUnit: {_movequeuesPost.ixTargetInventoryUnit.ToString() + System.Environment.NewLine}  SourceInventoryLocation: {_movequeuesPost.ixSourceInventoryLocation.ToString() + System.Environment.NewLine}  TargetInventoryLocation: {_movequeuesPost.ixTargetInventoryLocation.ToString() + System.Environment.NewLine}  SourceHandlingUnit: {_movequeuesPost.ixSourceHandlingUnit.ToString() + System.Environment.NewLine}  TargetHandlingUnit: {_movequeuesPost.ixTargetHandlingUnit.ToString() + System.Environment.NewLine}  PreferredResource: {_movequeuesPost.sPreferredResource.ToString() + System.Environment.NewLine}  BaseUnitQuantity: {_movequeuesPost.nBaseUnitQuantity.ToString() + System.Environment.NewLine}  StartBy: {_movequeuesPost.dtStartBy.ToString() + System.Environment.NewLine}  CompleteBy: {_movequeuesPost.dtCompleteBy.ToString() + System.Environment.NewLine}  StartedAt: {_movequeuesPost.dtStartedAt.ToString() + System.Environment.NewLine}  CompletedAt: {_movequeuesPost.dtCompletedAt.ToString() + System.Environment.NewLine}  InboundOrderLine: {_movequeuesPost.ixInboundOrderLine.ToString() + System.Environment.NewLine}  OutboundOrderLine: {_movequeuesPost.ixOutboundOrderLine.ToString() + System.Environment.NewLine}  PickBatch: {_movequeuesPost.ixPickBatch.ToString() + System.Environment.NewLine}  Status: {_movequeuesPost.ixStatus.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (movequeuesPost.ixMoveQueue > 0)
                                    {
                                        ixMoveQueue = movequeuesPost.ixMoveQueue;
                                    }
                                    else if (movequeuesPost.ixMoveQueue == 0)
                                    {
                                        await dc.BeginDialogAsync(FindMoveQueuesDialogId, null, cancellationToken);
                                    }
                                    else if (movequeuesPost.ixMoveQueue < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case MaterialHandlingUnitConfigurationsPost materialhandlingunitconfigurationsPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixMaterialHandlingUnitConfiguration = await _materialhandlingunitconfigurationsService.Create(materialhandlingunitconfigurationsPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The MaterialHandlingUnitConfiguration {ixMaterialHandlingUnitConfiguration} was created"), cancellationToken);
                                    currentBotUserData.ixMaterialHandlingUnitConfiguration = ixMaterialHandlingUnitConfiguration;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _materialhandlingunitconfigurationsService.Edit(materialhandlingunitconfigurationsPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The MaterialHandlingUnitConfiguration {materialhandlingunitconfigurationsPost.ixMaterialHandlingUnitConfiguration} was changed"), cancellationToken);
                                    ixMaterialHandlingUnitConfiguration = materialhandlingunitconfigurationsPost.ixMaterialHandlingUnitConfiguration;
                                    currentBotUserData.ixMaterialHandlingUnitConfiguration = materialhandlingunitconfigurationsPost.ixMaterialHandlingUnitConfiguration;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (materialhandlingunitconfigurationsPost.ixMaterialHandlingUnitConfiguration > 0)
                                        {
                                            _materialhandlingunitconfigurationsPost = _materialhandlingunitconfigurationsService.GetPost(materialhandlingunitconfigurationsPost.ixMaterialHandlingUnitConfiguration);
                                            await _materialhandlingunitconfigurationsService.Delete(_materialhandlingunitconfigurationsPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The MaterialHandlingUnitConfiguration {_materialhandlingunitconfigurationsPost.sMaterialHandlingUnitConfiguration} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _materialhandlingunitconfigurationsPost = _materialhandlingunitconfigurationsService.GetPost(materialhandlingunitconfigurationsPost.ixMaterialHandlingUnitConfiguration);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for MaterialHandlingUnitConfiguration ID {currentBotUserData.ixMaterialHandlingUnitConfiguration}: {System.Environment.NewLine}  MaterialHandlingUnitConfiguration: {_materialhandlingunitconfigurationsPost.sMaterialHandlingUnitConfiguration.ToString() + System.Environment.NewLine}  Material: {_materialhandlingunitconfigurationsPost.ixMaterial.ToString() + System.Environment.NewLine}  NestingLevel: {_materialhandlingunitconfigurationsPost.nNestingLevel.ToString() + System.Environment.NewLine}  HandlingUnitType: {_materialhandlingunitconfigurationsPost.ixHandlingUnitType.ToString() + System.Environment.NewLine}  Quantity: {_materialhandlingunitconfigurationsPost.nQuantity.ToString() + System.Environment.NewLine}  Length: {_materialhandlingunitconfigurationsPost.nLength.ToString() + System.Environment.NewLine}  LengthUnit: {_materialhandlingunitconfigurationsPost.ixLengthUnit.ToString() + System.Environment.NewLine}  Width: {_materialhandlingunitconfigurationsPost.nWidth.ToString() + System.Environment.NewLine}  WidthUnit: {_materialhandlingunitconfigurationsPost.ixWidthUnit.ToString() + System.Environment.NewLine}  Height: {_materialhandlingunitconfigurationsPost.nHeight.ToString() + System.Environment.NewLine}  HeightUnit: {_materialhandlingunitconfigurationsPost.ixHeightUnit.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (materialhandlingunitconfigurationsPost.ixMaterialHandlingUnitConfiguration > 0)
                                    {
                                        ixMaterialHandlingUnitConfiguration = materialhandlingunitconfigurationsPost.ixMaterialHandlingUnitConfiguration;
                                    }
                                    else if (materialhandlingunitconfigurationsPost.ixMaterialHandlingUnitConfiguration == 0)
                                    {
                                        await dc.BeginDialogAsync(FindMaterialHandlingUnitConfigurationsDialogId, null, cancellationToken);
                                    }
                                    else if (materialhandlingunitconfigurationsPost.ixMaterialHandlingUnitConfiguration < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case BusinessPartnersPost businesspartnersPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixBusinessPartner = await _businesspartnersService.Create(businesspartnersPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The BusinessPartner {ixBusinessPartner} was created"), cancellationToken);
                                    currentBotUserData.ixBusinessPartner = ixBusinessPartner;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _businesspartnersService.Edit(businesspartnersPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The BusinessPartner {businesspartnersPost.ixBusinessPartner} was changed"), cancellationToken);
                                    ixBusinessPartner = businesspartnersPost.ixBusinessPartner;
                                    currentBotUserData.ixBusinessPartner = businesspartnersPost.ixBusinessPartner;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (businesspartnersPost.ixBusinessPartner > 0)
                                        {
                                            _businesspartnersPost = _businesspartnersService.GetPost(businesspartnersPost.ixBusinessPartner);
                                            await _businesspartnersService.Delete(_businesspartnersPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The BusinessPartner {_businesspartnersPost.sBusinessPartner} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _businesspartnersPost = _businesspartnersService.GetPost(businesspartnersPost.ixBusinessPartner);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for BusinessPartner ID {currentBotUserData.ixBusinessPartner}: {System.Environment.NewLine}  BusinessPartner: {_businesspartnersPost.sBusinessPartner.ToString() + System.Environment.NewLine}  BusinessPartnerType: {_businesspartnersPost.ixBusinessPartnerType.ToString() + System.Environment.NewLine}  Company: {_businesspartnersPost.ixCompany.ToString() + System.Environment.NewLine}  Address: {_businesspartnersPost.ixAddress.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (businesspartnersPost.ixBusinessPartner > 0)
                                    {
                                        ixBusinessPartner = businesspartnersPost.ixBusinessPartner;
                                    }
                                    else if (businesspartnersPost.ixBusinessPartner == 0)
                                    {
                                        await dc.BeginDialogAsync(FindBusinessPartnersDialogId, null, cancellationToken);
                                    }
                                    else if (businesspartnersPost.ixBusinessPartner < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case InboundOrdersPost inboundordersPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixInboundOrder = await _inboundordersService.Create(inboundordersPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The InboundOrder {ixInboundOrder} was created"), cancellationToken);
                                    currentBotUserData.ixInboundOrder = ixInboundOrder;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _inboundordersService.Edit(inboundordersPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The InboundOrder {inboundordersPost.ixInboundOrder} was changed"), cancellationToken);
                                    ixInboundOrder = inboundordersPost.ixInboundOrder;
                                    currentBotUserData.ixInboundOrder = inboundordersPost.ixInboundOrder;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (inboundordersPost.ixInboundOrder > 0)
                                        {
                                            _inboundordersPost = _inboundordersService.GetPost(inboundordersPost.ixInboundOrder);
                                            await _inboundordersService.Delete(_inboundordersPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The InboundOrder {_inboundordersPost.sInboundOrder} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _inboundordersPost = _inboundordersService.GetPost(inboundordersPost.ixInboundOrder);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for InboundOrder ID {currentBotUserData.ixInboundOrder}: {System.Environment.NewLine}  InboundOrder: {_inboundordersPost.sInboundOrder.ToString() + System.Environment.NewLine}  OrderReference: {_inboundordersPost.sOrderReference.ToString() + System.Environment.NewLine}  InboundOrderType: {_inboundordersPost.ixInboundOrderType.ToString() + System.Environment.NewLine}  Facility: {_inboundordersPost.ixFacility.ToString() + System.Environment.NewLine}  Company: {_inboundordersPost.ixCompany.ToString() + System.Environment.NewLine}  BusinessPartner: {_inboundordersPost.ixBusinessPartner.ToString() + System.Environment.NewLine}  ExpectedAt: {_inboundordersPost.dtExpectedAt.ToString() + System.Environment.NewLine}  Status: {_inboundordersPost.ixStatus.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (inboundordersPost.ixInboundOrder > 0)
                                    {
                                        ixInboundOrder = inboundordersPost.ixInboundOrder;
                                    }
                                    else if (inboundordersPost.ixInboundOrder == 0)
                                    {
                                        await dc.BeginDialogAsync(FindInboundOrdersDialogId, null, cancellationToken);
                                    }
                                    else if (inboundordersPost.ixInboundOrder < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case InboundOrderLinesPost inboundorderlinesPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixInboundOrderLine = await _inboundorderlinesService.Create(inboundorderlinesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The InboundOrderLine {ixInboundOrderLine} was created"), cancellationToken);
                                    currentBotUserData.ixInboundOrderLine = ixInboundOrderLine;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _inboundorderlinesService.Edit(inboundorderlinesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The InboundOrderLine {inboundorderlinesPost.ixInboundOrderLine} was changed"), cancellationToken);
                                    ixInboundOrderLine = inboundorderlinesPost.ixInboundOrderLine;
                                    currentBotUserData.ixInboundOrderLine = inboundorderlinesPost.ixInboundOrderLine;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (inboundorderlinesPost.ixInboundOrderLine > 0)
                                        {
                                            _inboundorderlinesPost = _inboundorderlinesService.GetPost(inboundorderlinesPost.ixInboundOrderLine);
                                            await _inboundorderlinesService.Delete(_inboundorderlinesPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The InboundOrderLine {_inboundorderlinesPost.sInboundOrderLine} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _inboundorderlinesPost = _inboundorderlinesService.GetPost(inboundorderlinesPost.ixInboundOrderLine);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for InboundOrderLine ID {currentBotUserData.ixInboundOrderLine}: {System.Environment.NewLine}  InboundOrderLine: {_inboundorderlinesPost.sInboundOrderLine.ToString() + System.Environment.NewLine}  InboundOrder: {_inboundorderlinesPost.ixInboundOrder.ToString() + System.Environment.NewLine}  OrderLineReference: {_inboundorderlinesPost.sOrderLineReference.ToString() + System.Environment.NewLine}  Material: {_inboundorderlinesPost.ixMaterial.ToString() + System.Environment.NewLine}  MaterialHandlingUnitConfiguration: {_inboundorderlinesPost.ixMaterialHandlingUnitConfiguration.ToString() + System.Environment.NewLine}  HandlingUnitType: {_inboundorderlinesPost.ixHandlingUnitType.ToString() + System.Environment.NewLine}  HandlingUnitQuantity: {_inboundorderlinesPost.nHandlingUnitQuantity.ToString() + System.Environment.NewLine}  BaseUnitQuantityExpected: {_inboundorderlinesPost.nBaseUnitQuantityExpected.ToString() + System.Environment.NewLine}  BaseUnitQuantityReceived: {_inboundorderlinesPost.nBaseUnitQuantityReceived.ToString() + System.Environment.NewLine}  BatchNumber: {_inboundorderlinesPost.sBatchNumber.ToString() + System.Environment.NewLine}  SerialNumber: {_inboundorderlinesPost.sSerialNumber.ToString() + System.Environment.NewLine}  Status: {_inboundorderlinesPost.ixStatus.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (inboundorderlinesPost.ixInboundOrderLine > 0)
                                    {
                                        ixInboundOrderLine = inboundorderlinesPost.ixInboundOrderLine;
                                    }
                                    else if (inboundorderlinesPost.ixInboundOrderLine == 0)
                                    {
                                        await dc.BeginDialogAsync(FindInboundOrderLinesDialogId, null, cancellationToken);
                                    }
                                    else if (inboundorderlinesPost.ixInboundOrderLine < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case CarriersPost carriersPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixCarrier = await _carriersService.Create(carriersPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The Carrier {ixCarrier} was created"), cancellationToken);
                                    currentBotUserData.ixCarrier = ixCarrier;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _carriersService.Edit(carriersPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The Carrier {carriersPost.ixCarrier} was changed"), cancellationToken);
                                    ixCarrier = carriersPost.ixCarrier;
                                    currentBotUserData.ixCarrier = carriersPost.ixCarrier;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (carriersPost.ixCarrier > 0)
                                        {
                                            _carriersPost = _carriersService.GetPost(carriersPost.ixCarrier);
                                            await _carriersService.Delete(_carriersPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The Carrier {_carriersPost.sCarrier} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _carriersPost = _carriersService.GetPost(carriersPost.ixCarrier);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for Carrier ID {currentBotUserData.ixCarrier}: {System.Environment.NewLine}  Carrier: {_carriersPost.sCarrier.ToString() + System.Environment.NewLine}  CarrierType: {_carriersPost.ixCarrierType.ToString() + System.Environment.NewLine}  StandardCarrierAlphaCode: {_carriersPost.sStandardCarrierAlphaCode.ToString() + System.Environment.NewLine}  CarrierConsignmentNumberPrefix: {_carriersPost.sCarrierConsignmentNumberPrefix.ToString() + System.Environment.NewLine}  CarrierConsignmentNumberStart: {_carriersPost.nCarrierConsignmentNumberStart.ToString() + System.Environment.NewLine}  CarrierConsignmentNumberLastUsed: {_carriersPost.nCarrierConsignmentNumberLastUsed.ToString() + System.Environment.NewLine}  ScheduledPickupTime: {_carriersPost.dtScheduledPickupTime.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (carriersPost.ixCarrier > 0)
                                    {
                                        ixCarrier = carriersPost.ixCarrier;
                                    }
                                    else if (carriersPost.ixCarrier == 0)
                                    {
                                        await dc.BeginDialogAsync(FindCarriersDialogId, null, cancellationToken);
                                    }
                                    else if (carriersPost.ixCarrier < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case CarrierServicesPost carrierservicesPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixCarrierService = await _carrierservicesService.Create(carrierservicesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The CarrierService {ixCarrierService} was created"), cancellationToken);
                                    currentBotUserData.ixCarrierService = ixCarrierService;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _carrierservicesService.Edit(carrierservicesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The CarrierService {carrierservicesPost.ixCarrierService} was changed"), cancellationToken);
                                    ixCarrierService = carrierservicesPost.ixCarrierService;
                                    currentBotUserData.ixCarrierService = carrierservicesPost.ixCarrierService;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (carrierservicesPost.ixCarrierService > 0)
                                        {
                                            _carrierservicesPost = _carrierservicesService.GetPost(carrierservicesPost.ixCarrierService);
                                            await _carrierservicesService.Delete(_carrierservicesPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The CarrierService {_carrierservicesPost.sCarrierService} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _carrierservicesPost = _carrierservicesService.GetPost(carrierservicesPost.ixCarrierService);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for CarrierService ID {currentBotUserData.ixCarrierService}: {System.Environment.NewLine}  CarrierService: {_carrierservicesPost.sCarrierService.ToString() + System.Environment.NewLine}  Carrier: {_carrierservicesPost.ixCarrier.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (carrierservicesPost.ixCarrierService > 0)
                                    {
                                        ixCarrierService = carrierservicesPost.ixCarrierService;
                                    }
                                    else if (carrierservicesPost.ixCarrierService == 0)
                                    {
                                        await dc.BeginDialogAsync(FindCarrierServicesDialogId, null, cancellationToken);
                                    }
                                    else if (carrierservicesPost.ixCarrierService < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case OutboundOrdersPost outboundordersPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixOutboundOrder = await _outboundordersService.Create(outboundordersPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The OutboundOrder {ixOutboundOrder} was created"), cancellationToken);
                                    currentBotUserData.ixOutboundOrder = ixOutboundOrder;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _outboundordersService.Edit(outboundordersPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The OutboundOrder {outboundordersPost.ixOutboundOrder} was changed"), cancellationToken);
                                    ixOutboundOrder = outboundordersPost.ixOutboundOrder;
                                    currentBotUserData.ixOutboundOrder = outboundordersPost.ixOutboundOrder;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (outboundordersPost.ixOutboundOrder > 0)
                                        {
                                            _outboundordersPost = _outboundordersService.GetPost(outboundordersPost.ixOutboundOrder);
                                            await _outboundordersService.Delete(_outboundordersPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The OutboundOrder {_outboundordersPost.sOutboundOrder} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _outboundordersPost = _outboundordersService.GetPost(outboundordersPost.ixOutboundOrder);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for OutboundOrder ID {currentBotUserData.ixOutboundOrder}: {System.Environment.NewLine}  OutboundOrder: {_outboundordersPost.sOutboundOrder.ToString() + System.Environment.NewLine}  OrderReference: {_outboundordersPost.sOrderReference.ToString() + System.Environment.NewLine}  OutboundOrderType: {_outboundordersPost.ixOutboundOrderType.ToString() + System.Environment.NewLine}  Facility: {_outboundordersPost.ixFacility.ToString() + System.Environment.NewLine}  Company: {_outboundordersPost.ixCompany.ToString() + System.Environment.NewLine}  BusinessPartner: {_outboundordersPost.ixBusinessPartner.ToString() + System.Environment.NewLine}  DeliverEarliest: {_outboundordersPost.dtDeliverEarliest.ToString() + System.Environment.NewLine}  DeliverLatest: {_outboundordersPost.dtDeliverLatest.ToString() + System.Environment.NewLine}  CarrierService: {_outboundordersPost.ixCarrierService.ToString() + System.Environment.NewLine}  Status: {_outboundordersPost.ixStatus.ToString() + System.Environment.NewLine}  PickBatch: {_outboundordersPost.ixPickBatch.ToString() + System.Environment.NewLine}  OutboundShipment: {_outboundordersPost.ixOutboundShipment.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (outboundordersPost.ixOutboundOrder > 0)
                                    {
                                        ixOutboundOrder = outboundordersPost.ixOutboundOrder;
                                    }
                                    else if (outboundordersPost.ixOutboundOrder == 0)
                                    {
                                        await dc.BeginDialogAsync(FindOutboundOrdersDialogId, null, cancellationToken);
                                    }
                                    else if (outboundordersPost.ixOutboundOrder < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case OutboundShipmentsPost outboundshipmentsPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixOutboundShipment = await _outboundshipmentsService.Create(outboundshipmentsPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The OutboundShipment {ixOutboundShipment} was created"), cancellationToken);
                                    currentBotUserData.ixOutboundShipment = ixOutboundShipment;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _outboundshipmentsService.Edit(outboundshipmentsPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The OutboundShipment {outboundshipmentsPost.ixOutboundShipment} was changed"), cancellationToken);
                                    ixOutboundShipment = outboundshipmentsPost.ixOutboundShipment;
                                    currentBotUserData.ixOutboundShipment = outboundshipmentsPost.ixOutboundShipment;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (outboundshipmentsPost.ixOutboundShipment > 0)
                                        {
                                            _outboundshipmentsPost = _outboundshipmentsService.GetPost(outboundshipmentsPost.ixOutboundShipment);
                                            await _outboundshipmentsService.Delete(_outboundshipmentsPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The OutboundShipment {_outboundshipmentsPost.sOutboundShipment} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _outboundshipmentsPost = _outboundshipmentsService.GetPost(outboundshipmentsPost.ixOutboundShipment);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for OutboundShipment ID {currentBotUserData.ixOutboundShipment}: {System.Environment.NewLine}  OutboundShipment: {_outboundshipmentsPost.sOutboundShipment.ToString() + System.Environment.NewLine}  Facility: {_outboundshipmentsPost.ixFacility.ToString() + System.Environment.NewLine}  Company: {_outboundshipmentsPost.ixCompany.ToString() + System.Environment.NewLine}  Carrier: {_outboundshipmentsPost.ixCarrier.ToString() + System.Environment.NewLine}  CarrierConsignmentNumber: {_outboundshipmentsPost.sCarrierConsignmentNumber.ToString() + System.Environment.NewLine}  Status: {_outboundshipmentsPost.ixStatus.ToString() + System.Environment.NewLine}  Address: {_outboundshipmentsPost.ixAddress.ToString() + System.Environment.NewLine}  OutboundCarrierManifest: {_outboundshipmentsPost.ixOutboundCarrierManifest.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (outboundshipmentsPost.ixOutboundShipment > 0)
                                    {
                                        ixOutboundShipment = outboundshipmentsPost.ixOutboundShipment;
                                    }
                                    else if (outboundshipmentsPost.ixOutboundShipment == 0)
                                    {
                                        await dc.BeginDialogAsync(FindOutboundShipmentsDialogId, null, cancellationToken);
                                    }
                                    else if (outboundshipmentsPost.ixOutboundShipment < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case OutboundCarrierManifestsPost outboundcarriermanifestsPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixOutboundCarrierManifest = await _outboundcarriermanifestsService.Create(outboundcarriermanifestsPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The OutboundCarrierManifest {ixOutboundCarrierManifest} was created"), cancellationToken);
                                    currentBotUserData.ixOutboundCarrierManifest = ixOutboundCarrierManifest;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _outboundcarriermanifestsService.Edit(outboundcarriermanifestsPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The OutboundCarrierManifest {outboundcarriermanifestsPost.ixOutboundCarrierManifest} was changed"), cancellationToken);
                                    ixOutboundCarrierManifest = outboundcarriermanifestsPost.ixOutboundCarrierManifest;
                                    currentBotUserData.ixOutboundCarrierManifest = outboundcarriermanifestsPost.ixOutboundCarrierManifest;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (outboundcarriermanifestsPost.ixOutboundCarrierManifest > 0)
                                        {
                                            _outboundcarriermanifestsPost = _outboundcarriermanifestsService.GetPost(outboundcarriermanifestsPost.ixOutboundCarrierManifest);
                                            await _outboundcarriermanifestsService.Delete(_outboundcarriermanifestsPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The OutboundCarrierManifest {_outboundcarriermanifestsPost.sOutboundCarrierManifest} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _outboundcarriermanifestsPost = _outboundcarriermanifestsService.GetPost(outboundcarriermanifestsPost.ixOutboundCarrierManifest);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for OutboundCarrierManifest ID {currentBotUserData.ixOutboundCarrierManifest}: {System.Environment.NewLine}  OutboundCarrierManifest: {_outboundcarriermanifestsPost.sOutboundCarrierManifest.ToString() + System.Environment.NewLine}  Carrier: {_outboundcarriermanifestsPost.ixCarrier.ToString() + System.Environment.NewLine}  PickupInventoryLocation: {_outboundcarriermanifestsPost.ixPickupInventoryLocation.ToString() + System.Environment.NewLine}  ScheduledPickupAt: {_outboundcarriermanifestsPost.dtScheduledPickupAt.ToString() + System.Environment.NewLine}  Status: {_outboundcarriermanifestsPost.ixStatus.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (outboundcarriermanifestsPost.ixOutboundCarrierManifest > 0)
                                    {
                                        ixOutboundCarrierManifest = outboundcarriermanifestsPost.ixOutboundCarrierManifest;
                                    }
                                    else if (outboundcarriermanifestsPost.ixOutboundCarrierManifest == 0)
                                    {
                                        await dc.BeginDialogAsync(FindOutboundCarrierManifestsDialogId, null, cancellationToken);
                                    }
                                    else if (outboundcarriermanifestsPost.ixOutboundCarrierManifest < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case OutboundOrderLinesPost outboundorderlinesPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixOutboundOrderLine = await _outboundorderlinesService.Create(outboundorderlinesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The OutboundOrderLine {ixOutboundOrderLine} was created"), cancellationToken);
                                    currentBotUserData.ixOutboundOrderLine = ixOutboundOrderLine;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _outboundorderlinesService.Edit(outboundorderlinesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The OutboundOrderLine {outboundorderlinesPost.ixOutboundOrderLine} was changed"), cancellationToken);
                                    ixOutboundOrderLine = outboundorderlinesPost.ixOutboundOrderLine;
                                    currentBotUserData.ixOutboundOrderLine = outboundorderlinesPost.ixOutboundOrderLine;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (outboundorderlinesPost.ixOutboundOrderLine > 0)
                                        {
                                            _outboundorderlinesPost = _outboundorderlinesService.GetPost(outboundorderlinesPost.ixOutboundOrderLine);
                                            await _outboundorderlinesService.Delete(_outboundorderlinesPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The OutboundOrderLine {_outboundorderlinesPost.sOutboundOrderLine} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _outboundorderlinesPost = _outboundorderlinesService.GetPost(outboundorderlinesPost.ixOutboundOrderLine);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for OutboundOrderLine ID {currentBotUserData.ixOutboundOrderLine}: {System.Environment.NewLine}  OutboundOrderLine: {_outboundorderlinesPost.sOutboundOrderLine.ToString() + System.Environment.NewLine}  OrderLineReference: {_outboundorderlinesPost.sOrderLineReference.ToString() + System.Environment.NewLine}  Material: {_outboundorderlinesPost.ixMaterial.ToString() + System.Environment.NewLine}  BatchNumber: {_outboundorderlinesPost.sBatchNumber.ToString() + System.Environment.NewLine}  SerialNumber: {_outboundorderlinesPost.sSerialNumber.ToString() + System.Environment.NewLine}  BaseUnitQuantityOrdered: {_outboundorderlinesPost.nBaseUnitQuantityOrdered.ToString() + System.Environment.NewLine}  BaseUnitQuantityShipped: {_outboundorderlinesPost.nBaseUnitQuantityShipped.ToString() + System.Environment.NewLine}  Status: {_outboundorderlinesPost.ixStatus.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (outboundorderlinesPost.ixOutboundOrderLine > 0)
                                    {
                                        ixOutboundOrderLine = outboundorderlinesPost.ixOutboundOrderLine;
                                    }
                                    else if (outboundorderlinesPost.ixOutboundOrderLine == 0)
                                    {
                                        await dc.BeginDialogAsync(FindOutboundOrderLinesDialogId, null, cancellationToken);
                                    }
                                    else if (outboundorderlinesPost.ixOutboundOrderLine < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case PickBatchesPost pickbatchesPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixPickBatch = await _pickbatchesService.Create(pickbatchesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The PickBatch {ixPickBatch} was created"), cancellationToken);
                                    currentBotUserData.ixPickBatch = ixPickBatch;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _pickbatchesService.Edit(pickbatchesPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The PickBatch {pickbatchesPost.ixPickBatch} was changed"), cancellationToken);
                                    ixPickBatch = pickbatchesPost.ixPickBatch;
                                    currentBotUserData.ixPickBatch = pickbatchesPost.ixPickBatch;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (pickbatchesPost.ixPickBatch > 0)
                                        {
                                            _pickbatchesPost = _pickbatchesService.GetPost(pickbatchesPost.ixPickBatch);
                                            await _pickbatchesService.Delete(_pickbatchesPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The PickBatch {_pickbatchesPost.sPickBatch} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _pickbatchesPost = _pickbatchesService.GetPost(pickbatchesPost.ixPickBatch);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for PickBatch ID {currentBotUserData.ixPickBatch}: {System.Environment.NewLine}  PickBatch: {_pickbatchesPost.sPickBatch.ToString() + System.Environment.NewLine}  PickBatchType: {_pickbatchesPost.ixPickBatchType.ToString() + System.Environment.NewLine}  MultiResource: {_pickbatchesPost.bMultiResource.ToString() + System.Environment.NewLine}  StartBy: {_pickbatchesPost.dtStartBy.ToString() + System.Environment.NewLine}  CompleteBy: {_pickbatchesPost.dtCompleteBy.ToString() + System.Environment.NewLine}  Status: {_pickbatchesPost.ixStatus.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (pickbatchesPost.ixPickBatch > 0)
                                    {
                                        ixPickBatch = pickbatchesPost.ixPickBatch;
                                    }
                                    else if (pickbatchesPost.ixPickBatch == 0)
                                    {
                                        await dc.BeginDialogAsync(FindPickBatchesDialogId, null, cancellationToken);
                                    }
                                    else if (pickbatchesPost.ixPickBatch < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case ReceivingPost receivingPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixReceipt = await _receivingService.Create(receivingPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The Receipt {ixReceipt} was created"), cancellationToken);
                                    currentBotUserData.ixReceipt = ixReceipt;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _receivingService.Edit(receivingPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The Receipt {receivingPost.ixReceipt} was changed"), cancellationToken);
                                    ixReceipt = receivingPost.ixReceipt;
                                    currentBotUserData.ixReceipt = receivingPost.ixReceipt;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (receivingPost.ixReceipt > 0)
                                        {
                                            _receivingPost = _receivingService.GetPost(receivingPost.ixReceipt);
                                            await _receivingService.Delete(_receivingPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The Receipt {_receivingPost.sReceipt} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _receivingPost = _receivingService.GetPost(receivingPost.ixReceipt);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for Receipt ID {currentBotUserData.ixReceipt}: {System.Environment.NewLine}  Receipt: {_receivingPost.sReceipt.ToString() + System.Environment.NewLine}  InventoryLocation: {_receivingPost.ixInventoryLocation.ToString() + System.Environment.NewLine}  InboundOrder: {_receivingPost.ixInboundOrder.ToString() + System.Environment.NewLine}  HandlingUnit: {_receivingPost.ixHandlingUnit.ToString() + System.Environment.NewLine}  Material: {_receivingPost.ixMaterial.ToString() + System.Environment.NewLine}  MaterialHandlingUnitConfiguration: {_receivingPost.ixMaterialHandlingUnitConfiguration.ToString() + System.Environment.NewLine}  HandlingUnitType: {_receivingPost.ixHandlingUnitType.ToString() + System.Environment.NewLine}  HandlingUnitQuantity: {_receivingPost.nHandlingUnitQuantity.ToString() + System.Environment.NewLine}  BatchNumber: {_receivingPost.sBatchNumber.ToString() + System.Environment.NewLine}  SerialNumber: {_receivingPost.sSerialNumber.ToString() + System.Environment.NewLine}  BaseUnitQuantityReceived: {_receivingPost.nBaseUnitQuantityReceived.ToString() + System.Environment.NewLine}  Status: {_receivingPost.ixStatus.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (receivingPost.ixReceipt > 0)
                                    {
                                        ixReceipt = receivingPost.ixReceipt;
                                    }
                                    else if (receivingPost.ixReceipt == 0)
                                    {
                                        await dc.BeginDialogAsync(FindReceivingDialogId, null, cancellationToken);
                                    }
                                    else if (receivingPost.ixReceipt < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixOutboundCarrierManifestPickup = await _outboundcarriermanifestpickupsService.Create(outboundcarriermanifestpickupsPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The OutboundCarrierManifestPickup {ixOutboundCarrierManifestPickup} was created"), cancellationToken);
                                    currentBotUserData.ixOutboundCarrierManifestPickup = ixOutboundCarrierManifestPickup;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _outboundcarriermanifestpickupsService.Edit(outboundcarriermanifestpickupsPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The OutboundCarrierManifestPickup {outboundcarriermanifestpickupsPost.ixOutboundCarrierManifestPickup} was changed"), cancellationToken);
                                    ixOutboundCarrierManifestPickup = outboundcarriermanifestpickupsPost.ixOutboundCarrierManifestPickup;
                                    currentBotUserData.ixOutboundCarrierManifestPickup = outboundcarriermanifestpickupsPost.ixOutboundCarrierManifestPickup;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (outboundcarriermanifestpickupsPost.ixOutboundCarrierManifestPickup > 0)
                                        {
                                            _outboundcarriermanifestpickupsPost = _outboundcarriermanifestpickupsService.GetPost(outboundcarriermanifestpickupsPost.ixOutboundCarrierManifestPickup);
                                            await _outboundcarriermanifestpickupsService.Delete(_outboundcarriermanifestpickupsPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The OutboundCarrierManifestPickup {_outboundcarriermanifestpickupsPost.sOutboundCarrierManifestPickup} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _outboundcarriermanifestpickupsPost = _outboundcarriermanifestpickupsService.GetPost(outboundcarriermanifestpickupsPost.ixOutboundCarrierManifestPickup);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for OutboundCarrierManifestPickup ID {currentBotUserData.ixOutboundCarrierManifestPickup}: {System.Environment.NewLine}  OutboundCarrierManifestPickup: {_outboundcarriermanifestpickupsPost.sOutboundCarrierManifestPickup.ToString() + System.Environment.NewLine}  OutboundCarrierManifest: {_outboundcarriermanifestpickupsPost.ixOutboundCarrierManifest.ToString() + System.Environment.NewLine}  Status: {_outboundcarriermanifestpickupsPost.ixStatus.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (outboundcarriermanifestpickupsPost.ixOutboundCarrierManifestPickup > 0)
                                    {
                                        ixOutboundCarrierManifestPickup = outboundcarriermanifestpickupsPost.ixOutboundCarrierManifestPickup;
                                    }
                                    else if (outboundcarriermanifestpickupsPost.ixOutboundCarrierManifestPickup == 0)
                                    {
                                        await dc.BeginDialogAsync(FindOutboundCarrierManifestPickupsDialogId, null, cancellationToken);
                                    }
                                    else if (outboundcarriermanifestpickupsPost.ixOutboundCarrierManifestPickup < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;
                        case InventoryLocationsSlottingPost inventorylocationsslottingPost:
                                if (currentBotUserData.botUserEntityContext.entityIntent == "Create")
                                {
                                    ixInventoryLocationSlotting = await _inventorylocationsslottingService.Create(inventorylocationsslottingPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The InventoryLocationSlotting {ixInventoryLocationSlotting} was created"), cancellationToken);
                                    currentBotUserData.ixInventoryLocationSlotting = ixInventoryLocationSlotting;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Edit")
                                {
                                    await _inventorylocationsslottingService.Edit(inventorylocationsslottingPost);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The InventoryLocationSlotting {inventorylocationsslottingPost.ixInventoryLocationSlotting} was changed"), cancellationToken);
                                    ixInventoryLocationSlotting = inventorylocationsslottingPost.ixInventoryLocationSlotting;
                                    currentBotUserData.ixInventoryLocationSlotting = inventorylocationsslottingPost.ixInventoryLocationSlotting;
                                    await _botSpielUserStateAccessors.BotUserDataAccessor.SetAsync(turnContext, currentBotUserData, cancellationToken);
                                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Delete")
                                {
                                    if (inventorylocationsslottingPost.ixInventoryLocationSlotting > 0)
                                        {
                                            _inventorylocationsslottingPost = _inventorylocationsslottingService.GetPost(inventorylocationsslottingPost.ixInventoryLocationSlotting);
                                            await _inventorylocationsslottingService.Delete(_inventorylocationsslottingPost);
                                            await turnContext.SendActivityAsync(MessageFactory.Text($"The InventoryLocationSlotting {_inventorylocationsslottingPost.sInventoryLocationSlotting} has been deleted."), cancellationToken);
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                    else
                                        {
                                            await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                        }
                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Details")
                                {
                                    _inventorylocationsslottingPost = _inventorylocationsslottingService.GetPost(inventorylocationsslottingPost.ixInventoryLocationSlotting);
                                    await turnContext.SendActivityAsync(MessageFactory.Text($"The details for InventoryLocationSlotting ID {currentBotUserData.ixInventoryLocationSlotting}: {System.Environment.NewLine}  InventoryLocationSlotting: {_inventorylocationsslottingPost.sInventoryLocationSlotting.ToString() + System.Environment.NewLine}  InventoryLocation: {_inventorylocationsslottingPost.ixInventoryLocation.ToString() + System.Environment.NewLine}  Material: {_inventorylocationsslottingPost.ixMaterial.ToString() + System.Environment.NewLine}  MinimumBaseUnitQuantity: {_inventorylocationsslottingPost.nMinimumBaseUnitQuantity.ToString() + System.Environment.NewLine}  MaximumBaseUnitQuantity: {_inventorylocationsslottingPost.nMaximumBaseUnitQuantity.ToString() + System.Environment.NewLine}"), cancellationToken);
                                    await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);

                                }
                                else if (currentBotUserData.botUserEntityContext.entityIntent == "Find")
                                {
                                    if (inventorylocationsslottingPost.ixInventoryLocationSlotting > 0)
                                    {
                                        ixInventoryLocationSlotting = inventorylocationsslottingPost.ixInventoryLocationSlotting;
                                    }
                                    else if (inventorylocationsslottingPost.ixInventoryLocationSlotting == 0)
                                    {
                                        await dc.BeginDialogAsync(FindInventoryLocationsSlottingDialogId, null, cancellationToken);
                                    }
                                    else if (inventorylocationsslottingPost.ixInventoryLocationSlotting < 0)
                                    {
                                        await dc.BeginDialogAsync(RootDialogId, currentBotUserData.botUserEntityContext, cancellationToken);
                                    }
                                }
                            break;

                        default:
                            // We shouldn't get here.
                            break;
                    }
                }

                // Proactively send a welcome message to a personal chat the first time
                // (and only the first time) a user initiates a personal chat.
                if (didBotWelcomeUser == false)
                {
                    // Update user state flag to reflect bot handled first user interaction.
                    await _botSpielUserStateAccessors.DidBotWelcomeUser.SetAsync(turnContext, true);
                    await _botSpielUserStateAccessors.UserState.SaveChangesAsync(turnContext);

                    // the channel should sends the user name in the 'From' object
                    var userName = turnContext.Activity.From.Name;

                    // We give the user the opportunity to say or request something using natural language and funnel through recognizer
                    await turnContext.SendActivityAsync($"What would like to do? You can say things like ... or help me.", cancellationToken: cancellationToken);
                }
                else
                {

                    var text = turnContext.Activity.Text.ToLowerInvariant();

                    // Now attempt to infer the context (NLP)
                    // Placeholder for code to added

                    switch (text)
                    {
                        case "help me":
                            await turnContext.SendActivityAsync($"You said: {text}.", cancellationToken: cancellationToken);
                            break;
                        default:
                            if ( dc.ActiveDialog == null  && ( dialogTurnResult.Status is DialogTurnStatus.Complete || dialogTurnResult.Status is DialogTurnStatus.Empty || dialogTurnResult.Status is DialogTurnStatus.Cancelled ) )
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







