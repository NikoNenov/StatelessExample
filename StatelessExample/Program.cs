//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Program.cs">
//  (c) 2020 Nikolay Nenov, Solothurn, Switzerland, www.nenov.de
//  </copyright>
// 
//  <summary>
//    TODO .......
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
      Console.WriteLine("**** Bug tracker state machine example ****");

      Console.WriteLine("Create First bug");
      var bug = new Bug("First bug");
      bug.Assign("Joe");
      Console.WriteLine("Defer");
      bug.Defer();
      bug.Assign("Harry");
      bug.Assign("Fred");
      Console.WriteLine("Close bug");
      bug.Close();

      
      //Console.WriteLine($"{Environment.NewLine}State machine dotGraph: {bug.ToDotGraph()}");

      Console.ReadKey(false);
    }
  }
}