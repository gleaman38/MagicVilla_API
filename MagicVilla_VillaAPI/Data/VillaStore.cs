using MagicVilla_VillaAPI.Models.DTO;

namespace MagicVilla_VillaAPI.Data
{
    //comments done
    //used to temporarily mock up database access to villa data until we implement actual database access using Entity Framework Core
    public static class VillaStore
    {
        public static List<VillaDTO> villaList = new List<VillaDTO>
        {
            new VillaDTO { Id = 1, Name = "Pool View", Sqft=100, Occupancy=4 },
            new VillaDTO { Id = 2, Name = "Beach View", Sqft=300, Occupancy=3 },
            new VillaDTO { Id = 3, Name = "Beach View 2", Sqft=300, Occupancy=3 }
        };
    }
}
