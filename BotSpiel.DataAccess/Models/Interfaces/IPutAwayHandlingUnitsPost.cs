using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPutAwayHandlingUnitsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixPutAwayHandlingUnit { get; set; }
		String sPutAwayHandlingUnit { get; set; }
		String sInventoryDropLocation { get; set; }
		Int64 ixHandlingUnit { get; set; }
		Int64 ixInventoryLocation { get; set; }
		String UserName { get; set; }
    }
}
  

