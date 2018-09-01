using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4 : Gun {

    protected override void Awake() {
        base.Awake();
        ForceMultipler = 3;
        FireRate = .17f;
        PitchRange = .2f;
        DefaultPitch = 1f;
    }


    public override void Fire() {
        base.Fire();
    }
}
