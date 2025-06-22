using System.ComponentModel.DataAnnotations;

namespace YABM.BL
{
    public class Boat
    {
        public int Id { get; set; }
        private string _name;
        private string _description;
        public string Name
        {
            get => _name;
            set
            {
                ArgumentNullException.ThrowIfNull(value, "Name");
                if (value.Length == 0)
                {
                    throw new ArgumentException("A boat's name must be provided");
                }
                // Assumption 2 of #5
                if (value.Length > 100)
                {
                    throw new ArgumentException("A boat's name cannot exceed 100 characters.");
                }
                _name = value;
            }
        }
        public string Description
        {
            get => _description;

            set
            {
                if (value == null) { _description = string.Empty; return; }
                // commented away because of assumption 5 of #5
                //if (Name.Length == 0)
                //{
                //    throw new ArgumentException("A boat's description must be provided");
                //}
                // Assumption 3 of #5
                if (Name.Length > 100)
                {
                    throw new ArgumentException("A boat's name cannot exceed 100 characters.");
                }
                _description = value;
            }

        }

        public Boat(string name, string description)
        {
            this.Name = name;
            this.Description = description;
        }

        public Boat(int id, string name, string description)
        {
            this.Id = id;
            this.Name = name;
            this.Description = description;
        }
        /// <summary>
        /// TODO check this required part, back in my day this wasn't around.
        /// </summary>
        /// <param name="boat"></param>
        public Boat(DL.Boat boat)
        {
            this.Id = boat.BoatId;
            this.Name = boat.Name;
            this.Description = boat.Description;
        }

    }
}
