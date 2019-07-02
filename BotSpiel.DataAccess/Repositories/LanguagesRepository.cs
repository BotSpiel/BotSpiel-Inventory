using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class LanguagesRepository : ILanguagesRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly LanguagesDB _context;
       private readonly PeopleDB _contextPeople;
  
        public LanguagesRepository(LanguagesDB context, PeopleDB contextPeople)
        {
            _context = context;
           _contextPeople = contextPeople;
  
        }

        public LanguagesPost GetPost(Int64 ixLanguage) => _context.LanguagesPost.AsNoTracking().Where(x => x.ixLanguage == ixLanguage).First();
         
		public Languages Get(Int64 ixLanguage)
        {
            Languages languages = _context.Languages.AsNoTracking().Where(x => x.ixLanguage == ixLanguage).First();
            return languages;
        }

        public IQueryable<Languages> Index()
        {
            var languages = _context.Languages.AsNoTracking(); 
            return languages;
        }
        public bool VerifyLanguageUnique(Int64 ixLanguage, string sLanguage)
        {
            if (_context.Languages.AsNoTracking().Where(x => x.sLanguage == sLanguage).Any() && ixLanguage == 0L) return false;
            else if (_context.Languages.AsNoTracking().Where(x => x.sLanguage == sLanguage && x.ixLanguage != ixLanguage).Any() && ixLanguage != 0L) return false;
            else return true;
        }

        public List<string> VerifyLanguageDeleteOK(Int64 ixLanguage, string sLanguage)
        {
            List<string> existInEntities = new List<string>();
           if (_contextPeople.People.AsNoTracking().Where(x => x.ixLanguage == ixLanguage).Any()) existInEntities.Add("People");

            return existInEntities;
        }


        public void RegisterCreate(LanguagesPost languagesPost)
		{
            _context.LanguagesPost.Add(languagesPost); 
        }

        public void RegisterEdit(LanguagesPost languagesPost)
        {
            _context.Entry(languagesPost).State = EntityState.Modified;
        }

        public void RegisterDelete(LanguagesPost languagesPost)
        {
            _context.LanguagesPost.Remove(languagesPost);
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
  

