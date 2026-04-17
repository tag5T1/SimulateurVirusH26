using UnityEngine;
using System;

public static class Actions
{
    public static event Action NewOnInfection;
    public static event Action OnInfection;
    public static void InvokeOnInfection() 
    {
        OnInfection.Invoke();
    }

    public static void InvokeNewOnInfection()
    {
        NewOnInfection.Invoke();
    }
}