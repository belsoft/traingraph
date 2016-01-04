namespace TISMonitor
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct TimerMoveData
    {
        public string HeadElementID;
        public TimeSpan Interval;
        public MoveState State;
    }
}

