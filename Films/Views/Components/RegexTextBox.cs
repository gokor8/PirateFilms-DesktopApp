using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Input;

namespace Films.Views.Components
{
    public class RegexTextBox : TextBox
    {
        private static readonly Regex regex = new Regex("^[a-zA-Z0-9_]+$");

        protected override void OnPreviewTextInput(TextCompositionEventArgs e)
        {
            if (!regex.IsMatch(e.Text))
                e.Handled = true;
            base.OnPreviewTextInput(e);
        }
    }
}