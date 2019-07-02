using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPickBatchesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixPickBatch { get; set; }
		String sPickBatch { get; set; }
		Int64 ixPickBatchType { get; set; }
		Boolean bMultiResource { get; set; }
		DateTime dtStartBy { get; set; }
		DateTime dtCompleteBy { get; set; }
		Int64 ixStatus { get; set; }
		String UserName { get; set; }
    }
}
  

