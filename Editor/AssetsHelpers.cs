using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public static class AssetsHelpers {
	
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
	
}
