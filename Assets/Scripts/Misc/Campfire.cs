using Mono.Cecil;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Experimental.GlobalIllumination;

public class Campfire : MonoBehaviour
{
    public int sticksNeeded;
    ParticleSystem ps;
    FlameLight fl;
    Light pl;
    public UnityEvent campfireLit;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        fl = GetComponentInChildren<FlameLight>();
        pl = GetComponentInChildren<Light>();
        ps.Pause();
        fl.enabled = false;
        pl.enabled = false;
    }

    public void StartFire()
    {
        campfireLit.Invoke();
        ps.Play();
        fl.enabled = true;
        pl.enabled = true;
    }
}
