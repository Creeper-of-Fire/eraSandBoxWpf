using System.Windows.Controls;

namespace eraSandBoxWpf.Frame;

public partial class ShowPersonPage : Page
{
    private ShowPersonPage()
    {
        this.InitializeComponent();
    }

    public static ShowPersonPage Instance { get; } = new();
}