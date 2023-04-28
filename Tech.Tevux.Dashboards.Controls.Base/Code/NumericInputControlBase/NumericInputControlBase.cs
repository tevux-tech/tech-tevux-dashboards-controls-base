namespace Tech.Tevux.Dashboards.Controls;

[HideExposedOption(nameof(Caption))]
public partial class NumericInputControlBase : ControlBase, INumericControl, INumericInputControl, INumericIncrementableControl, IDisposable, IErrorMessageProviderControl {
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
