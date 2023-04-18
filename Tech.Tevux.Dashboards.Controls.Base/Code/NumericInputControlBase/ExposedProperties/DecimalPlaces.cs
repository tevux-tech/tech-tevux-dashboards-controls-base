namespace Tech.Tevux.Dashboards.Controls;

public partial class NumericInputControlBase {
    public static readonly DependencyProperty DecimalPlacesProperty = DependencyProperty.Register(
       nameof(DecimalPlaces),
       typeof(int),
       typeof(NumericInputControlBase),
       new PropertyMetadata(0),
       value => {
           var isValid = false;

           if (value is int decimalPlaces) {
               if (decimalPlaces >= 0) {
                   isValid = true;
               }
           }

           return isValid;
       });

    [ExposedOption(OptionType.Number)]
    [DisplayName("Decimal places")]
    [Category(OptionCategory.Visuals)]
    public int DecimalPlaces {
        get { return (int)GetValue(DecimalPlacesProperty); }
        set { SetValue(DecimalPlacesProperty, value); }
    }
}
