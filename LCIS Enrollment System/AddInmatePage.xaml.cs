using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using LCIS_Enrollment_System.SysClass;
using MySql.Data.MySqlClient;
using System.Data;
using System.Configuration;
using System.IO;

namespace LCIS_Enrollment_System
{
    /// <summary>
    /// Interaction logic for AddInmate.xaml
    /// </summary>
    public partial class AddInmate : Page
    {

        public AddInmate()
        {
            InitializeComponent();
        }

        WebCam owebcam;

        public static string LCISNUMBER = string.Empty;

        public static bool IsEditInmate = false;

        public static bool IsGetInmate = false;

        public static DataRowView Profile_row;

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            btnaddinmateLink.BorderThickness = new Thickness(2, 2, 0, 2);
            BindAllComboBox();
            if (IsEditInmate)
            {
                DisplayAllForEditView();
            }
            else
            {
                ImageCaptureItem.IsEnabled = false;
                FingerPrintItem.IsEnabled = false;
            }
        }

        private void DisplayInmateProfile()
        {
            string result = string.Empty;
            LDO dObj = new LDO();
            DataSet Ds = dObj.getPersonalInfo(LCISNUMBER.Trim(), out result);

            if ((Ds != null) && (Ds.Tables.Count > 0))
            {
                DataTable DT = Ds.Tables[0];
                for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                {
                    txtlastname.Text = DT.Rows[i].ItemArray[2].ToString().Trim();
                    txtfirstname.Text = DT.Rows[i].ItemArray[3].ToString().Trim();
                    txtothername.Text = DT.Rows[i].ItemArray[4].ToString().Trim();
                    txtgender.Text = DT.Rows[i].ItemArray[5].ToString().Trim();
                    txtaddress.Text = DT.Rows[i].ItemArray[6].ToString().Trim();
                    txtdob.Text = DT.Rows[i].ItemArray[7].ToString().Trim();
                    txtcountry.Text = DT.Rows[i].ItemArray[8].ToString().Trim();
                    if (DT.Rows[i].ItemArray[9].ToString().Trim().Contains(","))
                    {
                        txtstateoforigin.Text = DT.Rows[i].ItemArray[9].ToString().Trim().Split(',')[0];
                        txtlga.Text = DT.Rows[i].ItemArray[9].ToString().Trim().Split(',')[1];
                    }
                    else
                    {
                        txtstateoforigin.Text = DT.Rows[i].ItemArray[9].ToString().Trim();
                        txtlga.Text = DT.Rows[i].ItemArray[9].ToString().Trim();
                    }
                    txttribe.Text = DT.Rows[i].ItemArray[10].ToString().Trim();
                    txtreligion.Text = DT.Rows[i].ItemArray[11].ToString().Trim();
                    txtheight.Text = DT.Rows[i].ItemArray[12].ToString().Trim();
                    txtweight.Text = DT.Rows[i].ItemArray[13].ToString().Trim();
                    txtcoloureye.Text = DT.Rows[i].ItemArray[14].ToString().Trim();
                    txtcolourhair.Text = DT.Rows[i].ItemArray[15].ToString().Trim();
                    txttribalmark.Text = DT.Rows[i].ItemArray[16].ToString().Trim();

                }
            }

        }

        private void DisplayInmatePolice()
        {
            string result = string.Empty;
            LDO dObj = new LDO();
            DataSet Ds = dObj.getPoliceInfo(LCISNUMBER.Trim(), out result);

            if ((Ds != null) && (Ds.Tables.Count > 0))
            {
                DataTable DT = Ds.Tables[0];
                for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                {
                    txtpOffence.Text = DT.Rows[i].ItemArray[2].ToString().Trim();
                    txtcomplainant.Text = DT.Rows[i].ItemArray[3].ToString().Trim();
                    txtaddcomplainant.Text = DT.Rows[i].ItemArray[4].ToString().Trim();
                    txtdateoffcom.Text = DT.Rows[i].ItemArray[5].ToString().Trim();
                    txtlocoffcom.Text = DT.Rows[i].ItemArray[6].ToString().Trim();
                    txtdatedefendant.Text = DT.Rows[i].ItemArray[7].ToString().Trim();
                    txtnamelegalrep.Text = DT.Rows[i].ItemArray[8].ToString().Trim();
                    txtaddlegalrep.Text = DT.Rows[i].ItemArray[9].ToString().Trim();
                    txtchargeno.Text = DT.Rows[i].ItemArray[10].ToString().Trim();
                    txtpolicefileno.Text = DT.Rows[i].ItemArray[11].ToString().Trim();
                    txtiponame.Text = DT.Rows[i].ItemArray[12].ToString().Trim();
                    txtipoloc.Text = DT.Rows[i].ItemArray[13].ToString().Trim();
                    txtpolicepros.Text = DT.Rows[i].ItemArray[14].ToString().Trim();
                    txtpoliceprosloc.Text = DT.Rows[i].ItemArray[15].ToString().Trim();
                    txtotherdefendantn.Text = DT.Rows[i].ItemArray[16].ToString().Trim();

                }
            }

        }

