using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : Gun {


    protected override void Awake() {
        base.Awake();
        ForceMultipler = 3;
        FireRate = .5f;
        PitchRange = .2f;
        DefaultPitch = 1f;
    }


    public override void Fire() {
        if (CanFire()) {
            asShot.pitch = DefaultPitch + UnityEngine.Random.Range(-PitchRange, PitchRange);
            asShot.Play();
            AddForceToUser();
            SpawnProjectile();
            timeSinceLastShot = 0f;
        }
    }
}
