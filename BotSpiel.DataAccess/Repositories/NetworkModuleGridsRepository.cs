using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class NetworkModuleGridsRepository : INetworkModuleGridsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly NetworkModuleGridsDB _context;
  
        public NetworkModuleGridsRepository(NetworkModuleGridsDB context)
        {
            _context = context;
  
        }

        public NetworkModuleGridsPost GetPost(Int64 ixNetworkModuleGrid) => _context.NetworkModuleGridsPost.AsNoTracking().Where(x => x.ixNetworkModuleGrid == ixNetworkModuleGrid).First();
         
		public NetworkModuleGrids Get(Int64 ixNetworkModuleGrid)
        {
            NetworkModuleGrids networkmodulegrids = _context.NetworkModuleGrids.AsNoTracking().Where(x => x.ixNetworkModuleGrid == ixNetworkModuleGrid).First();
            return networkmodulegrids;
        }

        public IQueryable<NetworkModuleGrids> Index()
        {
            var networkmodulegrids = _context.NetworkModuleGrids.AsNoTracking(); 
            return networkmodulegrids;
        }

        public IQueryable<NetworkModuleGrids> IndexDb()
        {
            var networkmodulegrids = _context.NetworkModuleGrids.AsNoTracking(); 
            return networkmodulegrids;
        }
		public IQueryable<NetworkModuleGridsconfig> Indexconfig() => _context.NetworkModuleGridsconfig;
		public IQueryable<NetworkModuleGridsmd> Indexmd() => _context.NetworkModuleGridsmd;
		public IQueryable<NetworkModuleGridstx> Indextx() => _context.NetworkModuleGridstx;
		public IQueryable<NetworkModuleGridsanalytics> Indexanalytics() => _context.NetworkModuleGridsanalytics;
        public List<string> VerifyNetworkModuleGridDeleteOK(Int64 ixNetworkModuleGrid, string sNetworkModuleGrid)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(NetworkModuleGridsPost networkmodulegridsPost)
		{
            _context.NetworkModuleGridsPost.Add(networkmodulegridsPost); 
        }

        public void RegisterEdit(NetworkModuleGridsPost networkmodulegridsPost)
        {
            _context.Entry(networkmodulegridsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(NetworkModuleGridsPost networkmodulegridsPost)
        {
            _context.NetworkModuleGridsPost.Remove(networkmodulegridsPost);
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
  

