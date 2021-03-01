using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using static EngineRev.CANConnection;

namespace EngineRev
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        UInt32 m_bOpen = 0;
        UInt32 m_devind = 0;
        UInt32 m_canind = 0;
        UInt32 m_recover = 0;
        static UInt32 m_devtype = 3;//USBCAN2
        CANConnection da = new CANConnection();
        //连接CAN，并且开始接受CAN信息，显示到CAN报文接受区
        private void CANConnect_Click(object sender, EventArgs e)
        {

            
            if (m_bOpen == 1)
            {
                da.CANStop(m_devtype, m_devind);

                m_bOpen = 0;
            }
            else
            {
                try
                {
                    da.CANConnect(m_devtype, m_devind, m_canind);
                    da.CANStart( m_devtype, m_devind, m_canind);
                    m_bOpen = 1;
                    m_recover = 1;
                }
                catch
                {
                    MessageBox.Show("CAN初始化设备失败", "错误", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }

            if (m_bOpen != 1)
            {
                da.CANRest(m_devtype, m_devind, m_canind);
            }
            CANConnect.Text = m_bOpen == 1 ? "断开CAN连接" : "启动CAN连接";
            CANRecData.Enabled = m_bOpen == 1 ? true : false;

        }

        private void CANRecData_Tick(object sender, EventArgs e)
        {
            string str = "";
            da.CANRec(m_devtype, m_devind, m_canind, out str, out VCI_CAN_OBJ cd);

            listBoxCANInfo.Items.Add(str);
            listBoxCANInfo.SelectedIndex = listBoxCANInfo.Items.Count - 1;



        }



    }
}
