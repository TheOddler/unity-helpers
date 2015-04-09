using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class UIHelpers {
		
	/// <summary>
	/// Gets all the assets of a certain type. Even when they aren't loaded into memory yet.
	/// </summary>
	public static IEnumerable<T> GetAllAssetsOfType<T>() where T: ScriptableObject {
		string typeName = typeof(T).Name;
		// find all assets and convert to usable list
		// we use AssetDatabase.FindAssets because that finds even those not loaded in memory
		IEnumerable<T> assets = AssetDatabase.FindAssets("t:" + typeName)
			.Select(guid => AssetDatabase.GUIDToAssetPath(guid))
			.Select(path => AssetDatabase.LoadAssetAtPath(path, typeof(T)))
			.Select(obj  => (T)obj);
		
		return assets;
	}
	
	/// <summary>
	/// Draws a field for each instance of the asset in the project.
	/// Adds buttons to add missing or remove existing values.
	/// With each asset instance a single double is assosiated.
	/// </summary>
	public static void AssetDoubleFieldAll<TAsset>(
		Func<TAsset, String> prefix,
		Func<TAsset, bool> has,
		Func<TAsset, double> get,
		Action<TAsset, double> set,
		Action<TAsset> remove,
		double newVal
	) where TAsset: ScriptableObject {
		AssetPairField(
			GetAllAssetsOfType<TAsset>(),
			prefix, 
			has,
			get,
			set,
			remove,
			newVal
		);
	}
	
	/// <summary>
	/// Draws a field for each of the items in the assets list.
	/// Adds buttons to add missing or remove existing values.
	/// With each asset instance a single value is assosiated.
	/// </summary>
	public static void AssetPairField<TAsset, TValue>(
		IEnumerable<TAsset> assets, // the list of assets, each one will get an entry
		Func<TAsset, String> prefix, // a method to get a string prefix from an asset
		Func<TAsset, bool> has, // a method to check if this asset already has a value associated with it
		Func<TAsset, TValue> get, // a method to get the associated value of the given asset
		Action<TAsset, TValue> set, // a method to set the associated value of the given asset
		Action<TAsset> remove, // a method to move any associating of a value with this asset
		TValue defaultValue // the default value to be associated with an asset when creating the association
	) where TAsset: ScriptableObject {
		foreach(TAsset asset in assets) {
			EditorGUILayout.BeginHorizontal();
			
			EditorGUILayout.PrefixLabel( prefix(asset) );
			
			if (has(asset)) {
				TValue curVal = get(asset);
				TValue newVal = GenericField(curVal);
				if (!EqualityComparer<TValue>.Default.Equals(curVal, newVal)) {
					set(asset, newVal);
				}
				
				if (GUILayout.Button("-")) {
					remove(asset);
				}
			}
			else {
				if (GUILayout.Button("No modifier, add +")) {
					set(asset, defaultValue);
				}
			}
			GUILayout.EndHorizontal();
		}
	}
	
	/// <summary>
	/// Draws a field for each instance of the asset in the project.
	/// Adds buttons to add missing or remove existing values.
	/// With each asset instance a single value is assosiated.
	/// </summary>
	public static void AssetValueDictionaryFieldAll<TAsset, TValue>(
		Dictionary<TAsset, TValue> dictionary, // the dictionary with all known associations between an asset and value
		Func<TAsset, String> prefix, // a method to get the prefix to be used with the asset
		TValue defaultValue
	) where TAsset: ScriptableObject {
		IEnumerable<TAsset> assets = GetAllAssetsOfType<TAsset>();
		
		foreach(TAsset asset in assets) {
			EditorGUILayout.BeginHorizontal();
			
			EditorGUILayout.PrefixLabel( prefix(asset) );
			
			if (dictionary.ContainsKey(asset)) {
				TValue curVal = dictionary[asset];
				TValue newVal = GenericField(curVal);
				if (!EqualityComparer<TValue>.Default.Equals(curVal, newVal)) {
					dictionary[asset] = newVal;
				}
				
				if (GUILayout.Button("-")) {
					dictionary.Remove(asset);
				}
			}
			else {
				if (GUILayout.Button("No modifier, add +")) {
					dictionary[asset] = defaultValue;
				}
			}
			GUILayout.EndHorizontal();
		}
	}
	
	/// A wrapper around a bunch of EditorGUILayout.XXXField methods.
	/// It automatically selects the correct type.
	/// Doesn't work for all types, but for most.
	public static T GenericField<T>(T value, params GUILayoutOption[] options) {
		Type type = typeof(T);
		if (type == typeof(Bounds)) {
			return (T)(object)EditorGUILayout.BoundsField((Bounds)(object)value, options);
		}
		else if (type == typeof(Color)) {
			return (T)(object)EditorGUILayout.ColorField((Color)(object)value, options);
		}
		else if (type == typeof(AnimationCurve)) {
			return (T)(object)EditorGUILayout.CurveField((AnimationCurve)(object)value, options);
		}
		else if (type == typeof(double)) {
			return (T)(object)EditorGUILayout.DoubleField((double)(object)value, options);
		}
		else if (type == typeof(float)) {
			return (T)(object)EditorGUILayout.FloatField((float)(object)value, options);
		}
		else if (type == typeof(int)) {
			return (T)(object)EditorGUILayout.IntField((int)(object)value, options);
		}
		else if (type == typeof(long)) {
			return (T)(object)EditorGUILayout.LongField((long)(object)value, options);
		}
		else if (type == typeof(SerializedProperty)) {
			return (T)(object)EditorGUILayout.PropertyField((SerializedProperty)(object)value, options);
		}
		else if (type == typeof(Rect)) {
			return (T)(object)EditorGUILayout.RectField((Rect)(object)value, options);
		}
		else {
			return default(T);
		}
	}
	
}
