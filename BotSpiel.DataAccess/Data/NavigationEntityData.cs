using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using System.ComponentModel.DataAnnotations;

namespace BotSpiel.DataAccess.Data
{

    // Establish data for navigation structure

    public class NavigationModule
    {
        public string sModule;
        public int nSequenceOrder;
    }

    public class NavigationEntity
    {
        public string sModule;
        public string sDataEntity;
        public string sDataEntityPredecessors;
        public int nUIPresentationSequence;
    }

    public class EntityCrudAction
    {
        public string sDataEntity;
        public string sCrudAction;
    }

    [Serializable]
    public class EntityColumn
    {
        public string sDataEntity;
        public string sDataColumn;
        public string sDataColumnPrefix;
        public string sFKDataEntity;
        public string sFKDataColumn;
    }

    [Serializable]
    public class ModuleData
    {
        public ModuleData()
        {
        }

        NavigationModule[] NavigationModules = new NavigationModule[]
        {

               new NavigationModule { sModule = "Foundation", nSequenceOrder = 1 },
                new NavigationModule { sModule = "Network", nSequenceOrder = 2 },
                new NavigationModule { sModule = "Inbound", nSequenceOrder = 3 },
                new NavigationModule { sModule = "Inventory", nSequenceOrder = 4 },
                new NavigationModule { sModule = "Assembly", nSequenceOrder = 5 },
                new NavigationModule { sModule = "Replenishment", nSequenceOrder = 5 },
                new NavigationModule { sModule = "Projects", nSequenceOrder = 6 },
                new NavigationModule { sModule = "Service", nSequenceOrder = 7 },
                new NavigationModule { sModule = "Outbound", nSequenceOrder = 8 },
                new NavigationModule { sModule = "Execution", nSequenceOrder = 9 }


        };

}


    [Serializable]
    public class EntityColumnData
    {

        public EntityColumnData()
        {
        }

