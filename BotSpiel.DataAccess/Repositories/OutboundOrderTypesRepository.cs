using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class OutboundOrderTypesRepository : IOutboundOrderTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly OutboundOrderTypesDB _context;
       private readonly OutboundOrdersDB _contextOutboundOrders;
  
        public OutboundOrderTypesRepository(OutboundOrderTypesDB context, OutboundOrdersDB contextOutboundOrders)
        {
            _context = context;
           _contextOutboundOrders = contextOutboundOrders;
  
        }

        public OutboundOrderTypesPost GetPost(Int64 ixOutboundOrderType) => _context.OutboundOrderTypesPost.AsNoTracking().Where(x => x.ixOutboundOrderType == ixOutboundOrderType).First();
         
		public OutboundOrderTypes Get(Int64 ixOutboundOrderType)
        {
            OutboundOrderTypes outboundordertypes = _context.OutboundOrderTypes.AsNoTracking().Where(x => x.ixOutboundOrderType == ixOutboundOrderType).First();
            return outboundordertypes;
        }

        public IQueryable<OutboundOrderTypes> Index()
        {
            var outboundordertypes = _context.OutboundOrderTypes.AsNoTracking(); 
            return outboundordertypes;
        }
        public bool VerifyOutboundOrderTypeUnique(Int64 ixOutboundOrderType, string sOutboundOrderType)
        {
            if (_context.OutboundOrderTypes.AsNoTracking().Where(x => x.sOutboundOrderType == sOutboundOrderType).Any() && ixOutboundOrderType == 0L) return false;
            else if (_context.OutboundOrderTypes.AsNoTracking().Where(x => x.sOutboundOrderType == sOutboundOrderType && x.ixOutboundOrderType != ixOutboundOrderType).Any() && ixOutboundOrderType != 0L) return false;
            else return true;
        }

        public List<string> VerifyOutboundOrderTypeDeleteOK(Int64 ixOutboundOrderType, string sOutboundOrderType)
        {
            List<string> existInEntities = new List<string>();
           if (_contextOutboundOrders.OutboundOrders.AsNoTracking().Where(x => x.ixOutboundOrderType == ixOutboundOrderType).Any()) existInEntities.Add("OutboundOrders");

            return existInEntities;
        }


        public void RegisterCreate(OutboundOrderTypesPost outboundordertypesPost)
		{
            _context.OutboundOrderTypesPost.Add(outboundordertypesPost); 
        }

        public void RegisterEdit(OutboundOrderTypesPost outboundordertypesPost)
        {
            _context.Entry(outboundordertypesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(OutboundOrderTypesPost outboundordertypesPost)
        {
            _context.OutboundOrderTypesPost.Remove(outboundordertypesPost);
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
  

