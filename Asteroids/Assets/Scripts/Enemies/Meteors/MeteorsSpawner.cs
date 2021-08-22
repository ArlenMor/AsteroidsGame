using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meteors
{
    public class MeteorsSpawner : MonoBehaviour
    {
        public event System.Action<int> EventMeteorDestroyed;

        public int MeteorRemaining
        {
            get { return meteors.Count; }
        }

        [SerializeField]
        private GameObject meteorPrefab;

        [SerializeField]
        private float offScreenPadding;

        [SerializeField]
        private int startingMeteorsCount = 1;

        private List<Meteor> meteors;

        private void Awake()
        {
            Reset();
        }


        public void Spawn(int level)
        {
            int numberMeteors = startingMeteorsCount + level;
            for(int i = 0; i < numberMeteors; i++)
            {
                CreateMeteor(meteorPrefab, GetSpawnPosition(), GetSpawnRotation());
            }
        }

        public void Reset()
        {
            if (meteors != null)
                for (int i = 0; i < meteors.Count; i++)
                    Destroy(meteors[i].gameObject);

            meteors = new List<Meteor>();
        }


        private Vector3 GetSpawnPosition()
        {
            float posX = 0;
            float posY = 0;
            int randParthOfScreen = Random.Range(0, 4);
            switch(randParthOfScreen)
            {
                //bot
                case 0:
                    posX = Random.value;
                    posY = 1f;
                    posY += offScreenPadding;
                    break;
                //top
                case 1:
                    posX = Random.value;
                    posY = 0f;
                    posY -= offScreenPadding;
                    break;
                //left
                case 2:
                    posX = 0f;
                    posY = Random.value;
                    posY -= offScreenPadding;
                    break;
                //right
                case 3:
                    posX = 1f;
                    posY = Random.value;
                    posY += offScreenPadding;
                    break;
            }
            return Camera.main.ViewportToWorldPoint(new Vector3(posX, posY, 2f));
        }

        private Quaternion GetSpawnRotation()
        {
            int angle = 0;
            int randParthOfScreen = Random.Range(0, 4);
            switch(randParthOfScreen)
            {
                case 0:
                    angle = Random.Range(0, 50);
                    break;
                case 1:
                    angle = -Random.Range(0, 50);
                    break;
                case 2:
                    angle = Random.Range(90, 140);
                    break;
                case 3:
                    angle = -Random.Range(90, 140);
                    break;
            }

            return Quaternion.Euler(new Vector3(0, 0, angle));
        }
            
        
        private Meteor CreateMeteor(GameObject pref, Vector3 pos, Quaternion rotation)
        {
            GameObject currentMeteor = Instantiate(pref, pos, rotation) as GameObject;
            currentMeteor.transform.SetParent(gameObject.transform);

            Meteor meteor = currentMeteor.GetComponent<Meteor>();
            meteor.EventDie += OnMeteorDie;

            meteors.Add(meteor);
            return meteor;
        }

        private void OnMeteorDie(Meteor meteor, int points, Vector3 pos, GameObject[] childMeteor)
        {
            meteors.Remove(meteor);

            for(int i = 0; i < childMeteor.Length; i++)
            {
                Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Floor(Random.Range(-45f, 45f)))); ;
                CreateMeteor(childMeteor[i], pos, rotation);
            }

            EventMeteorDestroyed?.Invoke(points);
        }
    }
}

