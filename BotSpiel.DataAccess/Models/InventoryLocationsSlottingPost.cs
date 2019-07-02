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
    public class InventoryLocationsSlottingPost : IInventoryLocationsSlottingPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Inventory Location Slotting ID")]
		public virtual Int64 ixInventoryLocationSlotting { get; set; }
		[Display(Name = "Inventory Location Slotting")]
		public virtual String sInventoryLocationSlotting { get; set; }
		[Required]
		[Display(Name = "Inventory Location ID")]
		public virtual Int64 ixInventoryLocation { get; set; }
		[Required]
		[Display(Name = "Material ID")]
		public virtual Int64 ixMaterial { get; set; }
		[Required]
		[Display(Name = "Minimum Base Unit Quantity")]
		public virtual Double nMinimumBaseUnitQuantity { get; set; }
		[Required]
		[Display(Name = "Maximum Base Unit Quantity")]
		public virtual Double nMaximumBaseUnitQuantity { get; set; }
		public virtual String UserName { get; set; }
    }
    //Custom Code Start | Added Code Block 
    public class InventoryLocationsSlottingPostValidator : AbstractValidator<InventoryLocationsSlottingPost>
    {

        public InventoryLocationsSlottingPostValidator()
        {
            RuleFor(x => x.nMinimumBaseUnitQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The Minimum Base Unit Quantity must be greater than or equal to 0.");

            RuleFor(x => x.nMaximumBaseUnitQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The Maximum Base Unit Quantity must be greater than or equal to 0.");

            RuleFor(x => x.nMaximumBaseUnitQuantity)
                .GreaterThan(x => x.nMinimumBaseUnitQuantity)
                .WithMessage("The Maximum Base Unit Quantity must be greater than the Minimum Base Unit Quantity.");

        }

    }
    //Custom Code End
}
  
