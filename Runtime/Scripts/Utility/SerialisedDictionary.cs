using System;
using System.Collections.Generic;
using UnityEngine;

namespace LordSheo.JJTK
{
	[System.Serializable]
	public class SerialisedDictionary<TKey, TValue> : Dictionary<TKey, TValue>,
		ISerializationCallbackReceiver
	{
		[SerializeField]
		private TKey[] _keys;
		[SerializeField]
		public TValue[] _values;
		
		public void OnBeforeSerialize()
		{
			var tempKeys = _keys;
			var tempValues = _values;

			try
			{
				_keys = new TKey[Count];
				_values = new TValue[Count];

				var index = 0;
				foreach (var kvp in this)
				{
					_keys[index] = kvp.Key;
					_values[index] = kvp.Value;
				
					index++;
				}
			}
			catch (Exception e)
			{
				Debug.LogException(e);

				_keys = tempKeys;
				_values = tempValues;
			}
		}
		public void OnAfterDeserialize()
		{
			this.Clear();

			if (_keys.Length != _values.Length)
			{
				Debug.LogError("Key/Value count mismatch.");
				return;
			}
			
			for (var i = 0; i < _keys.Length; i++)
			{
				this[_keys[i]] = _values[i];
			}
		}
	}
}