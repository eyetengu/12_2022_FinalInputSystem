using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Game.Scripts.Player;
using Game.Scripts.LiveObjects;

public class InputManager : MonoSingleton<InputManager>
{
    //Notes:
    //the InputManager will control and manipulate(as needed) ALL of the input controls and their respective values
    //The Input Manager will control the following:
    //Player, Drone, Forklift
    //these will be activated in their respective scripts from the Input Manager
    //-----
    //the interaction key(E) will activate scripts in the InteractableZone.cs



    //ACTION Maps
    private CoreProject_Inputs _inputs;
    
    public Player _player;

    private void Start()
    {
        InitializeInputs();
    }

    public void InitializeInputs()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        _inputs = new CoreProject_Inputs();
        _inputs.Interactions.Enable();

        //Interactions
        _inputs.Interactions.InteractionKey.performed += InteractionKey_performed;
        _inputs.Interactions.InteractionHold.performed += InteractionHold_performed;
        _inputs.Interactions.InteractionKey.canceled += InteractionKey_canceled;

        //Player
        _inputs.Player.Enable();
        _inputs.Player.PlayerMovement.performed += PlayerMovement_performed;

        //Drone
        //Forklift

    }

    private void PlayerMovement_performed(InputAction.CallbackContext context)
    {
    }

    private void Update()
    {
        float movePlayer = _inputs.Player.PlayerMovement.ReadValue<float>();
        float rotatePlayer = _inputs.Player.PlayerRotation.ReadValue<float>();
        _player.CalculateMovement(movePlayer, rotatePlayer);
        
    }


    //INTERACTION KEY 'E'
    //Press 'E' key
    private void InteractionKey_performed(InputAction.CallbackContext context)
    {
        Debug.Log("E key 1");
        InteractableZone.Instance.PressedEKey();
    }
    //Hold 'E' key
    private void InteractionHold_performed(InputAction.CallbackContext context)
    {
        Debug.Log("E key 2");

        InteractableZone.Instance.PressHoldEKey();
    }
    //'E' key released
    private void InteractionKey_canceled(InputAction.CallbackContext context)
    {
        Debug.Log("E key 3");

        InteractableZone.Instance.EKeyReleased();
    }
}
