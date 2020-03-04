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
    public class InboundOrderLinesPost : IInboundOrderLinesPost
    {

        /*
        -- =============================================
        -- Author:		<BotSpiel>

        -- Description:	<Description>

        This class ....

        */
        //Custom Code Start | Added Code Block
        public InboundOrderLinesPost()
        {
            ixStatus = 5;
            nBaseUnitQuantityReceived = 0;
        }
        //Custom Code End


        [Display(Name = "Inbound Order Line ID")]
		public virtual Int64 ixInboundOrderLine { get; set; }
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
		public virtual String UserName { get; set; }
    }

    //Custom Code Start | Added Code Block 
    public class InboundOrderLinesPostValidator : AbstractValidator<InboundOrderLinesPost>
    {
        private readonly IHandlingUnitsRepository _handlingunitsRepository;
        private readonly IInventoryUnitsRepository _inventoryunitsRepository;
        private readonly IMaterialsRepository _materialsRepository;
        private readonly IInventoryLocationsRepository _inventorylocationsRepository;
        private readonly IReceivingRepository _receivingRepository;
        private readonly VolumeAndWeight _volumeAndWeight;
        public InboundOrderLinesPostValidator(IReceivingRepository receivingRepository, VolumeAndWeight volumeAndWeight, IHandlingUnitsRepository handlingunitsRepository, IInventoryUnitsRepository inventoryunitsRepository, IMaterialsRepository materialsRepository, IInventoryLocationsRepository inventorylocationsRepository)
        {
            _receivingRepository = receivingRepository;
            _handlingunitsRepository = handlingunitsRepository;
            _inventoryunitsRepository = inventoryunitsRepository;
            _materialsRepository = materialsRepository;
            _inventorylocationsRepository = inventorylocationsRepository;
            _volumeAndWeight = volumeAndWeight;

            //InventoryUnitsPost _inventoryUnit = new InventoryUnitsPost();

            When(rec => (rec.ixMaterialHandlingUnitConfiguration ?? 0) > 0, () =>
            {
                RuleFor(u => u.nHandlingUnitQuantity)
                .NotEmpty()
                .WithMessage("When using a material handling unit configuration, you must enter a Handling Unit Quantity greater than 0.");
            }
            );

            //When(rec => rec.sSerialNumber.Trim() != "", () =>
            //{
            //    RuleFor(u => u.nBaseUnitQuantityExpected)
            //    .Equal(1)
            //    .WithMessage("When entering a serial number, the Base Unit Quantity Expected must be 1.");
            //}
            //);

            RuleFor(u => u.nBaseUnitQuantityExpected)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The Base Unit Quantity Expected must be a positive number.");

            When(rec => _materialsRepository.GetPost(rec.ixMaterial).bTrackSerialNumber, () =>
            {
                RuleFor(iu => iu.sSerialNumber)
                .NotEmpty()
                .WithMessage("The material is serial tracked. Please enter a serial number.");
            }
            );

            When(rec => _materialsRepository.GetPost(rec.ixMaterial).bTrackSerialNumber, () =>
            {
                RuleFor(iu => iu.nBaseUnitQuantityExpected)
                .LessThanOrEqualTo(1)
                .WithMessage("The material is serial tracked. The base unit quantity must be 0 or 1");
            }
            );

            When(rec => _materialsRepository.GetPost(rec.ixMaterial).bTrackBatchNumber, () =>
            {
                RuleFor(iu => iu.sBatchNumber)
                .NotEmpty()
                .WithMessage("The material is batch tracked. Please enter a batch.");
            }
            );

            When(rec => _materialsRepository.GetPost(rec.ixMaterial).bTrackExpiry, () =>
            {
                RuleFor(iu => iu.dtExpireAt)
                .NotEmpty()
                .WithMessage("The material is expiry tracked. Please enter an expiry date.");
            }
            );

        }

    }
    //Custom Code End


}


