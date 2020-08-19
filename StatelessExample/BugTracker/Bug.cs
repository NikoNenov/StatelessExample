//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Bug.cs">
//  (c) 2020 Nikolay Nenov, Solothurn, Switzerland, www.nenov.de
//  </copyright>
// 
//  <summary>
//    Bug tracker 
//  </summary>
// 
//  <date>19-08-2020</date>
//  <author>Nikolay Nenov</author>
//  --------------------------------------------------------------------------------------------------------------------

using System;

namespace Nenov.StatelessExample.BugTracker
{
  public class Bug
  {
    private BugStateMachine _bugStateMachine;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="title"></param>
    public Bug(string title)
    {
      _bugStateMachine = new BugStateMachine(title);
      _bugStateMachine.SendMessage += DisplayMessage;
    }

    public void Assign(string assignee) => _bugStateMachine.Assign(assignee);

    public bool CanAssign() => _bugStateMachine.CanAssign;

    public void Defer() => _bugStateMachine.Defer();

    public void Close() => _bugStateMachine.Close();

    public string ToDotGraph() => _bugStateMachine.ToDotGraph();

    /// <summary>
    /// Display state machine message
    /// </summary>
    /// <param name="message"></param>
    private void DisplayMessage(string message)
    {
      Console.WriteLine($"State machine message: {message}");
    }
  }
}
