using DO;
namespace DalApi;

public class AddIsExists : Exception
{
    public AddIsExists(string massage) :base($"the {massage} you try to add already exist")
    { }

}

public class EntityOrIDNoFound : Exception
{
    public EntityOrIDNoFound(string massage) : base($"the {massage} is not found")
    { }

}

