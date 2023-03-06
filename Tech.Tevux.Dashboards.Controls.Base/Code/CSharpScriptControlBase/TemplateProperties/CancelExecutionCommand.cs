using System.Windows.Input;

namespace Tech.Tevux.Dashboards.Controls;

public partial class CSharpScriptControlBase {
    public static readonly DependencyProperty CancelExecutionProperty = DependencyProperty.Register(
        nameof(CancelExecutionCommand),
        typeof(ICommand),
        typeof(CSharpScriptControlBase),
        new PropertyMetadata(null));

    public ICommand CancelExecutionCommand {
        get { return (ICommand)GetValue(CancelExecutionProperty); }
        set { SetValue(CancelExecutionProperty, value); }
    }
}
