using Microsoft.AspNetCore.Mvc;

namespace Railway.Common;

public static class RailwayExtensions
{
	public static Result<TOut> OnSuccess<TIn, TOut>(this Result<TIn> input, Func<TIn, Result<TOut>> onSuccessFunction)
	{
		return input.IsSuccess
			? onSuccessFunction(input.Value)
			: Result<TOut>.Failure(input.Error);
	}



	public static Result<TOut> OnSuccess<TIn, TOut>(this Result<TIn> input, Func<TIn, TOut> onSuccessFunction)
	{
		return input.IsSuccess
			? Result<TOut>.Success(onSuccessFunction(input.Value))
			: Result<TOut>.Failure(input.Error);
	}



	public static Result<TOut> OnSuccess<TOut>(this UnitResult input, Func<TOut> onSuccessFunction)
	{
		return input.IsSuccess
			? Result<TOut>.Success(onSuccessFunction())
			: Result<TOut>.Failure(input.Error);
	}



	public static Result<T> DeadEnd<T>(this Result<T> input, Action<T> deadEndFunction)
	{
		if (input.IsSuccess)
			deadEndFunction(input.Value);

		return input;
	}



	public static Result<TOut> TryCatch<TIn, TOut>(this Result<TIn> input, Func<TIn, TOut> singleTrackFunction)
	{
		try
		{
			return input.IsSuccess
				? Result<TOut>.Success(singleTrackFunction(input.Value))
				: Result<TOut>.Failure(input.Error);
		}
		catch (Exception e)
		{
			return Result<TOut>.Failure(new PanicError(e));
		}
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



	public static Result<IEnumerable<T>> Combine<T>(this IEnumerable<Result<T>> results)
	{
		if (results.Count(r => r.IsFailure) > 0)
			return Result<IEnumerable<T>>.Failure(Error.Combine(results.Select(r => r.Error)));

		return Result<IEnumerable<T>>.Success(results.Select(r => r.Value));
	}



	public static IActionResult Handle<T>(this Result<T> result, ControllerBase controller)
	{
		if (result.IsSuccess)
			return controller.Ok(result.Value);

		return result.Error.ToActionResult(controller);
	}
}