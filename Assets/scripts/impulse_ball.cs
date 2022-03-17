using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class impulse_ball : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float force = 1f;
    [SerializeField] GameObject line; 
    Rigidbody m_Rigidbody;
    private float roll, pitch, yaw;
    private Vector2 roll2v, pitch2v, yaw2v;
    private Vector3 mouse_position;
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
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
        Quaternion quaternion_y = Quaternion.LookRotation((hit_vect - transform.position), Vector3.up);
        Quaternion quaternion = Quaternion.LookRotation(norm_vect, Vector3.forward);
        Vector3 rotation_y = quaternion_y.eulerAngles;
        Vector3 rotation = quaternion.eulerAngles;


        line.transform.eulerAngles = new Vector3(270 - rotation.x, rotation_y.y,270-rotation.z);//new Quaternion( 0,rotation_y.y,0,rotation_y.w );

        Debug.Log(rotation_y);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 force_vector = new Vector3(line.transform.rotation.x,line.transform.rotation.y, line.transform.rotation.z);
            m_Rigidbody.AddForce(line.transform.rotation * Vector3.forward * (force));
        }
    }
}
