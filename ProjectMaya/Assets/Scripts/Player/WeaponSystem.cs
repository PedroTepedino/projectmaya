using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private WeaponBase[] availableWeapons;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject aimSprite;

    public Vector2 aimDirection {get; private set;}
    private Camera camera;
    private WeaponBase selectedWeapon;
    private Rigidbody2D weaponRigidbody;
    private int selectedWeaponID;

    private void Awake() 
    {
        playerInput = this.GetComponentInParent<PlayerInput>();
        camera = Camera.main;
    }
    
    private void Start() 
    {
        selectedWeapon = availableWeapons[0];
        selectedWeapon.gameObject.SetActive(true);
        weaponRigidbody = selectedWeapon.gameObject.GetComponent<Rigidbody2D>();
    }

    private void OnEnable() 
    {
        playerInput.actions["Shoot"].started += ListenToShootButton;
        playerInput.actions["Reload"].started += ListenToReloadButton;
        playerInput.actions["SelectWeapon1"].started += ListenToSelectWeapon1Button;
        playerInput.actions["SelectWeapon2"].started += ListenToSelectWeapon2Button;
        playerInput.actions["SelectWeapon3"].started += ListenToSelectWeapon3Button;
        playerInput.actions["SelectWeapon4"].started += ListenToSelectWeapon4Button;
        playerInput.actions["SelectNextWeapon"].started += ListenToSelectNextWeaponButton;
        playerInput.actions["SelectPreviousWeapon"].started += ListenToSelectPreviousWeaponButton;
        playerInput.actions["Aim"].performed += ListenToAim;
    }

    private void OnDisable() 
    {
        playerInput.actions["Shoot"].started -= ListenToShootButton;
        playerInput.actions["Reload"].started -= ListenToReloadButton;
        playerInput.actions["SelectWeapon1"].started -= ListenToSelectWeapon1Button;
        playerInput.actions["SelectWeapon2"].started -= ListenToSelectWeapon2Button;
        playerInput.actions["SelectWeapon3"].started -= ListenToSelectWeapon3Button;
        playerInput.actions["SelectWeapon4"].started -= ListenToSelectWeapon4Button;
        playerInput.actions["SelectNextWeapon"].started -= ListenToSelectNextWeaponButton;
        playerInput.actions["SelectPreviousWeapon"].started -= ListenToSelectPreviousWeaponButton;
        playerInput.actions["Aim"].performed -= ListenToAim;
    }

    private void Update() {
        var mousePosition = playerInput.actions["aim"].ReadValue<Vector2>();
        var mouseWorldPosition = camera.ScreenToWorldPoint(mousePosition);
        mouseWorldPosition.z = 0f;

        aimSprite.transform.position = mouseWorldPosition;

        aimDirection = mouseWorldPosition - this.gameObject.transform.position;
        //aimDirection.Normalize();
        
        var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        weaponRigidbody.MoveRotation(angle);

        selectedWeapon.aimDirection = aimDirection;
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
            selectedWeapon.gameObject.SetActive(false);
            selectedWeapon = availableWeapons[0];
            selectedWeaponID = 0;
            selectedWeapon.gameObject.SetActive(true);
            weaponRigidbody = selectedWeapon.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void ListenToSelectWeapon2Button(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            selectedWeapon.gameObject.SetActive(false);
            selectedWeapon = availableWeapons[1];
            selectedWeaponID = 1;
            selectedWeapon.gameObject.SetActive(true);
            weaponRigidbody = selectedWeapon.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void ListenToSelectWeapon3Button(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            selectedWeapon.gameObject.SetActive(false);
            selectedWeapon = availableWeapons[2];
            selectedWeaponID = 2;
            selectedWeapon.gameObject.SetActive(true);
            weaponRigidbody = selectedWeapon.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void ListenToSelectWeapon4Button(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            selectedWeapon.gameObject.SetActive(false);
            selectedWeapon = availableWeapons[3];
            selectedWeaponID = 3;
            selectedWeapon.gameObject.SetActive(true);
            weaponRigidbody = selectedWeapon.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void ListenToSelectNextWeaponButton(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            selectedWeapon.gameObject.SetActive(false);
            selectedWeaponID++;
            selectedWeaponID = (selectedWeaponID > 4 ? 0 : selectedWeaponID);
            selectedWeapon = availableWeapons[selectedWeaponID];
            selectedWeapon.gameObject.SetActive(true);
            weaponRigidbody = selectedWeapon.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void ListenToSelectPreviousWeaponButton(InputAction.CallbackContext context)
    {
        if (context.canceled)
        {
            selectedWeapon.gameObject.SetActive(false);
            selectedWeaponID--;
            selectedWeaponID = (selectedWeaponID < 0 ? 4 : selectedWeaponID);
            selectedWeapon = availableWeapons[selectedWeaponID];
            selectedWeapon.gameObject.SetActive(true);
            weaponRigidbody = selectedWeapon.gameObject.GetComponent<Rigidbody2D>();
        }
    }

    private void ListenToAim(InputAction.CallbackContext context)
    {
        var mouseWorldPosition = camera.ScreenToWorldPoint(context.ReadValue<Vector2>());
        mouseWorldPosition.z = 0f;

        aimSprite.transform.position = mouseWorldPosition;

        aimDirection = mouseWorldPosition - this.gameObject.transform.position;
        aimDirection.Normalize();
        
        var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        weaponRigidbody.MoveRotation(angle);
    }
}




