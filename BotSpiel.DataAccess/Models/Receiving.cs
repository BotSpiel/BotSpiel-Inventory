using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class Receiving : IReceiving
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public Receiving()
        {
		InventoryLocations _InventoryLocations = new InventoryLocations();
		InventoryLocations = _InventoryLocations;
		InboundOrders _InboundOrders = new InboundOrders();
		InboundOrders = _InboundOrders;
		HandlingUnits _HandlingUnits = new HandlingUnits();
		HandlingUnits = _HandlingUnits;
		Materials _Materials = new Materials();
		Materials = _Materials;
		MaterialHandlingUnitConfigurations _MaterialHandlingUnitConfigurations = new MaterialHandlingUnitConfigurations();
		MaterialHandlingUnitConfigurations = _MaterialHandlingUnitConfigurations;
		HandlingUnitTypes _HandlingUnitTypes = new HandlingUnitTypes();
		HandlingUnitTypes = _HandlingUnitTypes;
		Statuses _Statuses = new Statuses();
		Statuses = _Statuses;

        }
		[Display(Name = "Receipt ID")]
		public virtual Int64 ixReceipt { get; set; }
		[Display(Name = "Receipt ID")]
		public virtual Int64 ixReceiptEdit { get; set; }
		[Display(Name = "Receipt")]
		public virtual String sReceipt { get; set; }
		[Required]
		[Display(Name = "Inventory Location ID")]
		public virtual Int64 ixInventoryLocation { get; set; }
		[Required]
		[Display(Name = "Inbound Order ID")]
		public virtual Int64 ixInboundOrder { get; set; }
		[Required]
		[Display(Name = "Handling Unit ID")]
		public virtual Int64 ixHandlingUnit { get; set; }
		[Required]
		[Display(Name = "Material ID")]
		public virtual Int64 ixMaterial { get; set; }
		[Display(Name = "Material Handling Unit Configuration ID")]
		public virtual Int64? ixMaterialHandlingUnitConfiguration { get; set; }
		[Display(Name = "Handling Unit Type ID")]
		public virtual Int64? ixHandlingUnitType { get; set; }
		[Display(Name = "Handling Unit Quantity")]
		public virtual Double? nHandlingUnitQuantity { get; set; }
		[StringLength(100)]
		[Display(Name = "Batch Number")]
		public virtual String sBatchNumber { get; set; }
		[StringLength(100)]
		[Display(Name = "Serial Number")]
		public virtual String sSerialNumber { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity Received")]
		public virtual Double nBaseUnitQuantityReceived { get; set; }
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
		[ForeignKey("ixInventoryLocation")]
		public virtual InventoryLocations InventoryLocations { get; set; }
		[ForeignKey("ixInboundOrder")]
		public virtual InboundOrders InboundOrders { get; set; }
		[ForeignKey("ixHandlingUnit")]
		public virtual HandlingUnits HandlingUnits { get; set; }
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
  

