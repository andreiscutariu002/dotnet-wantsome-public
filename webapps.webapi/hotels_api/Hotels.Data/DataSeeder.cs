namespace Hotels.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using Entities;

    public class DataSeeder
    {
        public static void SeedHotels(ApiDbContext context)
        {
            if (!context.Hotels.Any())
            {
                var hotels = new List<Hotel>();
                var rooms = new List<Room>();

                for (var i = 1; i < 100; i++)
                {
                    var h = new Hotel {Id = i, City = $"City {i}", Name = $"Hotel {i}"};
                    hotels.Add(h);

                    for (var j = 1; j < 25; j++)
                    {
                        var room = new Room {Hotel = h, Id = i * 100 + j, Number = $"0{j}"};

                        rooms.Add(room);
                    }
                }

                context.Hotels.AddRange(hotels);
                context.Rooms.AddRange(rooms);
                context.SaveChanges();
            }
        }
    }
}