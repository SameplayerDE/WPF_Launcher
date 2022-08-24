using System.Windows;
using System.Windows.Controls;

namespace WpfApp2
{
    public partial class NotificationMessage : UserControl
    {

        public string Text
        {
            get => TextBlock.Text;
            set => TextBlock.Text = value;
        }
        
        public string IconPath { get; set; }

        public CornerRadius Radius
        {
            get => Border.CornerRadius;
            set => Border.CornerRadius = value;
        }
        
        public NotificationMessage()
        {
            InitializeComponent();
            this.DataContext = this;
        }
    }
}