using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFO
{
    public class UFOSpawner : MonoBehaviour
    {
        public event System.Action<int> EventUFODestroyed;

        public int UFORemaining
        {
            get { return transform.childCount; }
        }

        [SerializeField]
        private GameObject UFOShipPref;

        [SerializeField]
        private float offScreenPadding;

        [SerializeField]
        private float botRandomSpawnTime;
        [SerializeField]
        private float topRandomSpawnTime;


        private UFO ufo;
        //0 - left
        //1 - right
        private bool partOfScreen;

        private void Awake()
        {
            Reset();
        }

        public void Spawn()
        {
            partOfScreen = 50 > Mathf.Floor(Random.Range(0f, 100f));
            float spawnTime = Random.Range(botRandomSpawnTime, topRandomSpawnTime);
            StartCoroutine(SpawnUFO(spawnTime));
        }

        public void Reset()
        {
            if (transform.childCount != 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    ufo = transform.GetChild(i).GetComponent<UFO>();
                    ufo.DestroyAllUFOBullets();
                    Destroy(transform.GetChild(i).gameObject);
                }
                    
            }
        }

        private IEnumerator SpawnUFO(float spawnTime)
        {
            yield return new WaitForSeconds(spawnTime);
            CreateUFO(UFOShipPref, GetSpawnPosition(partOfScreen), partOfScreen);
        }

        private void CreateUFO(GameObject prefShip, Vector3 pos, bool partOfScreen)
        {
            GameObject currentShip = Instantiate(prefShip, pos, Quaternion.identity) as GameObject;
            currentShip.transform.SetParent(gameObject.transform);

            ufo = currentShip.GetComponent<UFO>();
            ufo.EventDie += OnUFODie;

            MoveUFO moveUfo = currentShip.GetComponent<MoveUFO>();
            moveUfo.Init(partOfScreen);
        }

        private Vector3 GetSpawnPosition(bool partOfScreen)
        {
            float posX = 0;
            float posY = 0;
            
            //left
            if(!partOfScreen)
            {
                posX = 0f;
                posX -= offScreenPadding;
                posY = Random.Range(0.2f, 0.8f);
            }
            //right
            else
            {
                posX = 1f;
                posX += offScreenPadding;
                posY = Random.Range(0.2f, 0.8f);
            }
            return Camera.main.ViewportToWorldPoint(new Vector3(posX, posY, 0f));
        }

        private void OnUFODie(UFO ufo, int points)
        {
            EventUFODestroyed(points);
        }

    }

}
