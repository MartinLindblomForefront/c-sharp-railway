namespace Problem;

internal class UserService
{
	private readonly IList<User> _users = new List<User>
	{
		new ("Martin", "Lindblom"),
		new ("Anders", "Anka"),
		new ("Sofie", "Sparv"),
		new ("Petra", "Pingvin")
	};

	public User? FindUser(Guid id)
	{
		var user = _users.FirstOrDefault(x => x.Id == id);
		return user;
	}

	public User CreateUser(string firstName, string lastName)
	{
		if (_users.Any(u => u.FirstName.Equals(firstName)))
			throw new Exception($"User with first name '{firstName}' already exists");

		if (_users.Any(u => u.LastName.Equals(lastName)))
			throw new Exception($"User with last name '{lastName}' already exists");

		var newUser = new User(firstName, lastName);
		_users.Add(newUser);
		return newUser;
	}

	public IList<User> GetAll() => _users;
}