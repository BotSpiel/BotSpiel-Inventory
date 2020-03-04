using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IBotModuleGrids
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixBotModuleGrid { get; set; }
		Int64 ixBotModuleGridEdit { get; set; }
		String sBotModuleGrid { get; set; }
		String sShortDescription { get; set; }
		String sDataEntityType { get; set; }
		Boolean bCanCreate { get; set; }
		Boolean bCanEdit { get; set; }
		Boolean bCanDelete { get; set; }
    }
}
  

