using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IOutboundCarrierManifestPickupsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        OutboundCarrierManifestPickupsPost GetPost(Int64 ixOutboundCarrierManifestPickup);        
		OutboundCarrierManifestPickups Get(Int64 ixOutboundCarrierManifestPickup);
        IQueryable<OutboundCarrierManifestPickups> Index();
        IQueryable<OutboundCarrierManifestPickups> IndexDb();
       IQueryable<Statuses> selectStatuses();
        IQueryable<OutboundCarrierManifests> selectOutboundCarrierManifests();
       IQueryable<Statuses> StatusesDb();
        IQueryable<OutboundCarrierManifests> OutboundCarrierManifestsDb();
        bool VerifyOutboundCarrierManifestPickupUnique(Int64 ixOutboundCarrierManifestPickup, string sOutboundCarrierManifestPickup);
        List<string> VerifyOutboundCarrierManifestPickupDeleteOK(Int64 ixOutboundCarrierManifestPickup, string sOutboundCarrierManifestPickup);

        Task<Int64> Create(OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost);
        Task Edit(OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost);
        Task Delete(OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost);
    }
}
  

