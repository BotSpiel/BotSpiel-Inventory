using System;
using System.Collections.Generic;
using System.Text;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Models
{
    public class BotUserData : IBotUserData
    {
        public BotUserEntityContext botUserEntityContext { get; set; }
	public Int64  ixCountry { get; set; }
		public Int64  ixLocationFunction { get; set; }
		public Int64  ixStatus { get; set; }
		public Int64  ixLanguage { get; set; }
		public Int64  ixPerson { get; set; }
		public Int64  ixAddress { get; set; }
		public Int64  ixStateOrProvince { get; set; }
		public Int64  ixCompany { get; set; }
		public Int64  ixFacility { get; set; }
		public Int64  ixFacilityZone { get; set; }
		public Int64  ixFacilityWorkArea { get; set; }
		public Int64  ixFacilityFloor { get; set; }
		public Int64  ixFacilityAisleFace { get; set; }
		public Int64  ixBaySequenceType { get; set; }
		public Int64  ixPairedAisleFace { get; set; }
		public Int64  ixLogicalOrientation { get; set; }
		public Int64  ixAisleFaceStorageType { get; set; }
		public Int64  ixXOffsetUnit { get; set; }
		public Int64  ixYOffsetUnit { get; set; }
		public Int64  ixZOffsetUnit { get; set; }
		public Int64  ixDefaultFacilityZone { get; set; }
		public Int64  ixDefaultLocationFunction { get; set; }
		public Int64  ixInventoryLocationSize { get; set; }
		public Int64  ixLengthUnit { get; set; }
		public Int64  ixWidthUnit { get; set; }
		public Int64  ixHeightUnit { get; set; }
		public Int64  ixUsableVolumeUnit { get; set; }
		public Int64  ixDefaultInventoryLocationSize { get; set; }
		public Int64  ixMaterial { get; set; }
		public Int64  ixMaterialType { get; set; }
		public Int64  ixBaseUnit { get; set; }
		public Int64  ixDensityUnit { get; set; }
		public Int64  ixShelflifeUnit { get; set; }
		public Int64  ixInventoryUnit { get; set; }
		public Int64  ixInventoryState { get; set; }
		public Int64  ixHandlingUnit { get; set; }
		public Int64  ixHandlingUnitType { get; set; }
		public Int64  ixParentHandlingUnit { get; set; }
		public Int64  ixWeightUnit { get; set; }
		public Int64  ixInventoryLocation { get; set; }
		public Int64  ixMoveQueueContext { get; set; }
		public Int64  ixMoveQueue { get; set; }
		public Int64  ixMoveQueueType { get; set; }
		public Int64  ixSourceInventoryLocation { get; set; }
		public Int64  ixTargetInventoryLocation { get; set; }
		public Int64  ixSourceInventoryUnit { get; set; }
		public Int64  ixTargetInventoryUnit { get; set; }
		public Int64  ixSourceHandlingUnit { get; set; }
		public Int64  ixTargetHandlingUnit { get; set; }
		public Int64  ixMaterialHandlingUnitConfiguration { get; set; }
		public Int64  ixBusinessPartnerType { get; set; }
		public Int64  ixBusinessPartner { get; set; }
		public Int64  ixInboundOrderType { get; set; }
		public Int64  ixInboundOrder { get; set; }
		public Int64  ixInboundOrderLine { get; set; }
		public Int64  ixOutboundOrderType { get; set; }
		public Int64  ixCarrier { get; set; }
		public Int64  ixCarrierType { get; set; }
		public Int64  ixCarrierService { get; set; }
		public Int64  ixOutboundOrder { get; set; }
		public Int64  ixOutboundShipment { get; set; }
		public Int64  ixOutboundCarrierManifest { get; set; }
		public Int64  ixPickupInventoryLocation { get; set; }
		public Int64  ixOutboundOrderLine { get; set; }
		public Int64  ixPickBatchType { get; set; }
		public Int64  ixPickBatch { get; set; }
		public Int64  ixReceipt { get; set; }
		public Int64  ixOutboundCarrierManifestPickup { get; set; }
		public Int64  ixInventoryLocationSlotting { get; set; }
		public Int64  ixPackingMaterial { get; set; }

    }
}

