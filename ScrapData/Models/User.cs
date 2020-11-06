using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScrapData.Models
{
	class User
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public Address Address { get; set; }
		public Phone Home { get; set; }
		public Phone Mobile { get; set; }
		public List<Account> Accounts { get; set; }
	}

	public class Account
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string SubscriptionID { get; set; }
	}

	public class Phone
	{
		public string CountryCode { get; set; }
		public string Number { get; set; }
		public string Ext { get; set; }
	}

	public class Address
	{
		public string Line1 { get; set; }
		public string Line2 { get; set; }
		public string City { get; set; }
		public string State { get; set; }
		public string Country { get; set; }
	}
}
