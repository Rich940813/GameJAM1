using UnityEngine;

public class CamaraRotation : MonoBehaviour
{
    public GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(0.5f + player.transform.position.x,10,-1f+player.transform.position.z);
        
    }
}
