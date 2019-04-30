namespace StockAPI.Models
{
    public interface INews
    {
        string Headline { get; set; }
        string Source { get; set; }
        string URL { get; set; }
    }
}