using UnityEngine;
using System.Collections;

public class ExplosionScript : MonoBehaviour
{
    public void Destroy()
    {
        Destroy(this.gameObject);
    }
}
