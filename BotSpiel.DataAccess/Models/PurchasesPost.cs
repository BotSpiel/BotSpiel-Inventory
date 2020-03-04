using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PurchasesPost : IPurchasesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Purchase ID")]
		public virtual Int64 ixPurchase { get; set; }
		[Display(Name = "Purchase")]
		public virtual String sPurchase { get; set; }
		[Required]
		[Display(Name = "Person ID")]
		public virtual Int64 ixPerson { get; set; }
		[Display(Name = "Company ID")]
		public virtual Int64? ixCompany { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

