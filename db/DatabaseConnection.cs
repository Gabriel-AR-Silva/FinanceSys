using Microsoft.Data.Sqlite;

public class DatabaseConnection
{
    private string connectionPath = @"Data Source=C:\Users\biell\OneDrive\Desktop\projeto\FinanceSys\db\database.db";
    public SqliteConnection GetConnection()
    {
        return new SqliteConnection(connectionPath);
    }
}

/*
    Access to database origin to connection 
*/