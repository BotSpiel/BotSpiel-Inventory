using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IFacilityWorkAreasRepository
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
        bool VerifyFacilityWorkAreaUnique(Int64 ixFacilityWorkArea, string sFacilityWorkArea);
        List<string> VerifyFacilityWorkAreaDeleteOK(Int64 ixFacilityWorkArea, string sFacilityWorkArea);
        void RegisterCreate(FacilityWorkAreasPost facilityworkareasPost);
        void RegisterEdit(FacilityWorkAreasPost facilityworkareasPost);
        void RegisterDelete(FacilityWorkAreasPost facilityworkareasPost);
        void Rollback();
        void Commit();
    }
}
  

