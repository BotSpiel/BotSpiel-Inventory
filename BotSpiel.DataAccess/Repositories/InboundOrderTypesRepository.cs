using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class InboundOrderTypesRepository : IInboundOrderTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly InboundOrderTypesDB _context;
       private readonly InboundOrdersDB _contextInboundOrders;
  
        public InboundOrderTypesRepository(InboundOrderTypesDB context, InboundOrdersDB contextInboundOrders)
        {
            _context = context;
           _contextInboundOrders = contextInboundOrders;
  
        }

        public InboundOrderTypesPost GetPost(Int64 ixInboundOrderType) => _context.InboundOrderTypesPost.AsNoTracking().Where(x => x.ixInboundOrderType == ixInboundOrderType).First();
         
		public InboundOrderTypes Get(Int64 ixInboundOrderType)
        {
            InboundOrderTypes inboundordertypes = _context.InboundOrderTypes.AsNoTracking().Where(x => x.ixInboundOrderType == ixInboundOrderType).First();
            return inboundordertypes;
        }

        public IQueryable<InboundOrderTypes> Index()
        {
            var inboundordertypes = _context.InboundOrderTypes.AsNoTracking(); 
            return inboundordertypes;
        }

        public IQueryable<InboundOrderTypes> IndexDb()
        {
            var inboundordertypes = _context.InboundOrderTypes.AsNoTracking(); 
            return inboundordertypes;
        }
        public bool VerifyInboundOrderTypeUnique(Int64 ixInboundOrderType, string sInboundOrderType)
        {
            if (_context.InboundOrderTypes.AsNoTracking().Where(x => x.sInboundOrderType == sInboundOrderType).Any() && ixInboundOrderType == 0L) return false;
            else if (_context.InboundOrderTypes.AsNoTracking().Where(x => x.sInboundOrderType == sInboundOrderType && x.ixInboundOrderType != ixInboundOrderType).Any() && ixInboundOrderType != 0L) return false;
            else return true;
        }

        public List<string> VerifyInboundOrderTypeDeleteOK(Int64 ixInboundOrderType, string sInboundOrderType)
        {
            List<string> existInEntities = new List<string>();
           if (_contextInboundOrders.InboundOrders.AsNoTracking().Where(x => x.ixInboundOrderType == ixInboundOrderType).Any()) existInEntities.Add("InboundOrders");

            return existInEntities;
        }


        public void RegisterCreate(InboundOrderTypesPost inboundordertypesPost)
		{
            _context.InboundOrderTypesPost.Add(inboundordertypesPost); 
        }

        public void RegisterEdit(InboundOrderTypesPost inboundordertypesPost)
        {
            _context.Entry(inboundordertypesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(InboundOrderTypesPost inboundordertypesPost)
        {
            _context.InboundOrderTypesPost.Remove(inboundordertypesPost);
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
  

