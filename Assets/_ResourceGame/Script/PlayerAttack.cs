using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private _Bullet BulletPrefabs;
    [SerializeField] private Transform firePos;
    [SerializeField] float speed;

    public void gunFire()
    {
        _Bullet dumpBullet = Instantiate(BulletPrefabs, firePos.position, transform.rotation);
        dumpBullet.setUpBullet(transform.right * speed);
    }

}

