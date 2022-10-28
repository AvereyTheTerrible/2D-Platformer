using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SMS.Common
{
    public class InstanstiateUtil : MonoBehaviour
    {
        [SerializeField]
        private GameObject objectToInstantiate;

        public void CreateObject()
        {
            Instantiate(objectToInstantiate, transform.position, Quaternion.identity);
        }
    }
}   