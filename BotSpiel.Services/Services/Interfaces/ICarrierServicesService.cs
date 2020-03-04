using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ICarrierServicesService
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

        Task<Int64> Create(CarrierServicesPost carrierservicesPost);
        Task Edit(CarrierServicesPost carrierservicesPost);
        Task Delete(CarrierServicesPost carrierservicesPost);
    }
}
  

