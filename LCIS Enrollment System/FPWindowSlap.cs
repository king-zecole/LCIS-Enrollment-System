using System.Windows.Forms;
using HDPALM;
using HDFPSPLITE;
using System;
using LCIS_Enrollment_System.SysClass;
using System.Runtime.InteropServices;
using System.Drawing;
using System.IO;

namespace LCIS_Enrollment_System
{
    public partial class FPwindowSlap : Form
    {
        PALM palm;
        bool bOpen = false;
        bool bSplit = false;
        const int IMGW = 2304, IMGH = 2304, FPW = 640, FPH = 640;
        FPSPLITE fpsplite;
        HDFPSPLITE.FPSPLIT_INFO[] pfi;

        byte[] FingerThumb = null;
        byte[] FingerLeft = null;
        byte[] FingerRight = null;

        public FPwindowSlap()
        {
            InitializeComponent();
            //C:\Users\LCIS\Documents\LCIS\LCIS Enrollment Solution\LCIS Enrollment System\DLLs\BIOSLAP\ID_FprCap.dll
            palm = new PALM("DLLs\\PALM0410.dll");
            fpsplite = new FPSPLITE("DLLs\\FpSplit.dll");
        }

        private void rbFull(object sender, EventArgs e)
        {
            if (bOpen == false || palm.PM_SetVideoPreview == null) return;
            palm.PM_SetVideoPreview(0, 0);// 0 or 1
        }

        private void btnFpOpen_Click(object sender, System.EventArgs e)
        {
            if (palm.bDllOk == false) return;
            bOpen = palm.PM_Init() == 1;
            if (bOpen == false)
            {
                MessageBox.Show("Open device error");
                return;
            }
            rbFull(sender, e);

            bSplit = fpsplite.FPSPLIT_Init(IMGW, IMGH, 1) == 1;

            palm.PM_SetVideoWindow(this.FingerprintImage.Handle);
            //Change_ColorGreen(sender, e);

        }

        private void Change_ColorGreen(object sender, EventArgs e)
        {
            if (bOpen == false || palm.PM_ChangePalette == null) return;
            HDPALM.PALETTEENTRY[] pal = new HDPALM.PALETTEENTRY[256];
            for (int i = 0; i < 256; i++)
            {
                pal[i].peBlue = 0xff; 
                pal[i].peGreen = (byte)i;
                pal[i].peRed = (byte)i;
                pal[i].peFlags = 0;
            }
            palm.PM_ChangePalette(0, 256, pal);
        }

        private void btnFpExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FPwindow_Load(object sender, EventArgs e)
        {
            this.Location = new System.Drawing.Point(((Screen.PrimaryScreen.WorkingArea.Width - this.Width) / 2) + 82,
                          ((Screen.PrimaryScreen.WorkingArea.Height - this.Height) / 2) + 25);
        }

