using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArpScript : MonoBehaviour
{
    public LibPdInstance arpPatch;
    int[] chord;
    int[] scale;
    float trigRamp;
    float chordRamp;
    public bool trig;
    int[] PitchArray;
    int chordIndex;
    int dMs;
    [SerializeField]
    public int beat = 250;
    [SerializeField]
    public int chordSpeed = 6000;
    int selectChord;
    bool ascendChord;
    float filterLFO;
    bool ascendLFO;
    [SerializeField]
    GameObject obj;
    Material outerMat;
    Color lerpedColor;
    float visController; // also updated in makeNewChord()
    // Start is called before the first frame update
    void Start()
    {
        scale = new int[] { 2, 2, 1, 2, 2, 2};
        chord = new int[4];

        makeNewChord();
        chordIndex = 0;
        trigRamp = 0;
        chordRamp = 0;

        ascendChord = true;

        
        PitchArray = ControlFunctions.PitchArray(5, new Vector2Int(48, 72), scale);

        filterLFO = 200f;
        ascendLFO = true;

        lerpedColor = Color.yellow;

        outerMat = obj.GetComponent<Renderer>().material;
        outerMat.SetFloat("_ElectricScrollingSpeed", visController * 0.1f);
        outerMat.SetFloat("_SurfaceMovementSpeed", visController * 0.1f);
        outerMat.SetFloat("_DistortionSpeed", visController * 0.1f);
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

            if (newChord)
            {
                makeNewChord();
                chordIndex = 0;
                //Debug.Log("new chord");


                //Debug.Log("visController: " + visController);
                outerMat.SetFloat("_ElectricScrollingSpeed", visController);
                outerMat.SetFloat("_SurfaceMovementSpeed", visController);
                outerMat.SetFloat("_DistortionSpeed", visController);
                
            }

            if (chordIndex == 0)
            {
                ascendChord = true;
            } 
            if (chordIndex == 3)
            {
                ascendChord = false;
            }

            

            int pitch = PitchArray[chord[chordIndex]];
            arpPatch.SendMidiNoteOn(0, pitch, 80);

            lerpedColor = Color.Lerp(Color.yellow, Color.magenta, (float)(chordIndex / 3f));
            outerMat.SetColor("_BorderColor", lerpedColor);

            if (ascendChord)
            {
                chordIndex++;
            } else
            {
                chordIndex--;
            }

            if (ascendLFO)
            {
                filterLFO += 50f;
            }
            else
            {
                filterLFO -= 50f;
            }

            if (filterLFO >= 3500)
            {
                ascendLFO = false;
            }
            if (filterLFO <= 200)
            {
                ascendLFO = true;
            }

            arpPatch.SendFloat("filterCutoff", filterLFO);


            // TODO : VISUAL

            

            
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
        chord[3] = -1;
        int i = 0;
        while (chord[3] == -1)
        {

            selectChord = Mathf.RoundToInt(Random.value * 2) + 1;
            
            if (i > 0)
            {
                chord[i] = selectChord + chord[i - 1];
            }
            else
            {
                chord[i] = selectChord;
            }
            //Debug.Log(chord[i]);

            i++;
        }

        visController = ((chord[0] + chord[1] + chord[2] + chord[3])/8f - 2.5f) * 2f;
        if (visController == 0)
        {
            visController = 0.1f;
        }

    }
}
