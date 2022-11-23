using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace Stock.Collection.DataAccess.Entities
{
    public class Company 
    {
        public int Id { get; set; }
        public string StockCode { get; set; }
        public string CompanyName { get; set; }
        public string Industry { get; set; }
        public string StockExchange { get; set; }
        public double ListedStock { get; set; }
    }
}
