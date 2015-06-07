using UnityEngine;
using System;
using Conditional = System.Diagnostics.ConditionalAttribute;

public static class Assert
{
    const string DEFAULT_MESSAGE = "Assert failed.";

    // Default
    [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
    public static void True(bool check, string message = DEFAULT_MESSAGE)
    {
        if (!check)
        {
            Debug.LogError(message);
        }
    }

    [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
    public static void False(bool check, string message = DEFAULT_MESSAGE)
    {
        True(!check, message);
    }

    [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
    public static void Equals(object obj1, object obj2, string message = DEFAULT_MESSAGE)
    {
        True(obj1 == obj2, message);
    }

    // Functions
    [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
    public static void True(Func<bool> check, string message = DEFAULT_MESSAGE)
    {
        True(check(), message);
    }

    [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
    public static void False(Func<bool> check, string message = DEFAULT_MESSAGE)
    {
        False(check(), message);
    }

    // Objects
    [Conditional("UNITY_EDITOR"), Conditional("DEVELOPMENT_BUILD")]
    public static void NotNull(object obj, string message = DEFAULT_MESSAGE)
    {
        True(obj != null, message);
    }
}
