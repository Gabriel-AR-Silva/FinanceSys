using FinanceSys.Models.FinanceBox;

public class FinanceBoxRepository
{
    private DatabaseConnection database;

    public FinanceBoxRepository(DatabaseConnection database)
    {
        this.database = database;
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

            Console.WriteLine("\n>>>>>> Add a FinanceBox Succefully! >>>>>>\n");
        }
        catch (Exception err)
        {
            Console.WriteLine($"Error to add a FinanceBox: {err.Message}");
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
            Console.WriteLine($"Error to get all Finance Box: {err}");
        }

        return list;
    }
}