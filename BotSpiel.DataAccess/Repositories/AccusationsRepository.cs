using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class AccusationsRepository : IAccusationsRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly AccusationsDB _context;
  
        public AccusationsRepository(AccusationsDB context)
        {
            _context = context;
  
        }

        public AccusationsPost GetPost(Int64 ixAccusation) => _context.AccusationsPost.AsNoTracking().Where(x => x.ixAccusation == ixAccusation).First();
         
		public Accusations Get(Int64 ixAccusation)
        {
            Accusations accusations = _context.Accusations.AsNoTracking().Where(x => x.ixAccusation == ixAccusation).First();
            accusations.Languages = _context.Languages.Find(accusations.ixLanguage);
            accusations.LanguageStyles = _context.LanguageStyles.Find(accusations.ixLanguageStyle);
            accusations.ResponseTypes = _context.ResponseTypes.Find(accusations.ixResponseType);

            return accusations;
        }

        public IQueryable<Accusations> Index()
        {
            var accusations = _context.Accusations.Include(a => a.Languages).Include(a => a.LanguageStyles).Include(a => a.ResponseTypes).AsNoTracking(); 
            return accusations;
        }

        public IQueryable<Accusations> IndexDb()
        {
            var accusations = _context.Accusations.Include(a => a.Languages).Include(a => a.LanguageStyles).Include(a => a.ResponseTypes).AsNoTracking(); 
            return accusations;
        }
       public IQueryable<Languages> selectLanguages()
        {
            List<Languages> languages = new List<Languages>();
            _context.Languages.AsNoTracking()
                .ToList()
                .ForEach(x => languages.Add(x));
            return languages.AsQueryable();
        }
        public IQueryable<LanguageStyles> selectLanguageStyles()
        {
            List<LanguageStyles> languagestyles = new List<LanguageStyles>();
            _context.LanguageStyles.AsNoTracking()
                .ToList()
                .ForEach(x => languagestyles.Add(x));
            return languagestyles.AsQueryable();
        }
        public IQueryable<ResponseTypes> selectResponseTypes()
        {
            List<ResponseTypes> responsetypes = new List<ResponseTypes>();
            _context.ResponseTypes.AsNoTracking()
                .ToList()
                .ForEach(x => responsetypes.Add(x));
            return responsetypes.AsQueryable();
        }
       public IQueryable<Languages> LanguagesDb()
        {
            List<Languages> languages = new List<Languages>();
            _context.Languages.AsNoTracking()
                .ToList()
                .ForEach(x => languages.Add(x));
            return languages.AsQueryable();
        }
        public IQueryable<LanguageStyles> LanguageStylesDb()
        {
            List<LanguageStyles> languagestyles = new List<LanguageStyles>();
            _context.LanguageStyles.AsNoTracking()
                .ToList()
                .ForEach(x => languagestyles.Add(x));
            return languagestyles.AsQueryable();
        }
        public IQueryable<ResponseTypes> ResponseTypesDb()
        {
            List<ResponseTypes> responsetypes = new List<ResponseTypes>();
            _context.ResponseTypes.AsNoTracking()
                .ToList()
                .ForEach(x => responsetypes.Add(x));
            return responsetypes.AsQueryable();
        }
        public List<string> VerifyAccusationDeleteOK(Int64 ixAccusation, string sAccusation)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(AccusationsPost accusationsPost)
		{
            _context.AccusationsPost.Add(accusationsPost); 
        }

        public void RegisterEdit(AccusationsPost accusationsPost)
        {
            _context.Entry(accusationsPost).State = EntityState.Modified;
        }

        public void RegisterDelete(AccusationsPost accusationsPost)
        {
            _context.AccusationsPost.Remove(accusationsPost);
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
  

