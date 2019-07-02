using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IStatusesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixStatus { get; set; }
		String sStatus { get; set; }
		String sStatusCode { get; set; }
		String UserName { get; set; }
    }
}
  

