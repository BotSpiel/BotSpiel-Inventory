using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class ExecutionModuleGridsRepository : IExecutionModuleGridsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly ExecutionModuleGridsDB _context;
  
        public ExecutionModuleGridsRepository(ExecutionModuleGridsDB context)
        {
            _context = context;
  
        }

        public ExecutionModuleGridsPost GetPost(Int64 ixExecutionModuleGrid) => _context.ExecutionModuleGridsPost.AsNoTracking().Where(x => x.ixExecutionModuleGrid == ixExecutionModuleGrid).First();
         
		public ExecutionModuleGrids Get(Int64 ixExecutionModuleGrid)
        {
            ExecutionModuleGrids executionmodulegrids = _context.ExecutionModuleGrids.AsNoTracking().Where(x => x.ixExecutionModuleGrid == ixExecutionModuleGrid).First();
            return executionmodulegrids;
        }

        public IQueryable<ExecutionModuleGrids> Index()
        {
            var executionmodulegrids = _context.ExecutionModuleGrids.AsNoTracking(); 
            return executionmodulegrids;
        }

        public IQueryable<ExecutionModuleGrids> IndexDb()
        {
            var executionmodulegrids = _context.ExecutionModuleGrids.AsNoTracking(); 
            return executionmodulegrids;
        }
		public IQueryable<ExecutionModuleGridsconfig> Indexconfig() => _context.ExecutionModuleGridsconfig;
		public IQueryable<ExecutionModuleGridsmd> Indexmd() => _context.ExecutionModuleGridsmd;
		public IQueryable<ExecutionModuleGridstx> Indextx() => _context.ExecutionModuleGridstx;
		public IQueryable<ExecutionModuleGridsanalytics> Indexanalytics() => _context.ExecutionModuleGridsanalytics;
        public List<string> VerifyExecutionModuleGridDeleteOK(Int64 ixExecutionModuleGrid, string sExecutionModuleGrid)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(ExecutionModuleGridsPost executionmodulegridsPost)
		{
            _context.ExecutionModuleGridsPost.Add(executionmodulegridsPost); 
        }

        public void RegisterEdit(ExecutionModuleGridsPost executionmodulegridsPost)
        {
            _context.Entry(executionmodulegridsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(ExecutionModuleGridsPost executionmodulegridsPost)
        {
            _context.ExecutionModuleGridsPost.Remove(executionmodulegridsPost);
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
  

