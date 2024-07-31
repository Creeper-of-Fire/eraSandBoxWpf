using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using HandyControl.Tools.Extension;

namespace eraSandBoxWpf.Frame;

public partial class StoryPage : Page
{
    public StoryPage()
    {
        this.InitializeComponent();
    }

    public List<string> history = [];

    // private TextBlock? ContentTextBlock => this.FindName("ContentTextBlock") as TextBlock;

    // private void ForContentText(Action<InlineCollection> action)
    // {
    //     if (this.ContentTextBlock == null) return;
    //     action(this.ContentTextBlock.Inlines);
    // }
    //
    // private void ForContentTextBlock(Action<TextBlock> action)
    // {
    //     if (this.ContentTextBlock == null) return;
    //     action(this.ContentTextBlock);
    // }

    private static string GetTotalTextFromInlinesUsingTextRange(TextBlock? textBlock)
    {
        if (textBlock == null || textBlock.Inlines.Count == 0)
            return "";

        var start = textBlock.ContentStart;
        var end = textBlock.ContentEnd;
        var range = new TextRange(start, end);
        return range.Text;
    }

    public void Refresh()
    {
        this.history.Add(GetTotalTextFromInlinesUsingTextRange(this.StoryTextBlock));
        this.StoryTextBlock.Inlines.Clear();
    }

    public void AddText(string text)
    {
        this.StoryTextBlock.Inlines.Add(new Run(text));
    }

    private void Hyperlink_Click(object sender, RoutedEventArgs e)
    {
        // 在这里添加你的事件处理代码
        MessageBox.Show("你点击了链接！");
    }
}