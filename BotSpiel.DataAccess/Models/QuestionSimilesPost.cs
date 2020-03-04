using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class QuestionSimilesPost : IQuestionSimilesPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Question Simile ID")]
		public virtual Int64 ixQuestionSimile { get; set; }
		[Display(Name = "Question Simile")]
		public virtual String sQuestionSimile { get; set; }
		[Required]
		[Display(Name = "Question ID")]
		public virtual Int64 ixQuestion { get; set; }
		[Required]
		[Display(Name = "Question Simile Text")]
		public virtual String sQuestionSimileText { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

