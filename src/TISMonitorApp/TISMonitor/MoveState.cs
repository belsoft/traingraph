namespace TISMonitor
{
    using System;

    public enum MoveState
    {
        Unknown,
        Move,
        MoveToPerron,
        WaitAtPerron,
        FinalPerronReached,
        Stop
    }
}

