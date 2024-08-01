using System.Windows;
using eraSandBoxWpf.Frame.MainPage;

namespace eraSandBoxWpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        this.InitializeComponent();
        this.GamePage.Navigate(MainMenuPage.Instance);
    }
}