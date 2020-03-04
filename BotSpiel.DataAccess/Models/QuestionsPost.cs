using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class QuestionsPost : IQuestionsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Question ID")]
		public virtual Int64 ixQuestion { get; set; }
		[Required]
		[StringLength(4000)]
		[Display(Name = "Question")]
		public virtual String sQuestion { get; set; }
		[Required]
		[Display(Name = "Language ID")]
		public virtual Int64 ixLanguage { get; set; }
		[Required]
		[Display(Name = "Language Style ID")]
		public virtual Int64 ixLanguageStyle { get; set; }
		[Required]
		[Display(Name = "Topic ID")]
		public virtual Int64 ixTopic { get; set; }
		[Required]
		[Display(Name = "Ask")]
		public virtual String sAsk { get; set; }
		[Required]
		[Display(Name = "Answer")]
		public virtual String sAnswer { get; set; }
		[Required]
		[Display(Name = "Response Type ID")]
		public virtual Int64 ixResponseType { get; set; }
		[Required]
		[Display(Name = "Active")]
		public virtual Boolean bActive { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

