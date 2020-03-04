using System;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{
    [Serializable]
    public class TopicsPost : ITopicsPost
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
 
		[Display(Name = "Topic ID")]
		public virtual Int64 ixTopic { get; set; }
		[Required]
		[StringLength(4000)]
		[Display(Name = "Topic")]
		public virtual String sTopic { get; set; }
		public virtual String UserName { get; set; }
    }
}
  

