using System.ComponentModel.DataAnnotations;

namespace SampleAPI.Entities
{
    public class Order
    {
        public Guid Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Desscription { get; set; }
        public DateTime OrderDate { get; set; }
        public bool InvoiceGenerated { get; set; }
        public bool OrderDeleted { get; set; }
        public Order()
        {
            Id = Guid.NewGuid();
            InvoiceGenerated = true ;
            OrderDeleted = false ;
        }
    }
}
