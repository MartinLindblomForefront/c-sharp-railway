namespace Railway.Domain;

public record Entity
{
	public Guid Id { get; } = Guid.NewGuid();
}
