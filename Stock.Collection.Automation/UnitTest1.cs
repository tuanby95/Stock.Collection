using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [Test]
    public async Task OpenPageAndGetList()
    {
        await Page.GotoAsync("https://finance.vietstock.vn/doanh-nghiep-a-z?page=1");

        var companiesTableLocator = Page.Locator(selector: (".table-responsive"));
        var rows = companiesTableLocator.Locator("tr");
        var count = await rows.CountAsync();
        for (int i = 0; i < count; i++)
        {
            Console.WriteLine(await rows.Nth(i).TextContentAsync());
        }
    }
}