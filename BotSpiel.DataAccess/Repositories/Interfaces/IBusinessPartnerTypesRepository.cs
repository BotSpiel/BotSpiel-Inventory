using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IBusinessPartnerTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        BusinessPartnerTypesPost GetPost(Int64 ixBusinessPartnerType);        
		BusinessPartnerTypes Get(Int64 ixBusinessPartnerType);
        IQueryable<BusinessPartnerTypes> Index();
        bool VerifyBusinessPartnerTypeUnique(Int64 ixBusinessPartnerType, string sBusinessPartnerType);
        List<string> VerifyBusinessPartnerTypeDeleteOK(Int64 ixBusinessPartnerType, string sBusinessPartnerType);
        void RegisterCreate(BusinessPartnerTypesPost businesspartnertypesPost);
        void RegisterEdit(BusinessPartnerTypesPost businesspartnertypesPost);
        void RegisterDelete(BusinessPartnerTypesPost businesspartnertypesPost);
        void Rollback();
        void Commit();
    }
}
  

