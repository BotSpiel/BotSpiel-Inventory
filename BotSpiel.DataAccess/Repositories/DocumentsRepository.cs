using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class DocumentsRepository : IDocumentsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly DocumentsDB _context;
  
        public DocumentsRepository(DocumentsDB context)
        {
            _context = context;
  
        }

        public DocumentsPost GetPost(Int64 ixDocument) => _context.DocumentsPost.AsNoTracking().Where(x => x.ixDocument == ixDocument).First();
         
		public Documents Get(Int64 ixDocument)
        {
            Documents documents = _context.Documents.AsNoTracking().Where(x => x.ixDocument == ixDocument).First();
            documents.DocumentMessageTypes = _context.DocumentMessageTypes.Find(documents.ixDocumentMessageType);
            documents.Statuses = _context.Statuses.Find(documents.ixStatus);

            return documents;
        }

        public IQueryable<Documents> Index()
        {
            var documents = _context.Documents.Include(a => a.DocumentMessageTypes).Include(a => a.Statuses).AsNoTracking(); 
            return documents;
        }
       public IQueryable<DocumentMessageTypes> selectDocumentMessageTypes()
        {
            List<DocumentMessageTypes> documentmessagetypes = new List<DocumentMessageTypes>();
            _context.DocumentMessageTypes.AsNoTracking()
                .ToList()
                .ForEach(x => documentmessagetypes.Add(x));
            return documentmessagetypes.AsQueryable();
        }
        public IQueryable<Statuses> selectStatuses()
        {
            List<Statuses> statuses = new List<Statuses>();
            _context.Statuses.AsNoTracking()
                .ToList()
                .ForEach(x => statuses.Add(x));
            return statuses.AsQueryable();
        }
        public bool VerifyDocumentUnique(Int64 ixDocument, string sDocument)
        {
            if (_context.Documents.AsNoTracking().Where(x => x.sDocument == sDocument).Any() && ixDocument == 0L) return false;
            else if (_context.Documents.AsNoTracking().Where(x => x.sDocument == sDocument && x.ixDocument != ixDocument).Any() && ixDocument != 0L) return false;
            else return true;
        }

        public List<string> VerifyDocumentDeleteOK(Int64 ixDocument, string sDocument)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(DocumentsPost documentsPost)
		{
            _context.DocumentsPost.Add(documentsPost); 
        }

        public void RegisterEdit(DocumentsPost documentsPost)
        {
            _context.Entry(documentsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(DocumentsPost documentsPost)
        {
            _context.DocumentsPost.Remove(documentsPost);
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
  

