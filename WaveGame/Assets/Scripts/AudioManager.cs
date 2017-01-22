using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer Mixer;
    public WaveButtonScript Player1, Player2;
    public WaterManager Water;

    public float PanVolumeMax = 5f, PanVolumeMin = -80f, PanAbsoluteMin = -30f;
    public float PlayerInputMin = 0f, PlayerInputMax = 0.8f;

    public float MiddleVolumeMax = 5f, MiddleVolumeMin = -30;
    public float WaterMiddleMin = -3f, WaterMiddleMax = 3f;

    // Update is called once per frame
    void Update()
    {
        // Update mixer volumes for left, right, and middle based on 
        float p1ForceNormalized = normalize(Player1.InputForceP1, Player1.minCap, Player1.maxCap);
        float p2ForceNormalized = normalize(Player2.InputForceP2, Player2.minCap, Player2.maxCap);

        // get water middle node height
        Vector2 middlePos = Water.Positions[Water.NumNodes / 2];
        float waterPosNormalized = normalize(middlePos.y, WaterMiddleMin, WaterMiddleMax);

        float p1Volume = PanAbsoluteMin;
        if (p1ForceNormalized > PlayerInputMin)
        {
            p1Volume = (PanVolumeMax - PanVolumeMin) * p1ForceNormalized + PanVolumeMin;
        }

        float p2Volume = PanAbsoluteMin;
        if(p2ForceNormalized > PlayerInputMin)
        {
            p2Volume = (PanVolumeMax - PanVolumeMin) * p2ForceNormalized + PanVolumeMin;
        }

        float waterVolume = (MiddleVolumeMax - MiddleVolumeMin) * waterPosNormalized + MiddleVolumeMin;

        Mixer.SetFloat("LeftVolume", p1Volume);
        Mixer.SetFloat("RightVolume", p2Volume);
        Mixer.SetFloat("MiddleVolume", waterVolume);
    }

    private float normalize(float val, float min, float max)
    {
        val = Mathf.Clamp(val, min, max);
        return (val - min) / (max - min);
    }
}
