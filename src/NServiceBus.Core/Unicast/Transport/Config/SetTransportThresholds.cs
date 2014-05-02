namespace NServiceBus.Unicast.Transport.Config
{
    using Faults;
    using NServiceBus.Config;
    using Transports;

    class SetTransportThresholds : INeedInitialization
    {
        public void Init()
        {
            var transportConfig = Configure.GetConfigSection<TransportConfig>();
            var maximumThroughput = 0;
            var maximumNumberOfRetries = 5;
            var maximumConcurrencyLevel = 1;

            if (transportConfig != null)
            {
                maximumNumberOfRetries = transportConfig.MaxRetries;
                maximumThroughput = transportConfig.MaximumMessageThroughputPerSecond;
                maximumConcurrencyLevel = transportConfig.MaximumConcurrencyLevel;
            }

            var transactionSettings = new TransactionSettings
                {
                    MaxRetries = maximumNumberOfRetries
                };

            Configure.Instance.Configurer.ConfigureComponent(b => new TransportReceiver(transactionSettings, maximumConcurrencyLevel, maximumThroughput, b.Build<IDequeueMessages>(), b.Build<IManageMessageFailures>()), DependencyLifecycle.InstancePerCall);
        }

    }
}