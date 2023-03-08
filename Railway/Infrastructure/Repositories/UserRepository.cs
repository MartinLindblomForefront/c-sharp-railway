using Railway.Common;
using Railway.Domain;

namespace Railway.Infrastructure.Repositories;


public interface IUserRepository
{
    public Result<User> AddUser(User user);
    public Result<IList<User>> GetAll();
}

public class UserRepository : IUserRepository
{
    private readonly IList<User> inMemoryUsers = new List<User>();

    public Result<User> AddUser(User user)
    {
        if (user == null)
            return Result<User>.Failure(new NullValueError("Cannot save NULL user"));

        inMemoryUsers.Add(user);

        return Result<User>.Success(user);
    }

	public Result<IList<User>> GetAll()
	{
        return Result<IList<User>>.Success(inMemoryUsers);
	}
}
