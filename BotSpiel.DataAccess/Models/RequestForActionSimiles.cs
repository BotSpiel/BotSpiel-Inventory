using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class RequestForActionSimiles : IRequestForActionSimiles
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
        public RequestForActionSimiles()
        {
		RequestsForAction _RequestsForAction = new RequestsForAction();
		RequestsForAction = _RequestsForAction;

        }
		[Display(Name = "Request For Action Simile ID")]
		public virtual Int64 ixRequestForActionSimile { get; set; }
		[Display(Name = "Request For Action Simile ID")]
		public virtual Int64 ixRequestForActionSimileEdit { get; set; }
		[Required]
		[StringLength(4000)]
		[Display(Name = "Request For Action Simile")]
		public virtual String sRequestForActionSimile { get; set; }
		[Required]
		[Display(Name = "Request For Action ID")]
		public virtual Int64 ixRequestForAction { get; set; }
		[Required]
		[Display(Name = "Request For Action Simile Text")]
		public virtual String sRequestForActionSimileText { get; set; }
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
		[ForeignKey("ixRequestForAction")]
		public virtual RequestsForAction RequestsForAction { get; set; }
    }
}
  

