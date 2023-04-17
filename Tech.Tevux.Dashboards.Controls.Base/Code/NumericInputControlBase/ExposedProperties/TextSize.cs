﻿namespace Tech.Tevux.Dashboards.Controls;

public partial class NumericInputControlBase {
    public static readonly DependencyProperty TextSizeProperty = DependencyProperty.Register(
        nameof(TextSize),
        typeof(double),
        typeof(NumericInputControlBase),
        new PropertyMetadata(15.0));

    [ExposedOption(OptionType.Number)]
    [Category(OptionCategory.Visuals)]
    public double TextSize {
        get { return (double)GetValue(TextSizeProperty); }
        set { SetValue(TextSizeProperty, value); }
    }
}