            EntityColumn[] EntityColumns = new EntityColumn[]
            {
               new EntityColumn { sDataEntity = "Addresses", sDataColumn = "Address", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Addresses", sDataColumn = "StreetAndNumberOrPostOfficeBoxOne", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Addresses", sDataColumn = "StreetAndNumberOrPostOfficeBoxTwo", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Addresses", sDataColumn = "StreetAndNumberOrPostOfficeBoxThree", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Addresses", sDataColumn = "CityOrSuburb", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Addresses", sDataColumn = "ZipOrPostCode", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Addresses", sDataColumn = "StateOrProvince", sDataColumnPrefix = "s", sFKDataEntity = "CountrySubDivisions", sFKDataColumn = "CountrySubDivision"},
                new EntityColumn { sDataEntity = "Addresses", sDataColumn = "Country", sDataColumnPrefix = "s", sFKDataEntity = "Countries", sFKDataColumn = "Country"},
                new EntityColumn { sDataEntity = "AisleFaceStorageTypes", sDataColumn = "AisleFaceStorageType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "AssemblyModuleGrids", sDataColumn = "AssemblyModuleGrid", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "AssemblyModuleGrids", sDataColumn = "ShortDescription", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "AssemblyModuleGrids", sDataColumn = "DataEntityType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "AssemblyModuleGrids", sDataColumn = "CanCreate", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "AssemblyModuleGrids", sDataColumn = "CanEdit", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "AssemblyModuleGrids", sDataColumn = "CanDelete", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "BaySequenceTypes", sDataColumn = "BaySequenceType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "BusinessPartners", sDataColumn = "BusinessPartner", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "BusinessPartners", sDataColumn = "BusinessPartnerType", sDataColumnPrefix = "s", sFKDataEntity = "BusinessPartnerTypes", sFKDataColumn = "BusinessPartnerType"},
                new EntityColumn { sDataEntity = "BusinessPartners", sDataColumn = "Company", sDataColumnPrefix = "s", sFKDataEntity = "Companies", sFKDataColumn = "Company"},
                new EntityColumn { sDataEntity = "BusinessPartners", sDataColumn = "Address", sDataColumnPrefix = "s", sFKDataEntity = "Addresses", sFKDataColumn = "Address"},
                new EntityColumn { sDataEntity = "BusinessPartnerTypes", sDataColumn = "BusinessPartnerType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Carriers", sDataColumn = "Carrier", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Carriers", sDataColumn = "CarrierType", sDataColumnPrefix = "s", sFKDataEntity = "CarrierTypes", sFKDataColumn = "CarrierType"},
                new EntityColumn { sDataEntity = "Carriers", sDataColumn = "StandardCarrierAlphaCode", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Carriers", sDataColumn = "CarrierConsignmentNumberPrefix", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Carriers", sDataColumn = "CarrierConsignmentNumberStart", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Carriers", sDataColumn = "CarrierConsignmentNumberLastUsed", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Carriers", sDataColumn = "ScheduledPickupTime", sDataColumnPrefix = "dt", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "CarrierServices", sDataColumn = "CarrierService", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "CarrierServices", sDataColumn = "Carrier", sDataColumnPrefix = "s", sFKDataEntity = "Carriers", sFKDataColumn = "Carrier"},
                new EntityColumn { sDataEntity = "CarrierTypes", sDataColumn = "CarrierType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "CommunicationMediums", sDataColumn = "CommunicationMedium", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "CommunicationMediums", sDataColumn = "CommunicationMediumCode", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Companies", sDataColumn = "Company", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "ContactFunctions", sDataColumn = "ContactFunction", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "ContactFunctions", sDataColumn = "ContactFunctionCode", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Countries", sDataColumn = "Country", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Countries", sDataColumn = "PlanetSubRegion", sDataColumnPrefix = "s", sFKDataEntity = "PlanetSubRegions", sFKDataColumn = "PlanetSubRegion"},
                new EntityColumn { sDataEntity = "Countries", sDataColumn = "CountryCode", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "CountryLocations", sDataColumn = "CountryLocation", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "CountryLocations", sDataColumn = "CountrySubDivision", sDataColumnPrefix = "s", sFKDataEntity = "CountrySubDivisions", sFKDataColumn = "CountrySubDivision"},
                new EntityColumn { sDataEntity = "CountryLocations", sDataColumn = "LocationCode", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "CountryLocations", sDataColumn = "NameWithoutDiacritics", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "CountryLocations", sDataColumn = "Latitude", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "CountryLocations", sDataColumn = "Longitude", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "CountrySubDivisions", sDataColumn = "CountrySubDivision", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "CountrySubDivisions", sDataColumn = "Country", sDataColumnPrefix = "s", sFKDataEntity = "Countries", sFKDataColumn = "Country"},
                new EntityColumn { sDataEntity = "CountrySubDivisions", sDataColumn = "CountrySubDivisionCode", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Currencies", sDataColumn = "Currency", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "CurrencyTypes", sDataColumn = "CurrencyType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "CurrencyTypes", sDataColumn = "CurrencyTypeCode", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "DateTimePeriodFormats", sDataColumn = "DateTimePeriodFormat", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "DateTimePeriodFormats", sDataColumn = "DateTimePeriodFormatCode", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "DateTimePeriodFunctions", sDataColumn = "DateTimePeriodFunction", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "DateTimePeriodFunctions", sDataColumn = "DateTimePeriodFunctionCode", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "DocumentMessageTypes", sDataColumn = "DocumentMessageType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "DocumentMessageTypes", sDataColumn = "DocumentMessageTypeCode", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Documents", sDataColumn = "Document", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Documents", sDataColumn = "DocumentMessageType", sDataColumnPrefix = "s", sFKDataEntity = "DocumentMessageTypes", sFKDataColumn = "DocumentMessageType"},
                new EntityColumn { sDataEntity = "Documents", sDataColumn = "Version", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Documents", sDataColumn = "Revision", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Documents", sDataColumn = "Status", sDataColumnPrefix = "s", sFKDataEntity = "Statuses", sFKDataColumn = "Status"},
                new EntityColumn { sDataEntity = "ExecutionModuleGrids", sDataColumn = "ExecutionModuleGrid", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "ExecutionModuleGrids", sDataColumn = "ShortDescription", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "ExecutionModuleGrids", sDataColumn = "DataEntityType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "ExecutionModuleGrids", sDataColumn = "CanCreate", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "ExecutionModuleGrids", sDataColumn = "CanEdit", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "ExecutionModuleGrids", sDataColumn = "CanDelete", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Facilities", sDataColumn = "Facility", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Facilities", sDataColumn = "Address", sDataColumnPrefix = "s", sFKDataEntity = "Addresses", sFKDataColumn = "Address"},
                new EntityColumn { sDataEntity = "Facilities", sDataColumn = "Latitude", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Facilities", sDataColumn = "Longitude", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "FacilityAisleFaces", sDataColumn = "FacilityAisleFace", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "FacilityAisleFaces", sDataColumn = "FacilityFloor", sDataColumnPrefix = "s", sFKDataEntity = "FacilityFloors", sFKDataColumn = "FacilityFloor"},
                new EntityColumn { sDataEntity = "FacilityAisleFaces", sDataColumn = "Sequence", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "FacilityAisleFaces", sDataColumn = "BaySequenceType", sDataColumnPrefix = "s", sFKDataEntity = "BaySequenceTypes", sFKDataColumn = "BaySequenceType"},
                new EntityColumn { sDataEntity = "FacilityAisleFaces", sDataColumn = "PairedAisleFace", sDataColumnPrefix = "s", sFKDataEntity = "FacilityAisleFaces", sFKDataColumn = "FacilityAisleFace"},
                new EntityColumn { sDataEntity = "FacilityAisleFaces", sDataColumn = "LogicalOrientation", sDataColumnPrefix = "s", sFKDataEntity = "LogicalOrientations", sFKDataColumn = "LogicalOrientation"},
                new EntityColumn { sDataEntity = "FacilityAisleFaces", sDataColumn = "AisleFaceStorageType", sDataColumnPrefix = "s", sFKDataEntity = "AisleFaceStorageTypes", sFKDataColumn = "AisleFaceStorageType"},
                new EntityColumn { sDataEntity = "FacilityAisleFaces", sDataColumn = "XOffset", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "FacilityAisleFaces", sDataColumn = "XOffsetUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "FacilityAisleFaces", sDataColumn = "YOffset", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "FacilityAisleFaces", sDataColumn = "YOffsetUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "FacilityAisleFaces", sDataColumn = "Levels", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "FacilityAisleFaces", sDataColumn = "DefaultNumberOfBays", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "FacilityAisleFaces", sDataColumn = "DefaultNumberOfSlotsInBay", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "FacilityAisleFaces", sDataColumn = "DefaultFacilityZone", sDataColumnPrefix = "s", sFKDataEntity = "FacilityZones", sFKDataColumn = "FacilityZone"},
                new EntityColumn { sDataEntity = "FacilityAisleFaces", sDataColumn = "DefaultLocationFunction", sDataColumnPrefix = "s", sFKDataEntity = "LocationFunctions", sFKDataColumn = "LocationFunction"},
                new EntityColumn { sDataEntity = "FacilityAisleFaces", sDataColumn = "DefaultInventoryLocationSize", sDataColumnPrefix = "s", sFKDataEntity = "InventoryLocationSizes", sFKDataColumn = "InventoryLocationSize"},
                new EntityColumn { sDataEntity = "FacilityFloors", sDataColumn = "FacilityFloor", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "FacilityWorkAreas", sDataColumn = "FacilityWorkArea", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "FacilityZones", sDataColumn = "FacilityZone", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "FoundationModuleGrids", sDataColumn = "FoundationModuleGrid", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "FoundationModuleGrids", sDataColumn = "ShortDescription", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "FoundationModuleGrids", sDataColumn = "DataEntityType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "FoundationModuleGrids", sDataColumn = "CanCreate", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "FoundationModuleGrids", sDataColumn = "CanEdit", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "FoundationModuleGrids", sDataColumn = "CanDelete", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Galaxies", sDataColumn = "Galaxy", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Galaxies", sDataColumn = "Universe", sDataColumnPrefix = "s", sFKDataEntity = "Universes", sFKDataColumn = "Universe"},
                new EntityColumn { sDataEntity = "HandlingUnits", sDataColumn = "HandlingUnit", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "HandlingUnits", sDataColumn = "HandlingUnitType", sDataColumnPrefix = "s", sFKDataEntity = "HandlingUnitTypes", sFKDataColumn = "HandlingUnitType"},
                new EntityColumn { sDataEntity = "HandlingUnits", sDataColumn = "ParentHandlingUnit", sDataColumnPrefix = "s", sFKDataEntity = "HandlingUnits", sFKDataColumn = "HandlingUnit"},
                new EntityColumn { sDataEntity = "HandlingUnits", sDataColumn = "PackingMaterial", sDataColumnPrefix = "s", sFKDataEntity = "Materials", sFKDataColumn = "Material"},
                new EntityColumn { sDataEntity = "HandlingUnits", sDataColumn = "MaterialHandlingUnitConfiguration", sDataColumnPrefix = "s", sFKDataEntity = "MaterialHandlingUnitConfigurations", sFKDataColumn = "MaterialHandlingUnitConfiguration"},
                new EntityColumn { sDataEntity = "HandlingUnits", sDataColumn = "Length", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "HandlingUnits", sDataColumn = "LengthUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "HandlingUnits", sDataColumn = "Width", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "HandlingUnits", sDataColumn = "WidthUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "HandlingUnits", sDataColumn = "Height", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "HandlingUnits", sDataColumn = "HeightUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "HandlingUnits", sDataColumn = "Weight", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "HandlingUnits", sDataColumn = "WeightUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "HandlingUnits", sDataColumn = "Status", sDataColumnPrefix = "s", sFKDataEntity = "Statuses", sFKDataColumn = "Status"},
                new EntityColumn { sDataEntity = "HandlingUnitsShipping", sDataColumn = "HandlingUnitShipping", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "HandlingUnitsShipping", sDataColumn = "HandlingUnit", sDataColumnPrefix = "s", sFKDataEntity = "HandlingUnits", sFKDataColumn = "HandlingUnit"},
                new EntityColumn { sDataEntity = "HandlingUnitsShipping", sDataColumn = "Status", sDataColumnPrefix = "s", sFKDataEntity = "Statuses", sFKDataColumn = "Status"},
                new EntityColumn { sDataEntity = "HandlingUnitTypes", sDataColumn = "HandlingUnitType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InboundModuleGrids", sDataColumn = "InboundModuleGrid", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InboundModuleGrids", sDataColumn = "ShortDescription", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InboundModuleGrids", sDataColumn = "DataEntityType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InboundModuleGrids", sDataColumn = "CanCreate", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InboundModuleGrids", sDataColumn = "CanEdit", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InboundModuleGrids", sDataColumn = "CanDelete", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InboundOrderLines", sDataColumn = "InboundOrderLine", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InboundOrderLines", sDataColumn = "InboundOrder", sDataColumnPrefix = "s", sFKDataEntity = "InboundOrders", sFKDataColumn = "InboundOrder"},
                new EntityColumn { sDataEntity = "InboundOrderLines", sDataColumn = "OrderLineReference", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InboundOrderLines", sDataColumn = "Material", sDataColumnPrefix = "s", sFKDataEntity = "Materials", sFKDataColumn = "Material"},
                new EntityColumn { sDataEntity = "InboundOrderLines", sDataColumn = "MaterialHandlingUnitConfiguration", sDataColumnPrefix = "s", sFKDataEntity = "MaterialHandlingUnitConfigurations", sFKDataColumn = "MaterialHandlingUnitConfiguration"},
                new EntityColumn { sDataEntity = "InboundOrderLines", sDataColumn = "HandlingUnitType", sDataColumnPrefix = "s", sFKDataEntity = "HandlingUnitTypes", sFKDataColumn = "HandlingUnitType"},
                new EntityColumn { sDataEntity = "InboundOrderLines", sDataColumn = "HandlingUnitQuantity", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InboundOrderLines", sDataColumn = "BaseUnitQuantityExpected", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InboundOrderLines", sDataColumn = "BaseUnitQuantityReceived", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InboundOrderLines", sDataColumn = "BatchNumber", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InboundOrderLines", sDataColumn = "SerialNumber", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InboundOrderLines", sDataColumn = "Status", sDataColumnPrefix = "s", sFKDataEntity = "Statuses", sFKDataColumn = "Status"},
                new EntityColumn { sDataEntity = "InboundOrders", sDataColumn = "InboundOrder", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InboundOrders", sDataColumn = "OrderReference", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InboundOrders", sDataColumn = "InboundOrderType", sDataColumnPrefix = "s", sFKDataEntity = "InboundOrderTypes", sFKDataColumn = "InboundOrderType"},
                new EntityColumn { sDataEntity = "InboundOrders", sDataColumn = "Facility", sDataColumnPrefix = "s", sFKDataEntity = "Facilities", sFKDataColumn = "Facility"},
                new EntityColumn { sDataEntity = "InboundOrders", sDataColumn = "Company", sDataColumnPrefix = "s", sFKDataEntity = "Companies", sFKDataColumn = "Company"},
                new EntityColumn { sDataEntity = "InboundOrders", sDataColumn = "BusinessPartner", sDataColumnPrefix = "s", sFKDataEntity = "BusinessPartners", sFKDataColumn = "BusinessPartner"},
                new EntityColumn { sDataEntity = "InboundOrders", sDataColumn = "ExpectedAt", sDataColumnPrefix = "dt", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InboundOrders", sDataColumn = "Status", sDataColumnPrefix = "s", sFKDataEntity = "Statuses", sFKDataColumn = "Status"},
                new EntityColumn { sDataEntity = "InboundOrderTypes", sDataColumn = "InboundOrderType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "InventoryLocation", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "LocationFunction", sDataColumnPrefix = "s", sFKDataEntity = "LocationFunctions", sFKDataColumn = "LocationFunction"},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "Company", sDataColumnPrefix = "s", sFKDataEntity = "Companies", sFKDataColumn = "Company"},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "FacilityFloor", sDataColumnPrefix = "s", sFKDataEntity = "FacilityFloors", sFKDataColumn = "FacilityFloor"},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "FacilityZone", sDataColumnPrefix = "s", sFKDataEntity = "FacilityZones", sFKDataColumn = "FacilityZone"},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "FacilityWorkArea", sDataColumnPrefix = "s", sFKDataEntity = "FacilityWorkAreas", sFKDataColumn = "FacilityWorkArea"},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "FacilityAisleFace", sDataColumnPrefix = "s", sFKDataEntity = "FacilityAisleFaces", sFKDataColumn = "FacilityAisleFace"},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "Level", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "Bay", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "Slot", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "InventoryLocationSize", sDataColumnPrefix = "s", sFKDataEntity = "InventoryLocationSizes", sFKDataColumn = "InventoryLocationSize"},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "Sequence", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "XOffset", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "XOffsetUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "YOffset", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "YOffsetUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "ZOffset", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "ZOffsetUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "Latitude", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "Longitude", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "TrackUtilisation", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "UtilisationPercent", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocations", sDataColumn = "QueuedUtilisationPercent", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocationSizes", sDataColumn = "InventoryLocationSize", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocationSizes", sDataColumn = "Length", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocationSizes", sDataColumn = "LengthUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "InventoryLocationSizes", sDataColumn = "Width", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocationSizes", sDataColumn = "WidthUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "InventoryLocationSizes", sDataColumn = "Height", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocationSizes", sDataColumn = "HeightUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "InventoryLocationSizes", sDataColumn = "UsableVolume", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocationSizes", sDataColumn = "UsableVolumeUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "InventoryLocationsSlotting", sDataColumn = "InventoryLocationSlotting", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocationsSlotting", sDataColumn = "InventoryLocation", sDataColumnPrefix = "s", sFKDataEntity = "InventoryLocations", sFKDataColumn = "InventoryLocation"},
                new EntityColumn { sDataEntity = "InventoryLocationsSlotting", sDataColumn = "Material", sDataColumnPrefix = "s", sFKDataEntity = "Materials", sFKDataColumn = "Material"},
                new EntityColumn { sDataEntity = "InventoryLocationsSlotting", sDataColumn = "MinimumBaseUnitQuantity", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryLocationsSlotting", sDataColumn = "MaximumBaseUnitQuantity", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryModuleGrids", sDataColumn = "InventoryModuleGrid", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryModuleGrids", sDataColumn = "ShortDescription", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryModuleGrids", sDataColumn = "DataEntityType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryModuleGrids", sDataColumn = "CanCreate", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryModuleGrids", sDataColumn = "CanEdit", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryModuleGrids", sDataColumn = "CanDelete", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryStates", sDataColumn = "InventoryState", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryUnits", sDataColumn = "InventoryUnit", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryUnits", sDataColumn = "Facility", sDataColumnPrefix = "s", sFKDataEntity = "Facilities", sFKDataColumn = "Facility"},
                new EntityColumn { sDataEntity = "InventoryUnits", sDataColumn = "Company", sDataColumnPrefix = "s", sFKDataEntity = "Companies", sFKDataColumn = "Company"},
                new EntityColumn { sDataEntity = "InventoryUnits", sDataColumn = "Material", sDataColumnPrefix = "s", sFKDataEntity = "Materials", sFKDataColumn = "Material"},
                new EntityColumn { sDataEntity = "InventoryUnits", sDataColumn = "InventoryState", sDataColumnPrefix = "s", sFKDataEntity = "InventoryStates", sFKDataColumn = "InventoryState"},
                new EntityColumn { sDataEntity = "InventoryUnits", sDataColumn = "HandlingUnit", sDataColumnPrefix = "s", sFKDataEntity = "HandlingUnits", sFKDataColumn = "HandlingUnit"},
                new EntityColumn { sDataEntity = "InventoryUnits", sDataColumn = "InventoryLocation", sDataColumnPrefix = "s", sFKDataEntity = "InventoryLocations", sFKDataColumn = "InventoryLocation"},
                new EntityColumn { sDataEntity = "InventoryUnits", sDataColumn = "BaseUnitQuantity", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryUnits", sDataColumn = "SerialNumber", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryUnits", sDataColumn = "BatchNumber", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryUnits", sDataColumn = "ExpireAt", sDataColumnPrefix = "dt", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryUnits", sDataColumn = "Status", sDataColumnPrefix = "s", sFKDataEntity = "Statuses", sFKDataColumn = "Status"},
                new EntityColumn { sDataEntity = "InventoryUnitTransactionContexts", sDataColumn = "InventoryUnitTransactionContext", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "InventoryUnitTransaction", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "InventoryUnit", sDataColumnPrefix = "s", sFKDataEntity = "InventoryUnits", sFKDataColumn = "InventoryUnit"},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "InventoryUnitTransactionContext", sDataColumnPrefix = "s", sFKDataEntity = "InventoryUnitTransactionContexts", sFKDataColumn = "InventoryUnitTransactionContext"},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "FacilityBefore", sDataColumnPrefix = "s", sFKDataEntity = "Facilities", sFKDataColumn = "Facility"},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "FacilityAfter", sDataColumnPrefix = "s", sFKDataEntity = "Facilities", sFKDataColumn = "Facility"},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "CompanyBefore", sDataColumnPrefix = "s", sFKDataEntity = "Companies", sFKDataColumn = "Company"},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "CompanyAfter", sDataColumnPrefix = "s", sFKDataEntity = "Companies", sFKDataColumn = "Company"},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "MaterialBefore", sDataColumnPrefix = "s", sFKDataEntity = "Materials", sFKDataColumn = "Material"},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "MaterialAfter", sDataColumnPrefix = "s", sFKDataEntity = "Materials", sFKDataColumn = "Material"},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "InventoryStateBefore", sDataColumnPrefix = "s", sFKDataEntity = "InventoryStates", sFKDataColumn = "InventoryState"},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "InventoryStateAfter", sDataColumnPrefix = "s", sFKDataEntity = "InventoryStates", sFKDataColumn = "InventoryState"},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "HandlingUnitBefore", sDataColumnPrefix = "s", sFKDataEntity = "HandlingUnits", sFKDataColumn = "HandlingUnit"},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "HandlingUnitAfter", sDataColumnPrefix = "s", sFKDataEntity = "HandlingUnits", sFKDataColumn = "HandlingUnit"},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "InventoryLocationBefore", sDataColumnPrefix = "s", sFKDataEntity = "InventoryLocations", sFKDataColumn = "InventoryLocation"},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "InventoryLocationAfter", sDataColumnPrefix = "s", sFKDataEntity = "InventoryLocations", sFKDataColumn = "InventoryLocation"},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "BaseUnitQuantityBefore", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "BaseUnitQuantityAfter", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "SerialNumberBefore", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "SerialNumberAfter", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "BatchNumberBefore", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "BatchNumberAfter", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "ExpireAtBefore", sDataColumnPrefix = "dt", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "ExpireAtAfter", sDataColumnPrefix = "dt", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "StatusBefore", sDataColumnPrefix = "s", sFKDataEntity = "Statuses", sFKDataColumn = "Status"},
                new EntityColumn { sDataEntity = "InventoryUnitTransactions", sDataColumn = "StatusAfter", sDataColumnPrefix = "s", sFKDataEntity = "Statuses", sFKDataColumn = "Status"},
                new EntityColumn { sDataEntity = "Languages", sDataColumn = "Language", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Languages", sDataColumn = "LanguageCode", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "LocationFunctions", sDataColumn = "LocationFunction", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "LocationFunctions", sDataColumn = "LocationFunctionCode", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "LogicalOrientations", sDataColumn = "LogicalOrientation", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MaterialHandlingUnitConfigurations", sDataColumn = "MaterialHandlingUnitConfiguration", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MaterialHandlingUnitConfigurations", sDataColumn = "Material", sDataColumnPrefix = "s", sFKDataEntity = "Materials", sFKDataColumn = "Material"},
                new EntityColumn { sDataEntity = "MaterialHandlingUnitConfigurations", sDataColumn = "NestingLevel", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MaterialHandlingUnitConfigurations", sDataColumn = "HandlingUnitType", sDataColumnPrefix = "s", sFKDataEntity = "HandlingUnitTypes", sFKDataColumn = "HandlingUnitType"},
                new EntityColumn { sDataEntity = "MaterialHandlingUnitConfigurations", sDataColumn = "Quantity", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MaterialHandlingUnitConfigurations", sDataColumn = "Length", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MaterialHandlingUnitConfigurations", sDataColumn = "LengthUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "MaterialHandlingUnitConfigurations", sDataColumn = "Width", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MaterialHandlingUnitConfigurations", sDataColumn = "WidthUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "MaterialHandlingUnitConfigurations", sDataColumn = "Height", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MaterialHandlingUnitConfigurations", sDataColumn = "HeightUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "Material", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "Description", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "MaterialType", sDataColumnPrefix = "s", sFKDataEntity = "MaterialTypes", sFKDataColumn = "MaterialType"},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "BaseUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "TrackSerialNumber", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "TrackBatchNumber", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "TrackExpiry", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "Density", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "DensityUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "Shelflife", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "ShelflifeUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "Length", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "LengthUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "Width", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "WidthUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "Height", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "HeightUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "Weight", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Materials", sDataColumn = "WeightUnit", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "MaterialTypes", sDataColumn = "MaterialType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MeasurementSystems", sDataColumn = "MeasurementSystem", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MeasurementUnitsOf", sDataColumn = "MeasurementUnitOf", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MessageFunctions", sDataColumn = "MessageFunction", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MessageFunctions", sDataColumn = "MessageFunctionCode", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MessageResponseTypes", sDataColumn = "MessageResponseType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MessageResponseTypes", sDataColumn = "MessageResponseTypeCode", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MonetaryAmountTypes", sDataColumn = "MonetaryAmountType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MonetaryAmountTypes", sDataColumn = "MonetaryAmountTypeCode", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MoveQueueContexts", sDataColumn = "MoveQueueContext", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "MoveQueue", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "MoveQueueType", sDataColumnPrefix = "s", sFKDataEntity = "MoveQueueTypes", sFKDataColumn = "MoveQueueType"},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "MoveQueueContext", sDataColumnPrefix = "s", sFKDataEntity = "MoveQueueContexts", sFKDataColumn = "MoveQueueContext"},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "SourceInventoryUnit", sDataColumnPrefix = "s", sFKDataEntity = "InventoryUnits", sFKDataColumn = "InventoryUnit"},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "TargetInventoryUnit", sDataColumnPrefix = "s", sFKDataEntity = "InventoryUnits", sFKDataColumn = "InventoryUnit"},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "SourceInventoryLocation", sDataColumnPrefix = "s", sFKDataEntity = "InventoryLocations", sFKDataColumn = "InventoryLocation"},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "TargetInventoryLocation", sDataColumnPrefix = "s", sFKDataEntity = "InventoryLocations", sFKDataColumn = "InventoryLocation"},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "SourceHandlingUnit", sDataColumnPrefix = "s", sFKDataEntity = "HandlingUnits", sFKDataColumn = "HandlingUnit"},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "TargetHandlingUnit", sDataColumnPrefix = "s", sFKDataEntity = "HandlingUnits", sFKDataColumn = "HandlingUnit"},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "PreferredResource", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "BaseUnitQuantity", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "StartBy", sDataColumnPrefix = "dt", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "CompleteBy", sDataColumnPrefix = "dt", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "StartedAt", sDataColumnPrefix = "dt", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "CompletedAt", sDataColumnPrefix = "dt", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "InboundOrderLine", sDataColumnPrefix = "s", sFKDataEntity = "InboundOrderLines", sFKDataColumn = "InboundOrderLine"},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "OutboundOrderLine", sDataColumnPrefix = "s", sFKDataEntity = "OutboundOrderLines", sFKDataColumn = "OutboundOrderLine"},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "PickBatch", sDataColumnPrefix = "s", sFKDataEntity = "PickBatches", sFKDataColumn = "PickBatch"},
                new EntityColumn { sDataEntity = "MoveQueues", sDataColumn = "Status", sDataColumnPrefix = "s", sFKDataEntity = "Statuses", sFKDataColumn = "Status"},
                new EntityColumn { sDataEntity = "MoveQueueTypes", sDataColumn = "MoveQueueType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "NetworkModuleGrids", sDataColumn = "NetworkModuleGrid", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "NetworkModuleGrids", sDataColumn = "ShortDescription", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "NetworkModuleGrids", sDataColumn = "DataEntityType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "NetworkModuleGrids", sDataColumn = "CanCreate", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "NetworkModuleGrids", sDataColumn = "CanEdit", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "NetworkModuleGrids", sDataColumn = "CanDelete", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundCarrierManifestPickups", sDataColumn = "OutboundCarrierManifestPickup", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundCarrierManifestPickups", sDataColumn = "OutboundCarrierManifest", sDataColumnPrefix = "s", sFKDataEntity = "OutboundCarrierManifests", sFKDataColumn = "OutboundCarrierManifest"},
                new EntityColumn { sDataEntity = "OutboundCarrierManifestPickups", sDataColumn = "Status", sDataColumnPrefix = "s", sFKDataEntity = "Statuses", sFKDataColumn = "Status"},
                new EntityColumn { sDataEntity = "OutboundCarrierManifests", sDataColumn = "OutboundCarrierManifest", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundCarrierManifests", sDataColumn = "Carrier", sDataColumnPrefix = "s", sFKDataEntity = "Carriers", sFKDataColumn = "Carrier"},
                new EntityColumn { sDataEntity = "OutboundCarrierManifests", sDataColumn = "PickupInventoryLocation", sDataColumnPrefix = "s", sFKDataEntity = "InventoryLocations", sFKDataColumn = "InventoryLocation"},
                new EntityColumn { sDataEntity = "OutboundCarrierManifests", sDataColumn = "ScheduledPickupAt", sDataColumnPrefix = "dt", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundCarrierManifests", sDataColumn = "Status", sDataColumnPrefix = "s", sFKDataEntity = "Statuses", sFKDataColumn = "Status"},
                new EntityColumn { sDataEntity = "OutboundModuleGrids", sDataColumn = "OutboundModuleGrid", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundModuleGrids", sDataColumn = "ShortDescription", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundModuleGrids", sDataColumn = "DataEntityType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundModuleGrids", sDataColumn = "CanCreate", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundModuleGrids", sDataColumn = "CanEdit", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundModuleGrids", sDataColumn = "CanDelete", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundOrderLines", sDataColumn = "OutboundOrderLine", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundOrderLines", sDataColumn = "OrderLineReference", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundOrderLines", sDataColumn = "Material", sDataColumnPrefix = "s", sFKDataEntity = "Materials", sFKDataColumn = "Material"},
                new EntityColumn { sDataEntity = "OutboundOrderLines", sDataColumn = "BatchNumber", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundOrderLines", sDataColumn = "SerialNumber", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundOrderLines", sDataColumn = "BaseUnitQuantityOrdered", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundOrderLines", sDataColumn = "BaseUnitQuantityShipped", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundOrderLines", sDataColumn = "Status", sDataColumnPrefix = "s", sFKDataEntity = "Statuses", sFKDataColumn = "Status"},
                new EntityColumn { sDataEntity = "OutboundOrderPacking", sDataColumn = "OutboundOrderPack", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundOrderPacking", sDataColumn = "OutboundOrderLine", sDataColumnPrefix = "s", sFKDataEntity = "OutboundOrderLines", sFKDataColumn = "OutboundOrderLine"},
                new EntityColumn { sDataEntity = "OutboundOrderPacking", sDataColumn = "HandlingUnit", sDataColumnPrefix = "s", sFKDataEntity = "HandlingUnits", sFKDataColumn = "HandlingUnit"},
                new EntityColumn { sDataEntity = "OutboundOrderPacking", sDataColumn = "Status", sDataColumnPrefix = "s", sFKDataEntity = "Statuses", sFKDataColumn = "Status"},
                new EntityColumn { sDataEntity = "OutboundOrders", sDataColumn = "OutboundOrder", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundOrders", sDataColumn = "OrderReference", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundOrders", sDataColumn = "OutboundOrderType", sDataColumnPrefix = "s", sFKDataEntity = "OutboundOrderTypes", sFKDataColumn = "OutboundOrderType"},
                new EntityColumn { sDataEntity = "OutboundOrders", sDataColumn = "Facility", sDataColumnPrefix = "s", sFKDataEntity = "Facilities", sFKDataColumn = "Facility"},
                new EntityColumn { sDataEntity = "OutboundOrders", sDataColumn = "Company", sDataColumnPrefix = "s", sFKDataEntity = "Companies", sFKDataColumn = "Company"},
                new EntityColumn { sDataEntity = "OutboundOrders", sDataColumn = "BusinessPartner", sDataColumnPrefix = "s", sFKDataEntity = "BusinessPartners", sFKDataColumn = "BusinessPartner"},
                new EntityColumn { sDataEntity = "OutboundOrders", sDataColumn = "DeliverEarliest", sDataColumnPrefix = "dt", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundOrders", sDataColumn = "DeliverLatest", sDataColumnPrefix = "dt", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundOrders", sDataColumn = "CarrierService", sDataColumnPrefix = "s", sFKDataEntity = "CarrierServices", sFKDataColumn = "CarrierService"},
                new EntityColumn { sDataEntity = "OutboundOrders", sDataColumn = "Status", sDataColumnPrefix = "s", sFKDataEntity = "Statuses", sFKDataColumn = "Status"},
                new EntityColumn { sDataEntity = "OutboundOrders", sDataColumn = "PickBatch", sDataColumnPrefix = "s", sFKDataEntity = "PickBatches", sFKDataColumn = "PickBatch"},
                new EntityColumn { sDataEntity = "OutboundOrders", sDataColumn = "OutboundShipment", sDataColumnPrefix = "s", sFKDataEntity = "OutboundShipments", sFKDataColumn = "OutboundShipment"},
                new EntityColumn { sDataEntity = "OutboundOrderTypes", sDataColumn = "OutboundOrderType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundShipments", sDataColumn = "OutboundShipment", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundShipments", sDataColumn = "Facility", sDataColumnPrefix = "s", sFKDataEntity = "Facilities", sFKDataColumn = "Facility"},
                new EntityColumn { sDataEntity = "OutboundShipments", sDataColumn = "Company", sDataColumnPrefix = "s", sFKDataEntity = "Companies", sFKDataColumn = "Company"},
                new EntityColumn { sDataEntity = "OutboundShipments", sDataColumn = "Carrier", sDataColumnPrefix = "s", sFKDataEntity = "Carriers", sFKDataColumn = "Carrier"},
                new EntityColumn { sDataEntity = "OutboundShipments", sDataColumn = "CarrierConsignmentNumber", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "OutboundShipments", sDataColumn = "Status", sDataColumnPrefix = "s", sFKDataEntity = "Statuses", sFKDataColumn = "Status"},
                new EntityColumn { sDataEntity = "OutboundShipments", sDataColumn = "Address", sDataColumnPrefix = "s", sFKDataEntity = "Addresses", sFKDataColumn = "Address"},
                new EntityColumn { sDataEntity = "OutboundShipments", sDataColumn = "OutboundCarrierManifest", sDataColumnPrefix = "s", sFKDataEntity = "OutboundCarrierManifests", sFKDataColumn = "OutboundCarrierManifest"},
                new EntityColumn { sDataEntity = "People", sDataColumn = "Person", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "People", sDataColumn = "FirstName", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "People", sDataColumn = "LastName", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "People", sDataColumn = "Language", sDataColumnPrefix = "s", sFKDataEntity = "Languages", sFKDataColumn = "Language"},
                new EntityColumn { sDataEntity = "PickBatches", sDataColumn = "PickBatch", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "PickBatches", sDataColumn = "PickBatchType", sDataColumnPrefix = "s", sFKDataEntity = "PickBatchTypes", sFKDataColumn = "PickBatchType"},
                new EntityColumn { sDataEntity = "PickBatches", sDataColumn = "MultiResource", sDataColumnPrefix = "b", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "PickBatches", sDataColumn = "StartBy", sDataColumnPrefix = "dt", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "PickBatches", sDataColumn = "CompleteBy", sDataColumnPrefix = "dt", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "PickBatches", sDataColumn = "Status", sDataColumnPrefix = "s", sFKDataEntity = "Statuses", sFKDataColumn = "Status"},
                new EntityColumn { sDataEntity = "PickBatchTypes", sDataColumn = "PickBatchType", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "PlanetarySystems", sDataColumn = "PlanetarySystem", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "PlanetarySystems", sDataColumn = "Galaxy", sDataColumnPrefix = "s", sFKDataEntity = "Galaxies", sFKDataColumn = "Galaxy"},
                new EntityColumn { sDataEntity = "PlanetRegions", sDataColumn = "PlanetRegion", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "PlanetRegions", sDataColumn = "Planet", sDataColumnPrefix = "s", sFKDataEntity = "Planets", sFKDataColumn = "Planet"},
                new EntityColumn { sDataEntity = "Planets", sDataColumn = "Planet", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Planets", sDataColumn = "PlanetarySystem", sDataColumnPrefix = "s", sFKDataEntity = "PlanetarySystems", sFKDataColumn = "PlanetarySystem"},
                new EntityColumn { sDataEntity = "PlanetSubRegions", sDataColumn = "PlanetSubRegion", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "PlanetSubRegions", sDataColumn = "PlanetRegion", sDataColumnPrefix = "s", sFKDataEntity = "PlanetRegions", sFKDataColumn = "PlanetRegion"},
                new EntityColumn { sDataEntity = "Receiving", sDataColumn = "Receipt", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Receiving", sDataColumn = "InventoryLocation", sDataColumnPrefix = "s", sFKDataEntity = "InventoryLocations", sFKDataColumn = "InventoryLocation"},
                new EntityColumn { sDataEntity = "Receiving", sDataColumn = "InboundOrder", sDataColumnPrefix = "s", sFKDataEntity = "InboundOrders", sFKDataColumn = "InboundOrder"},
                new EntityColumn { sDataEntity = "Receiving", sDataColumn = "HandlingUnit", sDataColumnPrefix = "s", sFKDataEntity = "HandlingUnits", sFKDataColumn = "HandlingUnit"},
                new EntityColumn { sDataEntity = "Receiving", sDataColumn = "Material", sDataColumnPrefix = "s", sFKDataEntity = "Materials", sFKDataColumn = "Material"},
                new EntityColumn { sDataEntity = "Receiving", sDataColumn = "MaterialHandlingUnitConfiguration", sDataColumnPrefix = "s", sFKDataEntity = "MaterialHandlingUnitConfigurations", sFKDataColumn = "MaterialHandlingUnitConfiguration"},
                new EntityColumn { sDataEntity = "Receiving", sDataColumn = "HandlingUnitType", sDataColumnPrefix = "s", sFKDataEntity = "HandlingUnitTypes", sFKDataColumn = "HandlingUnitType"},
                new EntityColumn { sDataEntity = "Receiving", sDataColumn = "HandlingUnitQuantity", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Receiving", sDataColumn = "BatchNumber", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Receiving", sDataColumn = "SerialNumber", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Receiving", sDataColumn = "BaseUnitQuantityReceived", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Receiving", sDataColumn = "Status", sDataColumnPrefix = "s", sFKDataEntity = "Statuses", sFKDataColumn = "Status"},
                new EntityColumn { sDataEntity = "Statuses", sDataColumn = "Status", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Statuses", sDataColumn = "StatusCode", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "UnitOfMeasurementConversions", sDataColumn = "UnitOfMeasurementConversion", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "UnitOfMeasurementConversions", sDataColumn = "UnitOfMeasurementFrom", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "UnitOfMeasurementConversions", sDataColumn = "UnitOfMeasurementTo", sDataColumnPrefix = "s", sFKDataEntity = "UnitsOfMeasurement", sFKDataColumn = "UnitOfMeasurement"},
                new EntityColumn { sDataEntity = "UnitOfMeasurementConversions", sDataColumn = "Multiplier", sDataColumnPrefix = "n", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "UnitsOfMeasurement", sDataColumn = "UnitOfMeasurement", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "UnitsOfMeasurement", sDataColumn = "MeasurementUnitOf", sDataColumnPrefix = "s", sFKDataEntity = "MeasurementUnitsOf", sFKDataColumn = "MeasurementUnitOf"},
                new EntityColumn { sDataEntity = "UnitsOfMeasurement", sDataColumn = "MeasurementSystem", sDataColumnPrefix = "s", sFKDataEntity = "MeasurementSystems", sFKDataColumn = "MeasurementSystem"},
                new EntityColumn { sDataEntity = "UnitsOfMeasurement", sDataColumn = "Symbol", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = ""},
                new EntityColumn { sDataEntity = "Universes", sDataColumn = "Universe", sDataColumnPrefix = "s", sFKDataEntity = "", sFKDataColumn = "" }

            };

        public List<string> ColumnsForEntity(string entityOption)
        {

            var columnsForEntity = from n in EntityColumns
                                            where n.sDataEntity.ToString() == entityOption.ToString()
                                             group n by n.sDataColumn.ToString() into g
                                             select g.Key;

            return columnsForEntity.ToList();
        }

        public List<string> SearchColumnsForEntity(string entityOption)
        {

            var searchColumnsForEntity = from n in EntityColumns
                                   where n.sDataEntity.ToString() == entityOption.ToString() && (n.sDataColumnPrefix == "s" || n.sFKDataEntity != "")
                                   group n by n.sDataColumn.ToString() into g
                                   select g.Key;

            return searchColumnsForEntity.ToList();
        }

    }

    public class NavigationEntityData
    {

        public NavigationEntityData()
        {

        }

        public List<string> NavigationModules()
        {

            NavigationModule[] NavigationModules = new NavigationModule[]
            {

               new NavigationModule { sModule = "Foundation", nSequenceOrder = 1 },
                new NavigationModule { sModule = "Network", nSequenceOrder = 2 },
                new NavigationModule { sModule = "Inbound", nSequenceOrder = 3 },
                new NavigationModule { sModule = "Inventory", nSequenceOrder = 4 },
                new NavigationModule { sModule = "Assembly", nSequenceOrder = 5 },
                new NavigationModule { sModule = "Replenishment", nSequenceOrder = 5 },
                new NavigationModule { sModule = "Projects", nSequenceOrder = 6 },
                new NavigationModule { sModule = "Service", nSequenceOrder = 7 },
                new NavigationModule { sModule = "Outbound", nSequenceOrder = 8 },
                new NavigationModule { sModule = "Execution", nSequenceOrder = 9 }

            };

            var navigationModules = NavigationModules.OrderBy(x => x.nSequenceOrder).Select(x => x.sModule.ToString());

            return navigationModules.ToList();

        }



        public List<string> NavigationEntitiesForModule(string moduleOptions)
        {

            NavigationEntity[] NavigationEntities = new NavigationEntity[]
            {

                new NavigationEntity { sModule = "Foundation", sDataEntity = "People", sDataEntityPredecessors = null, nUIPresentationSequence = 1 },
                new NavigationEntity { sModule = "Foundation", sDataEntity = "Addresses", sDataEntityPredecessors = null, nUIPresentationSequence = 2 },
                new NavigationEntity { sModule = "Network", sDataEntity = "Facilities", sDataEntityPredecessors = "Addresses", nUIPresentationSequence = 3 },
                new NavigationEntity { sModule = "Network", sDataEntity = "Companies", sDataEntityPredecessors = null, nUIPresentationSequence = 4 },
                new NavigationEntity { sModule = "Network", sDataEntity = "BusinessPartners", sDataEntityPredecessors = "Addresses", nUIPresentationSequence = 5 },
                new NavigationEntity { sModule = "Network", sDataEntity = "BusinessPartners", sDataEntityPredecessors = "Companies", nUIPresentationSequence = 5 },
                new NavigationEntity { sModule = "Network", sDataEntity = "Carriers", sDataEntityPredecessors = null, nUIPresentationSequence = 6 },
                new NavigationEntity { sModule = "Network", sDataEntity = "FacilityFloors", sDataEntityPredecessors = null, nUIPresentationSequence = 8 },
                new NavigationEntity { sModule = "Network", sDataEntity = "FacilityZones", sDataEntityPredecessors = null, nUIPresentationSequence = 9 },
                new NavigationEntity { sModule = "Network", sDataEntity = "FacilityWorkAreas", sDataEntityPredecessors = null, nUIPresentationSequence = 10 },
                new NavigationEntity { sModule = "Network", sDataEntity = "InventoryLocationSizes", sDataEntityPredecessors = null, nUIPresentationSequence = 11 },
                new NavigationEntity { sModule = "Network", sDataEntity = "FacilityAisleFaces", sDataEntityPredecessors = "FacilityZones", nUIPresentationSequence = 12 },
                new NavigationEntity { sModule = "Network", sDataEntity = "FacilityAisleFaces", sDataEntityPredecessors = "FacilityFloors", nUIPresentationSequence = 12 },
                new NavigationEntity { sModule = "Network", sDataEntity = "FacilityAisleFaces", sDataEntityPredecessors = "FacilityAisleFaces", nUIPresentationSequence = 12 },
                new NavigationEntity { sModule = "Network", sDataEntity = "FacilityAisleFaces", sDataEntityPredecessors = "InventoryLocationSizes", nUIPresentationSequence = 12 },
                new NavigationEntity { sModule = "Network", sDataEntity = "InventoryLocations", sDataEntityPredecessors = "Companies", nUIPresentationSequence = 13 },
                new NavigationEntity { sModule = "Network", sDataEntity = "InventoryLocations", sDataEntityPredecessors = "FacilityZones", nUIPresentationSequence = 13 },
                new NavigationEntity { sModule = "Network", sDataEntity = "InventoryLocations", sDataEntityPredecessors = "FacilityWorkAreas", nUIPresentationSequence = 13 },
                new NavigationEntity { sModule = "Network", sDataEntity = "InventoryLocations", sDataEntityPredecessors = "FacilityFloors", nUIPresentationSequence = 13 },
                new NavigationEntity { sModule = "Network", sDataEntity = "InventoryLocations", sDataEntityPredecessors = "FacilityAisleFaces", nUIPresentationSequence = 13 },
                new NavigationEntity { sModule = "Network", sDataEntity = "InventoryLocations", sDataEntityPredecessors = "InventoryLocationSizes", nUIPresentationSequence = 13 },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "CarrierServices", sDataEntityPredecessors = "Carriers", nUIPresentationSequence = 7 },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "InboundOrders", sDataEntityPredecessors = "Companies", nUIPresentationSequence = 17 },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "InboundOrders", sDataEntityPredecessors = "Facilities", nUIPresentationSequence = 17 },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "InboundOrders", sDataEntityPredecessors = "BusinessPartners", nUIPresentationSequence = 17 },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "InboundOrderLines", sDataEntityPredecessors = "Materials", nUIPresentationSequence = 18 },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "InboundOrderLines", sDataEntityPredecessors = "MaterialHandlingUnitConfigurations", nUIPresentationSequence = 18 },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "InboundOrderLines", sDataEntityPredecessors = "InboundOrders", nUIPresentationSequence = 18 },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "Receiving", sDataEntityPredecessors = "Materials", nUIPresentationSequence = 19 },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "Receiving", sDataEntityPredecessors = "HandlingUnits", nUIPresentationSequence = 19 },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "Receiving", sDataEntityPredecessors = "InventoryLocations", nUIPresentationSequence = 19 },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "Receiving", sDataEntityPredecessors = "MaterialHandlingUnitConfigurations", nUIPresentationSequence = 19 },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "Receiving", sDataEntityPredecessors = "InboundOrders", nUIPresentationSequence = 19 },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "Materials", sDataEntityPredecessors = null, nUIPresentationSequence = 14 },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "MaterialHandlingUnitConfigurations", sDataEntityPredecessors = "Materials", nUIPresentationSequence = 15 },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "InventoryLocationsSlotting", sDataEntityPredecessors = "Materials", nUIPresentationSequence = 16 },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "InventoryLocationsSlotting", sDataEntityPredecessors = "InventoryLocations", nUIPresentationSequence = 16 },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "HandlingUnits", sDataEntityPredecessors = "Materials", nUIPresentationSequence = 20 },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "HandlingUnits", sDataEntityPredecessors = "HandlingUnits", nUIPresentationSequence = 20 },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "HandlingUnits", sDataEntityPredecessors = "MaterialHandlingUnitConfigurations", nUIPresentationSequence = 20 },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "InventoryUnits", sDataEntityPredecessors = "Companies", nUIPresentationSequence = 21 },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "InventoryUnits", sDataEntityPredecessors = "Facilities", nUIPresentationSequence = 21 },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "InventoryUnits", sDataEntityPredecessors = "Materials", nUIPresentationSequence = 21 },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "InventoryUnits", sDataEntityPredecessors = "HandlingUnits", nUIPresentationSequence = 21 },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "InventoryUnits", sDataEntityPredecessors = "InventoryLocations", nUIPresentationSequence = 21 },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundOrders", sDataEntityPredecessors = "Companies", nUIPresentationSequence = 22 },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundOrders", sDataEntityPredecessors = "Facilities", nUIPresentationSequence = 22 },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundOrders", sDataEntityPredecessors = "BusinessPartners", nUIPresentationSequence = 22 },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundOrders", sDataEntityPredecessors = "CarrierServices", nUIPresentationSequence = 22 },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundOrders", sDataEntityPredecessors = "OutboundShipments", nUIPresentationSequence = 22 },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundOrders", sDataEntityPredecessors = "PickBatches", nUIPresentationSequence = 22 },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundOrderLines", sDataEntityPredecessors = "Materials", nUIPresentationSequence = 23 },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "PickBatches", sDataEntityPredecessors = null, nUIPresentationSequence = 24 },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundShipments", sDataEntityPredecessors = "Addresses", nUIPresentationSequence = 25 },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundShipments", sDataEntityPredecessors = "Companies", nUIPresentationSequence = 25 },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundShipments", sDataEntityPredecessors = "Facilities", nUIPresentationSequence = 25 },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundShipments", sDataEntityPredecessors = "Carriers", nUIPresentationSequence = 25 },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundShipments", sDataEntityPredecessors = "OutboundCarrierManifests", nUIPresentationSequence = 25 },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundCarrierManifests", sDataEntityPredecessors = "InventoryLocations", nUIPresentationSequence = 26 },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundCarrierManifests", sDataEntityPredecessors = "Carriers", nUIPresentationSequence = 26 },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundCarrierManifestPickups", sDataEntityPredecessors = "OutboundCarrierManifests", nUIPresentationSequence = 27 },
                new NavigationEntity { sModule = "Execution", sDataEntity = "MoveQueues", sDataEntityPredecessors = "InventoryUnits", nUIPresentationSequence = 0 },
                new NavigationEntity { sModule = "Execution", sDataEntity = "MoveQueues", sDataEntityPredecessors = "HandlingUnits", nUIPresentationSequence = 0 },
                new NavigationEntity { sModule = "Execution", sDataEntity = "MoveQueues", sDataEntityPredecessors = "InventoryLocations", nUIPresentationSequence = 0 },
                new NavigationEntity { sModule = "Execution", sDataEntity = "MoveQueues", sDataEntityPredecessors = "InboundOrderLines", nUIPresentationSequence = 0 },
                new NavigationEntity { sModule = "Execution", sDataEntity = "MoveQueues", sDataEntityPredecessors = "OutboundOrderLines", nUIPresentationSequence = 0 },
                new NavigationEntity { sModule = "Execution", sDataEntity = "MoveQueues", sDataEntityPredecessors = "PickBatches", nUIPresentationSequence = 0 }
            };


            var topLevelNavigationEntities = from n in NavigationEntities
                                             where n.sModule.ToString() == moduleOptions.ToString()
                                             orderby n.nUIPresentationSequence
                                             group n by n.sDataEntity.ToString() into g
                                             select g.Key;

           return topLevelNavigationEntities.ToList();

        }

        public List<string> BranchEntitiesForEntity(string entityOptions)
        {

            NavigationEntity[] NavigationEntities = new NavigationEntity[]
            {

                new NavigationEntity { sModule = "Foundation", sDataEntity = "People", sDataEntityPredecessors = null },
                new NavigationEntity { sModule = "Foundation", sDataEntity = "Addresses", sDataEntityPredecessors = null },
                new NavigationEntity { sModule = "Network", sDataEntity = "Facilities", sDataEntityPredecessors = "Addresses" },
                new NavigationEntity { sModule = "Network", sDataEntity = "Companies", sDataEntityPredecessors = null },
                new NavigationEntity { sModule = "Network", sDataEntity = "BusinessPartners", sDataEntityPredecessors = "Addresses" },
                new NavigationEntity { sModule = "Network", sDataEntity = "BusinessPartners", sDataEntityPredecessors = "Companies" },
                new NavigationEntity { sModule = "Network", sDataEntity = "Carriers", sDataEntityPredecessors = null },
                new NavigationEntity { sModule = "Network", sDataEntity = "FacilityFloors", sDataEntityPredecessors = null },
                new NavigationEntity { sModule = "Network", sDataEntity = "FacilityZones", sDataEntityPredecessors = null },
                new NavigationEntity { sModule = "Network", sDataEntity = "FacilityWorkAreas", sDataEntityPredecessors = null },
                new NavigationEntity { sModule = "Network", sDataEntity = "InventoryLocationSizes", sDataEntityPredecessors = null },
                new NavigationEntity { sModule = "Network", sDataEntity = "FacilityAisleFaces", sDataEntityPredecessors = "FacilityZones" },
                new NavigationEntity { sModule = "Network", sDataEntity = "FacilityAisleFaces", sDataEntityPredecessors = "FacilityFloors" },
                new NavigationEntity { sModule = "Network", sDataEntity = "FacilityAisleFaces", sDataEntityPredecessors = "FacilityAisleFaces" },
                new NavigationEntity { sModule = "Network", sDataEntity = "FacilityAisleFaces", sDataEntityPredecessors = "InventoryLocationSizes" },
                new NavigationEntity { sModule = "Network", sDataEntity = "InventoryLocations", sDataEntityPredecessors = "Companies" },
                new NavigationEntity { sModule = "Network", sDataEntity = "InventoryLocations", sDataEntityPredecessors = "FacilityZones" },
                new NavigationEntity { sModule = "Network", sDataEntity = "InventoryLocations", sDataEntityPredecessors = "FacilityWorkAreas" },
                new NavigationEntity { sModule = "Network", sDataEntity = "InventoryLocations", sDataEntityPredecessors = "FacilityFloors" },
                new NavigationEntity { sModule = "Network", sDataEntity = "InventoryLocations", sDataEntityPredecessors = "FacilityAisleFaces" },
                new NavigationEntity { sModule = "Network", sDataEntity = "InventoryLocations", sDataEntityPredecessors = "InventoryLocationSizes" },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "CarrierServices", sDataEntityPredecessors = "Carriers" },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "InboundOrders", sDataEntityPredecessors = "Companies" },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "InboundOrders", sDataEntityPredecessors = "Facilities" },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "InboundOrders", sDataEntityPredecessors = "BusinessPartners" },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "InboundOrderLines", sDataEntityPredecessors = "Materials" },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "InboundOrderLines", sDataEntityPredecessors = "MaterialHandlingUnitConfigurations" },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "InboundOrderLines", sDataEntityPredecessors = "InboundOrders" },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "Receiving", sDataEntityPredecessors = "Materials" },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "Receiving", sDataEntityPredecessors = "HandlingUnits" },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "Receiving", sDataEntityPredecessors = "InventoryLocations" },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "Receiving", sDataEntityPredecessors = "MaterialHandlingUnitConfigurations" },
                new NavigationEntity { sModule = "Inbound", sDataEntity = "Receiving", sDataEntityPredecessors = "InboundOrders" },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "Materials", sDataEntityPredecessors = null },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "MaterialHandlingUnitConfigurations", sDataEntityPredecessors = "Materials" },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "InventoryLocationsSlotting", sDataEntityPredecessors = "Materials" },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "InventoryLocationsSlotting", sDataEntityPredecessors = "InventoryLocations" },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "HandlingUnits", sDataEntityPredecessors = "Materials" },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "HandlingUnits", sDataEntityPredecessors = "HandlingUnits" },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "HandlingUnits", sDataEntityPredecessors = "MaterialHandlingUnitConfigurations" },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "InventoryUnits", sDataEntityPredecessors = "Companies" },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "InventoryUnits", sDataEntityPredecessors = "Facilities" },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "InventoryUnits", sDataEntityPredecessors = "Materials" },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "InventoryUnits", sDataEntityPredecessors = "HandlingUnits" },
                new NavigationEntity { sModule = "Inventory", sDataEntity = "InventoryUnits", sDataEntityPredecessors = "InventoryLocations" },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundOrders", sDataEntityPredecessors = "Companies" },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundOrders", sDataEntityPredecessors = "Facilities" },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundOrders", sDataEntityPredecessors = "BusinessPartners" },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundOrders", sDataEntityPredecessors = "CarrierServices" },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundOrders", sDataEntityPredecessors = "OutboundShipments" },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundOrders", sDataEntityPredecessors = "PickBatches" },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundOrderLines", sDataEntityPredecessors = "Materials" },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "PickBatches", sDataEntityPredecessors = null },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundShipments", sDataEntityPredecessors = "Addresses" },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundShipments", sDataEntityPredecessors = "Companies" },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundShipments", sDataEntityPredecessors = "Facilities" },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundShipments", sDataEntityPredecessors = "Carriers" },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundShipments", sDataEntityPredecessors = "OutboundCarrierManifests" },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundCarrierManifests", sDataEntityPredecessors = "InventoryLocations" },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundCarrierManifests", sDataEntityPredecessors = "Carriers" },
                new NavigationEntity { sModule = "Outbound", sDataEntity = "OutboundCarrierManifestPickups", sDataEntityPredecessors = "OutboundCarrierManifests" },
                new NavigationEntity { sModule = "Execution", sDataEntity = "MoveQueues", sDataEntityPredecessors = "InventoryUnits" },
                new NavigationEntity { sModule = "Execution", sDataEntity = "MoveQueues", sDataEntityPredecessors = "HandlingUnits" },
                new NavigationEntity { sModule = "Execution", sDataEntity = "MoveQueues", sDataEntityPredecessors = "InventoryLocations" },
                new NavigationEntity { sModule = "Execution", sDataEntity = "MoveQueues", sDataEntityPredecessors = "InboundOrderLines" },
                new NavigationEntity { sModule = "Execution", sDataEntity = "MoveQueues", sDataEntityPredecessors = "OutboundOrderLines" },
                new NavigationEntity { sModule = "Execution", sDataEntity = "MoveQueues", sDataEntityPredecessors = "PickBatches" }
            };

            string[] rootEntity = { entityOptions, "Choose an area" };

            var branchEntities =
                (from n in NavigationEntities
                where n.sDataEntityPredecessors != null && n.sDataEntityPredecessors.ToString() == entityOptions.ToString()
                select n.sDataEntity).Union(rootEntity);

            return branchEntities.ToList();

        }

        public List<string> CrudActionForEntities(string entityOptions)
        {

            EntityCrudAction[] EntityCrudActions = new EntityCrudAction[]
            {

                new EntityCrudAction { sDataEntity = "People", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "People", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "People", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "People", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "Addresses", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "Addresses", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "Addresses", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "Addresses", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "Companies", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "Companies", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "Companies", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "Companies", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "Facilities", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "Facilities", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "Facilities", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "Facilities", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "FacilityZones", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "FacilityZones", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "FacilityZones", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "FacilityZones", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "FacilityWorkAreas", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "FacilityWorkAreas", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "FacilityWorkAreas", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "FacilityWorkAreas", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "FacilityFloors", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "FacilityFloors", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "FacilityFloors", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "FacilityFloors", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "FacilityAisleFaces", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "FacilityAisleFaces", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "FacilityAisleFaces", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "FacilityAisleFaces", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "InventoryLocationSizes", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "InventoryLocationSizes", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "InventoryLocationSizes", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "InventoryLocationSizes", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "Materials", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "Materials", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "Materials", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "Materials", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "InventoryUnits", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "InventoryUnits", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "InventoryUnits", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "InventoryUnits", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "HandlingUnits", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "HandlingUnits", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "HandlingUnits", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "HandlingUnits", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "InventoryLocations", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "InventoryLocations", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "InventoryLocations", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "InventoryLocations", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "MoveQueues", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "MoveQueues", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "MoveQueues", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "MoveQueues", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "MaterialHandlingUnitConfigurations", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "MaterialHandlingUnitConfigurations", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "MaterialHandlingUnitConfigurations", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "MaterialHandlingUnitConfigurations", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "BusinessPartners", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "BusinessPartners", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "BusinessPartners", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "BusinessPartners", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "InboundOrders", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "InboundOrders", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "InboundOrders", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "InboundOrders", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "InboundOrderLines", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "InboundOrderLines", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "InboundOrderLines", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "InboundOrderLines", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "Carriers", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "Carriers", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "Carriers", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "Carriers", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "CarrierServices", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "CarrierServices", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "CarrierServices", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "CarrierServices", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "OutboundOrders", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "OutboundOrders", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "OutboundOrders", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "OutboundOrders", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "OutboundShipments", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "OutboundShipments", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "OutboundShipments", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "OutboundShipments", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "OutboundCarrierManifests", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "OutboundCarrierManifests", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "OutboundCarrierManifests", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "OutboundCarrierManifests", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "OutboundOrderLines", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "OutboundOrderLines", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "OutboundOrderLines", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "OutboundOrderLines", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "PickBatches", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "PickBatches", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "PickBatches", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "PickBatches", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "Receiving", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "Receiving", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "Receiving", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "Receiving", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "OutboundCarrierManifestPickups", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "OutboundCarrierManifestPickups", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "OutboundCarrierManifestPickups", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "OutboundCarrierManifestPickups", sCrudAction = "Edit" },
                new EntityCrudAction { sDataEntity = "InventoryLocationsSlotting", sCrudAction = "Create" },
                new EntityCrudAction { sDataEntity = "InventoryLocationsSlotting", sCrudAction = "Delete" },
                new EntityCrudAction { sDataEntity = "InventoryLocationsSlotting", sCrudAction = "Details" },
                new EntityCrudAction { sDataEntity = "InventoryLocationsSlotting", sCrudAction = "Edit" }
            };


            var crudActions =
                from n in EntityCrudActions
                where n.sDataEntity.ToString() == entityOptions.ToString()
                select n.sCrudAction;

            return crudActions.ToList();

        }

    }

    public enum ModuleOptions { [Display(Name = "Execution")] Execution,[Display(Name = "Foundation")] Foundation,[Display(Name = "Inbound")] Inbound,[Display(Name = "Inventory")] Inventory,[Display(Name = "Network")] Network,[Display(Name = "Outbound")] Outbound };

	public enum boolOptions {On,Off};

}
