using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class CarrierTypesRepository : ICarrierTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly CarrierTypesDB _context;
       private readonly CarriersDB _contextCarriers;
  
        public CarrierTypesRepository(CarrierTypesDB context, CarriersDB contextCarriers)
        {
            _context = context;
           _contextCarriers = contextCarriers;
  
        }

        public CarrierTypesPost GetPost(Int64 ixCarrierType) => _context.CarrierTypesPost.AsNoTracking().Where(x => x.ixCarrierType == ixCarrierType).First();
         
		public CarrierTypes Get(Int64 ixCarrierType)
        {
            CarrierTypes carriertypes = _context.CarrierTypes.AsNoTracking().Where(x => x.ixCarrierType == ixCarrierType).First();
            return carriertypes;
        }

        public IQueryable<CarrierTypes> Index()
        {
            var carriertypes = _context.CarrierTypes.AsNoTracking(); 
            return carriertypes;
        }

        public IQueryable<CarrierTypes> IndexDb()
        {
            var carriertypes = _context.CarrierTypes.AsNoTracking(); 
            return carriertypes;
        }
        public bool VerifyCarrierTypeUnique(Int64 ixCarrierType, string sCarrierType)
        {
            if (_context.CarrierTypes.AsNoTracking().Where(x => x.sCarrierType == sCarrierType).Any() && ixCarrierType == 0L) return false;
            else if (_context.CarrierTypes.AsNoTracking().Where(x => x.sCarrierType == sCarrierType && x.ixCarrierType != ixCarrierType).Any() && ixCarrierType != 0L) return false;
            else return true;
        }

        public List<string> VerifyCarrierTypeDeleteOK(Int64 ixCarrierType, string sCarrierType)
        {
            List<string> existInEntities = new List<string>();
           if (_contextCarriers.Carriers.AsNoTracking().Where(x => x.ixCarrierType == ixCarrierType).Any()) existInEntities.Add("Carriers");

            return existInEntities;
        }


        public void RegisterCreate(CarrierTypesPost carriertypesPost)
		{
            _context.CarrierTypesPost.Add(carriertypesPost); 
        }

        public void RegisterEdit(CarrierTypesPost carriertypesPost)
        {
            _context.Entry(carriertypesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(CarrierTypesPost carriertypesPost)
        {
            _context.CarrierTypesPost.Remove(carriertypesPost);
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
  

