using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterfaceSegregationIdentityAfter.Contracts
{
	public interface IRegister : IPassword
	{
		void Register(string username, string password);
	}
}
