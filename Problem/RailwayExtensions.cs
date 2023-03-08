namespace Problem;

internal static class RailwayExtensions
{
	public static Result<TOut> Operation<TIn, TOut>(TIn value)
	{
		// Do some work that can result in success or failure
		// .
		// .
		// .

		return Result<TOut>.Failure(new Error("Not implemented"));
	}

	public static Result<TOut> OnSuccess<TIn, TOut>(
		this Result<TIn> input,
		Func<TIn, Result<TOut>> onSuccessFunction)
	{
		return input.IsSuccess
			? onSuccessFunction(input.Value)
			: Result<TOut>.Failure(input.Error);
	}



	public static Result<T> DeadEnd<T>(this Result<T> input, Action<T> deadEndFunction)
	{
		if (input.IsSuccess)
			deadEndFunction(input.Value);

		return input;
	}



	public static Result<TOut> OnBoth<TIn, TOut>(
		this Result<TIn> input,
		Func<TIn, TOut> successSingleTrackFunction,
		Func<TIn, TOut> failureSingleTrackFunction)
	{
		if (input.IsSuccess)
		{
			return Result<TOut>.Success(successSingleTrackFunction(input.Value));
		}

		failureSingleTrackFunction(input.Value);
		return Result<TOut>.Failure(input.Error);
	}

	public static void Handle<TIn>(this Result<TIn> input)
	{
		if (input.IsSuccess)
			Console.WriteLine("Ended in Success :D");
		else
			Console.WriteLine($"Ended in Failure :( with '{input.Error.Message}'");
	}
}