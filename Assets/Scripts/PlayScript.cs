using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TextCore.Text;



public class PlayScript : MonoBehaviour
{
    // Start is called before the first frame update
    private float moveSpeed = 3f;
    //private float rotationSpeed = 10f;
    [SerializeField] Transform target;
    [SerializeField] Transform cube;
    bool isSeeking,isFleeing,isArriving,isAvoiding;
    private float slowDistance = 3f;
    [SerializeField] Transform obstacle;
    private float avoidDistance = 2f;
    [SerializeField] AudioSource se1;
    [SerializeField] AudioSource se2;
    [SerializeField] AudioSource se3;
    [SerializeField] AudioSource se4;
    [SerializeField] AudioSource se5;
    void Start()
    {
        cube.gameObject.SetActive(false);
        target.gameObject.SetActive(false);
        obstacle.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            se1.Play();
            cube.gameObject.SetActive(false);
            target.gameObject.SetActive(false);
            obstacle.gameObject.SetActive(false);

            isSeeking = isFleeing = isArriving =  isAvoiding =false;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            se2.Play();
            target.gameObject.SetActive(true);
            cube.gameObject.SetActive(true);
            obstacle.gameObject.SetActive(false);

            isFleeing = isArriving = isAvoiding = false; ResetAction();
            isSeeking = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            se3.Play();
            target.gameObject.SetActive(true);
            cube.gameObject.SetActive(true);
            obstacle.gameObject.SetActive(false);
            isSeeking = isArriving = isAvoiding = false; ResetAction();
            isFleeing = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            se4.Play();
            target.gameObject.SetActive(true);
            cube.gameObject.SetActive(true);
            obstacle.gameObject.SetActive(false);
            isFleeing = isSeeking = isAvoiding = false; ResetAction();
            
            isArriving = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            se5.Play();
            target.gameObject.SetActive(true);
            cube.gameObject.SetActive(true);
            obstacle.gameObject.SetActive(true);
            isFleeing = isArriving = isSeeking = false; ResetAction();
            obstacle.position = new Vector3((cube.position.x + target.position.x) / 2f, (cube.position.y + target.position.y) / 2f, 0f);
            isAvoiding = true;
        }
        
        if (target != null)
        {
            if (isSeeking)
            {

                Vector3 direction = (target.position - cube.position);
                cube.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                cube.GetComponent<Rigidbody>().velocity = direction.normalized * moveSpeed;
            }
            
            if (isFleeing)
            {
                Vector3 direction = (cube.position - target.position);
                Vector3 newdirection = cube.position + direction * moveSpeed * Time.deltaTime;
                if ((cube.position.x > -9f && cube.position.x < 9f) && (cube.position.y < 6f && cube.position.y > -4f))
                {
                    
                    cube.GetComponent<Rigidbody>().velocity = direction.normalized * moveSpeed;
                    cube.rotation = Quaternion.LookRotation(Vector3.forward, direction);
                }
                else
                {
                    Vector3 edge = new Vector3(
                        Mathf.Clamp(newdirection.x, -9f, 9f),
                        Mathf.Clamp(newdirection.y, -4f, 6f),
                        cube.position.z
                    );
                    Vector3 moveEdge=  (edge - cube.position).normalized;
                    cube.GetComponent<Rigidbody>().velocity = moveEdge * moveSpeed;
                }
            }
            
            if(isArriving)
            {
                Vector3 direction = (target.position - cube.position);
                cube.rotation = Quaternion.LookRotation(Vector3.forward, direction);               
                float distance = Vector3.Distance(cube.position, target.position);
                if(distance > slowDistance)
                {
                    cube.GetComponent<Rigidbody>().velocity = direction.normalized * moveSpeed;
                }
                else
                {
                    float newSpeed = Mathf.Lerp(0, moveSpeed, distance / slowDistance);
                    cube.GetComponent<Rigidbody>().velocity = direction.normalized * newSpeed;
                    if(distance < 0.8f)
                    {
                        cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    }
                }
                
            }
            if (isAvoiding)
            {
                
                Vector3 dTarget = (target.position - cube.position).normalized;
                Vector3 dObstacle = (obstacle.position - cube.position).normalized;

                float distanceObstacle = Vector3.Distance(cube.position, obstacle.position);
                float distanceTarget = Vector3.Distance(cube.position, target.position);
                if (distanceTarget > 0.8f)
                {
                    Vector3 move;
                    if (distanceObstacle < avoidDistance)
                    {
                        Vector3 avoidDirection = Vector3.Cross(Vector3.forward, dObstacle).normalized;
                        if (Vector3.Dot(avoidDirection, dTarget) < 0)
                        {
                            avoidDirection = -avoidDirection;
                        }
                        move = (dTarget + avoidDirection).normalized;
                        cube.position += (dTarget + avoidDirection) * moveSpeed * Time.deltaTime;
                        cube.rotation = Quaternion.LookRotation(move, Vector3.up);
                    }
                    else
                    {
                        cube.position += dTarget * moveSpeed * Time.deltaTime;                       
                        cube.rotation = Quaternion.LookRotation(Vector3.forward,dTarget);
                    }                   
                }
                else
                {
                    cube.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }

                
            }
        }
        void ResetAction()
        {
            cube.position = new Vector3(Random.Range(-9f, 9f), Random.Range(-4f, 6f), 0);
            target.position = new Vector3(Random.Range(-9f, 9f), Random.Range(-4f, 6f), 0);
            cube.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

}
