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
        var result = new Result<FinanceBox>(); 

        if(data is null)
            throw new Exception ("Name is required");

        if(string.IsNullOrWhiteSpace(data.Name))
            throw new Exception ("Name is required");

        var data = repository.Get(financeBoxId);

        if (data is null)
        {
            return result.Fail($"Finance Box with ID '{financeBoxId}' not found");
        }

        repository.Create(financeBox);
    }

    public Result<FinanceBox> Update(CreateFinanceBoxDTO data)
    {
        if(string.IsNullOrWhiteSpace(data.Name))
            throw new Exception ("Name is required");

        var financeBox = new FinanceBox { Name = data.Name };

        repository.Update(financeBoxId, data.Name);
    }

    public Result<FinanceBox> Get(int financeBoxId)
    {
        var result = new Result<FinanceBox>(); 
        var data = repository.Get(financeBoxId);

        if (data is null)
        {
            return result.Fail($"Finance Box with ID '{financeBoxId}' not found");
        }

        return result.OK(data);
    }

    public Result<FinanceBox> Destroy(int financeBoxId)
    {
        var result = new Result<FinanceBox>(); 
        var data = repository.Get(financeBoxId);

        if (data is null)
        {
            return result.Fail($"Finance Box with ID '{financeBoxId}' not found");
        }

        repository.Destroy(financeBoxId);

        return result.OK(data);
    }

    public List<FinanceBox> GetAll()
    {
       var financeBoxes = repository.GetAll();

       return financeBoxes;
    }
}