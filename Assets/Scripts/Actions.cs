using UnityEngine;
using System;

public static class Actions
{
    public static event Action NewOnInfection;
    public static event Action OnInfection;
    public static event Action OnGueri;
    public static void InvokeOnInfection() 
    {
        OnInfection.Invoke();
    }

    public static void InvokeNewOnInfection()
    {
        NewOnInfection.Invoke();
    }

    public static void InvokeOnGueri()
    {
        OnGueri.Invoke();
    }
}