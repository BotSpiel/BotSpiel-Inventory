using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BotSpiel.DataAccess.Models
{

    public class InboundModuleGridsanalytics 
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/

		[Display(Name = "Inbound Module Grid ID")]
		public virtual Int64 ixInboundModuleGrid { get; set; }
		[Display(Name = "Inbound Module Grid ID")]
		public virtual Int64 ixInboundModuleGridCreate { get; set; }
		[Display(Name = "Inbound Module Grid")]
		public virtual String sInboundModuleGrid { get; set; }
		[Display(Name = "Short Description")]
		public virtual String sShortDescription { get; set; }
		[Display(Name = "Data Entity Type")]
		public virtual String sDataEntityType { get; set; }
		[Display(Name = "Can Create")]
		public virtual Boolean bCanCreate { get; set; }
		[Display(Name = "Can Edit")]
		public virtual Boolean bCanEdit { get; set; }
		[Display(Name = "Can Delete")]
		public virtual Boolean bCanDelete { get; set; }
    }
}
  

