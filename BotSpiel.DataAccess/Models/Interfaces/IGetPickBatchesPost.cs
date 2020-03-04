using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IGetPickBatchesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixGetPickBatch { get; set; }
		String sGetPickBatch { get; set; }
		String UserName { get; set; }
    }
}
  

