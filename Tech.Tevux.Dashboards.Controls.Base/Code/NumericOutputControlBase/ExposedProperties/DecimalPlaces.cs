namespace Tech.Tevux.Dashboards.Controls;

public partial class NumericOutputControlBase {
    public static readonly DependencyProperty DecimalPlacesProperty = DependencyProperty.Register(
       nameof(DecimalPlaces),
       typeof(int),
       typeof(NumericOutputControlBase),
       new PropertyMetadata(0, (d, e) => {
           (d as NumericOutputControlBase)?.Reconfigure();
       }),
       value => {
           var isValid = false;

           if (value is int decimalPlaces) {
               if (decimalPlaces >= 0) {
                   isValid = true;
               }
           }

           return isValid;
       });

    [ExposedNumber]
    [DisplayName("Decimal places")]
    [Category(OptionCategory.Visuals)]
    public int DecimalPlaces {
        get { return (int)GetValue(DecimalPlacesProperty); }
        set { SetValue(DecimalPlacesProperty, value); }
    }
}
