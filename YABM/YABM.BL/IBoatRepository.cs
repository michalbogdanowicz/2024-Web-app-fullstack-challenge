using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YABM.BL
{
    public interface IBoatRepository
    {
        void Add(Boat boat);
        /// <summary>
        /// TODO : Should accept the whole BOAT object or the single fields that get updated?
        /// Google around.
        /// </summary>
        /// <param name="boat"></param>
        void Update(Boat boat);
        void Delete(long boatId);
        IEnumerable<Boat> GetAll();
        Boat Get(long id);
    }
}
