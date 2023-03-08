using Railway.Common;

namespace Railway.Domain;

public record Name
{
	public string Value { get; private init; }

	protected Name() { }

	public static Result<Name> Create(string value)
	{
		return Validate(value)
			.OnSuccess<Name>(() => new() { Value = value });
	}

	private static UnitResult Validate(string value)
	{
		if (string.IsNullOrWhiteSpace(value))
			return UnitResult.Failure(new EmptyStringError("Name must not be NULL nor just white-space"));

		return UnitResult.Success();
	}
}