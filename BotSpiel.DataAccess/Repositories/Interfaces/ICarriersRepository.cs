using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ICarriersRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        CarriersPost GetPost(Int64 ixCarrier);        
		Carriers Get(Int64 ixCarrier);
        IQueryable<Carriers> Index();
        IQueryable<Carriers> IndexDb();
       IQueryable<CarrierTypes> selectCarrierTypes();
       IQueryable<CarrierTypes> CarrierTypesDb();
        bool VerifyCarrierUnique(Int64 ixCarrier, string sCarrier);
        List<string> VerifyCarrierDeleteOK(Int64 ixCarrier, string sCarrier);
        void RegisterCreate(CarriersPost carriersPost);
        void RegisterEdit(CarriersPost carriersPost);
        void RegisterDelete(CarriersPost carriersPost);
        void Rollback();
        void Commit();
    }
}
  

