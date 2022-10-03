namespace Tech.Tevux.Dashboards.Controls;

/// <summary>
/// Condition used to check input value against the rule.
/// </summary>
public enum Condition {
    Undefined,
    Equal,
    NotEqual,
    MoreThan,
    MoreThanOrEqual,
    LessThan,
    LessThanOrEqual,
    BitSet,
    BitNotSet
}
