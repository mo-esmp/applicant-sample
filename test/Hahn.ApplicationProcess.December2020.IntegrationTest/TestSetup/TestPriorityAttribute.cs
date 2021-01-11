using System;

namespace Hahn.ApplicationProcess.December2020.IntegrationTest.TestSetup
{
    [AttributeUsage(AttributeTargets.Method)]
    internal class TestPriorityAttribute : Attribute
    {
        public TestPriorityAttribute(int priority)
        {
            Priority = priority;
        }

        public int Priority { get; }
    }
}