using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This class is attached the Snake prefab.
public class PlayerInput : MonoBehaviour
{
    #region Variables
    private PlayerController playerController;

    public int horizontal = 0;
    public int vertical = 0;

    public bool isLeft;
    public bool isRight;
    public bool isDown;
    public bool isUp; 
    #endregion

    public enum Axis
    {
        Horizontal,
        Vertical
    }

    void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }

    void Update()
    {
        horizontal = 0;
        vertical = 0;

        GetKeyboardInput();

        SetMovement();
    }
    #region Inputs for Player movement ()
    private void GetKeyboardInput()
    {
        horizontal = GetAxisRaw(Axis.Horizontal);
        vertical = GetAxisRaw(Axis.Vertical);

        if (horizontal != 0)
        {
            vertical = 0;
        }
    }
    private void SetMovement()
    {
        if (vertical != 0)
        {
            playerController.SetInputDirection((vertical == 1) ? PlayerDirection.UP : PlayerDirection.DOWN);
        }
        else if (horizontal != 0)
        {
            playerController.SetInputDirection((horizontal == 1) ? PlayerDirection.RIGHT : PlayerDirection.LEFT);
        }
    }
    int GetAxisRaw(Axis axis)
    {
        if (axis == Axis.Horizontal)
        {
            bool left = Input.GetKeyDown(KeyCode.LeftArrow);
            bool right = Input.GetKeyDown(KeyCode.RightArrow);

            if (isLeft)
            {
                return -1;
            }

            if (isRight)
            {
                return 1;
            }
            return 0;
        }
        else if (axis == Axis.Vertical)
        {
            bool up = Input.GetKeyDown(KeyCode.UpArrow);
            bool down = Input.GetKeyDown(KeyCode.DownArrow);

            if (isUp)
            {
                return 1;
            }

            if (isDown)
            {
                return -1;
            }
            return 0;
        }

        return 0;
    }

    public void GetButtonIndex(int index)
    {
        switch (index)
        {
            case 1:
                isLeft = true;
                Invoke("AllBools", .001f);
                break;
            case 2:
                isRight = true;
                Invoke("AllBools", .001f);
                break;
            case 3:
                isUp = true;
                Invoke("AllBools", .001f);
                break;
            case 4:
                isDown = true;
                Invoke("AllBools", .001f);
                break;
            default:
                break;
        }
    }
    public void AllBools()
    {
        isLeft = false;
        isRight = false;
        isDown = false;
        isUp = false;
    } 
    #endregion
}
