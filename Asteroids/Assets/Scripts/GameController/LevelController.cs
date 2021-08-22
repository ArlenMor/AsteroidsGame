using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Player;
using Meteors;
using UFO;

namespace GameController
{
    public class LevelController : MonoBehaviour
    {
        public event System.Action<int> EventPoints;

        public event System.Action EventPlayerDied;

        [SerializeField]
        private int level;
        public int Level { get => level; set => level = value; }

        [SerializeField]
        private Player.Player player;

        [SerializeField]
        private MeteorsSpawner meteorsSpawner;

        [SerializeField]
        private UFOSpawner ufoSpawner;

        [SerializeField]
        private float startLevelDelay = 3.0f;
        

        private void Awake()
        {
            meteorsSpawner.EventMeteorDestroyed += OnMeteorDestroyed;
            ufoSpawner.EventUFODestroyed += OnUFODestroyed;
            player.EventDie += OnPlayerDied;
        }

        public void Reset()
        {
            level = 1;
            meteorsSpawner.Reset();
            ufoSpawner.Reset();
        }

        public void StartLevel()
        {
            meteorsSpawner.Spawn(level);
            ufoSpawner.Spawn();
        }

        public void SpawnPlayer()
        {
            player.Spawn();
        }

        private void OnMeteorDestroyed(int points)
        {
            if (EventPoints != null)
                EventPoints(points);
            
            if(meteorsSpawner.MeteorRemaining == 0 && ufoSpawner.UFORemaining == 0)
            {
                level += 1;
                Invoke(nameof(StartLevel), startLevelDelay);
            }
        }

        private void OnUFODestroyed(int points)
        {
            if (EventPoints != null)
                EventPoints(points);

            if (meteorsSpawner.MeteorRemaining == 0 && ufoSpawner.UFORemaining == 0)
            {
                level += 1;
                Invoke(nameof(StartLevel), startLevelDelay);
            }
        }

        private void OnPlayerDied()
        {
            if (EventPlayerDied != null)
                EventPlayerDied();
        }
    }
}

