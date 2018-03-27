//need .net2.0 or later
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace HDPALM
{
    /// <summary>
    /// HDGALS: Dynamic loading the DLL for Ten Print
    /// </summary>
    /// 

    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }
    public struct PALETTEENTRY {
        public byte peRed;
        public byte peGreen;
        public byte peBlue;
        public byte peFlags;
    } ;
    public struct Fp_Product_Info
    {
	    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]public byte[] Serial;  
 	    public struct  Date
	    {
		    public ushort Year;
		    public byte Month;
		    public byte Day;
	    };
	    public short Operater;
	    public short Reserved;
    };
	public delegate int PALM_BEGINVIDEO_(IntPtr hdc, ref RECT prct, byte []pBuf, ref int pnWidth, ref int pnHeight);
	public delegate  int PALM_ENDVIDEO_(IntPtr hdc, ref RECT prct, byte []pBuf, int nWidth, int nHeight);

    public class PALM
    {
        //error code 
        public readonly int PM_ERROR_SUCCESS = 1;
        public readonly int PM_ERROR_INVALID_PARAMETER = -1;
        public readonly int PM_ERROR_NOT_ENOUGH_MEMORY = -2;
        public readonly int PM_ERROR_NOT_SUPPORT_FUNCTION = -3;
        public readonly int PM_ERROR_DEVICE_NOT_FOUND = -4;
        public readonly int PM_ERROR_DEVICE_NOT_INIT = -5;
        public readonly int PM_ERROR_INVALIDE_CODE = -6;
        public readonly int PM_ERROR_NO_PRIVILEGE = -7;
        public readonly int PM_ERROR_UNKNOWN = -101;
        public readonly int PM_ERROR_ACCESS_DEVICE = -102;
        public readonly int PM_ERROR_NO_ENOUPH_BANDWIDTH = -103;
        public readonly int PM_ERROR_VIDEO_NOT_INIT = -104;
        public readonly int PM_ERROR_ACCESS_FILE = -105;
        public readonly int PM_ERROR_DLL_NOT_FOUND = -106;

        //GALS functions
        public delegate int PALM_Init_();
        public delegate int PALM_Close_();
        public delegate int PALM_GetChannelCount_();
        public delegate int PALM_SetBright_(int nChannel, int nBright);
        public delegate int PALM_SetContrast_(int nChannel, int nContrast);
        public delegate int PALM_GetBright_(int nChannel, ref int pnBright);
        public delegate int PALM_GetContrast_(int nChannel, ref int pnContrast);
        public delegate int PALM_GetMaxImageSize_(int nChannel, ref int pnWidth, ref int pnHeight);
        public delegate int PALM_GetCaptWindow_(int nChannel, ref int pnOriginX, ref int pnOriginY, ref int pnWidth, ref int pnHeight);
        public delegate int PALM_SetCaptWindow_(int nChannel, int nOriginX, int nOriginY, int nWidth, int nHeight);
        public delegate int PALM_Setup_();
        public delegate int PALM_BeginCapture_(int nChannel);
        public delegate int PALM_GetFPRawData_(int nChannel, byte[] pRawData);
        public delegate int PALM_EndCapture_(int nChannel);
        public delegate int PALM_IsSupportCaptWindow_(int nChannel);
        public delegate int PALM_IsSupportSetup_();
        public delegate int PALM_GetPreviewImageSize_(int nChannel, ref int pnWidth, ref int pnHeight);
        public delegate int PALM_GetPreviewData_(int nChannel, byte[] pRawData);
        public delegate int PALM_IsSupportPreview_();
        public delegate int PALM_GetVersion_();
        public delegate int PALM_GetDesc_(sbyte[] pszDesc);
        public delegate int PALM_GetErrorInfo_(int nErrorNo, sbyte[] pszErrorInfo);

        //Expanded function
        public delegate int PALM_SaveToFile_(byte[] pBuf, int nWidth, int nHeight, string szFile, int nFormat);
        public delegate int PALM_SetVideoWindow_(IntPtr hWnd);
        public delegate int PALM_SetVideoPreview_(int nChannel, int nPreview);
        public delegate int PALM_GetVideoPreview_(int nChannel);
        public delegate int PALM_SetBeginVideo_(PALM_BEGINVIDEO_ pfnBeginVideo);
        public delegate int PALM_SetEndVideo_(PALM_ENDVIDEO_ pfnEndVideo);
        public delegate int PALM_ChangePalette_(int nStart, int nLen, PALETTEENTRY []lppe);
        public delegate int PALM_GetProductInfo_(int nChannel,ref Fp_Product_Info pfpi);
        public delegate int PALM_AccessLamp_(int nChannel, sbyte[] pData, int bRead);
        public delegate int PALM_GetUserSpace_(int nChannel);
        public delegate int PALM_WriteUserData_(int nChannel, byte[] pBuf, uint dwAddr, uint dwLen);
        public delegate int PALM_ReadUserData_(int nChannel, byte[] pBuf, uint dwAddr, uint dwLen);

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
        public  PALM_Init_                  PM_Init;
        public PALM_Close_                  PM_Close;
        public PALM_GetChannelCount_        PM_GetChannelCount = null;
        public PALM_SetBright_              PM_SetBright = null;
        public PALM_SetContrast_            PM_SetContrast = null;
        public PALM_GetBright_              PM_GetBright = null;
        public PALM_GetContrast_            PM_GetContrast = null;
        public PALM_GetMaxImageSize_        PM_GetMaxImageSize = null;
        public PALM_GetCaptWindow_          PM_GetCaptWindow = null;
        public PALM_SetCaptWindow_          PM_SetCaptWindow = null;
        public PALM_Setup_                  PM_Setup = null;
        public PALM_BeginCapture_           PM_BeginCapture = null;
        public PALM_GetFPRawData_           PM_GetFPRawData = null;
        public PALM_EndCapture_             PM_EndCapture = null;
        public PALM_IsSupportCaptWindow_    PM_IsSupportCaptWindow = null;
        public PALM_IsSupportSetup_         PM_IsSupportSetup = null;
        public PALM_GetPreviewImageSize_    PM_GetPreviewImageSize = null;
        public PALM_GetPreviewData_         PM_GetPreviewData = null;
        public PALM_IsSupportPreview_       PM_IsSupportPreview = null;
        public PALM_GetVersion_             PM_GetVersion = null;
        public PALM_GetDesc_                PM_GetDesc = null;
        public PALM_GetErrorInfo_           PM_GetErrorInfo = null;
        // ---------------------------------------------------------------------------
        public PALM_SaveToFile_             PM_SaveToFile = null;
        public PALM_SetVideoWindow_         PM_SetVideoWindow = null;
        public PALM_SetVideoPreview_        PM_SetVideoPreview = null;
        public PALM_GetVideoPreview_        PM_GetVideoPreview = null;
        public PALM_SetBeginVideo_          PM_SetBeginVideo = null;
        public PALM_SetEndVideo_            PM_SetEndVideo = null;
        public PALM_ChangePalette_          PM_ChangePalette = null;
        public PALM_GetProductInfo_         PM_GetProductInfo = null;
        public PALM_GetUserSpace_           PM_GetUserSpace = null;
        public PALM_WriteUserData_          PM_WriteUserData = null;
        public PALM_ReadUserData_           PM_ReadUserData = null;
        // ---------------------------------------------------------------------------

        public PALM(string sDllName)
        {
            hModule = LoadLibrary(sDllName);

            if (hModule == IntPtr.Zero)
            {
                MessageBox.Show("Load " + sDllName + " failed.", "Error");
                return;
            }

            PM_Init =               (PALM_Init_)            GetAddress(hModule, "PALM_Init",            typeof(PALM_Init_));
            PM_Close =              (PALM_Close_)           GetAddress(hModule, "PALM_Close",           typeof(PALM_Close_));
            PM_GetChannelCount =    (PALM_GetChannelCount_) GetAddress(hModule, "PALM_GetChannelCount", typeof(PALM_GetChannelCount_));
            PM_SetContrast =        (PALM_SetContrast_)     GetAddress(hModule, "PALM_SetContrast",     typeof(PALM_SetContrast_));
            PM_GetBright =          (PALM_GetBright_)       GetAddress(hModule, "PALM_GetBright",       typeof(PALM_GetBright_));
            PM_SetBright =          (PALM_SetBright_)       GetAddress(hModule, "PALM_SetBright",       typeof(PALM_SetBright_));
            PM_GetContrast =        (PALM_GetContrast_)     GetAddress(hModule, "PALM_GetContrast",     typeof(PALM_GetContrast_));
            PM_GetMaxImageSize =    (PALM_GetMaxImageSize_) GetAddress(hModule, "PALM_GetMaxImageSize", typeof(PALM_GetMaxImageSize_));
            PM_GetCaptWindow =      (PALM_GetCaptWindow_)   GetAddress(hModule, "PALM_GetCaptWindow",   typeof(PALM_GetCaptWindow_));
            PM_SetCaptWindow =      (PALM_SetCaptWindow_ )  GetAddress(hModule, "PALM_SetCaptWindow",   typeof(PALM_SetCaptWindow_));
            PM_Setup =              (PALM_Setup_)           GetAddress(hModule, "PALM_Setup",           typeof(PALM_Setup_));
            PM_GetErrorInfo =       (PALM_GetErrorInfo_)    GetAddress(hModule, "PALM_GetErrorInfo",    typeof(PALM_GetErrorInfo_));
            PM_BeginCapture =       (PALM_BeginCapture_)    GetAddress(hModule, "PALM_BeginCapture",    typeof(PALM_BeginCapture_));
            PM_GetFPRawData =       (PALM_GetFPRawData_)    GetAddress(hModule, "PALM_GetFPRawData",    typeof(PALM_GetFPRawData_));
            PM_EndCapture =         (PALM_EndCapture_)      GetAddress(hModule, "PALM_EndCapture",      typeof(PALM_EndCapture_));
            PM_IsSupportCaptWindow =(PALM_IsSupportCaptWindow_)GetAddress(hModule,"PALM_IsSupportCaptWindow", typeof(PALM_IsSupportCaptWindow_));
            PM_IsSupportSetup =     (PALM_IsSupportSetup_)  GetAddress(hModule, "PALM_IsSupportSetup",  typeof(PALM_IsSupportSetup_));
            PM_GetVersion =         (PALM_GetVersion_)      GetAddress(hModule, "PALM_GetVersion",      typeof(PALM_GetVersion_));
            PM_GetDesc =            (PALM_GetDesc_)         GetAddress(hModule, "PALM_GetDesc",         typeof(PALM_GetDesc_));
            PM_GetPreviewImageSize =(PALM_GetPreviewImageSize_)GetAddress(hModule,"PALM_GetPreviewImageSize", typeof(PALM_GetPreviewImageSize_));
            PM_IsSupportPreview =   (PALM_IsSupportPreview_)GetAddress(hModule, "PALM_IsSupportPreview", typeof(PALM_IsSupportPreview_));
            PM_GetPreviewData =     (PALM_GetPreviewData_)  GetAddress(hModule, "PALM_GetPreviewData",  typeof(PALM_GetPreviewData_));
            PM_SetVideoWindow =     (PALM_SetVideoWindow_)  GetAddress(hModule, "PALM_SetVideoWindow",  typeof(PALM_SetVideoWindow_));
            PM_SetVideoPreview =    (PALM_SetVideoPreview_) GetAddress(hModule, "PALM_SetVideoPreview", typeof(PALM_SetVideoPreview_));
            PM_GetVideoPreview =    (PALM_GetVideoPreview_) GetAddress(hModule, "PALM_GetVideoPreview", typeof(PALM_GetVideoPreview_));
            PM_SetVideoWindow =     (PALM_SetVideoWindow_)  GetAddress(hModule, "PALM_SetVideoWindow",  typeof(PALM_SetVideoWindow_));
            PM_SetBeginVideo =      (PALM_SetBeginVideo_)   GetAddress(hModule, "PALM_SetBeginVideo",   typeof(PALM_SetBeginVideo_));
            PM_SetEndVideo =        (PALM_SetEndVideo_)     GetAddress(hModule, "PALM_SetEndVideo",     typeof(PALM_SetEndVideo_));
            PM_ChangePalette =      (PALM_ChangePalette_)   GetAddress(hModule, "PALM_ChangePalette",   typeof(PALM_ChangePalette_));
            PM_SaveToFile =         (PALM_SaveToFile_)      GetAddress(hModule, "PALM_SaveToFile",      typeof(PALM_SaveToFile_));
            PM_GetProductInfo =     (PALM_GetProductInfo_)  GetAddress(hModule, "PALM_GetProductInfo",  typeof(PALM_GetProductInfo_));
            PM_GetUserSpace =       (PALM_GetUserSpace_)    GetAddress(hModule, "PALM_GetUserSpace",    typeof(PALM_GetUserSpace_));
            PM_WriteUserData =      (PALM_WriteUserData_)   GetAddress(hModule, "PALM_WriteUserData",   typeof(PALM_WriteUserData_));
            PM_ReadUserData =       (PALM_ReadUserData_)    GetAddress(hModule, "PALM_ReadUserData",    typeof(PALM_ReadUserData_));

            if (PM_Init != null || PM_Close != null || PM_GetChannelCount != null || PM_SetContrast
                   != null || PM_GetBright != null || PM_SetBright != null || PM_GetContrast != null || PM_GetMaxImageSize
                   != null || PM_GetCaptWindow != null || PM_Setup != null || PM_GetErrorInfo
                   != null || PM_BeginCapture != null || PM_GetFPRawData != null || PM_EndCapture != null || PM_IsSupportCaptWindow
                   != null || PM_IsSupportSetup != null || PM_GetVersion != null || PM_GetDesc != null
              )
                bDllOk = true;
       }
        ~PALM()
        {
            FreeLibrary(hModule);
        }
    }

};
