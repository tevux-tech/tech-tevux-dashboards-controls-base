using System.Windows.Media;

namespace Tech.Tevux.Dashboards.Controls;
public partial class TextualOutputControlBase : Control, IBasicControl, ITextualOutputControl, IDisposable, IErrorMessageProviderControl, IConditionalTextualOutputControl {


    private readonly object _rulesLock = new();

    // Cached values only using for reconfiguration purposes. If value updates come in rarely,
    // modifying formatting does not result in immediate visual changes. That is annoying,
    // so using cached values to temporarily simulate "new" incoming values.
    private decimal _cachedDecimalValue = 0m;

    private string _cachedStringValue = "...";
    private readonly Dictionary<uint, SolidColorBrush> _backgroundBrushCache = new();
    private readonly Dictionary<uint, SolidColorBrush> _foregroundBrushCache = new();
    private readonly SolidColorBrush _defaultBackgroundBrush;
    private readonly SolidColorBrush _defaultForegroundBrush;


    protected List<AppearanceRule> AppearanceRules { get; } = new();

    public TextualOutputControlBase() {
        var backgroundBytes = BitConverter.GetBytes(AppearanceRuleStyle.Normal.Background);
        _defaultBackgroundBrush = new SolidColorBrush(Color.FromArgb(backgroundBytes[3], backgroundBytes[2], backgroundBytes[1], backgroundBytes[0]));
        _defaultBackgroundBrush.Freeze();

        var foregroundBytes = BitConverter.GetBytes(AppearanceRuleStyle.Normal.Foreground);
        _defaultForegroundBrush = new SolidColorBrush(Color.FromArgb(foregroundBytes[3], foregroundBytes[2], foregroundBytes[1], foregroundBytes[0]));
        _defaultForegroundBrush.Freeze();
    }

    public virtual void Reconfigure() {
        lock (_rulesLock) {
            var rules = Rules.Replace("\r", "").Split('\n');

            AppearanceRules.Clear();
            _backgroundBrushCache.Clear();
            _foregroundBrushCache.Clear();
            foreach (var ruleString in rules) {
                if (AppearanceRule.TryParse(ruleString, out var parsedRule)) {
                    AppearanceRules.Add(parsedRule);

                    var backgroundBytes = BitConverter.GetBytes(parsedRule.Style.Background);
                    var backgroundBrush = new SolidColorBrush(Color.FromArgb(backgroundBytes[3], backgroundBytes[2], backgroundBytes[1], backgroundBytes[0]));
                    backgroundBrush.Freeze();
                    _backgroundBrushCache[parsedRule.Style.Background] = backgroundBrush;

                    var foregroundBytes = BitConverter.GetBytes(parsedRule.Style.Foreground);
                    var foregroundBrush = new SolidColorBrush(Color.FromArgb(foregroundBytes[3], foregroundBytes[2], foregroundBytes[1], foregroundBytes[0]));
                    foregroundBrush.Freeze();
                    _foregroundBrushCache[parsedRule.Style.Foreground] = foregroundBrush;
                }
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

    protected void ApplyAppearanceRules(decimal number) {
        _cachedStringValue = "";
        _cachedDecimalValue = number;
        var ruleApplied = false;
        CombinedFormat = $"{Prefix}{{0:{Format}}}{Suffix}";

        lock (_rulesLock) {
            foreach (var appearanceRule in AppearanceRules) {
                if (appearanceRule.Matches(number)) {
                    Background = _backgroundBrushCache[appearanceRule.Style.Background];
                    Foreground = _foregroundBrushCache[appearanceRule.Style.Foreground];

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
            Background = _defaultBackgroundBrush;
            Foreground = _defaultForegroundBrush;
        }

        try {
            OutputText = string.Format(CultureInfo.InvariantCulture, CombinedFormat, number);
        } catch (FormatException) {
            OutputText = "@FormatError@";
        }
    }

    protected void ApplyAppearanceRules(string text) {
        _cachedStringValue = text;
        _cachedDecimalValue = 0;
        var ruleApplied = false;
        CombinedFormat = $"{Prefix}{{0:{Format}}}{Suffix}";
        lock (_rulesLock) {
            // Trying to apply color rules first.
            foreach (var appearanceRule in AppearanceRules) {
                if (appearanceRule.Matches(text)) {
                    Background = _backgroundBrushCache[appearanceRule.Style.Background];
                    Foreground = _foregroundBrushCache[appearanceRule.Style.Foreground];

                    if (string.IsNullOrEmpty(appearanceRule.TextFormat) == false) {
                        CombinedFormat = appearanceRule.TextFormat;
                    }

                    ruleApplied = true;
                }
            }
        }

        // If the rules weren't applied, reverting back to default visual properties.
        if (ruleApplied == false) {
            Background = _defaultBackgroundBrush;
            Foreground = _defaultForegroundBrush;
        }

        OutputText = string.Format(CultureInfo.InvariantCulture, CombinedFormat, text);
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
