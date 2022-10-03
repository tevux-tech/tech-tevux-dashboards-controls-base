using System.Collections.Generic;
using System.Windows.Media;

namespace Tech.Tevux.Dashboards.Controls;

public class Style {
    static Style() {
        Normal = new Style() { Type = Type.Normal, Foreground = Brushes.Black, Background = Brushes.LightGray };
        Passive = new Style() { Type = Type.Passive, Foreground = Brushes.Gray, Background = Brushes.LightGray };
        Selected = new Style() { Type = Type.Selected, Foreground = Brushes.White, Background = Brushes.Green };
        Warning = new Style() { Type = Type.Warning, Foreground = Brushes.Black, Background = Brushes.Orange };
        Error = new Style() { Type = Type.Error, Foreground = Brushes.White, Background = Brushes.Red };
    }

    public static Style Error { get; private set; }

    public static Style Normal { get; private set; }

    public static Style Passive { get; private set; }

    public static Style Selected { get; private set; }

    public static Style Warning { get; private set; }

    public Brush Background { get; private set; } = Brushes.Transparent;

    public Brush Foreground { get; private set; } = Brushes.Transparent;

    public string Name {
        get { return Type.ToString(); }
    }
    public Type Type { get; private set; }
    public static Style FromType(Type type) {
        switch (type) {
            case Type.Normal:
                return Normal;

            case Type.Passive:
                return Passive;

            case Type.Selected:
                return Selected;

            case Type.Warning:
                return Warning;

            case Type.Error:
                return Error;

            default:
                return Normal;
        }
    }

    public static List<Style> GetAllStyles() {
        return new List<Style>() { Normal, Passive, Selected, Warning, Error };
    }
}

public class AppearanceRule {
    private decimal? _decimalValue;
    private string _stringValue = "";
    public AppearanceRule() { }

    public AppearanceRule(Condition condition, Type type = Type.Warning, string textFormat = "") {
        Condition = condition;
        TextFormat = textFormat;
        Style = Style.FromType(type);
    }

    public AppearanceRule(Condition condition, decimal value, Type style = Type.Warning, string textFormat = "") : this(condition, style, textFormat) {
        _decimalValue = value;
        _stringValue = value.ToString(CultureInfo.InvariantCulture);
    }

    public AppearanceRule(Condition condition, string value, Type style = Type.Warning, string textFormat = "") : this(condition, style, textFormat) {
        Value = value;
    }

    /// <summary>
    /// A <see cref="Controls.Condition"/> used to evaluate whether input value fall under the rule.
    /// </summary>
    public Condition Condition { get; set; } = Condition.Equal;

    /// <summary>
    /// Style to use if the input value matches the rule.
    /// </summary>
    public Style Style { get; set; } = Style.Normal;
    /// <summary>
    /// Text format to use if the input value matches the rule.
    /// </summary>
    public string TextFormat { get; set; } = "";

    public string Value {
        get {
            return _stringValue;
        }
        set {
            if (decimal.TryParse(value, NumberStyles.Number, CultureInfo.InvariantCulture, out var number)) {
                _stringValue = value;
                _decimalValue = number;
            } else {
                _stringValue = value;
                _decimalValue = null;
            }
            _stringValue = value;

        }
    }

    public static bool TryParse(string rawString, out AppearanceRule rule) {
        var ruleParts = rawString.Split('|');

        if (ruleParts.Length < 3) { goto error; }

        if (TryParseCondition(ruleParts[0], out var condition) == false) { goto error; }
        if (TryParseStyle(ruleParts[2], out var style) == false) { goto error; }

        var format = "";
        if (ruleParts.Length > 3) {
            format = ruleParts[3];
        }

        if (decimal.TryParse(ruleParts[1], NumberStyles.Number, CultureInfo.InvariantCulture, out var number)) {
            rule = new AppearanceRule(condition, number, style, format);
        } else {
            rule = new AppearanceRule(condition, ruleParts[1], style, format);
        }

        return true;

    error:
        rule = new AppearanceRule();
        return false;
    }

    public bool Matches(decimal checkValue) {
        return Matches(checkValue, _decimalValue);
    }

    public bool Matches(string checkValue) {
        return Matches(checkValue, _stringValue);
    }

    public override string ToString() {
        var returnString = $"{ShortenCondition(Condition)}|{_stringValue}|{Style.Type}";

        if (string.IsNullOrEmpty(TextFormat) == false) { returnString += $"|{TextFormat}"; }

        return returnString;
    }

    protected static bool TryParseCondition(string rawString, out Condition condition) {
        var returnValue = true;

        switch (rawString.Trim().ToLower()) {
            case "equal":
            case "==":
                condition = Condition.Equal;
                break;

            case "notequal":
            case "!=":
                condition = Condition.NotEqual;
                break;

            case "lessthan":
            case "<":
                condition = Condition.LessThan;
                break;

            case "morethan":
            case ">":
                condition = Condition.MoreThan;
                break;

            case "lessthanorequal":
            case "<=":
                condition = Condition.LessThanOrEqual;
                break;

            case "morethanorequal":
            case ">=":
                condition = Condition.MoreThanOrEqual;
                break;

            case "bitset":
                condition = Condition.BitSet;
                break;

            case "bitnotset":
                condition = Condition.BitNotSet;
                break;

            default:
                condition = Condition.Undefined;
                returnValue = false;
                break;
        }

        return returnValue;
    }
    protected static bool TryParseStyle(string rawString, out Type style) {
        var returnValue = true;

        switch (rawString.Trim().ToLower()) {
            case "normal":
            case "norm":
            case "n":
                style = Type.Normal;
                break;

            case "passive":
            case "pass":
                style = Type.Passive;
                break;

            case "selected":
            case "sel":
                style = Type.Selected;
                break;

            case "warning":
            case "warn":
                style = Type.Warning;
                break;

            case "error":
            case "err":
                style = Type.Error;
                break;

            default:
                style = Type.Undefined;
                returnValue = false;
                break;
        }

        return returnValue;
    }

