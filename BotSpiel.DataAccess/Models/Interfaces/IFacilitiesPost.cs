using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IFacilitiesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixFacility { get; set; }
		String sFacility { get; set; }
		Int64 ixAddress { get; set; }
		String sLatitude { get; set; }
		String sLongitude { get; set; }
		String UserName { get; set; }
    }
}
  

