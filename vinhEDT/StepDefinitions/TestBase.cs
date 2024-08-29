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
    public class TestBase
    {
        public IBrowser _browser {  get; set; }
        public IPage _page {  get; set; }

    [SetUp]
        public async Task Setup()
        {
            var _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
            var _context = await _browser.NewContextAsync(new()
            {
                StorageStatePath = Path.Combine(Directory.GetCurrentDirectory(), "state.json")
            });

            _page = await _context.NewPageAsync();

        }
        [TearDown]

        public async Task TearDown()
        {
            await _page.CloseAsync();
            await _browser.CloseAsync();
        }
    }
}
