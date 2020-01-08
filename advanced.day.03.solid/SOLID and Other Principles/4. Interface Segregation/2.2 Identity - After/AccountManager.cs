namespace InterfaceSegregationIdentityAfter
{
    using System.Collections.Generic;

    using InterfaceSegregationIdentityAfter.Contracts;

    public class AccountManager : IAccount
    {
        public bool RequireUniqueEmail { get; set; }


        

        public IEnumerable<IUser> GetAllUsersOnline()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IUser> GetAllUsers()
        {
            throw new System.NotImplementedException();
        }

        public IUser GetUserByName(string name)
        {
            throw new System.NotImplementedException();
        }
    }
}
