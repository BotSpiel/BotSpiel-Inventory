using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PurchaseLinesPost : IPurchaseLinesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Purchase Line ID")]
		public virtual Int64 ixPurchaseLine { get; set; }
		[Display(Name = "Purchase Line")]
		public virtual String sPurchaseLine { get; set; }
		[Required]
		[Display(Name = "Purchase ID")]
		public virtual Int64 ixPurchase { get; set; }
		[Required]
		[Display(Name = "Material ID")]
		public virtual Int64 ixMaterial { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

