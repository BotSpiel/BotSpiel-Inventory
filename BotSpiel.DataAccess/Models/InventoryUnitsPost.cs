using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//Custom Code Start | Added Code Block 
using BotSpiel.DataAccess.Utilities;
using FluentValidation;
using BotSpiel.DataAccess.Repositories;
using System.Collections.Generic;
using System.Linq;
//Custom Code End

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class InventoryUnitsPost : IInventoryUnitsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        //Custom Code Start | Added Code Block
        public InventoryUnitsPost()
        {
            ixStatus = 5;
        }
        //Custom Code End
 
		[Display(Name = "Inventory Unit ID")]
		public virtual Int64 ixInventoryUnit { get; set; }
		[Display(Name = "Inventory Unit")]
		public virtual String sInventoryUnit { get; set; }
		[Required]
		[Display(Name = "Facility ID")]
		public virtual Int64 ixFacility { get; set; }
		[Required]
		[Display(Name = "Company ID")]
		public virtual Int64 ixCompany { get; set; }
		[Required]
		[Display(Name = "Material ID")]
		public virtual Int64 ixMaterial { get; set; }
		[Required]
		[Display(Name = "Inventory State ID")]
		public virtual Int64 ixInventoryState { get; set; }
		[Display(Name = "Handling Unit ID")]
		public virtual Int64? ixHandlingUnit { get; set; }
		[Required]
		[Display(Name = "Inventory Location ID")]
		public virtual Int64 ixInventoryLocation { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity")]
		public virtual Double nBaseUnitQuantity { get; set; }
		[StringLength(100)]
		[Display(Name = "Serial Number")]
		public virtual String sSerialNumber { get; set; }
		[StringLength(100)]
		[Display(Name = "Batch Number")]
		public virtual String sBatchNumber { get; set; }
		[Display(Name = "Expire At")]
		public virtual DateTime? dtExpireAt { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity Queued")]
		public virtual Double nBaseUnitQuantityQueued { get; set; }
		[Required]
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatus { get; set; }
		public virtual String UserName { get; set; }
    }

    //Custom Code Start | Added Code Block 
    public class InventoryUnitsPostValidator : AbstractValidator<InventoryUnitsPost>
    {
        private readonly VolumeAndWeight _volumeAndWeight;
        private readonly IInventoryLocationsRepository _inventorylocationsRepository;
        private readonly IMaterialsRepository _materialsRepository;
        public InventoryUnitsPostValidator(VolumeAndWeight volumeAndWeight, IInventoryLocationsRepository inventorylocationsRepository, IMaterialsRepository materialsRepository)
        {
            _volumeAndWeight = volumeAndWeight;
            _inventorylocationsRepository = inventorylocationsRepository;
            _materialsRepository = materialsRepository;

            RuleFor(u => u.nBaseUnitQuantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The base unit quantity must be a positive number.");

            When(iu => _materialsRepository.GetPost(iu.ixMaterial).bTrackSerialNumber, () =>
            {
                RuleFor(iu => iu.sSerialNumber)
                .NotEmpty()
                .WithMessage("The material is serial tracked. Please enter a serial number.");
            }
            );

            When(iu => _materialsRepository.GetPost(iu.ixMaterial).bTrackSerialNumber, () =>
            {
                RuleFor(iu => iu.nBaseUnitQuantity)
                .LessThanOrEqualTo(1)
                .WithMessage("The material is serial tracked. The base unit quantity must be 0 or 1");
            }
            );

            When(iu => _materialsRepository.GetPost(iu.ixMaterial).bTrackBatchNumber, () =>
            {
                RuleFor(iu => iu.sBatchNumber)
                .NotEmpty()
                .WithMessage("The material is batch tracked. Please enter a batch.");
            }
            );

            When(iu => _materialsRepository.GetPost(iu.ixMaterial).bTrackExpiry, () =>
            {
                RuleFor(iu => iu.dtExpireAt)
                .NotEmpty()
                .WithMessage("The material is expiry tracked. Please enter an expiry date.");
            }
            );


            When(inventoryUnit => !_volumeAndWeight.inventoryUnitWillFitLocation(inventoryUnit, _inventorylocationsRepository.GetPost(inventoryUnit.ixInventoryLocation)), () =>
            {
                RuleFor(u => u.ixInventoryLocation)
                .Empty()
                .WithMessage("The inventory will not fit into the selected location.");
            }
            );

        }

    }
    //Custom Code End


}
  

