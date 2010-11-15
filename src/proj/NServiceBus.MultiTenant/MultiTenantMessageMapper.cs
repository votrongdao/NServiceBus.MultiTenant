namespace NServiceBus.MultiTenant
{
	using System;
	using MessageMutator;
	using Unicast.Transport;

	public class MultiTenantMessageMapper : IMapOutgoingTransportMessages
	{
		private const string TenantHeader = "TenantId";
		private readonly Func<Guid> getTenantId;

		public MultiTenantMessageMapper(Func<Guid> getTenantId)
		{
			this.getTenantId = getTenantId;
		}

		public void MapOutgoing(IMessage[] messages, TransportMessage transportMessage)
		{
			var tenantId = this.getTenantId();
			if (tenantId != Guid.Empty)
				transportMessage.Headers[TenantHeader] = tenantId.ToString();
		}
	}
}