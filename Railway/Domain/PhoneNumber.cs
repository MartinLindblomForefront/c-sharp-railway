using Microsoft.AspNetCore.Mvc;
using Railway.Common;
using System.Text.RegularExpressions;

namespace Railway.Domain;

public record PhoneNumber
{
	public string Value { get; private init; }

	protected PhoneNumber() { }

	public static Result<PhoneNumber> Create(string value)
	{
		return Validate(value)
			.OnSuccess<PhoneNumber>(() => new() { Value = value });
	}

	private static UnitResult Validate(string value)
	{
		var regex = new Regex("^[\\d\\-\\s]+$");
		if (!regex.IsMatch(value))
			return UnitResult.Failure(new InvalidPhoneNumberError($"'{value}' is not a valid phone number, must only contain numbers, dashes, or spaces"));

		return UnitResult.Success();
	}



	public record InvalidPhoneNumberError : Error
	{
		public override string Code => "domain.phone-number.invalid-phone-number";

		public InvalidPhoneNumberError(string message) : base(message) { }

		public override IActionResult ToActionResult(ControllerBase controller) => controller.BadRequest(Format());
	}
}
