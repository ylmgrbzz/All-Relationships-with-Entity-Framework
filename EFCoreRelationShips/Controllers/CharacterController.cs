using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EFCoreRelationShips.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CharacterController : ControllerBase
    {
        private readonly DataContext _dataContext;

        public CharacterController(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Character>>> Get(int userId)
        {
            var characters = await _dataContext.Characters.Where(
                character => character.UserId == userId
                           ).ToListAsync();

            return Ok(characters);
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> Post(Character character)
        {
            var user = await _dataContext.Users.FindAsync(
                               character.UserId
                                          );
            if (user == null)
            {
                  return BadRequest("User not found");
            }
            _dataContext.Characters.Add(character);
            await _dataContext.SaveChangesAsync();

            return await Get(character.UserId);
        }
    }
}