Inversion Of Control:
---------------------

control refers to any additional responsibilities a class has, other than its main responsibility, such as control over the flow of an application,
or control over the dependent object creation and binding (Remember SRP - Single Responsibility Principle).


Depenedency Inversion Principle:
--------------------------------

Higher level module should not depend on lower level module both depend on abstraction.
Abstraction should not depend on details. Details depend on Abstraction 

Factory class implement inversion of control principle 

EmployeeBusiness class refers its additional responsibility to factoryclass while it's focus its main functionality which is employee
Now factoryclass control EmployeeBusiness class additional functionality like dependent class object creation

Now Single Responsibilty principle also valid for this scenario 
every class only have one reason to change




public interface IEmployeeDataAcess   //Abstract
{
	Void Insert(Employee);
}


public class EmployeeDbAccess: IEmployeeDataAccess  //Description
{

	public void Insert(Employee)
	{
			//Insert in Db
	}

}

public class EmployeeFromFile : IEmployeeDataAccess   //Description
{

	public void Insert(Employee)
	{
		//Insert In file
	}
}


//It is like Inversion of control using factory method

public class FacroryClass
{
    public static IEmployeeDataAcess GetEmployeeDataAccessObj()
    {
		    //Assign Object Either of File type object or db type object
            return new EmployeeDbAccess();
    }
}

//Not depend on concrete type class depend ion abstraction 
//Both High level and low level modeule depend on Abstraction
public class EmployeeBussiness
{
	private readonly IEmployeeDataAcess employee;//Abstraction
	
	public EmployeeBussiness()
	{
		employee = FacroryClass.GetEmployeeDataAccessObj();//If we assign db type class object it provide db type implementation
														   //If we assign file type  class object it provide file type implementation
	}
    private void InsertEmployee(Employee employee)
	{
		employee.Insert(employee);
	}

}


if we inject this class in controller so we make new interface for EmployeeBussiness and inherit EmployeeBussiness with interface and 
inject this interface on controller.


It is like Repository Pattern where we hide data access logic

