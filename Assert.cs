using UnityEngine;
using System;

public static class Assert
{
    const string DEFAULT_MESSAGE = "Assert failed.";

    public static void True(bool check, string message = DEFAULT_MESSAGE)
    {
        if (!check)
        {
            Debug.LogError(message);
        }
    }

    public static void False(bool check, string message = DEFAULT_MESSAGE)
    {
        True(!check, message);
    }

    // Functions
    public static void True(Func<bool> check, string message = DEFAULT_MESSAGE)
    {
        True(check(), message);
    }

    public static void False(Func<bool> check, string message = DEFAULT_MESSAGE)
    {
        False(check(), message);
    }
}
