using System.Globalization;
using System.Windows;

namespace eraSandBoxWpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        
        // 设置当前UI文化，这应该基于用户的系统设置或用户的选择
        Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
    }
    // protected override void OnStartup(StartupEventArgs e)
    // {
    //     base.OnStartup(e);
    //     
    //     // 设置当前UI文化，这应该基于用户的系统设置或用户的选择
    //     Thread.CurrentThread.CurrentUICulture = new CultureInfo("zh-CN");
    // }
}