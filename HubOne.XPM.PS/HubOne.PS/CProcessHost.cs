using System;
using System.Diagnostics;
using System.IO.Pipes;
using System.IO;
using System.Threading;

namespace HubOne.PS
{
  class ReceivedEventArg : EventArgs
  {
    public string Data;

    public ReceivedEventArg(string paramData)
    {
      this.Data = paramData;
    }
  }

  /// <summary>
  /// Class for managing the child process lifetime
  /// </summary>
  class CProcessHost : IDisposable
  {
    public const int BUFFER_SIZE = 1024 * 2;

    private string m_PipeID;
    private Process m_ChildProcess;
    private NamedPipeServerStream m_PipeServerStream;
    private bool m_IsDisposing;
    private Thread m_PipeMessagingThread;
    public string WorkflowMaxAccountKey { get; set; }
    public class WorkflowMaxKeyAcquiredEventArgs : EventArgs
    {
        public string Key { get; set; }
        public WorkflowMaxKeyAcquiredEventArgs(string key)
        {
            Key = key;
        }
    }
    public delegate void WorkflowMaxKeyAcquiredEventHandler(object sender, WorkflowMaxKeyAcquiredEventArgs e);
    public event WorkflowMaxKeyAcquiredEventHandler WorkflowMaxKeyAcquiredHandlerEvent;
    
    /// <summary>
    /// Constructor
    /// </summary>
    public CProcessHost()
    {

    }

    /// <summary>
    /// Starts the IPC server and run the child process
    /// </summary>
    /// <param name="paramUID">Unique ID of the named pipe</param>
    /// <param name="token">The Token</param>
    /// <param name="keyGenFilePath">The KeyGen File path</param>
    /// <returns></returns>
    public bool Start(string paramUID, string token, string keyGenFilePath)
    {
        m_PipeID = paramUID;

        m_PipeMessagingThread = new Thread(new ThreadStart(StartIPCServer));
        m_PipeMessagingThread.Name = this.GetType().Name + ".PipeMessagingThread";
        m_PipeMessagingThread.IsBackground = true;
        m_PipeMessagingThread.Start();

        var fileExe = keyGenFilePath;

        var processInfo = new ProcessStartInfo(fileExe, token + "|||" + this.m_PipeID);
        m_ChildProcess = Process.Start(processInfo);      

        return true;
    }

    /// <summary>
    ///  Send message to the child process
    /// </summary>
    /// <param name="paramData"></param>
    /// <returns></returns>
    public bool Send(string paramData)
    {

      return true;
    }

    /// <summary>
    /// Start the IPC server listener and wait for
    /// incomming messages from the appropriate child process
    /// </summary>
    void StartIPCServer()
    {
      if (m_PipeServerStream == null)
      {
        m_PipeServerStream = new NamedPipeServerStream(m_PipeID,
                                                      PipeDirection.InOut,
                                                      1,
                                                      PipeTransmissionMode.Byte,
                                                      PipeOptions.Asynchronous,
                                                      BUFFER_SIZE,
                                                      BUFFER_SIZE);

      }

      // Wait for a client to connect
      Console.WriteLine(string.Format("{0}:Waiting for child process connection...", m_PipeID));
      try
      {
        //Wait for connection from the child process
        m_PipeServerStream.WaitForConnection();
        Console.WriteLine(string.Format("Child process {0} is connected.", m_PipeID));
      }
      catch (ObjectDisposedException exDisposed)
      {
        Console.WriteLine(string.Format("StartIPCServer for process {0} error: {1}", this.m_PipeID, exDisposed.Message));
      }
      catch (IOException exIO)
      {
        Console.WriteLine(string.Format("StartIPCServer for process {0} error: {1}", this.m_PipeID, exIO.Message));
      }

      bool retRead = true; ;
      while (retRead && !m_IsDisposing)
      {
        retRead = StartAsyncReceive();
        Thread.Sleep(30);
      }
    }

    /// <summary>
    /// Read line of text from the connected client
    /// </summary>
    /// <returns>return false on pipe communication exception</returns>
    bool StartAsyncReceive()
    {
      var sr = new StreamReader(m_PipeServerStream);
      try
      {
        string str = sr.ReadLine();

        if (string.IsNullOrEmpty(str))
        {
          // The client is down
          return false;
        }
        else
        {
            WorkflowMaxAccountKey = str;
            if (WorkflowMaxKeyAcquiredHandlerEvent != null) WorkflowMaxKeyAcquiredHandlerEvent(this, new WorkflowMaxKeyAcquiredEventArgs(WorkflowMaxAccountKey));
        }
      }
      // Catch the IOException that is raised if the pipe is broken
      // or disconnected.
      catch (Exception)
      {
        return false;
      }

      return true;

    }
        
    /// <summary>
    /// Dispose the client process
    /// </summary>
    void DisposeClientProcess()
    {
      try
      {
        m_IsDisposing = true;

        try
        {
          //I will fails if the process doesn't exist
          m_ChildProcess.Kill();
        }
        catch
        {}
                
        m_PipeServerStream.Dispose();//This will stop any pipe activity
        
        Console.WriteLine(string.Format("Process {0} is Closed", m_PipeID));
      }
      catch(Exception ex)
      {
        Console.WriteLine(string.Format("Process {0} is Close error: {1}", this.m_PipeID,ex.Message));
      }
    
    }

    #region IDisposable Members

    public void Dispose()
    {
      DisposeClientProcess();
    }

    #endregion
  }
}
