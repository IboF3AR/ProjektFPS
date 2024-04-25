using System.Collections;
using UnityEngine;
using TMPro;

public class Player_HandleWeapon : MonoBehaviour
{
    private Player_Inputs player_Inputs;
    public bool showBulletCubes = true;
    public Gun equiptedWeapon;
    public Gun[] allWeapons;
    private int equiptedWeaponNumber = 1;
    private Player_HandleCrossHair handleCrossHair;

    private bool isReloading  = false;      //info: reloading kann nicht unterbrochen werden sollte aber siehe CSGO
    // Precision
    private float calmSpeed = 0;  // info: the lower, the slower the player will calm down after shooting
    private bool precisionIsPerfect = true;
    public float currentRecoilValue = 0;
    private float recoilValue = 0.036f;
    private float calmDuration = 1;
    public Player_WeaponRecoil player_WeaponRecoil;

    private DitzeGames.Effects.CameraEffects cameraEffects;
    // Ammuntion
    public Item_Ammunition currentWeaponsAmunition;
    public int BulletAmountOfCurrentWeapon;
    public TMP_Text ui_txt_bullets;

    // Start is called before the first frame update
    void Start()
    {
        ui_txt_bullets = GameObject.Find("ed_txt_bullets").GetComponent<TMP_Text>();
        StartCoroutine(TEST());
        player_Inputs = Player_Inputs.Inst;
        SetUpDefaultGun();
        handleCrossHair = FindObjectOfType<Player_HandleCrossHair>();
        handleCrossHair.SetUp(calmDuration);
        foreach (var item in GetComponentsInChildren<Gun>())
        {
            item.showBulletCubes = showBulletCubes;
            if(item != equiptedWeapon)
            {
                item.gameObject.SetActive(false);
            }
        }
        
    }

    void HookInputs()
    {
        //player_Inputs.inputs.InGame.
    }

    private void SetUpDefaultGun()
    {
        cameraEffects = GetComponent<DitzeGames.Effects.CameraEffects>();
        calmDuration = equiptedWeapon.calmDuration;
        recoilValue = equiptedWeapon.recoilValue;
        ReloadGun();
        player_WeaponRecoil.SetRecoilValues(equiptedWeapon.gun_EffectValues);
        cameraEffects.SetValues(equiptedWeapon.gun_EffectValues);
        currentRecoilValue = recoilValue;
        calmSpeed = recoilValue / calmDuration;
    }

    IEnumerator TEST()
    {
        yield return new WaitForSeconds(0.2f);
        foreach (var item in allWeapons)
        {
            Inventory.Inst.Add_Item(item.ammunition_Type, 16);
        }
        UpdateUI();
    }

    // Update is called once per frame
    void Update()
    {
        Check_Input();
        if(!precisionIsPerfect) CalmDown();
        equiptedWeapon.Tick();
    }

    private void Check_Input()
    {
        if(player_Inputs.Get_Btn_WS1())
        {
            ChangeGun(1);
        } else if(player_Inputs.Get_Btn_WS2())
        {
            ChangeGun(2);
        }

        handleCrossHair.Tick();
        if(player_Inputs.LeftMB)
        {
            TryToShoot();
        } 
    }

    private void TryToShoot()
    {
        if(equiptedWeapon.CanWeaponBeTriggerd())
        {
            if(equiptedWeapon.magazineNumberOfBullets == 0) 
            {
                ReloadGun();
            }
            else 
            {
                equiptedWeapon.Shoot();
                player_WeaponRecoil.RecoilFire();
                cameraEffects.Shake();
                UpdateUI();
                DecreasePrecision();
            }
        }
    }

    private void CalmDown()
    {
        currentRecoilValue -= Time.deltaTime * calmSpeed;
        if(currentRecoilValue <= 0)
        {
            currentRecoilValue = 0;
            precisionIsPerfect = true;
            equiptedWeapon.ResetPrecision();
        }
        equiptedWeapon.currentRecoilValue = currentRecoilValue;
    }

    public void DecreasePrecision()
    {
        precisionIsPerfect = false;
        currentRecoilValue = Random.Range(recoilValue*0.6f, recoilValue);
        calmSpeed = currentRecoilValue / calmDuration;

        handleCrossHair.DecreasePrecision();
        equiptedWeapon.precisionIsPerfect = false;
        equiptedWeapon.currentRecoilValue = currentRecoilValue;
    }

    public void ChangeGun(int v)
    {
        isReloading  = equiptedWeapon.isReloading;
        if(isReloading) return;
        if(v <= allWeapons.Length && v != equiptedWeaponNumber)
        {
            equiptedWeapon.gameObject.SetActive(false);
            equiptedWeapon = allWeapons[v -1];
            equiptedWeaponNumber = v;
            equiptedWeapon.gameObject.SetActive(true);
            ReloadGun();
            // Recoil
            calmDuration = equiptedWeapon.calmDuration;
            recoilValue = equiptedWeapon.recoilValue;
            player_WeaponRecoil.SetRecoilValues(equiptedWeapon.gun_EffectValues);
            cameraEffects.SetValues(equiptedWeapon.gun_EffectValues);
        }
    }

    private void ReloadGun()
    {
        // Amunition
        currentWeaponsAmunition = equiptedWeapon.ammunition_Type;
        if(Inventory.Inst.items.ContainsKey(currentWeaponsAmunition))
        {
            BulletAmountOfCurrentWeapon = Inventory.Inst.items[currentWeaponsAmunition];
            equiptedWeapon.StartReloading(BulletAmountOfCurrentWeapon);
            BulletAmountOfCurrentWeapon -= equiptedWeapon.magazineCapacity;
            if(BulletAmountOfCurrentWeapon < 0) BulletAmountOfCurrentWeapon = 0;
            Inventory.Inst.Remove_Item(currentWeaponsAmunition, equiptedWeapon.magazineCapacity);
        } else
        {
            BulletAmountOfCurrentWeapon = 0;
        }
        UpdateUI();
        // consume the munition in the inventory already
    }

    public void OnAmmunitionPickedUp(Item pickedUpAmmou)
    {
        if(pickedUpAmmou == currentWeaponsAmunition)
        {
            BulletAmountOfCurrentWeapon = Inventory.Inst.items[currentWeaponsAmunition];
        }
        UpdateUI();   
    }

    private void UpdateUI()
    {
        ui_txt_bullets.text = equiptedWeapon.magazineNumberOfBullets + " / " + BulletAmountOfCurrentWeapon;
    }
}
