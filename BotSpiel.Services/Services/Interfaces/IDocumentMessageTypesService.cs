using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IDocumentMessageTypesService
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        DocumentMessageTypesPost GetPost(Int64 ixDocumentMessageType);        
		DocumentMessageTypes Get(Int64 ixDocumentMessageType);
        IQueryable<DocumentMessageTypes> Index();
        bool VerifyDocumentMessageTypeUnique(Int64 ixDocumentMessageType, string sDocumentMessageType);
        List<string> VerifyDocumentMessageTypeDeleteOK(Int64 ixDocumentMessageType, string sDocumentMessageType);

        Task<Int64> Create(DocumentMessageTypesPost documentmessagetypesPost);
        Task Edit(DocumentMessageTypesPost documentmessagetypesPost);
        Task Delete(DocumentMessageTypesPost documentmessagetypesPost);
    }
}
  

