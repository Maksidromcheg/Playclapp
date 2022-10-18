using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Profiling.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class Spawn : MonoBehaviour
{   
    public InputField TimeSpawnInputField;
    public InputField DistanceInputField;
    public InputField SpeedInputField;
    public Button AcceptButton;

    public Text CurrentSpeedText;
    public Text CurrentDistanceText;
    public Text CurrentTimeSpawnText;
    
    public float cooldown = 0;
    
    [SerializeField] private GameObject cubePrefab;
    [SerializeField] private Transform[] spawnPositions;

    private int spawnTime = 3;
    private int speed = 1;
    private int distance = 10; 

    private int positionSwitch = 0;
    
    void Start()
    {
        cooldown = spawnTime;
        TimeSpawnInputField.characterValidation = InputField.CharacterValidation.Integer;
        DistanceInputField.characterValidation = InputField.CharacterValidation.Integer;
        SpeedInputField.characterValidation = InputField.CharacterValidation.Integer;
        
        UpdateCurrentValues();
    }

    void Update()
    {
        SpawnCubes();
        UpdateCurrentValues();
#if UNITY_ANDROID || UNITY_IOS
        AcceptButton.gameObject.SetActive(true);
#endif
#if UNITY_STANDALONE
        if (Input.GetKeyDown(KeyCode.Return))
        {
            AcceptValues();
        }
        #endif
    }

    public void AcceptValues()
    {
        if (DistanceInputField.text != "" && Convert.ToInt32(DistanceInputField.text) != 0)
        {
            distance = Convert.ToInt32(DistanceInputField.text);
        }

        if (SpeedInputField.text != "" && Convert.ToInt32(SpeedInputField.text) != 0)
        {
            speed = Convert.ToInt32(SpeedInputField.text);
        }

        if (TimeSpawnInputField.text != "")
        {
            spawnTime = Convert.ToInt32(TimeSpawnInputField.text);
        }

        TimeSpawnInputField.text = "";
        DistanceInputField.text = "";
        SpeedInputField.text = "";
    }

    private void SpawnCubes()
    {
        if (spawnTime == 0)
        {
            cooldown = 0;
        }
        else
        {
            cooldown -= Time.deltaTime;
        }

        if (cooldown <= 0 && spawnTime != 0)
        {
            var cube = Instantiate(cubePrefab, ChoicePosCube(), Quaternion.identity).GetComponent<CubeMovement>();
            cube.distance = distance;
            cube.speed = speed;
            cooldown = spawnTime;
        }
    }

    private void UpdateCurrentValues()
    {
        CurrentDistanceText.text = "Current Dist: " + distance;
        CurrentSpeedText.text = "Current Speed: " + speed;
        CurrentTimeSpawnText.text = "Current TimeSpawn: " + spawnTime.ToString();
    }

    private Vector3 ChoicePosCube()
    {
        positionSwitch++;
        if (positionSwitch == 3)
        {
            positionSwitch = 0;
        }
        return spawnPositions[positionSwitch].transform.position;
    }
}
