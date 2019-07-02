using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class MaterialTypesRepository : IMaterialTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly MaterialTypesDB _context;
       private readonly MaterialsDB _contextMaterials;
  
        public MaterialTypesRepository(MaterialTypesDB context, MaterialsDB contextMaterials)
        {
            _context = context;
           _contextMaterials = contextMaterials;
  
        }

        public MaterialTypesPost GetPost(Int64 ixMaterialType) => _context.MaterialTypesPost.AsNoTracking().Where(x => x.ixMaterialType == ixMaterialType).First();
         
		public MaterialTypes Get(Int64 ixMaterialType)
        {
            MaterialTypes materialtypes = _context.MaterialTypes.AsNoTracking().Where(x => x.ixMaterialType == ixMaterialType).First();
            return materialtypes;
        }

        public IQueryable<MaterialTypes> Index()
        {
            var materialtypes = _context.MaterialTypes.AsNoTracking(); 
            return materialtypes;
        }
        public bool VerifyMaterialTypeUnique(Int64 ixMaterialType, string sMaterialType)
        {
            if (_context.MaterialTypes.AsNoTracking().Where(x => x.sMaterialType == sMaterialType).Any() && ixMaterialType == 0L) return false;
            else if (_context.MaterialTypes.AsNoTracking().Where(x => x.sMaterialType == sMaterialType && x.ixMaterialType != ixMaterialType).Any() && ixMaterialType != 0L) return false;
            else return true;
        }

        public List<string> VerifyMaterialTypeDeleteOK(Int64 ixMaterialType, string sMaterialType)
        {
            List<string> existInEntities = new List<string>();
           if (_contextMaterials.Materials.AsNoTracking().Where(x => x.ixMaterialType == ixMaterialType).Any()) existInEntities.Add("Materials");

            return existInEntities;
        }


        public void RegisterCreate(MaterialTypesPost materialtypesPost)
		{
            _context.MaterialTypesPost.Add(materialtypesPost); 
        }

        public void RegisterEdit(MaterialTypesPost materialtypesPost)
        {
            _context.Entry(materialtypesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(MaterialTypesPost materialtypesPost)
        {
            _context.MaterialTypesPost.Remove(materialtypesPost);
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
  

