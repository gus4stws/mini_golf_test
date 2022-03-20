using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class follow_ball : MonoBehaviour
{
    public GameObject ball;
    [HideInInspector] impulse_ball ball_script;
    [SerializeField] Vector3 cameraPosOffset = new Vector3(0,1,1);
    [SerializeField] Vector3 cameraRotOffset;
    private float ant_ball_y_rot = 0;
    private float ball_y_rot = 0;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = ball.transform.position + cameraPosOffset;
        transform.eulerAngles = cameraRotOffset;
        ball_script = ball.GetComponent<impulse_ball>();
        ant_ball_y_rot = ball_y_rot;
        ball_y_rot = ball_script.y_rot;
    }

    // Update is called once per frame
    void Update()
    {
        
        ball_y_rot = ball_script.y_rot;
        
        if (ball_y_rot != ant_ball_y_rot)
        {
            transform.RotateAround(ball.transform.position, Vector3.up, 1*(ant_ball_y_rot - ball_y_rot));
            ant_ball_y_rot = ball_y_rot;
        }
        else {
            //transform.position = ball.transform.position + cameraPosOffset;
        }

        

    }
}
