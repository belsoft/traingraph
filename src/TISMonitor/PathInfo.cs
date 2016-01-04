namespace TISMonitor
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential), Serializable]
    public struct PathInfo
    {
        public PathElement FromElement;
        public int FromSource;
        public PathElement ToElement;
        public int ToSource;
    }
}

