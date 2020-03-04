using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface ICarrierServicesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        CarrierServicesPost GetPost(Int64 ixCarrierService);        
		CarrierServices Get(Int64 ixCarrierService);
        IQueryable<CarrierServices> Index();
        IQueryable<CarrierServices> IndexDb();
       IQueryable<Carriers> selectCarriers();
       IQueryable<Carriers> CarriersDb();
        bool VerifyCarrierServiceUnique(Int64 ixCarrierService, string sCarrierService);
        List<string> VerifyCarrierServiceDeleteOK(Int64 ixCarrierService, string sCarrierService);
        void RegisterCreate(CarrierServicesPost carrierservicesPost);
        void RegisterEdit(CarrierServicesPost carrierservicesPost);
        void RegisterDelete(CarrierServicesPost carrierservicesPost);
        void Rollback();
        void Commit();
    }
}
  

