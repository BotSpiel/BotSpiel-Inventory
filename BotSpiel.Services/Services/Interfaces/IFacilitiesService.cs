using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IFacilitiesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        FacilitiesPost GetPost(Int64 ixFacility);        
		Facilities Get(Int64 ixFacility);
        IQueryable<Facilities> Index();
       IQueryable<Addresses> selectAddresses();
        bool VerifyFacilityUnique(Int64 ixFacility, string sFacility);
        List<string> VerifyFacilityDeleteOK(Int64 ixFacility, string sFacility);

        Task<Int64> Create(FacilitiesPost facilitiesPost);
        Task Edit(FacilitiesPost facilitiesPost);
        Task Delete(FacilitiesPost facilitiesPost);
    }
}
  

