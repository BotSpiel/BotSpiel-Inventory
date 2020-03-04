using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class InboundModuleGridsRepository : IInboundModuleGridsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly InboundModuleGridsDB _context;
  
        public InboundModuleGridsRepository(InboundModuleGridsDB context)
        {
            _context = context;
  
        }

        public InboundModuleGridsPost GetPost(Int64 ixInboundModuleGrid) => _context.InboundModuleGridsPost.AsNoTracking().Where(x => x.ixInboundModuleGrid == ixInboundModuleGrid).First();
         
		public InboundModuleGrids Get(Int64 ixInboundModuleGrid)
        {
            InboundModuleGrids inboundmodulegrids = _context.InboundModuleGrids.AsNoTracking().Where(x => x.ixInboundModuleGrid == ixInboundModuleGrid).First();
            return inboundmodulegrids;
        }

        public IQueryable<InboundModuleGrids> Index()
        {
            var inboundmodulegrids = _context.InboundModuleGrids.AsNoTracking(); 
            return inboundmodulegrids;
        }

        public IQueryable<InboundModuleGrids> IndexDb()
        {
            var inboundmodulegrids = _context.InboundModuleGrids.AsNoTracking(); 
            return inboundmodulegrids;
        }
		public IQueryable<InboundModuleGridsconfig> Indexconfig() => _context.InboundModuleGridsconfig;
		public IQueryable<InboundModuleGridsmd> Indexmd() => _context.InboundModuleGridsmd;
		public IQueryable<InboundModuleGridstx> Indextx() => _context.InboundModuleGridstx;
		public IQueryable<InboundModuleGridsanalytics> Indexanalytics() => _context.InboundModuleGridsanalytics;
        public List<string> VerifyInboundModuleGridDeleteOK(Int64 ixInboundModuleGrid, string sInboundModuleGrid)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(InboundModuleGridsPost inboundmodulegridsPost)
		{
            _context.InboundModuleGridsPost.Add(inboundmodulegridsPost); 
        }

        public void RegisterEdit(InboundModuleGridsPost inboundmodulegridsPost)
        {
            _context.Entry(inboundmodulegridsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(InboundModuleGridsPost inboundmodulegridsPost)
        {
            _context.InboundModuleGridsPost.Remove(inboundmodulegridsPost);
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
  

