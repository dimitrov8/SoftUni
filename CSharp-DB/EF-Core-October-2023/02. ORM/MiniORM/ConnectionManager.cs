namespace MiniORM;

using System;

/// <summary>
///     Used for wrapping a database connection with a using statement and
///     automatically closing it when the using statement ends
/// </summary>
internal class ConnectionManager : IDisposable
{
    private readonly DatabaseConnection connection;

    /// <summary>
    /// Constructor for the ConnectionManager class.
    /// Initializes a new instance and opens the database connection.
    /// </summary>
    /// <param name="connection">The database connection to manage.</param>
    public ConnectionManager(DatabaseConnection connection)
    {
        this.connection = connection;

        this.connection.Open();
    }

    /// <summary>
    /// Disposes of the managed database connection.
    /// Closes the connection when the using statement scope ends.
    /// </summary>
    public void Dispose()
    {
        this.connection.Close();
    }
}