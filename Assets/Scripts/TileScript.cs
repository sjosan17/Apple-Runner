using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour {
    // How many seconds before the tile falls after player has been over.
    private float fallDelay = 1;

    // Starts the falldown after the player has been over the tile.
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            TileManager.Instance.SpawnTile();
            StartCoroutine(FallDown());
        }
    }
    // Enabling gravity(kinematic) for the falldown.
    IEnumerator FallDown()
    {
        yield return new WaitForSeconds(fallDelay);
        GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(2);
        switch (gameObject.name)
        {
            case "LeftTile":
                TileManager.Instance.LeftTiles.Push(gameObject);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.SetActive(false);
                break;

            case "TopTile":
                TileManager.Instance.TopTiles.Push(gameObject);
                gameObject.GetComponent<Rigidbody>().isKinematic = true;
                gameObject.SetActive(false);
                break;
        }
    }
}
