using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IDocuments
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixDocument { get; set; }
		Int64 ixDocumentEdit { get; set; }
		String sDocument { get; set; }
		Int64 ixDocumentMessageType { get; set; }
		String sVersion { get; set; }
		String sRevision { get; set; }
		Int64 ixStatus { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		DocumentMessageTypes DocumentMessageTypes { get; set; }
		Statuses Statuses { get; set; }
    }
}
  

