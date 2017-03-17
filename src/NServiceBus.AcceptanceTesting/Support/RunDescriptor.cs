﻿namespace NServiceBus.AcceptanceTesting.Support
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class RunDescriptor
    {
        public RunDescriptor(RunSettings settings, ScenarioContext context)
        {
            Settings = settings;
            ScenarioContext = context;
        }

        public RunSettings Settings { get; }

        public ScenarioContext ScenarioContext { get; }

        public void OnTestCompleted(Func<RunSummary, Task> testCompletedCallback)
        {
            onCompletedCallbacks.Add(testCompletedCallback);
        }

        internal async Task RaiseOnTestCompleted(RunSummary result)
        {
            await Task.WhenAll(onCompletedCallbacks.Select(c => c(result)));
            onCompletedCallbacks = null;
        }

        internal List<Func<RunSummary, Task>> onCompletedCallbacks;
    }
}