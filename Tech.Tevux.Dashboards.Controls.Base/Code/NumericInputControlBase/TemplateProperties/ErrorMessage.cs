namespace Tech.Tevux.Dashboards.Controls;

public partial class NumericInputControlBase {
    public static readonly DependencyProperty ErrorMessageProperty = DependencyProperty.Register(
        nameof(ErrorMessage),
        typeof(string),
        typeof(NumericInputControlBase),
        new PropertyMetadata(""));

    public string ErrorMessage {
        get { return (string)GetValue(ErrorMessageProperty); }
        set { SetValue(ErrorMessageProperty, value); }
    }
}
