using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using HandyControl.Tools.Extension;
using Microsoft.Xaml.Behaviors.Core;

namespace eraSandBoxWpf.Controls;

public partial class MultiButton : UserControl
{
    public MultiButton()
    {
        this.InitializeComponent();
        this.ButtonModels = new ObservableCollection<ButtonModel>();
        for (int i = 0; i < 100; i++)
        {
            this.ButtonModels.Add(new ButtonModel("Button " + i, "Tooltip " + i));
        }

        this.DataContext = this;
    }

    public ObservableCollection<ButtonModel> ButtonModels { get; }

    public void RefreshButton(params ButtonModel[] buttonModels)
    {
        this.ButtonModels.Clear();
        this.ButtonModels.AddRange(buttonModels);
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        // Handle click events of each button
        var button = sender as Button;
        MessageBox.Show($"Clicked on {button?.Content}");
        button?.Command?.Execute(e);
    }
}

public readonly struct ButtonModel
{
    public ButtonModel(string text, string tooltip, Action command)
    {
        this.Text = text;
        this.Tooltip = tooltip;
        this.Command = new ActionCommand(command);
    }

    public ButtonModel(string text, string tooltip)
    {
        this.Text = text;
        this.Tooltip = tooltip;
        this.Command = new ActionCommand(() => { });
    }

    public ButtonModel(string text, string tooltip, Action<object> objectCommand)
    {
        this.Text = text;
        this.Tooltip = tooltip;
        this.Command = new ActionCommand(objectCommand);
    }

    public string Text { get; init; }
    public string Tooltip { get; init; }
    public ActionCommand Command { get; init; }
}