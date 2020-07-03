namespace Hotels.Api.Resources
{
    using System.ComponentModel.DataAnnotations;

    public class RoomResource
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }
    }
}
