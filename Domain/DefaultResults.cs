namespace Domain
{
    public class DefaultResults : ResultBase
    {
        public static Result NotFoundResult => GetResult("NotFound", "Not found");
        public static Result InvalidRequest => GetResult("InvalidRequest", "Invalid request");
    }
}
