using System.Windows;
using System.Windows.Controls;

namespace eraSandBoxWpf.Frame.MainPage;

public partial class MainMenuPage : Page
{
    public MainMenuPage()
    {
        this.InitializeComponent();
    }

    public static MainMenuPage Instance { get; } = new();

    private void StartGame_Click(object sender, RoutedEventArgs e)
    {
        this.NavigationService?.NavigateToHomePage(InGamePage.Instance);
    }
}