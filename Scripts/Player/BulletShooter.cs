using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] public Bullet[] bullets =  new Bullet[2];
    float _time = 0;
    [SerializeField] float _coolTime;

    public bool IsFireable;

    private void Start() {
        transform.DOLocalMove(Vector2.up * 0.5f, 1f)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine);
    }

    public void InitializeBulletsByPlayerData(PlayerData playerData){
        bullets =  new Bullet[2];
        bullets[0] = GameManager.Instance.BulletDataTableDesign.GetBulletByINDEX(playerData.Character_bullet_index_1);
        bullets[1] = GameManager.Instance.BulletDataTableDesign.GetBulletByINDEX(playerData.Character_bullet_index_2);
    }

    public void InitializeBulletsByMobData(MobData mobData){
        if(!IsFireable) {return;}
        bullets =  new Bullet[1];
        bullets[0] = GameManager.Instance.BulletDataTableDesign.GetBulletByINDEX(mobData.Mob_bullet_index);
    }

    public void FireBullet()
    {
        if(!IsFireable) {return;}
        if (_time < _coolTime) {_time += Time.deltaTime; return;}
        else {
            GameObject bullet = ObjectPool.instance.GetObject(bullets[0].gameObject);
            bullet.transform.position = transform.position;
            bullet.SetActive(true);
            GameManager.Instance.GlobalSoundManager.PlayByClip(bullets[0].bulletSoundData.Shoot, SOUND_TYPE.CONFLICTED);
            _time = 0;
        }
    }

    public void FireJumpBullet()
    {
        if(!IsFireable) {return;}
        GameObject bullet = ObjectPool.instance.GetObject(bullets[1].gameObject);
        bullet.transform.position = transform.position;
        bullet.SetActive(true);
        GameManager.Instance.GlobalSoundManager.PlayByClip(bullets[1].bulletSoundData.Shoot, SOUND_TYPE.CONFLICTED);
    }
}
