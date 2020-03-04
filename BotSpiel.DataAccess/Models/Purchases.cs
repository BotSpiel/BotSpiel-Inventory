using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class Purchases : IPurchases
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public Purchases()
        {
		People _People = new People();
		People = _People;
		Companies _Companies = new Companies();
		Companies = _Companies;

        }
		[Display(Name = "Purchase ID")]
		public virtual Int64 ixPurchase { get; set; }
		[Display(Name = "Purchase ID")]
		public virtual Int64 ixPurchaseEdit { get; set; }
		[Display(Name = "Purchase")]
		public virtual String sPurchase { get; set; }
		[Required]
		[Display(Name = "Person ID")]
		public virtual Int64 ixPerson { get; set; }
		[Display(Name = "Company ID")]
		public virtual Int64? ixCompany { get; set; }
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
		[ForeignKey("ixPerson")]
		public virtual People People { get; set; }
		[ForeignKey("ixCompany")]
		public virtual Companies Companies { get; set; }
    }
}
  

