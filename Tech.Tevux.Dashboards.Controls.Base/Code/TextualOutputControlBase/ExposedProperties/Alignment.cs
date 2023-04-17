namespace Tech.Tevux.Dashboards.Controls;

public partial class TextualOutputControlBase {
    public static readonly DependencyProperty AlignmentProperty = DependencyProperty.Register(
        nameof(Alignment),
        typeof(string),
        typeof(TextualOutputControlBase),
        new PropertyMetadata("Middle"));

    [ExposedOption(OptionType.ChoiceText, choices: new[] { "Left", "Top", "Right", "Bottom", "Middle" })]
    [Category(OptionCategory.Visuals)]
    public string Alignment {
        get { return (string)GetValue(AlignmentProperty); }
        set { SetValue(AlignmentProperty, value); }
    }
}
