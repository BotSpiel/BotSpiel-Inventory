using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IHandlingUnitsShippingPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixHandlingUnitShipping { get; set; }
		String sHandlingUnitShipping { get; set; }
		Int64 ixHandlingUnit { get; set; }
		Int64 ixStatus { get; set; }
		String UserName { get; set; }
    }
}
  

