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
    public class ReceivingPost : IReceivingPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

        */
        //Custom Code Start | Added Code Block
        public ReceivingPost()
        {
            ixStatus = 19;
        }
        //Custom Code End

        [Display(Name = "Receipt ID")]
		public virtual Int64 ixReceipt { get; set; }
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
		[Display(Name = "Serial Number")]
		public virtual String sSerialNumber { get; set; }
		[StringLength(100)]
		[Display(Name = "Batch Number")]
		public virtual String sBatchNumber { get; set; }
		[Display(Name = "Expire At")]
		public virtual DateTime? dtExpireAt { get; set; }
		[Required]
		[Display(Name = "Base Unit Quantity Received")]
		public virtual Double nBaseUnitQuantityReceived { get; set; }
        [Required]
        [Display(Name = "Inventory State ID")]
        public virtual Int64 ixInventoryState { get; set; }
        [Required]
        [Display(Name = "Status ID")]
        public virtual Int64 ixStatus { get; set; }
        public virtual String UserName { get; set; }
    }

    //Custom Code Start | Added Code Block 
    public class ReceivingPostValidator : AbstractValidator<ReceivingPost>
    {
        private readonly IHandlingUnitsRepository _handlingunitsRepository;
        private readonly IInventoryUnitsRepository _inventoryunitsRepository;
        private readonly IMaterialsRepository _materialsRepository;
        private readonly IInventoryLocationsRepository _inventorylocationsRepository;
        private readonly IReceivingRepository _receivingRepository;
        private readonly VolumeAndWeight _volumeAndWeight;
        private readonly IInboundOrderLinesRepository _inboundorderlinesRepository;

        public ReceivingPostValidator(IReceivingRepository receivingRepository, VolumeAndWeight volumeAndWeight, IHandlingUnitsRepository handlingunitsRepository, IInventoryUnitsRepository inventoryunitsRepository, IMaterialsRepository materialsRepository, IInventoryLocationsRepository inventorylocationsRepository, IInboundOrderLinesRepository inboundorderlinesRepository)
        {
            _receivingRepository = receivingRepository;
            _handlingunitsRepository = handlingunitsRepository;
            _inventoryunitsRepository = inventoryunitsRepository;
            _materialsRepository = materialsRepository;
            _inventorylocationsRepository = inventorylocationsRepository;
            _volumeAndWeight = volumeAndWeight;
            _inboundorderlinesRepository = inboundorderlinesRepository;

            //InventoryUnitsPost _inventoryUnit = new InventoryUnitsPost();

            RuleFor(rec => rec.sReceipt)
                .NotEmpty()
                .WithMessage("Please enter a handling unit identifier.");

            When(rec => rec.sReceipt.Trim() != "" && !_handlingunitsRepository.IndexDb().Where(x => x.sHandlingUnit == rec.sReceipt.Trim()).Any(), () =>
            {
                RuleFor(u => u.ixHandlingUnitType)
                .NotEqual(0)
                .NotEmpty()
                .WithMessage("This is a new handling unit, please select a handling unit type.");
            }
            );

            When(rec => (rec.ixMaterialHandlingUnitConfiguration ?? 0) > 0, () =>
            {
                RuleFor(u => u.nHandlingUnitQuantity)
                .NotEmpty()
                .WithMessage("When using a material handling unit configuration, you must enter a Handling Unit Quantity greater than 0.");
            }
            );

            //When(rec => rec.sSerialNumber.Trim() != "", () =>
            //{
            //    RuleFor(u => u.nBaseUnitQuantityReceived)
            //    .Equal(1)
            //    .WithMessage("When entering a serial number, the Base Unit Quantity Received must be 1.");
            //}
            //);

            RuleFor(u => u.nBaseUnitQuantityReceived)
                .GreaterThanOrEqualTo(0)
                .WithMessage("The Base Unit Quantity Received must be a positive number.");

            When(rec => _materialsRepository.GetPost(rec.ixMaterial).bTrackSerialNumber, () =>
            {
                RuleFor(iu => iu.sSerialNumber)
                .NotEmpty()
                .WithMessage("The material is serial tracked. Please enter a serial number.");
            }
            );

            When(rec => _materialsRepository.GetPost(rec.ixMaterial).bTrackSerialNumber, () =>
            {
                RuleFor(iu => iu.nBaseUnitQuantityReceived)
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

            // When(rec => !_volumeAndWeight.inventoryUnitWillFitLocation(new InventoryUnitsPost
            //     { ixFacility = _receivingRepository.InboundOrdersDb().Where(x => x.ixInboundOrder == rec.ixInboundOrder).Select(x => x.ixFacility).FirstOrDefault(),
            //       ixCompany = _receivingRepository.InboundOrdersDb().Where(x => x.ixInboundOrder == rec.ixInboundOrder).Select(x => x.ixCompany).FirstOrDefault(),
            //       ixMaterial = rec.ixMaterial,
            //       ixInventoryState = _inventoryunitsRepository.InventoryStatesDb().Where(x => x.sInventoryState == "")
            // }, _inventorylocationsRepository.GetPost(rec.ixInventoryLocation)), () =>
            //{
            //    RuleFor(u => u.ixInventoryLocation)
            //    .Empty()
            //    .WithMessage("The inventory will not fit into the selected location.");
            //}
            // );

            When(rec => _inboundorderlinesRepository.IndexDb().Where(x => x.ixInboundOrder == rec.ixInboundOrder && x.ixMaterial == rec.ixMaterial).Select(x => x.nBaseUnitQuantityExpected - x.nBaseUnitQuantityReceived).Sum() < rec.nBaseUnitQuantityReceived, () =>
            {
                RuleFor(r => r.nBaseUnitQuantityReceived)
                .LessThan(0)
                .WithMessage("The Base Unit Quantity Received exceeds the open quantity on the inbound order.");
            }
            );


        }

    }
    //Custom Code End

}
  

