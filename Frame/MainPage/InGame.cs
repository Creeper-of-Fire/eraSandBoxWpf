using System.Windows.Controls;
using System.Windows.Navigation;
using eraSandBoxWpf.Logic.World;
using MultiButton = eraSandBoxWpf.Controls.MultiButton;

namespace eraSandBoxWpf.Frame.MainPage;

public class InGame
{
    private static TotalWorld TotalWorld => TotalWorld.Instance;
    private static NavigationService MainPageNavigationService => InGamePage.Instance.MainPageNavigationService;
    private static MultiButton MainButtons => InGamePage.Instance.InGameMultiButton;
}

public static class InGameUtility
{
    public static void NavigateToHomePage(this NavigationService navigationService, Page homePage)
    {
        while (navigationService.CanGoBack)
        {
            navigationService.GoBack();
        }

        navigationService.Navigate(homePage);
        navigationService.RemoveBackEntry();
    }
}