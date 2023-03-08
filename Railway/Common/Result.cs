namespace Railway.Common;

public sealed class Result<T>
{
	public T Value => IsSuccess && _value != null ? _value : throw new InvalidOperationException("Tried to access Value property for non-successful result");
	public Error Error => !IsSuccess && _error != null ? _error : throw new InvalidOperationException("Tried to access Error property for successful result");

	public readonly bool IsSuccess;
	public bool IsFailure => !IsSuccess;

	private readonly T? _value;
	private readonly Error? _error;

	private Result(T value)
	{
		_value = value;
		IsSuccess = true;
	}

	private Result(Error error)
	{
		_error = error;
		IsSuccess = false;
	}

	public static Result<T> Success(T value)
		=> new(value);

	public static Result<T> Failure(Error error)
		=> new(error);
}