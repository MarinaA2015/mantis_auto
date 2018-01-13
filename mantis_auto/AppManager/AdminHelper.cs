﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SimpleBrowser.WebDriver;
using System.Text.RegularExpressions;

namespace mantis_auto
{
    public class AdminHelper : HelperBase
    {
        private string baseUrl;

        public AdminHelper(ApplicationManager manager, String baseUrl) : base(manager)
        {
            this.baseUrl = baseUrl;
        }

        public List<AccountData> GetAllAccounts()
        {
            List<AccountData> accounts = new List<AccountData>();
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "/ manage_user_page.php";
            IList<IWebElement> rows = driver.FindElements(By.CssSelector(".table-hover td>a"));
            foreach(IWebElement el in rows)
            {
                IWebElement link = el.FindElement(By.TagName("a"));
                string name = link.Text;
                string href = link.GetAttribute("href");
                Match m = Regex.Match(href, @"\d+$");
                string id = m.Value;
                accounts.Add(new AccountData()
                {
                    Name = name,
                    Id = id
                });
            }
            return accounts;
        }

        public void DeleteAccount(AccountData account)
        {
            IWebDriver driver = OpenAppAndLogin();
            driver.Url = baseUrl + "/manage_user_edit_page.php?user_id=" + account.Id;
            driver.FindElement(By.CssSelector("input[value = 'Delete User']")).Click();
            driver.FindElement(By.CssSelector("input[value = 'Delete Account']")).Click();

        }

        private IWebDriver OpenAppAndLogin()
        {
            IWebDriver driver = new SimpleBrowserDriver();
            driver.Url = baseUrl + "/login_page.php";
            Console.Out.WriteLine("on the login page");
            System.Threading.Thread.Sleep(3000);
            driver.FindElement(By.Id("username")).SendKeys("administrator");
            //driver.FindElement(By.CssSelector("#username")).SendKeys("administrator");
            driver.FindElement(By.CssSelector("input[type = 'submit']")).Click();
            driver.FindElement(By.Id("Password")).SendKeys("root");
            driver.FindElement(By.CssSelector("input[type = 'submit']")).Click();
            return driver;

        }
    }
}
