namespace StockAPI.Models.Interfaces
{
    public interface INews
    {
        string Headline { get; set; }
        string Source { get; set; }
        string URL { get; set; }
    }
}