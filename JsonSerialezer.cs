using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashMana.Models;
using CashMana.Models.Categories;
using Newtonsoft.Json;

namespace CashMana.JsonSerializer
{
    public class JsonSerilizer
    {
        
    }
    public static string ToJson(Profile o)
{
	string output = JsonConvert.SerializeObject(o);
	return output;
}


public static Profile ToProfile(string json)
{
	Profile profile = JsonConvert.DeserializeObject<Profile>(json);



	foreach(var account in profile.Accounts)
	{
		account.Categories.Salary.Amount = 0;
                account.Categories.Transport.Amount = 0;
                account.Categories.Telephone.Amount = 0;
                account.Categories.Food.Amount = 0;
                account.Categories.Entertaiment.Amount = 0;
                account.Categories.Internet.Amount = 0;
                account.Categories.Everything.Amount = 0;
		account.CurrentBalance = 0 + account.start;
		account.Histories.OrderBy(o = > o.DateOfOperation);
		foreach(var history in account.Histories)
		{
			if (history.Income)
			{
				account.CurrentBalance += history.Amount;
			}
			else
			{
				account.CurrentBalance -= history.Amount;
			}

		}

	}

	foreach(var account in profile.Accounts)
	{
		foreach(var history in account.Histories)
		{
			if (history.CurrentCategory.name == "Зарплата")
			{
				account.Categories.Salary.Amount += history.Amount;

			}
			else if (history.CurrentCategory.name == "Транспорт")
			{
				account.Categories.Transport.Amount += history.Amount;



			}
			else if (history.CurrentCategory.name == "Телефон")
			{

				account.Categories.Telephone.Amount += history.Amount;

			}

			else if (history.CurrentCategory.name == "Еда")
			{
				account.Categories.Food.Amount += history.Amount;
			}

			else if (history.CurrentCategory.name == "Развлечения")
			{
				account.Categories.Entertaiment.Amount += history.Amount;

			}

			else if (history.CurrentCategory.name == "Интернет")
			{

				account.Categories.Internet.Amount += history.Amount;
			}
			else
			{
				if (!history.Income)
				{

					account.Categories.Everything.Amount += history.Amount;
				}

			}

		}

		account.ListCategoryStats.Clear();
		account.ListCategoryStats.Add(new CategoryStat(){ amount = account.Categories.Transport.Amount, name = account.Categories.Transport.name });
		account.ListCategoryStats.Add(new CategoryStat(){ amount = account.Categories.Entertaiment.Amount, name = account.Categories.Entertaiment.name });
		account.ListCategoryStats.Add(new CategoryStat(){ amount = account.Categories.Food.Amount, name = account.Categories.Food.name });
		account.ListCategoryStats.Add(new CategoryStat(){ amount = account.Categories.Internet.Amount, name = account.Categories.Internet.name });
		account.ListCategoryStats.Add(new CategoryStat(){ amount = account.Categories.Telephone.Amount, name = account.Categories.Telephone.name });
		account.ListCategoryStats.Add(new CategoryStat(){ amount = account.Categories.Everything.Amount, name = account.Categories.Everything.name });

	}

	return profile;
}

}
