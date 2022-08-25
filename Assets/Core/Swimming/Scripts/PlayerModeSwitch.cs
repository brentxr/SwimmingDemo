using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;

public class PlayerModeSwitch : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerTeleport playerTeleport;
    [SerializeField] private PlayerGravity playerGravity;
    [SerializeField] private Swimmer swimmer;
    

    [SerializeField] private Transform waterStartLocation;
    [SerializeField] private Transform baseEntranceLocation;
    

    private bool isSwimmer;

    public void ToggleSwimmer()
    {

        playerTeleport.TeleportPlayerToTransform(waterStartLocation);
        rb.useGravity = false;
        swimmer.enabled = true;
        playerGravity.enabled = false;
       
    }
    
    public void ToggleBackToBase()
    {
        
        playerTeleport.TeleportPlayerToTransform(baseEntranceLocation);
        rb.useGravity = true;
        swimmer.enabled = false;
        playerGravity.enabled = true;

    }
}
