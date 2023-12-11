using System;
using System.Collections.Generic;
using NotSpaceInvaders;
using TMPro;
using UnityEngine;

public class SelectShip : MonoBehaviour
{
    public Spaceship[] spaceships;
    public Spaceship currentSpaceship;
    
    [SerializeField] private SpriteRenderer spaceShipImage;
    [SerializeField] private GameObject playImage;
    [SerializeField] private GameObject abilityButton;
    [SerializeField] private TMP_Text shipText;
    [SerializeField] private GameObject buyArea;
    [SerializeField] private TMP_Text buyText;
    
    private int _currentIndex;

    public int GetIndex()
    {
        return _currentIndex;
    }
    
    public void GoRight()
    {
        if (_currentIndex < spaceships.Length - 1)
            _currentIndex++;
        else
            _currentIndex = 0;
        
        UpdateProperties();
    }

    public void GoLeft()
    {
        if (_currentIndex > 0)
            _currentIndex--;
        else
            _currentIndex = spaceships.Length - 1;
        
        UpdateProperties();
    }

    public void PurchaseCar()
    {
        Bridge.GetInstance().BuySpaceship(currentSpaceship.id);

        currentSpaceship.purchased = true;
        
        UpdateProperties();
    }

    public void CheckShips()
    {
        for (int i = _currentIndex; i < spaceships.Length; i++)
        {
            if (spaceships[i].id == spaceships[_currentIndex].id)
                spaceships[_currentIndex].purchased = true;
            else
                spaceships[_currentIndex].purchased = false;
        }
    }
    
    public void UpdateProperties()
    {
        spaceShipImage.sprite = spaceships[_currentIndex].sprite;

        currentSpaceship = spaceships[_currentIndex];

        List<NotSpaceInvaders.Spaceship> spaceshipsData = Bridge.GetInstance().thisPlayerInfo.data.spaceships;

        shipText.text = currentSpaceship.name;
        
        if (_currentIndex > 0)
            abilityButton.SetActive(true);
        else
            abilityButton.SetActive(false);

        if (spaceships[_currentIndex].purchased)
        {
            playImage.SetActive(true);
            buyArea.SetActive(false);
            buyText.gameObject.SetActive(false);
        }
        else
        {
            playImage.SetActive(false);

            buyText.gameObject.SetActive(true);
            
            if (Bridge.GetInstance().thisPlayerInfo.coins >= currentSpaceship.coinsRequired)
            {
                buyText.text = "Not purchased";
                buyArea.SetActive(true);
            }
            else
            {
                buyText.text = "Not enough coins";
                buyArea.SetActive(false);
            }

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
