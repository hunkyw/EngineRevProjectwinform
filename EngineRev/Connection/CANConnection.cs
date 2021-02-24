﻿using System;
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
		static extern UInt32 VCI_OpenDevice(UInt32 DeviceType, UInt32 DeviceInd, UInt32 Reserved);
		[DllImport("controlcan.dll")]
		static extern UInt32 VCI_CloseDevice(UInt32 DeviceType, UInt32 DeviceInd);
		[DllImport("controlcan.dll")]
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
		static extern UInt32 VCI_GetReceiveNum(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);
		[DllImport("controlcan.dll")]
		static extern UInt32 VCI_ClearBuffer(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);

		[DllImport("controlcan.dll")]
		static extern UInt32 VCI_StartCAN(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);
		[DllImport("controlcan.dll")]
		static extern UInt32 VCI_ResetCAN(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd);

		[DllImport("controlcan.dll")]
		static extern UInt32 VCI_Transmit(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_CAN_OBJ pSend, UInt32 Len);

		//[DllImport("controlcan.dll")]
		//static extern UInt32 VCI_Receive(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, ref VCI_CAN_OBJ pReceive, UInt32 Len, Int32 WaitTime);
		[DllImport("controlcan.dll", CharSet = CharSet.Ansi)]
		static extern UInt32 VCI_Receive(UInt32 DeviceType, UInt32 DeviceInd, UInt32 CANInd, IntPtr pReceive, UInt32 Len, Int32 WaitTime);


	}
}
