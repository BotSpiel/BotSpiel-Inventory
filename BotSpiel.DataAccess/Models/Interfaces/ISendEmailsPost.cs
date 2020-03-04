using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface ISendEmailsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixSendEmail { get; set; }
		String sSendEmail { get; set; }
		Int64 ixPerson { get; set; }
		String sSubject { get; set; }
		String sContent { get; set; }
		String sAttachment { get; set; }
		String UserName { get; set; }
    }
}
  

