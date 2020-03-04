using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IBusinessPartnersRepository
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
        IQueryable<BusinessPartners> IndexDb();
       IQueryable<Addresses> selectAddresses();
        IQueryable<Companies> selectCompanies();
        IQueryable<BusinessPartnerTypes> selectBusinessPartnerTypes();
       IQueryable<Addresses> AddressesDb();
        IQueryable<Companies> CompaniesDb();
        IQueryable<BusinessPartnerTypes> BusinessPartnerTypesDb();
        bool VerifyBusinessPartnerUnique(Int64 ixBusinessPartner, string sBusinessPartner);
        List<string> VerifyBusinessPartnerDeleteOK(Int64 ixBusinessPartner, string sBusinessPartner);
        void RegisterCreate(BusinessPartnersPost businesspartnersPost);
        void RegisterEdit(BusinessPartnersPost businesspartnersPost);
        void RegisterDelete(BusinessPartnersPost businesspartnersPost);
        void Rollback();
        void Commit();
    }
}
  

