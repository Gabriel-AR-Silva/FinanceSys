using FinanceSys.Models.FinanceBox;

public class FinanceBoxRepository
{
    private DatabaseConnection database;

    public FinanceBoxRepository(DatabaseConnection database)
    {
        this.database = database;
    }
    public FinanceBox? Get(int financeBoxId)
    {
        try
        {
            using var connect = database.GetConnection();
            connect.Open();

            var cmd = connect.CreateCommand();
            cmd.CommandText = @"SELECT Id, Name, created_at FROM finance_box WHERE Id = $Id";
            cmd.Parameters.AddWithValue("$Id", financeBoxId);

            using (var reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    var financeBox = new FinanceBox
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        created_at = reader.GetDateTime(2)
                    };

                    return financeBox;
                }
            }

            return null;
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error To Get Finance Box: {err.Message}");
            return null;
        }
    }
    
    public void Create(FinanceBox financeBox)
    {

        try
        {            
            using var connection = database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText = @"Insert Into finance_box (name) VALUES ($name)";

            command.Parameters.AddWithValue("$name", financeBox.Name);

            command.ExecuteNonQuery();

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n>>>>>> Created Finance Box Succefully! >>>>>>\n");
            Console.ResetColor();
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error to add a FinanceBox: {err.Message}");
        }
    }

    public void Destroy(int financeBoxId)
    {
        try
        {
            using var connection = database.GetConnection();
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = "DELETE FROM finance_box WHERE Id = $Id";
            cmd.Parameters.AddWithValue("$Id", financeBoxId);
            cmd.ExecuteNonQuery();
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error to delete Finance Box: {err.Message}");
        }
    }

    public List<FinanceBox> GetAll()
    {
        var list = new List<FinanceBox>();

        try
        {
            using var connection = database.GetConnection();
            connection.Open();

            var cmd = connection.CreateCommand();
            cmd.CommandText = "SELECT * FROM finance_box";

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    var financeBox = new FinanceBox
                    {
                      Id = reader.GetInt32(0),
                      Name = reader.GetString(1),
                      created_at = reader.GetDateTime(2)  
                    };

                    list.Add(financeBox);
                }
            } 
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error to get all Finance Box: {err.Message}");
        }

        return list;
    }
}