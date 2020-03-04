using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ISendTextMessagesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixSendTextMessage { get; set; }
		String sSendTextMessage { get; set; }
		Int64 ixPerson { get; set; }
		String sContent { get; set; }
		String UserName { get; set; }
    }
}
  

