namespace Tech.Tevux.Dashboards.Controls;

public partial class NumericInputControlBase {
    public static readonly DependencyProperty StepProperty = DependencyProperty.Register(
        nameof(Step),
        typeof(decimal),
        typeof(NumericInputControlBase),
        new PropertyMetadata(1m, (d, e) => { ((NumericInputControlBase)d).Reconfigure(); }), value => {
            var isValid = false;

            if (value is decimal number) {
                if (number > 0) {
                    isValid = true;
                }
            }

            return isValid;
        });

    [ExposedOption(OptionType.Number)]
    [Category(OptionCategory.Main)]
    public decimal Step {
        get { return (decimal)GetValue(StepProperty); }
        set { SetValue(StepProperty, value); }
    }
}
