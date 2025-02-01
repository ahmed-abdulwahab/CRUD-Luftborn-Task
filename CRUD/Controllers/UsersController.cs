using AutoMapper;
using AutoMapper.QueryableExtensions;
using BAL.DTOs;
using BAL.Repository;
using BAL.UnitOfWork;
using CRUD.Common;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace CRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IMapper _mapper;

        public UsersController(IUnitOfWork unitOfWork, IRepository<AppUser> userRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers([FromQuery] string sortBy = "lastActive")
        {
            var usersQuery = _userRepository.GetAllAsQueryable().ProjectTo<MemberDto>(_mapper.ConfigurationProvider);

            if (sortBy == "score")
            {
                usersQuery = usersQuery.OrderByDescending(u => u.Score);
            }
            else if (sortBy == "lastActive")
            {
                usersQuery = usersQuery.OrderByDescending(u => u.LastActive);
            }

            var users = await usersQuery.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{username}")]
        public async Task<ActionResult<MemberDto>> GetUser(string username)
        {
            var user = await _userRepository.GetMemberAsync(username);

            if (user == null) return NotFound();

            return user;
        }


        //  I have added this attribute 'score' inside the app user as just a simple idea to applay the filtration based on it,
        //  and user it inside the angular app.
        [HttpPut("{id}/score")]
        public async Task<IActionResult> UpdateUserScore(int id, [FromBody] ScoreUpdateDto scoreDto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return NotFound();

            user.Score = scoreDto.Score;
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitAsync();

            return Ok(user);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateUser(MemberUpdateDto memberUpdate)
        {
            var user = await _userRepository.GetByIdAsync(memberUpdate.Id);
            if (user == null) throw new Exception("User not found");

            _mapper.Map(memberUpdate, user);
            await _userRepository.UpdateAsync(user);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return NotFound();

            await _userRepository.DeleteAsync(user);
            await _unitOfWork.CommitAsync();

            return NoContent();
        }
    }
}
