using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using Stock.Collection.BussinessLogic.Services;

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
        var count = await rows.CountAsync();
        for (int i = 1; i < count; i++)
        {
            string row = await rows.Nth(i).TextContentAsync();
            Console.WriteLine(row);
        }
    }
}