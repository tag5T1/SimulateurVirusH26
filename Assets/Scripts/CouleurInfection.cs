using UnityEngine;

public class CouleurInfection
{
    /// <summary>
    /// Retourne la couleur que prendara la case selon le taux d'infection.
    /// </summary>
    /// <param name="tauxInfection"> Rapport d'infecté sur le total (0-1) </param>
    public Color CalculerCouleurCase(float tauxInfection)
    {
        Color color;
        // 4 steps
        if (tauxInfection < 0.25f)
        {
            color = new Color(0, tauxInfection/0.25f, 255);
        }
        else if (tauxInfection < 0.50f) {
            color = new Color(0, 1.0f, 1.0f - (tauxInfection - 0.25f)/0.25f);
        }
        else if (tauxInfection < 0.75f)
        {
            color = new Color((tauxInfection - 0.5f) / 0.25f, 1.0f, 0);
        }
        else
        {
            color = new Color(1.0f, 1.0f - (tauxInfection - 0.75f) / 0.25f, 0);
        }
        return color;
    }
}
