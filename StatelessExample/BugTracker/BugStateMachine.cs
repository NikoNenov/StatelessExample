//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BugStateMachine.cs">
//  (c) 2020 Nikolay Nenov, Solothurn, Switzerland, www.nenov.de
//  </copyright>
// 
//  <summary>
//    Bug tracker state machine
//  </summary>
// 
//  <date>19-08-2020</date>
//  <author>Nikolay Nenov</author>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using Stateless;
using Stateless.Graph;

namespace Nenov.StatelessExample.BugTracker
{
  public class BugStateMachine
  {
    private readonly string _title;
    private string _assignee;

    private readonly StateMachine<BugState, BugTrigger> _machine;

    /// <summary>
    /// The TriggerWithParameters object is used when a trigger requires a payload.
    /// </summary>
    private readonly StateMachine<BugState, BugTrigger>.TriggerWithParameters<string> _assignTrigger;

    /// <summary>
    /// Send state machine OnEntry message
    /// </summary>
    public Action<string> SendMessage;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="title"></param>
    public BugStateMachine(string title)
    {
      _title = title;

      // Instantiate a new state machine in the Open state
      _machine = new StateMachine<BugState, BugTrigger>(BugState.Open);

      // Instantiate a new trigger with a parameter. 
      _assignTrigger = _machine.SetTriggerParameters<string>(BugTrigger.Assign);

      ConfigureStateMachine();
    }

    /// <summary>
    /// Configure state machine
    /// </summary>
    public void ConfigureStateMachine()
    {
      // Configure the Open state
      _machine.Configure(BugState.Open)
        .Permit(BugTrigger.Assign, BugState.Assigned);

      // Configure the Assigned state
      _machine.Configure(BugState.Assigned)
        .SubstateOf(BugState.Open)
        .OnEntryFrom(_assignTrigger, OnAssigned)  // This is where the TriggerWithParameters is used. Note that the TriggerWithParameters object is used, not something from the enum
        .PermitReentry(BugTrigger.Assign)
        .Permit(BugTrigger.Close, BugState.Closed)
        .Permit(BugTrigger.Defer, BugState.Deferred)
        .OnExit(OnDeassigned);

      // Configure the Deferred state
      _machine.Configure(BugState.Deferred)
        .OnEntry(OnDeferred)
        .Permit(BugTrigger.Assign, BugState.Assigned);
    }

    /// <summary>
    /// This method is called automatically when the Assigned state is entered,
    /// but only when the trigger is _assignTrigger.
    /// </summary>
    /// <param name="assignee"></param>
    private void OnAssigned(string assignee)
    {
      if (_assignee != null && assignee != _assignee)
      {
        SendMessage?.Invoke("Don't forget to help the new employee!");
      }

      _assignee = assignee;
      SendMessage?.Invoke($"The bug is assigned to {assignee}.");
    }
    /// <summary>
    /// This method is called when the state machine exits the Assigned state
    /// </summary>
    private void OnDeassigned()
    {
      SendMessage?.Invoke("You're off the hook.");
    }

    /// <summary>
    /// Deferred method
    /// </summary>
    private void OnDeferred()
    {
      _assignee = null;
    }

    /// <summary>
    /// Fire Assign trigger
    /// </summary>
    /// <param name="assignee"></param>
    public void Assign(string assignee)
    {
      // This is how a trigger with parameter is used,
      // the parameter is supplied to the state machine as a parameter to the Fire method.
      _machine.Fire(_assignTrigger, assignee);
    }

    /// <summary>
    /// Check if can assign
    /// </summary>
    public bool CanAssign => _machine.CanFire(BugTrigger.Assign);

    /// <summary>
    /// Fire Defer trigger
    /// </summary>
    public void Defer()
    {
      _machine.Fire(BugTrigger.Defer);
    }

    /// <summary>
    /// Fire Close trigger
    /// </summary>
    public void Close()
    {
      _machine.Fire(BugTrigger.Close);
    }

    /// <summary>
    /// State machine to dotGraph
    /// </summary>
    /// <returns></returns>
    public string ToDotGraph()
    {
      return UmlDotGraph.Format(_machine.GetInfo());
    }
  }
}
