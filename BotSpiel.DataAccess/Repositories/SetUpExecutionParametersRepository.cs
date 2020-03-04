using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BotSpiel.DataAccess.Models;
using BotSpiel.DataAccess.Data;

namespace BotSpiel.DataAccess.Repositories
{

    public class SetUpExecutionParametersRepository : ISetUpExecutionParametersRepository
    {

/*
-- =============================================
-- Author:		<BotSpiel>

-- Description:	<Description>

This class ....

*/
        private readonly SetUpExecutionParametersDB _context;
  
        public SetUpExecutionParametersRepository(SetUpExecutionParametersDB context)
        {
            _context = context;
  
        }

        public SetUpExecutionParametersPost GetPost(Int64 ixSetUpExecutionParameter) => _context.SetUpExecutionParametersPost.AsNoTracking().Where(x => x.ixSetUpExecutionParameter == ixSetUpExecutionParameter).First();
         
		public SetUpExecutionParameters Get(Int64 ixSetUpExecutionParameter)
        {
            SetUpExecutionParameters setupexecutionparameters = _context.SetUpExecutionParameters.AsNoTracking().Where(x => x.ixSetUpExecutionParameter == ixSetUpExecutionParameter).First();
            setupexecutionparameters.Companies = _context.Companies.Find(setupexecutionparameters.ixCompany);
            setupexecutionparameters.Facilities = _context.Facilities.Find(setupexecutionparameters.ixFacility);
            setupexecutionparameters.FacilityWorkAreas = _context.FacilityWorkAreas.Find(setupexecutionparameters.ixFacilityWorkArea);

            return setupexecutionparameters;
        }

        public IQueryable<SetUpExecutionParameters> Index()
        {
            var setupexecutionparameters = _context.SetUpExecutionParameters.Include(a => a.Facilities).Include(a => a.Companies).Include(a => a.FacilityWorkAreas).AsNoTracking(); 
            return setupexecutionparameters;
        }

        public IQueryable<SetUpExecutionParameters> IndexDb()
        {
            var setupexecutionparameters = _context.SetUpExecutionParameters.Include(a => a.Facilities).Include(a => a.Companies).Include(a => a.FacilityWorkAreas).AsNoTracking(); 
            return setupexecutionparameters;
        }
       public IQueryable<Companies> selectCompanies()
        {
            List<Companies> companies = new List<Companies>();
            _context.Companies.AsNoTracking()
                .ToList()
                .ForEach(x => companies.Add(x));
            return companies.AsQueryable();
        }
        public IQueryable<Facilities> selectFacilities()
        {
            List<Facilities> facilities = new List<Facilities>();
            _context.Facilities.Include(a => a.Addresses).AsNoTracking()
                .ToList()
                .ForEach(x => facilities.Add(x));
            return facilities.AsQueryable();
        }
        public IQueryable<FacilityWorkAreas> selectFacilityWorkAreas()
        {
            List<FacilityWorkAreas> facilityworkareas = new List<FacilityWorkAreas>();
            _context.FacilityWorkAreas.AsNoTracking()
                .ToList()
                .ForEach(x => facilityworkareas.Add(x));
            return facilityworkareas.AsQueryable();
        }
       public IQueryable<Companies> CompaniesDb()
        {
            List<Companies> companies = new List<Companies>();
            _context.Companies.AsNoTracking()
                .ToList()
                .ForEach(x => companies.Add(x));
            return companies.AsQueryable();
        }
        public IQueryable<Facilities> FacilitiesDb()
        {
            List<Facilities> facilities = new List<Facilities>();
            _context.Facilities.Include(a => a.Addresses).AsNoTracking()
                .ToList()
                .ForEach(x => facilities.Add(x));
            return facilities.AsQueryable();
        }
        public IQueryable<FacilityWorkAreas> FacilityWorkAreasDb()
        {
            List<FacilityWorkAreas> facilityworkareas = new List<FacilityWorkAreas>();
            _context.FacilityWorkAreas.AsNoTracking()
                .ToList()
                .ForEach(x => facilityworkareas.Add(x));
            return facilityworkareas.AsQueryable();
        }
        public bool VerifySetUpExecutionParameterUnique(Int64 ixSetUpExecutionParameter, string sSetUpExecutionParameter)
        {
            if (_context.SetUpExecutionParameters.AsNoTracking().Where(x => x.sSetUpExecutionParameter == sSetUpExecutionParameter).Any() && ixSetUpExecutionParameter == 0L) return false;
            else if (_context.SetUpExecutionParameters.AsNoTracking().Where(x => x.sSetUpExecutionParameter == sSetUpExecutionParameter && x.ixSetUpExecutionParameter != ixSetUpExecutionParameter).Any() && ixSetUpExecutionParameter != 0L) return false;
            else return true;
        }

        public List<string> VerifySetUpExecutionParameterDeleteOK(Int64 ixSetUpExecutionParameter, string sSetUpExecutionParameter)
        {
            List<string> existInEntities = new List<string>();

            return existInEntities;
        }


        public void RegisterCreate(SetUpExecutionParametersPost setupexecutionparametersPost)
		{
            _context.SetUpExecutionParametersPost.Add(setupexecutionparametersPost); 
        }

        public void RegisterEdit(SetUpExecutionParametersPost setupexecutionparametersPost)
        {
            _context.Entry(setupexecutionparametersPost).State = EntityState.Modified;
        }

        public void RegisterDelete(SetUpExecutionParametersPost setupexecutionparametersPost)
        {
            _context.SetUpExecutionParametersPost.Remove(setupexecutionparametersPost);
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
  

