using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IFacilityAisleFacesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixFacilityAisleFace { get; set; }
		String sFacilityAisleFace { get; set; }
		Int64 ixFacility { get; set; }
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
		String UserName { get; set; }
    }
}
  

