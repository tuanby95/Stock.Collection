using Stock.Collection.DataAccess.Entities;
using Stock.Collection.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Stock.Collection.DataAccess.Repository
{
    public class CompanyRepository : ICompanyRepository, IDisposable
    {
        private StockDbContext context;
        public CompanyRepository(StockDbContext context)
        {
            this.context = context;
        }

        public void DeleteCompany(int CompanyID)
        {
            Company company = context.Companies.Find(CompanyID);
            context.Companies.Remove(company);
            context.SaveChanges();
        }
        public IEnumerable<Company> GetCompanies()
        {
            return context.Companies.ToList();
        }

        public Company GetCompanyByID(int id)
        {
            return context.Companies.Find(id);
        }

        public void InsertCompany(Company company)
        {
            context.Companies.Add(company);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void UpdateCompany(Company company)
        {
            context.Entry(company).State = EntityState.Modified;
            context.SaveChanges();
        }
        private bool _disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            _disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
