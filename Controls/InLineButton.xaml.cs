using System.Windows.Controls;
using System.Windows.Input;

namespace eraSandBoxWpf.Controls;

public partial class InLineButton : UserControl
{
    public InLineButton()
    {
        InitializeComponent();
    }
    
    public ICommand Command { get; set; }
    public object PopupContent { get; set; }
    public string Text { get; set; }
}