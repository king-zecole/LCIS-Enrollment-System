using LCIS_Enrollment_System.SysClass;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace LCIS_Enrollment_System
{
    /// <summary>
    /// Interaction logic for EditInmatePage.xaml
    /// </summary>
    public partial class EditInmatePage : Page
    {
        public EditInmatePage()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            btneditinmateLink.BorderThickness = new Thickness(2, 2, 0, 2);
        }

        private void btnexitLink_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnsearch_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtsearch.Text))
            {
                LDO dObj = new LDO();
                string result = string.Empty;
                DataSet SearchDs = dObj.SearchPersonalRecords(txtsearch.Text, out result);
                if (SearchDs != null)
                {
                    errorlbl.Content = result;
                    errorlbl.Foreground = Brushes.Green;
                    editDG.ItemsSource = new DataView (SearchDs.Tables[0]);
                }
                else {

                    errorlbl.Content = result;
                    errorlbl.Foreground = Brushes.Red;
                }

            }
        }

        private void editDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if(editDG.SelectedItem != null)
            {
                DataRowView dataRow = (DataRowView)editDG.SelectedItem;
                string profile_id =  dataRow.Row.ItemArray[0].ToString();
                string Lcis_number = dataRow.Row.ItemArray[1].ToString();
                AddInmate.IsEditInmate = true;
                AddInmate.IsGetInmate = false;
                AddInmate.LCISNUMBER = Lcis_number;
                AddInmate.Profile_row = dataRow;
                AddInmate.intialize = 0;
                this.NavigationService.Navigate(new Uri("AddInmatePage.xaml", UriKind.Relative));
            }
       
        }

        private void btnaddinmateLink_Click(object sender, RoutedEventArgs e)
        {
            AddInmate.IsEditInmate = false;
            AddInmate.IsGetInmate = false;
            AddInmate.LCISNUMBER = string.Empty;
            AddInmate.intialize = 0;
            AddInmate.Profile_row = (DataRowView)editDG.SelectedItem;
            //NavigationService.GoBack();
            this.NavigationService.Navigate(new Uri("AddInmatePage.xaml", UriKind.Relative));
        }

        private void btnviewinmate_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("ViewInmates.xaml", UriKind.Relative));
        }
    }
}
