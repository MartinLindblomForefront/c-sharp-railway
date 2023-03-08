using Railway.Common;
using Railway.Domain;
using Railway.Infrastructure.Adapters;
using Railway.Infrastructure.Repositories;

namespace Railway.Application;

public interface IUserService
{
	Result<User> Create(string name, string email, string phoneNumber);
	Result<IList<User>> GetAll();
}

public class UserService : IUserService
{
	private readonly IUserRepository _userRepository;
	private readonly IUserEventPublisher _userEventPublisher;

	public UserService(IUserRepository userRepository, IUserEventPublisher userEventPublisher)
	{
		_userRepository = userRepository;
		_userEventPublisher = userEventPublisher;
	}

	public Result<User> Create(string name, string email, string phoneNumber)
	{
		var nameResult = Name.Create(name);
		if (nameResult.IsFailure)
			return Result<User>.Failure(nameResult.Error);

		var emailResult = Email.Create(email);
		if (emailResult.IsFailure)
			return Result<User>.Failure(emailResult.Error);

		var phoneNumberResult = PhoneNumber.Create(phoneNumber);
		if (phoneNumberResult.IsFailure)
			return Result<User>.Failure(phoneNumberResult.Error);


		return User
			.Create(nameResult.Value, emailResult.Value, phoneNumberResult.Value)
			.OnSuccess(_userRepository.AddUser)
			.DeadEnd(_userEventPublisher.PublishUserCreatedEvent);
	}

	public Result<IList<User>> GetAll()
	{
		return _userRepository.GetAll();
	}
}
