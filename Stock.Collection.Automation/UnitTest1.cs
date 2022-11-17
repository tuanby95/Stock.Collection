using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using Stock.Collection.BussinessLogic.Services;
using Stock.Collection.DataAccess.Entities;
using Stock.Collection.DataAccess.Repository;
using Stock.Collection.DataAccess.Data;

namespace PlaywrightTests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    
    ICompanyService _companyService;
    ICompanyRepository _companyRepository;
    StockDbContext StockDbContext;

    [SetUp]
    public void Initialize()
    {
        _companyService = new CompanyService();
        _companyRepository = new CompanyRepository(StockDbContext);
    }

    [Test]
    public async Task OpenPageAndGetList()
    {
        await Page.GotoAsync("https://finance.vietstock.vn/doanh-nghiep-a-z?page=1");

        var companiesTableLocator = Page.Locator(selector: (".table-responsive"));
        var rows = companiesTableLocator.Locator("tr");
        var columns = rows.Locator("td");
        var count = await rows.CountAsync();
        List<Company> companiesList = new List<Company>();

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
            companiesList.Add(Company);
        }
        foreach (var company in companiesList)
        {
            Console.WriteLine(company.CompanyName);
        }
        _companyService.StoreCompanyData(companiesList);
    }
    public async Task OpenNextPage()
    {
        await Page.GotoAsync("https://finance.vietstock.vn/doanh-nghiep-a-z?page=1");
        var loginBtn = Page.Locator("text=Đăng nhập").First;
        await loginBtn.ClickAsync();
        var emailField = loginBtn.Locator("name=txtEmail").ScreenshotAsync(new() { Path = "hello.png" });
        var passwordField = Page.Locator("css=[placeholder='Mật khẩu']").ScreenshotAsync(new() { Path = "hehehehe.png" });
        //var pageArea = Page.Locator(selector: ".form-group");
        //var nextBtn = pageArea.Locator(selector: ".btn-group").First;
        //await nextBtn.ClickAsync();
    }
}