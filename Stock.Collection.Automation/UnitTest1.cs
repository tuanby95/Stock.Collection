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
            }
        }
    }
}