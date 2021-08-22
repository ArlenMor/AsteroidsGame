using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UFO
{
    public class MoveUFO : MonoBehaviour
    {
        [SerializeField]
        private float shipSpeed = 1f;

        private bool partOfScreen;


        private void Update()
        {
            if(partOfScreen)
                transform.Translate(-transform.right * shipSpeed * Time.deltaTime, Space.World);
            else
                transform.Translate(transform.right * shipSpeed * Time.deltaTime, Space.World);

            transform.position = new Vector3(transform.position.x, transform.position.y, 1f);
        }

        public void Init(bool _partOfScreen)
        {
            partOfScreen = _partOfScreen;
        }
    }

}
