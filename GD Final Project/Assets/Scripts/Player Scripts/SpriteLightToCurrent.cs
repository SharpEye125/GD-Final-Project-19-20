using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SpriteLightToCurrent : MonoBehaviour
{
    Light2D myLight;
    // Start is called before the first frame update
    void Start()
    {
        myLight = GetComponent<Light2D>(); 
    }

    // Update is called once per frame
    void Update()
    {
        myLight.lightCookieSprite = GetComponentInParent<SpriteRenderer>().sprite;
    }
}
