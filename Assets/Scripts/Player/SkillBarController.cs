using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillBarController : MonoBehaviour
{
    [Header("Spell Y")]
    public Image imageCooldownY;
    private float cooldownY;
    private bool _isCooldownY;

    [Header("Spell X")]
    public Image imageCooldownX;
    private float cooldownX;
    private bool _isCooldownX;

    [Header("Spell B")]
    public Image imageCooldownB;
    private float cooldownB;
    private bool _isCooldownB;

    [Header("Spell A")]
    public Image imageCooldownA;
    private float cooldownA = 0.5f;
    private bool _isCooldownA;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        cooldownX = Player.playerInstance.bearTimeBetweenAttack;
        cooldownB = Player.playerInstance.timeBetweenDash;
        cooldownY = Player.playerInstance.timeBetweenShoots;

        // TOUCHE Y
        if (Input.GetKeyDown(KeyCode.Joystick1Button3))
        {

            _isCooldownY = true;

        }

        if (_isCooldownY)
        {
            imageCooldownY.fillAmount += 1 / cooldownY * Time.deltaTime;
        }

        if (imageCooldownY.fillAmount >= 1)
        {
            imageCooldownY.fillAmount = 0;
            _isCooldownY = false;
        }

        // TOUCHE X
        if (Input.GetKeyUp(KeyCode.Joystick1Button2))
        {

            _isCooldownX = true;

        }

        if (_isCooldownX)
        {
            imageCooldownX.fillAmount += 1 / cooldownX * Time.deltaTime;
        }

        if (imageCooldownX.fillAmount >= 1)
        {
            imageCooldownX.fillAmount = 0;
            _isCooldownX = false;
        }

        // TOUCHE B
        if (Input.GetKeyDown(KeyCode.Joystick1Button1))
        {

            _isCooldownB = true;

        }

        if (_isCooldownB)
        {
            imageCooldownB.fillAmount += 1 / cooldownB * Time.deltaTime;
        }

        if (imageCooldownB.fillAmount >= 1)
        {
            imageCooldownB.fillAmount = 0;
            _isCooldownB = false;
        }

        // TOUCHE A
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {

            _isCooldownA = true;

        }

        if (_isCooldownA)
        {
            imageCooldownA.fillAmount += 1 / cooldownA * Time.deltaTime;
        }

        if (imageCooldownA.fillAmount >= 1)
        {
            imageCooldownA.fillAmount = 0;
            _isCooldownA = false;
        }
    }
}
