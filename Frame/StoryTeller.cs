using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
    /// <param name="AddTo"></param>
    public static IControlBuilder<Button> Button(
        string text,
        string toolTip = "",
        Brush? foreground = null,
        bool AddTo = true)
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
        if (AddTo)
            AddToParagraph(inlineUiContainer);
        return new ControlBuilder<Button>(button).InitToolTip(toolTip).InitForeground(foreground);
    }

    /// <summary>
    /// 添加一个超链接到当前段落
    /// </summary>
    /// <param name="text"></param>
    /// <param name="toolTip"></param>
    /// <param name="foreground"></param>
    /// <param name="AddTo"></param>
    /// <returns></returns>
    private static ITextBuilder<Hyperlink> HyperLink(
        string text,
        string toolTip = "",
        Brush? foreground = null,
        bool AddTo = true)
    {
        var hyperlink = new Hyperlink(new Run(text));
        if (foreground != null)
            hyperlink.Foreground = foreground;
        if (!toolTip.IsNullOrEmpty())
            hyperlink.ToolTip = toolTip;
        if (AddTo)
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
    /// <param name="AddTo"></param>
    /// <returns></returns>
    public static ITextBuilder<Span> Text(string text,
        bool isBold = false,
        bool isItalia = false,
        bool isUnderLine = false,
        Brush? foreground = null,
        double fontSize = -1,
        bool AddTo = true)
    {
        var run = new Run(text);
        run.FontSize = fontSize < 0 ? LastParagraph.FontSize : fontSize;
        var span = new Span(run);
        if (isBold)
            span = new Bold(span);
        if (isItalia)
            span = new Italic(span);
        if (isUnderLine)
            span = new Underline(span);
        span.FontSize = fontSize < 0 ? LastParagraph.FontSize : fontSize;

        if (AddTo)
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

    public static ITextBuilder<Span> Title(
        string text,
        bool isBold = false,
        bool isItalia = false,
        bool isUnderLine = false,
        Brush? foreground = null,
        double fontSize = -1,
        bool AddTo = true
    )
    {
        if (AddTo)
            ToNewParagraph();
        var span = Text(text: text, isBold: isBold, isItalia: isItalia, isUnderLine: isUnderLine,
            foreground: foreground, fontSize: fontSize < 0 ? LastParagraph.FontSize * 2 : fontSize, AddTo: AddTo);
        if (AddTo)
            ToNewParagraph();
        return span;
    }

    public static IControlBuilder<Divider> ToDivideMode(this ITextBuilder<Span> span, IControlBuilder<Divider> divider)
    {
        AddToSection(divider.SetContent(span).Contain);
        return divider;
    }


    public static IControlBuilder<Divider> DivideTitle(
        string text,
        bool isBold = false,
        bool isItalia = false,
        bool isUnderLine = false,
        Brush? foreground = null,
        double fontSize = -1,
        double LR_padding = -1,
        HorizontalAlignment HorizontalContentAlignment = HorizontalAlignment.Left,
        SolidColorBrush? lineStroke = null,
        double lineStrokeThickness = 2,
        DoubleCollection? lineStrokeDashArray = null
    )
    {
        var span = Title(text: text, isBold: isBold, isItalia: isItalia, isUnderLine: isUnderLine,
            foreground: foreground, fontSize: fontSize, AddTo: false);
        return Divide(dividerContent: span, LR_padding: LR_padding,
            HorizontalContentAlignment: HorizontalContentAlignment,
            lineStroke: lineStroke, lineStrokeThickness: lineStrokeThickness,
            lineStrokeDashArray: lineStrokeDashArray);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="dividerContent">内容</param>
    /// <param name="LR_padding">左右的间距</param>
    /// <param name="margin"></param>
    /// <param name="HorizontalContentAlignment">内容所在的位置，默认靠左</param>
    /// <param name="lineStroke">颜色</param>
    /// <param name="lineStrokeThickness">厚度，默认为2</param>
    /// <param name="lineStrokeDashArray">用来生成虚线</param>
    /// <param name="AddTo"></param>
    public static IControlBuilder<Divider> Divide(
        object? dividerContent = null,
        double LR_padding = -1,
        Thickness? margin = null,
        HorizontalAlignment HorizontalContentAlignment = HorizontalAlignment.Left,
        SolidColorBrush? lineStroke = null,
        double lineStrokeThickness = 2,
        DoubleCollection? lineStrokeDashArray = null,
        bool AddTo = true
    )
    {
        var divider = new Divider();
        switch (dividerContent)
        {
            case null:
                break;
            case string s:
                divider.Content = Text(s, AddTo: false).Contain;
                break;
            case IElementBuilder<object> builder:
                divider.Content = builder.RawContain;
                break;
            default:
                divider.Content = dividerContent;
                break;
        }


        if (LR_padding > 0)
            divider.Padding = new Thickness(LR_padding, 0, LR_padding, 0);
        if (margin is not null)
            divider.Margin = (Thickness)margin;
        divider.Orientation = Orientation.Horizontal;
        divider.UseLayoutRounding = true;
        divider.HorizontalContentAlignment = HorizontalContentAlignment;
        if (lineStroke != null)
            divider.LineStroke = lineStroke;
        else
        {
            // var binding = new Binding();
            // binding.Source = "{DynamicResource PrimaryTextBrush}";
            // binding.Path = new PropertyPath("LineStroke");
            divider.SetResourceReference(Divider.LineStrokeProperty, "PrimaryTextBrush");
        }

        if (lineStrokeThickness > 0)
            divider.LineStrokeThickness = lineStrokeThickness;
        if (lineStrokeDashArray != null)
            divider.LineStrokeDashArray = lineStrokeDashArray;
        if (AddTo)
            AddToSection(divider);
        return new ControlBuilder<Divider>(new Divider());
    }

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
        var paragraph = new Paragraph();
        section.Document.Blocks.Add(paragraph);
        AddToSection(section);
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
        AddAsSection(section);
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
            object? lastBlock = StoryTextFlow.Items[^1];
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

    private static void AddToSection(object item)
    {
        LastSection.Items.Add(item);
        StoryTextScroller.ScrollToBottom();
    }

    private static void AddAsSection(object item)
    {
        StoryTextFlow.Items.Add(item);
        StoryTextScroller.ScrollToBottom();
    }

    private static void AddToParagraph(Inline inline)
    {
        LastParagraph.Inlines.Add(inline);
        StoryTextScroller.ScrollToBottom();
    }
}