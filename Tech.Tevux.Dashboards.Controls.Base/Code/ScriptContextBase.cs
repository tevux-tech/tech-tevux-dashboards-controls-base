namespace Tech.Tevux.Dashboards.Controls {
    public abstract class ScriptContextBase {
        public bool IsCancellationRequested { get; set; }
        public abstract ISharedLibraryMessagingProvider Messenger { get; }
        public void WriteLine(string text) {
            Messenger.Send("editor-debug-output", new SetValueMessage(text));
        }
    }
}
