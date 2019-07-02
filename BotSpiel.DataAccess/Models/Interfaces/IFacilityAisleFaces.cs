using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IFacilityAisleFaces
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixFacilityAisleFace { get; set; }
		Int64 ixFacilityAisleFaceEdit { get; set; }
		String sFacilityAisleFace { get; set; }
		Int64 ixFacilityFloor { get; set; }
		Int64 nSequence { get; set; }
		Int64 ixBaySequenceType { get; set; }
		Int64? ixPairedAisleFace { get; set; }
		Int64 ixLogicalOrientation { get; set; }
		Int64 ixAisleFaceStorageType { get; set; }
		Double? nXOffset { get; set; }
		Int64? ixXOffsetUnit { get; set; }
		Double? nYOffset { get; set; }
		Int64? ixYOffsetUnit { get; set; }
		Int32 nLevels { get; set; }
		Int32? nDefaultNumberOfBays { get; set; }
		Int32? nDefaultNumberOfSlotsInBay { get; set; }
		Int64? ixDefaultFacilityZone { get; set; }
		Int64? ixDefaultLocationFunction { get; set; }
		Int64 ixDefaultInventoryLocationSize { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		FacilityFloors FacilityFloors { get; set; }
		BaySequenceTypes BaySequenceTypes { get; set; }
		FacilityAisleFaces FacilityAisleFacesFKDiffPairedAisleFace { get; set; }
		LogicalOrientations LogicalOrientations { get; set; }
		AisleFaceStorageTypes AisleFaceStorageTypes { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffXOffsetUnit { get; set; }
		UnitsOfMeasurement UnitsOfMeasurementFKDiffYOffsetUnit { get; set; }
		FacilityZones FacilityZonesFKDiffDefaultFacilityZone { get; set; }
		LocationFunctions LocationFunctionsFKDiffDefaultLocationFunction { get; set; }
		InventoryLocationSizes InventoryLocationSizesFKDiffDefaultInventoryLocationSize { get; set; }
    }
}
  

