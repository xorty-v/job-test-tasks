using System.Reflection;

namespace Roboline.Persistence;

public abstract class AssemblyMarker
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly;
}