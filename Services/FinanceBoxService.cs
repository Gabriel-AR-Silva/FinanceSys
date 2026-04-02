using FinanceSys.Models.FinanceBox;

public class FinanceBoxService
{
    private readonly FinanceBoxRepository repository;

    public FinanceBoxService(FinanceBoxRepository repository)
    {
        this.repository = repository;
    }

    public void Update(BaseFinanceBoxDTO dtoData)
    {
        var result = new Result<FinanceBox>(); 

        if(dtoData is null)
            throw new Exception ("Name is required");

        if(string.IsNullOrWhiteSpace(dtoData.Name))
            throw new Exception ("Name is required");

        // var data = repository.Get(financeBoxId);

        // if (data is null)
        // {
        //     return result.Fail($"Finance Box with ID '{financeBoxId}' not found");
        // }

        // repository.Create(financeBox);
    }

    public Result<FinanceBox> Create(BaseFinanceBoxDTO data)
    {
        var result = new Result<FinanceBox>(); 
        var validated = data.Validate();

        if (validated.Any())
        {
            // Change it in refactor to get List<string>
            return result.Fail(validated[0]);
        }

        var financeBox = new FinanceBox { Name = data.Name };

        var newData = repository.Create(financeBox);

        if (newData is null)
        {
            return result.Fail("Can't Possibly Create The Finance Box");
        }

        return result.OK(newData);
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