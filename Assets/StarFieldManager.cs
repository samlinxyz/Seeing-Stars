using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.CompilerServices;

[ExecuteInEditMode]
public class StarFieldManager : MonoBehaviour
{
    public Camera mainCamera;

    public static StarFieldManager instance;

    public GameObject starPrefab;

    [SerializeField, Range(0f, 1f)]
    private float dimAlpha = 0.5f;
    public float DimAlpha { get { return dimAlpha; } }

    private Star[] stars = null;
    public Star[] Stars
    {
        get
        {
            if (stars == null || stars.Length == 0)
            {
                stars = GetComponentsInChildren<Star>();
            }
            return stars;
        }
    }

    void Awake()
    {
        instance = this;
    }

    //  Creates the original positions of the stars
    public void ConfigureStarsTransform()
    {
        foreach (Star star in GetComponentsInChildren<Star>())
        {
            star.ConfigureTransform();
            star.UpdateTransform();
        }
    }

    private void LoadStars(StarFieldManager field)
    {
        //  Loads data about position, color, and absolute magnitude of all stars from a csv.
        string starInfoArray = File.ReadAllText(Application.dataPath + "/Resources/Stars - GameData.csv");
        starInfoArray.Replace("\r", null);

        foreach (string starInfo in starInfoArray.Split('\n'))
        {
            string[] dataEntry = starInfo.Split(',');

            //  Instantiate star
            GameObject starObject = Instantiate(field.starPrefab, field.transform);
            starObject.tag = "Star";
            Star star = starObject.GetComponent<Star>();

            //  Set references
            star.cam = field.mainCamera.transform;
            star.field = field;

            //  Set position and parameters
            Vector3 positionData = new Vector3(float.Parse(dataEntry[0]), float.Parse(dataEntry[1]), float.Parse(dataEntry[2]));
            star.TruePosition = positionData;
            star.Temperature = float.Parse(dataEntry[3]);
            star.absoluteMagnitude = float.Parse(dataEntry[4]);

            //  Properly size and rotate star sprite.
            star.ConfigureTransform();
            star.UpdateTransform();

            //  Update Transform sets rotation equal to camera rotation, so rotation must be performed here.
            star.transform.rotation = Quaternion.LookRotation(positionData);
        }
    }

    private void DeleteAllStars()
    {
        GameObject[] allStars = GameObject.FindGameObjectsWithTag("Star");
        int starCount = allStars.Length;
        for (int i = 0; i < starCount; i++)
        {
            Destroy(allStars[starCount - 1 - i]);
        }
    }

    [SerializeField]
    public Settings.Locus locus = Settings.Locus.Planckian;

    public void RecolorStars()
    {

    }
}
