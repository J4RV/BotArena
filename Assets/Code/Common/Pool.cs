﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace BotArena {
    
    [Serializable]
    public class Pool{

        [SerializeField]
        public GameObject original;
        readonly Transform poolParent;
        readonly List<GameObject> pooledObjects;

        public Pool(GameObject original){
            poolParent = new GameObject(original.name+"Pool").transform;
            pooledObjects = new List<GameObject>();
            this.original = original;
        }

        /// Returns an inactive pooled object
        public GameObject Get(){
            foreach(GameObject pooledObj in pooledObjects){
                if (!pooledObj.activeInHierarchy){
                    return pooledObj;
                }
            }
            // No inactive pooled objects, lets create one!
            GameObject newPooledObj = UnityEngine.Object.Instantiate(
                original, Vector3.zero, Quaternion.identity);
            newPooledObj.transform.SetParent(poolParent);
            pooledObjects.Add(newPooledObj);
            return newPooledObj;
        }

    }
}
