using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InventoryLocations : IInventoryLocations
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public InventoryLocations()
        {
		LocationFunctions _LocationFunctions = new LocationFunctions();
		LocationFunctions = _LocationFunctions;
		Companies _Companies = new Companies();
		Companies = _Companies;
		FacilityFloors _FacilityFloors = new FacilityFloors();
		FacilityFloors = _FacilityFloors;
		FacilityZones _FacilityZones = new FacilityZones();
		FacilityZones = _FacilityZones;
		FacilityWorkAreas _FacilityWorkAreas = new FacilityWorkAreas();
		FacilityWorkAreas = _FacilityWorkAreas;
		FacilityAisleFaces _FacilityAisleFaces = new FacilityAisleFaces();
		FacilityAisleFaces = _FacilityAisleFaces;
		InventoryLocationSizes _InventoryLocationSizes = new InventoryLocationSizes();
		InventoryLocationSizes = _InventoryLocationSizes;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffXOffsetUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffXOffsetUnit = _UnitsOfMeasurementFKDiffXOffsetUnit;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffYOffsetUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffYOffsetUnit = _UnitsOfMeasurementFKDiffYOffsetUnit;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffZOffsetUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffZOffsetUnit = _UnitsOfMeasurementFKDiffZOffsetUnit;

        }
		[Display(Name = "Inventory Location ID")]
		public virtual Int64 ixInventoryLocation { get; set; }
		[Display(Name = "Inventory Location ID")]
		public virtual Int64 ixInventoryLocationEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Inventory Location")]
		public virtual String sInventoryLocation { get; set; }
		[Required]
		[Display(Name = "Location Function ID")]
		public virtual Int64 ixLocationFunction { get; set; }
		[Display(Name = "Company ID")]
		public virtual Int64? ixCompany { get; set; }
		[Required]
		[Display(Name = "Facility Floor ID")]
		public virtual Int64 ixFacilityFloor { get; set; }
		[Required]
		[Display(Name = "Facility Zone ID")]
		public virtual Int64 ixFacilityZone { get; set; }
		[Required]
		[Display(Name = "Facility Work Area ID")]
		public virtual Int64 ixFacilityWorkArea { get; set; }
		[Required]
		[Display(Name = "Facility Aisle Face ID")]
		public virtual Int64 ixFacilityAisleFace { get; set; }
		[StringLength(100)]
		[Display(Name = "Level")]
		public virtual String sLevel { get; set; }
		[StringLength(100)]
		[Display(Name = "Bay")]
		public virtual String sBay { get; set; }
		[StringLength(100)]
		[Display(Name = "Slot")]
		public virtual String sSlot { get; set; }
		[Display(Name = "Inventory Location Size ID")]
		public virtual Int64? ixInventoryLocationSize { get; set; }
		[Required]
		[Display(Name = "Sequence")]
		public virtual Int64 nSequence { get; set; }
		[Display(Name = "X Offset")]
		public virtual Double? nXOffset { get; set; }
		[Display(Name = "X Offset Unit ID")]
		public virtual Int64? ixXOffsetUnit { get; set; }
		[Display(Name = "Y Offset")]
		public virtual Double? nYOffset { get; set; }
		[Display(Name = "Y Offset Unit ID")]
		public virtual Int64? ixYOffsetUnit { get; set; }
		[Display(Name = "Z Offset")]
		public virtual Double? nZOffset { get; set; }
		[Display(Name = "Z Offset Unit ID")]
		public virtual Int64? ixZOffsetUnit { get; set; }
		[StringLength(30)]
		[Display(Name = "Latitude")]
		public virtual String sLatitude { get; set; }
		[StringLength(30)]
		[Display(Name = "Longitude")]
		public virtual String sLongitude { get; set; }
		[Required]
		[Display(Name = "Track Utilisation")]
		public virtual Boolean bTrackUtilisation { get; set; }
		[Display(Name = "Utilisation Percent")]
		public virtual Double? nUtilisationPercent { get; set; }
		[Display(Name = "Queued Utilisation Percent")]
		public virtual Double? nQueuedUtilisationPercent { get; set; }
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
		[ForeignKey("ixLocationFunction")]
		public virtual LocationFunctions LocationFunctions { get; set; }
		[ForeignKey("ixCompany")]
		public virtual Companies Companies { get; set; }
		[ForeignKey("ixFacilityFloor")]
		public virtual FacilityFloors FacilityFloors { get; set; }
		[ForeignKey("ixFacilityZone")]
		public virtual FacilityZones FacilityZones { get; set; }
		[ForeignKey("ixFacilityWorkArea")]
		public virtual FacilityWorkAreas FacilityWorkAreas { get; set; }
		[ForeignKey("ixFacilityAisleFace")]
		public virtual FacilityAisleFaces FacilityAisleFaces { get; set; }
		[ForeignKey("ixInventoryLocationSize")]
		public virtual InventoryLocationSizes InventoryLocationSizes { get; set; }
		[ForeignKey("ixXOffsetUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffXOffsetUnit { get; set; }
		[ForeignKey("ixYOffsetUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffYOffsetUnit { get; set; }
		[ForeignKey("ixZOffsetUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffZOffsetUnit { get; set; }
    }
}
  

