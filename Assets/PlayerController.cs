using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;

    //wartosc wczytana z klawiatury
    Vector2 input;

    //mnoznik przyspieszenia statku
    public float enginePower = 10;

    //mnoznik sterowania
    public float gyroPower = 2;

    private CameraScript cs;

    //miejsce na prefab pocisku
    public GameObject bulletPrefab;
    //okresl miejsca spawnowania pociskow
    public Transform GunLeft, GunRight;

    //predkosc poczatkowa pocisku
    public float bulletSpeed = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cs = Camera.main.transform.GetComponent<CameraScript>();
        input = Vector2.zero;
        GunLeft = transform.Find("GunLeft").transform;
        GunRight = transform.Find("GunRight").transform;
    }

    // Update is called once per frame
    void Update()
    {
        //sterowanie w poziomie(a,d)
        float x = Input.GetAxis("Horizontal");
        //sterowanie w pionie(w,s)
        float y = Input.GetAxis("Vertical");

        input.x = x;
        input.y = y;

        //teleportuj statek jesli wyjdzie z ekranu
        if (Mathf.Abs(transform.position.x) > cs.gameWidth / 2)
        {
            Vector3 newPosition = new Vector3(transform.position.x * (-0.99f),
                                                0,
                                                transform.position.z);
            transform.position = newPosition;
        }

        if (Mathf.Abs(transform.position.z) > cs.gameHeight / 2)
        {
            Vector3 newPosition = new Vector3(transform.position.x,
                                                0,
                                                transform.position.z * (-0.99f));
            transform.position = newPosition;
        }
        if (Input.GetKeyDown(KeyCode.Space))
            Fire();
    }
    void FixedUpdate()
    {
        rb.AddForce(transform.forward * input.y * enginePower, ForceMode.Acceleration);
        rb.AddTorque(transform.up * input.x * gyroPower, ForceMode.Acceleration);
    }

    void Fire()
    {
        GameObject leftBullet = Instantiate(bulletPrefab, GunLeft.position, Quaternion.identity);
        //zmiana predkosci pocisku
        leftBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed,
                                                        ForceMode.VelocityChange);

        //zniszcz pocisk po 5s
        Destroy(leftBullet, 5);

        GameObject rightBullet = Instantiate(bulletPrefab, GunRight.position, Quaternion.identity);
        //zmiana predkosci pocisku
        rightBullet.GetComponent<Rigidbody>().AddForce(transform.forward * bulletSpeed,
                                                        ForceMode.VelocityChange);

        //zniszcz pocisk po 5s
        Destroy(rightBullet, 5);
    }
}