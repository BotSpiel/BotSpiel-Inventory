using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPurchasesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixPurchase { get; set; }
		String sPurchase { get; set; }
		Int64 ixPerson { get; set; }
		Int64? ixCompany { get; set; }
		String UserName { get; set; }
    }
}
  

