using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*

to make organic movement: use a shader with normal map

*/


public class BassScript : MonoBehaviour
{
    public LibPdInstance bassPatch;
    int[] scale;
    float trigRamp;
    float chordRamp;
    int[] PitchArray;
    int dMs;
    public ArpScript masterClock;
    int beat;
    int chordSpeed;
    int selectNote;
    float filterCutoff;
    [SerializeField]
    public float filterThresh = 7500f;
    // Start is called before the first frame update

    [SerializeField]
    GameObject cyanLight;
    [SerializeField]
    GameObject pinkLight;

    Light cyan;
    Light pink;
    void Start()
    {
        beat = masterClock.beat;
        chordSpeed = masterClock.chordSpeed;

        cyan = cyanLight.GetComponent<Light>();
        pink = pinkLight.GetComponent<Light>();

        scale = new int[] { 2, 2, 1, 2, 2, 2};

        selectNote = Mathf.RoundToInt(Random.value * 6);
        trigRamp = 0;
        chordRamp = 0;

        filterCutoff = filterThresh;
        
        PitchArray = ControlFunctions.PitchArray(5, new Vector2Int(24, 36), scale);

        updateLight();
    }

    // Update is called once per frame
    void Update()
    {
        
        // TODO : SYNC TO LIGHTS (COLOR AND POSITION) -- MAYBE INSTEAD OF RUNNING ON ONE LIGHT, RUN ON EMPTY OBJECT THAT CREATES MULTIPLE LIGHTS
        

        dMs = Mathf.RoundToInt(Time.deltaTime * 1000);
        bool trig = trigRamp > ((trigRamp + dMs) % beat);
        trigRamp = (trigRamp + dMs) % (beat);
        bool newChord = chordRamp > ((chordRamp + dMs) % chordSpeed);
        chordRamp = (chordRamp + dMs) % chordSpeed;

        if (trig)
        {

            if (newChord)
            {
                selectNote = Mathf.RoundToInt(Random.value * 6);
                filterCutoff = filterThresh;
                updateLight();
            }


            int pitch = PitchArray[selectNote];
            bassPatch.SendMidiNoteOn(0, pitch, 80);
            //Debug.Log("pitch: " + pitch);
            bassPatch.SendFloat("bassFilterCutoff", filterCutoff);

            if (filterCutoff > 800)
            {
                filterCutoff -= 500f;
            }
            
            
        }


        // debug block
        /*
        Debug.Log("trig: " + trig);
        Debug.Log("newChord: " + newChord);
        Debug.Log("ramp: " + ramp);
        //Debug.Log("");
        */

    }

    void updateLight()
    {

        cyan.intensity = selectNote / 7f;
        pink.intensity = (7f - selectNote) / 7f;

    }

}
