
using PagedList;
using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;
using TengDa.Models;

namespace TengDa.UserControls
{
    /// <summary>
    /// Pager.xaml 的交互逻辑
    /// </summary>
    public partial class Pager : Border, IComponentConnector
    {
        public Pager()
        {
            InitializeComponent();
        }

        private int pageNumber;

        public int PageNumber
        {
            get
            {
                if (this.pageNumber >= 1)
                    return this.pageNumber;
                return 1;
            }
            set
            {
                this.pageNumber = value;
            }
        }

        public event EventHandler<PageNumberChangedEventArgs> PageNumberChanged;
        

        protected void RaisePageNoChangedEvent(PageNumberChangedEventArgs args)
        {
            this.pageNumber = args.PageNumber;
            // ISSUE: reference to a compiler-generated field
            if (this.PageNumberChanged == null)
                return;
            // ISSUE: reference to a compiler-generated field
            this.PageNumberChanged(this, args);
        }

        public void Setup(IPagedList pagedList)
        {
            this.Setup((IPagedData)new PagedData<object>()
            {
                PageNumber = pagedList.PageNumber,
                PageCount = pagedList.PageCount,
                TotalItemCount = pagedList.TotalItemCount,
                PageSize = pagedList.PageSize
            });
        }

        public void Setup(IPagedData pagedList)
        {
            this.pageNumbers.Children.Clear();
            int num1 = pagedList.PageNumber - 3 < 1 ? 1 : pagedList.PageNumber - 3;
            int num2 = num1 + 6 > pagedList.PageCount ? pagedList.PageCount : num1 + 6;
            Style style1 = this.FindResource("PageButtonStyle") as Style;
            Style style2 = this.FindResource("CurrentPageButtonStyle") as Style;
            for (int index = num1; index <= num2; ++index)
            {
                Button button = new Button();
                button.Tag = index;
                button.Style = index == pagedList.PageNumber ? style2 : style1;
                button.Click += new RoutedEventHandler(this.PageButton_Click);
                button.Content = index;
                this.pageNumbers.Children.Add((UIElement)button);
            }
            this.btnFirstPage.Tag = 1;
            if (pagedList.PageNumber > 1)
            {
                this.btnPrevPage.Visibility = Visibility.Visible;
                this.btnPrevPage.Tag = (pagedList.PageNumber - 1);
            }
            else
                this.btnPrevPage.Visibility = Visibility.Collapsed;
            if (pagedList.PageNumber < pagedList.PageCount)
            {
                this.btnNextPage.Visibility = Visibility.Visible;
                this.btnNextPage.Tag = (pagedList.PageNumber + 1);
            }
            else
                this.btnNextPage.Visibility = Visibility.Collapsed;
            this.btnLastPage.Tag = pagedList.PageCount;
            this.Visibility = Visibility.Visible;
            this.pageNumber = pagedList.PageNumber;
        }

        private void PageButton_Click(object sender, RoutedEventArgs e)
        {
            int num = (int)(sender as Button).Tag;
            if (this.pageNumber == num)
                return;
            this.RaisePageNoChangedEvent(new PageNumberChangedEventArgs()
            {
                PageNumber = num
            });
        }
    }
}
