using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IMaterialTypesService
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
        bool VerifyMaterialTypeUnique(Int64 ixMaterialType, string sMaterialType);
        List<string> VerifyMaterialTypeDeleteOK(Int64 ixMaterialType, string sMaterialType);

        Task<Int64> Create(MaterialTypesPost materialtypesPost);
        Task Edit(MaterialTypesPost materialtypesPost);
        Task Delete(MaterialTypesPost materialtypesPost);
    }
}
  

