using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// used as a controller script to determine if the platform will shrink or grow in size in the X axis.
/// </summary>
public class ShrinkingPlatformController : MonoBehaviour
{
    public float platformTimer;
    public float threshold;
    public bool isActive;
    public PlayerBehaviour player;
    
    ///
    Vector3 startingPos;
    bool movingUP;
    [SerializeField]
    AudioSource shrinking;



    // Start is called before the first frame update
    void Start()
    {
        movingUP = true;
        startingPos = transform.position;
        player = FindObjectOfType<PlayerBehaviour>();
        isActive = false;
    }


    // Update is called once per frame
    void Update()
    {
        // used to move the platform up and down in a rhythmic pattern 
        if (movingUP)
        {
            float newY = Mathf.Lerp(transform.position.y, startingPos.y + 0.3f, Time.deltaTime / 2.0f);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            if (newY > startingPos.y + 0.1f)
                movingUP = !movingUP;
        }
        else
        {
            float newY = Mathf.Lerp(transform.position.y, startingPos.y - 0.3f, Time.deltaTime / 2.0f);
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            if (newY < startingPos.y - 0.1f)
                movingUP = !movingUP;
        }
        // if active will shrink and if not active will grow to original size
        Vector3 temp = transform.localScale;
        if (isActive && temp.x > 0)
        {
            _Shrink();
        }
        else if (!isActive && temp.x < 1)
        {
            _GrowToOriginalSize();
        }
    }

    /// <summary>
    /// shrinking function
    /// </summary>
    private void _Shrink()
    {
        if (Time.frameCount % 50 == 0) // will only play the shrinking noise every 100 ms
        {
            shrinking.Play();
        }
        Vector3 temp = transform.localScale;
        temp.x = Mathf.Max(0.0f, Mathf.Lerp(transform.localScale.x, -0.5f, Time.deltaTime / 3.0f));
        if (temp.x == 0) // prevents >>> if stays 0 player cannot trigger on collisionexit to set isactive=false
            StartCoroutine(PauseAndSetActiveFalse());

        
        Debug.Log("Shrinking to Scale: " + temp.x);
        transform.localScale = temp;
    }

    /// <summary>
    /// growing to size function
    /// </summary>
    private void _GrowToOriginalSize()
    {
        Vector3 temp = transform.localScale;
        // MATH.MIN will keep it clamped within 0-1 
        temp.x = Mathf.Min(1, Mathf.Lerp(transform.localScale.x, 1.5f, Time.deltaTime / 3.0f));

        Debug.Log("Growing to Scale: " + temp.x);
        transform.localScale = temp;
    }

    /// <summary>
    /// after a brief delay which allows the player to fall through platform will set active to false to allow platform to grow again.
    /// </summary>
    /// <returns></returns>
    IEnumerator PauseAndSetActiveFalse()
    {
        yield return new WaitForSeconds(0.5f);
        isActive = false;
    }
}
