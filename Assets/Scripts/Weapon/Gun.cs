using UnityEngine;
using UnityEngine.VFX;
using System.Collections;

public class Gun : MonoBehaviour
{
    public LayerMask ignoreLayer;
    [Header("Damage Values")]
    public int damage_lowRange; 
    public int damage_midRange;
    public int damage_HightRange;
    public float lowRange, midRange; 
    public float maxRange = 100f;
    public int currentDamageTodeal = 3;
    protected Camera fpsCam;
    // Shooting speed
    [Header("Shoot Fastness ")]
    public float timeBetweenShots;
    private float timer_betweenShots = 0f;
    [HideInInspector] public bool waitingForTimeBetweenShots  = false;
    // Precision
    [Header("Precision")]
    public float calmDuration = 0.64f;      //info: PlayerHandleWeapon gets this
    public float recoilValue = 0.036f;  //info: PlayerHandleWeapon gets this
    public float currentRecoilValue;    //info: PlayerHandleWeapon sets this
    public bool precisionIsPerfect = true;    //info: PlayerHandleWeapon sets this
    protected Vector3 newFPSCamForward;
    [Header("Amunition")]
    // Ammunition
    public int magazineCapacity;
    public int magazineNumberOfBullets = 0;
    public bool isReloading = false;
    public float timeNeeded_Reloading;
    private float timer_Reloading;
    private int totalNumberOfBullets = 0;
    private int numberOfbulletToLoadIn = 0;

    public Item_Ammunition ammunition_Type;

    [Header("Audio")]
    public AudioSource shootSound;
    public AudioSource reloadSound;

    public AudioSource defaultOnHitSound;

    public MaterialSound[] materialSounds;
    [Header("Visuals and UI")]
    // Visuals
    public Transform muzzleTransform;
    public ParticleSystem muzzleFlash;
    //public ParticleSystem onhitSolidEffect;
    public Gun_EffectValues gun_EffectValues;

    public VisualEffect onHitEffect;

    public TrailRenderer bulletTrailRenderer;
    //TEST
    public GameObject TEST_visualObject;
    

    public bool showBulletCubes;

    private Animator animator;

    private void Awake()
    {
        SetUp();
    }

    protected virtual void SetUp()
    {
        animator = GetComponent<Animator>();
        timer_Reloading = timeNeeded_Reloading;
        fpsCam = Camera.main;
        fpsCam = FindObjectOfType<Camera>();
    }

    public void Tick()
    {
        HandleTimers();
    }

    private void HandleTimers()
    {
        if(waitingForTimeBetweenShots)
        {
            timer_betweenShots -= Time.deltaTime;
            if(timer_betweenShots <= 0)
            {
                timer_betweenShots = timeBetweenShots;
                waitingForTimeBetweenShots = false;
            } 
        }
        if(isReloading)
        {
            timer_Reloading -= Time.deltaTime;
            if(timer_Reloading <= 0)
            {
                timer_Reloading = timeNeeded_Reloading;
                isReloading = false;
                Reload();
            }
        }
    }

    //info: returns true if succesfully shooted
    public bool CanWeaponBeTriggerd()
    {
        if(waitingForTimeBetweenShots || isReloading)
        {
            return false;
        }
        return true; 
    }

    public virtual void Shoot()
    {
        
    }

    protected int GetDamageBasedOnRange(float range)
    {
        if(range < lowRange)
        {
            return damage_lowRange;
        } 
        else if(range < midRange)
        {
            return damage_midRange;
        }
        else 
        {
            return damage_HightRange;
        }
    }

    protected void PrepareShooting()
    {
        // Reset Timmer
        timer_betweenShots = timeBetweenShots;
        waitingForTimeBetweenShots = true;
        // Handle Precision
        newFPSCamForward = fpsCam.transform.forward;
        if(!precisionIsPerfect)
        {
            newFPSCamForward.x += currentRecoilValue;
            newFPSCamForward.y += currentRecoilValue;
        }
        precisionIsPerfect = false;
        // Actual Shooting
        magazineNumberOfBullets--;
    }

    #region  Effects
    protected void Test_CreateVisuals(Vector3 position)
    {
        if(showBulletCubes)
        {
            Instantiate(TEST_visualObject, position, Quaternion.identity);
        }
    }

