using Microsoft.Playwright;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace vinhEDT.StepDefinitions
{
    [SetUpFixture]
    public class LoginFixture
    {
        private static IPlaywright _playwright;
        private static IBrowser _browser;
        private IPage _page;
        private IBrowserContext _context;
        private const string LoginUrl = "https://qa115.edtdev.blue";
        private const string Username = "vinh";
        private const string Password = "Vi!123456";

        [OneTimeSetUp]
        public async Task Login()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            _context = await _browser.NewContextAsync(new BrowserNewContextOptions());
            _page = await _context.NewPageAsync();

            await _page.GotoAsync(LoginUrl);
            await _page.GetByLabel("User Name").FillAsync(Username);
            await _page.GetByLabel("Password").FillAsync(Password);
            await _page.GetByRole(AriaRole.Button, new() { Name = "Log In" }).ClickAsync();

            await _context.StorageStateAsync(new BrowserContextStorageStateOptions
            {
                Path = Path.Combine(Directory.GetCurrentDirectory(), "state.json")
            });

            await _page.CloseAsync();
            await _browser.CloseAsync();

        }
    }
}
