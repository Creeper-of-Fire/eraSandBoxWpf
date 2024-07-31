using System.Collections.ObjectModel;
using System.Diagnostics.Tracing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace eraSandBoxWpf.Control;

public partial class MultiButton : UserControl
{
    public MultiButton()
    {
        this.InitializeComponent();
        this.ButtonModels = new ObservableCollection<ButtonModel>();
        for (int i = 0; i < 100; i++)
        {
            this.AddButton("Button " + i, "Tooltip " + i, null);
        }

        this.DataContext = this;
    }
    //


    public ObservableCollection<ButtonModel> ButtonModels { get; set; }

    private void AddButton(string text, string tooltip, ICommand? command)
    {
        // Add a new button with a unique content
        this.ButtonModels.Add(new ButtonModel(text, tooltip, command));
    }

    private void RemoveButton(object sender, RoutedEventArgs e)
    {
        // Remove the last added button if there are any buttons
        if (this.ButtonModels.Count > 0)
        {
            this.ButtonModels.RemoveAt(this.ButtonModels.Count - 1);
        }
    }

    private void Button_Click(object sender, RoutedEventArgs e)
    {
        // Handle click events of each button
        var button = sender as Button;
        MessageBox.Show($"Clicked on {button?.Content}");
    }
}

public class ButtonModel(string text, string tooltip, ICommand? command)
{
    public ButtonModel() : this("Test", "Test", null)
    {
    }

    public string Text { get; init; } = text;
    public string Tooltip { get; init; } = tooltip;
    public ICommand? Command { get; init; } = command;
}