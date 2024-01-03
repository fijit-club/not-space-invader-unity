using System;
using System.Collections.Generic;
using NotSpaceInvaders;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectShip : MonoBehaviour
{
    public Spaceship[] spaceships;
    public Spaceship currentSpaceship;
    public int currentIndex;

    [SerializeField] private SpriteRenderer spaceShipImage;
    [SerializeField] private SpriteRenderer spaceShipVisual;
    [SerializeField] private GameObject playImage;
    [SerializeField] private GameObject abilitySpawner;
    [SerializeField] private TMP_Text shipText;
    [SerializeField] private TMP_Text costText;
    [SerializeField] private GameObject buyArea;
    [SerializeField] private Color32 normalColor;
    [SerializeField] private Color32 disabledColor;

    [SerializeField] private Button buyButton;
    [SerializeField] private TMP_Text buyText;
    [SerializeField] private Animator visual;
    [SerializeField] private TMP_Text coinsText;
    [SerializeField] private TMP_Text shipCostCoinsText;
    [SerializeField] private PlayerShooting playerShooting;

    private bool _changedSpriteInStart;
    
    public int GetIndex()
    {
        return currentIndex;
    }

    public void GoRight()
    {
        if (currentIndex < spaceships.Length - 1)
            currentIndex++;
        else
            currentIndex = 0;
        
        visual.Play("MoveRight", -1, 0f);
        
        UpdateProperties();
    }

    public void GoLeft()
    {
        if (currentIndex > 0)
            currentIndex--;
        else
            currentIndex = spaceships.Length - 1;
        
        visual.Play("MoveLeft", -1, 0f);
        UpdateProperties();
    }

    public void PurchaseCar()
    {
        Bridge.GetInstance().BuySpaceship(currentSpaceship.id);

        currentSpaceship.purchased = true;
        coinsText.text = Bridge.GetInstance().thisPlayerInfo.coins.ToString();
        UpdateProperties();
    }
    
    [ContextMenu("Do Something")]
    public void CheckShips()
    {
        List<NotSpaceInvaders.Asset> dataInBridge = Bridge.GetInstance().thisPlayerInfo.data.assets;
        
        for (int i = 0; i < spaceships.Length; i++)
        {
            for (int j = 0; j < dataInBridge.Count; j++)
            {
                string id = dataInBridge[j].id;
                if (spaceships[i].id == id)
                    spaceships[i].purchased = true;
            }
        }

        coinsText.text = Bridge.GetInstance().thisPlayerInfo.coins.ToString();
    }

    public void ChangeSprite()
    {
        spaceShipVisual.sprite = currentSpaceship.sprite;
    }
    
    public void UpdateProperties()
    {
        currentSpaceship = spaceships[currentIndex];
        
        spaceShipImage.sprite = currentSpaceship.sprite;

        List<NotSpaceInvaders.Asset> spaceshipsData = Bridge.GetInstance().thisPlayerInfo.data.assets;

        shipText.text = currentSpaceship.name;

        if (spaceships[currentIndex].purchased)
        {
            playImage.SetActive(true);
            buyArea.SetActive(false);
            buyButton.gameObject.SetActive(false);
            buyText.gameObject.SetActive(false);
            playerShooting.shooting = currentSpaceship.shootingType;
        }
        else
        {
            playImage.SetActive(false);

            buyText.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(true);
            
            if (Bridge.GetInstance().thisPlayerInfo.coins >= currentSpaceship.coinsRequired)
            {
                buyText.text = "Buy?";
                buyArea.SetActive(true);
                buyButton.interactable = true;
                costText.color = normalColor;
            }
            else
            {
                buyText.text = "Not enough coins";
                buyArea.SetActive(false);
                buyButton.interactable = false;
                costText.color = disabledColor;
            }
            shipCostCoinsText.text = currentSpaceship.coinsRequired.ToString();

        }

        if (!_changedSpriteInStart)
        {
            _changedSpriteInStart = true;
            ChangeSprite();
        }
    }
}

[Serializable]
public class Spaceship
{
    public string name;
    public string id;
    public Sprite sprite;
    public int coinsRequired;
    public bool purchased;
    public ShootingType shootingType;
}
