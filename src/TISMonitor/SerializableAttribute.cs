namespace TISMonitor
{
    using System;

    [AttributeUsage(AttributeTargets.Delegate | AttributeTargets.Enum | AttributeTargets.Struct | AttributeTargets.Class, Inherited=false)]
    public sealed class SerializableAttribute : Attribute
    {
    }
}

