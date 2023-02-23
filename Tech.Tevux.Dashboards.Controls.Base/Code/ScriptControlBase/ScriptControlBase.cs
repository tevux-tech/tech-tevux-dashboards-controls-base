namespace Tech.Tevux.Dashboards.Controls;

public partial class ScriptControlBase : ControlBase {
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
    static ScriptControlBase() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ScriptControlBase), new FrameworkPropertyMetadata(typeof(ScriptControlBase)));
    }
}
