using Problem;


//var userService = new UserService();
//try
//{
//	userService.CreateUser("Martin", "Katt");

//	var users = userService.GetAll();
//	foreach (var user in users)
//	{
//		Console.WriteLine($"{user.FirstName}, {user.LastName} - {user.Id}");
//	}
//}
//catch (Exception ex)
//{
//	Console.Error.WriteLine(ex.ToString());
//}

//Console.ReadLine();









//var userService = new UserService();

//try
//{
//	userService.CreateUser("Anders", "And");
//}
//catch (Exception ex)
//{
//	Console.WriteLine(ex.ToString());
//}

//var users = userService.GetAll();
//foreach (var user in users)
//{
//	Console.WriteLine($"{user.FirstName}, {user.LastName} - {user.Id}");
//}

//Console.ReadLine();








//var resultUserService = new ResultUserService();

//var createUserResult = resultUserService.CreateUser("     ", "And");
//if (createUserResult.IsFailure)
//	Console.WriteLine(createUserResult.Error.Message);



//var usersResult = resultUserService.GetAll();
//if (usersResult.IsFailure)
//	Console.WriteLine(usersResult.Error.Message);

//foreach (var user in usersResult.Value)
//{
//	Console.WriteLine($"{user.FirstName}, {user.LastName} - {user.Id}");
//}

//Console.ReadLine();









var logger = new ResultLogger();
var resultUserService = new ResultUserService();

resultUserService
	.CreateUser("    ", "Katt")
	.OnSuccess(_ => resultUserService.GetAll())
	.OnSuccess(logger.LogUsers)
	.Handle();


Console.ReadLine();

