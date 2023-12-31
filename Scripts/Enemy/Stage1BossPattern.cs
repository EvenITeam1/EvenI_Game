using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class Stage1BossPattern : MonoBehaviour, IDamagable {
    public  GameObject HitParticle = null;
    public  List<SpriteRenderer> modelSprites = new List<SpriteRenderer>();
    private BossHP bossHP;
    public  Transform particleInstantPosition;

    public List<BossBasicBullet> bullets;
    private int bulletsCount;

    public AudioClip GetDamaged;

    private void Awake() {
        bossHP = GetComponent<BossHP>();
        bulletsCount = bullets.Count;
    }

    public bool IsHitable(){return true;}
    public void GetDamage(float _amount)
    {
        float currentHp = bossHP.getHP();
        bossHP.setHP(currentHp - _amount);
        GameManager.Instance.GlobalSoundManager.PlayByClip(GetDamaged, SOUND_TYPE.SFX);

        //(HitParticle, transform);
        var hitObject = ObjectPool.instance.GetObject(HitParticle);
        hitObject.transform.position = particleInstantPosition.position;
        hitObject.transform.localScale = Vector2.one * 8f;
        hitObject.SetActive(true);
        StartCoroutine(AsyncOnHitVisual());
        bossHP.setHP(currentHp - _amount);
        bossHP.updateHpBar();
        ShootBullet();
    }

    public bool IsShootable = true;
    public void ActiveShootable(){IsShootable = true;}
    public void DeActiveShootable(){IsShootable = false;}
    public void ShootBullet(){
        if(IsShootable == true){
            IsShootable = false;
            Invoke("ActiveShootable", Random.Range(0.2f, 0.6f));
            BossBasicBullet refBullet = bullets[Random.Range(0, bulletsCount)];
            var instantBossBullet = Instantiate(refBullet, transform.position, Quaternion.identity);
            Rigidbody2D bulletRigid = instantBossBullet.GetComponent<Rigidbody2D>();
            bulletRigid.velocity *= Random.Range(-1f, -2f);
            bulletRigid.velocity += 10* Vector2.up * Random.Range(0.5f, 1f);
        }
    }

    IEnumerator AsyncOnHitVisual()
    {
        modelSprites.ForEach(E => E.color = Color.red);
        yield return YieldInstructionCache.WaitForSeconds(0.05f);
        modelSprites.ForEach(E => E.color = Color.white);
    }

}