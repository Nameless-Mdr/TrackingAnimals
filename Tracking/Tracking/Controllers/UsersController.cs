using AutoMapper;
using BLL.Service.Interfaces;
using Common;
using Common.Const;
using Domain.DTO.User;
using Domain.Entity.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tracking.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    [HttpPost]
    [AllowAnonymous]
    public async Task<int> CreateUser([FromForm] CreateUserModel user)
    {
        if (!PassHelper.IsValidPassword(user.Password))
            throw new Exception("Password is not correct");

        var id = User.GetClaimValue<int>(ClaimNames.Id);
        
        if (id != default)
            throw new Exception("you are already authorized");
        
        return await _userService.Create(_mapper.Map<User>(user));
    }

    [HttpGet]
    public async Task<IEnumerable<GetUserModel>> GetAllUsers()
    {
        var users = await _userService.GetAllModels();
        return users.Select(item => _mapper.Map<GetUserModel>(item));
    }

    [HttpPut]
    public async Task<int> UpdateUser([FromForm] CreateUserModel model)
    {
        if (!PassHelper.IsValidPassword(model.Password))
            throw new Exception("Password is not correct");
        
        var id = User.GetClaimValue<int>(ClaimNames.Id);
        
        if (id == default)
            throw new Exception("you are not authorized");

        var user = _mapper.Map<User>(model);
        user.Id = id;
        
        return await _userService.Update(user);
    }

    [HttpDelete]
    public async Task<bool> DeleteUser()
    {
        var id = User.GetClaimValue<int>(ClaimNames.Id);
        
        if (id == default)
            throw new Exception("you are not authorized");
        
        return await _userService.Delete(id);
    }
}