using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using HandyControl.Controls;
using HandyControl.Tools.Extension;
using Microsoft.Xaml.Behaviors.Core;
using static eraSandBoxWpf.Frame.StoryTellerBuilder;
using ScrollViewer = System.Windows.Controls.ScrollViewer;

namespace eraSandBoxWpf.Frame;

public static class StoryTeller
{
    private static ItemsControl StoryTextFlow => StoryPage.Instance.StoryTextFlow;

    private static ScrollViewer StoryTextScroller => StoryPage.Instance.StoryTextScroller;
    //
    // public void UnAbleAle()

    /// <summary>
    /// 添加一个按钮到当前段落
    /// </summary>
    /// <param name="text"></param>
    /// <param name="toolTip"></param>
    /// <param name="foreground"></param>
    public static IControlBuilder<Button> Button(string text, string toolTip = "", Brush? foreground = null)
    {
        var button = new Button
        {
            Margin = new Thickness(0, 0, 0, 0),
            Padding = new Thickness(0, 0, 0, 0),
            FontSize = LastParagraph.FontSize,
            Content = text
        };
        var inlineUiContainer = new InlineUIContainer
        {
            BaselineAlignment = BaselineAlignment.Center,
            Child = button
        };
        LastParagraph.Inlines.Add(inlineUiContainer);
        return new ControlBuilder<Button>(button).InitToolTip(toolTip).InitForeground(foreground);
    }

    /// <summary>
    /// 添加一个超链接到当前段落
    /// </summary>
    /// <param name="text"></param>
    /// <param name="toolTip"></param>
    /// <param name="foreground"></param>
    /// <returns></returns>
    private static ITextBuilder<Hyperlink> HyperLink(string text, string toolTip = "", Brush? foreground = null)
    {
        var hyperlink = new Hyperlink(new Run(text));
        if (foreground != null)
            hyperlink.Foreground = foreground;
        if (!toolTip.IsNullOrEmpty())
            hyperlink.ToolTip = toolTip;
        AddToParagraph(hyperlink);
        return new TextBuilder<Hyperlink>(hyperlink).InitToolTip(toolTip).InitForeground(foreground);
    }

    /// <summary>
    /// 添加文本到当前段落
    /// </summary>
    /// <param name="text"></param>
    /// <param name="isBold"></param>
    /// <param name="isItalia"></param>
    /// <param name="isUnderLine"></param>
    /// <param name="foreground"></param>
    /// <param name="fontSize"></param>
    /// <returns></returns>
    public static ITextBuilder<Span> Text(string text,
        bool isBold = false,
        bool isItalia = false,
        bool isUnderLine = false,
        Brush? foreground = null,
        double fontSize = -1)
    {
        var span = new Span(new Run(text));
        if (isBold)
            span = new Bold(span);
        if (isItalia)
            span = new Italic(span);
        if (isUnderLine)
            span = new Underline(span);
        if (fontSize < 0)
            span.FontSize = LastParagraph.FontSize;
        AddToParagraph(span);
        return new TextBuilder<Span>(span).InitForeground(foreground);
    }

    /// <summary>
    /// 换行
    /// </summary>
    /// <returns></returns>
    public static ITextBuilder<LineBreak> NewLine()
    {
        var lineBreak = new LineBreak();
        AddToParagraph(lineBreak);
        return new TextBuilder<LineBreak>(lineBreak);
    }

    public static void Title(string text,
        bool isBold = false,
        bool isItalia = false,
        bool isUnderLine = false,
        Brush? foreground = null,
        double fontSize = -1
    )
    {
        ToNewParagraph();
        ToNewParagraph();
        if (fontSize < 0)
            fontSize = LastParagraph.FontSize * 2;
        Text(text, isBold, isItalia, isUnderLine, foreground, fontSize);
        ToNewParagraph();
    }

    // /// <summary>
    // /// 创建一个新的自然段
    // /// </summary>
    // /// <returns></returns>
    // public static Paragraph ToNewParagraph(bool isDivide = false, double divideThick = 1)
    // {
    //     var paragraph = new Paragraph();
    //     paragraph.BorderBrush = LastSection.BorderBrush;
    //     if (isDivide)
    //         paragraph.BorderThickness = new Thickness(0, 0, 0, 1);
    //     LastSection.Blocks.Add(paragraph);
    //     return paragraph;
    // }

    private const int FontSize = 18;

    /// <summary>
    /// 创建一个新的自然段
    /// </summary>
    /// <returns></returns>
    public static Paragraph ToNewParagraph()
    {
        var section = new RichTextBox
        {
            FontSize = FontSize,
            IsReadOnly = true,
            IsReadOnlyCaretVisible = true,
            BorderThickness = new Thickness(0, 0, 0, 0),
            Margin = new Thickness(0, 0, 0, 0),
            Padding = new Thickness(0, 0, 0, 0),
            Document = new FlowDocument()
        };
        BorderElement.SetCircular(section, false);
        BorderElement.SetCornerRadius(section, new CornerRadius(0));
        LastSection.Items.Add(section);
        var paragraph = new Paragraph();
        section.Document.Blocks.Add(paragraph);
        return paragraph;
    }

    /// <summary>
    /// 创建一个新的大段落，会禁用之前大段落中所有的可交互控件
    /// </summary>
    /// <returns></returns>
    public static ItemsControl ToNewSection()
    {
        foreach (object lastSectionItem in LastSection.Items)
        {
            switch (lastSectionItem)
            {
                case UIElement button:
                    button.IsEnabled = false;
                    break;
                case ContentElement hyperlink:
                    hyperlink.IsEnabled = false;
                    break;
            }
        }

        var section = new ItemsControl();
        section.Padding = new Thickness(0, 0, 0, 0);
        StoryTextFlow.Items.Add(section);
        return section;
    }

    private static Paragraph LastParagraph
    {
        get
        {
            object? lastBlock = StoryTextFlow.Items[^1];
            if (lastBlock is RichTextBox richTextBox)
            {
                if (richTextBox.Document.Blocks.LastBlock is Paragraph paragraph)
                    return paragraph;
            }

            Console.Out.WriteLine("没有找到Paragraph，创建一个新的");
            return ToNewParagraph();
        }
    }

    private static ItemsControl LastSection
    {
        get
        {
            var lastBlock = StoryTextFlow.Items[^1];
            if (lastBlock is ItemsControl paragraph)
                return paragraph;
            Console.Out.WriteLine("没有找到Paragraph，创建一个新的");
            return ToNewSection();
        }
    }

    private static void AddToParagraph(UIElement element)
    {
        LastParagraph.Inlines.Add(element);
        StoryTextScroller.ScrollToBottom();
    }

    private static void AddToParagraph(Inline inline)
    {
        LastParagraph.Inlines.Add(inline);
        StoryTextScroller.ScrollToBottom();
    }
}