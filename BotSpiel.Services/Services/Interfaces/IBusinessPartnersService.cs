using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IBusinessPartnersService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        BusinessPartnersPost GetPost(Int64 ixBusinessPartner);        
		BusinessPartners Get(Int64 ixBusinessPartner);
        IQueryable<BusinessPartners> Index();
       IQueryable<Addresses> selectAddresses();
        IQueryable<Companies> selectCompanies();
        IQueryable<BusinessPartnerTypes> selectBusinessPartnerTypes();
        bool VerifyBusinessPartnerUnique(Int64 ixBusinessPartner, string sBusinessPartner);
        List<string> VerifyBusinessPartnerDeleteOK(Int64 ixBusinessPartner, string sBusinessPartner);

        Task<Int64> Create(BusinessPartnersPost businesspartnersPost);
        Task Edit(BusinessPartnersPost businesspartnersPost);
        Task Delete(BusinessPartnersPost businesspartnersPost);
    }
}
  

