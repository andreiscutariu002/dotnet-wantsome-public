using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregationIdentityAfter
{
	using InterfaceSegregationIdentityAfter.Contracts;
	class PasswordManager : IPassword
	{
		public int MinRequiredPasswordLength { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
		public int MaxRequiredPasswordLength { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		public void ChangePassword(string oldPass, string newPass)
		{
			throw new NotImplementedException();
		}
	}
}
