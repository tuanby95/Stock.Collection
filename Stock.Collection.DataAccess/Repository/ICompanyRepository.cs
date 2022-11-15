using Stock.Collection.DataAccess.Entities;

namespace Stock.Collection.DataAccess.Repository
{
    public interface ICompanyRepository : IDisposable
    {
        IEnumerable<Company> GetCompanies();
        Company GetCompanyByID (int id);
        void InsertCompany (Company company);
        void DeleteCompany(int CompanyID);
        void UpdateCompany (Company company);
        void Save();
    }
}
