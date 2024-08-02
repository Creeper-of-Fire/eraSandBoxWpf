// using System.ComponentModel;
// using System.Windows;
// using System.Windows.Controls;
// using System.Windows.Controls.Primitives;
// using System.Windows.Data;
// using System.Windows.Documents;
// using System.Windows.Input;
// using HandyControl.Controls;
// using TextBox = HandyControl.Controls.TextBox;
//
// namespace eraSandBoxWpf.Controls;
//
// public class NumericUpDownWithEnter : NumericUpDown, ICommandSource
// {
//     private TextBox? _textBox;
//     private const string ElementTextBox = "PART_TextBox";
//
//
//     public override void OnApplyTemplate()
//     {
//         if (this._textBox != null)
//         {
//             this._textBox.PreviewKeyDown -= this.TxtInput_PreviewKeyDown;
//             // this._textBox.TextChanged -= TextBox_TextChanged;
//             // this._textBox.LostFocus -= TextBox_LostFocus;
//         }
//
//         base.OnApplyTemplate();
//
//         this._textBox = this.GetTemplateChild(ElementTextBox) as TextBox;
//
//         if (this._textBox == null) return;
//
//         this._textBox.SetBinding(SelectionBrushProperty, new Binding(SelectionBrushProperty.Name) { Source = this });
// #if NET48_OR_GREATER
//             _textBox.SetBinding(SelectionTextBrushProperty, new Binding(SelectionTextBrushProperty.Name) { Source =
//  this });
// #endif
//         this._textBox.SetBinding(SelectionOpacityProperty,
//             new Binding(SelectionOpacityProperty.Name) { Source = this });
//         this._textBox.SetBinding(CaretBrushProperty, new Binding(CaretBrushProperty.Name) { Source = this });
//         this._textBox.PreviewKeyDown += this.TxtInput_PreviewKeyDown;
//         // this._textBox.TextChanged += TextBox_TextChanged;
//         // this._textBox.LostFocus += TextBox_LostFocus;
//         // this._textBox.Text = CurrentText;
//     }
//
//     private void TxtInput_PreviewKeyDown(object sender, KeyEventArgs e)
//     {
//         if (this.IsReadOnly) return;
//         if (e.Key == Key.Enter)
//         {
//             Command.Execute(e);
//         }
//     }
//
//     #region ICommandSource
//
//     /// <summary>
//     ///     The DependencyProperty for RoutedCommand
//     /// </summary>
//     public static readonly DependencyProperty CommandProperty =
//         DependencyProperty.Register(
//             nameof(Command),
//             typeof(ICommand),
//             typeof(NumericUpDownWithEnter),
//             new FrameworkPropertyMetadata(null,
//                 OnCommandChanged));
//
//     /// <summary>
//     /// Get or set the Command property
//     /// </summary>
//     [Bindable(true), Category("Action")]
//     [Localizability(LocalizationCategory.NeverLocalize)]
//     public ICommand Command
//     {
//         get => (ICommand)this.GetValue(CommandProperty);
//         set => this.SetValue(CommandProperty, value);
//     }
//
//     private static void OnCommandChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//     {
//         var h = (NumericUpDownWithEnter)d;
//         h.OnCommandChanged((ICommand)e.OldValue, (ICommand)e.NewValue);
//     }
//
//     private void OnCommandChanged(ICommand oldCommand, ICommand newCommand)
//     {
//         if (oldCommand != null)
//         {
//             this.UnhookCommand(oldCommand);
//         }
//
//         if (newCommand != null)
//         {
//             this.HookCommand(newCommand);
//         }
//     }
//
//     private void UnhookCommand(ICommand command)
//     {
//         CanExecuteChangedEventManager.RemoveHandler(command, this.OnCanExecuteChanged);
//         this.UpdateCanExecute();
//     }
//
//     private void HookCommand(ICommand command)
//     {
//         CanExecuteChangedEventManager.AddHandler(command, this.OnCanExecuteChanged);
//         this.UpdateCanExecute();
//     }
//
//     private void OnCanExecuteChanged(object sender, EventArgs e)
//     {
//         this.UpdateCanExecute();
//     }
//
//     private void UpdateCanExecute()
//     {
//         if (this.Command != null)
//         {
//             this.CanExecute = CanExecuteCommandSource(this);
//         }
//         else
//         {
//             this.CanExecute = true;
//         }
//     }
//
//     internal static bool CanExecuteCommandSource(ICommandSource commandSource)
//     {
//         ICommand command = commandSource.Command;
//         if (command != null)
//         {
//             object parameter = commandSource.CommandParameter;
//             IInputElement target = commandSource.CommandTarget;
//
//             RoutedCommand routed = command as RoutedCommand;
//             if (routed != null)
//             {
//                 if (target == null)
//                 {
//                     target = commandSource as IInputElement;
//                 }
//
//                 return routed.CanExecute(parameter, target);
//             }
//             else
//             {
//                 return command.CanExecute(parameter);
//             }
//         }
//
//         return false;
//     }
//
//     private bool _canExecute = true;
//
//     private bool CanExecute
//     {
//         get => this._canExecute;
//         set
//         {
//             if (this._canExecute != value)
//             {
//                 this._canExecute = value;
//                 this.CoerceValue(IsEnabledProperty);
//             }
//         }
//     }
//
//     /// <summary>
//     ///     Fetches the value of the IsEnabled property
//     /// </summary>
//     /// <remarks>
//     ///     The reason this property is overridden is so that Hyperlink
//     ///     can infuse the value for CanExecute into it.
//     /// </remarks>
//     protected override bool IsEnabledCore => base.IsEnabledCore && this.CanExecute;
//
//     /// <summary>
//     /// The DependencyProperty for the CommandParameter
//     /// </summary>
//     public static readonly DependencyProperty CommandParameterProperty =
//         DependencyProperty.Register(
//             nameof(CommandParameter),
//             typeof(object),
//             typeof(NumericUpDownWithEnter),
//             new FrameworkPropertyMetadata(
//                 null,
//                 OnCommandParameterChanged));
//
//     /// <summary>
//     /// Reflects the parameter to pass to the CommandProperty upon execution.
//     /// </summary>
//     [Bindable(true), Category("Action")]
//     [Localizability(LocalizationCategory.NeverLocalize)]
//     public object CommandParameter
//     {
//         get => this.GetValue(CommandParameterProperty);
//         set => this.SetValue(CommandParameterProperty, value);
//     }
//
//     private static void OnCommandParameterChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
//     {
//         var h = (NumericUpDownWithEnter)d;
//         h.UpdateCanExecute();
//     }
//
//     /// <summary>
//     ///     The DependencyProperty for Target property
//     ///     Flags:              None
//     ///     Default Value:      null
//     /// </summary>
//     public static readonly DependencyProperty CommandTargetProperty =
//         DependencyProperty.Register(
//             nameof(CommandTarget),
//             typeof(IInputElement),
//             typeof(NumericUpDownWithEnter),
//             new FrameworkPropertyMetadata((IInputElement)null));
//
//     /// <summary>
//     ///     The target element on which to fire the command.
//     /// </summary>
//     [Bindable(true), Category("Action")]
//     public IInputElement CommandTarget
//     {
//         get => (IInputElement)this.GetValue(CommandTargetProperty);
//         set => this.SetValue(CommandTargetProperty, value);
//     }
//
//     #endregion
// }