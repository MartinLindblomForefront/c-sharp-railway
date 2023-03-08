using Railway.Common;

namespace Railway.Domain;

public record User : Entity
{
	public Name Name { get; private init; }
	public Email Email { get; private init; }
	public PhoneNumber PhoneNumber { get; private init; }

	protected User() { }

	public static Result<User> Create(Name name, Email email, PhoneNumber phoneNumber)
	{
		return Validate(email, phoneNumber)
			.OnSuccess<User>(() => new()
				{
					Name = name,
					Email = email,
					PhoneNumber = phoneNumber
				}
			);
	}

	private static UnitResult Validate(Email email, PhoneNumber phoneNumber)
	{
		if (email == null)
			return UnitResult.Failure(new NullValueError("Must specify Email for user"));

		if (phoneNumber == null)
			return UnitResult.Failure(new NullValueError("Must specify Phone Number for user"));

		return UnitResult.Success();
	}
}
