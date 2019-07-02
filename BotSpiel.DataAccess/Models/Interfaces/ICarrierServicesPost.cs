using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ICarrierServicesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixCarrierService { get; set; }
		String sCarrierService { get; set; }
		Int64 ixCarrier { get; set; }
		String UserName { get; set; }
    }
}
  

