using ErrorOr;

namespace BurberBreakfast.ServicesErrors;

public static class Errors
{
    public static class Breakfast
    {
        public static  Error NotFound => Error.NotFound(
            code: "Breakfast.NotFound",
            description: "The requested Breakfast was not found.");
    }
}