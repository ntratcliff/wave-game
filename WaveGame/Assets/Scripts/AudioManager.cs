using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    //node[0] is left and node[node.length-1] is right
    public AudioMixer Mixer;
    public WaveButtonScript Player1, Player2;
    public WaterManager Water;

    public float PanVolumeMax = 5f, PanVolumeMin = -80f, PanAbsoluteMin = -30f;

    public float MiddleVolumeMax = 5f, MiddleVolumeMin = -30, MiddleVolumeTitleMin = -40f, MiddleVolumeRunningMin = -20f;
    public float WaterMiddleMin = -3f, WaterMiddleMax = 3f;

    public int MiddleChannelSampleSize = 0;

    public GameManagerScript GameManager;

    // Update is called once per frame
    void Update()
    {
        // update middle volume based on game state
        if(GameManager.preGame || GameManager.gameEnded)
        {
            MiddleVolumeMin = MiddleVolumeTitleMin;
        }
        else
        {
            MiddleVolumeMin = MiddleVolumeRunningMin;
        }

        // Update mixer volumes for left, right, and middle based on 
        float p1ForceNormalized = normalize(Player1.InputForceP1, Player1.minCap, Player1.maxCap);
        float p2ForceNormalized = normalize(Player2.InputForceP2, Player2.minCap, Player2.maxCap);
        
        int index = Water.NumNodes / 2 - MiddleChannelSampleSize / 2;
        float average = 0.0f;
        for (int i = 0; i < MiddleChannelSampleSize; i++)
        {
            average += Water.Positions[index].y;
            index++;
        }
        average /= MiddleChannelSampleSize;

        // get water middle node height
        Vector2 middlePos = Water.Positions[Water.NumNodes / 2];
        Vector2 leftPos = Water.Positions[0];
        Vector2 rightPos = Water.Positions[Water.NumNodes - 1];
        //float middlePosNormalized = normalize(middlePos.y, WaterMiddleMin, WaterMiddleMax);
        float middlePosNormalized = normalize(average, WaterMiddleMin, WaterMiddleMax);
        float leftPosNormalized = normalize(leftPos.y, WaterMiddleMin, WaterMiddleMax);
        float rightPosNormalized = normalize(rightPos.y, WaterMiddleMin, WaterMiddleMax);

        float p1Volume = PanAbsoluteMin;
        float p2Volume = PanAbsoluteMin;

        float waterVolume = (MiddleVolumeMax - MiddleVolumeMin) * middlePosNormalized + MiddleVolumeMin;
        p1Volume = (PanVolumeMax - PanVolumeMin) * leftPosNormalized + PanVolumeMin;
        p2Volume = (PanVolumeMax - PanVolumeMin) * rightPosNormalized + PanVolumeMin;

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