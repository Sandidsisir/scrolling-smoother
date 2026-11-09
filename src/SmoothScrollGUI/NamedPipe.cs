using System;
using System.IO.Pipes;

namespace SmoothScrollGUI;

internal class NamedPipe
{
    public readonly string Name;
    public NamedPipeClientStream Stream;

    public NamedPipe(string name)
    {
        Name = name;
    }

    public void Disconnect()
    {
        Stream?.Close();
    }

    public void EnsureConnected()
    {
        if (Stream != null && Stream.IsConnected)
        {
            return;
        }

        Stream = new NamedPipeClientStream(".", Name, PipeDirection.InOut, PipeOptions.Asynchronous);
        Stream.Connect();
        Stream.ReadMode = PipeTransmissionMode.Message;
    }
}
