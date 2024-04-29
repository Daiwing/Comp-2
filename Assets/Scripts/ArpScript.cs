using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArpScript : MonoBehaviour
{
    public LibPdInstance patch;
    int[] chord;
    float trigRamp;
    float chordRamp;
    int[] PitchArray;
    int chordIndex;
    int dMs;
    [SerializeField]
    public int beat = 150;
    [SerializeField]
    public int chordSpeed = 3000;
    // Start is called before the first frame update
    void Start()
    {
        makeNewChord();
        chordIndex = 0;
        trigRamp = 0;
        chordRamp = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
        
        

        dMs = Mathf.RoundToInt(Time.deltaTime * 1000);
        bool trig = trigRamp > ((trigRamp + dMs) % beat);
        trigRamp = (trigRamp + dMs) % (beat);
        bool newChord = chordRamp > ((chordRamp + dMs) % chordSpeed);
        chordRamp = (chordRamp + dMs) % chordSpeed;
        

        if (trig)
        {
            if (chordIndex < chord.Length)
            {
                int pitch = PitchArray[chordIndex];
                patch.SendMidiNoteOn(0, pitch, 80);
                //Debug.Log("pitch: " + pitch);
                chordIndex++;
            }

            if (newChord)
            {
                makeNewChord();
                chordIndex = 0;
                //Debug.Log("new chord");
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


    void makeNewChord()
    {

        int selectChord = Mathf.RoundToInt(Random.value * 6); // * num of chords - 1
        
        switch(selectChord) // hardcoded chords
        {
            case 0:
                chord = new int[] {4, 3, 5}; // major triad
                break;
            case 1:
                chord = new int[] {3, 4, 5 }; // minor triad
                break;
            case 2:
                chord = new int[] {4, 3, 4, 1 }; // major 7
                break;
            case 3:
                chord = new int[] {3, 4, 3, 2 }; // minor 7
                break;
            case 4:
                chord = new int[] {4, 3, 3, 2 }; // dom 7
                break;
            case 5:
                chord = new int[] {4, 3, 2, 3 }; // M add 6
                break;
            case 6:
                chord = new int[] {4, 3, 1, 4 }; // M add b6
                break;
            default:
                chord = new int[] { 4, 3, 5 }; // major triad
                break;
        }

        //Debug.Log("Select: " + selectChord);


        int tonic;
        int selectTonic = Mathf.RoundToInt(Random.value * 7);

        switch(selectTonic) // normalize random to C minor/harmonic minor scale
        {
            case 0:
                tonic = 0;
                break;
            case 1:
                tonic = 2;
                break;
            case 2:
                tonic = 3;
                break;
            case 3:
                tonic = 5;
                break;
            case 4:
                tonic = 7;
                break;
            case 5:
                tonic = 8;
                break;
            case 6:
                tonic = 10;
                break;
            case 7:
                tonic = 11;
                break;
            default:
                tonic = 0;
                break;
        }


        PitchArray = ControlFunctions.PitchArray(tonic, new Vector2Int(48, 72), chord);
    }
}
