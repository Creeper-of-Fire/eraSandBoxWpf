using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using HandyControl.Tools.Extension;
using Microsoft.Xaml.Behaviors.Core;

namespace eraSandBoxWpf.Frame;

public static class StoryTellerBuilder
{
    public class TextBuilder<T>(T contain) : ITextBuilder<T>
        where T : FrameworkContentElement
    {
        public T Contain { get; set; } = contain;
    }

    public class ControlBuilder<T>(T contain) : IControlBuilder<T>
        where T : FrameworkElement
    {
        public T Contain { get; set; } = contain;
    }

    public interface IControlBuilder<T>
        where T : FrameworkElement
    {
        public T Contain { get; set; }
    }

    public interface ITextBuilder<T>
        where T : FrameworkContentElement
    {
        public T Contain { get; set; }
    }

    public static IControlBuilder<T> InitToolTip<T>(
        this IControlBuilder<T> builder, string toolTip)
        where T : FrameworkElement
    {
        if (!toolTip.IsNullOrEmpty())
            builder.Contain.ToolTip = toolTip;
        return builder;
    }

    public static ITextBuilder<T> InitToolTip<T>(
        this ITextBuilder<T> builder, string toolTip)
        where T : FrameworkContentElement
    {
        if (!toolTip.IsNullOrEmpty())
            builder.Contain.ToolTip = toolTip;
        return builder;
    }

    public static ITextBuilder<T> InitForeground<T>(
        this ITextBuilder<T> builder, Brush? foreground)
        where T : TextElement
    {
        if (foreground != null)
            builder.Contain.Foreground = foreground;
        return builder;
    }

    public static IControlBuilder<T> InitForeground<T>(
        this IControlBuilder<T> builder, Brush? foreground)
        where T : Control
    {
        if (foreground != null)
            builder.Contain.Foreground = foreground;
        return builder;
    }

    public static ITextBuilder<T> InitFrontSize<T>(
        this ITextBuilder<T> builder, double fontSize)
        where T : TextElement
    {
        builder.Contain.FontSize = fontSize;
        return builder;
    }

    public static IControlBuilder<T> InitFrontSize<T>(
        this IControlBuilder<T> builder, double fontSize)
        where T : Control
    {
        builder.Contain.FontSize = fontSize;
        return builder;
    }

    public static ITextBuilder<T> InitCommand<T>(
        this ITextBuilder<T> builder, ICommand? command)
        where T : Hyperlink
    {
        if (command != null)
            builder.Contain.Command = command;
        return builder;
    }

    public static ITextBuilder<T> InitCommand<T>(
        this ITextBuilder<T> builder, Action action)
        where T : Hyperlink =>
        InitCommand(builder, new ActionCommand(action));

    public static ITextBuilder<T> InitCommand<T>(
        this ITextBuilder<T> builder, Action<object> objectAction)
        where T : Hyperlink =>
        InitCommand(builder, new ActionCommand(objectAction));

    public static IControlBuilder<T> InitCommand<T>(
        this IControlBuilder<T> builder, ICommand command)
        where T : ButtonBase
    {
        builder.Contain.Command = command;
        return builder;
    }

    public static IControlBuilder<T> InitCommand<T>(
        this IControlBuilder<T> builder, Action action)
        where T : ButtonBase =>
        InitCommand(builder, new ActionCommand(action));

    public static IControlBuilder<T> InitCommand<T>(
        this IControlBuilder<T> builder, Action<object> objectAction)
        where T : ButtonBase =>
        InitCommand(builder, new ActionCommand(objectAction));
}