namespace Problem;


internal record User
{
	public Guid Id { get; } = Guid.NewGuid();
	public string FirstName { get; private init; }
	public string LastName { get; private init; }

	protected User() {}

	public User(string firstName, string lastName)
	{
		if (string.IsNullOrWhiteSpace(firstName))
			throw new Exception("First name must not be null or whitespace");

		if (string.IsNullOrWhiteSpace(lastName))
			throw new Exception("Last name must not be null or whitespace");

		FirstName = firstName;
		LastName = lastName;
	}

	public static Result<User> Create(string firstName, string lastName)
	{
		if (string.IsNullOrWhiteSpace(firstName))
			return Result<User>.Failure(new Error("First name must not be null or whitespace"));

		if (string.IsNullOrWhiteSpace(lastName))
			return Result<User>.Failure(new Error("Last name must not be null or whitespace"));

		return Result<User>.Success(new User()
		{
			FirstName = firstName,
			LastName = lastName,
		});
	}
}

