using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    public HeroKnight player;

    public GameObject showHealthText;
    public GameObject showStaminaText;
    public GameObject showManaText;

    public Text healthText;
    public Text staminaText;
    public Text manaText;
    // Start is called before the first frame update
    void Start()
    {
        showHealthText.SetActive(false);
        showStaminaText.SetActive(false);
        showManaText.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = player.currentPlayerHealth.ToString();
        staminaText.text = player.currentPlayerStamina.ToString();
        manaText.text = player.currentPlayerMana.ToString();
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            showHealthText.SetActive(true);
            showStaminaText.SetActive(true);
            showManaText.SetActive(true);
        }
        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            showHealthText.SetActive(false);
            showStaminaText.SetActive(false);
            showManaText.SetActive(false);
        }
    }
}
