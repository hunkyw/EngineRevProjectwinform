﻿
namespace EngineRev
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.CANConnect = new System.Windows.Forms.Button();
            this.EnginedataGridView = new System.Windows.Forms.DataGridView();
            this.listBoxCANInfo = new System.Windows.Forms.ListBox();
            this.CANRecData = new System.Windows.Forms.Timer(this.components);
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.EnginedataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // CANConnect
            // 
            this.CANConnect.Location = new System.Drawing.Point(1411, 106);
            this.CANConnect.Margin = new System.Windows.Forms.Padding(4);
            this.CANConnect.Name = "CANConnect";
            this.CANConnect.Size = new System.Drawing.Size(112, 34);
            this.CANConnect.TabIndex = 0;
            this.CANConnect.Text = "CAN连接";
            this.CANConnect.UseVisualStyleBackColor = true;
            this.CANConnect.Click += new System.EventHandler(this.CANConnect_Click);
            // 
            // EnginedataGridView
            // 
            this.EnginedataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.EnginedataGridView.Location = new System.Drawing.Point(33, 97);
            this.EnginedataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.EnginedataGridView.Name = "EnginedataGridView";
            this.EnginedataGridView.RowHeadersWidth = 62;
            this.EnginedataGridView.RowTemplate.Height = 23;
            this.EnginedataGridView.Size = new System.Drawing.Size(258, 881);
            this.EnginedataGridView.TabIndex = 1;
            // 
            // listBoxCANInfo
            // 
            this.listBoxCANInfo.FormattingEnabled = true;
            this.listBoxCANInfo.ItemHeight = 18;
            this.listBoxCANInfo.Location = new System.Drawing.Point(333, 882);
            this.listBoxCANInfo.Margin = new System.Windows.Forms.Padding(4);
            this.listBoxCANInfo.Name = "listBoxCANInfo";
            this.listBoxCANInfo.Size = new System.Drawing.Size(1020, 94);
            this.listBoxCANInfo.TabIndex = 2;
            // 
            // CANRecData
            // 
            this.CANRecData.Tick += new System.EventHandler(this.CANRecData_Tick);
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(321, 97);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1032, 744);
            this.chart1.TabIndex = 3;
            this.chart1.Text = "chart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1596, 1022);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.listBoxCANInfo);
            this.Controls.Add(this.EnginedataGridView);
            this.Controls.Add(this.CANConnect);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.EnginedataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button CANConnect;
        private System.Windows.Forms.DataGridView EnginedataGridView;
        private System.Windows.Forms.ListBox listBoxCANInfo;
        private System.Windows.Forms.Timer CANRecData;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}

