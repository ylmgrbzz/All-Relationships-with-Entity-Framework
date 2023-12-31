﻿using Microsoft.AspNetCore.Mvc;
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
                           ).Include(character => character.Weapon).Include(character => character.Skills).ToListAsync();

            return Ok(characters);
        }

        [HttpPost("weapon")]
        public async Task<ActionResult<Character>> Post(AddWeaponDto request)
        {
            var character = await _dataContext.Characters.FindAsync(
                               request.CharacterId
                                          );
            var newWeapon = new Weapon
            {
                Name = request.Name,
                Damage = request.Damage,
                Character = character
            };

            if (character == null)
            {
                return BadRequest("character not found");
            }
            _dataContext.Weapons.Add(newWeapon);
            await _dataContext.SaveChangesAsync();
            return Ok(newWeapon);
        }

        [HttpPost]
        public async Task<ActionResult<List<Character>>> AddWeapon(CreateCharacterDto request)
        {
            var user = await _dataContext.Users.FindAsync(
                               request.UserId
                                          );
            var newCharacter = new Character
            {
                Name = request.Name,
                RpgClass = request.RpgClass,
                User = user
            };

            if (user == null)
            {
                return BadRequest("User not found");
            }
            _dataContext.Characters.Add(newCharacter);
            await _dataContext.SaveChangesAsync();

            return await Get(newCharacter.UserId);
        }
    }
}