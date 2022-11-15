using Stock.Collection.DataAccess.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Collection.BussinessLogic.Services
{
    public interface ICompanyService
    {
        void StoreCompanyData(List<Company> companies);
        void DeleteCompanyData(int CompanyID);
        void UpdateCompanyData(List<Company> companies);
        void GetDetailCompanyData(int CompanyID);
    }
}
