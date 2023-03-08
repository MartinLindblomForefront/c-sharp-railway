namespace Problem;

internal class ResultLogger
{
	internal Result<IList<User>> LogUsers(IList<User> users)
	{
		foreach (var user in users)
		{
			Console.WriteLine($"{user.FirstName}, {user.LastName} - {user.Id}");
		}

		return Result<IList<User>>.Success(users);
	}
}