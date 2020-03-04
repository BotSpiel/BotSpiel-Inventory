using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IGreetingsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixGreeting { get; set; }
		String sGreeting { get; set; }
		Int64 ixLanguage { get; set; }
		Int64 ixLanguageStyle { get; set; }
		String sGreetingOffered { get; set; }
		String sGreetingResponse { get; set; }
		Int64 ixResponseType { get; set; }
		Boolean bActive { get; set; }
		String UserName { get; set; }
    }
}
  

