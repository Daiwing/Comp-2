using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PingScript : MonoBehaviour
{
    public LibPdInstance pingPatch;
    public ArpScript masterClock;
    int beat;
    int chordSpeed;
    int[] scale;
    int[] PitchArray;
    int retrig;
    int pitchToggle;
    bool moveRight;
    bool trig;
    int dMs;
    int trigRamp;
    int chordRamp;

    // Start is called before the first frame update
    void Start()
    {
        beat = masterClock.beat;
        chordSpeed = masterClock.chordSpeed;
        trigRamp = 0;
        chordRamp = 0;

        scale = new int[] { 2, 2, 1, 2, 2, 2 };
        PitchArray = ControlFunctions.PitchArray(5, new Vector2Int(72, 84), scale);

        transform.localPosition = new Vector3(-4, 1.5f, 0);
        moveRight = true; // false -> move left
        retrig = 0;
        pitchToggle = 4;
    }

    // Update is called once per frame
    void Update()
    {
        dMs = Mathf.RoundToInt(Time.deltaTime * 1000);
        trig = trigRamp > ((trigRamp + dMs) % beat);
        trigRamp = (trigRamp + dMs) % (beat);
        bool newChord = chordRamp > ((chordRamp + dMs) % chordSpeed);
        chordRamp = (chordRamp + dMs) % chordSpeed;

        if (trig)
        {
            pingPatch.SendFloat("_panRight", (transform.localPosition.x + 4f) / 8f);
            pingPatch.SendFloat("_panLeft", 1f - ((transform.localPosition.x + 4f) / 8f));
            if (newChord)
            {
                retrig = 0;
                if (pitchToggle == 4)
                {
                    pitchToggle = 0;
                }
                else
                {
                    pitchToggle = 4;
                }

            }

            if (retrig == 6)
            {
                int pitch = PitchArray[pitchToggle];
                pingPatch.SendMidiNoteOn(0, pitch, 80);
                retrig = 1;

                //Debug.Log("this happened");
            } else
            {
                retrig++;
            }


        }
        
        if (transform.localPosition.x >= 4)
        {
            moveRight = false;
        }
        if (transform.localPosition.x <= -4)
        {
            moveRight = true;
        }

        if (moveRight)
        {
            transform.localPosition = new Vector3(transform.localPosition.x + 0.015f , 1.5f, 0);
            //Debug.Log("move right");
        } else
        {
            transform.localPosition = new Vector3(transform.localPosition.x - 0.015f, 1.5f, 0);
            //Debug.Log("move left");
        }
        

    }
}
