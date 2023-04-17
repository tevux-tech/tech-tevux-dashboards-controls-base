namespace Tech.Tevux.Dashboards.Controls;
public partial class TextualOutputControlBase : Control, IControlBase, IOutputControlBase, IDisposable, IErrorMessageProvider, IConditionalOutputBase {


    private readonly object _rulesLock = new();

    // Cached values only using for reconfiguration purposes. If value updates come in rarely,
    // modifying formatting does not result in immediate visual changes. That is annoying,
    // so using cached values to temporarily simulate "new" incoming values.
    private decimal _cachedDecimalValue = 0m;

    private string _cachedStringValue = "...";

    protected List<AppearanceRule> AppearanceRules { get; } = new();

    public virtual void Reconfigure() {
        lock (_rulesLock) {
            var rules = Rules.Replace("\r", "").Split('\n');

            AppearanceRules.Clear();
            foreach (var ruleString in rules) {
                if (AppearanceRule.TryParse(ruleString, out var parsedRule)) { AppearanceRules.Add(parsedRule); }
            }
        }

        CombinedFormat = $"{Prefix}{{0:{Format}}}{Suffix}";

        // Reformatting last used value. Strings are used way more rarely.
        // So if it is not empty, control is definitely sending strings. Otherwise - decimals.
        if (string.IsNullOrEmpty(_cachedStringValue) == false) {
            ApplyAppearanceRules(_cachedStringValue);
        } else {
            ApplyAppearanceRules(_cachedDecimalValue);
        }
    }

    protected virtual void ApplyAppearanceRules(decimal number) {
        _cachedStringValue = "";
        _cachedDecimalValue = number;
        var ruleApplied = false;
        CombinedFormat = $"{Prefix}{{0:{Format}}}{Suffix}";

        lock (_rulesLock) {
            foreach (var appearanceRule in AppearanceRules) {
                if (appearanceRule.Matches(number)) {
                    Background = appearanceRule.Style.Background;
                    Foreground = appearanceRule.Style.Foreground;

                    // User may have not specified text format for the rule; using default text format then.
                    if (string.IsNullOrEmpty(appearanceRule.TextFormat) == false) {
                        CombinedFormat = appearanceRule.TextFormat;
                    }

                    ruleApplied = true;
                }
            }
        }

        // If the rules weren't applied, reverting back to default visual properties.
        if (ruleApplied == false) {
            Background = Controls.Style.Normal.Background;
            Foreground = Controls.Style.Normal.Foreground;
        }

        try {
            Caption = string.Format(CultureInfo.InvariantCulture, CombinedFormat, number);
        } catch (FormatException) {
            Caption = "@FormatError@";
        }
    }

    protected virtual void ApplyAppearanceRules(string text) {
        _cachedStringValue = text;
        _cachedDecimalValue = 0;
        var ruleApplied = false;
        CombinedFormat = $"{Prefix}{{0:{Format}}}{Suffix}";
        lock (_rulesLock) {
            // Trying to apply color rules first.
            foreach (var appearanceRule in AppearanceRules) {
                if (appearanceRule.Matches(text)) {
                    Background = appearanceRule.Style.Background;
                    Foreground = appearanceRule.Style.Foreground;

                    if (string.IsNullOrEmpty(appearanceRule.TextFormat) == false) {
                        CombinedFormat = appearanceRule.TextFormat;
                    }

                    ruleApplied = true;
                }
            }
        }

        // If the rules weren't applied, reverting back to default visual properties.
        if (ruleApplied == false) {
            Background = Controls.Style.Normal.Background;
            Foreground = Controls.Style.Normal.Foreground;
        }

        Caption = string.Format(CultureInfo.InvariantCulture, CombinedFormat, text);
    }

    #region IDisposable

    private bool _isDisposed;

    public void Dispose() {
        // A good article explaining how to implement Dispose. https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool isCalledManually) {
        if (_isDisposed == false) {
            if (isCalledManually) {
                // Dispose managed objects here.
            }

            // Free unmanaged resources here and set large fields to null.

            _isDisposed = true;
        }
    }

    #endregion
}
