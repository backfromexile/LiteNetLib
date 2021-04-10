using System;
using System.Net;
using System.Net.Sockets;

namespace LiteNetLib.Utils
{
    internal sealed class NtpRequest
    {
        private static readonly TimeSpan ResendTimer = TimeSpan.FromMilliseconds(1000);
        private static readonly TimeSpan KillTimer = TimeSpan.FromMilliseconds(10000);
        public const int DefaultPort = 123;
        private readonly IPEndPoint _ntpEndPoint;
        private TimeSpan _resendTime = ResendTimer;
        private TimeSpan _killTime = TimeSpan.Zero;

        public NtpRequest(IPEndPoint endPoint)
        {
            _ntpEndPoint = endPoint;
        }

        public bool NeedToKill => _killTime >= KillTimer;

        public bool Send(NetSocket socket, TimeSpan time)
        {
            _resendTime += time;
            _killTime += time;
            if (_resendTime < ResendTimer)
            {
                return false;
            }
            SocketError errorCode = 0;
            var packet = new NtpPacket();
            var sendCount = socket.SendTo(packet.Bytes, 0, packet.Bytes.Length, _ntpEndPoint, ref errorCode);
            return errorCode == 0 && sendCount == packet.Bytes.Length;
        }
    }
}
