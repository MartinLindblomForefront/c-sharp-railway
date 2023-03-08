using Microsoft.AspNetCore.Mvc;
using Railway.Common;
using System.Text.RegularExpressions;

namespace Railway.Domain;

public record Email
{
	public string Value { get; private init; }

	protected Email() { }

	public static Result<Email> Create(string value)
	{
		return Validate(value)
			.OnSuccess<Email>(() => new() { Value = value });
	}

	private static UnitResult Validate(string value)
	{
		var regex = new Regex("^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$");
		if (!regex.IsMatch(value))
			return UnitResult.Failure(new InvalidEmailError($"'{value}' is not a valid email address"));

		return UnitResult.Success();
	}



	public record InvalidEmailError : Error
	{
		public override string Code => "domain.email.invalid-email";
		
		public InvalidEmailError(string message) : base(message) { }

		public override IActionResult ToActionResult(ControllerBase controller) => controller.BadRequest(Format());
	}
}
