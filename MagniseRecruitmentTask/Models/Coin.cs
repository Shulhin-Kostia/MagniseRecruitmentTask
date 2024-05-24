using System.ComponentModel.DataAnnotations;

namespace MagniseRecruitmentTask.Models
{
    public class Coin
    {
        [Key]
        public string Code { get; set; }

        public decimal Price { get; set; }

        public DateTime PriceUpdated { get; set; }
    }
}
