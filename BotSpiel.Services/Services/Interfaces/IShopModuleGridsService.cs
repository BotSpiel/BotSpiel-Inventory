using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IShopModuleGridsService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        ShopModuleGridsPost GetPost(Int64 ixShopModuleGrid);        
		ShopModuleGrids Get(Int64 ixShopModuleGrid);
        IQueryable<ShopModuleGrids> Index();
        IQueryable<ShopModuleGrids> IndexDb();
		IQueryable<ShopModuleGridsconfig> Indexconfig();
		IQueryable<ShopModuleGridsmd> Indexmd();
		IQueryable<ShopModuleGridstx> Indextx();
		IQueryable<ShopModuleGridsanalytics> Indexanalytics();
        List<string> VerifyShopModuleGridDeleteOK(Int64 ixShopModuleGrid, string sShopModuleGrid);

        Task<Int64> Create(ShopModuleGridsPost shopmodulegridsPost);
        Task Edit(ShopModuleGridsPost shopmodulegridsPost);
        Task Delete(ShopModuleGridsPost shopmodulegridsPost);
    }
}
  

