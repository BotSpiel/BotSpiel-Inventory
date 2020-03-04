using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IOutboundCarrierManifestsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        OutboundCarrierManifestsPost GetPost(Int64 ixOutboundCarrierManifest);        
		OutboundCarrierManifests Get(Int64 ixOutboundCarrierManifest);
        IQueryable<OutboundCarrierManifests> Index();
        IQueryable<OutboundCarrierManifests> IndexDb();
       IQueryable<Statuses> selectStatuses();
        IQueryable<Facilities> selectFacilities();
        IQueryable<InventoryLocations> selectInventoryLocations();
        IQueryable<Carriers> selectCarriers();
       IQueryable<Statuses> StatusesDb();
        IQueryable<Facilities> FacilitiesDb();
        IQueryable<InventoryLocations> InventoryLocationsDb();
        IQueryable<Carriers> CarriersDb();
       List<KeyValuePair<Int64?, string>> selectInventoryLocationsNullable();
        bool VerifyOutboundCarrierManifestUnique(Int64 ixOutboundCarrierManifest, string sOutboundCarrierManifest);
        List<string> VerifyOutboundCarrierManifestDeleteOK(Int64 ixOutboundCarrierManifest, string sOutboundCarrierManifest);
        void RegisterCreate(OutboundCarrierManifestsPost outboundcarriermanifestsPost);
        void RegisterEdit(OutboundCarrierManifestsPost outboundcarriermanifestsPost);
        void RegisterDelete(OutboundCarrierManifestsPost outboundcarriermanifestsPost);
        void Rollback();
        void Commit();
    }
}
  

