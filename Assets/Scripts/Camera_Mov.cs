using UnityEngine;

public class Camera_Mov : MonoBehaviour
{
    private Transform player;

    [SerializeField] private bool IsStatic;
    [SerializeField] private float limitX, minY, maxY;

    void Start()
    {
        player = GameObject.Find("Human").GetComponent<Transform>();
    }

    void Update()
    {
        if(IsStatic != true)
        {
            transform.position = new Vector3(
                Mathf.Clamp(player.position.x, player.position.x - limitX, player.position.x + limitX), 
                Mathf.Clamp(player.position.y, minY, player.position.y + maxY), 
                transform.position.z);
        }
    }
}