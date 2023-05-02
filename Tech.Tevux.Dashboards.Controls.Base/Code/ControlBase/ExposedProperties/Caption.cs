namespace Tech.Tevux.Dashboards.Controls;

public partial class ControlBase {
    public static readonly DependencyProperty CaptionProperty = DependencyProperty.Register(
        nameof(Caption),
        typeof(string),
        typeof(ControlBase),
        new PropertyMetadata("...", (d, e) => {
            ((ControlBase)d).Reconfigure();
        }));

    [ExposedOption(OptionType.SingleLineText)]
    [Category(OptionCategory.Main)]
    public string Caption {
        get { return (string)GetValue(CaptionProperty); }
        set { SetValue(CaptionProperty, value); }
    }
}
