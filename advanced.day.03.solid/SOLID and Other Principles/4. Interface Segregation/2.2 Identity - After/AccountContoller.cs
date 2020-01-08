namespace InterfaceSegregationIdentityAfter
{
    using InterfaceSegregationIdentityAfter.Contracts;

    public class AccountContoller
    {
        private IPassword manager;

        public AccountContoller(IPassword manager)
        {
            this.manager = manager;
        }

        public void ChangePassword(string oldPass, string newPass)
        {
            this.manager.ChangePassword(oldPass, newPass);
        }
    }
}
