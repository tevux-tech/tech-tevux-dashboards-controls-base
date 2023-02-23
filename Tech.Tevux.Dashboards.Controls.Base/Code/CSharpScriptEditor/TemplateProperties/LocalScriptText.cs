namespace Tech.Tevux.Dashboards.Controls;

public partial class CSharpScriptEditor {
    public static readonly DependencyProperty LocalScriptTextProperty = DependencyProperty.Register(
        nameof(LocalScriptText),
        typeof(string),
        typeof(CSharpScriptEditor),
        new PropertyMetadata("", (d, e) => {
            (d as CSharpScriptEditor)?.HandleScriptChange(e.NewValue);
        }));

    public string LocalScriptText {
        get { return (string)GetValue(LocalScriptTextProperty); }
        set { SetValue(LocalScriptTextProperty, value); }
    }
}
