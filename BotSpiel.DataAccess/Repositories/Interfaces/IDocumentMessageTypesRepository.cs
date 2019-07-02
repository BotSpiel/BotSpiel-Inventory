using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IDocumentMessageTypesRepository
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
        void RegisterCreate(DocumentMessageTypesPost documentmessagetypesPost);
        void RegisterEdit(DocumentMessageTypesPost documentmessagetypesPost);
        void RegisterDelete(DocumentMessageTypesPost documentmessagetypesPost);
        void Rollback();
        void Commit();
    }
}
  

