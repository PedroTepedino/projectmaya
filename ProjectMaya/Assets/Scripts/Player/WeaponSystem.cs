using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponSystem : MonoBehaviour
{
    [SerializeField] private WeaponBase[] availableWeapons;
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private GameObject aimSprite;

    public Vector2 aimDirection { get; private set; }
    private Camera camera;
    private WeaponBase selectedWeapon;
    private Rigidbody2D weaponRigidbody;
    private int selectedWeaponID;
    private bool isShooting;

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
        // if (GameManager.GamepadConnected)
        // {
        //     Debug.Log(aimSprite.GetComponentInChildren<SpriteRenderer>().gameObject.transform.position);
        //     aimSprite.GetComponentInChildren<SpriteRenderer>().gameObject.transform.position = new Vector3(6, 0, 0);
        // }
        // else
        // {
        //     aimSprite.GetComponentInChildren<SpriteRenderer>().gameObject.transform.position = Vector3.zero;
        // }

    }

    private void OnEnable()
    {
        //playerInput.actions["Shoot"].performed += ListenToShootButton;
        playerInput.actions["Shoot"].started += ListenToShootButton;
        playerInput.actions["Shoot"].canceled += ListenToShootButton;
        playerInput.actions["Reload"].started += ListenToReloadButton;
        playerInput.actions["SelectWeapon1"].started += ListenToSelectWeapon1Button;
        playerInput.actions["SelectWeapon2"].started += ListenToSelectWeapon2Button;
        playerInput.actions["SelectWeapon3"].started += ListenToSelectWeapon3Button;
        playerInput.actions["SelectWeapon4"].started += ListenToSelectWeapon4Button;
        playerInput.actions["SelectNextWeapon"].started += ListenToSelectNextWeaponButton;
        playerInput.actions["SelectPreviousWeapon"].started += ListenToSelectPreviousWeaponButton;
    }

    private void OnDisable()
    {
        //playerInput.actions["Shoot"].performed -= ListenToShootButton;
        playerInput.actions["Shoot"].started -= ListenToShootButton;
        playerInput.actions["Shoot"].canceled -= ListenToShootButton;
        playerInput.actions["Reload"].started -= ListenToReloadButton;
        playerInput.actions["SelectWeapon1"].started -= ListenToSelectWeapon1Button;
        playerInput.actions["SelectWeapon2"].started -= ListenToSelectWeapon2Button;
        playerInput.actions["SelectWeapon3"].started -= ListenToSelectWeapon3Button;
        playerInput.actions["SelectWeapon4"].started -= ListenToSelectWeapon4Button;
        playerInput.actions["SelectNextWeapon"].started -= ListenToSelectNextWeaponButton;
        playerInput.actions["SelectPreviousWeapon"].started -= ListenToSelectPreviousWeaponButton;
    }

    private void Update()
    {
        if (GameManager.GamepadConnected)
        {
            GamepadAim();
        }
        else
        {
            MouseAim();
        }

        if (isShooting)
        {
            selectedWeapon.Shoot();
        }
    }

    private void MouseAim()
    {
        var mousePosition = playerInput.actions["aim"].ReadValue<Vector2>();
        var mouseWorldPosition = camera.ScreenToWorldPoint(mousePosition);
        mouseWorldPosition.z = 0f;

        aimSprite.transform.position = mouseWorldPosition;

        aimDirection = mouseWorldPosition - this.gameObject.transform.position;

        var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        weaponRigidbody.MoveRotation(angle);

        selectedWeapon.aimDirection = aimDirection;
        Debug.Log("test");
    }

    private void GamepadAim()
    {

        if (playerInput.actions["aim"].ReadValue<Vector2>().magnitude > 0.1f)
        {
            var aimStickDirection = playerInput.actions["aim"].ReadValue<Vector2>();

            aimDirection = aimStickDirection;
        }

        var angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        weaponRigidbody.MoveRotation(angle);
        aimSprite.transform.eulerAngles = new Vector3(0, 0, angle);

        selectedWeapon.aimDirection = aimDirection;
    }

    private void ListenToShootButton(InputAction.CallbackContext context)
    {
        //selectedWeapon.Shoot();
        if (context.started)
        {
            isShooting = true;
        }

        if (context.canceled)
        {
            isShooting = false;
        }
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
}




