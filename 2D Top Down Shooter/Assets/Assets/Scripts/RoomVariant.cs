using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomVariant : MonoBehaviour
{
    public GameObject[] topRooms;
    public GameObject[] bottomRooms;
    public GameObject[] rightRooms;
    public GameObject[] leftRooms;

    public GameObject key;
    public GameObject gun;

    [HideInInspector] public List<GameObject> rooms;

    private void Start()
    {
        StartCoroutine(RandomSpawner());
    }

    IEnumerator RandomSpawner()
    {
        yield return new WaitForSeconds(5f);
        AddRoom lastRoom = rooms[rooms.Count - 1].GetComponent<AddRoom>();
        int rnd = Random.Range(0, rooms.Count - 2);

        Instantiate(key, rooms[rnd].transform.position, Quaternion.identity);
        Instantiate(gun, rooms[rooms.Count - 2].transform.position, Quaternion.identity);

        lastRoom.door.SetActive(true);
        lastRoom.bossSpawner.gameObject.SetActive(true);
        lastRoom.DestroyWalls();
    }
}
