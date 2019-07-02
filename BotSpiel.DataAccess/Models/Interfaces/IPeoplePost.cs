using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IPeoplePost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/
		Int64 ixPerson { get; set; }
		String sPerson { get; set; }
		String sFirstName { get; set; }
		String sLastName { get; set; }
		Int64 ixLanguage { get; set; }
		String UserName { get; set; }
    }
}
  