        private void DisplayInmateMOJ()
        {
            string result = string.Empty;
            LDO dObj = new LDO();
            DataSet Ds = dObj.getmojInfo(LCISNUMBER.Trim(), out result);

            if ((Ds != null) && (Ds.Tables.Count > 0))
            {
                DataTable DT = Ds.Tables[0];
                for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                {
                    txtOffenceMOJ.Text = DT.Rows[i].ItemArray[2].ToString().Trim();
                    txtSubOffenceMOJ.Text = DT.Rows[i].ItemArray[3].ToString().Trim();
                    txtdpprefn.Text = DT.Rows[i].ItemArray[4].ToString().Trim();
                    txtdatecasefiler.Text = DT.Rows[i].ItemArray[5].ToString().Trim();
                    txtdateadtninfo.Text = DT.Rows[i].ItemArray[6].ToString().Trim();
                    txtaccusedbailremand.Text = DT.Rows[i].ItemArray[7].ToString().Trim();
                    txtlastadjourn.Text = DT.Rows[i].ItemArray[8].ToString().Trim();
                    txtdatenextadjourn.Text = DT.Rows[i].ItemArray[9].ToString().Trim();

                }
            }

        }

        private void DisplayInmateJUD()
        {
            string result = string.Empty;
            LDO dObj = new LDO();
            DataSet Ds = dObj.getjudiciaryInfo(LCISNUMBER.Trim(), out result);

            if ((Ds != null) && (Ds.Tables.Count > 0))
            {
                DataTable DT = Ds.Tables[0];
                for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                {
                    txtnametrialc.Text = DT.Rows[i].ItemArray[2].ToString().Trim();
                    txtjudgename.Text = DT.Rows[i].ItemArray[3].ToString().Trim();
                    txtmgcourtname.Text = DT.Rows[i].ItemArray[4].ToString().Trim();
                    txthgcourtname.Text = DT.Rows[i].ItemArray[5].ToString().Trim();
                    txtmgcourtref.Text = DT.Rows[i].ItemArray[6].ToString().Trim();
                    txthgcourtref.Text = DT.Rows[i].ItemArray[7].ToString().Trim();
                    txtlastadjournJD.Text = DT.Rows[i].ItemArray[8].ToString().Trim();
                    txtnexthearingJD.Text = DT.Rows[i].ItemArray[9].ToString().Trim();

                }
            }

        }

