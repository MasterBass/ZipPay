namespace TestProject.Common.Exception
{
    public class InputParameterIsNotCorrect : TestProjectException
    {
        private readonly string _message;
        public InputParameterIsNotCorrect(string message)
        {
            _message = message;
        }
        
        public override string Message => $"Argument Exception: {_message}";
    }
}