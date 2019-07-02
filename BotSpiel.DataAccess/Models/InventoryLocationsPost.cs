using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//Custom Code Start | Added Code Block 
using FluentValidation;
using BotSpiel.DataAccess.Repositories;
using System.Collections.Generic;
using System.Linq;
//Custom Code End

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InventoryLocationsPost : IInventoryLocationsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Inventory Location ID")]
		public virtual Int64 ixInventoryLocation { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyInventoryLocation", controller: "InventoryLocations", AdditionalFields = nameof(ixInventoryLocation))]
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
		public virtual String UserName { get; set; }
    }

    //Custom Code Start | Added Code Block 
    public class InventoryLocationsPostValidator : AbstractValidator<InventoryLocationsPost>
    {

        public InventoryLocationsPostValidator()
        {


            When(location => location.bTrackUtilisation == true, () =>
            {
                RuleFor(x => x.ixInventoryLocationSize)
                .NotEmpty()
                .WithMessage("When the utilisation is tracked, a location size must be specified. ");
            }
            );
        }

    }
    //Custom Code End

}