        private void DisplayInmatePrison()
        {
            string result = string.Empty;
            LDO dObj = new LDO();
            DataSet Ds = dObj.getPrisonInfo(LCISNUMBER.Trim(), out result);

            if ((Ds != null) && (Ds.Tables.Count > 0))
            {
                DataTable DT = Ds.Tables[0];
                int IsDPPReady = 2;
                for (int i = 0; i < Ds.Tables[0].Rows.Count; i++)
                {
                    txtprisonname.Text = DT.Rows[i].ItemArray[2].ToString().Trim();
                    txtinmateprisonno.Text = DT.Rows[i].ItemArray[3].ToString().Trim();
                    txtinmatestatus.Text = DT.Rows[i].ItemArray[4].ToString().Trim();
                    txtaddtionalstatus.Text = DT.Rows[i].ItemArray[5].ToString().Trim();
                    if (DT.Rows[i].ItemArray[6] != System.DBNull.Value)
                    {
                        IsDPPReady = int.Parse(DT.Rows[i].ItemArray[6].ToString());
                    }
                    if (IsDPPReady == 1) { YesDPP.IsChecked = true; } else if (IsDPPReady == 0) { NoDpp.IsChecked = true; } else { NotSureDpp.IsChecked = true; }
                    endtermdate.Text = DT.Rows[i].ItemArray[7].ToString().Trim();
                    txtyardno.Text = DT.Rows[i].ItemArray[8].ToString().Trim();
                    txtdatefirstaddmission.Text = DT.Rows[i].ItemArray[9].ToString().Trim();
                    txtadtnreftype.Text = DT.Rows[i].ItemArray[10].ToString().Trim();
                    txtadtnorganname.Text = DT.Rows[i].ItemArray[11].ToString().Trim();
                    txtadtnrefno.Text = DT.Rows[i].ItemArray[12].ToString().Trim();
                    intialize = 2;
                    byte[] img = null;
                    if (DT.Rows[i].ItemArray[14] != System.DBNull.Value)
                    {
                        img = (byte[])DT.Rows[i].ItemArray[16];
                    }
                    WebcamImage.Source = Helper.LoadImage(img);

                }
            }

        }

        private void BindAllComboBox()
        {
            BindComboBox(txtpOffence, "offences","name","name");
            BindComboBox(txtOffenceMOJ, "offences", "name", "name");
        }

        private void btnexitLink_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void btnCapture_Click(object sender, RoutedEventArgs e)
        {
            if (intialize != 2) owebcam.Stop();
        }

        private void btnReCapture_Click(object sender, RoutedEventArgs e)
        {
            if(intialize !=2) owebcam.Stop();
            WebCam Refresh_owebcam = new WebCam();
            owebcam = Refresh_owebcam;
            owebcam.InitializeWebCam(ref WebcamImage, 1200, 700);
            ImgStatus.Content = "Please wait for few seconds.";
            System.Threading.Thread.Sleep(1000);
            owebcam.Start();
            intialize = 1;
        }

        private void btnWebCamSave_Click(object sender, RoutedEventArgs e)
        {
            ImgStatus.Content = string.Empty;
            byte[] Bytes = Helper.ImageSourceToBytes((BitmapSource)WebcamImage.Source);
            //Helper.SaveImageCapture((BitmapSource)WebcamImage.Source);

            LDO dObj = new LDO();
            string rs = string.Empty;
            if (dObj.saveInmateImage(out rs, LCISNUMBER, Bytes))
            {
                MessageBox.Show(rs, "Picture Capturing : Success", MessageBoxButton.OK, MessageBoxImage.Information);
                FingerPrintItem.IsSelected = true;
            }
            else
            {
                MessageBox.Show(rs, "Picture Capturing : Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }

        }

        private void btnResolutionCam_Click(object sender, RoutedEventArgs e)
        {
            owebcam.ResolutionSetting();
        }

        private void btnSettingCam_Click(object sender, RoutedEventArgs e)
        {
            owebcam.AdvanceSetting();
        }

        public static int intialize = 0;
        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TabItem TabSelected = (TabItem)tabControl.SelectedItem;

                if (TabSelected.Name == "ImageCaptureItem" && intialize == 0)
                {
                    // TODO: Add event handler implementation here.
                    WebcamImage.Source = new BitmapImage();
                    WebCam Refresh_owebcam = new WebCam();
                    owebcam = Refresh_owebcam;
                    owebcam.InitializeWebCam(ref WebcamImage, 1200, 700);
                    System.Threading.Thread.Sleep(1000);
                    owebcam.Start();
                    intialize = 1;
                }
            }
            catch { }
        }

