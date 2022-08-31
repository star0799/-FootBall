
using Ionic.Zip;
using Microsoft.Win32;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FootBall
{
    class UpdateChromDriver
    {
        static log _log = new log();
        public void UpdateChromDriverFun()
        {
            string ChromeDriverPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string ChromDriverVersion = GetChromDriverVersion(ChromeDriverPath);
            string ChromeWebVersion = GetWebChromeVersion();
            if (ChromDriverVersion != ChromeWebVersion)
            {
                var urlToDownload = GetURLToDownload(ChromeWebVersion);
                KillAllChromeDriverProcesses();
                DownloadNewVersionOfChrome(urlToDownload, ChromeDriverPath);
                string extract = ExtructZip(ChromeDriverPath);
            }
        }
        static string GetChromDriverVersion(string ChromeDriverePath)
        {
            string driverversion = "";
            try
            {
                if (File.Exists(ChromeDriverePath + "\\chromedriver.exe"))
                {
                    IWebDriver driver = new ChromeDriver(ChromeDriverePath);
                    ICapabilities capabilities = ((ChromeDriver)driver).Capabilities;
                    driverversion = ((capabilities.GetCapability("chrome") as Dictionary<string, object>)["chromedriverVersion"]).ToString().Split(' ').First();
                    driver.Dispose();
                }
                else
                {
                    Console.WriteLine("ChromeDriver.exe missing !!");
                }
            }
            catch (Exception ex)
            {
                _log.WriteLog(ex.ToString());
            }
            return driverversion;
        }
        static string GetChromeWebPath()
        {
            //Path originates from here: https://chromedriver.chromium.org/downloads/version-selection            
            using (RegistryKey key = Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\App Paths\\chrome.exe"))
            {
                if (key != null)
                {
                    Object o = key.GetValue("");
                    if (!String.IsNullOrEmpty(o.ToString()))
                    {
                        return o.ToString();
                    }
                    else
                    {
                        throw new ArgumentException("Unable to get version because chrome registry value was null");
                    }
                }
                else
                {
                    throw new ArgumentException("Unable to get version because chrome registry path was null");
                }
            }
        }
        static string GetWebChromeVersion()
        {
            string productVersionPath = GetChromeWebPath();
            if (String.IsNullOrEmpty(productVersionPath))
            {
                throw new ArgumentException("Unable to get version because path is empty");
            }

            if (!File.Exists(productVersionPath))
            {
                throw new FileNotFoundException("Unable to get version because path specifies a file that does not exists");
            }

            var versionInfo = FileVersionInfo.GetVersionInfo(productVersionPath);
            if (versionInfo != null && !String.IsNullOrEmpty(versionInfo.FileVersion))
            {
                return versionInfo.FileVersion;
            }
            else
            {
                throw new ArgumentException("Unable to get version from path because the version is either null or empty: " + productVersionPath);
            }
        }
        static string GetURLToDownload(string version)
        {
            if (String.IsNullOrEmpty(version))
            {
                throw new ArgumentException("Unable to get url because version is empty");
            }

            //URL's originates from here: https://chromedriver.chromium.org/downloads/version-selection
            string html = string.Empty;
            string urlToPathLocation = @"https://chromedriver.storage.googleapis.com/LATEST_RELEASE_" + String.Join(".", version.Split('.').Take(3));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlToPathLocation);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }

            if (String.IsNullOrEmpty(html))
            {
                throw new WebException("Unable to get version path from website");
            }

            return "https://chromedriver.storage.googleapis.com/" + html + "/chromedriver_win32.zip";
        }
        static void KillAllChromeDriverProcesses()
        {
            var processes = Process.GetProcessesByName("chromedriver");
            foreach (var process in processes)
            {
                try
                {
                    process.Kill();
                }
                catch
                {

                }
            }
        }
        static void DownloadNewVersionOfChrome(string urlToDownload, string ChromDriverPath)
        {
            if (String.IsNullOrEmpty(urlToDownload))
            {
                throw new ArgumentException("Unable to get url because urlToDownload is empty");
            }
            using (var client = new WebClient())
            {
                if (File.Exists(ChromDriverPath + "\\chromedriver.zip"))
                {
                    File.Delete(ChromDriverPath + "\\chromedriver.zip");
                }

                client.DownloadFile(urlToDownload, "chromedriver.zip");

                if (File.Exists(ChromDriverPath + "\\chromedriver.zip") && File.Exists(ChromDriverPath + "\\chromedriver.exe"))
                {
                    File.Delete(ChromDriverPath + "\\chromedriver.exe");
                }

            }
        }
        static string ExtructZip(string ChromeDriverPath)
        {
            string errorMsg = "";
            string zipFileName = "";
            try
            {
                zipFileName = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\chromedriver.zip";
                string orgFileName = "";
                using (ZipFile zip = ZipFile.Read(zipFileName))
                {
                    foreach (ZipEntry e in zip)
                    {
                        e.Extract(ChromeDriverPath, ExtractExistingFileAction.OverwriteSilently);
                        orgFileName = e.FileName;
                    }
                }
                File.Delete(zipFileName);
                errorMsg = "Done";
            }
            catch (Exception ex)
            {
                errorMsg = "ExtructZip  file " + zipFileName.Split('\\').Last() + " - " + ex.Message;
            }
            return errorMsg;
        }

    }
}
