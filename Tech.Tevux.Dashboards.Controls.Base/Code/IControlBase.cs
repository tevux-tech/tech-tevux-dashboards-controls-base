namespace Tech.Tevux.Dashboards.Controls;
// ? IDisposable ?
public interface IControlBase {
    string Alignment { get; set; }
    string Caption { get; set; }
    double TextSize { get; set; }
}

public interface ITooltipProvider {
    string TooltipText { get; set; }
}

public interface IErrorMessageProvider {
    string ErrorMessage { get; set; }
}

public interface INumericInputControlBase {
    decimal Minimum { get; set; }
    decimal Maximum { get; set; }
    decimal Step { get; set; }
}

public interface IOutputControlBase {
    string Format { get; set; }
    string Prefix { get; set; }
    string Suffix { get; set; }
    string OutputText { get; set; } // ?..
}

public interface IConditionalOutputBase {
    string Rules { get; set; }
}

public interface ICSharpScriptControlBase {
    string Script { get; set; }
}
