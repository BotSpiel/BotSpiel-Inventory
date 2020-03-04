using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class FacilityAisleFacesPost : IFacilityAisleFacesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Facility Aisle Face ID")]
		public virtual Int64 ixFacilityAisleFace { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyFacilityAisleFace", controller: "FacilityAisleFaces", AdditionalFields = nameof(ixFacilityAisleFace))]
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
		public virtual String UserName { get; set; }
    }
}
  

