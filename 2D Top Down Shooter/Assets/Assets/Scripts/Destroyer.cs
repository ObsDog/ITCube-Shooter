using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    [SerializeField] private float lifetime;

    private void OnEnable() => Invoke(nameof(DestroyGO), lifetime);

    void DestroyGO()
    {
        Destroy(gameObject);
    }
}
