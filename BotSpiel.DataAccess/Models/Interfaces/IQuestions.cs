using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public interface IQuestions
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

		Int64 ixQuestion { get; set; }
		Int64 ixQuestionEdit { get; set; }
		String sQuestion { get; set; }
		Int64 ixLanguage { get; set; }
		Int64 ixLanguageStyle { get; set; }
		Int64 ixTopic { get; set; }
		String sAsk { get; set; }
		String sAnswer { get; set; }
		Int64 ixResponseType { get; set; }
		Boolean bActive { get; set; }
		DateTime dtCreatedAt { get; set; }
		DateTime dtChangedAt { get; set; }
		String sCreatedBy { get; set; }
		String sChangedBy { get; set; }
		LanguageStyles LanguageStylesFKDiffLanguage { get; set; }
		LanguageStyles LanguageStyles { get; set; }
		Topics Topics { get; set; }
		ResponseTypes ResponseTypes { get; set; }
    }
}
  

