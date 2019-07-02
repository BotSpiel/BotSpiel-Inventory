using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IBusinessPartnerTypesService
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

        Task<Int64> Create(BusinessPartnerTypesPost businesspartnertypesPost);
        Task Edit(BusinessPartnerTypesPost businesspartnertypesPost);
        Task Delete(BusinessPartnerTypesPost businesspartnertypesPost);
    }
}
  

