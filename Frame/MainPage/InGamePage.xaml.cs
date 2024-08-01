using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using eraSandBoxWpf.Logic.World;

namespace eraSandBoxWpf.Frame.MainPage;

public partial class InGamePage : Page
{
    public static readonly DependencyProperty IsTakingTurnProperty =
        DependencyProperty.Register(nameof(IsTakingTurn), typeof(bool), typeof(MainWindow),
            new PropertyMetadata(false));

    public bool IsTakingTurn
    {
        get => (bool)this.GetValue(IsTakingTurnProperty);
        set => this.SetValue(IsTakingTurnProperty, value);
    }

    public NavigationService MainPageNavigationService => this.MainPageOfGame.NavigationService;
   

    private InGamePage()
    {
        this.InitializeComponent();
        this.MainPageNavigationService.Navigate(StoryPage.Instance);
    }

    public static InGamePage Instance { get; } = new();

    private async void TakeTurn_Click(object sender, RoutedEventArgs e)
    {
        this.IsTakingTurn = true; // 开始禁用控件
        var progress = new Progress<TotalWorld.TotalWorldProgressInfo>();
        try
        {
            await Task.Run(() =>
            {
                TotalWorld.Instance.TakeTurn(progress);
                // // 执行耗时操作
                // Debug.WriteLine("Taking turn...");
                Thread.Sleep(2000); // 模拟耗时操作
            });
        }
        finally
        {
            this.IsTakingTurn = false; // 结束后启用控件
        }
    }

    private void TestSeeCharacterButton_Click(object sender, RoutedEventArgs e)
    {
        if (Equals(this.MainPageNavigationService.Content, ShowPersonPage.Instance))
            this.MainPageNavigationService.NavigateToHomePage(StoryPage.Instance);
        else
            this.MainPageNavigationService.Navigate(ShowPersonPage.Instance);
    }
}