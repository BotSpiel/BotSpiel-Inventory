using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
//Custom Code Start | Added Code Block 
using FluentValidation;
using BotSpiel.DataAccess.Repositories;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Utilities;
//Custom Code End

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class OutboundCarrierManifestPickupsPost : IOutboundCarrierManifestPickupsPost
    {

        /*
        -- =============================================
        -- Author:		<BotSpiel>

        -- Description:	<Description>

        This class ....

        */
        //Custom Code Start | Added Code Block
        public OutboundCarrierManifestPickupsPost()
        {
            ixStatus = 19;
        }
        //Custom Code End


        [Display(Name = "Outbound Carrier Manifest Pickup ID")]
		public virtual Int64 ixOutboundCarrierManifestPickup { get; set; }
		[Display(Name = "Outbound Carrier Manifest Pickup")]
		public virtual String sOutboundCarrierManifestPickup { get; set; }
		[Required]
		[Display(Name = "Outbound Carrier Manifest ID")]
		public virtual Int64 ixOutboundCarrierManifest { get; set; }
		[Required]
		[Display(Name = "Status ID")]
		public virtual Int64 ixStatus { get; set; }
		public virtual String UserName { get; set; }
    }

    //Custom Code Start | Added Code Block 
    public class OutboundCarrierManifestPickupsPostValidator : AbstractValidator<OutboundCarrierManifestPickupsPost>
    {
        private readonly IOutboundShipmentsRepository _outboundshipmentsRepository;
        private readonly IOutboundOrdersRepository _outboundordersRepository;
        private readonly CommonLookUpsRepository _commonLookUps;
        private readonly IOutboundOrderLinesRepository _outboundorderlinesRepository;
        private readonly IOutboundOrderLinesInventoryAllocationRepository _outboundorderlinesinventoryallocationRepository;
        private readonly IOutboundOrderLinePackingRepository _outboundorderlinepackingRepository;

        public OutboundCarrierManifestPickupsPostValidator(
                IOutboundShipmentsRepository outboundshipmentsRepository
                , IOutboundOrdersRepository outboundordersRepository
                , CommonLookUpsRepository commonLookUps
                , IOutboundOrderLinesRepository outboundorderlinesRepository
                , IOutboundOrderLinesInventoryAllocationRepository outboundorderlinesinventoryallocationRepository
            )
        {
            //We have to check that all the outbound shipments are completely picked (Allocated qtys)
            _outboundshipmentsRepository = outboundshipmentsRepository;
            _outboundordersRepository = outboundordersRepository;
            _commonLookUps = commonLookUps;
            _outboundorderlinesRepository = outboundorderlinesRepository;
            _outboundorderlinesinventoryallocationRepository = outboundorderlinesinventoryallocationRepository;

            var ixStatusActive = _commonLookUps.getStatuses().Where(s => s.sStatus == "Active").Select(s => s.ixStatus).FirstOrDefault();

            RuleFor(x => x.ixOutboundCarrierManifest)
                .Custom((ixOutboundCarrierManifest, context) =>
                {
                    var outboundshipments = _outboundshipmentsRepository.IndexDb().Where(sh => sh.ixOutboundCarrierManifest == ixOutboundCarrierManifest && sh.ixStatus == ixStatusActive)
                    .Select(sh => sh.ixOutboundShipment).ToList();

                    var outboundorders = _outboundordersRepository.IndexDb().Where(o => o.ixStatus == ixStatusActive)
                    .Join(outboundshipments, o => o.ixOutboundShipment, sh => sh, (o, sh) => new { O = o, Sh = sh })
                    .Select(x => x.O.ixOutboundOrder).ToList();

                    var outboundorderlines = _outboundorderlinesRepository.IndexDb()
                        .Join(outboundorders, ol => ol.ixOutboundOrder, o => o, (ol, o) => new { Ol = ol, O = o })
                        .Select(x => x.Ol.ixOutboundOrderLine).ToList();

                    var outboundorderlinesNotCompletelyPicked = _outboundorderlinesinventoryallocationRepository.IndexDb()
                        .Join(outboundorderlines, ola => ola.ixOutboundOrderLine, ol => ol, (ola, ol) => new { Ola = ola, Ol = ol })
                        .Where(x => x.Ola.nBaseUnitQuantityPicked < x.Ola.nBaseUnitQuantityAllocated).Select(x => x.Ola.ixOutboundOrderLine).ToList();

                    if (outboundorderlinesNotCompletelyPicked.Count() > 0)
                    {
                        string errorList = "";

                        outboundorderlinesNotCompletelyPicked
                            .ToList()
                            .ForEach(e => errorList += e.ToString() + ", ");

                        errorList = errorList.TrimEnd(' ');
                        errorList = errorList.TrimEnd(',');


                       context.AddFailure($"The following outbound order lines have not been completely picked: {errorList}. Please complete the picking first.");

                    }

                }
                );

        }

    }
    //Custom Code End
}


