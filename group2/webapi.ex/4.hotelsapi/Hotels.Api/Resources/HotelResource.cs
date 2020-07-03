namespace Hotels.Api.Resources
{
    using System.ComponentModel.DataAnnotations;
    using Data.Entities;

    public class HotelResource
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string City { get; set; }
    }

    public static class HotelResourceExtensions
    {
        public static Hotel MapToEntity(this HotelResource hotel)
        {
            return new Hotel
            {
                City = hotel.City,
                Name = hotel.Name,
                Id = hotel.Id
            };
        }

        public static HotelResource MapToResource(this Hotel hotel)
        {
            return new HotelResource
            {
                City = hotel.City,
                Name = hotel.Name,
                Id = hotel.Id
            };
        }
    }
}
