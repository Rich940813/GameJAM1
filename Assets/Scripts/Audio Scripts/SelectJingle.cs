using UnityEngine;

public class SelectJingle : MonoBehaviour
{
    // Esto es un script general para asignar un jingle a un objeto.
    public bool jingle;
    void Start()
    {
        // condicional temporal
        if (jingle == true)
        {
            // 0 = Wrong 1 = small success 2 = big success
            // Reproduce un sonido especifico del array dentro del AudioManager
            AudioManager.instance.PlayJingle(AudioManager.instance.jingles[2]);
        }
    }
}
