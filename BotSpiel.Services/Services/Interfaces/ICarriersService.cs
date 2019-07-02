using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ICarriersService
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
       IQueryable<CarrierTypes> selectCarrierTypes();
        bool VerifyCarrierUnique(Int64 ixCarrier, string sCarrier);
        List<string> VerifyCarrierDeleteOK(Int64 ixCarrier, string sCarrier);

        Task<Int64> Create(CarriersPost carriersPost);
        Task Edit(CarriersPost carriersPost);
        Task Delete(CarriersPost carriersPost);
    }
}
  

