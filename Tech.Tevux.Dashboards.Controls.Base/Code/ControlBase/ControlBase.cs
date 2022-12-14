using System.Windows.Controls;

namespace Tech.Tevux.Dashboards.Controls;

public partial class ControlBase : Control, IEridanusControl {
    private bool _isDisposed = false;

    public event PropertyChangedEventHandler? PropertyChanged = delegate { };

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
}
