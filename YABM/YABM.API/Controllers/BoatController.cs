using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyModel.Resolution;
using YABM.API.DTO;

namespace YABM.API.Controllers
{
    [Route("[controller]")]
    public class BoatController : ControllerBase
    {
        private BL.IBoatRepository _boatRepository;

        public BoatController(BL.IBoatRepository boatRepository)
        {
            _boatRepository = boatRepository;
        }



        [HttpGet]
        public ActionResult<IEnumerable<Boat>> Get(int? id)
        {
            var result = new List<Boat>();

            if (id.HasValue)
            {
                var boat = _boatRepository.Get(id.Value);

                if (boat == null) { return NotFound($"No object with id {id}"); }
                else { result.Add(new Boat(boat)); }
            }
            else
            {
                var boats = _boatRepository.GetAll();

                foreach (var boat in boats)
                {
                    result.Add(new Boat(boat));
                }
            }
            return result;
        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create(IFormCollection collection)
        {
            // let the Boat BL tell us what is right.
            // TODO a more efficient way would be to not use the exception, as that eats away resoureces. It is not really an "exceptional" behaviour. But not the end of the world in this case.
            // TODO add a check for the existance of the paramters.
            _boatRepository.Add(new BL.Boat(collection["name"], collection["description"]));
            return Ok();
        }


        [HttpPut]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            // TODO could be a patch instead of a put.
            throw new NotImplementedException();
        }



        [HttpDelete]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            _boatRepository.Delete(id);
            // TODO what happens when there is no object to delete?
            return Ok();
        }
    }
}
