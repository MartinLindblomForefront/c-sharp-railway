using Microsoft.AspNetCore.Mvc;
using Railway.Application;
using Railway.Common;
using Railway.Domain;

namespace Railway.Infrastructure.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
	private readonly IUserService _userService;

	public UsersController(IUserService userService)
	{
		_userService = userService;
	}

	[HttpPost]
	public IActionResult Add([FromBody] AddUserDto dto)
	{
		return _userService
			.Create(dto.Name, dto.Email, dto.PhoneNumber)
			.OnSuccess(UserResultDto.MapFromUser)
			.Handle(this);
	}

	[HttpGet]
	public IActionResult GetAll()
	{
		return _userService
			.GetAll()
			.OnSuccess(users => users
				.Select(u => UserResultDto.MapFromUser(u))
				.Combine())
			.Handle(this);
	}
}

public record AddUserDto(string Name, string Email, string PhoneNumber);

public record UserResultDto(Guid Id, string Name, string Email, string PhoneNumber)
{
	public static Result<UserResultDto> MapFromUser(User user)
		=> Result<UserResultDto>.Success(new(user.Id, user.Name.Value, user.Email.Value, user.PhoneNumber.Value));
}