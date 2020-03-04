using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class QuestionSimiles : IQuestionSimiles
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public QuestionSimiles()
        {
		Questions _Questions = new Questions();
		Questions = _Questions;

        }
		[Display(Name = "Question Simile ID")]
		public virtual Int64 ixQuestionSimile { get; set; }
		[Display(Name = "Question Simile ID")]
		public virtual Int64 ixQuestionSimileEdit { get; set; }
		[Display(Name = "Question Simile")]
		public virtual String sQuestionSimile { get; set; }
		[Required]
		[Display(Name = "Question ID")]
		public virtual Int64 ixQuestion { get; set; }
		[Required]
		[Display(Name = "Question Simile Text")]
		public virtual String sQuestionSimileText { get; set; }
		[Required]
		[Display(Name = "Created At")]
		public virtual DateTime dtCreatedAt { get; set; }
		[Required]
		[Display(Name = "Changed At")]
		public virtual DateTime dtChangedAt { get; set; }
		[StringLength(256)]
		[Required]
		[Display(Name = "Created By")]
		public virtual String sCreatedBy { get; set; }
		[StringLength(256)]
		[Required]
		[Display(Name = "Changed By")]
		public virtual String sChangedBy { get; set; }
		[ForeignKey("ixQuestion")]
		public virtual Questions Questions { get; set; }
    }
}
  

