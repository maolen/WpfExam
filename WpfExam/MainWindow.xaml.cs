using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfExam
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private const string URL = "https://swapi.co/api/people/?page=";
        private Rootobject rootobject = new Rootobject();
        private Dictionary<string, List<Result>> dictionary = new Dictionary<string, List<Result>>();
        private List<Result> Results { get; set; } = new List<Result>();
        public MainWindow()
        {
            InitializeComponent();
            
        }
        private bool IsDigit(string inputString)
        {
            foreach (char letter in inputString)
            {
                if (letter < '0' || letter > '9')
                {
                    return false;
                }
            }

            return true;
        }
        private void UploadPage(object sender, RoutedEventArgs e)
        {
            if (IsDigit(pageNumber.Text))
            {
                try
                {
                    
                    if (dictionary.ContainsKey(pageNumber.Text) == false)
                    {
                        Results.RemoveRange(0, Results.Count);
                        using (var client = new WebClient())
                        {
                            var json = client.DownloadString($"{URL}{pageNumber.Text}");
                            rootobject = JsonConvert.DeserializeObject<Rootobject>(json);
                            foreach (var result in rootobject.Results)
                            {
                                Results.Add(result);
                            }

                            dictionary.Add(pageNumber.Text, Results);
                            dataGrid.ItemsSource = null;
                            dataGrid.ItemsSource = Results;
                        }
                    }
                    else
                    {
                        dataGrid.ItemsSource = dictionary.GetValueOrDefault(pageNumber.Text);
                    }
                }
                catch (WebException)
                {
                    MessageBox.Show("Страница не найдена!");
                }
            }
            else
            {
                MessageBox.Show("Можно вводить только положительные целые цифры!");
            }
        }
    }
}
