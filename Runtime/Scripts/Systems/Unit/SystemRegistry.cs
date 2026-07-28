using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	public class ServiceRegistry<TType>
	{
		private readonly Dictionary<System.Type, TType> _systems = new();

		public void Add<TBase, TImpl>(TImpl system)
			where TBase : TType
			where TImpl : TBase
		{
			var type = typeof(TBase);

			if (Contains<TBase>())
			{
				Debug.LogError($"Cannot contain 2 of the same TBase: {type.Name}");
				return;
			}

			_systems[type] = system;
		}

		public bool Remove<TBase>()
			where TBase : TType
		{
			var type = typeof(TBase);
			return _systems.Remove(type);
		}

		public TBase Get<TBase>()
			where TBase : TType
		{
			var type = typeof(TBase);
			return (TBase)_systems[type];
		}
		public bool TryGet<TBase>(out TBase value)
			where TBase : TType
		{
			if (Contains<TBase>())
			{
				value = Get<TBase>();
				return true;
			}

			value = default;
			return false;
		}

		public bool Contains<TBase>()
			where TBase : TType
		{
			var type = typeof(TBase);
			return _systems.ContainsKey(type);
		}
	}
	public class SystemRegistry : ServiceRegistry<ISystem>
	{
	}
}