        public void BindComboBox(ComboBox comboBoxName, string table_name, string fieldvalue, string fielddisplay)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(ConfigurationManager.ConnectionStrings["CrimeDbConstring"].ConnectionString);
                MySqlDataAdapter da = new MySqlDataAdapter("Select DISTINCT * FROM " + table_name + " ORDER BY " + fieldvalue, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, table_name);
                comboBoxName.ItemsSource = ds.Tables[0].DefaultView;
                comboBoxName.DisplayMemberPath = ds.Tables[0].Columns[fielddisplay].ToString();
                comboBoxName.SelectedValuePath = ds.Tables[0].Columns[fieldvalue].ToString();
            }
            catch
            {

            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            FPwindow ScannerWindow = new FPwindow();
            ScannerWindow.ShowDialog();
        }

        private void btneditinmateLink_Click(object sender, RoutedEventArgs e)
        {
            if (intialize != 2 && intialize != 0) owebcam.Stop();
            this.NavigationService.Navigate(new Uri("EditInmatePage.xaml", UriKind.Relative));
        }

        private void btnPersonalSave_Click(object sender, RoutedEventArgs e)
        {
            LDO dObj = new LDO();
            string LCISNO = dObj.assignLCISNo();
            string rs = string.Empty;
            int CountExistRecord = dObj.getPersonalInfo(LCISNUMBER, out rs).Tables[0].Rows.Count;

            if ((IsEditInmate && CountExistRecord > 0) || CountExistRecord > 0)
            {

                if (dObj.EditBasicInfo(out rs, LCISNUMBER, txtlastname.Text.Trim(), txtfirstname.Text.Trim(), txtothername.Text.Trim(), txtgender.Text.Trim(), txtaddress.Text.Trim(),
                    txtdob.Text.Trim(), txtstateoforigin.Text.Trim() + "," + txtlga.Text.Trim(), txttribe.Text.Trim(), txtreligion.Text.Trim(), txtheight.Text.Trim(), txtweight.Text.Trim(),
                    txtcoloureye.Text.Trim(), txtcolourhair.Text.Trim(), txttribalmark.Text.Trim(), txtcountry.Text.Trim()))
                {
                    MessageBox.Show(rs, "Edit Inmate Profile: Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(rs, "Edit Inmate Profile: Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                if (dObj.saveBasicInfo(out rs, LCISNO, txtlastname.Text.Trim(), txtfirstname.Text.Trim(), txtothername.Text.Trim(), txtgender.Text.Trim(), txtaddress.Text.Trim(),
                    txtdob.Text.Trim(), txtstateoforigin.Text.Trim() + "," + txtlga.Text.Trim(), txttribe.Text.Trim(), txtreligion.Text.Trim(), txtheight.Text.Trim(), txtweight.Text.Trim(),
                    txtcoloureye.Text.Trim(), txtcolourhair.Text.Trim(), txttribalmark.Text.Trim(), txtcountry.Text.Trim()))
                {

                    MessageBox.Show(rs + "\n\nGENERATED LCIS NUMBER : " + LCISNO, "Create Inmate Profile: Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LCISNUMBER = LCISNO;
                    Police_Info.IsSelected = true;
                }
                else
                {
                    MessageBox.Show(rs, "Create Inmate Profile: Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }

        }

        private void btnPoliceSave_Click(object sender, RoutedEventArgs e)
        {
            LDO dObj = new LDO();
            string rs = string.Empty;
            if (txtotherdefendantn.Text.ToLower() == "Seperate with a Comma (,)".ToLower()){txtotherdefendantn.Clear();}
            int CountExistRecord = dObj.getPoliceInfo(LCISNUMBER, out rs).Tables[0].Rows.Count;

            char LawyerType = 'P';

            if (chkbngolaw.IsChecked == true)
            {
                LawyerType = 'N';
            }
            else if (chkbgovernmentlaw.IsChecked == true)
            {
                LawyerType = 'G';
            }
            else if(chkbprivatelaw.IsChecked == true)
            {
                LawyerType = 'P';
            }
            else
            {
                chkbprivatelaw.IsChecked = true;
            }


            if ((IsEditInmate && CountExistRecord > 0) || CountExistRecord > 0)
            {
                if (dObj.EditPoliceInfo(out rs, LCISNUMBER, txtpOffence.Text.Trim(), txtcomplainant.Text.Trim(), txtaddcomplainant.Text.Trim(), txtdateoffcom.Text.Trim(),
                txtlocoffcom.Text.Trim(), txtdatedefendant.Text.Trim(), txtnamelegalrep.Text.Trim(), txtaddlegalrep.Text.Trim(),
                txtchargeno.Text.Trim(), txtpolicefileno.Text.Trim(), txtiponame.Text.Trim(), txtipoloc.Text.Trim(), txtpolicepros.Text.Trim(),
                txtpoliceprosloc.Text.Trim(), txtotherdefendantn.Text.Trim(), LawyerType))
                {
                    MessageBox.Show(rs, "Inmate (Police) Edit : Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(rs, "Inmate (Police) : Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                if (dObj.savePoliceInfo(out rs, LCISNUMBER, txtpOffence.Text.Trim(), txtcomplainant.Text.Trim(), txtaddcomplainant.Text.Trim(), txtdateoffcom.Text.Trim(),
                txtlocoffcom.Text.Trim(), txtdatedefendant.Text.Trim(), txtnamelegalrep.Text.Trim(), txtaddlegalrep.Text.Trim(),
                txtchargeno.Text.Trim(), txtpolicefileno.Text.Trim(), txtiponame.Text.Trim(), txtipoloc.Text.Trim(), txtpolicepros.Text.Trim(),
                txtpoliceprosloc.Text.Trim(), txtotherdefendantn.Text.Trim(), LawyerType))
                {

                    MessageBox.Show(rs, "Inmate (Police) Saved: Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    MOJ_Info.IsSelected = true;
                }
                else
                {
                    MessageBox.Show(rs, "Inmate (Police) : Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnMojSave_Click(object sender, RoutedEventArgs e)
        {
            LDO dObj = new LDO();
            string rs = string.Empty;
            int CountExistRecord = dObj.getmojInfo(LCISNUMBER, out rs).Tables[0].Rows.Count;

            if ((IsEditInmate && CountExistRecord > 0) || CountExistRecord > 0)
            {
                if (dObj.EditMOJInfo(out rs, LCISNUMBER, txtOffenceMOJ.Text.Trim(), txtSubOffenceMOJ.Text.Trim(), txtdpprefn.Text.Trim(), txtdatecasefiler.Text.Trim(),
                txtdateadtninfo.Text.Trim(), txtaccusedbailremand.Text.Trim(), txtlastadjourn.Text.Trim(), txtdatenextadjourn.Text.Trim()))
                {
                    MessageBox.Show(rs, "Inmate (MOJ) Edit: Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(rs, "Inmate (MOJ) : Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            else
            {
                if (dObj.saveMOJInfo(out rs, LCISNUMBER, txtOffenceMOJ.Text.Trim(), txtSubOffenceMOJ.Text.Trim(), txtdpprefn.Text.Trim(), txtdatecasefiler.Text.Trim(),
                txtdateadtninfo.Text.Trim(), txtaccusedbailremand.Text.Trim(), txtlastadjourn.Text.Trim(), txtdatenextadjourn.Text.Trim()))
                {
                    MessageBox.Show(rs, "Inmate (MOJ) Saved: Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Judisciary_Info.IsSelected = true;
                }
                else
                {
                    MessageBox.Show(rs, "Inmate (MOJ) : Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnJudisciarySave_Click(object sender, RoutedEventArgs e)
        {
            LDO dObj = new LDO();
            string rs = string.Empty;
            int CountExistRecord = dObj.getjudiciaryInfo(LCISNUMBER, out rs).Tables[0].Rows.Count;

            if ((IsEditInmate && CountExistRecord > 0) || CountExistRecord > 0)
            {
                if (dObj.EditJudisciaryInfo(out rs, LCISNUMBER, txtnametrialc.Text.Trim(), txtjudgename.Text.Trim(), txtmgcourtname.Text.Trim(),
                txthgcourtname.Text.Trim(), txtmgcourtref.Text.Trim(), txthgcourtref.Text.Trim(), txtlastadjournJD.Text.Trim(), txtnexthearingJD.Text.Trim()))
                {
                    MessageBox.Show(rs, "Inmate (Judiciary) Edit: Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(rs, "Inmate (Judiciary) : Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
            else
            {
                if (dObj.saveJudisciaryInfo(out rs, LCISNUMBER, txtnametrialc.Text.Trim(), txtjudgename.Text.Trim(), txtmgcourtname.Text.Trim(),
                txthgcourtname.Text.Trim(), txtmgcourtref.Text.Trim(), txthgcourtref.Text.Trim(), txtlastadjournJD.Text.Trim(), txtnexthearingJD.Text.Trim()))
                {
                    MessageBox.Show(rs, "Inmate (Judiciary) Saved: Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    Prison_Info.IsSelected = true;
                }
                else
                {
                    MessageBox.Show(rs, "Inmate (Judiciary) : Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }

        private void btnPrisonSave_Click(object sender, RoutedEventArgs e)
        {
            LDO dObj = new LDO();
            string rs = string.Empty;
            int IsDPPReady = 2;

            if (YesDPP.IsChecked == true)
            {
                IsDPPReady = 1;
            }
            else if (NoDpp.IsChecked == true)
            {
                IsDPPReady = 0;
            }
            else
            {
                NotSureDpp.IsChecked = true;
            }


            int CountExistRecord = dObj.getPrisonInfo(LCISNUMBER, out rs).Tables[0].Rows.Count;

            if ((IsEditInmate && CountExistRecord > 0) || CountExistRecord > 0)
            {
                if (dObj.EditPrisonInfo(out rs, LCISNUMBER, txtprisonname.Text.Trim(), txtinmateprisonno.Text.Trim(), txtinmatestatus.Text.Trim(), txtaddtionalstatus.Text.Trim(), IsDPPReady, endtermdate.Text.Trim(), txtyardno.Text.Trim(), txtdatefirstaddmission.Text.Trim(),
               txtadtnreftype.Text.Trim(), txtadtnorganname.Text.Trim(), txtadtnrefno.Text.Trim()))
                {
                    MessageBox.Show(rs, "Inmate (Prison) Edit : Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show(rs, "Inmate (Prison) : Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
               if (dObj.savePrisonInfo(out rs, LCISNUMBER, txtprisonname.Text.Trim(), txtinmateprisonno.Text.Trim(), txtinmatestatus.Text.Trim(), txtaddtionalstatus.Text.Trim(), IsDPPReady, endtermdate.Text.Trim(), txtyardno.Text.Trim(), txtdatefirstaddmission.Text.Trim(),
               txtadtnreftype.Text.Trim(), txtadtnorganname.Text.Trim(), txtadtnrefno.Text.Trim()))
               {
                    MessageBox.Show(rs, "Inmate (Prison) Saved : Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    ImageCaptureItem.IsEnabled = true;
                    FingerPrintItem.IsEnabled = true;
                    ImageCaptureItem.IsSelected = true;
                }
                else
                {
                    MessageBox.Show(rs, "Inmate (Prison) : Error", MessageBoxButton.OK, MessageBoxImage.Error);

                }
            }
        }

        private void btnaddinmateLink_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult rs = MessageBox.Show("You are about to add a new Inmate Profile.", "Confirm Operation", MessageBoxButton.OKCancel, MessageBoxImage.Question);
            if (rs == MessageBoxResult.OK)
            {
                AddInmate.IsEditInmate = false;
                AddInmate.LCISNUMBER = string.Empty;
                if (intialize != 2 && intialize != 0) owebcam.Stop();
                clearControls(GridPersonalControl);
                clearControls(GridPoliceControl);
                clearControls(GridMOJControl);
                clearControls(GridJudControl);
                clearControls(GridPrisonControl);
                clearControls(GridImageControl);
                ImageCaptureItem.IsEnabled = false;
                FingerPrintItem.IsEnabled = false;
                Personal_Info.IsSelected = true;
                this.NavigationService.Refresh();
                intialize = 0;
                //this.NavigationService.Navigate(new Uri("AddInmatePage.xaml", UriKind.Relative));

            }

        }

        void clearControls(Grid gridName)
        {
            endtermdate.IsEnabled = false;

            foreach (object txtBox in gridName.Children)
            {
                if (txtBox.GetType() == typeof(TextBox))
                {
                    ((TextBox)txtBox).Clear();
                }
                if (txtBox.GetType() == typeof(PasswordBox))
                {
                    ((PasswordBox)txtBox).Clear();
                }
                if (txtBox.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)txtBox).Text = string.Empty;
                }
                if (txtBox.GetType() == typeof(CheckBox))
                {
                    ((CheckBox)txtBox).IsChecked = false;
                }
                if (txtBox.GetType() == typeof(Image))
                {
                    ((Image)txtBox).Source = new BitmapImage();
                    intialize = 0;
                }
                if (txtBox.GetType() == typeof(DatePicker))
                {
                    ((DatePicker)txtBox).SelectedDate = null;
                    ((DatePicker)txtBox).DisplayDate = DateTime.Today;
                }
            }
        }

        void DisableControl(Grid gridName)
        {

            foreach (object txtBox in gridName.Children)
            {
                if (txtBox.GetType() == typeof(TextBox))
                {
                    ((TextBox)txtBox).IsEnabled = false;
                }
                if (txtBox.GetType() == typeof(PasswordBox))
                {
                    ((PasswordBox)txtBox).IsEnabled = false;
                }
                if (txtBox.GetType() == typeof(ComboBox))
                {
                    ((ComboBox)txtBox).IsEnabled = false;
                }
                if (txtBox.GetType() == typeof(CheckBox))
                {
                    ((CheckBox)txtBox).IsEnabled = false;
                }
                if (txtBox.GetType() == typeof(DatePicker))
                {
                    ((DatePicker)txtBox).IsEnabled = false;
                }
                if (txtBox.GetType() == typeof(Button))
                {
                    ((Button)txtBox).IsEnabled = false;
                }
            }
        }

        private void btnviewinmate_Click(object sender, RoutedEventArgs e)
        {
            if (intialize != 2 && intialize != 0) owebcam.Stop();
            this.NavigationService.Navigate(new Uri("ViewInmates.xaml", UriKind.Relative));
        }

        private void DisplayAllForEditView()
        {

            DisplayInmateProfile();
            DisplayInmatePolice();
            DisplayInmateMOJ();
            DisplayInmateJUD();
            DisplayInmatePrison();

            if (IsGetInmate)
            {
                DisableControl(GridPersonalControl);
                DisableControl(GridPoliceControl);
                DisableControl(GridMOJControl);
                DisableControl(GridJudControl);
                DisableControl(GridPrisonControl);
                DisableControl(GridImageControl);
                DisableControl(GridFingerPrint);
            }

        }

        private void YesDPP_Checked(object sender, RoutedEventArgs e)
        {
            NoDpp.IsChecked = !YesDPP.IsChecked;
            NotSureDpp.IsChecked = !YesDPP.IsChecked;
        }

        private void NoDpp_Checked(object sender, RoutedEventArgs e)
        {
            YesDPP.IsChecked = !NoDpp.IsChecked;
            NotSureDpp.IsChecked = !NoDpp.IsChecked;
        }

        private void NotSureDpp_Checked(object sender, RoutedEventArgs e)
        {
            NoDpp.IsChecked = !NotSureDpp.IsChecked;
            YesDPP.IsChecked = !NotSureDpp.IsChecked;
        }

        private void txtinmatestatus_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (txtinmatestatus.SelectedValue != null)
            {
                ComboBoxItem cbi = (ComboBoxItem)txtinmatestatus.SelectedValue;
                string selectedval = cbi.Content.ToString().ToLower();
                if (selectedval == "convicted".ToLower() || selectedval == "Condemned Convict".ToLower())
                {
                    endtermdate.IsEnabled = true;
                }
                else
                {
                    endtermdate.IsEnabled = false;
                }
            }

        }

        private void txtaddress_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtaddress.Text.ToLower() == "Type Address here...".ToLower())
            {
                txtaddress.Clear();
            }
        }

        private void txtaddlegalrep_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtaddlegalrep.Text.ToLower() == "Type Lawyer Location here...".ToLower())
            {
                txtaddlegalrep.Clear();
            }
        }

        private void txtotherdefendantn_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtotherdefendantn.Text.ToLower() == "Seperate with a Comma (,)".ToLower())
            {
                txtotherdefendantn.Clear();
            }
        }

        private void chkbgovernmentlaw_Checked(object sender, RoutedEventArgs e)
        {
            chkbngolaw.IsChecked = !chkbgovernmentlaw.IsChecked;
            chkbprivatelaw.IsChecked = !chkbgovernmentlaw.IsChecked;
        }

        private void chkbprivatelaw_Checked(object sender, RoutedEventArgs e)
        {
            chkbngolaw.IsChecked = !chkbprivatelaw.IsChecked;
            chkbgovernmentlaw.IsChecked = !chkbprivatelaw.IsChecked;
        }

        private void chkbngolaw_Checked(object sender, RoutedEventArgs e)
        {
            chkbprivatelaw.IsChecked = !chkbngolaw.IsChecked;
            chkbgovernmentlaw.IsChecked = !chkbngolaw.IsChecked;
        }
    }
}
