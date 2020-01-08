using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregationIdentityAfter.Contracts
{
	public interface ILogin
	{
		void Login(string username, string password);
		void Logount();
	}
}
