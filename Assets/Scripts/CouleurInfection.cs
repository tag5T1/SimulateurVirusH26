using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CouleurInfection
{
    /// <summary>
    /// Retourne la couleur que prendara la case selon le taux d'infection.
    /// </summary>
    /// <param name="tauxInfection"> Rapport d'infecté sur le total (0-1) </param>
    public Color CalculerCouleurCase(float tauxInfection, float r)
    {
        float resistance = 1.0f - r / 2;
        Color color;
        // 4 steps
        if (tauxInfection < 0.25f)
        {
            color = new Color(0, tauxInfection/0.25f, 1.0f, resistance);
        }
        else if (tauxInfection < 0.50f) {
            color = new Color(0, 1.0f, 1.0f - (tauxInfection - 0.25f)/0.25f, resistance);
        }
        else if (tauxInfection < 0.75f)
        {
            color = new Color((tauxInfection - 0.5f) / 0.25f, 1.0f, 0, resistance);
        }
        else
        {
            color = new Color(1.0f, 1.0f - (tauxInfection - 0.75f) / 0.25f, 0, resistance);
        }
        return color;
    }
}
