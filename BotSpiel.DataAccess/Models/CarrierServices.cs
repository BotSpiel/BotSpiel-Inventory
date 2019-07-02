using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class CarrierServices : ICarrierServices
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public CarrierServices()
        {
		Carriers _Carriers = new Carriers();
		Carriers = _Carriers;

        }
		[Display(Name = "Carrier Service ID")]
		public virtual Int64 ixCarrierService { get; set; }
		[Display(Name = "Carrier Service ID")]
		public virtual Int64 ixCarrierServiceEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Carrier Service")]
		public virtual String sCarrierService { get; set; }
		[Required]
		[Display(Name = "Carrier ID")]
		public virtual Int64 ixCarrier { get; set; }
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
		[ForeignKey("ixCarrier")]
		public virtual Carriers Carriers { get; set; }
    }
}
  

