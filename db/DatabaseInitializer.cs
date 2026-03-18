public class DatabaseInitializer
{
    private readonly DatabaseConnection database;

    public DatabaseInitializer(DatabaseConnection database)
    {
        this.database = database;
    }

    public void Initialize()
    {
        try
        {
            using var connection = this.database.GetConnection();
            connection.Open();
            Console.WriteLine("\n===== Connected To Database =====");

            var financeBoxTable = new CreateFinanceBoxTable(connection);

            Console.WriteLine("\n===== Creating Tables =====");
            financeBoxTable.CreateTable();
            Console.WriteLine("===========================");

        }
        catch (Exception err)
        {
            Console.WriteLine($"Error to initialize database: {err.Message}");
        }
    }
}