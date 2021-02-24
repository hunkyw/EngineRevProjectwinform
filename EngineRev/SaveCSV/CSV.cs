using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace EngineRev.SaveCSV
{
    public class CSV
    {
        public void SaveCSV()
        {

        }
        public void SetCSVPath(string Path,string TestNO,string DAQData)
        {
            string fileName = Path + "\\" + TestNO + "data"+".csv";//TestNO为自定义字符串        

            StreamWriter fileWriter = new StreamWriter(fileName, true, Encoding.ASCII);//TRUE 存在则添加，不存在则新建     

            fileWriter.Write("Time,");//时间字段名         

            for (int i = 1; i <= DAQData.Length; i++)//DAQData为要保存的float数组数据
            {
                fileWriter.Write("Temperature" + i.ToString());
                if (i == DAQData.Length)
                {
                    fileWriter.Write("\r\n");
                    break;
                }
                fileWriter.Write(",");
            }
            fileWriter.Flush();
            fileWriter.Close();
        }
    }
}
