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

        }

        public void Delete(long boatId)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        public void Dispose()
        {

        }

        public Boat? Get(long id)
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
