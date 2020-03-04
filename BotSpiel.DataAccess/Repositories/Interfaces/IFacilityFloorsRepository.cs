using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IFacilityFloorsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        FacilityFloorsPost GetPost(Int64 ixFacilityFloor);        
		FacilityFloors Get(Int64 ixFacilityFloor);
        IQueryable<FacilityFloors> Index();
        IQueryable<FacilityFloors> IndexDb();
        bool VerifyFacilityFloorUnique(Int64 ixFacilityFloor, string sFacilityFloor);
        List<string> VerifyFacilityFloorDeleteOK(Int64 ixFacilityFloor, string sFacilityFloor);
        void RegisterCreate(FacilityFloorsPost facilityfloorsPost);
        void RegisterEdit(FacilityFloorsPost facilityfloorsPost);
        void RegisterDelete(FacilityFloorsPost facilityfloorsPost);
        void Rollback();
        void Commit();
    }
}
  

