using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IInventoryUnitTransactionContextsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixInventoryUnitTransactionContext { get; set; }
		String sInventoryUnitTransactionContext { get; set; }
		String UserName { get; set; }
    }
}
  

