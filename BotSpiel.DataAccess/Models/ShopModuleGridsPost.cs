using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class ShopModuleGridsPost : IShopModuleGridsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Shop Module Grid ID")]
		public virtual Int64 ixShopModuleGrid { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Shop Module Grid")]
		public virtual String sShopModuleGrid { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Short Description")]
		public virtual String sShortDescription { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Data Entity Type")]
		public virtual String sDataEntityType { get; set; }
		[Required]
		[Display(Name = "Can Create")]
		public virtual Boolean bCanCreate { get; set; }
		[Required]
		[Display(Name = "Can Edit")]
		public virtual Boolean bCanEdit { get; set; }
		[Required]
		[Display(Name = "Can Delete")]
		public virtual Boolean bCanDelete { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

