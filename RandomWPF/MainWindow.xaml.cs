using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Net;

namespace RandomWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

    private void button1_Click(object sender, RoutedEventArgs e)
    {
        int min;
        bool op1ok = int.TryParse(txt_RMin.Text,out min);

        int max;
        bool op2ok = int.TryParse(txt_RMax.Text,out max);

        if (op1ok == false || op2ok == false )
            throw new ArgumentException("wrong numbers");

        int hownumbers = (int)Slider.Value;

        string url = @"http://www.random.org/integers/?num="+ 
            hownumbers.ToString() +
            "&min=" + min.ToString() + 
            "&max=" + max.ToString() +
            "&col=1&base=10&format=plain&rnd=new";
        try
        {
            string data = HtmlDownload.HtmlToString(url);

            string[] dataArray = data.Split('\n');

            foreach (var item in dataArray)
            {
                listBox1.Items.Add(item);
            }

        }
        //błedy 503
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    private void button2_Click(object sender, RoutedEventArgs e)
    {
        string url = "http://www.random.org/quota/?format=plain";
        try 
	    {	        
		    string data = HtmlDownload.HtmlToString(url);
            MessageBox.Show(data);
	    }
	    catch (Exception ex )
	    {		
		     MessageBox.Show(ex.Message);
	    }
    }
    }

    /// <summary>
    /// Methods for downloading a web-page from the internet.
    /// </summary>
    static class HtmlDownload
    {
        /// <summary>
        /// Download webpage from given url-address to a StreamReader object.
        /// </summary>
        /// <param name="url">Internet address of web-page to download.</param>
        /// <returns>StreamReader-object with web-page contents.</returns>
        public static StreamReader HtmlToStream(string url)
        {
            WebRequest webRequest = WebRequest.Create(url);
            WebResponse webResponse = webRequest.GetResponse();           
            StreamReader streamReader = new StreamReader(webResponse.GetResponseStream());

            return streamReader;
        }

        /// <summary>
        /// Download webpage from given url-address to a string.
        /// </summary>
        /// <param name="url">Internet address of web-page to download.</param>
        /// <returns>String with web-page contents.</returns>
        public static string HtmlToString(string url)
        {
            return HtmlToStream(url).ReadToEnd();
        }
    }
}