    private bool Matches(decimal x, decimal? y) {
        if (y.HasValue == false) { return false; }

        switch (Condition) {
            case Condition.Equal:
                return x == y;

            case Condition.NotEqual:
                return x != y;

            case Condition.LessThan:
                return x < y;

            case Condition.LessThanOrEqual:
                return x <= y;

            case Condition.MoreThan:
                return x > y;

            case Condition.MoreThanOrEqual:
                return x >= y;

            case Condition.BitSet:
                return ((((int)(x) >> (int)(y)) & 1) == 1);

            case Condition.BitNotSet:
                return ((((int)(x) >> (int)(y)) & 1) == 0);

            default:
                return false;
        }
    }
    private bool Matches(string x, string y) {
        switch (Condition) {
            case Condition.Equal:
                return x == y;

            case Condition.NotEqual:
                return x != y;

            default:
                return false;
        }
    }

    private static string ShortenCondition(Condition condition) {
        switch (condition) {
            case Condition.Equal:
                return "==";

            case Condition.NotEqual:
                return "!=";

            case Condition.LessThan:
                return "<";

            case Condition.LessThanOrEqual:
                return "<=";

            case Condition.MoreThan:
                return ">";

            case Condition.MoreThanOrEqual:
                return ">=";

            case Condition.BitSet:
                return "BitSet";

            case Condition.BitNotSet:
                return "BitNotSet";

            default:
                return "==";
        }
    }
}

//public class NumericAppearanceRule : AppearanceRule {
//    public NumericAppearanceRule() { }
//    public NumericAppearanceRule(Condition condition, decimal value, Style style = Style.Warning, string textFormat = "") : base(condition, style, textFormat) {
//        Value = value;
//    }

//    public decimal Value { get; set; } = 0m;

//    public static bool TryParse(string rawString, out NumericAppearanceRule rule) {
//        var ruleParts = rawString.Split('|');

//        if (ruleParts.Length < 3) { goto error; }

//        if (TryParseCondition(ruleParts[0], out var condition) == false) { goto error; }
//        if (decimal.TryParse(ruleParts[1], NumberStyles.Number, CultureInfo.InvariantCulture, out var number) == false) { goto error; }
//        if (TryParseStyle(ruleParts[2], out var style) == false) { goto error; }

//        var format = "";
//        if (ruleParts.Length > 3) {
//            format = ruleParts[3];
//        }

//        rule = new NumericAppearanceRule(condition, number, style, format);
//        return true;

//    error:
//        rule = new NumericAppearanceRule();
//        return false;
//    }

//    public bool Matches(decimal checkValue) {
//        return Matches(checkValue, Value);
//    }

//    private bool Matches(decimal x, decimal y) {
//        switch (Condition) {
//            case Condition.Equal:
//                return x == y;

//            case Condition.NotEqual:
//                return x != y;

//            case Condition.LessThan:
//                return x < y;

//            case Condition.LessThanOrEqual:
//                return x <= y;

//            case Condition.MoreThan:
//                return x > y;

//            case Condition.MoreThanOrEqual:
//                return x >= y;

//            case Condition.BitSet:
//                return ((((int)(x) >> (int)(y)) & 1) == 1);

//            case Condition.BitNotSet:
//                return ((((int)(x) >> (int)(y)) & 1) == 0);

//            default:
//                return false;
//        }
//    }
//}

//public class TextualAppearanceRule : AppearanceRule {
//    public TextualAppearanceRule() { }
//    public TextualAppearanceRule(Condition condition, string value, Style style = Style.Warning, string textFormat = "") : base(condition, style, textFormat) {
//        Value = value;
//    }

//    public string Value { get; private set; } = "";

//    public static bool TryParse(string rawString, out TextualAppearanceRule rule) {
//        var ruleParts = rawString.Split('|');

//        if (ruleParts.Length < 3) { goto error; }

//        if (TryParseCondition(ruleParts[0], out var condition) == false) { goto error; }
//        if (TryParseStyle(ruleParts[2], out var style) == false) { goto error; }

//        var format = "";
//        if (ruleParts.Length > 3) {
//            format = ruleParts[3];
//        }

//        rule = new TextualAppearanceRule(condition, ruleParts[1], style, format);
//        return true;

//    error:
//        rule = new TextualAppearanceRule();
//        return false;
//    }

//    public bool Matches(string checkValue) {
//        return Matches(checkValue, Value);
//    }

//    private bool Matches(string x, string y) {
//        switch (Condition) {
//            case Condition.Equal:
//                return x == y;

//            case Condition.NotEqual:
//                return x != y;

//            default:
//                return false;
//        }
//    }
//}
