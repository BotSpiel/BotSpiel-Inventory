using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface ICarrierTypesService
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
        IQueryable<CarrierTypes> IndexDb();
        bool VerifyCarrierTypeUnique(Int64 ixCarrierType, string sCarrierType);
        List<string> VerifyCarrierTypeDeleteOK(Int64 ixCarrierType, string sCarrierType);

        Task<Int64> Create(CarrierTypesPost carriertypesPost);
        Task Edit(CarrierTypesPost carriertypesPost);
        Task Delete(CarrierTypesPost carriertypesPost);
    }
}
  

