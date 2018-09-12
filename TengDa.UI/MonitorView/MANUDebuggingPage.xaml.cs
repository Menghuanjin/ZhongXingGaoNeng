using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TengDa.UserControls;

namespace TengDa.UI.MonitorView
{
    /// <summary>
    /// MANUDebugging.xaml 的交互逻辑
    /// </summary>
    public partial class MANUDebuggingPage :  BasePage
    {
        private static List<string> robotList = new List<string>()
        {
            "左上料机器人", "右上料机器人"
        };
        private static List<string> addStorey = new List<string>()
        {
                "上层","中层","下层"
        };

        private static List<string> instructlist = new List<string>()
        {
            "关门","开门","运行","停止","继续干燥"
        };    
        private static List<int> valueList = new List<int>()
        {
            1,2,1,2,3
        };
        public MANUDebuggingPage()
        {
            InitializeComponent();
            for (int i = 0; i < robotList.Count; i++)
            {
                this.comboBox1.Items.Add(robotList[i]);
            }
            for (int i = 0; i < 9; i++)
            {
                this.comMachine.Items.Add(string.Format("{0}#真空炉", (1 + i).ToString()));
            }
            for (int i = 0; i < addStorey.Count; i++)
            {
                this.comStorey.Items.Add(addStorey[i]);
            }
            for (int i = 0; i < instructlist.Count; i++)
            {
                this.comInstructlist.Items.Add(instructlist[i]);
            }
            this.comboBox1.SelectedIndex = 0; this.comStorey.SelectedIndex = 0;
            this.comInstructlist.SelectedIndex = 0; this.comMachine.SelectedIndex = 0;
            this.textTestTra.Text = TengDa.Communication.IPAddress.transportRobot;
        }

