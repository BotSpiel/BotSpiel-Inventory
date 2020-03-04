using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IOutboundOrderLinePackingPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixOutboundOrderLinePack { get; set; }
		String sOutboundOrderLinePack { get; set; }
		Int64 ixOutboundOrderLine { get; set; }
		Int64 ixHandlingUnit { get; set; }
		Double nBaseUnitQuantityPacked { get; set; }
		Int64 ixStatus { get; set; }
		String UserName { get; set; }
    }
}
  

