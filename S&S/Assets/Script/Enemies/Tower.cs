using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

    [SerializeField] List<Sprite> spriteList;

    [SerializeField] float shootCooldown;

    [SerializeField] GameObject bullet;

    enum turretOrientation { South, North, East, West, SouthWest, SouthEast, NorthEast, NorthWest};
    [SerializeField] turretOrientation myOrientation;

    float offset;

    float bulletRot;
    Vector3 bulletDir;

    Vector3 spawnPos;

    SpriteRenderer mySprite;

    // Start is called before the first frame update
    void Start()
    {
        offset = 0.2f;
        mySprite = GetComponent<SpriteRenderer>();

        switch (myOrientation)
        {
            case turretOrientation.North:
                spawnPos = transform.position + new Vector3(0, offset, 0);
                mySprite.sprite = spriteList[0];
                bulletRot = 90;
                bulletDir = new Vector3(0, 1, 0);
                break;
            case turretOrientation.South:
                spawnPos = transform.position + new Vector3(0, -offset, 0);
                mySprite.sprite = spriteList[1];
                bulletRot = 270;
                bulletDir = new Vector3(0, -1, 0);
                break;
            case turretOrientation.East:
                spawnPos = transform.position + new Vector3(offset, 0, 0);
                mySprite.sprite = spriteList[2];
                bulletRot = 0;
                bulletDir = new Vector3(1, 0, 0);
                break;
            case turretOrientation.West:
                spawnPos = transform.position + new Vector3(-offset, 0, 0);
                mySprite.sprite = spriteList[3];
                bulletRot = 180;
                bulletDir = new Vector3(-1, 0, 0);
                break;
            case turretOrientation.NorthEast:
                spawnPos = transform.position + new Vector3(offset, offset, 0);
                mySprite.sprite = spriteList[4];
                bulletRot = 45;
                bulletDir = new Vector3(1, 1, 0);
                break;
            case turretOrientation.NorthWest:
                spawnPos = transform.position + new Vector3(-offset, offset, 0);
                mySprite.sprite = spriteList[5];
                bulletRot = 135;
                bulletDir = new Vector3(-1, 1, 0);
                break;
            case turretOrientation.SouthEast:
                spawnPos = transform.position + new Vector3(offset, -offset, 0);
                mySprite.sprite = spriteList[6];
                bulletRot = 315;
                bulletDir = new Vector3(1, -1, 0);
                break;
            case turretOrientation.SouthWest:
                spawnPos = transform.position + new Vector3(-offset, -offset, 0);
                mySprite.sprite = spriteList[7];
                bulletRot = 225;
                bulletDir = new Vector3(-1, -1, 0);
                break;
        }

        StartCoroutine("ShootRoutine");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShootRoutine()
    {

        while (true)
        {
            yield return new WaitForSeconds(shootCooldown);
            GameObject newBullet = Instantiate(bullet);
            newBullet.transform.position = spawnPos;
            newBullet.transform.parent = transform;
            newBullet.transform.GetChild(0).Rotate(0, 0, bulletRot, Space.World);
            newBullet.GetComponent<GenericBullet>().direction = bulletDir;
        }
      
        
    }
}