    protected void Effect_OnShoot(RaycastHit hit)
    {
        // Visuals
        muzzleFlash.Play();
        shootSound.Play();
        //visualEffect.Play();
        TrailRenderer  trailRenderer = Instantiate(bulletTrailRenderer, muzzleTransform.position, Quaternion.identity);
        StartCoroutine(SpawnTrail(trailRenderer, hit));
        animator.SetTrigger("shoot");
    }

    private IEnumerator SpawnTrail(TrailRenderer trailRenderer, RaycastHit hit)
    {
        float time = 0;
        Vector3 startPos = trailRenderer.transform.position;
        while(time < 1)
        {
            trailRenderer.transform.position = Vector3.Lerp(startPos, hit.point, time);
            time += Time.deltaTime / trailRenderer.time;
            
            yield return null;
        }
        trailRenderer.transform.position = hit.point;
        // Create Impact stuff;
        Effect_OnHit(hit);
        Destroy(trailRenderer.gameObject, trailRenderer.time);
    }

    protected void Effect_OnHit(RaycastHit hit)
    {
        /*
        if(hit.transform.tag == "Solid")
        {
            Debug.Log("Play Solid Hit Effect");
            //onhitSolidEffect.transform.position = hit.point;
            //onhitSolidEffect.transform.rotation = Quaternion.LookRotation(hit.normal);
            //onhitSolidEffect.Play();
        }
        */

        //onHitEffect.gameObject.SetActive(true);
        //onHitEffect.Play();
        //onHitEffect.transform.position = hit.point;
        //onHitEffect.transform.rotation = Quaternion.LookRotation(hit.point.normalized);
        if(hit.transform.tag == "Solid")
        {
            AudioSource audio = GetMaterialSound(GetMaterial(hit.transform));
            if(audio == null)
            {
                Debug.Log("did not found audio");
            }
            else
                audio.Play();
        }
    }

    private Material GetMaterial(Transform hittedTransform)
    { 
        MeshRenderer hittedMeshRenderer = hittedTransform.GetComponent<MeshRenderer>();
        if(hittedMeshRenderer == null)
        {
            hittedMeshRenderer = hittedTransform.GetComponentInParent<MeshRenderer>();
        }
        if(hittedMeshRenderer == null)
        {
            hittedMeshRenderer = hittedTransform.GetComponentInChildren<MeshRenderer>();
        }
        Debug.Log(hittedMeshRenderer.material);
        return hittedMeshRenderer.material;
    }

    private AudioSource GetMaterialSound(Material material)
    {
        
        foreach (var item in materialSounds)
        {
            foreach (var i in item.material)
            {
                if(i.name == material.name)
                {
                    return item.sound;
                }
            } 
        }
        return defaultOnHitSound;
    }

    #endregion

    public void ResetPrecision()
    {
        precisionIsPerfect = true;
    }

    protected virtual void DoAction(GameObject target, Vector3 pointOfDamage, Vector3 normal)
    {
        Damageable damageable = target.GetComponent<Damageable>();
        if(damageable != null && damageable.enabled)    //info: koennte ignoriert werden dann die collider aus gehen.
        {
            damageable.TakeDamage(currentDamageTodeal, pointOfDamage, normal);
        }
    }

    public void StartReloading(int totalNumberOfBullets)
    {
        this.totalNumberOfBullets = totalNumberOfBullets;
        if(totalNumberOfBullets != 0)
        {
            if(totalNumberOfBullets < magazineCapacity)
            {
                numberOfbulletToLoadIn = totalNumberOfBullets;
            }
            else
            {
                numberOfbulletToLoadIn = magazineCapacity;
            }
            animator.SetTrigger("reload");
            isReloading = true;
            magazineNumberOfBullets = numberOfbulletToLoadIn;
        } 
    }

    private void Reload()
    {                
        // magazineNumberOfBullets = numberOfbulletToLoadIn;
        // Wenn unbeding alles nach der animation gemacht werden soll, 
        // muss ein event erstellt werden wo dann updateUi gesubbed wird und diese Method würde das das even triggert
    }

    
}
