namespace Tech.Tevux.Dashboards.Controls;

public partial class ControlBase {
    public static readonly DependencyProperty TooltipTextProperty = DependencyProperty.Register(
        nameof(TooltipText),
        typeof(string),
        typeof(ControlBase),
        new PropertyMetadata(""));

    #region ITooltipProvider Members

    public string TooltipText {
        get { return (string)GetValue(TooltipTextProperty); }
        set { SetValue(TooltipTextProperty, value); }
    }

    #endregion
}
