using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInventoryLocationsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixInventoryLocation { get; set; }
		String sInventoryLocation { get; set; }
		Int64 ixLocationFunction { get; set; }
		Int64? ixCompany { get; set; }
		Int64 ixFacilityFloor { get; set; }
		Int64 ixFacilityZone { get; set; }
		Int64 ixFacilityWorkArea { get; set; }
		Int64 ixFacilityAisleFace { get; set; }
		String sLevel { get; set; }
		String sBay { get; set; }
		String sSlot { get; set; }
		Int64? ixInventoryLocationSize { get; set; }
		Int64 nSequence { get; set; }
		Double? nXOffset { get; set; }
		Int64? ixXOffsetUnit { get; set; }
		Double? nYOffset { get; set; }
		Int64? ixYOffsetUnit { get; set; }
		Double? nZOffset { get; set; }
		Int64? ixZOffsetUnit { get; set; }
		String sLatitude { get; set; }
		String sLongitude { get; set; }
		Boolean bTrackUtilisation { get; set; }
		Double? nUtilisationPercent { get; set; }
		Double? nQueuedUtilisationPercent { get; set; }
		String UserName { get; set; }
    }
}
  

