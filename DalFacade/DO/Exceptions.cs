namespace DO;

public class AddException : Exception
{
    public AddException(string massage) : base($"THE {massage} YOU TRY TO ADD ALREADY EXIST")
    { }
}

public class NoFoundException : Exception
{
    public NoFoundException(string massage) : base($"THE {massage} DOES NOT EXIST")
    { }
}