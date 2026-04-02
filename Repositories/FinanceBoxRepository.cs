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
    
    public FinanceBox? Create(FinanceBox financeBox)
    {
        try
        {            
            using var connection = database.GetConnection();
            connection.Open();

            var command = connection.CreateCommand();

            command.CommandText = @"INSERT INTO finance_box (name) VALUES ($name); SELECT last_insert_rowid();";

            command.Parameters.AddWithValue("$name", financeBox.Name);

            var id = (long)command.ExecuteScalar()!;

            return Get((int)id);
        }
        catch (Exception)
        {
            return null;
        }
    }

    public void Update(int financeBoxId, string newName)
    {
        using var connection = database.GetConnection();
        connection.Open();

        var cmd = connection.CreateCommand();
        cmd.CommandText = "UPDATE finance_box SET Name = '$name' WHERE Id = $id";
        cmd.Parameters.AddWithValue("$id", financeBoxId);
        cmd.Parameters.AddWithValue("$name", newName);
        cmd.ExecuteNonQuery();

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