using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OptikeyPlugins
{
    class BeamWrapper
    {
        private const string DllName = "tracker_client.dll";

        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr create_tracker_instance(string hostname, int communication_port);

        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern void release_tracker_instance(IntPtr p_instance);

        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        public static extern ScreenGazeInfo get_screen_gaze_info(IntPtr p_instance);

        [DllImport(DllName, SetLastError = true, CallingConvention = CallingConvention.Cdecl)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static extern bool connected(IntPtr p_instance);

        public static bool IsLost(ScreenGazeInfo info)
        {
            return info.is_lost == 257;
        }
    }

    public enum TrackingConfidence : uint
    {
        UNRELIABLE,
        LOW,
        MEDIUM,
        HIGH
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ScreenGazeInfo
    {
        public uint screen_id;
        public uint x;
        public uint y;
        public TrackingConfidence confidence;
        public uint is_lost; // Can't marshal as bool, ends up as 256 if not-lost, 257 if lost
    }
}