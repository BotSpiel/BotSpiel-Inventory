using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class PurchaseLines : IPurchaseLines
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public PurchaseLines()
        {
		Purchases _Purchases = new Purchases();
		Purchases = _Purchases;
		Materials _Materials = new Materials();
		Materials = _Materials;

        }
		[Display(Name = "Purchase Line ID")]
		public virtual Int64 ixPurchaseLine { get; set; }
		[Display(Name = "Purchase Line ID")]
		public virtual Int64 ixPurchaseLineEdit { get; set; }
		[Display(Name = "Purchase Line")]
		public virtual String sPurchaseLine { get; set; }
		[Required]
		[Display(Name = "Purchase ID")]
		public virtual Int64 ixPurchase { get; set; }
		[Required]
		[Display(Name = "Material ID")]
		public virtual Int64 ixMaterial { get; set; }
		[Required]
		[Display(Name = "Created At")]
		public virtual DateTime dtCreatedAt { get; set; }
		[Required]
		[Display(Name = "Changed At")]
		public virtual DateTime dtChangedAt { get; set; }
		[StringLength(256)]
		[Required]
		[Display(Name = "Created By")]
		public virtual String sCreatedBy { get; set; }
		[StringLength(256)]
		[Required]
		[Display(Name = "Changed By")]
		public virtual String sChangedBy { get; set; }
		[ForeignKey("ixPurchase")]
		public virtual Purchases Purchases { get; set; }
		[ForeignKey("ixMaterial")]
		public virtual Materials Materials { get; set; }
    }
}
  

