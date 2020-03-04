using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InboundOrderLines : IInboundOrderLines
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public InboundOrderLines()
        {
		InboundOrders _InboundOrders = new InboundOrders();
		InboundOrders = _InboundOrders;
		Materials _Materials = new Materials();
		Materials = _Materials;
		MaterialHandlingUnitConfigurations _MaterialHandlingUnitConfigurations = new MaterialHandlingUnitConfigurations();
		MaterialHandlingUnitConfigurations = _MaterialHandlingUnitConfigurations;
		HandlingUnitTypes _HandlingUnitTypes = new HandlingUnitTypes();
		HandlingUnitTypes = _HandlingUnitTypes;
		Statuses _Statuses = new Statuses();
		Statuses = _Statuses;

        }
		[Display(Name = "Inbound Order Line ID")]
		public virtual Int64 ixInboundOrderLine { get; set; }
		[Display(Name = "Inbound Order Line ID")]
		public virtual Int64 ixInboundOrderLineEdit { get; set; }
		[Display(Name = "Inbound Order Line")]
		public virtual String sInboundOrderLine { get; set; }
		[Required]
		[Display(Name = "Inbound Order ID")]
		public virtual Int64 ixInboundOrder { get; set; }
		[StringLength(300)]
		[Required]
		[Display(Name = "Order Line Reference")]
		public virtual String sOrderLineReference { get; set; }
		[Required]
		[Display(Name = "Material ID")]
		public virtual Int64 ixMaterial { get; set; }
		[Display(Name = "Material Handling Unit Configuration ID")]
		public virtual Int64? ixMaterialHandlingUnitConfiguration { get; set; }
		[Display(Name = "Handling Unit Type ID")]
		public virtual Int64? ixHandlingUnitType { get; set; }
		[Display(Name = "Handling Unit Quantity")]
		public virtual Double? nHandlingUnitQuantity { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity Expected")]
		public virtual Double nBaseUnitQuantityExpected { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity Received")]
		public virtual Double nBaseUnitQuantityReceived { get; set; }
		[StringLength(100)]
		[Display(Name = "Serial Number")]
		public virtual String sSerialNumber { get; set; }
		[StringLength(100)]
		[Display(Name = "Batch Number")]
		public virtual String sBatchNumber { get; set; }
		[Display(Name = "Expire At")]
		public virtual DateTime? dtExpireAt { get; set; }
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
		[ForeignKey("ixInboundOrder")]
		public virtual InboundOrders InboundOrders { get; set; }
		[ForeignKey("ixMaterial")]
		public virtual Materials Materials { get; set; }
		[ForeignKey("ixMaterialHandlingUnitConfiguration")]
		public virtual MaterialHandlingUnitConfigurations MaterialHandlingUnitConfigurations { get; set; }
		[ForeignKey("ixHandlingUnitType")]
		public virtual HandlingUnitTypes HandlingUnitTypes { get; set; }
		[ForeignKey("ixStatus")]
		public virtual Statuses Statuses { get; set; }
    }
}
  

