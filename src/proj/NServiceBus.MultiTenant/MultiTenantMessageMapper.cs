namespace NServiceBus.MultiTenant
{
	using System;
	using MessageMutator;
	using Unicast.Transport;

	public class MultiTenantMessageMapper : IMapOutgoingTransportMessages
	{
		private const string TenantHeader = "TenantId";
		private readonly Func<Guid> getAccountId;

		public MultiTenantMessageMapper(Func<Guid> getAccountId)
		{
			this.getAccountId = getAccountId;
		}

		public void MapOutgoing(IMessage[] messages, TransportMessage transportMessage)
		{
			var accountId = this.getAccountId();
			if (accountId != Guid.Empty)
				transportMessage.Headers[TenantHeader] = accountId.ToString();
		}
	}
}