using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerData {

    private static int deaths;

    public static int Deaths {
        get {
            return deaths;
        }

        set {
            deaths = value;
        }
    }
}
