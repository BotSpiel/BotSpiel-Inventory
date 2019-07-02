using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPickBatches
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixPickBatch { get; set; }
		Int64 ixPickBatchEdit { get; set; }
		String sPickBatch { get; set; }
		Int64 ixPickBatchType { get; set; }
		Boolean bMultiResource { get; set; }
		DateTime dtStartBy { get; set; }
		DateTime dtCompleteBy { get; set; }
		Int64 ixStatus { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		PickBatchTypes PickBatchTypes { get; set; }
		Statuses Statuses { get; set; }
    }
}
  

