namespace PayrollEntities
{
    public interface IEmployee
    {
        int Id { get; set; }
        string Name { get; set; }
        decimal CalculatePay();
    }
}
