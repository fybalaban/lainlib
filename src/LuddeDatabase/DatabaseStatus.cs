namespace lainlib.Database
{
    public partial class Database
    {
        public enum DatabaseStatus
        {
            /// <summary>
            /// The object is disposed, all properties are null.
            /// </summary>
            Disposed = 0,

            ReadyToConnect = 1,

            /// <summary>
            /// Connection is available.
            /// </summary>
            Connected = 2,
            Exporting = 3,
            Closing = 4
        }
    }
}
