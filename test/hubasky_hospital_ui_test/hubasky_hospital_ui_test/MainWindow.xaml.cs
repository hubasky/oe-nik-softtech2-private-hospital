using System;
using System.Collections.Generic;
using System.Linq;
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

namespace hubasky_hospital_ui_test
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            List<HBItem> hbitems = new List<HBItem>();
            hbitems.Add(new HBItem("XA213424", "fecskendő", "mellkassebészet", "314", "db"));
            hbitems.Add(new HBItem("BG123457", "gézlap", "mellkassebészet", "1230", "db"));
            hbitems.Add(new HBItem("HB8234z1", "10-es tű", "mellkassebészet", "634", "db"));
            hbitems.Add(new HBItem("ZHdq89389", "stetoszkóp", "intenzív osztály", "14", "db"));
            hbitems.Add(new HBItem("HU123913", "gurulós hordágy", "sürgősségi osztály", "12", "db"));
            hbitems.Add(new HBItem("JH893822", "szike", "szülészet", "184", "db"));
            hbitems.Add(new HBItem("KAu738134", "orvosi köppeny", "kardiológia", "8", "db"));

            dg_hb.ItemsSource = hbitems;
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ProcedureWindow pr = new ProcedureWindow();
            pr.Show();
        }
    }
}
