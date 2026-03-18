using Microsoft.Data.Sqlite;

public class CreateFinanceBoxTable
{
    private readonly SqliteConnection connection;

    public CreateFinanceBoxTable(SqliteConnection connection)
    {
        this.connection = connection;
    }

    public void CreateTable()
    {
        try
        {            
            var sql = @"Create Table finance_box(
                id INTEGER PRIMARY KEY,
                name TEXT NOT NULL,
                created_at DATETIME DEFAULT (datetime('now', '-3 hours'))
            );";

            using var command = new SqliteCommand(sql, connection);
            command.ExecuteNonQuery();

            Console.WriteLine("- Created FinanceBoxTable Succefully!");
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error to create FinanceBoxTable: {err.Message}");
        }
    }
}