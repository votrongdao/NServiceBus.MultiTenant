namespace NServiceBus.MultiTenant
{
	using System;
	using System.Collections.Generic;
	using System.Reflection;
	using MessageMutator;

	public class AppendAccountMessageMutator : IMutateOutgoingMessages
	{
		private static readonly IDictionary<IReflect, PropertyInfo> Cache =
			new Dictionary<IReflect, PropertyInfo>();

		private const string MessageProperty = "AccountId";
		private readonly Func<Guid> getAccountId;

		public AppendAccountMessageMutator(Func<Guid> getAccountId)
		{
			this.getAccountId = getAccountId;
		}

		public IMessage MutateOutgoing(IMessage message)
		{
			this.AppendAccountId(message);
			return message;
		}
		private void AppendAccountId(object message)
		{
			if (message == null)
				return;

			var accountId = this.getAccountId();
			if (accountId == Guid.Empty)
				return;

			var property = GetProperty(message.GetType());
			if (property == null)
				return;

			var value = property.GetValue(message, null);
			if (Guid.Empty.Equals(value))
				property.SetValue(message, accountId, null);
		}
		private static PropertyInfo GetProperty(IReflect type)
		{
			PropertyInfo propertyInfo;
			if (!Cache.TryGetValue(type, out propertyInfo))
				lock (Cache)
					if (!Cache.TryGetValue(type, out propertyInfo))
						Cache[type] = propertyInfo = type.GetProperty(MessageProperty, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

			return propertyInfo;
		}
	}
}