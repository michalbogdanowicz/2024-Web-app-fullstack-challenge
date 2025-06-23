using SQLitePCL;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YABM.BL
{
    /// <summary>
    /// Uses the context to create 
    /// </summary>
    public class BoatRepository : IBoatRepository, IDisposable
    {
        private DL.YABMContext _context = new DL.YABMContext();

        //public BoatRepository(DL.YABMContext context)
        //{
        //    _context = context;
        //}
        public void Add(Boat boat)
        {
            _context.Boats.Add(new DL.Boat() { Name = boat.Name, Description = boat.Description }); // Boat must be a valid BL object, checks already happen in there.
            _context.SaveChanges();
        }

        public void Delete(int boatId)
        {
            // TODO preformacne improvement : by not loading the object into memory, there is a way I forgot to mark it in the context for deletion.
            var boat = _context.Boats.FirstOrDefault(i => i.BoatId == boatId);
            if (boat == null)
            {
                throw new ArgumentException($"No boat with id {boatId}");
            }

            _context.Boats.Remove(boat);
            _context.SaveChanges();
        }



        public IEnumerable<Boat> GetAll()
        {
            List<Boat> list = new List<Boat>();

            foreach (var dbBoat in _context.Boats)
            {
                try
                {
                    list.Add(new Boat(dbBoat));
                }
                catch (Exception ex)
                {
                    throw new InvalidOperationException("The database contains data that does not conform to the requirments. ", ex); // TODO :possible additional features. Sanitize or inform the user about the corrupted data and ask for corrections.
                }
            }
            return list;
        }

        public void Update(Boat boat)
        {
            var dbBoat = _context.Boats.FirstOrDefault(i => i.BoatId == boat.Id);
            if (dbBoat == null)
            {
                throw new ArgumentException($"No boat with id {boat.Id}");
            }
            else
            {
                dbBoat.Name = boat.Name;
                dbBoat.Description = boat.Description;
                _context.SaveChanges();
            }
        }
        public void Dispose()
        {

        }

        public Boat? Get(int id)
        {

            var dbBoat = _context.Boats.FirstOrDefault(i => i.BoatId == id);
            if (dbBoat == null) { return null; }

            try
            {
                return new Boat(dbBoat);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("The database contains data that is does not conform to the requirments. ", ex); // TODO :possible additional features. Sanitize or inform the user about the corrupted data.
            }

        }
    }
}
