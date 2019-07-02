using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IFacilityWorkAreasPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixFacilityWorkArea { get; set; }
		String sFacilityWorkArea { get; set; }
		String UserName { get; set; }
    }
}
  

