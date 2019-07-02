using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ICarriersPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixCarrier { get; set; }
		String sCarrier { get; set; }
		Int64 ixCarrierType { get; set; }
		String sStandardCarrierAlphaCode { get; set; }
		String sCarrierConsignmentNumberPrefix { get; set; }
		Int64 nCarrierConsignmentNumberStart { get; set; }
		Int64? nCarrierConsignmentNumberLastUsed { get; set; }
		TimeSpan? dtScheduledPickupTime { get; set; }
		String UserName { get; set; }
    }
}
  

