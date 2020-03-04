using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ISendEmails
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixSendEmail { get; set; }
		Int64 ixSendEmailEdit { get; set; }
		String sSendEmail { get; set; }
		Int64 ixPerson { get; set; }
		String sSubject { get; set; }
		String sContent { get; set; }
		String sAttachment { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		People People { get; set; }
    }
}
  

