namespace Hotels.Api.Resources
{
    using System.ComponentModel.DataAnnotations;
    using Data.Entities;

    public class RoomResource
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }
    }

    public static class RoomResourceExtensions
    {
        public static Room MapToEntity(this RoomResource room)
        {
            return new Room
            {
                Number = room.Number,
                Name = room.Name,
                Id = room.Id
            };
        }

        public static RoomResource MapToResource(this Room room)
        {
            return new RoomResource
            {
                Number = room.Number,
                Name = room.Name,
                Id = room.Id
            };
        }
    }
}
