# Final Compo


### Idea: 
This is a direct continuation of compo 2 (thus, in the same GitHub repo-- I hope that is ok), however it is completely revamped. In contrast with my compo 1, which was very concrete, I wanted to do something
more musically and visually abstract. So, this piece has no formal structure, and is randomly generated such that it will continue on uniquely forever. 

I really like the sound of cluster chords in diatonic scales; they are a perfect mix of dissonance and consonance. Thus, I wanted to generate random cluster chords in the major scale to create harmonically interesting
pads. I used a 4-note (in a triplet rhythm) arpeggio, with 4 different randomly generated scale degrees (in sequence) to create these chords, and reverb to fill out the sound. Then, an independently-generated bass tone
fills out the low end and in effect creates a 5 voice chord. Visually, I represented this very abstractly, with a central plasma orb that reacts to the arpeggio (on each note, where the color of the outer ring changes,
and on the chord changes where the animation speed changes). I also created a colorful backdrop with the lighting changing with the bass, and a moving orb that reflects the stereo panning of the light ostinato 
sine synth.

Moving from compo 2 to this final iteration, I threw out all of the visuals that I had because they were all placeholder. Musically, I knew I wanted to elaborate further, but I started by changing the entire process by which I was generating chords (because I had done it incorrectly before, and was generating a new scale for each chord rather than sticking with one scale and playing notes within that scale). Once that was fixed,
it was much easier to achieve the randomly-generated chords that I wanted.

### Elements: 
- randomly generated saw 4-note arpeggio that continuously creates interesting chords/tone clusters diatonic to the major scale (with a filter LFO desynced from the chord changes)
- randomly generated square bass that, with the arpeggio, creates a 5 note harmony (this bass also has a filter LFO synced to the chord changes)
- 'ping' synth that serves as a high pedal tone

- central visual blob, border color and vfx speed correlated with chords
- pink and blue lights, the balance of which is correlated with the bass pitch
- white floating orb, of which the x position controls the panning of the ping synth

### Techniques:
- advanced URP shader graph usage (of which I learned a lot from YouTube tutorials) - textures, time, fresnel effect, etc.
- vfx particle engine (for the head & tail particles surrounding the orbs)
- sequencer-like structure
- passing variables between scripts
- reverb and lowpass filter in pure data
- randomly generating indices of scale degrees in a major scale to generate cluster chords
