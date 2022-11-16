using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using Stock.Collection.BussinessLogic.Services;
using Stock.Collection.DataAccess.Entities;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    public readonly ICompanyService _companyService;

    [Test]
    public async Task OpenPageAndGetList()
    {
        await Page.GotoAsync("https://finance.vietstock.vn/doanh-nghiep-a-z?page=1");

        var companiesTableLocator = Page.Locator(selector: (".table-responsive"));
        var rows = companiesTableLocator.Locator("tr");
        var columns = rows.Locator("td");
        var count = await rows.CountAsync();
        List<Company> companies = new List<Company>();

        for (int i = 0; i < count - 1; i++)
        {
            var Company = new Company();
            for (var j = 0; j < 6; j++)
            {
                var colIndex = (6 * i) + j;
                var column = await columns.Nth(colIndex).TextContentAsync();
                if (j == 0)
                {
                    Company.Id = int.Parse(column);
                }
                if (j == 1)
                {
                    Company.StockCode = column;
                }
                if (j == 2)
                {
                    Company.CompanyName = column;
                }
                if (j == 3)
                {
                    Company.Industry = column;
                }
                if (j == 4)
                {
                    Company.StockExchange = column;
                }
                if (j == 5)
                {
                    Company.ListedStock = double.Parse(column);
                }
            }
            companies.Add(Company);
        }
        foreach (var company in companies)
        {
            Console.WriteLine(company.CompanyName);
        }
        _companyService.StoreCompanyData(companies);
    }
}