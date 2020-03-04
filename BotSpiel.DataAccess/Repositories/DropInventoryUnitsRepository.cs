using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class DropInventoryUnitsRepository : IDropInventoryUnitsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly DropInventoryUnitsDB _context;
  
        public DropInventoryUnitsRepository(DropInventoryUnitsDB context)
        {
            _context = context;
  
        }

        public DropInventoryUnitsPost GetPost(Int64 ixDropInventoryUnit) => _context.DropInventoryUnitsPost.AsNoTracking().Where(x => x.ixDropInventoryUnit == ixDropInventoryUnit).First();
         
		public DropInventoryUnits Get(Int64 ixDropInventoryUnit)
        {
            DropInventoryUnits dropinventoryunits = _context.DropInventoryUnits.AsNoTracking().Where(x => x.ixDropInventoryUnit == ixDropInventoryUnit).First();
            return dropinventoryunits;
        }

        public IQueryable<DropInventoryUnits> Index()
        {
            var dropinventoryunits = _context.DropInventoryUnits.AsNoTracking(); 
            return dropinventoryunits;
        }

        public IQueryable<DropInventoryUnits> IndexDb()
        {
            var dropinventoryunits = _context.DropInventoryUnits.AsNoTracking(); 
            return dropinventoryunits;
        }
        public List<string> VerifyDropInventoryUnitDeleteOK(Int64 ixDropInventoryUnit, string sDropInventoryUnit)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(DropInventoryUnitsPost dropinventoryunitsPost)
		{
            _context.DropInventoryUnitsPost.Add(dropinventoryunitsPost); 
        }

        public void RegisterEdit(DropInventoryUnitsPost dropinventoryunitsPost)
        {
            _context.Entry(dropinventoryunitsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(DropInventoryUnitsPost dropinventoryunitsPost)
        {
            _context.DropInventoryUnitsPost.Remove(dropinventoryunitsPost);
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
  

