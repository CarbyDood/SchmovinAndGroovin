using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MegaHealthPickUp : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private int recoverAmount = 15;
    [SerializeField] private SoundManager.Sound soundToPlay;

    private float bobHeight = 0.1f;
    private float bobSpeed = 3f;
    private float ogPosY;
    private float yRot = 0f;

    [SerializeField] private bool canRespawn;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        ogPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //spin and bob up and down
        Vector3 pos = transform.position;
        Vector3 rot = transform.rotation.eulerAngles;
        float newY = Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(pos.x, ogPosY + newY, pos.z);
        yRot += 0.3f;
        transform.rotation = Quaternion.Euler(rot.x, yRot, rot.z);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if(other.tag == "Player")
        {
            PlayerMovement player = other.transform.GetComponent<PlayerMovement>();
            player.GiveHealth(recoverAmount, true);

            SoundManager.instance.PlaySound(soundToPlay);
            gameManager.ConsoleMessage("Candy, chocolately goodness! Charged with 100 health!");

            if(canRespawn) gameManager.DisableObjectForTime(gameObject, 5);
            else Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.tag == "Player")
        {
            PlayerMovement player = other.transform.GetComponent<PlayerMovement>();
            player.GiveHealth(recoverAmount, true);

            SoundManager.instance.PlaySound(soundToPlay);
            gameManager.ConsoleMessage("Candy, chocolately goodness! Charged with 100 health!");

            if(canRespawn) gameManager.DisableObjectForTime(gameObject, 5);
            else Destroy(gameObject);
        }
    }
}
