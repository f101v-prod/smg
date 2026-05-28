using System;

public enum ResourceKind
{
    Red,
    Green,
    Blue
}

[Serializable]
public class ResourceCount
{
    public ResourceKind kind;
    public int count;
}
