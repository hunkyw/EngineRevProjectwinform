using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static EngineRev.CANConnection;

namespace EngineRev.Viewmodel
{
	public class CANtransNum
	{
		unsafe public int CANtrans(Int32 CANID, int buff1,int buff2, VCI_CAN_OBJ cd)
		{
			int Num = 0;

			if ((Int32)cd.ID == CANID)
			{

				Num = BitConverter.ToInt16(new byte[] { cd.Data[buff2], cd.Data[buff1] }, 0);
			}
			else
			{
				return -1;
			}


			return Num;

		}

	}
}
