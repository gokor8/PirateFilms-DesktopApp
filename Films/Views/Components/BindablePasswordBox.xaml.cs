using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace Films.Views.Components
{
    /// <summary>
    /// Логика взаимодействия для BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        private static readonly Regex regex = new Regex("^[a-zA-Z0-9]+$");

        public static readonly DependencyProperty PropertyTypeProperty = DependencyProperty.Register(
            "Password", typeof(string),
            typeof(BindablePasswordBox), new FrameworkPropertyMetadata(
                string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, PasswordPropertyChanged));

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BindablePasswordBox passwordBox)
            {
                passwordBox.UpdatePassword();
            }
        }

        private bool _isPasswordChanging;

        public BindablePasswordBox()
        {
            InitializeComponent();
        }

        public string Password
        {
            get
            {
                string val = (string)GetValue(PropertyTypeProperty);
                return (string)GetValue(PropertyTypeProperty);
            }
            set
            {
                SetValue(PropertyTypeProperty, value);
            }
        }

        private void PasswordBox_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            _isPasswordChanging = true;

            Password = PasswordBox.Password;
            PasswordTextBox.Text = PasswordBox.Password;

            _isPasswordChanging = false;
        }

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (!regex.IsMatch(e.Text))
                e.Handled = true;
            base.OnPreviewTextInput(e);
        }

        private void UpdatePassword()
        {
            if (!_isPasswordChanging)
            {
                PasswordBox.Password = Password;
            }
        }
    }
}
