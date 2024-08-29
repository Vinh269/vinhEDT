using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;
using static System.Net.Mime.MediaTypeNames;


namespace vinhEDT.StepDefinitions
{

    public class Test : TestBase
    {

        private string GenerateRandomName(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
            Random random = new Random();
            char[] name = new char[length];

            for (int i = 0; i < length; i++)
            {
                name[i] = chars[random.Next(chars.Length)];
            }

            return new string(name);
        }

        [Test]

        public async Task CreateANewCase()
        {
            //            _page.SetDefaultTimeout(60_000);
            string randomName = GenerateRandomName(8);
            string newCaseName = ("Case Name: " + randomName).ToString();

            await _page.GotoAsync("https://qa115.edtdev.blue");
            //await Assertions.Expect(_page.Locator(".v-icon.notranslate.material-icons.theme--light.primary--text")).ToBeVisibleAsync();
            //Console.WriteLine("Homepage is displayed successfully.");

            await _page.GetByLabel("Create New Case").ClickAsync();
            await Task.Delay(10000);
            await _page.GetByLabel("Name*").FillAsync(newCaseName);
            await _page.GetByRole(AriaRole.Dialog).Locator("i").Nth(1).ClickAsync();
            await _page.GetByText("(UTC-10:00) Hawaii").ClickAsync();
            //            await _page.GetByLabel("Description", new() { Exact = true }).FillAsync("automation test");
            await _page.GetByRole(AriaRole.Button, new() { Name = "Save" }).ClickAsync();
            await Task.Delay(30000);
            //            await _page.GetByLabel("Search").
            // Adjust to target the input field specifically
                       await _page.GetByRole(AriaRole.Textbox, new() { Name = "Search" }).FillAsync(newCaseName);
                       await _page.GetByRole(AriaRole.Button, new() { Name = "Search" }).ClickAsync();

            Console.WriteLine("Case:" + newCaseName + "is created success");

        }


    }
}
