namespace Railway.Common;

public sealed class UnitResult
{
	public Error Error => !IsSuccess && _error != null ? _error : throw new InvalidOperationException("Tried to access Error property for successful result");

	public readonly bool IsSuccess;
	public bool IsFailure => !IsSuccess;

	private readonly Error? _error;

	private UnitResult()
	{
		IsSuccess = true;
	}

	private UnitResult(Error error)
	{
		_error = error;
		IsSuccess = false;
	}

	public static UnitResult Success()
		=> new();

	public static UnitResult Failure(Error error)
		=> new(error);
}
