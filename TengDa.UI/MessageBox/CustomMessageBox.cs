using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TengDa.UI.MessageBox
{
    public class CustomMessageBox
    {
        public static CustomMessageBoxResult Show(string messageBoxText, CustomMessageBoxButton messageBoxButton, CustomMessageBoxIcon messageBoxImage)
        {
            CustomMessageBoxWindow window = new CustomMessageBoxWindow();
            //window.Owner = Application.Current.MainWindow;
            window.Topmost = true;
            window.MessageBoxText = messageBoxText;
            switch (messageBoxImage)
            {
                case CustomMessageBoxIcon.Question:
                    window.ImagePath = @"/image/question.png";
                    window.MessageBoxTitel = "提示";
                    break;
                case CustomMessageBoxIcon.Error:
                    window.ImagePath = @"/image/error.png";
                    window.MessageBoxTitel = "错误";
                    break;
                case CustomMessageBoxIcon.Warning:
                    window.ImagePath = @"/image/alert.png";
                    window.MessageBoxTitel = "警告";
                    break;
            }
            switch (messageBoxButton)
            {
                case CustomMessageBoxButton.OK:
                    window.OkButtonVisibility = Visibility.Visible;
                    break;
                case CustomMessageBoxButton.OKCancel:
                    window.OkButtonVisibility = Visibility.Visible;
                    window.CancelButtonVisibility = Visibility.Visible;
                    break;
                case CustomMessageBoxButton.YesNo:
                    window.YesButtonVisibility = Visibility.Visible;
                    window.NoButtonVisibility = Visibility.Visible;
                    break;
                case CustomMessageBoxButton.YesNoCancel:
                    window.YesButtonVisibility = Visibility.Visible;
                    window.NoButtonVisibility = Visibility.Visible;
                    window.CancelButtonVisibility = Visibility.Visible;
                    break;
                default:
                    window.OkButtonVisibility = Visibility.Visible;
                    break;
            }

            window.ShowDialog();
            return window.Result;
        }
    }

    #region Enum Class
    /// <summary>
    /// 显示按钮类型
    /// </summary>
    public enum CustomMessageBoxButton
    {
        OK=0,
        OKCancel=1,
        YesNo=2,
        YesNoCancel=3
    }
    /// <summary>
    /// 消息框的返回值
    /// </summary>
    public enum CustomMessageBoxResult
    {
        //用户直接关闭了消息窗口
        None = 0,
        //用户点击确定按钮
        OK = 1,
        //用户点击取消按钮
        Cancel = 2,
        //用户点击是按钮
        Yes = 3,
        //用户点击否按钮
        No = 4
    }
    /// <summary>
    /// 图标类型
    /// </summary>
    public enum CustomMessageBoxIcon
    {
        None = 0,
        Error = 1,
        Question = 2,
        Warning = 3
    }
    #endregion
}
