using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Settings : ScriptableObject
{


    public enum Locus
    {
        Planckian,
        Alto,
        DramaticPlanckian
    }

    private static Settings instance;
    public static Settings I
    {
        get
        {
            if (!instance)
            {
                instance = Resources.Load("Settings") as Settings;
            }
            return instance;
        }
    }

    [SerializeField]
    private float starReferenceMaxErrorDegrees = 0.01f;

    public float StarReferenceMaxErrorDegrees
    {
        get { return starReferenceMaxErrorDegrees; }
    }

    [SerializeField]
    private float maxTemp = 30000f;

    [SerializeField]
    public Locus locus = Locus.Planckian;

    [SerializeField]
    private Gradient planckianLocus = null;
    [SerializeField]
    private Gradient altoLocus = null;
    [SerializeField]
    private Gradient dramaticPlanckianLocus = null;
    public Color ReadLocus(float temperature)
    {
        float temperatureNormalized = temperature / maxTemp;
        if (temperatureNormalized > 1f || temperatureNormalized <= 0f)
        {
            Debug.LogError($"A star has a temperature of {temperature}, which is outside the range (1K, 30000 K]. The error color is applied.");
            return Color.magenta;
        }
        Gradient heatingGradient;
        switch (locus)
        {
            case Locus.Planckian:
                heatingGradient = planckianLocus;
                break;
            case Locus.Alto:
                heatingGradient = altoLocus;
                break;
            case Locus.DramaticPlanckian:
                heatingGradient = dramaticPlanckianLocus;
                break;
            default:
                return Color.black;
        }
        return heatingGradient.Evaluate(temperatureNormalized);
    }
    public Color ReadLocus(float temperature, Locus selectedLocus)
    {
        float temperatureNormalized = temperature / 30000f;
        if (temperatureNormalized > 1f || temperatureNormalized <= 0f)
        {
            Debug.LogError($"A star has a temperature of {temperature}, which is outside the range (1K, 30000 K]. The error color is applied.");
            return Color.magenta;
        }
        Gradient heatingGradient;
        switch (selectedLocus)
        {
            case Locus.Planckian:
                heatingGradient = planckianLocus;
                break;
            case Locus.Alto:
                heatingGradient = altoLocus;
                break;
            case Locus.DramaticPlanckian:
                heatingGradient = dramaticPlanckianLocus;
                break;
            default:
                return Color.black;
        }
        return heatingGradient.Evaluate(temperatureNormalized);
    }

    [SerializeField, Range(0f, 0.05f)]
    private float starSize = 0f;
    public float StarSize
    {
        get { return starSize; }
    }
}
