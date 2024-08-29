using System;
using System.IO;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using Microsoft.Playwright;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;
using System;using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace SpecFlowProject1.StepDefinitions
{
    [Binding]
    public class LoginStepDefinitions
    {
        private IPage _page;
        private IBrowserContext _context;
        private IBrowser _browser;
        private const string LoginUrl = "https://qa115.edtdev.blue";
        private const string Username = "vinh";
        private const string Password = "Vi!123456";

        [BeforeScenario]
        public async Task Setup()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            _context = await _browser.NewContextAsync(new BrowserNewContextOptions());
            _page = await _context.NewPageAsync();
        }

        [Given(@"I navigate to the login page")]
        public async Task GivenINavigateToTheLoginPage()
        {
            await _page.GotoAsync(LoginUrl);
        }

        [Given(@"I enter username in the Username")]
        public async Task GivenIEnterAValidUsername()
        {
            await _page.GetByLabel("User Name").FillAsync(Username);
        }

        [Given(@"I enter password in the Password")]
        public async Task GivenIEnterAValidPassword()
        {
            await _page.GetByLabel("Password").FillAsync(Password);
        }
        [When(@"I click on the Login button")]
        public void WhenIClickOnTheLoginButton()
        {
            _page.GetByRole(AriaRole.Button, new() { Name = "Log In" }).ClickAsync();
        }

        [Then(@"I can see Homepage is displayed")]
        public async Task ThenICanSeeHomepageIsDisplayed()
        {
            var locator = _page.Locator(".v-icon.notranslate.material-icons.theme--light.primary--text");
            await locator.WaitForAsync();
            if (!await locator.IsVisibleAsync())
            {
                throw new Exception("Homepage is not displayed.");
            }

            Console.WriteLine("Homepage is displayed successfully.");
        }
//        public async Task SaveAuthenticationState(IPage page)
//        {
//            string path = Path.GetFullPath("../../../playwright/.auth/state.json");
//            Console.WriteLine($"Saving state to: {path}");
//            await page.Context.StorageStateAsync(new BrowserContextStorageStateOptions
//            {
//                Path = path
 //           });
 //       }


        [AfterScenario]
        public async Task Teardown()
        {
            await _browser.CloseAsync();
        }
    }
}
