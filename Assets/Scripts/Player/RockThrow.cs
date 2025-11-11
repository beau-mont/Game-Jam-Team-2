using Unity.Mathematics;
using UnityEngine;

public class RockThrow : MonoBehaviour
{
    public bool rockThrow = false;
    public GameObject cam;
    public GameObject rock;
    public float throwForce;
    public float cooldown = 1f;
    float timer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "RockThrowZone")
        {
            rockThrow = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "RockThrowZone")
        {
            rockThrow = false;
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && timer < Time.time && rockThrow)
        {
            timer = Time.time + cooldown;
            GameObject thrown = Instantiate(rock, cam.transform.position + cam.transform.forward, quaternion.identity);
            thrown.GetComponent<Rigidbody>().AddForce(cam.transform.forward * throwForce + GetComponent<Rigidbody>().linearVelocity);
            thrown.GetComponent<Rigidbody>().AddTorque(new Vector3(UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-0.2f, 0.2f), UnityEngine.Random.Range(-0.2f, 0.2f)));
        }
    }
}
