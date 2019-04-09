using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePresetValues : MonoBehaviour
{
    public NoisePresetConfig npc;
    
    public void setOctaves()
    {
        npc.octaves = (int)gameObject.GetComponent<Slider>().value;
    }
    
    public void setMultiplier()
    {
        npc.multiplier = (int)gameObject.GetComponent<Slider>().value;
    }
    
    public void setAmplitude()
    {
        npc.amplitude = gameObject.GetComponent<Slider>().value;
    }
    
    public void setLacunarity()
    {
        npc.lacunarity = gameObject.GetComponent<Slider>().value;
    }
    
    public void setPersistence()
    {
        npc.persistence = gameObject.GetComponent<Slider>().value;
    }
    
    public void setTau()
    {
        npc.tau = gameObject.GetComponent<Slider>().value;
    }
}
