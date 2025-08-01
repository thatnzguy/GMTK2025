using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private Player _player;
    
    [SerializeField] private Spaceship[] _spaceships;
    private void Awake()
    {
        int dayNumber = GameManager.Instance.DayNumber;
        
        //TODO remember which ships have been activated..
        int shipsActivated = dayNumber;
        if (dayNumber > _spaceships.Length)
        {
            shipsActivated = _spaceships.Length;
        }
        
        for (int i = 0; i<_spaceships.Length; i++)
        {
            Spaceship spaceship = _spaceships[i];
            spaceship.gameObject.SetActive(i < shipsActivated);
        }

        _player.SetSpawn(_spaceships[shipsActivated - 1].PlayerSpawn);
    }
}