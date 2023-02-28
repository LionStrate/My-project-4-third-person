using UnityEngine;
using UnityEngine.UI;

public class CollectableItem : MonoBehaviour
{
    public int speedIncreaseAmount = 1;

    public int collectableCount = 0;
    private Text collectableCountText;
    public AIFollow aiScript;

    public float spawnRadius;

    public AudioClip collectSound; 
    
    public AudioClip[] audioClipArray;


    public Vector3 spawnOffset;


    private void Start()
    {



        // Spawn the collectible at a random position within the spawn radius
        Vector3 spawnPosition = Random.insideUnitSphere * spawnRadius + transform.position + spawnOffset;
        spawnPosition.y = transform.position.y;
        transform.position = spawnPosition;


        // Find the collectable count text object in the scene
        collectableCountText = GameObject.Find("CollectableCountText").GetComponent<Text>();
        UpdateCollectableCountText();
    }
    private void UpdateCollectableCountText()
    {
        // Update the collectable count text in the GUI
        collectableCountText.text = "Collectable Count: " + collectableCount;

    }
    private void OnTriggerEnter(Collider other)
    {
        // Check if the player has collected the item
        if (other.CompareTag("Player"))
        {
            // Increase the collectable count and update the GUI
            collectableCount++;
            UpdateCollectableCountText();



            // Increase the AI's speed
            AIFollow aiFollow = FindObjectOfType<AIFollow>();
            if (aiFollow != null)
            {
                aiFollow.moveSpeed += speedIncreaseAmount;
            }


            // Play the collect sound
            AudioSource.PlayClipAtPoint(collectSound, transform.position);
            



            // Destroy the collectable object
            Destroy(gameObject);
        }
    }


}
