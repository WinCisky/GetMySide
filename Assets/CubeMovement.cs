using UnityEngine;

public class CubeMovement : MonoBehaviour
{
    //main camera rotating with the view
    public Transform targetForRotation;
    //main cube moving in the scene
    Rigidbody rb;
    //main cube box collider size
    BoxCollider bc;
    //force multiplier to apply
    public float force;
    //active camera index
    private int cam_index;
    //array of virtual cameras
    public GameObject[] virtual_cams;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        bc = gameObject.GetComponent<BoxCollider>();
        bc.size = new Vector3(1, 1, 30);
        cam_index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //direction based on rotation
        Vector3 rightVector = targetForRotation.transform.rotation * Vector3.right;
        Vector3 leftVector = targetForRotation.transform.rotation * Vector3.left;
        //apply force based on rotation
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(rightVector * force);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(leftVector * force);
        }
        //switching the side
        if (Input.GetKeyDown(KeyCode.S))
        {
            SwitchSide();
        }
        //jumping
        if (Input.GetKeyDown(KeyCode.W))
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (rb.constraints == (RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY))
        {
            transform.position = new Vector3(transform.position.z, transform.position.y, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.x);
        }
    }

    //costraint change
    //reset the cube rotation to allow smooth movement in the other dimension
    public void SwitchSide()
    {
        if(rb.constraints == ( RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY))
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
            transform.rotation = Quaternion.identity;
            transform.position = new Vector3(transform.position.z, transform.position.y, transform.position.z);
            bc.size = new Vector3(1, 1, 30);
        }
        else
        {
            rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY;
            transform.rotation = Quaternion.identity;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.x);
            bc.size = new Vector3(30, 1, 1);
        }
        //change the active camera
        SwitchCamera();
    }

    //change the active camera
    private void SwitchCamera()
    {
        virtual_cams[cam_index % virtual_cams.Length].SetActive(false);
        virtual_cams[++cam_index % virtual_cams.Length].SetActive(true);
    }

    public void Jump()
    {
        //jump if I'm touching the ground
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            if(hitColliders[i].tag == "ground")
            {
                rb.AddForce(Vector3.up * force / 4, ForceMode.Impulse);
                break;
            }
        }
    }
}
