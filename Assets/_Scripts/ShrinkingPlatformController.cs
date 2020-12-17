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

    private void _GrowToOriginalSize()
    {
        
    }

    private void _Shrink()
    {
        
    }
}
