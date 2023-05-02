﻿using System.Windows.Media;

namespace Tech.Tevux.Dashboards.Controls;
[HideExposedOption(nameof(Caption))]
public partial class TextualOutputControlBase : ControlBase, ITextualOutputControl, IConditionalTextualOutputControl {
    private readonly object _rulesLock = new();

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

    public override void Reconfigure() {
        base.Reconfigure();

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

        CombinedFormat = $"{Prefix}{{0}}{Suffix}";

        // Reformatting last used value.       
        ApplyAppearanceRules();
    }

    protected void ApplyAppearanceRules() {
        var ruleApplied = false;
        CombinedFormat = $"{Prefix}{{0}}{Suffix}";
        lock (_rulesLock) {
            // Trying to apply color rules first.
            foreach (var appearanceRule in AppearanceRules) {
                if (appearanceRule.Matches(TextualValue)) {
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

        OutputText = string.Format(CultureInfo.InvariantCulture, CombinedFormat, TextualValue);
    }

    #region IDisposable

    private bool _isDisposed;

    protected override void Dispose(bool isCalledManually) {
        if (_isDisposed == false) {
            if (isCalledManually) {
                // Dispose managed objects here.
            }

            // Free unmanaged resources here and set large fields to null.

            _isDisposed = true;
        }

        base.Dispose(isCalledManually);
    }

    #endregion
}