        private void buttTraComTest_Click(object sender, RoutedEventArgs e)
        {
            if (TengDa.Communication.Robot.RobotSocket.RobotConnect(out TengDa.Communication.SocketBLL.transportRobot, this.textTestTra.Text.Trim(), 54600, 500))
            {
                this.textTraCode.Text = "GOOD";
                TengDa.Common.logHelp.WriteLog(string.Format("发送搬运机器人通信测试,成功"), 2);
            }
            else
            {
                this.textTraCode.Text = "NG";
                TengDa.Common.logHelp.WriteLog(string.Format("发送搬运机器人通信测试,失败"), 2);
                MessageBox.CustomMessageBox.Show("请确认该机器人是否切换到调试模式!", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Warning);
            }
        }
        private void buttSendTra_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.textSendTra.Text))
            {
                MessageBox.CustomMessageBox.Show("发送指令为空，发送失败！", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Warning);
            }
            else
            {
                string ss = string.Format("<Sensor><RecCmd>hello</RecCmd><RecStation>{0}</RecStation><RecRow>2111</RecRow><RecColumn>3111</RecColumn></Sensor>", this.textSendTra.Text);
                if (string.IsNullOrEmpty(TengDa.Communication.Robot.RobotSocket.RobotSendAddress(TengDa.Communication.SocketBLL.transportRobot, ss)))
                {
                    this.textTraCode.Text = "尚未建立握手连接,发送失败";
                    TengDa.Common.logHelp.WriteLog(string.Format("发送搬运机器人指令码{0},失败", this.textTraCode.Text), 2);
                }
                else
                {
                    this.textTraCode.Text = "FNISH";
                    TengDa.Common.logHelp.WriteLog(string.Format("发送搬运机器人指令码{0},成功", this.textTraCode.Text), 2);
                }

            }
        }
        private void buttSmaComTest_Click(object sender, RoutedEventArgs e)
        {
            string ss = this.comboBox1.SelectedIndex == 0 ? TengDa.Communication.IPAddress.leftRobot : TengDa.Communication.IPAddress.rightRobot;
            if (TengDa.Communication.Robot.RobotSocket.RobotConnect(out TengDa.Communication.SocketBLL.smallRobot[this.comboBox1.SelectedIndex], ss, 54600, 500))
            {
                this.textSmaCode.Text = "GOOD";
                TengDa.Common.logHelp.WriteLog(string.Format("发送上料机器人通信测试,成功"), 2);
            }
            else
            {
                this.textSmaCode.Text = "NG";
                TengDa.Common.logHelp.WriteLog(string.Format("发送上料机器人通 信测试,失败"), 2);
                MessageBox.CustomMessageBox.Show("请确认该机器人是否切换到调试模式！", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Warning);
            }
        }
        private void buttSendSma_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.textSendSma.Text))
            {
                MessageBox.CustomMessageBox.Show("发送指令为空，发送失败！", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Warning);
            }
            else
            {
                string ss = string.Format("<Sensor><RecCmd>hello</RecCmd><RecStation>{0}</RecStation><RecRow>2111</RecRow><RecColumn>3111</RecColumn></Sensor>", this.textSendSma.Text);
                if (string.IsNullOrEmpty(TengDa.Communication.Robot.RobotSocket.RobotSendAddress(TengDa.Communication.SocketBLL.smallRobot[this.comboBox1.SelectedIndex], ss)))
                {
                    this.textSmaCode.Text = "尚未建立握手连接,发送失败";
                    TengDa.Common.logHelp.WriteLog(string.Format("发送上料机器人指令码{0},失败", this.textSendSma.Text), 2);
                }
                else
                {
                    this.textSmaCode.Text = "FNISH";
                    TengDa.Common.logHelp.WriteLog(string.Format("发送上料机器人指令码{0},成功", this.textSendSma.Text), 2);
                }
            }
        }

        private void buttBakComTest_Click(object sender, RoutedEventArgs e)
        {
            
            if (TengDa.Communication.SocketBLL.bakingContent[this.comMachine.SelectedIndex].Connected)
                this.textBakCode.Text = "通信正常";
            else
                this.textBakCode.Text = "通信异常";
        }

        private void buttSendBak_Click(object sender, RoutedEventArgs e)
        {
            int ss = -1;
            if ((this.comInstructlist.Text.Contains("关门")|| this.comInstructlist.Text.Contains("开门")) && this.comStorey.SelectedIndex==0)
            {
                ss = 5389;
            }
            else if ((this.comInstructlist.Text.Contains("运行")|| this.comInstructlist.Text.Contains("停止")|| this.comInstructlist.Text.Contains("继续干燥"))&& this.comStorey.SelectedIndex == 0)
            {
                ss = 5388;
            }
            if ((this.comInstructlist.Text.Contains("关门") || this.comInstructlist.Text.Contains("开门")) && this.comStorey.SelectedIndex == 1)
            {
                ss = 5344;
            }
            else if ((this.comInstructlist.Text.Contains("运行") || this.comInstructlist.Text.Contains("停止") || this.comInstructlist.Text.Contains("继续干燥")) && this.comStorey.SelectedIndex == 1)
            {
                ss = 5343;
            }
            if ((this.comInstructlist.Text.Contains("关门") || this.comInstructlist.Text.Contains("开门")) && this.comStorey.SelectedIndex == 2)
            {
                ss = 5299;
            }
            else if ((this.comInstructlist.Text.Contains("运行") || this.comInstructlist.Text.Contains("停止") || this.comInstructlist.Text.Contains("继续干燥") )&& this.comStorey.SelectedIndex == 2)
            {
                ss = 5298;
            }
            if (TengDa.Communication.SocketBLL.bakingContent[this.comMachine.SelectedIndex].Connected)
            {
                if (TengDa.Communication.OMRONPLCAddress.WriteDM(TengDa.Communication.SocketBLL.bakingContent[this.comMachine.SelectedIndex], ss, valueList[this.comInstructlist.SelectedIndex]))
                {
                    this.textBakCode.Text = string.Format("发送{0}指令成功", instructlist[this.comInstructlist.SelectedIndex]);
                    TengDa.Common.logHelp.WriteLog(string.Format("发送{0}指令成功", instructlist[this.comInstructlist.SelectedIndex]), 2);
                }
                else
                {
                    this.textBakCode.Text = string.Format("发送{0}指令失败", instructlist[this.comInstructlist.SelectedIndex]);
                    TengDa.Common.logHelp.WriteLog(string.Format("发送{0}指令失败", instructlist[this.comInstructlist.SelectedIndex]), 2);
                }
            }
            else
            {
                this.textBakCode.Text = string.Format("发送{0}指令失败", instructlist[this.comInstructlist.SelectedIndex]);
            }
        }

        private void buttSendBak2_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.textAddress.Text) && string.IsNullOrEmpty(this.textValue.Text))
            {
                MessageBox.CustomMessageBox.Show("D区地址或者值不能为空", MessageBox.CustomMessageBoxButton.OK, MessageBox.CustomMessageBoxIcon.Warning);
            }
            else
            {
                if (TengDa.Communication.OMRONPLCAddress.WriteDM(TengDa.Communication.SocketBLL.bakingContent[this.comMachine.SelectedIndex], Convert.ToInt16(this.textAddress.Text), Convert.ToInt16(this.textValue.Text)))
                {
                    this.textBakCode.Text = string.Format("发送地址{0},值{1},成功", this.textAddress.Text, this.textValue.Text);
                   TengDa.Common.logHelp.WriteLog(string.Format("发送地址{0},值{1},成功", this.textAddress.Text, this.textValue.Text), 2);
                }
                else
                {
                    this.textBakCode.Text = string.Format("发送地址{0},值{1},失败", this.textAddress.Text, this.textValue.Text);
                  TengDa.Common.logHelp.WriteLog(string.Format("发送地址{0},值{1},失败", this.textAddress.Text, this.textValue.Text), 2);
                }
            }
        }
    }
}
