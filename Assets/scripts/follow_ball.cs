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
    private Vector3 current_position, prev_position;
    private Quaternion current_rotation;
    // Start is called before the first frame update
    void Start()
    {
        prev_position = transform.position;
        transform.position = ball.transform.position + cameraPosOffset;
        transform.eulerAngles = cameraRotOffset;
        ball_script = ball.GetComponent<impulse_ball>();
        //transform.position = ball.transform.position + transform.TransformDirection(cameraPosOffset);
        //transform.RotateAround(ball.transform.position, Vector3.up, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        /*
        if (ball_y_rot != ant_ball_y_rot)
        {
            transform.RotateAround(ball.transform.position, Vector3.up, -(ant_ball_y_rot - ball_y_rot));
            Debug.Log(ball_y_rot);
            ant_ball_y_rot = ball_y_rot;
        }
        else {
            transform.position = ball.transform.position + transform.TransformDirection(cameraPosOffset);
        }
        ball_y_rot = ball_script.y_rot -90;
        */
        
        ball_y_rot = ball_script.y_rot;
        current_position = new Vector3(Circle_x((ball_y_rot) - 270, 1, ball.transform.position.x), ball.transform.position.y + 1, Circle_y((ball_y_rot) - 90, 1, ball.transform.position.z));
        Vector3 relativePos = ball.transform.position - transform.position;
        current_rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        if (!ball_script.outsided_track)
        {
            transform.rotation = current_rotation;//Quaternion.Slerp(prev_rotation, current_rotation, (1/60f));
            transform.position = Vector3.Slerp(prev_position, current_position, (1 / 60f));
            prev_position = transform.position;
        }
    }

    float Circle_x(float angle_deg, float radius, float x) 
    {
        float angle_rad = (angle_deg * Mathf.PI) / 180;
        return (x + (radius * Mathf.Cos(angle_rad)));
    }
    float Circle_y(float angle_deg, float radius, float y)
    {
        float angle_rad = (angle_deg * Mathf.PI) / 180;
        return (y + (radius * Mathf.Sin(angle_rad)));
    }
}
