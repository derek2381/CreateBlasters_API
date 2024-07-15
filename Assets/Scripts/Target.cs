using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
  private Rigidbody targetRb;
  private GameManager gameManager;
  private float maxSpeed = 16; //16
  private float minSpeed = 12; //12
  private float maxTourque = 10;
  private float xRange = 4;
  private float ySpawnpos = -4;

  public int pointValue;
  public ParticleSystem explosionParticle;


    // Start is called before the first frame update
    void Start()
    {
      targetRb = GetComponent<Rigidbody>();

      gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

      targetRb.AddForce(RandomForce(), ForceMode.Impulse);
      targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

      transform.position = RandomSapwnPos();

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnMouseDown(){
      if(gameManager.isGameActive){
        Destroy(gameObject);
        Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
        gameManager.UpdateScore(pointValue);
      }

    }

    private void OnTriggerEnter(Collider other){
      Destroy(gameObject);
      if(!gameObject.CompareTag("Bad")){
        gameManager.GameOver();
      }
    }

    Vector3 RandomForce(){
      return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque(){
      return Random.Range(-maxTourque, maxTourque);
    }

    Vector3 RandomSapwnPos(){
      return new Vector3(Random.Range(-xRange, xRange), ySpawnpos);
    }
}
