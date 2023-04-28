namespace Tech.Tevux.Dashboards.Controls;

public partial class NumericOutputControlBase {
    public static readonly DependencyProperty ErrorMessageProperty = DependencyProperty.Register(
        nameof(ErrorMessage),
        typeof(string),
        typeof(NumericOutputControlBase),
        new PropertyMetadata(""));

    public string ErrorMessage {
        get { return (string)GetValue(ErrorMessageProperty); }
        set { SetValue(ErrorMessageProperty, value); }
    }
}
