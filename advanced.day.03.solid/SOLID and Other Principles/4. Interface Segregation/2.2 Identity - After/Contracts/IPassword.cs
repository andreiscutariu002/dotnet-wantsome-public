using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregationIdentityAfter.Contracts
{
	public interface IPassword
	{
		int MinRequiredPasswordLength { get; set; }

		int MaxRequiredPasswordLength { get; set; }
		void ChangePassword(string oldPass, string newPass);
	}
}
