using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ICarrierTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        CarrierTypesPost GetPost(Int64 ixCarrierType);        
		CarrierTypes Get(Int64 ixCarrierType);
        IQueryable<CarrierTypes> Index();
        bool VerifyCarrierTypeUnique(Int64 ixCarrierType, string sCarrierType);
        List<string> VerifyCarrierTypeDeleteOK(Int64 ixCarrierType, string sCarrierType);
        void RegisterCreate(CarrierTypesPost carriertypesPost);
        void RegisterEdit(CarrierTypesPost carriertypesPost);
        void RegisterDelete(CarrierTypesPost carriertypesPost);
        void Rollback();
        void Commit();
    }
}
  

