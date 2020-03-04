using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IOutboundCarrierManifestPickupsRepository
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
        void RegisterCreate(OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost);
        void RegisterEdit(OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost);
        void RegisterDelete(OutboundCarrierManifestPickupsPost outboundcarriermanifestpickupsPost);
        void Rollback();
        void Commit();
    }
}
  

