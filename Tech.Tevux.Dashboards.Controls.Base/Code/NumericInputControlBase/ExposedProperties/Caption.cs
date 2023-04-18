namespace Tech.Tevux.Dashboards.Controls;

public partial class NumericInputControlBase {
    public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register(
        nameof(Caption),
        typeof(string),
        typeof(NumericInputControlBase),
        new PropertyMetadata("..."));

    [ExposedOption(OptionType.SingleLineText)]
    [Category(OptionCategory.Main)]
    public string Caption {
        get { return (string)GetValue(CaptionProperty); }
        set { SetValue(CaptionProperty, value); }
    }
}
