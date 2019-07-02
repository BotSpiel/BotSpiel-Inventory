using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IFacilities
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixFacility { get; set; }
		Int64 ixFacilityEdit { get; set; }
		String sFacility { get; set; }
		Int64 ixAddress { get; set; }
		String sLatitude { get; set; }
		String sLongitude { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Addresses Addresses { get; set; }
    }
}
  

