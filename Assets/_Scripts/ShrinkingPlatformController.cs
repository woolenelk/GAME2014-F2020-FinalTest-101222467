using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [SerializeField]
    AudioSource reseting;



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

        if (isActive)
        {
            _Shrink();
        }
        else
        {
            _GrowToOriginalSize();
        }
    }


    private void _Shrink()
    {
        Vector3 temp = transform.localScale;
        temp.x = Mathf.Max(0.0f, Mathf.Lerp(transform.localScale.x, -0.5f, Time.deltaTime / 3.0f));
        if (temp.x == 0) // if stays 0 player cannot trigger on collisionexit to set isactive=false
            StartCoroutine(PauseAndSetActiveFalse());

        Debug.Log("Shrinking to Scale: " + temp.x);
        transform.localScale = temp;
    }

    IEnumerator PauseAndSetActiveFalse()
    {
        yield return new WaitForSeconds(0.5f);
        isActive = false;
    }

    private void _GrowToOriginalSize()
    {

        Vector3 temp = transform.localScale;
        temp.x = Mathf.Min(1, Mathf.Lerp(transform.localScale.x, 1.5f, Time.deltaTime / 3.0f));

        Debug.Log("Growing to Scale: " + temp.x);
        transform.localScale = temp;
    }
}
