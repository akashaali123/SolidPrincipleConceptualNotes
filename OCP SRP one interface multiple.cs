

Open Close Principle , Single Responsibilty , One Interface Multiple Implementation:
------------------------------------------------------------------------------------

public class Item
{
	private bool Authenticate Item(Item item)
	{
		if(itemType=="Catalog")
		{
			//Catalog Item Authenticate Logic here
		}
		else if(itemType=="Standard")
		{
			//Standard Item Authenticate Logic here
		}
		//In future if we add new functionality we will need to modify this class
		//which is costly for maintainablity,testing etc
	}
}


Above example badly invalidate the single responsibilty and open close principle 



Implement Open close Principle:
-------------------------------

Below example implement single responsiblity , open close principle , IOC container  and Dependency Injection,
One Interface multiple implementtation core concept.

-Now Every class has only one reason to change which valid single responsibilty principle
-Now we extend class with new functionality without modifying its source code by creating new class
 which is also validate single responsibilty and open close principle 
-We resolve dependency in startup class through IOC container
-One Interface extend multiple class so we resolve the issue by adding new current name property. So it is easy for us to call desire object through IItem 


public interface IItem   //Abstraction
{
		string CurrentName { get; }
		bool Authenticate Item(Item item);
}

public class catlogItem : IItem 
{
	public string CurrentName => nameof(catlogItem);
	
	private bool Authenticate Item(Item item)
	{
		//Catalog Item Authenticate Logic here 
	}
}

public class StandardItem : IItem
{
	public string CurrentName => nameof(StandardItem);
	
	private bool Authenticate Item(Item item)
	{
		//Standard Item Authenticate Logic here 
	}
}

public class QuoteItem : IItem
{
	public string CurrentName => nameof(QuoteItem);
	
	private bool Authenticate Item(Item item)
	{
		//Quote Item Authenticate Logic here 
	}
}

public class OtherItem : IItem
{
	public string CurrentName => nameof(OtherItem);
	
	private bool Authenticate Item(Item item)
	{
		//Other Item Authenticate Logic here 
	}
}


-BasketRepo does not depend on concrete type any item class 
-- Both BasketRepo(High level) and ItemClasses(Low level) depend on abstraction which is IItem
-- IItem not depend on Authenticate Definition while Authenticate Definition depend on abstraction(IItem)
So it is also validate Dependency Inversion principle

public class BasketRepo
{
	private readonly IEnumerable<IItem> item;
	
	public BasketRepo(IEnumerable<IItem> item)
	{
		this.item = item
	}
	
	private bool AddtoBasket(Item item)
	{
		if(item.CatalogItemId > 0)
		{
			//If item is catalog assing catalogItem object to IItem e.g
			//IItem item = new CatalogItem();
			var item = item.FirstOrDefault(h => h.CurrentName == AppSettings.CatalogItemKey);
			item.Authenticate(item);
		}
		else if(item.StandardItemId > 0)
		{
			//If item is standard assing StandardItem object to IItem e.g
			//IItem item = new StandardItem();
			var item = item.FirstOrDefault(h => h.CurrentName == AppSettings.StandardItemKey);
			item.Authenticate(item);
		}
		else if(item.QuoteItem > 0)
		{
			//If item is Quote assing QuoteItem object to IItem e.g
			//IItem item = new QuoteItem();
			var item = item.FirstOrDefault(h => h.CurrentName == AppSettings.QuoteItemKey);
			item.Authenticate(item);
		} 
		else if(item.OtherItemId)
		{
			//If item is Other assing OtherItem object to IItem e.g
			//IItem item = new OtherItem();
			var item = item.FirstOrDefault(h => h.CurrentName == AppSettings.OtherItem);
			item.Authenticate(item);
		} 
		
	}
}

startup class

service.Addscoped<IItem,catalogItem>
service.Addscoped<IItem,StandardItem>
service.Addscoped<IItem,QuoteItem>
service.Addscoped<IItem,OthItem>


Appsettion.JSON

"AppSettings": {
  "CatalogItemKey": "catlogItem",
  "QuoteItemKey": "QuoteItem",
  "StandardItemKey": "StandardItem",
  "OtherItemKey": "OtherItem",
}



Like above if we add new functionality by adding new classes without modifiying existing logic