        private void btnsave_Click(object sender, EventArgs e)
        {

            LDO dObj = new LDO();
            string rs = string.Empty;

            if (dObj.saveInmateFingerPrint(out rs, AddInmate.LCISNUMBER,FingerThumb,FingerLeft,FingerRight))
            {
                MessageBox.Show(rs, "FingerPrint Capturing : Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show(rs, "FingerPrint Capturing : Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFpClose_Click(object sender, EventArgs e)
        {
            if (bOpen == true)
            {
                palm.PM_Close();
                bOpen = false;
            }
        }

        private void btnCaptureLeft_Click(object sender, EventArgs e)
        {
            string pathImage;
            string Fizename = string.Concat(AddInmate.LCISNUMBER.Replace('/','_').Replace('.',' '), "_Left.bmp");
            FingerLeft =  CaptureFinger(out pathImage, Fizename);
            if (FingerLeft != null)
            {
               LeftImage.Image = Image.FromFile(pathImage);
               StatusBox.Items.Add("Left Finger : Biometric capture successfully");
            }
            else
            {
               StatusBox.Items.Add("Left Finger : Unable to Capture Biometric, please try again.");
            }
        }

        private void btnCaptureRight_Click(object sender, EventArgs e)
        {
            string pathImage;
            string Fizename = string.Concat(AddInmate.LCISNUMBER.Replace('/', '_').Replace('.', ' '), "_Right.bmp");
            FingerRight = CaptureFinger( out pathImage, Fizename);
            if (FingerRight != null)
            {
                RightImage.Image = Image.FromFile(pathImage);
                StatusBox.Items.Add("Right Finger : Biometric capture successfully");
            }
            else
            {
                StatusBox.Items.Add("Right Finger : Unable to Capture Biometric, please try again.");
            }
        }

        private void btnCaptureThumb_Click(object sender, EventArgs e)
        {
            string pathImage;
            string Fizename = string.Concat(AddInmate.LCISNUMBER.Replace('/', '_').Replace('.', ' '), "_Thumbs.bmp");
            FingerThumb = CaptureFinger(out pathImage,Fizename);
            if (FingerThumb != null)
            {
                ThumbImage.Image = Image.FromFile(pathImage);
                StatusBox.Items.Add("Two Thumbs : Biometric capture successfully");
            }
            else
            {
                StatusBox.Items.Add("Two Thumbs : Unable to Capture Biometric, please try again.");
            }
        }

        private void btnSave_Demo_Click(object sender, EventArgs e)
        {
            if (bOpen == false || palm.PM_SaveToFile == null) return;
            SaveFileDialog sfdlg = new SaveFileDialog();
            sfdlg.Filter = "*.bmp|*.bmp";
            sfdlg.DefaultExt = ".bmp";
            sfdlg.Title = "Save Bitmap File";
            if (sfdlg.ShowDialog() == DialogResult.OK)
            {
                int nRet;
                byte[] buf = new byte[IMGW * IMGH];
                palm.PM_BeginCapture(0);
                nRet = palm.PM_GetFPRawData(0, buf);
                palm.PM_EndCapture(0);
                if (nRet != palm.PM_ERROR_SUCCESS)
                {
                    MessageBox.Show("Get image error, please try again.");
                }
                else
                    palm.PM_SaveToFile(buf, IMGW, IMGH, sfdlg.FileName, 0);
            }
        }

        private void btnSplite_Click(object sender, EventArgs e)
        {
            if (bOpen == false || fpsplite.bDllOk == false) return;
            int nRet;
            byte[] buf = new byte[IMGW * IMGH];
            palm.PM_BeginCapture(0);
            nRet = palm.PM_GetFPRawData(0, buf);
            palm.PM_EndCapture(0);
            if (nRet != palm.PM_ERROR_SUCCESS)
            {
                MessageBox.Show("Get image error, please try again.");
            }
            else
            {
                int FingerNum = 0;
                int size = Marshal.SizeOf(typeof(HDFPSPLITE.FPSPLIT_INFO));
                IntPtr infosIntptr = Marshal.AllocHGlobal(size * 10);

                for (int i = 0; i < 10; i++)
                {
                    IntPtr ptr = (IntPtr)((UInt32)infosIntptr + i * size + 16);
                    IntPtr p = Marshal.AllocHGlobal(FPW * FPH);
                    Marshal.WriteIntPtr(ptr, p);
                }

                int n = fpsplite.FPSPLIT_DoSplit(buf, IMGW, IMGH, 1, FPW, FPH, ref FingerNum, infosIntptr);
                if (FingerNum > 0)
                {
                    byte[] raw = new byte[FPW * FPH];
                    for (int i = 0; i < FingerNum; i++)
                    {
                        IntPtr ptr = Marshal.ReadIntPtr((IntPtr)((UInt32)infosIntptr + i * size + 16));
                        Marshal.Copy(ptr, raw, 0, FPW * FPH);

                        palm.PM_SaveToFile(raw, FPW, FPH, i.ToString() + ".bmp", 0);
                    }
                }
                for (int i = 0; i < 10; i++)
                {
                    Marshal.FreeHGlobal(Marshal.ReadIntPtr((IntPtr)((UInt32)infosIntptr + i * size + 16)));
                }
                MessageBox.Show("Find finger num " + FingerNum.ToString());
                Marshal.FreeHGlobal(infosIntptr);

            }

        }

        byte[] CaptureFinger(out string imagepath, string sfile)
        {
            var executingFolder = CreateFolderAppData();
            var SaveFilez = System.IO.Path.Combine(executingFolder, sfile);
            int nRet;
            byte[] buf = new byte[IMGW * IMGH];
            palm.PM_BeginCapture(0);
            nRet = palm.PM_GetFPRawData(0, buf);
            palm.PM_EndCapture(0);
            if (nRet != palm.PM_ERROR_SUCCESS)
            {
                MessageBox.Show("Unable to Capture Biometric, please try again.","Get image Error");
                imagepath = string.Empty;
                return null;
            }
            else
            {

                if (palm.PM_SaveToFile(buf, IMGW, IMGH, SaveFilez, 0) == 1)
                {
                    imagepath = SaveFilez;
                    return ImageToByte(SaveFilez);
                }
                else {
                    imagepath = string.Empty;
                    return null;
                }
            }

        }

        public static byte [] ImageToByte(string imagepath)
        {

            try
            {
                Image img = Image.FromFile(imagepath);
                byte[] arr;
                using (MemoryStream ms = new MemoryStream())
                {
                    img.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
                    arr = ms.ToArray();
                }
                return arr;
            }
            catch
            {
                return null;

            }
        }

        public string CreateFolderAppData()
        {
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            string specificFolder = Path.Combine(folder, "LCIS");
            // CreateDirectory will check if folder exists and, if not, create it.
            // If folder exists then CreateDirectory will do nothing.
            Directory.CreateDirectory(specificFolder);
            return specificFolder;
        }

    }
}
