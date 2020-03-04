using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPurchaseLinesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixPurchaseLine { get; set; }
		String sPurchaseLine { get; set; }
		Int64 ixPurchase { get; set; }
		Int64 ixMaterial { get; set; }
		String UserName { get; set; }
    }
}
  

