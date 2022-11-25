using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using Stock.Collection.BussinessLogic.Services;
using Stock.Collection.DataAccess.Entities;
using Stock.Collection.DataAccess.Repository;
using Stock.Collection.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
using Polly;
using Microsoft.Playwright;

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
        StockDbContext= new StockDbContext();
        _companyRepository = new CompanyRepository(StockDbContext);
    }


    public async Task OpenPage()
    {
        await Page.GotoAsync("https://finance.vietstock.vn/doanh-nghiep-a-z?page=1");
    }
    public async Task GetCompanyList()
    {
        var companiesTableLocator = Page.Locator(selector: (".table-responsive"));
        var rows = companiesTableLocator.Locator("tr");
        var columns = rows.Locator("td");
        var count = await rows.CountAsync();
        List<Company> companiesList = new List<Company>();
        var pageArea = Page.Locator(selector: ".form-group").Nth(3);
        var nextBtn = pageArea.Locator(selector: ".btn-group").First;

        var k = 161; //tổng số page
        do
        {
            await Page.WaitForSelectorAsync(selector: (".table-responsive"));
            for (int i = 0; i < count - 1; i++)
            {
                var Company = new Company();
                for (var j = 0; j < 6; j++)
                {
                    var colIndex = (6 * i) + j;
                    var columnValue = await columns.Nth(colIndex).TextContentAsync();
                    if (j == 1)
                    {
                        Company.StockCode = columnValue;
                    }
                    if (j == 2)
                    {
                        Company.CompanyName = columnValue;
                    }
                    if (j == 3)
                    {
                        Company.Industry = columnValue;
                    }
                    if (j == 4)
                    {
                        Company.StockExchange = columnValue;
                    }
                    if (j == 5)
                    {
                        Company.ListedStock = double.Parse(columnValue);
                        if (colIndex == 119)
                        {
                            colIndex = 0;
                        }
                    }
                }
                companiesList.Add(Company);
            }
            await nextBtn.ClickAsync();
            k--;
            Thread.Sleep(2000);
        } while (k > 0);
        try
        {
            using (var dbContext = new StockDbContext())
            {
                dbContext.Companies.AddRange(companiesList);
                dbContext.SaveChanges();
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
        //_companyService.StoreCompanyData(companiesList);

    }
    public async Task Login()
    {
        var loginBtn = Page.Locator("text=Đăng nhập").First;
        await loginBtn.ClickAsync();
        var emailField = Page.Locator("css=[id='txtEmailLogin']");
        await emailField.FillAsync("dmtuan1995@gmail.com");
        var passwordField = Page.Locator("css=[id='txtPassword']");
        await passwordField.FillAsync("a5562915");
        await Page.Keyboard.PressAsync("Enter");
    }
    public async Task GoToNextPage()
    {
        var i = 165;
        try
        {
            do
            {
                var pageArea = Page.Locator(selector: ".form-group").Nth(3);
                var nextBtn = pageArea.Locator(selector: ".btn-group").First;
                await nextBtn.ClickAsync();
                i--;
            } while (i > 0);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }


    }
    [Test]
    public async Task TestFlow()
    {
        await OpenPage();
        await Login();
        //await GoToNextPage();
        await GetCompanyList();
    }
}