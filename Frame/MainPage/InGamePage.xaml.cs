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
    
    // private System.Windows.Controls.Frame? MainPageOfGame => 
    //     this.FindName("MainPageOfGame") as System.Windows.Controls.Frame;
    private NavigationService MainPageNavigationService=>this.MainPageOfGame.NavigationService;
    public InGamePage()
    {
        this.InitializeComponent();

        
    }

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

    
}