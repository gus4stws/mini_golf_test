using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impulse_ball : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float force = 1f;
    [SerializeField] GameObject line;
    [HideInInspector] public bool outsided_track = false;
    Rigidbody m_Rigidbody;
    private Vector3 last_pos;
    private float time_init = 0;
    public float y_rot = 90;
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
        last_pos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {


        RaycastHit hit;
        var mouse_ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 norm_vect;
        Vector3 hit_vect;

        if (Physics.Raycast(mouse_ray, out hit))
        {
            hit_vect = hit.point;
            norm_vect = hit.normal;
        }
        else
        {
            hit_vect = Vector3.zero;
            norm_vect= Vector3.zero;
        }
        Vector3 norm_ball;
        RaycastHit hit_ball; 
        if (Physics.Raycast(transform.position, -Vector3.up, out hit_ball))
        {
            norm_ball = hit_ball.normal;
        }
        else
        {
            norm_ball = Vector3.zero;
        }

        Quaternion quaternion_y = Quaternion.LookRotation((transform.position - hit_vect), Vector3.up);
        Quaternion quaternion = Quaternion.LookRotation(norm_ball, Vector3.forward);
        Vector3 rotation_y = quaternion_y.eulerAngles;
        Vector3 rotation = quaternion.eulerAngles;
        line.transform.eulerAngles = new Vector3(180 - rotation.x, rotation_y.y,270-rotation.z);//new Quaternion( 0,rotation_y.y,0,rotation_y.w );
        float dist = Vector3.Distance(new Vector3(hit_vect.x, hit_vect.y, transform.position.z), transform.position); // distance between the ball and the mouse, but just using z position of the ball
        if (Input.GetKeyDown(KeyCode.Space) & (m_Rigidbody.velocity == Vector3.zero))
        {
            last_pos = transform.position;
            Quaternion force_vector = Quaternion.Euler(new Vector3(270 - rotation.x, rotation_y.y, 270 - rotation.z));
            m_Rigidbody.AddForce(force_vector * Vector3.forward * (force) * dist);
            y_rot = rotation_y.y;
        }

        if (((Time.fixedTime - time_init) > 2) & outsided_track)
        {
            //m_Rigidbody.velocity = new Vector3(0, 0, 0); // uncoment when the ball is moving during the relocation
            outsided_track = false;
            transform.position = last_pos;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "floor")
        {
            time_init = Time.fixedTime;
            outsided_track = true;
            
        }
    }
}
