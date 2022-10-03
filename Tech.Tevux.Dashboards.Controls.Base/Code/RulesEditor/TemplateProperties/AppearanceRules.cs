using System.Collections.ObjectModel;

namespace Tech.Tevux.Dashboards.Controls;

public partial class RulesEditor {
    public static readonly DependencyProperty NumericRulesProperty = DependencyProperty.Register(
        nameof(AppearanceRules),
        typeof(ObservableCollection<AppearanceRule>),
        typeof(RulesEditor),
        new PropertyMetadata(null));

    public ObservableCollection<AppearanceRule> AppearanceRules {
        get { return (ObservableCollection<AppearanceRule>)GetValue(NumericRulesProperty); }
        set { SetValue(NumericRulesProperty, value); }
    }
}
