using System;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    
    [SerializeField] private Spaceship[] _spaceships;
    private void Awake()
    {
        int dayNumber = GameManager.Instance.DayNumber;

        bool[] activeShips = new bool[_spaceships.Length];
        
        //which ships are activated
        //Less than day number
        //hasn't already been activated
        for (int i = 0; i < _spaceships.Length; i++)
        {
            bool shipAlreadyActivated = GameManager.Instance.ActivatedShips[i];
            
            bool enableShip = i < dayNumber && !shipAlreadyActivated;
            activeShips[i] = enableShip;
            
            Spaceship spaceship = _spaceships[i];
            spaceship.gameObject.SetActive(enableShip);
            
            if (enableShip)
            {
                int shipIndex = i;
                spaceship.OnEngineActivated.AddListener(() => OnEngineActivated(shipIndex));
            }
        }

        //Spawn at the next avalible ship location
        //Go next and wrap
        int spawnIndex = GameManager.Instance.LastSpawnIndex + 1;
        if (spawnIndex >= _spaceships.Length)
        {
            spawnIndex = 0;
        }

        //While this slot isn't active
        //Go to the next slot
        //And wrap
        //This should never get stuck surely.
        try
        {
            while (activeShips[spawnIndex] == false)
            {
                spawnIndex++;
                if (spawnIndex >= _spaceships.Length)
                {
                    spawnIndex = 0;
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        _player.SetSpawn(_spaceships[spawnIndex].PlayerSpawn);
        
        GameManager.Instance.LastSpawnIndex = spawnIndex;
    }

    private void OnEngineActivated(int index)
    {
        GameManager.Instance.EngineActivated(index);
    }
}