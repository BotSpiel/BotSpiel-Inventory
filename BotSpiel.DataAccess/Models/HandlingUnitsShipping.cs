using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class HandlingUnitsShipping : IHandlingUnitsShipping
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public HandlingUnitsShipping()
        {
		HandlingUnits _HandlingUnits = new HandlingUnits();
		HandlingUnits = _HandlingUnits;
		Statuses _Statuses = new Statuses();
		Statuses = _Statuses;

        }
		[Display(Name = "Handling Unit Shipping ID")]
		public virtual Int64 ixHandlingUnitShipping { get; set; }
		[Display(Name = "Handling Unit Shipping ID")]
		public virtual Int64 ixHandlingUnitShippingEdit { get; set; }
		[Display(Name = "Handling Unit Shipping")]
		public virtual String sHandlingUnitShipping { get; set; }
		[Required]
		[Display(Name = "Handling Unit ID")]
		public virtual Int64 ixHandlingUnit { get; set; }
		[Required]
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatus { get; set; }
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
		[ForeignKey("ixHandlingUnit")]
		public virtual HandlingUnits HandlingUnits { get; set; }
		[ForeignKey("ixStatus")]
		public virtual Statuses Statuses { get; set; }
    }
}
  

