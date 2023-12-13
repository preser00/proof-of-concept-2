using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullablePlatformCleanup : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Respawn")
        {

            Destroy(this.gameObject);

        }
    }
}
