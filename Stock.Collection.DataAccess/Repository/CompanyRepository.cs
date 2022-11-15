using Stock.Collection.DataAccess.Entities;
using Stock.Collection.DataAccess.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Company> GetCompanies()
        {
            throw new NotImplementedException();
        }

        public Company GetCompanyByID(int id)
        {
            throw new NotImplementedException();
        }

        public void InsertCompany(Company company)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void UpdateCompany(Company company)
        {
            throw new NotImplementedException();
        }
    }
}
