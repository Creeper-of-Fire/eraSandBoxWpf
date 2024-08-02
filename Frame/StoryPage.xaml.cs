using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using HandyControl.Data;
using static eraSandBoxWpf.Frame.StoryTeller;
using MessageBox = System.Windows.MessageBox;

namespace eraSandBoxWpf.Frame;

public partial class StoryPage : Page
{
    public StoryPage()
    {
        this.InitializeComponent();
    }

    public static StoryPage Instance { get; } = new();

    public List<string> history = [];

    private static string GetTotalTextFromInlinesUsingTextRange(TextBlock? textBlock)
    {
        if (textBlock == null || textBlock.Inlines.Count == 0)
            return "";

        var start = textBlock.ContentStart;
        var end = textBlock.ContentEnd;
        var range = new TextRange(start, end);
        return range.Text;
    }

    private void Hyperlink_Click(object sender, RoutedEventArgs e)
    {
        // 在这里添加你的事件处理代码
        Button("test").InitToolTip("test");
        Text("test", isBold: true);
        Title("test");
        NewLine();
        Text("test", isBold: true);
        Text("test", isBold: true);
        Divide(lineStrokeDashArray: new DoubleCollection { 2, 2 });
        Divide(Text("test", AddTo: false));
        DivideTitle("TestTitle");
        Divide(margin: new Thickness(0, 0, 400, 0));
        // StoryTeller.ToNewSection();
        // 假设你已经有了一个 ScrollViewer 对象，名为 myScrollViewer

        // StoryTeller.ToNewSection();
        // StoryTeller.Paragraph();
        // this.UpdateLayout();
        // StoryTeller.Divider();

        // MessageBox.Show("你点击了链接！");
    }
}