namespace Hotels.Api.Data.Entities
{
    using System.ComponentModel.DataAnnotations;

    public class Hotel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string City { get; set; }
    }
}
