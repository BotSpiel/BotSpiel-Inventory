using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IMaterialTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        MaterialTypesPost GetPost(Int64 ixMaterialType);        
		MaterialTypes Get(Int64 ixMaterialType);
        IQueryable<MaterialTypes> Index();
        IQueryable<MaterialTypes> IndexDb();
        bool VerifyMaterialTypeUnique(Int64 ixMaterialType, string sMaterialType);
        List<string> VerifyMaterialTypeDeleteOK(Int64 ixMaterialType, string sMaterialType);
        void RegisterCreate(MaterialTypesPost materialtypesPost);
        void RegisterEdit(MaterialTypesPost materialtypesPost);
        void RegisterDelete(MaterialTypesPost materialtypesPost);
        void Rollback();
        void Commit();
    }
}
  

