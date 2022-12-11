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
    [SerializeField] private InteractableZone interactableZone;
    [SerializeField] private bool _isWalking, _isTurning;



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
        _inputs.Player.PlayerMovement.performed += PlayerMovement;
        _inputs.Player.PlayerMovement.canceled += PlayerMovement_canceled;

        _inputs.Player.PlayerRotation.performed += PlayerRotation;
        _inputs.Player.PlayerRotation.canceled += PlayerRotation_canceled;

        //Drone
        _inputs.Drone.MoveDrone.performed += MoveDrone_performed;
        _inputs.Drone.RotateDrone.performed += RotateDrone_performed;
        _inputs.Drone.Elevation.performed += Elevation_performed;

        //Forklift
        _inputs.Forklift.MoveForklift.performed += MoveForklift_performed;
        _inputs.Forklift.RaiseLowerForks.performed += RaiseLowerForks_performed;

    }

    public void Update()
    {
        if (_isWalking)
        {
            float movePlayer = _inputs.Player.PlayerMovement.ReadValue<float>();
            _player.CalculateMovement(movePlayer);
        }

        if(_isTurning)
        {
            var rotation = _inputs.Player.PlayerRotation.ReadValue<float>();
            _player.CalculateRotation(rotation);
        }
    }



    #region INTERACTION KEY 'E'
    //Press 'E' key
    private void InteractionKey_performed(InputAction.CallbackContext context)
    {
        Debug.Log("E key 1");
        interactableZone.InteractionKeyPressed();
    }
    //Hold 'E' key
    private void InteractionHold_performed(InputAction.CallbackContext context)
    {
        Debug.Log("E key 2");

        interactableZone.InteractionKeyHeld();
    }
    //'E' key released
    private void InteractionKey_canceled(InputAction.CallbackContext context)
    {
        Debug.Log("E key 3");

        interactableZone.InteractionKeyReleased();
    }
    #endregion

    #region PLAYER
    //Movement
    void PlayerMovement(InputAction.CallbackContext context)
    {
        _isWalking = true;
        //Debug.Log("PlayerMovement");
        //float movePlayer = _inputs.Player.PlayerMovement.ReadValue<float>();
        //_player.CalculateMovement(movePlayer);
        //Debug.Log("Player Move " + movePlayer);
    }
    
    private void PlayerMovement_canceled(InputAction.CallbackContext context)
    {
        _isWalking = false;

        float rotatePlayer = _inputs.Player.PlayerRotation.ReadValue<float>();
        _player.CalculateMovement(rotatePlayer);

    }
    
    //Rotation
    private void PlayerRotation(InputAction.CallbackContext context)
    {
        _isTurning = true;
        var rotation = _inputs.Player.PlayerRotation.ReadValue<float>();
        _player.CalculateRotation(rotation);
    }

    private void PlayerRotation_canceled(InputAction.CallbackContext context)
    {
        _isTurning = false;
        var rotation = _inputs.Player.PlayerRotation.ReadValue<float>();
        _player.CalculateRotation(rotation);
    }
    
    #endregion

    #region DRONE
    private void MoveDrone_performed(InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
    }

    private void RotateDrone_performed(InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
    }

    private void Elevation_performed(InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region FORKLIFT

    private void MoveForklift_performed(InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
    }

    private void RaiseLowerForks_performed(InputAction.CallbackContext obj)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region Enable/Disable Objects
    public void EnableDrone()
    {
        _inputs.Drone.Enable();
        _inputs.Forklift.Disable();
        //DisablePlayer();
    }

    public void EnableForklift()
    {
        _inputs.Forklift.Enable();
        _inputs.Drone.Disable();
        //DisablePlayer();
    }

    public void DisablePlayer()
    {
        _inputs.Player.Disable();
    }

    #endregion



}
