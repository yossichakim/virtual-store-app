namespace DO;

[Serializable]
public class AddException : Exception
{
    public AddException(string massage) : base($"THE {massage} YOU TRY TO ADD ALREADY EXIST")
    { }
}

[Serializable]
public class NoFoundException : Exception
{
    public NoFoundException(string massage) : base($"THE {massage} DOES NOT EXIST")
    { }
}

[Serializable]
public class DalConfigException : Exception
{
    public DalConfigException(string msg) : base(msg) { }
    public DalConfigException(string msg, Exception ex) : base(msg, ex) { }
}