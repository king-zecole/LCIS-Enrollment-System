//need .net2.0 or later
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HDFPSPLITE
{
    /// <summary>
    /// HDGALS: Dynamic loading the DLL for Ten Print
    /// </summary>
    /// 
    [StructLayoutAttribute(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 1)]
    public struct FPSPLIT_INFO
    {
        public int x;
        public int y;
        public int angle;
        public int reserved;
        public byte[] pOutBuf;
    }


    public class FPSPLITE
    {

        //SPLITE functions
        public delegate int FPSPLIT_Init_(int nImgW, int nImgH, int nPreview);
        public delegate void FPSPLIT_Uninit_();
        public delegate int FPSPLIT_DoSplit_(byte[] pImgBuf,  int nImgW, int nImgH, int nPreview,
            int nSplitW, int nSplitH, ref int pnFpNum, IntPtr pInfo);//FPSPLIT_INFO[] 


        //Dynamic loading
        [DllImport("kernel32.dll")]

        static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll")]

        static extern IntPtr GetProcAddress(IntPtr hModule, string lpProcName);

        [DllImport("kernel32", EntryPoint = "FreeLibrary", SetLastError = true)]

        static extern bool FreeLibrary(IntPtr hModule);

        private static Delegate GetAddress(IntPtr dllModule, string functionname, Type t)
        {
            IntPtr addr = GetProcAddress(dllModule, functionname);
            return addr == IntPtr.Zero ? null : Marshal.GetDelegateForFunctionPointer(addr, t);
        }
        // ---------------------------------------------------------------------------

        //dll handle
        private IntPtr hModule = IntPtr.Zero;
        
        //check dll is loaded ok
        public bool bDllOk = false;

        //define functions
        public  FPSPLIT_Init_               FPSPLIT_Init = null;
        public FPSPLIT_Uninit_              FPSPLIT_Uninit = null;
        public FPSPLIT_DoSplit_             FPSPLIT_DoSplit = null;

        // ---------------------------------------------------------------------------

        public FPSPLITE(string sDllName)
        {
            hModule = LoadLibrary(sDllName);

            if (hModule == IntPtr.Zero)
            {
                MessageBox.Show("Load " + sDllName + " failed.", "Error");
                return;
            }

            FPSPLIT_Init =      (FPSPLIT_Init_)GetAddress(hModule, "FPSPLIT_Init",  typeof(FPSPLIT_Init_));
            FPSPLIT_Uninit =    (FPSPLIT_Uninit_)GetAddress(hModule, "FPSPLIT_Uninit",  typeof(FPSPLIT_Uninit_));
            FPSPLIT_DoSplit =   (FPSPLIT_DoSplit_)GetAddress(hModule, "FPSPLIT_DoSplit", typeof(FPSPLIT_DoSplit_));

            if (FPSPLIT_Init != null || FPSPLIT_Uninit != null || FPSPLIT_DoSplit != null)
            {
                bDllOk = true;
            }
       }
        ~FPSPLITE()
        {
            if (hModule != IntPtr.Zero) FreeLibrary(hModule);
        }
    }

};
