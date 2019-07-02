using System;
using System.Collections.Generic;
using System.Linq;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.DataAccess.Repositories
{

    public interface IDocumentsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This interface ....

*/

        DocumentsPost GetPost(Int64 ixDocument);        
		Documents Get(Int64 ixDocument);
        IQueryable<Documents> Index();
       IQueryable<Statuses> selectStatuses();
        IQueryable<DocumentMessageTypes> selectDocumentMessageTypes();
        bool VerifyDocumentUnique(Int64 ixDocument, string sDocument);
        List<string> VerifyDocumentDeleteOK(Int64 ixDocument, string sDocument);
        void RegisterCreate(DocumentsPost documentsPost);
        void RegisterEdit(DocumentsPost documentsPost);
        void RegisterDelete(DocumentsPost documentsPost);
        void Rollback();
        void Commit();
    }
}
  

