using System.Reflection.Metadata;
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
            Console.Write("-> Choice an action: ");  
            var action = Console.ReadLine();
            
            Console.Clear();
            Console.Write("\n");  

             switch (action)
            {
                case "1":
                    // Select a Finance Box and show your detail
                    HandleShow(financeBoxService, financeBoxes);
                    break;

                case "2":
                    // Create a new Finance Box
                    HandleCreate(financeBoxService);
                    break;

                case "3":
                    Console.WriteLine("Updating Finance Box");
                    break;

                case "4":
                    // Select a Finance Box and removi 
                    HandleRemove(financeBoxService, financeBoxes);
                    break;

                case "5":
                    return;

                default:
                    InsertionError();
                    break;
            }
        }
    }

    public void PrintFinanceBoxes(List<FinanceBox> list, bool noTitle = false)
    {
        if (!noTitle)
        { 
            Console.WriteLine("====== Listing Finance Boxes ======");
        }

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

    public void HandleRemove(FinanceBoxService financeBoxService, List<FinanceBox> financeBoxes)
    {
        while (true)
        {
            string inputValue;
            int removeId;

            Console.WriteLine("\n====== Removing Finance Box ======");
            PrintFinanceBoxes(financeBoxes, true);

            do
            {
                Console.Write("-> What's Finance Box's Id To Remove (type '0' to cancel): ");
                inputValue = Console.ReadLine()!;

            } while (string.IsNullOrWhiteSpace(inputValue) || !int.TryParse(inputValue, out removeId));

            if (removeId == 0)
            {
                Console.Clear();
                return;
            }

            var result = financeBoxService.Destroy(removeId);
            var data = result.Data;

            if (result.Success && data is not null)
            {
                Console.WriteLine($"XXXXXX Removed Finance Box '{data.Name}' Successfully XXXXXX");
                return;
            } else
            {
                Console.WriteLine(result.Error);
            }
        }
    }
    
    public void HandleCreate(FinanceBoxService financeBoxService)
    {
        while (true)
        {
            string name;
            
            Console.WriteLine("\n====== Creating Finance Box ======");

            do
            {
                Console.Write("-> What's Finance Box's name (type 'back' to cancel): ");
                name = Console.ReadLine()!;

            } while (string.IsNullOrWhiteSpace(name) || name.Length > 150);

            if (name == "back")
            {
                Console.Clear();
                return;
            } 

            var data = new CreateFinanceBoxDTO { Name = name};
            financeBoxService.Create(data);
            return;
        }
    }

    public void HandleShow(FinanceBoxService financeBoxService, List<FinanceBox> financeBoxes)
    {
        PrintFinanceBoxes(financeBoxes);

        Console.Write("-> What's Finance Box's Id To Show (type '0' to cancel): ");
        var showId = Convert.ToInt32(Console.ReadLine()!);

        if (showId == 0)
        {
            Console.Clear();
            return;
        } 

        var financeBox = financeBoxService.Get(showId);
        var data = financeBox.Data;

        Console.Clear(); 

        if (financeBox.Success && data is not null)
        {
            Console.WriteLine($"====== {data.Name} ======");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("Credit: R$350.23 ");

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("Debit: R$44 ");

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Total: R$306.23");

            Console.ResetColor();
            Console.WriteLine();
        } else
        {
            Console.WriteLine(financeBox.Error);
        }

        Console.WriteLine("\n");
    }

    public void InsertionError()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("XXXXXX Insert a valid value! XXXXXX");
        Console.ResetColor();
    }
}