using Microsoft.AspNetCore.Mvc;

namespace Railway.Common;

public abstract record Error(string Message)
{
	public abstract string Code { get; }
	public string Message { get; } = Message;

	public string Format() => $"{Code} - {Message}";

	public virtual IActionResult ToActionResult(ControllerBase controller) => controller.StatusCode(500, Format());

	public static Error Combine(IEnumerable<Error> errors)
	{
		return new CombinedError(string.Join(",", errors.Select(e => e.Format())));
	}
}

public record CombinedError(string combinedMessage) : Error(combinedMessage)
{
	public override string Code => "general.combined";
}

public record PanicError(Exception Exception) : Error(Exception.ToString())
{
	public override string Code => "general.panic";
}

public record NotFoundError : Error
{
	public override string Code => "general.not-found";
	private static string CreateMessage(string identifier, string resourceName) => $"'{resourceName}' with identifier {identifier} was not found";

	public NotFoundError(string identifier, string resourceName) : base(CreateMessage(identifier, resourceName)) { }

	public NotFoundError(Guid identifier, string resourceName) : this(identifier.ToString(), resourceName) { }

	public override IActionResult ToActionResult(ControllerBase controller) => controller.NotFound(Format());
}

public record NullValueError : Error
{
	public override string Code => "general.null-value";

	public NullValueError(string message) : base(message) { }

	public override IActionResult ToActionResult(ControllerBase controller) => controller.BadRequest(Format());
}

public record EmptyStringError : Error
{
	public override string Code => "general.empty-string";

	public EmptyStringError(string message) : base(message) { }

	public override IActionResult ToActionResult(ControllerBase controller) => controller.BadRequest(Format());
}