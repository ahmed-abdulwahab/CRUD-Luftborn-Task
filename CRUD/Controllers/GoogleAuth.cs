using AutoMapper;
using AutoMapper.QueryableExtensions;
using BAL.DTOs;
using BAL.Repository;
using BAL.UnitOfWork;
using DAL.Entities;
using Google.Apis.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
    //###################################################################
    //###################################################################


    /* Note This Controller is for Google Auth is for testing SSO Additional Task
       will send the Token from the Angular app generated from the google auth to the end point
       If the token is valid we will get the users from the database assuming it's different data
       that can be showed to the user if he doesn't want to register or log in with his username and Pass
    */


    //###################################################################
    //###################################################################
    [Route("api/[controller]")]
    [ApiController]
    public class GoogleAuth : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<AppUser> _userRepository;
        private readonly IMapper _mapper;
        private readonly string _googleClientId = "846665467842-pjibgnpkb0o06shkfb7t1pdp2km6n4mg.apps.googleusercontent.com";  // Replace with your actual client ID
        public GoogleAuth(IUnitOfWork unitOfWork, IRepository<AppUser> userRepository, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("googleauth")]
        public async Task<ActionResult<IEnumerable<MemberDto>>> GetUsers()
        {
            var token = Request.Headers["Authorization"].ToString().Split(" ")[1];

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized("Authorization token is missing");
            }

            try
            {
                var payload = await VerifyGoogleTokenAsync(token);

                var usersDtos = _userRepository.GetAllAsQueryable().ProjectTo<MemberDto>(_mapper.ConfigurationProvider);
                return Ok(await usersDtos.ToListAsync());
            }
            catch
            {
                return Unauthorized("Invalid or expired token");
            }
        }

        private async Task<GoogleJsonWebSignature.Payload> VerifyGoogleTokenAsync(string token)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings
            {
                Audience = new[] { _googleClientId }  // Make sure the audience is your Google client ID
            };

            // Verify and decode the token
            var payload = await GoogleJsonWebSignature.ValidateAsync(token, settings);
            return payload;
        }
    }
}
