//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="BugState.cs">
//  (c) 2020 Nikolay Nenov, Solothurn, Switzerland, www.nenov.de
//  </copyright>
// 
//  <summary>
//    Bug states
//  </summary>
// 
//  <date>19-08-2020</date>
//  <author>Nikolay Nenov</author>
//  --------------------------------------------------------------------------------------------------------------------

namespace Nenov.StatelessExample.BugTracker
{
  public enum BugState
  {
    Open, 
    Assigned, 
    Deferred, 
    Closed
  }
}
