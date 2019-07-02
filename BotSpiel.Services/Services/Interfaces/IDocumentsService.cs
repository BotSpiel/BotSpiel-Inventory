using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BotSpiel.DataAccess.Models;

namespace BotSpiel.Services
{

    public interface IDocumentsService
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

        Task<Int64> Create(DocumentsPost documentsPost);
        Task Edit(DocumentsPost documentsPost);
        Task Delete(DocumentsPost documentsPost);
    }
}
  

