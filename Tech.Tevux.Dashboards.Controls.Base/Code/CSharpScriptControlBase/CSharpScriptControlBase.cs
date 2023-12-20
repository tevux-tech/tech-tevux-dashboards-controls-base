using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.CodeAnalysis.Scripting.Hosting;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Loader;

namespace Tech.Tevux.Dashboards.Controls;

public partial class CSharpScriptControlBase : ControlBase, ICSharpScriptControl {
    [SuppressMessage("Usage", "CA2213:Disposable fields should be disposed", Justification = "https://stackoverflow.com/questions/6960520/when-to-dispose-cancellationtokensource")]
    private readonly CancellationTokenSource _globalCts = new();

    private bool _isDisposed;

    static CSharpScriptControlBase() {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(CSharpScriptControlBase), new FrameworkPropertyMetadata(typeof(CSharpScriptControlBase)));
        CaptionProperty.OverrideMetadata(typeof(CSharpScriptControlBase), new FrameworkPropertyMetadata("Click me"));
    }

    public CSharpScriptControlBase() {
        ExecuteCommand = new AsyncCommand(() => Task.Factory.StartNew(Execute));
        CancelExecutionCommand = new AsyncCommand(() => Task.Factory.StartNew(CancelExecution), CanCancelExecutionCommand);
    }

    protected AssemblyLoadContext AssemblyLoadContext { get; set; } = AssemblyLoadContext.Default;
    protected ScriptContextBase ScriptContext { get; set; } = new EmptyScriptContextBase();

    public bool CanCancelExecutionCommand() {
        return ((AsyncCommand)ExecuteCommand).IsExecuting;
    }

    public void CancelExecution() {
        ScriptContext.IsCancellationRequested = true;
    }

    public void Execute() {
        var script = "";
        var imports = "";
        var errorMessage = "";

        Dispatcher.Invoke(() => {
            script = Script;
            imports = Imports;
        });

        Task.Run(() => {
            ExecuteScript(script, imports, AssemblyLoadContext, ScriptContext, out errorMessage);
        }).Wait();

        Dispatcher.Invoke(() => {
            ErrorMessage = errorMessage;
        });
    }

    public override void OnApplyTemplate() {
        base.OnApplyTemplate();
    }

    protected override void Dispose(bool isCalledManually) {
        if (_isDisposed == false) {
            if (isCalledManually) {
                // Official docs say you should always dispose CTS'es, but that is complicated because you then may get ObjectDisposedException.
                // Internet says it is not so crucial and Cancel() is enough in 99.9% of cases, unless one uses WaitHandles, which is very rare.
                // https://stackoverflow.com/questions/6960520/when-to-dispose-cancellationtokensource
                _globalCts.Cancel();

                CancelExecution();
            }

            _isDisposed = true;
        }

        base.Dispose(isCalledManually);
    }

    private static void ExecuteScript(string script, string imports, AssemblyLoadContext assemblyContext, ScriptContextBase scriptContext, out string errorMessage) {
        errorMessage = "";
        var stopwatch = Stopwatch.StartNew();

        try {
            var options = ScriptOptions.Default;

            using var funkyLoader = new InteractiveAssemblyLoader();
            foreach (var assembly in assemblyContext.Assemblies) {
                if (assembly.IsDynamic == false) {
                    // _log.Info("Addding assembly {0} to script execution context.", assembly.FullName);
                    options = options.AddReferences(assembly);
                    funkyLoader.RegisterDependency(assembly);
                }
            }

            options = options.WithImports(imports.Split("\r\n"));

            if (scriptContext is not null) {
                // _log.Info("ScriptContext has been found.");
                var zeScript = CSharpScript.Create(script, options, scriptContext.GetType(), funkyLoader);
                zeScript.RunAsync(scriptContext).Wait();
            } else {
                // _log.Info("ScriptContext has not been found, continuing without it.");
                var zeScript = CSharpScript.Create(script, options, null, funkyLoader);
                stopwatch = Stopwatch.StartNew();
                zeScript.RunAsync().Wait();
            }
        } catch (TaskCanceledException) {
            // Swallowing.
        } catch (CompilationErrorException ex) {
            //_log.Error(ex, "Script cannot compile.");
            errorMessage = ex.Message;
        } catch (Exception ex) {
            //_log.Error(ex, "Script execution error.");
            errorMessage = ex.Message;
        }

        stopwatch.Stop();
        if (string.IsNullOrEmpty(errorMessage)) {
            //_log.Info("Script executed succesfully in {0} s.", (stopwatch.ElapsedMilliseconds / 1000.0).ToString("0.0000", CultureInfo.InvariantCulture));
        }
    }
}
