using System.Windows;
using System.Windows.Controls;

namespace eraSandBoxWpf.Frame.MainPage;

public partial class MainMenuPage : Page
{
    public MainMenuPage()
    {
        this.InitializeComponent();
    }

    private void StartGame_Click(object sender, RoutedEventArgs e)
    {
        var inGameMainPage = new InGamePage();
        // var parentWindow = Window.GetWindow(this);
        // if (parentWindow is MainWindow mainWindow)
        // {
        //     mainWindow.MainPage.Navigate(uri);
        // }
        this.NavigationService?.Navigate(inGameMainPage);
        // if (this.Frame != null && this.Frame.SourcePageType != typeof(InGameMainPage))
        // {
        //     this.Frame.Navigate(typeof(InGameMainPage));
        // }
    }
}