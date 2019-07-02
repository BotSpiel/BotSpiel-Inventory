using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IAisleFaceStorageTypesRepository
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
        void RegisterCreate(AisleFaceStorageTypesPost aislefacestoragetypesPost);
        void RegisterEdit(AisleFaceStorageTypesPost aislefacestoragetypesPost);
        void RegisterDelete(AisleFaceStorageTypesPost aislefacestoragetypesPost);
        void Rollback();
        void Commit();
    }
}
  

