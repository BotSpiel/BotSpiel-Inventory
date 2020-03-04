using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IShopModuleGridsRepository
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
        void RegisterCreate(ShopModuleGridsPost shopmodulegridsPost);
        void RegisterEdit(ShopModuleGridsPost shopmodulegridsPost);
        void RegisterDelete(ShopModuleGridsPost shopmodulegridsPost);
        void Rollback();
        void Commit();
    }
}
  

