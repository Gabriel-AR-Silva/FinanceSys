namespace FinanceSys;

class Program
{
    public static void Main()
    {
        var database = new DatabaseConnection(); 
        var initializer = new DatabaseInitializer(database);
        initializer.Initialize();

        while (true)
        {          
            Console.WriteLine("\n======= Welcome To FinanceSys =======");  
            Console.WriteLine("(1) - Finance Box");  
            Console.WriteLine("(2) - Leave");  
            Console.Write("Choice an action: ");  
            var action = Console.ReadLine();
            
            Console.Clear();
            Console.Write("\n");  

            switch (action)
            {
                case "1":
                    var financeBoxCommand = new FinanceBoxCommand();
                    financeBoxCommand.Execute();
                    break;

                case "2":
                    Console.WriteLine("System Closed! <(*-*)>");
                    return;

                default:
                    Console.WriteLine("Insert a valid value!");
                    break;
            }
        }
    }
}