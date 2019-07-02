using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IFacilitiesRepository
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
        void RegisterCreate(FacilitiesPost facilitiesPost);
        void RegisterEdit(FacilitiesPost facilitiesPost);
        void RegisterDelete(FacilitiesPost facilitiesPost);
        void Rollback();
        void Commit();
    }
}
  

