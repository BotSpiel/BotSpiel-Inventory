using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInboundOrdersPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixInboundOrder { get; set; }
		String sInboundOrder { get; set; }
		String sOrderReference { get; set; }
		Int64 ixInboundOrderType { get; set; }
		Int64 ixFacility { get; set; }
		Int64 ixCompany { get; set; }
		Int64 ixBusinessPartner { get; set; }
		DateTime? dtExpectedAt { get; set; }
		Int64 ixStatus { get; set; }
		String UserName { get; set; }
    }
}
  

