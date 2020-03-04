using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IFacilityZonesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        FacilityZonesPost GetPost(Int64 ixFacilityZone);        
		FacilityZones Get(Int64 ixFacilityZone);
        IQueryable<FacilityZones> Index();
        IQueryable<FacilityZones> IndexDb();
        bool VerifyFacilityZoneUnique(Int64 ixFacilityZone, string sFacilityZone);
        List<string> VerifyFacilityZoneDeleteOK(Int64 ixFacilityZone, string sFacilityZone);
        void RegisterCreate(FacilityZonesPost facilityzonesPost);
        void RegisterEdit(FacilityZonesPost facilityzonesPost);
        void RegisterDelete(FacilityZonesPost facilityzonesPost);
        void Rollback();
        void Commit();
    }
}
  

