using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ISendTextMessages
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixSendTextMessage { get; set; }
		Int64 ixSendTextMessageEdit { get; set; }
		String sSendTextMessage { get; set; }
		Int64 ixPerson { get; set; }
		String sContent { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		People People { get; set; }
    }
}
  

