using System.Collections;
using System.Collections.Generic;
using BNG;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public class PlayerModeSwitch : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private PlayerTeleport playerTeleport;
    [SerializeField] private PlayerGravity playerGravity;
    [SerializeField] private Swimmer swimmer;
    [SerializeField] private GameObject backToBaseButton;
    [SerializeField] private ScreenFader screenFader;
    

    [SerializeField] private Transform waterStartLocation;
    [SerializeField] private Transform baseEntranceLocation;
    

    private bool isSwimmer;

    public void ToggleSwimmer()
    {
        screenFader = FindObjectOfType<ScreenFader>();

        screenFader.DoFadeIn();
        playerTeleport.TeleportPlayerToTransform(waterStartLocation);
        rb.useGravity = false;
        swimmer.enabled = true;
        playerGravity.enabled = false;
        screenFader.DoFadeOut();
        //backToBaseButton.GetComponent<Button>().enabled = true;
        //backToBaseButton.GetComponent<Image>().enabled = true;

    }
    
    public void ToggleBackToBase()
    {
        screenFader = FindObjectOfType<ScreenFader>();

        screenFader.DoFadeIn();
        playerTeleport.TeleportPlayerToTransform(baseEntranceLocation);
        rb.useGravity = true;
        swimmer.enabled = false;
        playerGravity.enabled = true;
        screenFader.DoFadeOut();
        //backToBaseButton.GetComponent<Button>().enabled = false;
        //backToBaseButton.GetComponent<Image>().enabled = false;
    }
}
