using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace EngineRev
{
	public class CANConnection
	{
		//1.CAN卡版本
		public struct VCI_BOARD_INFO
		{
			public UInt16 hw_Version;
			public UInt16 fw_Version;
			public UInt16 dr_Version;
			public UInt16 in_Version;
			public UInt16 irq_Num;
			public byte can_Num;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] public byte[] str_Serial_Num;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
			public byte[] str_hw_Type;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
			public byte[] Reserved;
		}

		/////////////////////////////////////////////////////
		//2.定义CAN信息帧的数据类型。
		unsafe public struct VCI_CAN_OBJ  //使用不安全代码
		{
			public uint ID;
			public uint TimeStamp;
			public byte TimeFlag;
			public byte SendType;
			public byte RemoteFlag;//是否是远程帧
			public byte ExternFlag;//是否是扩展帧
			public byte DataLen;

			public fixed byte Data[8];

			public fixed byte Reserved[3];

		}
		//3.定义CAN控制器状态的数据类型。
		public struct VCI_CAN_STATUS
		{
			public byte ErrInterrupt;
			public byte regMode;
			public byte regStatus;
			public byte regALCapture;
			public byte regECCapture;
			public byte regEWLimit;
			public byte regRECounter;
			public byte regTECounter;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
			public byte[] Reserved;
		}
		//4.定义错误信息的数据类型。
		public struct VCI_ERR_INFO
		{
			public UInt32 ErrCode;
			public byte Passive_ErrData1;
			public byte Passive_ErrData2;
			public byte Passive_ErrData3;
			public byte ArLost_ErrData;
		}

		//5.定义初始化CAN的数据类型
		public struct VCI_INIT_CONFIG
		{
			public UInt32 AccCode;
			public UInt32 AccMask;
			public UInt32 Reserved;
			public byte Filter;
			public byte Timing0;
			public byte Timing1;
			public byte Mode;
		}

		public struct CHGDESIPANDPORT
		{
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
			public byte[] szpwd;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
			public byte[] szdesip;
			public Int32 desport;

			public void Init()
			{
				szpwd = new byte[10];
				szdesip = new byte[20];
			}
		}

		/** 设备号
		const int VCI_PCI5121 = 1;
		const int VCI_PCI9810 = 2;
		const int VCI_USBCAN1 = 3;
		const int VCI_USBCAN2 = 4;
		const int VCI_USBCAN2A = 4;
		const int VCI_PCI9820 = 5;
		const int VCI_CAN232 = 6;
		const int VCI_PCI5110 = 7;
		const int VCI_CANLITE = 8;
		const int VCI_ISA9620 = 9;
		const int VCI_ISA5420 = 10;
		const int VCI_PC104CAN = 11;
		const int VCI_CANETUDP = 12;
		const int VCI_CANETE = 12;
		const int VCI_DNP9810 = 13;
		const int VCI_PCI9840 = 14;
		const int VCI_PC104CAN2 = 15;
		const int VCI_PCI9820I = 16;
		const int VCI_CANETTCP = 17;
		const int VCI_PEC9920 = 18;
		const int VCI_PCI5010U = 19;
		const int VCI_USBCAN_E_U = 20;
		const int VCI_USBCAN_2E_U = 21;
		const int VCI_PCI5020U = 22;
		const int VCI_EG20T_CAN = 23;
		const int VCI_PCIE9221 = 24;
		const int VCI_PCIE9211 = 25;
		**/
		/// <summary>
		/// 
		/// </summary>
		/// <param name="DeviceType"></param>
		/// <param name="DeviceInd"></param>
		/// <param name="Reserved"></param>
		/// <returns></returns>
		/// 导入can驱动
		[DllImport("controlcan.dll")]
		/// <summary>
		/// 打开设备
		/// </summary>
		static extern UInt32 VCI_OpenDevice(UInt32 DeviceType, UInt32 DeviceInd, UInt32 Reserved);
		[DllImport("controlcan.dll")]
		/// <summary>
		/// 关闭设备
		/// </summary>
		static extern UInt32 VCI_CloseDevice(UInt32 DeviceType, UInt32 DeviceInd);
		[DllImport("controlcan.dll")]
		/// <summary>
		/// 设备初始化
		/// </summary>
		static extern UInt32 VCI_InitCAN(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_INIT_CONFIG pInitConfig);
		[DllImport("controlcan.dll")]

		static extern UInt32 VCI_ReadBoardInfo(UInt32 DeviceType, UInt32 DeviceInd, ref VCI_BOARD_INFO pInfo);
		[DllImport("controlcan.dll")]
		static extern UInt32 VCI_ReadErrInfo(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_ERR_INFO pErrInfo);
		[DllImport("controlcan.dll")]
		static extern UInt32 VCI_ReadCANStatus(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_CAN_STATUS pCANStatus);

		[DllImport("controlcan.dll")]
		static extern UInt32 VCI_GetReference(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, UInt32 RefType, ref byte pData);
		[DllImport("controlcan.dll")]
		static extern UInt32 VCI_SetReference(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, UInt32 RefType, ref byte pData);

		[DllImport("controlcan.dll")]
		/// <summary>
		/// 获取接受区字节数
		/// </summary>
		static extern UInt32 VCI_GetReceiveNum(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);
		[DllImport("controlcan.dll")]
		static extern UInt32 VCI_ClearBuffer(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);

		[DllImport("controlcan.dll")]
		/// <summary>
		/// 打开can连接
		/// </summary>
		static extern UInt32 VCI_StartCAN(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);
		[DllImport("controlcan.dll")]
		/// <summary>
		/// 关闭can连接
		/// </summary>
		static extern UInt32 VCI_ResetCAN(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);

		[DllImport("controlcan.dll")]
		/// <summary>
		/// 发送can信息
		/// </summary>
		static extern UInt32 VCI_Transmit(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_CAN_OBJ pSend, UInt32 Len);

		//[DllImport("controlcan.dll")]
		//static extern UInt32 VCI_Receive(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_CAN_OBJ pReceive, UInt32 Len, Int32 WaitTime);
		[DllImport("controlcan.dll", CharSet = CharSet.Ansi)]
		/// <summary>
		/// 接受数据
		/// </summary>
		static extern UInt32 VCI_Receive(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, IntPtr pReceive, UInt32 Len, Int32 WaitTime);

		/// <summary>
		/// CAN连接
		/// </summary>
		public bool CANConnect( UInt32 m_devtype,UInt32 m_devind, UInt32 m_canind)
		{

				//m_devtype = m_devtype;设备类型
				//m_devind = m_devind;设备索引号
				//m_canind = m_canind;地基路CAN
				if (VCI_OpenDevice(m_devtype, m_devind, 0) == 0)
				{
					//MessageBox.Show("打开设备失败,请检查设备类型和设备索引号是否正确", "错误",
							//MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}

				VCI_INIT_CONFIG config = new VCI_INIT_CONFIG();

				config.AccCode = System.Convert.ToUInt32("0x" + "00000000", 16);
				config.AccMask = System.Convert.ToUInt32("0x" + "FFFFFFFF", 16);
				config.Timing0 = System.Convert.ToByte("0x" + "00", 16);
				config.Timing1 = System.Convert.ToByte("0x" + "1C", 16);
				config.Filter = (Byte) 1 ;
				config.Mode = (Byte)0;


				if (VCI_InitCAN(m_devtype, m_devind, m_canind, ref config) == 0)
				{
					//MessageBox.Show("初始化设备失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

					return false;
				}

				return true;
		}

		
		/// <summary>
		/// CAN打开
		/// </summary>
		public void CANStart(uint m_devtype, UInt32 m_devind, UInt32 m_canind)
		{
			uint ret = VCI_StartCAN(m_devtype, m_devind, m_canind);
			if (ret != 1)
			{
				MessageBox.Show("启动失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}
		/// <summary>
		/// CAN停止
		/// </summary>
		public void CANStop( uint m_devtype, UInt32 m_devind, UInt32 m_canind)
		{
			VCI_ResetCAN(m_devtype, m_devind, m_canind);
		}
		/// <summary>
		/// CAN接收 返回str
		/// </summary>
		unsafe public void CANRec(uint m_devtype, UInt32 m_devind, UInt32 m_canind, out String str)
		{
			UInt32 res = new UInt32();
			res = VCI_GetReceiveNum(m_devtype, m_devind, m_canind);
			//if (res == 0)
				//return 0;
			//res = VCI_Receive(m_devtype, m_devind, m_canind, ref m_recobj[0],50, 100);

			/////////////////////////////////////
			UInt32 con_maxlen = 50;
			IntPtr pt = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(VCI_CAN_OBJ)) * (Int32)con_maxlen);




			res = VCI_Receive(m_devtype, m_devind, m_canind, pt, con_maxlen, 100);
			////////////////////////////////////////////////////////

			str = "";
			for (UInt32 i = 0; i < res; i++)
			{
				VCI_CAN_OBJ obj = (VCI_CAN_OBJ)Marshal.PtrToStructure((IntPtr)((UInt32)pt + i * Marshal.SizeOf(typeof(VCI_CAN_OBJ))), typeof(VCI_CAN_OBJ));

				str = "接收到数据: ";
				str += "  帧ID:0x" + System.Convert.ToString((Int32)obj.ID, 16);
				str += "  帧格式:";
				if (obj.RemoteFlag == 0)
					str += "数据帧 ";
				else
					str += "远程帧 ";
				if (obj.ExternFlag == 0)
					str += "标准帧 ";
				else
					str += "扩展帧 ";

				//////////////////////////////////////////
				if (obj.RemoteFlag == 0)
				{
					str += "数据: ";
					byte len = (byte)(obj.DataLen % 9);
					byte j = 0;
					if (j++ < len)
						str += " " + System.Convert.ToString(obj.Data[0], 16);
					if (j++ < len)
						str += " " + System.Convert.ToString(obj.Data[1], 16);
					if (j++ < len)
						str += " " + System.Convert.ToString(obj.Data[2], 16);
					if (j++ < len)
						str += " " + System.Convert.ToString(obj.Data[3], 16);
					if (j++ < len)
						str += " " + System.Convert.ToString(obj.Data[4], 16);
					if (j++ < len)
						str += " " + System.Convert.ToString(obj.Data[5], 16);
					if (j++ < len)
						str += " " + System.Convert.ToString(obj.Data[6], 16);
					if (j++ < len)
						str += " " + System.Convert.ToString(obj.Data[7], 16);

				}

			}
			Marshal.FreeHGlobal(pt);
		}

		unsafe public void CANSend(uint m_bOpen, uint m_devtype, UInt32 m_devind, UInt32 m_canind, byte SendType, byte FrameFormat, byte FrameType, string SendText)
		{
			if (m_bOpen == 0)
				return;

			VCI_CAN_OBJ sendobj = new VCI_CAN_OBJ();
			//sendobj.Init();
			sendobj.SendType = SendType;
			sendobj.RemoteFlag = FrameFormat;
			sendobj.ExternFlag = FrameType;
			sendobj.ID = System.Convert.ToUInt32("0x" + SendText, 16);
			int len = (SendText.Length + 1) / 3;
			sendobj.DataLen = System.Convert.ToByte(len);
			String strdata = SendText;
			int i = -1;
			if (i++ < len - 1)
				sendobj.Data[0] = System.Convert.ToByte("0x" + strdata.Substring(i * 3, 2), 16);
			if (i++ < len - 1)
				sendobj.Data[1] = System.Convert.ToByte("0x" + strdata.Substring(i * 3, 2), 16);
			if (i++ < len - 1)
				sendobj.Data[2] = System.Convert.ToByte("0x" + strdata.Substring(i * 3, 2), 16);
			if (i++ < len - 1)
				sendobj.Data[3] = System.Convert.ToByte("0x" + strdata.Substring(i * 3, 2), 16);
			if (i++ < len - 1)
				sendobj.Data[4] = System.Convert.ToByte("0x" + strdata.Substring(i * 3, 2), 16);
			if (i++ < len - 1)
				sendobj.Data[5] = System.Convert.ToByte("0x" + strdata.Substring(i * 3, 2), 16);
			if (i++ < len - 1)
				sendobj.Data[6] = System.Convert.ToByte("0x" + strdata.Substring(i * 3, 2), 16);
			if (i++ < len - 1)
				sendobj.Data[7] = System.Convert.ToByte("0x" + strdata.Substring(i * 3, 2), 16);

			if (VCI_Transmit(m_devtype, m_devind, m_canind, ref sendobj, 1) == 0)
			{
				MessageBox.Show("发送失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
		}


	}


}

