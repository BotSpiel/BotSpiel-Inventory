using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IOutboundOrderPackingPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixOutboundOrderPack { get; set; }
		String sOutboundOrderPack { get; set; }
		Int64 ixOutboundOrderLine { get; set; }
		Int64 ixHandlingUnit { get; set; }
		Int64 ixStatus { get; set; }
		String UserName { get; set; }
    }
}
  

