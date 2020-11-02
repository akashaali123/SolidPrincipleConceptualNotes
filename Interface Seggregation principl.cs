
Interface Segregation Principle:
--------------------------------

No client should be forced to depend on method it does not use
One fat Interface need to split to many smaller and relevant interface so that client know about interface
that are relevant to them.


e.g
--

public interface IUser
{
	void Register();
	Void Login();
	Void SendEmail();
	Void Logging();
}

public class User : IUser
{
	public void Register()
	{
		//Add Register Logic
	}
	public Void Login(){}
	public Void SendEmail(){}
	public Void Logging(){}
}

//In above example send email and logging is not only a user functionality we use it any other class where login and register are use less on those case
//It is also invalidate single responsibilty principle because logging and send email is not a part of user functionality 
// So we split one fat interface to samller relevent interface

public interface IUser
{
	void Register();
	Void Login();
}

public interface IEmail
{
	Void SendEmail();
}

public interface ILogging
{
	Void Logging();
}

