namespace InterfaceSegregationIdentityAfter.Contracts
{
    using System.Collections.Generic;

    public interface IAccount
    {
        bool RequireUniqueEmail { get; set; }

       

        

        

        

        IEnumerable<IUser> GetAllUsersOnline();

        IEnumerable<IUser> GetAllUsers();

        IUser GetUserByName(string name);
    }
}
