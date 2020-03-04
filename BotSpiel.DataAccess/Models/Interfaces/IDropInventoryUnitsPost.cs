using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IDropInventoryUnitsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixDropInventoryUnit { get; set; }
		String sDropInventoryUnit { get; set; }
		String UserName { get; set; }
    }
}
  

