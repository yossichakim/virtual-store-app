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

public class NoValidException : Exception
{
    public NoValidException(string massage) : base($"the {massage} are not valid")
    {
    }
}

public class ErrorDeleteException : Exception
{
    public ErrorDeleteException(string massage) : base($"You are trying to delete a {massage}")
    {
    }
}

public class ErrorUpdateException : Exception
{
    public ErrorUpdateException(string massage) : base($"the order as been {massage}")
    {
    }
}