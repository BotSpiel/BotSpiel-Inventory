using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InventoryLocationSizesPost : IInventoryLocationSizesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Inventory Location Size ID")]
		public virtual Int64 ixInventoryLocationSize { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyInventoryLocationSize", controller: "InventoryLocationSizes", AdditionalFields = nameof(ixInventoryLocationSize))]
		[Display(Name = "Inventory Location Size")]
		public virtual String sInventoryLocationSize { get; set; }
		[Required]
		[Display(Name = "Length")]
		public virtual Double nLength { get; set; }
		[Required]
		[Display(Name = "Length Unit ID")]
		public virtual Int64 ixLengthUnit { get; set; }
		[Required]
		[Display(Name = "Width")]
		public virtual Double nWidth { get; set; }
		[Required]
		[Display(Name = "Width Unit ID")]
		public virtual Int64 ixWidthUnit { get; set; }
		[Required]
		[Display(Name = "Height")]
		public virtual Double nHeight { get; set; }
		[Required]
		[Display(Name = "Height Unit ID")]
		public virtual Int64 ixHeightUnit { get; set; }
		[Required]
		[Display(Name = "Usable Volume")]
		public virtual Double nUsableVolume { get; set; }
		[Required]
		[Display(Name = "Usable Volume Unit ID")]
		public virtual Int64 ixUsableVolumeUnit { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

