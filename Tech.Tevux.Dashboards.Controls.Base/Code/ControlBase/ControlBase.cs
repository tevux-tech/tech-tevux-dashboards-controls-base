namespace Tech.Tevux.Dashboards.Controls;

public partial class ControlBase : ContentControl, IDisposable, IBasicControl, IErrorMessageProviderControl, ITooltipProvider {
    public virtual void Reconfigure() {
        // Nothing to do for this class. But those deriving from this, should override Reconfigure method.
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

    protected ControlBase() {
        DataContext = this;
        SetBinding(FontSizeProperty, "TextSize");
    }

    #endregion
}
