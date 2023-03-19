using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pillar : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 10f);
    }
}
