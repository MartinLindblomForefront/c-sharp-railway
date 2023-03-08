namespace Problem;

internal class ResultUserService
{
	private readonly IList<User> _users = new List<User>
	{
		new ("Martin", "Lindblom"),
		new ("Anders", "Anka"),
		new ("Sofie", "Sparv"),
		new ("Petra", "Pingvin")
	};

	public Result<User> FindUser(Guid id)
	{
		var user = _users.FirstOrDefault(x => x.Id == id);

		return user == null
			? Result<User>.Failure(new Error($"User with id '{id}' was not found"))
			: Result<User>.Success(user);
	}

	public Result<User> CreateUser(string firstName, string lastName)
	{
		if (_users.Any(u => u.FirstName.Equals(firstName)))
			return Result<User>.Failure(new Error($"User with first name '{firstName}' already exist"));

		if (_users.Any(u => u.LastName.Equals(lastName)))
			return Result<User>.Failure(new Error($"User with last name '{lastName}' already exist"));

		var newUserResult = User.Create(firstName, lastName);
		if (newUserResult.IsFailure)
			return newUserResult;
		
		_users.Add(newUserResult.Value);

		return newUserResult;
	}

	public Result<IList<User>> GetAll() => Result<IList<User>>.Success(_users);
}