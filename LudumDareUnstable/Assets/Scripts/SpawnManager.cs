using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour
{
    float spawnTimer = 0;
    public float spawnRate;
    public float fallSpeedModifier;
    public Element[] elementArray = new Element[0];
    public Transform firstSpawnPoint;
    public Transform secondSpawnPoint;
    public Transform destroyLimit;
    Dictionary<Element, int> elementWeight = new Dictionary<Element, int>();
    private void Start() {
        for (int i = 0; i < elementArray.Length; i++) {
            elementWeight.Add(elementArray[i], 1);
        }
    }
    private void Update() {
        TimerBeforeSpawn();    
    }
    void SpawnBetweenTwoPoints(Element element) {
        float randomPosition = Random.Range(0, 1000)/1000f;
        Vector3 elementPosition = Vector3.Lerp(firstSpawnPoint.position, secondSpawnPoint.position, randomPosition);
        Element newElement = Instantiate(element, elementPosition, Quaternion.identity);
        newElement.bounds = destroyLimit;
    }

    Element RandomElementChoosen() {
        int totWeight = 0;

        for (int i = 0; i < elementWeight.Count; i++) {
            totWeight += elementWeight.ElementAt(i).Value;
        }

        int random = UnityEngine.Random.Range(0, totWeight);

        for (int i = 0; i < elementWeight.Count; i++) {
            if (random < elementWeight.ElementAt(i).Value) {
                return elementWeight.ElementAt(i).Key;
            }
            random -= elementWeight.ElementAt(i).Value;
        }

        return elementWeight.ElementAt(elementWeight.Count - 1).Key;
    }

    void TimerBeforeSpawn() {
        if (spawnTimer == 0) {
            spawnTimer = spawnRate;
        }
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0) {
            spawnTimer = 0;
            SpawnBetweenTwoPoints(RandomElementChoosen());
        }
    }

}
