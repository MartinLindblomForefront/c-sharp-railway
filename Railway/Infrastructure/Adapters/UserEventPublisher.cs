using Railway.Domain;

namespace Railway.Infrastructure.Adapters;

public interface IUserEventPublisher
{
	public void PublishUserCreatedEvent(User user);
}

public class UserEventPublisher : IUserEventPublisher
{
	public void PublishUserCreatedEvent(User user)
	{
		Console.WriteLine($"EVENT: User '{user.Id}' created");
	}
}
