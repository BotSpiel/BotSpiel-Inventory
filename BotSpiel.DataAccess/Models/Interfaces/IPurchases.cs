using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPurchases
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixPurchase { get; set; }
		Int64 ixPurchaseEdit { get; set; }
		String sPurchase { get; set; }
		Int64 ixPerson { get; set; }
		Int64? ixCompany { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		People People { get; set; }
		Companies Companies { get; set; }
    }
}
  

