using DevBot9.Mvvm;

namespace Tech.Tevux.Dashboards.Controls;

public partial class RulesEditor {
    public static readonly DependencyProperty ExecuteGuiCommandProperty = DependencyProperty.Register(
        nameof(ExecuteGuiCommand),
        typeof(DelegateCommand<string>),
        typeof(RulesEditor),
        new PropertyMetadata(null));

    public DelegateCommand<string> ExecuteGuiCommand {
        get { return (DelegateCommand<string>)GetValue(ExecuteGuiCommandProperty); }
        set { SetValue(ExecuteGuiCommandProperty, value); }
    }
}
