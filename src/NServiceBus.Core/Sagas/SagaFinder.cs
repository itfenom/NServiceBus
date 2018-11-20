﻿namespace NServiceBus
{
    using System.Threading.Tasks;
    using Extensibility;
    using ObjectBuilder;
    using Persistence;
    using Sagas;

    abstract class SagaFinder
    {
        public abstract Task<SagaInstance> Find(IBuilder builder, SagaFinderDefinition finderDefinition, SynchronizedStorageSession storageSession, ContextBag context, object message);
    }
}