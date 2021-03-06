using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IOutboundCarrierManifestsService
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

        Task<Int64> Create(OutboundCarrierManifestsPost outboundcarriermanifestsPost);
        Task Edit(OutboundCarrierManifestsPost outboundcarriermanifestsPost);
        Task Delete(OutboundCarrierManifestsPost outboundcarriermanifestsPost);
    }
}
  

