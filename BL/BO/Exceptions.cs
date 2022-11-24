namespace BO;

public class AddException : Exception
{
    public AddException(Exception innerException, string massage = "") : base(massage, innerException)
    { }
}

public class NoFoundException : Exception
{
    public NoFoundException(Exception innerException, string massage = "") : base(massage, innerException)
    { }
}