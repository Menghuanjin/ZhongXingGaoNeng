using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace TengDa.Core
{
    public class MenuNode
    {
        private List<MenuNode> children;

        public PermissionNode PermissionNode { get; set; }

        public Button Button { get; set; }

        public bool Selected { get; set; }

        public string Url { get; set; }

        public List<MenuNode> Children
        {
            get
            {
                return this.children ?? (this.children = new List<MenuNode>());
            }
            set
            {
                this.children = value;
            }
        }

        public FrameworkElement ChildPanel { get; set; }
    }
}
