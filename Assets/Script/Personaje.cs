using UnityEngine;

public class Personaje : MonoBehaviour
{
    public string nombre;
    public int vida;
    public int velocidad;
    private bool flagMensaje1 = false;
    private bool flagMensaje2 = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= 2.0f && !flagMensaje1)
        {
            Presentarse();
            flagMensaje1 = true;
        }
        else if (Time.time >= 6.0f && !flagMensaje2)
        {
            Mover();
            flagMensaje2 = true;
        }
    }

    void Presentarse()
    {
        Debug.Log($"Hola, mi nombre es {nombre}");
    }
    void Mover()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + velocidad, transform.position.z);
        Debug.Log($"Movimiento de {velocidad} casillas");
    }
}
