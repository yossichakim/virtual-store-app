namespace DalApi;

public class AddException : Exception
{
    public AddException(string massage) :base($"the {massage} you try to add already exist")
    { }

}

public class NoFoundException : Exception
{
    public NoFoundException(string massage) : base($"the {massage} is not found")
    { }

}