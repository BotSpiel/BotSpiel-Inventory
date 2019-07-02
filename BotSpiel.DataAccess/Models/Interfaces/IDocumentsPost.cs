using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IDocumentsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixDocument { get; set; }
		String sDocument { get; set; }
		Int64 ixDocumentMessageType { get; set; }
		String sVersion { get; set; }
		String sRevision { get; set; }
		Int64 ixStatus { get; set; }
		String UserName { get; set; }
    }
}
  

