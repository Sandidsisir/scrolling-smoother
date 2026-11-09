using System;
using System.Collections.Concurrent;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace SmoothScrollGUI;

public class PiedPiper
{
    private const int BufferSize = 4096;

    private readonly NamedPipe _readerPipe;
    private readonly NamedPipe _writerPipe;
    private readonly Thread _readerThread;
    private readonly Thread _writerThread;
    private readonly BlockingCollection<string> _writeQueue = new BlockingCollection<string>();

    public event EventHandler<MessageReceivedEventArgs> MessageReceived;

    public class MessageReceivedEventArgs : EventArgs
    {
        public string Message { get; set; }
        public DateTime Time { get; set; }
    }

    public PiedPiper(string readerName, string writerName)
    {
        _readerPipe = new NamedPipe(readerName);
        _writerPipe = new NamedPipe(writerName);

        _readerThread = new Thread(ReaderThreadWorker) { IsBackground = true };
        _readerThread.Start();

        _writerThread = new Thread(WriterThreadWorker) { IsBackground = true };
        _writerThread.Start();
    }

    public void Stop()
    {
        _readerPipe?.Disconnect();
        _writerPipe?.Disconnect();
        _readerThread?.Abort();
        _writerThread?.Abort();
    }

    public void Send(object message)
    {
        Send(JsonConvert.SerializeObject(message));
    }

    public void Send(string message)
    {
        _writeQueue.Add(message);
    }

    protected virtual void OnMessageReceived(MessageReceivedEventArgs e)
    {
        MessageReceived?.Invoke(this, e);
    }

    private void ReaderThreadWorker()
    {
        while (true)
        {
            ReadAllInput();
        }
    }

    private void WriterThreadWorker()
    {
        while (true)
        {
            SendAllOutput();
        }
    }

    private void ReadAllInput()
    {
        _readerPipe.EnsureConnected();

        try
        {
            string message = ReadMessageString(_readerPipe.Stream);
            if (message.EndsWith("\0"))
            {
                message = message.Remove(message.Length - 1);
            }

            OnMessageReceived(new MessageReceivedEventArgs
            {
                Message = message,
                Time = DateTime.Now
            });
        }
        catch (Exception)
        {
            // The pipe was closed or the peer went away; reconnect on the next loop.
        }
    }

    private void SendAllOutput()
    {
        _writerPipe.EnsureConnected();

        try
        {
            using (var writer = new StreamWriter(_writerPipe.Stream))
            {
                foreach (string message in _writeQueue.GetConsumingEnumerable())
                {
                    if (!_writerPipe.Stream.IsConnected)
                    {
                        break;
                    }

                    writer.Write(message);
                    writer.Flush();
                }
            }

            _writerPipe.Stream.WaitForPipeDrain();
        }
        catch (Exception)
        {
            // Reconnect on the next loop.
        }
    }

    protected static string ReadMessageString(PipeStream stream)
    {
        return UTF8BytesToString(ReadMessage(stream));
    }

    protected static byte[] ReadMessage(PipeStream stream)
    {
        using (var buffer = new MemoryStream())
        {
            byte[] chunk = new byte[BufferSize];
            do
            {
                buffer.Write(chunk, 0, stream.Read(chunk, 0, chunk.Length));
            }
            while (!stream.IsMessageComplete);

            return buffer.ToArray();
        }
    }

    protected static string UTF8BytesToString(byte[] bytes)
    {
        return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
    }
}
