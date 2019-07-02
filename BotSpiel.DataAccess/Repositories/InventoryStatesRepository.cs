using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class InventoryStatesRepository : IInventoryStatesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly InventoryStatesDB _context;
       private readonly InventoryUnitsDB _contextInventoryUnits;
        private readonly InventoryUnitTransactionsDB _contextInventoryUnitTransactions;
  
        public InventoryStatesRepository(InventoryStatesDB context, InventoryUnitsDB contextInventoryUnits, InventoryUnitTransactionsDB contextInventoryUnitTransactions)
        {
            _context = context;
           _contextInventoryUnits = contextInventoryUnits;
            _contextInventoryUnitTransactions = contextInventoryUnitTransactions;
  
        }

        public InventoryStatesPost GetPost(Int64 ixInventoryState) => _context.InventoryStatesPost.AsNoTracking().Where(x => x.ixInventoryState == ixInventoryState).First();
         
		public InventoryStates Get(Int64 ixInventoryState)
        {
            InventoryStates inventorystates = _context.InventoryStates.AsNoTracking().Where(x => x.ixInventoryState == ixInventoryState).First();
            return inventorystates;
        }

        public IQueryable<InventoryStates> Index()
        {
            var inventorystates = _context.InventoryStates.AsNoTracking(); 
            return inventorystates;
        }
        public bool VerifyInventoryStateUnique(Int64 ixInventoryState, string sInventoryState)
        {
            if (_context.InventoryStates.AsNoTracking().Where(x => x.sInventoryState == sInventoryState).Any() && ixInventoryState == 0L) return false;
            else if (_context.InventoryStates.AsNoTracking().Where(x => x.sInventoryState == sInventoryState && x.ixInventoryState != ixInventoryState).Any() && ixInventoryState != 0L) return false;
            else return true;
        }

        public List<string> VerifyInventoryStateDeleteOK(Int64 ixInventoryState, string sInventoryState)
        {
            List<string> existInEntities = new List<string>();
           if (_contextInventoryUnits.InventoryUnits.AsNoTracking().Where(x => x.ixInventoryState == ixInventoryState).Any()) existInEntities.Add("InventoryUnits");
            if (_contextInventoryUnitTransactions.InventoryUnitTransactions.AsNoTracking().Where(x => x.ixInventoryStateAfter == ixInventoryState).Any()) existInEntities.Add("InventoryUnitTransactions");
            if (_contextInventoryUnitTransactions.InventoryUnitTransactions.AsNoTracking().Where(x => x.ixInventoryStateBefore == ixInventoryState).Any()) existInEntities.Add("InventoryUnitTransactions");

            return existInEntities;
        }


        public void RegisterCreate(InventoryStatesPost inventorystatesPost)
		{
            _context.InventoryStatesPost.Add(inventorystatesPost); 
        }

        public void RegisterEdit(InventoryStatesPost inventorystatesPost)
        {
            _context.Entry(inventorystatesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(InventoryStatesPost inventorystatesPost)
        {
            _context.InventoryStatesPost.Remove(inventorystatesPost);
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
  

