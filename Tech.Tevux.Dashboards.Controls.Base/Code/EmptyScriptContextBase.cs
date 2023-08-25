namespace Tech.Tevux.Dashboards.Controls {
    /// <summary>
    /// An empty implementation of <see cref="ScriptContextBase"/> to use instead of <c>null</c>.
    /// </summary>
    public sealed class EmptyScriptContextBase : ScriptContextBase {
        public override ISharedLibraryMessagingProvider Messenger { get; } = new EmptySharedLibraryMessagingProvider();
    }
}
