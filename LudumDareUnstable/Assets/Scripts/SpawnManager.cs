using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnManager : MonoBehaviour {
    float spawnTimer = 5; 
    public float spawnRate;
    public float fallSpeedModifier;
    public Transform firstSpawnPoint;
    public Transform secondSpawnPoint;
    public Transform destroyLimit;
    Dictionary<Element, int> elementWeight = new Dictionary<Element, int>();
    private void Start() {
        for (int i = 0; i < GameManager._instance.elementArray.Length; i++) {
            elementWeight.Add(GameManager._instance.elementArray[i], 1);
        }
    }
    private void Update() {
        TimerBeforeSpawn();
    }
    void SpawnBetweenTwoPoints(Element element) {
        float randomPosition = Random.Range(0f, 1f);
        Vector3 elementPosition = Vector3.Lerp(firstSpawnPoint.position, secondSpawnPoint.position, randomPosition);
        if (!Physics2D.OverlapBox(elementPosition, element.GetComponent<BoxCollider2D>().size, 0f)){
            Element newElement = Instantiate(element, elementPosition, Quaternion.identity);
            newElement.bounds = destroyLimit;
            newElement.speed *= fallSpeedModifier;
        }
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
       
        spawnTimer -= Time.deltaTime;
        if (spawnTimer <= 0) {
            SpawnBetweenTwoPoints(RandomElementChoosen());
            spawnTimer = spawnRate;
        }
    }

}
