using System.Reflection;

namespace Roboline.Service;

public abstract class AssemblyMarker
{
    public static readonly Assembly Assembly = typeof(AssemblyMarker).Assembly;
}