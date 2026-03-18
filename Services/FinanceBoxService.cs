using FinanceSys.Models.FinanceBox;

public class FinanceBoxService
{
    private readonly FinanceBoxRepository repository;

    public FinanceBoxService(FinanceBoxRepository repository)
    {
        this.repository = repository;
    }

    public void Create(CreateFinanceBoxDTO data)
    {
        if(string.IsNullOrWhiteSpace(data.Name))
            throw new Exception ("Name is required");

        var financeBox = new FinanceBox { Name = data.Name };

        repository.Create(financeBox);
    }

    public List<FinanceBox> GetAll()
    {
       var financeBoxes = repository.GetAll();

       return financeBoxes;
    }
}