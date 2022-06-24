using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private WeaponBase[] availableWeapons;
    [SerializeField] private PlayerInput playerInput;

    private WeaponBase selectedWeapon;
    private int selectedWeaponID;

    private void Awake() {
        playerInput = this.GetComponent<PlayerInput>();
    }

    private void OnEnable() {
        playerInput.actions["Shoot"].started += ListenToShootButton;
        playerInput.actions["Reload"].started += ListenToReloadButton;
        playerInput.actions["SelectWeapon1"].started += ListenToSelectWeapon1Button;
        playerInput.actions["SelectWeapon2"].started += ListenToSelectWeapon2Button;
        playerInput.actions["SelectWeapon3"].started += ListenToSelectWeapon3Button;
        playerInput.actions["SelectWeapon4"].started += ListenToSelectWeapon4Button;
        playerInput.actions["SelectNextWeapon"].started += ListenToSelectNextWeaponButton;
        playerInput.actions["SelectPreviousWeapon"].started += ListenToSelectPreviousWeaponButton;
    }

    private void OnDisable() {
        playerInput.actions["Shoot"].started -= ListenToShootButton;
        playerInput.actions["Reload"].started -= ListenToReloadButton;
        playerInput.actions["SelectWeapon1"].started -= ListenToSelectWeapon1Button;
        playerInput.actions["SelectWeapon2"].started -= ListenToSelectWeapon2Button;
        playerInput.actions["SelectWeapon3"].started -= ListenToSelectWeapon3Button;
        playerInput.actions["SelectWeapon4"].started -= ListenToSelectWeapon4Button;
        playerInput.actions["SelectNextWeapon"].started -= ListenToSelectNextWeaponButton;
        playerInput.actions["SelectPreviousWeapon"].started -= ListenToSelectPreviousWeaponButton;
    }

    private void ListenToShootButton(InputAction.CallbackContext context)
    {
        selectedWeapon.Shoot();
    }

    private void ListenToReloadButton(InputAction.CallbackContext context)
    {
        selectedWeapon.Reload();
    }

    private void ListenToSelectWeapon1Button(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            selectedWeapon = availableWeapons[0];
            selectedWeaponID = 0;
        }
    }

    private void ListenToSelectWeapon2Button(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            selectedWeapon = availableWeapons[1];
            selectedWeaponID = 0;
        }
    }

    private void ListenToSelectWeapon3Button(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            selectedWeapon = availableWeapons[2];
            selectedWeaponID = 0;
        }
    }

    private void ListenToSelectWeapon4Button(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            selectedWeapon = availableWeapons[3];
            selectedWeaponID = 0;
        }
    }

    private void ListenToSelectNextWeaponButton(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            selectedWeaponID++;
            selectedWeaponID = (selectedWeaponID > 4 ? 0 : selectedWeaponID);
            selectedWeapon = availableWeapons[selectedWeaponID];
        }
    }

    private void ListenToSelectPreviousWeaponButton(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            selectedWeaponID--;
            selectedWeaponID = (selectedWeaponID < 0 ? 4 : selectedWeaponID);
            selectedWeapon = availableWeapons[selectedWeaponID];
        }
    }
}
