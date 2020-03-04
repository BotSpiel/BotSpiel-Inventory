using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPurchaseLines
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixPurchaseLine { get; set; }
		Int64 ixPurchaseLineEdit { get; set; }
		String sPurchaseLine { get; set; }
		Int64 ixPurchase { get; set; }
		Int64 ixMaterial { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		Purchases Purchases { get; set; }
		Materials Materials { get; set; }
    }
}
  

