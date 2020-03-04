using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class BotModuleGridsRepository : IBotModuleGridsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly BotModuleGridsDB _context;
  
        public BotModuleGridsRepository(BotModuleGridsDB context)
        {
            _context = context;
  
        }

        public BotModuleGridsPost GetPost(Int64 ixBotModuleGrid) => _context.BotModuleGridsPost.AsNoTracking().Where(x => x.ixBotModuleGrid == ixBotModuleGrid).First();
         
		public BotModuleGrids Get(Int64 ixBotModuleGrid)
        {
            BotModuleGrids botmodulegrids = _context.BotModuleGrids.AsNoTracking().Where(x => x.ixBotModuleGrid == ixBotModuleGrid).First();
            return botmodulegrids;
        }

        public IQueryable<BotModuleGrids> Index()
        {
            var botmodulegrids = _context.BotModuleGrids.AsNoTracking(); 
            return botmodulegrids;
        }

        public IQueryable<BotModuleGrids> IndexDb()
        {
            var botmodulegrids = _context.BotModuleGrids.AsNoTracking(); 
            return botmodulegrids;
        }
		public IQueryable<BotModuleGridsconfig> Indexconfig() => _context.BotModuleGridsconfig;
		public IQueryable<BotModuleGridsmd> Indexmd() => _context.BotModuleGridsmd;
		public IQueryable<BotModuleGridstx> Indextx() => _context.BotModuleGridstx;
		public IQueryable<BotModuleGridsanalytics> Indexanalytics() => _context.BotModuleGridsanalytics;
        public List<string> VerifyBotModuleGridDeleteOK(Int64 ixBotModuleGrid, string sBotModuleGrid)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(BotModuleGridsPost botmodulegridsPost)
		{
            _context.BotModuleGridsPost.Add(botmodulegridsPost); 
        }

        public void RegisterEdit(BotModuleGridsPost botmodulegridsPost)
        {
            _context.Entry(botmodulegridsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(BotModuleGridsPost botmodulegridsPost)
        {
            _context.BotModuleGridsPost.Remove(botmodulegridsPost);
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
  

