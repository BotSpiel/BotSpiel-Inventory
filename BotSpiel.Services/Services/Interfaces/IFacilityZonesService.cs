using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IFacilityZonesService
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

        Task<Int64> Create(FacilityZonesPost facilityzonesPost);
        Task Edit(FacilityZonesPost facilityzonesPost);
        Task Delete(FacilityZonesPost facilityzonesPost);
    }
}
  

