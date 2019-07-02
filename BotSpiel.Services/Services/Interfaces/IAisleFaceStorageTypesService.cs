using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IAisleFaceStorageTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        AisleFaceStorageTypesPost GetPost(Int64 ixAisleFaceStorageType);        
		AisleFaceStorageTypes Get(Int64 ixAisleFaceStorageType);
        IQueryable<AisleFaceStorageTypes> Index();
        bool VerifyAisleFaceStorageTypeUnique(Int64 ixAisleFaceStorageType, string sAisleFaceStorageType);
        List<string> VerifyAisleFaceStorageTypeDeleteOK(Int64 ixAisleFaceStorageType, string sAisleFaceStorageType);

        Task<Int64> Create(AisleFaceStorageTypesPost aislefacestoragetypesPost);
        Task Edit(AisleFaceStorageTypesPost aislefacestoragetypesPost);
        Task Delete(AisleFaceStorageTypesPost aislefacestoragetypesPost);
    }
}
  

