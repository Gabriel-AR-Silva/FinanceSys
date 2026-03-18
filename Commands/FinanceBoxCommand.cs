using FinanceSys.Models.FinanceBox;

public class FinanceBoxCommand
{
    public void Execute()
    {
        var database = new DatabaseConnection();
        var financeBoxRepository = new FinanceBoxRepository(database);
        var financeBoxService = new FinanceBoxService(financeBoxRepository);

        while (true)
        {
            // Listing Finance Boxes
            var financeBoxes = financeBoxService.GetAll();
            PrintFinanceBoxes(financeBoxes);
                
            Console.WriteLine("\n======= Menu Finance Box =======");  
            Console.WriteLine("(1) - Show");  
            Console.WriteLine("(2) - Create");  
            Console.WriteLine("(3) - Edit");  
            Console.WriteLine("(4) - Remove");  
            Console.WriteLine("(5) - Back");  
            Console.Write("Choice an action: ");  
            var action = Console.ReadLine();
            
            Console.Clear();
            Console.Write("\n");  

             switch (action)
            {
                case "1":
                    Console.WriteLine("Showing Finance Box");
                    break;

                case "2":
                    Console.WriteLine("\n====== Creating Finance Box ======");
                    Console.Write("What's Finance Box's name: ");
                    var name = Console.ReadLine()!;

                    var data = new CreateFinanceBoxDTO { Name = name};
                    financeBoxService.Create(data);
                    break;

                case "3":
                    Console.WriteLine("Updating Finance Box");
                    break;

                case "4":
                    Console.WriteLine("Removing Finance Box");
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Insert a valid value!");
                    break;
            }
        }
    }

    public void PrintFinanceBoxes(List<FinanceBox> list)
    {
        Console.WriteLine("====== Listing Finance Boxes ======");

        if (!list.Any())
        {            
            Console.WriteLine("There are not any Finance Box");
            return;
        }

        Console.WriteLine("{0,-5} {1,-15} {2,-25}", "ID", "Name", "Created_At");

        foreach (var item in list)
        {
            Console.WriteLine("{0,-5} {1,-15} {2,-25}",
                item.Id,
                item.Name,
                item.created_at);
        }
    }
}