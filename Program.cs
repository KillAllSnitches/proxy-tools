using System;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Threading;
using System.Windows.Forms;

namespace ProxyGenerator
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Console.Title = "Proxy Tools | Made by Kye | Tweaked by VX ";
            int selection = 0;
        start: Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("                                                   ~ Contributors ~");
            Console.WriteLine("                                               github.com/kyeondiscord");
            Console.WriteLine("                                                  github.com/vx-dev");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("");
            Console.WriteLine(" > Proxy Tools <");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("  1 = HTTP/S");
            Console.WriteLine("  2 = SOCKS4");
            Console.WriteLine("  3 = SOCKS5");
            Console.WriteLine("  4 = Checker");


            try
            {
                selection = int.Parse(Console.ReadLine());
            }
            catch
            {
                Console.Clear();
                goto start;
            }
            if (selection > 4)
            {
                Console.Clear();
                goto start;
            }
            string[] proxyurls;
           switch (selection)
            {
                case 1:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(" > Proxy Scraper <");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Getting HTTP/S Proxies!");
                    proxyurls = new string[6]{ "https://api.proxyscrape.com/?request=getproxies&proxytype=http&timeout=10000&country=all&ssl=all&anonymity=all", "https://raw.githubusercontent.com/clarketm/proxy-list/master/proxy-list-raw.txt", "https://raw.githubusercontent.com/TheSpeedX/PROXY-List/master/http.txt", "https://raw.githubusercontent.com/ShiftyTR/Proxy-List/master/http.txt", "https://raw.githubusercontent.com/roosterkid/openproxylist/main/HTTPS_RAW.txt", "https://raw.githubusercontent.com/Volodichev/proxy-list/main/http.txt" };
                    WriteProxies(proxyurls, "HTTP");
                    Console.Clear();
                    goto start;

                case 2:
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(" > Proxy Scraper <");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Getting SOCKS4 Proxies!");
                    proxyurls = new string[4]{ "https://api.proxyscrape.com/?request=getproxies&proxytype=socks4&timeout=10000&country=all", "https://raw.githubusercontent.com/TheSpeedX/PROXY-List/master/socks4.txt", "https://raw.githubusercontent.com/ShiftyTR/Proxy-List/master/socks4.txt", "https://raw.githubusercontent.com/roosterkid/openproxylist/main/SOCKS4_RAW.txt" };
                    WriteProxies(proxyurls, "Socks4");
                    Console.Clear();
                    goto start;

                case 3:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(" > Proxy Scraper <");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine("Getting SOCKS5 Proxies!");
                    proxyurls = new string[4]{ "https://api.proxyscrape.com/?request=getproxies&proxytype=socks5&timeout=10000&country=all", "https://raw.githubusercontent.com/TheSpeedX/PROXY-List/master/socks5.txt", "https://raw.githubusercontent.com/ShiftyTR/Proxy-List/master/socks5.txt", "https://raw.githubusercontent.com/roosterkid/openproxylist/main/SOCKS5_RAW.txt" };
                    WriteProxies(proxyurls, "Socks5");
                    Console.Clear();
                    goto start;

                case 4:
                    if (!File.Exists("good_proxies.txt"))
                    {
                        File.Create("good_proxies.txt").Close();
                    }
                    if (!File.Exists("bad_proxies.txt"))
                    {
                        File.Create("bad_proxies.txt").Close();
                    }
                    CheckProxies();
                    Console.Clear();
                    goto start;
            }
            }
        public static int good { get; set; }
        public static int bad { get; set; }
        public static void CheckProxies()
        {
           
            Console.WriteLine("Select a proxy .txt file");
            Thread.Sleep(300);
            OpenFileDialog o = new OpenFileDialog();
            o.Filter = "txt files (*.txt)|*.txt";
            o.Title = "Select a Proxy";
            o.ShowDialog();
            Console.Clear();
            string[] proxies = File.ReadAllLines(o.FileName);
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("> Proxy Checker <");
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Live: " + good);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Dead: " + bad);
            foreach (string proxy in proxies)
            {
                Thread.Sleep(50);
                if (ProxyCheck(proxy) == true)
                {
                    string append = File.ReadAllText("good_proxies.txt");
                    using (StreamWriter writer = new StreamWriter("good_proxies.txt"))
                    {
                        writer.WriteLine(append + proxy);
                    }
                    good++;
                }
                else
                {
                    string append = File.ReadAllText("bad_proxies.txt");
                    using (StreamWriter writer = new StreamWriter("bad_proxies.txt"))
                    {
                        writer.WriteLine(append + proxy);
                    }
                    bad++;
                }
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("> Proxy Checker <");
                Console.WriteLine(" ");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Live: " + good);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Dead: " + bad);
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("> Proxy Checker <");
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Results:");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Live:" + good);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Dead:" + bad);
            Console.ReadKey();
        }
        static void WriteProxies(string[] url, string type)
        {
            string filename = type + "_proxies.txt";
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            WebClient webClient = new WebClient();
            webClient.Timeout = 2500;
            string proxylist = "";
            string[] proxyarray = url;
            foreach (string proxyurl in proxyarray)
            {
                proxylist = proxylist + webClient.DownloadString(proxyurl) + Environment.NewLine;
            }
            using (StreamWriter writer = new StreamWriter(filename))
            {
                writer.Write(proxylist);
            }
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            int lineCount = File.ReadLines(filename).Count();
            Console.WriteLine("Exported " + lineCount + " proxies to: " + AppDomain.CurrentDomain.BaseDirectory + filename);
            Console.ReadKey();
        }
        public static bool ProxyCheck(string ipAddressport)
        {
            string[] data = ipAddressport.Split(':');
            int port = 0;
            try
            {
                port = int.Parse(data[1]);
            }
            catch
            {
                return false;
            }
            try
            {
                IWebProxy proxy = new WebProxy(data[0], port);
                WebClient wc = new WebClient();
                wc.Timeout = 2500;
                wc.Proxy = proxy;
                wc.Encoding = Encoding.UTF8;
                string result = wc.DownloadString("http://ip-api.com/line/?fields=8192");
                return true;
            }
            catch
            {
                return false;
            }
        }
        private class WebClient : System.Net.WebClient
        {
            public int Timeout { get; set; }
            protected override WebRequest GetWebRequest(Uri uri)
            {
                WebRequest lWebRequest = base.GetWebRequest(uri);
                lWebRequest.Timeout = Timeout;
                ((HttpWebRequest)lWebRequest).ReadWriteTimeout = Timeout;
                return lWebRequest;
            }
        }
    }
}
