using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;
using System.Linq.Expressions;
using static System.Net.Mime.MediaTypeNames;

namespace TestProject1
{
    public class Tests
    {
        private const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        protected WindowsDriver<WindowsElement>? NotepadSession;


        [SetUp]
        public void Setup()
        {
            Process.Start(new ProcessStartInfo { FileName = @"C:Program Files\Windows Application Driver\WinAppDriver.exe", UseShellExecute = true });

            if (NotepadSession == null) { 

                AppiumOptions appiumOptions = new AppiumOptions();
                appiumOptions.AddAdditionalCapability("app", @"C:\Windows\System32\notepad.exe");
                

                NotepadSession = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), appiumOptions);

            }
        }

        [TearDown]
        public void TearDown()
        {
           if(NotepadSession != null) 
           { 
                
                NotepadSession.Quit();
                NotepadSession.Dispose();
                NotepadSession = null;
           }

            
        }

        [Test]
        public void textType()

        {
            Console.WriteLine("Escrever texto no bloco de notas");

            NotepadSession.FindElementByClassName("RichEditD2DPT").SendKeys("Minha primeira automação");
            Assert.AreEqual(NotepadSession.FindElementByClassName("RichEditD2DPT").Text, "Minha primeira automação");
          

        }


        [Test]
        public void SavetextType()

        {
            Console.WriteLine("Salvar texto escrito");

            NotepadSession.FindElementByClassName("RichEditD2DPT").SendKeys(Keys.Control + "s");
            Thread.Sleep(TimeSpan.FromSeconds(1));
            Assert.IsTrue(NotepadSession.FindElementByName("Salvar como").Text.Contains("Salvar como"));
            NotepadSession.FindElementByName("Downloads (fixo)").Click();
            Assert.IsTrue(NotepadSession.FindElementByName("Downloads (fixo)").Text.Contains("Downloads"));
            Assert.AreEqual(NotepadSession.FindElementByName("Salvar").Text, "Salvar");
            NotepadSession.FindElementByName("Salvar").Click();



        }
    }
}