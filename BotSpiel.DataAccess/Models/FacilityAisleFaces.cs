using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class FacilityAisleFaces : IFacilityAisleFaces
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public FacilityAisleFaces()
        {
		Facilities _Facilities = new Facilities();
		Facilities = _Facilities;
		FacilityFloors _FacilityFloors = new FacilityFloors();
		FacilityFloors = _FacilityFloors;
		BaySequenceTypes _BaySequenceTypes = new BaySequenceTypes();
		BaySequenceTypes = _BaySequenceTypes;
		LogicalOrientations _LogicalOrientations = new LogicalOrientations();
		LogicalOrientations = _LogicalOrientations;
		AisleFaceStorageTypes _AisleFaceStorageTypes = new AisleFaceStorageTypes();
		AisleFaceStorageTypes = _AisleFaceStorageTypes;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffXOffsetUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffXOffsetUnit = _UnitsOfMeasurementFKDiffXOffsetUnit;
		UnitsOfMeasurement _UnitsOfMeasurementFKDiffYOffsetUnit = new UnitsOfMeasurement();
		UnitsOfMeasurementFKDiffYOffsetUnit = _UnitsOfMeasurementFKDiffYOffsetUnit;
		FacilityZones _FacilityZonesFKDiffDefaultFacilityZone = new FacilityZones();
		FacilityZonesFKDiffDefaultFacilityZone = _FacilityZonesFKDiffDefaultFacilityZone;
		LocationFunctions _LocationFunctionsFKDiffDefaultLocationFunction = new LocationFunctions();
		LocationFunctionsFKDiffDefaultLocationFunction = _LocationFunctionsFKDiffDefaultLocationFunction;
		InventoryLocationSizes _InventoryLocationSizesFKDiffDefaultInventoryLocationSize = new InventoryLocationSizes();
		InventoryLocationSizesFKDiffDefaultInventoryLocationSize = _InventoryLocationSizesFKDiffDefaultInventoryLocationSize;

        }
		[Display(Name = "Facility Aisle Face ID")]
		public virtual Int64 ixFacilityAisleFace { get; set; }
		[Display(Name = "Facility Aisle Face ID")]
		public virtual Int64 ixFacilityAisleFaceEdit { get; set; }
		[Required]
		[StringLength(300)]
		[Display(Name = "Facility Aisle Face")]
		public virtual String sFacilityAisleFace { get; set; }
		[Required]
		[Display(Name = "Facility ID")]
		public virtual Int64 ixFacility { get; set; }
		[Required]
		[Display(Name = "Facility Floor ID")]
		public virtual Int64 ixFacilityFloor { get; set; }
		[Required]
		[Display(Name = "Sequence")]
		public virtual Int64 nSequence { get; set; }
		[Required]
		[Display(Name = "Bay Sequence Type ID")]
		public virtual Int64 ixBaySequenceType { get; set; }
		[Display(Name = "Paired Aisle Face ID")]
		public virtual Int64? ixPairedAisleFace { get; set; }
		[Required]
		[Display(Name = "Logical Orientation ID")]
		public virtual Int64 ixLogicalOrientation { get; set; }
		[Required]
		[Display(Name = "Aisle Face Storage Type ID")]
		public virtual Int64 ixAisleFaceStorageType { get; set; }
		[Display(Name = "X Offset")]
		public virtual Double? nXOffset { get; set; }
		[Display(Name = "X Offset Unit ID")]
		public virtual Int64? ixXOffsetUnit { get; set; }
		[Display(Name = "Y Offset")]
		public virtual Double? nYOffset { get; set; }
		[Display(Name = "Y Offset Unit ID")]
		public virtual Int64? ixYOffsetUnit { get; set; }
		[Required]
		[Display(Name = "Levels")]
		public virtual Int32 nLevels { get; set; }
		[Display(Name = "Default Number Of Bays")]
		public virtual Int32? nDefaultNumberOfBays { get; set; }
		[Display(Name = "Default Number Of Slots In Bay")]
		public virtual Int32? nDefaultNumberOfSlotsInBay { get; set; }
		[Display(Name = "Default Facility Zone ID")]
		public virtual Int64? ixDefaultFacilityZone { get; set; }
		[Display(Name = "Default Location Function ID")]
		public virtual Int64? ixDefaultLocationFunction { get; set; }
		[Required]
		[Display(Name = "Default Inventory Location Size ID")]
		public virtual Int64 ixDefaultInventoryLocationSize { get; set; }
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
		[ForeignKey("ixFacility")]
		public virtual Facilities Facilities { get; set; }
		[ForeignKey("ixFacilityFloor")]
		public virtual FacilityFloors FacilityFloors { get; set; }
		[ForeignKey("ixBaySequenceType")]
		public virtual BaySequenceTypes BaySequenceTypes { get; set; }
		[ForeignKey("ixPairedAisleFace")]
		public virtual FacilityAisleFaces FacilityAisleFacesFKDiffPairedAisleFace { get; set; }
		[ForeignKey("ixLogicalOrientation")]
		public virtual LogicalOrientations LogicalOrientations { get; set; }
		[ForeignKey("ixAisleFaceStorageType")]
		public virtual AisleFaceStorageTypes AisleFaceStorageTypes { get; set; }
		[ForeignKey("ixXOffsetUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffXOffsetUnit { get; set; }
		[ForeignKey("ixYOffsetUnit")]
		public virtual UnitsOfMeasurement UnitsOfMeasurementFKDiffYOffsetUnit { get; set; }
		[ForeignKey("ixDefaultFacilityZone")]
		public virtual FacilityZones FacilityZonesFKDiffDefaultFacilityZone { get; set; }
		[ForeignKey("ixDefaultLocationFunction")]
		public virtual LocationFunctions LocationFunctionsFKDiffDefaultLocationFunction { get; set; }
		[ForeignKey("ixDefaultInventoryLocationSize")]
		public virtual InventoryLocationSizes InventoryLocationSizesFKDiffDefaultInventoryLocationSize { get; set; }
    }
}
  

