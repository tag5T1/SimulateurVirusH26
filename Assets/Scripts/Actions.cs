using UnityEngine;
using System;

public static class Actions
{
    public static event Action NewOnInfection;
    public static event Action OnInfection;
    public static event Action OnGueri;

    //Appeler lors de l'infection
    public static void InvokeOnInfection() 
    {
        OnInfection?.Invoke();
    }

    //Appeler seulement lorsqu'une personne non-infect� devient infect�
    public static void InvokeNewOnInfection()
    {
        NewOnInfection?.Invoke();
    }

    //Appeler lorsqu'une personne gueri
    public static void InvokeOnGueri()
    {
        OnGueri.Invoke();
    }
}