using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using DevBot9.Mvvm;

namespace Tech.Tevux.Dashboards.Controls;

[OptionEditor(typeof(OutputControlBase), "Rule editor")]
public partial class RulesEditor : Control, IDisposable {
    private readonly CancellationTokenSource _globalCts = new();
    private bool _isDisposed = false;
    private DataGrid? _ruleDataGrid;
    private readonly OutputControlBase? _ruleSourceControl;
    static RulesEditor() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(RulesEditor), new FrameworkPropertyMetadata(typeof(RulesEditor)));
    }

    public RulesEditor() {
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) { return; }
    }

    public RulesEditor(OutputControlBase control) : this() {
        _ruleSourceControl = control;

        AppearanceRules = new ObservableCollection<AppearanceRule>();
        var splitRules = _ruleSourceControl.Rules.Replace("\r", "").Split('\n');
        foreach (var ruleString in splitRules) {
            if (AppearanceRule.TryParse(ruleString, out var parsedRule)) {
                AppearanceRules.Add(parsedRule);
            }
        }

        ExecuteGuiCommand = new DelegateCommand<string>(argument => {
            switch (argument) {
                case "add":
                    AppearanceRules.Add(new AppearanceRule());
                    break;

                case "remove":
                    if (_ruleDataGrid is not null) {
                        AppearanceRules.Remove((AppearanceRule)_ruleDataGrid.SelectedItem);
                    }
                    break;
            }
        }, argument => {
            switch (argument) {
                case "add":
                    return true;

                case "remove":
                    if (_ruleDataGrid is not null) {
                        return _ruleDataGrid.SelectedIndex > -1;
                    } else {
                        return false;
                    }

                default:
                    return false;
            }
        });

        Task.Run(async () => {
            while (_globalCts.IsCancellationRequested == false) {
                Dispatcher.Invoke(() => {
                    // There no way of detecting changes in current DataGrid row with current template (which is fast and requires less clicking than the dfault one).
                    // So, periodically updating source. This, however, means, that manually editing source will not work - all changes will be overwritten by this timer.
                    var localConvertedRules = "";
                    foreach (var rule in AppearanceRules) {
                        localConvertedRules += rule.ToString() + "\r\n";
                    }
                    localConvertedRules = localConvertedRules.TrimEnd();

                    if (localConvertedRules != _ruleSourceControl.Rules) {
                        _ruleSourceControl.Rules = localConvertedRules;
                    }
                });

                await Task.Delay(300, _globalCts.Token).ContinueWith(task => { });
            }
        });
    }

    public List<Style> AllStyles { get; private set; } = Controls.Style.GetAllStyles();
    public void Dispose() {
        // A good article explaining how to implement Dispose. https://docs.microsoft.com/en-us/dotnet/standard/garbage-collection/implementing-dispose
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public override void OnApplyTemplate() {
        base.OnApplyTemplate();

        if (Template.FindName("PART_DataGrid", this) is DataGrid dataGrid) {
            _ruleDataGrid = dataGrid;
        }
    }
    protected virtual void Dispose(bool isCalledManually) {
        if (_isDisposed == false) {
            if (isCalledManually) {
                _globalCts.Cancel();
                // Dispose managed objects here.
            }

            // Free unmanaged resources here and set large fields to null.

            _isDisposed = true;
        }
    }
}
