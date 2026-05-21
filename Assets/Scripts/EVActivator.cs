using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


public class EVActivator : MonoBehaviour
{
    public string[] Buttons;
    //public GameObject[] Environnement;

    public Material[] Skybox;

    //public LightingSettings[] LightingSetting;


    public GameObject[] Environnement;

    //public SkySettings[] SkySetting;






    void Update()
    {
        //if (!isLocalPlayer) { return; }
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (Input.GetKey(Buttons[i]))
            {
                // Environnement
                Environnement[i].SetActive(true);

                // Skybox
                RenderSettings.skybox = Skybox[i];
/*
                //Reflextion
                RenderSettings.reflectionIntensity = SkySetting[i].intensityMultiplier;


                // Fog
                RenderSettings.fog = SkySetting[i].fogEnabled;
                RenderSettings.fogColor = SkySetting[i].fogColor;
                RenderSettings.fogMode = SkySetting[i].fogMode;
                RenderSettings.fogDensity = SkySetting[i].fogDensity;
                RenderSettings.fogStartDistance = SkySetting[i].fogStartDistance;
                RenderSettings.fogEndDistance = SkySetting[i].fogEndDistance;
*/
                
                //Lightmapping.lightingSettings = SkySetting[i].LightingSetting;

                for (int j = 0; j < Buttons.Length; j++)
                    if (j != i)
                        Environnement[j].SetActive(false);
                        RenderSettings.skybox = Skybox[i];
                
            }


        }


    }





}