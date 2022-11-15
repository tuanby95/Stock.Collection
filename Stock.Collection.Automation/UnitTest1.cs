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
    private ICompanyService _companyService;
    
    [Test]
    public async Task OpenPageAndGetList()
    {
        await Page.GotoAsync("https://finance.vietstock.vn/doanh-nghiep-a-z?page=1");

        var companiesTableLocator = Page.Locator(selector: (".table-responsive"));
        var rows = companiesTableLocator.Locator("tr");
        var columns = rows.Locator("td");
        var count = await rows.CountAsync();
        var countCL = await columns.CountAsync();
        for (int i = 1; i < count; i++)
        {
            var Company = new Company();
            //var row = await rows.Nth(i).TextContentAsync();
            for (int j = 0; j < countCL; j++)
            {
                var column = await columns.Nth(j).TextContentAsync();
                //Company.Id = await columns.Nth(j).TextContentAsync();
                if(j == 0)
                {
                    Company.Id = int.Parse(column);
                    Console.WriteLine(Company.Id);
                }
                if(j == 1)
                {
                    Company.StockCode = column;
                    Console.WriteLine(Company.StockCode);
                }
                if(j == 2)
                {
                    Company.CompanyName = column;
                    Console.WriteLine(Company.CompanyName);
                 }
                if(j == 3)
                {
                    Company.Industry = column;
                    Console.WriteLine(Company.Industry);
                }
                if(j == 4) 
                {
                    Company.StockExchange = column;
                    Console.WriteLine(Company.StockExchange);
                }
                if(j == 5)
                {
                    Company.ListedStock = double.Parse(column);
                    Console.WriteLine(Company.ListedStock);
                }
            }
            Console.WriteLine(Company);
        }
    }
}