using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IShopModuleGridsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixShopModuleGrid { get; set; }
		String sShopModuleGrid { get; set; }
		String sShortDescription { get; set; }
		String sDataEntityType { get; set; }
		Boolean bCanCreate { get; set; }
		Boolean bCanEdit { get; set; }
		Boolean bCanDelete { get; set; }
		String UserName { get; set; }
    }
}
  

