using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IFacilityWorkAreasService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        FacilityWorkAreasPost GetPost(Int64 ixFacilityWorkArea);        
		FacilityWorkAreas Get(Int64 ixFacilityWorkArea);
        IQueryable<FacilityWorkAreas> Index();
        IQueryable<FacilityWorkAreas> IndexDb();
        bool VerifyFacilityWorkAreaUnique(Int64 ixFacilityWorkArea, string sFacilityWorkArea);
        List<string> VerifyFacilityWorkAreaDeleteOK(Int64 ixFacilityWorkArea, string sFacilityWorkArea);

        Task<Int64> Create(FacilityWorkAreasPost facilityworkareasPost);
        Task Edit(FacilityWorkAreasPost facilityworkareasPost);
        Task Delete(FacilityWorkAreasPost facilityworkareasPost);
    }
}
  

