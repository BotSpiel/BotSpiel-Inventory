using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IFacilityFloorsService
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
        bool VerifyFacilityFloorUnique(Int64 ixFacilityFloor, string sFacilityFloor);
        List<string> VerifyFacilityFloorDeleteOK(Int64 ixFacilityFloor, string sFacilityFloor);

        Task<Int64> Create(FacilityFloorsPost facilityfloorsPost);
        Task Edit(FacilityFloorsPost facilityfloorsPost);
        Task Delete(FacilityFloorsPost facilityfloorsPost);
    }
}
  

