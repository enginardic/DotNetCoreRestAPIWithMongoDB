namespace Domain
{
    public class ResultBase
    {
        public static Result GetResult(string code, string message)
        {
            return new Result { Title = code, Message = message };
        }
    }
}