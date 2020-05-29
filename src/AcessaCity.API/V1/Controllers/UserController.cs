using System;
using System.Threading.Tasks;
using AcessaCity.API.Controllers;
using AcessaCity.API.Dtos.User;
using AcessaCity.Business.Dto.User;
using AcessaCity.Business.Interfaces;
using AcessaCity.Business.Interfaces.Repository;
using AcessaCity.Business.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;
using AcessaCity.Business.Models;
using System.Collections.Generic;

namespace AcessaCity.API.V1.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/user")]        
    public class UserController : MainController
    {
        private readonly IUserRepository _repository;
        private readonly IUserService _service;

        public UserController(
            INotifier notifier,
            IUserRepository repository,
            IUserService service) : base(notifier)
        {
            _repository = repository;
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult> Post(UserCreateDto user)
        {
            var newUser = await _service.AddFirebaseUser(user);
            return CustomResponse(newUser);
        }

        [HttpGet("{userId:guid}")]
        public async Task<ActionResult<User>> GetUser(Guid userId)
        {
            User user = await _service.FindById(userId);            
            return CustomResponse(user);
        }


        [HttpPut("{userId:guid}/update-role")]
        public async Task<ActionResult> UdpdateUserRole(
            Guid userId, 
            UpdateUserRoleDto role,
            [FromServices] IUserRoleService roleService)
        {
            await roleService.UpdateUserRole(role.Role, userId, role.Allow);

            return CustomResponse();
        }

        [HttpPut("update-data-profile")]
        public async Task<ActionResult<User>> UpteEmailProfile(UpdateUserDataDto userData)
        {
            User user = await _repository.GetById(userData.UserId);
            bool emailOk = true;
            bool userOk = true;
            if (user != null)
            {
                if (user.Email != userData.Email)
                {
                    emailOk = await _service.UpdateUserEmail(user.FirebaseUserId, user.Email, userData.Email);                            
                    user.Email = userData.Email;
                }
            }

            if (emailOk)
            {
                user.FirstName = userData.FirstName;
                user.LastName = userData.LastName;
                
                userOk = await _service.UpdateUserData(user.FirebaseUserId, userData.FirstName, userData.LastName);          
            }

            if (userOk && emailOk)
            {
                await _service.Update(user);  
            }
            
            return CustomResponse(user);
        }

        [HttpPut("update-photo-profile")]
        public async Task<ActionResult<User>> UptePhotoProfile(UpdateUserProfilePhoto photo)
        {
            User user = await _repository.GetById(photo.UserId);
            
            bool ok = await _service.UpdateUserPhotoUrl(user.FirebaseUserId, photo.PhotoURL);
            if (ok)
            {
                user.ProfileUrl = photo.PhotoURL;
                await _service.Update(user);
            }
            
            return CustomResponse(user);
        }

        [HttpDelete("{userId:guid}")]
        public async Task<ActionResult> Delete(Guid userId)
        {   
            await _repository.Inactive(userId);
            
            return CustomResponse();
        }

        [HttpGet("{userId:guid}/coordinators")]
        public async Task<ActionResult> GetCoordinatorsList(Guid userId)
        {
            var user = await _repository.GetById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return CustomResponse(await _repository.UserCoordinators(userId));
        }

        [HttpGet("")]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            var users = await _repository.Find(x => x.CityHallId == Guid.Empty);
            return CustomResponse(users);
        }
    }
}