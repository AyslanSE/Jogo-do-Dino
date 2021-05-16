using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] private float ParaEfecct, length, startpos, speed;

    public Transform cam;

    [SerializeField] private bool continuoParallax, stop;

    void Start()
    {
        cam = Camera.main.transform;
        startpos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void Update()
    {
        if (stop == false)
        {
            if (continuoParallax == false)
            {
                float repos = cam.transform.position.x * (1 - ParaEfecct);
                float distance = cam.transform.position.x * ParaEfecct;

                transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

                if (repos > startpos + length)
                    startpos += length;
                else if (repos < startpos - length)
                    startpos -= length;
            }
            else
            {
                float repos = cam.transform.position.x * (1 - ParaEfecct);
                float distance = speed * ParaEfecct;
                speed++;
                transform.position = new Vector3(startpos + distance, transform.position.y, transform.position.z);

                if (transform.position.x > cam.transform.position.x + length)
                    startpos -= length;
                else if (transform.position.x < cam.transform.position.x - length)
                    startpos += length;
            }
        }
    }
}