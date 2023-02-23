using System.Windows.Input;

namespace Tech.Tevux.Dashboards.Controls;

public partial class CSharpScriptControlBase {
    public static readonly DependencyProperty ExecuteCommandProperty = DependencyProperty.Register(
        nameof(ExecuteCommand),
        typeof(ICommand),
        typeof(CSharpScriptControlBase),
        new PropertyMetadata(null));

    public ICommand ExecuteCommand {
        get { return (ICommand)GetValue(ExecuteCommandProperty); }
        set { SetValue(ExecuteCommandProperty, value); }
    }
}
