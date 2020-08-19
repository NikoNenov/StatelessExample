//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Program.cs">
//  (c) 2020 Nikolay Nenov, Solothurn, Switzerland, www.nenov.de
//  </copyright>
// 
//  <summary>
//    Stateless state machine example 
//  </summary>
// 
//  <date>19-08-2020</date>
//  <author>Nikolay Nenov</author>
//  --------------------------------------------------------------------------------------------------------------------

using System;
using Nenov.StatelessExample.BugTracker;

namespace Nenov.StatelessExample
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // Execute bug tracker example
      ExecuteBugTrackerExample();
      
      // Console.WriteLine($"{Environment.NewLine}State machine dotGraph: {bug.ToDotGraph()}");
      Console.ReadKey(false);
    }

    /// <summary>
    /// Execute bug tracker example
    /// </summary>
    private static void ExecuteBugTrackerExample()
    {
      Console.WriteLine("**** Bug tracker state machine example ****");
      Console.WriteLine();

      var bug = new Bug("First bug");

      bug.Assign("Niko");
      Console.WriteLine();

      bug.Assign("Ivan");
      Console.WriteLine();

      bug.Defer();
      Console.WriteLine();

      bug.Assign("Harry");
      Console.WriteLine();
      
      bug.Assign("Fred");
      Console.WriteLine();

      bug.Assign("Arnold");
      Console.WriteLine();

      bug.Close();
    }
  }
}