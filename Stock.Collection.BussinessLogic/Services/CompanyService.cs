﻿using Microsoft.AspNetCore.Mvc;
using Stock.Collection.DataAccess.Data;
using Stock.Collection.DataAccess.Entities;
using Stock.Collection.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Collection.BussinessLogic.Services
{
    public class CompanyService
    {
        private readonly ICompanyRepository companyRepository;

        public CompanyService(ICompanyRepository companyRepository)
        {
            this.companyRepository = companyRepository;
        }

        public void StoreCompanyData(List<Company> companies)
        {
            foreach (Company company in companies)
            {
                companyRepository.InsertCompany(company);
            }
        }
        public void DeleteCompanyData(int CompanyID)
        {
            companyRepository.DeleteCompany(CompanyID);
        }
        public void UpdateCompanyData(List<Company> companies)
        {
            foreach (Company company in companies)
            {
                companyRepository.UpdateCompany(company);
            }
        }
        public void GetDetailCompanyData(int CompanyID)
        {
            companyRepository.GetCompanyByID(CompanyID);
        }
    }
}