namespace TestProject.Common.Exception
{
    public class UserAlreadyExists : TestProjectException
    {
        private readonly string _email;

        public UserAlreadyExists(string email)
        {
            _email = email;
        }

        public override string Message => $"User with email {_email} already exists";
    }
}