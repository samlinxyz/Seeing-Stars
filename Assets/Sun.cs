using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public Transform cam;
    public StarFieldManager field;

    [SerializeField]
    private Vector3 truePosition = Vector3.zero;
    public Vector3 TruePosition
    {
        get
        {
            return truePosition;
        }
        set
        {
            if (truePosition == Vector3.zero)
            {
                transform.position = value;
                truePosition = value;
            }
            else
            {
                Debug.LogError("TruePosition must only be set once, immediately after instantiating the star.");
            }
        }
    }

    public float absoluteMagnitude;

    [SerializeField]
    private SpriteRenderer sprite;

    public Color TrueColor
    {
        get
        {
            return sprite.color;
        }
    }

    [SerializeField]
    private float temperature = 0f;
    public float Temperature
    {
        get
        {
            return temperature;
        }
        //  This should be used only once, at initialization
        set
        {
            if (temperature == 0f)
            {
                Color starColor = Settings.I.ReadLocus(value);
                sprite.color = starColor;
                temperature = value;
            }
            else
            {
                Debug.LogError("Temperature should only be set once, immediately after instantiating the star.");
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.transform;
        field = StarFieldManager.instance;

        sprite = GetComponent<SpriteRenderer>();

        ConfigureTransform();
    }

    // Update is called once per frame
    void Update()
    {
        if (sprite.isVisible)
        {
            UpdateTransform();
        }
    }

    public void ConfigureTransform()
    {
        transform.localPosition = truePosition;
    }

    public float VmagAt(float distance)
    {
        return absoluteMagnitude - 7.5f + 5 * Mathf.Log10(distance);
    }

    public void UpdateTransform()
    {
        float distance = Vector3.Distance(cam.position, transform.position);    //  Distance is not optimal. Use sqrMagnitude.
        float visibility = 1f - VmagAt(distance) / 6.5f;
        visibility = Mathf.Clamp(visibility, 0f, 10f); // 5 is way more than the max

        //  Transformations
        float size = 0.75f * visibility + 0.25f;
        transform.localScale = Settings.I.StarSize * distance * Vector3.one * size;

        transform.rotation = cam.rotation;
    }


}
