using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//
// Unity doesn't know how to serialize a Dictionary
// So this is a simple extension of a dictionary that saves as two lists.
[System.Serializable]
public class SerializableDictionary<TKey,TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
	[SerializeField]
	private List<TKey> _keys;
	[SerializeField]
	private List<TValue> _values;
	
	public void OnBeforeSerialize()
	{
		_keys = new List<TKey>(this.Count);
		_values = new List<TValue>(this.Count);
		foreach(var kvp in this)
		{
			_keys.Add(kvp.Key);
			_values.Add(kvp.Value);
		}
	}
	
	public void OnAfterDeserialize()
	{
		this.Clear();
		for (int i=0; i!= Mathf.Min(_keys.Count,_values.Count); i++) {
			this.Add(_keys[i],_values[i]);
		}
	}
}
