namespace Tech.Tevux.Dashboards.Controls;

public partial class InputOutputControlBase {
    public static readonly DependencyProperty StepProperty = DependencyProperty.Register(
        nameof(Step),
        typeof(decimal),
        typeof(InputOutputControlBase),
        new PropertyMetadata(1m), value => {
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
