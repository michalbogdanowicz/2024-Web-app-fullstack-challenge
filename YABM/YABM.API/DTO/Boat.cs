namespace YABM.API.DTO
{
    /// <summary>
    /// Possibly the representation that comes out from the DL and BL might need to changed.
    /// </summary>
    public class Boat
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public Boat() { }
        public Boat(BL.Boat boat)
        {
            this.Id = boat.Id;
            this.Name = boat.Name;
            this.Description = boat.Description;
        }

    }
}
