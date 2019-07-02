using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class DocumentMessageTypesRepository : IDocumentMessageTypesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly DocumentMessageTypesDB _context;
       private readonly DocumentsDB _contextDocuments;
  
        public DocumentMessageTypesRepository(DocumentMessageTypesDB context, DocumentsDB contextDocuments)
        {
            _context = context;
           _contextDocuments = contextDocuments;
  
        }

        public DocumentMessageTypesPost GetPost(Int64 ixDocumentMessageType) => _context.DocumentMessageTypesPost.AsNoTracking().Where(x => x.ixDocumentMessageType == ixDocumentMessageType).First();
         
		public DocumentMessageTypes Get(Int64 ixDocumentMessageType)
        {
            DocumentMessageTypes documentmessagetypes = _context.DocumentMessageTypes.AsNoTracking().Where(x => x.ixDocumentMessageType == ixDocumentMessageType).First();
            return documentmessagetypes;
        }

        public IQueryable<DocumentMessageTypes> Index()
        {
            var documentmessagetypes = _context.DocumentMessageTypes.AsNoTracking(); 
            return documentmessagetypes;
        }
        public bool VerifyDocumentMessageTypeUnique(Int64 ixDocumentMessageType, string sDocumentMessageType)
        {
            if (_context.DocumentMessageTypes.AsNoTracking().Where(x => x.sDocumentMessageType == sDocumentMessageType).Any() && ixDocumentMessageType == 0L) return false;
            else if (_context.DocumentMessageTypes.AsNoTracking().Where(x => x.sDocumentMessageType == sDocumentMessageType && x.ixDocumentMessageType != ixDocumentMessageType).Any() && ixDocumentMessageType != 0L) return false;
            else return true;
        }

        public List<string> VerifyDocumentMessageTypeDeleteOK(Int64 ixDocumentMessageType, string sDocumentMessageType)
        {
            List<string> existInEntities = new List<string>();
           if (_contextDocuments.Documents.AsNoTracking().Where(x => x.ixDocumentMessageType == ixDocumentMessageType).Any()) existInEntities.Add("Documents");

            return existInEntities;
        }


        public void RegisterCreate(DocumentMessageTypesPost documentmessagetypesPost)
		{
            _context.DocumentMessageTypesPost.Add(documentmessagetypesPost); 
        }

        public void RegisterEdit(DocumentMessageTypesPost documentmessagetypesPost)
        {
            _context.Entry(documentmessagetypesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(DocumentMessageTypesPost documentmessagetypesPost)
        {
            _context.DocumentMessageTypesPost.Remove(documentmessagetypesPost);
        }

        public void Rollback()
        {
            _context.Rollback();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

    }
}
  

