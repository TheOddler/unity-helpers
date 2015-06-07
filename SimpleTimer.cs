using UnityEngine;

public static class SimpleTimer {
	
	private static System.Diagnostics.Stopwatch _stopwatch;
	private static string _name;
	
	public static void Start(string name = "Timer") {
		_name = name;
		_stopwatch = System.Diagnostics.Stopwatch.StartNew();
	}
	
	public static void Report() {
		Debug.Log(_name + " (ms): " + _stopwatch.ElapsedMilliseconds);
	}
	
	public static void Stop() {
		_stopwatch.Stop();
	}
	
}
