namespace TISMonitor
{
    using System;

    [Serializable]
    public enum DeviceState
    {
        UNKNOWN,
        OK,
        RING,
        BUSY,
        ERROR
    }
}

