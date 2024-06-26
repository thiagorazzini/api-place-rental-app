using PlaceRentalApp.API.Entities;

namespace PlaceRentalApp.API.Persistence
{
    public class PlaceRentalDbContext
    {
        public PlaceRentalDbContext()
        {
            Places = [];
            PlaceAmenities = [];
            PlaceBooks = [];
            Users = [];
            Comments = [];
        }
        public List<Place> Places { get; private set; }
        public List<PlaceAmenity> PlaceAmenities { get; set; }
        public List<PlaceBook> PlaceBooks { get; set; }
        public List<User> Users { get; set; }
        public List<Comment> Comments { get; set; }
    }
}
