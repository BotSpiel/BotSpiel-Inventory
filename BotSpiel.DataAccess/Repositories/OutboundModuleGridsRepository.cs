using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class OutboundModuleGridsRepository : IOutboundModuleGridsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly OutboundModuleGridsDB _context;
  
        public OutboundModuleGridsRepository(OutboundModuleGridsDB context)
        {
            _context = context;
  
        }

        public OutboundModuleGridsPost GetPost(Int64 ixOutboundModuleGrid) => _context.OutboundModuleGridsPost.AsNoTracking().Where(x => x.ixOutboundModuleGrid == ixOutboundModuleGrid).First();
         
		public OutboundModuleGrids Get(Int64 ixOutboundModuleGrid)
        {
            OutboundModuleGrids outboundmodulegrids = _context.OutboundModuleGrids.AsNoTracking().Where(x => x.ixOutboundModuleGrid == ixOutboundModuleGrid).First();
            return outboundmodulegrids;
        }

        public IQueryable<OutboundModuleGrids> Index()
        {
            var outboundmodulegrids = _context.OutboundModuleGrids.AsNoTracking(); 
            return outboundmodulegrids;
        }
		public IQueryable<OutboundModuleGridsconfig> Indexconfig() => _context.OutboundModuleGridsconfig;
		public IQueryable<OutboundModuleGridsmd> Indexmd() => _context.OutboundModuleGridsmd;
		public IQueryable<OutboundModuleGridstx> Indextx() => _context.OutboundModuleGridstx;
		public IQueryable<OutboundModuleGridsanalytics> Indexanalytics() => _context.OutboundModuleGridsanalytics;
        public List<string> VerifyOutboundModuleGridDeleteOK(Int64 ixOutboundModuleGrid, string sOutboundModuleGrid)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(OutboundModuleGridsPost outboundmodulegridsPost)
		{
            _context.OutboundModuleGridsPost.Add(outboundmodulegridsPost); 
        }

        public void RegisterEdit(OutboundModuleGridsPost outboundmodulegridsPost)
        {
            _context.Entry(outboundmodulegridsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(OutboundModuleGridsPost outboundmodulegridsPost)
        {
            _context.OutboundModuleGridsPost.Remove(outboundmodulegridsPost);
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
  

