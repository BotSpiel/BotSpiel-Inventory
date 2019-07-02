using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class HandlingUnitsPost : IHandlingUnitsPost
    {

        /*
        -- =============================================
        -- Author:		<BotSpiel>

        -- Description:	<Description>

        This class ....

        */

        //Custom Code Start | Added Code Block
        public HandlingUnitsPost()
        {
            ixStatus = 5;
        }
        //Custom Code End


        [Display(Name = "Handling Unit ID")]
		public virtual Int64 ixHandlingUnit { get; set; }
		[Required]
		[StringLength(300)]
		[Remote(action: "VerifyHandlingUnit", controller: "HandlingUnits", AdditionalFields = nameof(ixHandlingUnit))]
		[Display(Name = "Handling Unit")]
		public virtual String sHandlingUnit { get; set; }
		[Required]
		[Display(Name = "Handling Unit Type ID")]
		public virtual Int64 ixHandlingUnitType { get; set; }
		[Display(Name = "Parent Handling Unit ID")]
		public virtual Int64? ixParentHandlingUnit { get; set; }
		[Display(Name = "Packing Material ID")]
		public virtual Int64? ixPackingMaterial { get; set; }
		[Display(Name = "Material Handling Unit Configuration ID")]
		public virtual Int64? ixMaterialHandlingUnitConfiguration { get; set; }
		[Display(Name = "Length")]
		public virtual Double? nLength { get; set; }
		[Display(Name = "Length Unit ID")]
		public virtual Int64? ixLengthUnit { get; set; }
		[Display(Name = "Width")]
		public virtual Double? nWidth { get; set; }
		[Display(Name = "Width Unit ID")]
		public virtual Int64? ixWidthUnit { get; set; }
		[Display(Name = "Height")]
		public virtual Double? nHeight { get; set; }
		[Display(Name = "Height Unit ID")]
		public virtual Int64? ixHeightUnit { get; set; }
		[Display(Name = "Weight")]
		public virtual Double? nWeight { get; set; }
		[Display(Name = "Weight Unit ID")]
		public virtual Int64? ixWeightUnit { get; set; }
		[Display(Name = "Status ID")]
		public virtual Int64? ixStatus { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

