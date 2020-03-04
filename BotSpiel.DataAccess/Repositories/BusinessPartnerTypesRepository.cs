using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class BusinessPartnerTypesRepository : IBusinessPartnerTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly BusinessPartnerTypesDB _context;
       private readonly BusinessPartnersDB _contextBusinessPartners;
  
        public BusinessPartnerTypesRepository(BusinessPartnerTypesDB context, BusinessPartnersDB contextBusinessPartners)
        {
            _context = context;
           _contextBusinessPartners = contextBusinessPartners;
  
        }

        public BusinessPartnerTypesPost GetPost(Int64 ixBusinessPartnerType) => _context.BusinessPartnerTypesPost.AsNoTracking().Where(x => x.ixBusinessPartnerType == ixBusinessPartnerType).First();
         
		public BusinessPartnerTypes Get(Int64 ixBusinessPartnerType)
        {
            BusinessPartnerTypes businesspartnertypes = _context.BusinessPartnerTypes.AsNoTracking().Where(x => x.ixBusinessPartnerType == ixBusinessPartnerType).First();
            return businesspartnertypes;
        }

        public IQueryable<BusinessPartnerTypes> Index()
        {
            var businesspartnertypes = _context.BusinessPartnerTypes.AsNoTracking(); 
            return businesspartnertypes;
        }

        public IQueryable<BusinessPartnerTypes> IndexDb()
        {
            var businesspartnertypes = _context.BusinessPartnerTypes.AsNoTracking(); 
            return businesspartnertypes;
        }
        public bool VerifyBusinessPartnerTypeUnique(Int64 ixBusinessPartnerType, string sBusinessPartnerType)
        {
            if (_context.BusinessPartnerTypes.AsNoTracking().Where(x => x.sBusinessPartnerType == sBusinessPartnerType).Any() && ixBusinessPartnerType == 0L) return false;
            else if (_context.BusinessPartnerTypes.AsNoTracking().Where(x => x.sBusinessPartnerType == sBusinessPartnerType && x.ixBusinessPartnerType != ixBusinessPartnerType).Any() && ixBusinessPartnerType != 0L) return false;
            else return true;
        }

        public List<string> VerifyBusinessPartnerTypeDeleteOK(Int64 ixBusinessPartnerType, string sBusinessPartnerType)
        {
            List<string> existInEntities = new List<string>();
           if (_contextBusinessPartners.BusinessPartners.AsNoTracking().Where(x => x.ixBusinessPartnerType == ixBusinessPartnerType).Any()) existInEntities.Add("BusinessPartners");

            return existInEntities;
        }


        public void RegisterCreate(BusinessPartnerTypesPost businesspartnertypesPost)
		{
            _context.BusinessPartnerTypesPost.Add(businesspartnertypesPost); 
        }

        public void RegisterEdit(BusinessPartnerTypesPost businesspartnertypesPost)
        {
            _context.Entry(businesspartnertypesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(BusinessPartnerTypesPost businesspartnertypesPost)
        {
            _context.BusinessPartnerTypesPost.Remove(businesspartnertypesPost);
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

    }
}
  

