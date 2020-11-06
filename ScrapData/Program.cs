using ScrapData.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace ScrapData
{
	class Program
	{
		static void Main(string[] args)
		{
			List<string> props = new List<string>() { "FirstName", "LastName", "SubscriptionID" };

			var obj = new User();
			obj.FirstName = "Utkarsh";
			obj.LastName = "Chauhan";
			obj.Address = new Address();
			obj.Address.City = "Mumbai";
			obj.Address.State = "Mumbai";
			obj.Address.Country = "Mumbai";
			obj.Address.Line1 = "Line1";
			obj.Address.Line2 = "Line2";
			obj.Mobile = new Phone();
			obj.Mobile.CountryCode = "123";
			obj.Mobile.Number = "8569658";
			obj.Mobile.Ext = "352";
			obj.Home = new Phone();
			obj.Home.CountryCode = "123";
			obj.Home.Number = "8569658";
			obj.Home.Ext = "352";
			obj.Accounts = new List<Account>()
			{
				new Account()
				{
					ID = 123,
					Name = "CC",
					SubscriptionID = "sedfsf"
				},
				new Account()
				{
					ID = 54,
					Name = "XYZ",
					SubscriptionID = "terterte"
				}
			};

			ScrapProperties(obj, props);
			PrintProperties(obj, 2);
			Console.ReadKey();
		}

		public static void ScrapProperties(object obj, List<string> propertiesToScrap)
		{
			string scrapText = "XXXX";

			if (obj == null)
				return;

			Type objType = obj.GetType();
			PropertyInfo[] properties = objType.GetProperties();

			foreach (PropertyInfo property in properties)
			{
				object propValue;

				if (objType == typeof(string))
					return; // Handled at a higher level, so nothing to do here.

				if (property.PropertyType.IsArray)
					propValue = (Array)property.GetValue(obj, null);
				else
					propValue = property.GetValue(obj, null);

				var elems = propValue as IList;

				if (elems != null)
				{
					for (int i = 0; i < elems.Count; ++i)
					{

						if (objType != typeof(string))
							ScrapProperties(elems[i], propertiesToScrap);
					}
				}
				else
				{
					if (property.PropertyType.Assembly == objType.Assembly)
					{
						ScrapProperties(propValue, propertiesToScrap);
					}
					else
					{
						if (propertiesToScrap.Any(x => x == property.Name))
						{
							property.SetValue(obj, scrapText, null);
						}
					}
				}
			}
		}

		public static void PrintProperties(object obj, int indent = 1)
		{
			if (obj == null)
				return;

			string indentString = new string(' ', indent);

			Type objType = obj.GetType();
			PropertyInfo[] properties = objType.GetProperties();

			foreach (PropertyInfo property in properties)
			{
				object propValue;

				if (objType == typeof(string))
					return; // Handled at a higher level, so nothing to do here.

				if (property.PropertyType.IsArray)
					propValue = (Array)property.GetValue(obj, null);
				else
					propValue = property.GetValue(obj, null);

				var elems = propValue as IList;

				if (elems != null)
				{
					Console.WriteLine("{0}{1}: IList of {2}", indentString, property.Name, propValue.GetType().Name);

					for (int i = 0; i < elems.Count; ++i)
					{
						Console.WriteLine("{0}{1}[{2}] == {3}", indentString, property.Name, i, elems[i]);

						if (objType != typeof(string))
							PrintProperties(elems[i], indent + 3);
					}
				}
				else
				{
					if (property.PropertyType.Assembly == objType.Assembly)
					{
						Console.WriteLine("{0}{1}:", indentString, property.Name);

						PrintProperties(propValue, indent + 2);
					}
					else
					{
						Console.WriteLine("{0}{1}: {2}", indentString, property.Name, propValue);
					}
				}
			}
		}
	}
}
