using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingBullet : MonoBehaviour
{
    public static ObjectPoolingBullet Instance => _instance;
    private static ObjectPoolingBullet _instance;

    private List<Bullet> _listPoolingBullets = new List<Bullet>();
    private List<BulletCircleRed> _listPoolingCircleRed = new List<BulletCircleRed>();
    public Transform parentCcRed;
    private int _amountToPool = 60;

    [SerializeField] private Bullet pbPooling;
    [SerializeField] private BulletCircleRed pbPoolingCircleRed;
    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
        
        InitializePool();
    }
    private void InitializePool()
    {
	    _listPoolingBullets = new List<Bullet>();
	    _listPoolingCircleRed = new List<BulletCircleRed>();
	    
        for (int i = 0; i < _amountToPool; i++)
        {
	        //bullet normal
            Bullet bullet = Instantiate(pbPooling,transform);
            bullet.gameObject.SetActive(false);
            _listPoolingBullets.Add(bullet);
            
            // bullet circle red
            BulletCircleRed ccRed = Instantiate(pbPoolingCircleRed, parentCcRed);
            ccRed.gameObject.SetActive(false);
            _listPoolingCircleRed.Add(ccRed);
        }
    }
    public void GetBullet(Vector3 position, Quaternion rotation)
    {
        int num = (!GameManager.Instance.isMaxBullet) ? GameManager.Instance.bullet : 3;
        if (num == 0)
        {
            num = 1;
        }

        num = num * 2 + 2;
        Bullet[] array2 = new Bullet[num];
        if (transform.childCount > 0)
        {
            for (int k = 0; k < transform.childCount; k++)
            {
                if (transform.GetChild(k).gameObject.activeSelf || num <= 0)
                {
                    break;
                }
                Bullet component2 = transform.GetChild(0).GetComponent<Bullet>();
                component2.transform.SetAsLastSibling();
                component2.transform.localRotation = Quaternion.identity;
                num--;
                array2[num] = component2;
            }
        }
        if (num > 0)
        {
            for (int l = 0; l < num; l++)
            {
                Bullet bullet2 = Instantiate(pbPooling, transform, true);
                bullet2.transform.localScale = Vector3.one;
                bullet2.transform.SetAsLastSibling();
                bullet2.gameObject.SetActive(false);
                array2[l] = bullet2;
            }
        }
        IntanceBullet(array2);
    }
    
    public void GetBulletCircleRed()
    {
	    int numCcRed = (!GameManager.Instance.isMaxBullet) ? 0 : 2;
	    BulletCircleRed[] array2 = new BulletCircleRed[numCcRed];
	    if (parentCcRed.childCount > 0)
	    {
		    for (int k = 0; k < parentCcRed.childCount; k++)
		    {
			    if (parentCcRed.GetChild(k).gameObject.activeSelf || numCcRed <= 0)
			    {
				    break;
			    }
			    BulletCircleRed component2 = parentCcRed.GetChild(0).GetComponent<BulletCircleRed>();
			    component2.transform.SetAsLastSibling();
			    component2.transform.localRotation = Quaternion.identity;
			    numCcRed--;
			    array2[numCcRed] = component2;
		    }
	    }
	    if (numCcRed > 0)
	    {
		    for (int l = 0; l < numCcRed; l++)
		    {
			    BulletCircleRed bullet2 = Instantiate(pbPoolingCircleRed, parentCcRed, true);
			    bullet2.transform.localScale = Vector3.one;
			    bullet2.transform.SetAsLastSibling();
			    array2[l] = bullet2;
		    }
	    }
	    FireCcRed(array2);
    }
    
    public void ReturnBullet(GameObject bullet)
    {
        bullet.SetActive(false);
        bullet.transform.position = Vector3.zero;
        bullet.transform.rotation = Quaternion.identity;
    }
    
    public void ReturnBulletCircleRed(GameObject bullet)
    {
	    bullet.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
	    bullet.SetActive(false);
	    bullet.transform.position = Vector3.zero;
	    bullet.transform.rotation = Quaternion.identity;
    }
    
    #region Bullet Normal Fire

    private void IntanceBullet(Bullet[] prArrBullet)
	{
		Fire0(prArrBullet);
	}

	private void Fire0(Bullet[] x)
	{
		switch (x.Length)
		{
			case 4:
				x[0].Init(1f, 12f, new Vector3(-0.1f, 0f), (int)GameManager.Instance.eAirPlaneType, 1f);
				x[1].Init(1f, 12f, new Vector3(0.1f, 0f), (int)GameManager.Instance.eAirPlaneType, 1f);
				x[2].Init(1f, 12f, new Vector3(-0.1f, 0f), (int)GameManager.Instance.eAirPlaneType+1, 1f);
				x[3].Init(1f, 12f, new Vector3(0.1f, 0f), (int)GameManager.Instance.eAirPlaneType+1, 1f);
				return;
			case 6:
				x[0].Init(1f, 12f, new Vector3(-0.25f, 0f), (int)GameManager.Instance.eAirPlaneType, 1f);
				x[1].Init(1f, 12f, new Vector3(-0.05f, 0f), (int)GameManager.Instance.eAirPlaneType, 1f);
				x[2].Init(1f, 12f, new Vector3(0.15f, 0f), (int)GameManager.Instance.eAirPlaneType,  1f);
				x[3].Init(1f, 12f, new Vector3(-0.15f, 0f), (int)GameManager.Instance.eAirPlaneType+1,  1f);
				x[4].Init(1f, 12f, new Vector3(0.05f, 0f), (int)GameManager.Instance.eAirPlaneType+1,  1f);
				x[5].Init(1f, 12f, new Vector3(0.25f, 0f), (int)GameManager.Instance.eAirPlaneType+1,  1f);
				return;
		}

		x[0].Init(1f, 12f, new Vector3(-0.4f, 0f), (int)GameManager.Instance.eAirPlaneType, 1f);
		x[1].Init(1f, 12f, new Vector3(-0.2f, 0f), (int)GameManager.Instance.eAirPlaneType, 1f);
		x[2].Init(1f, 12f, new Vector3(0f, 0f), (int)GameManager.Instance.eAirPlaneType,  1f);
		x[3].Init(1f, 12f, new Vector3(0.2f, 0f), (int)GameManager.Instance.eAirPlaneType,  1f);
		x[4].Init(1f, 12f, new Vector3(0.4f, 0f), (int)GameManager.Instance.eAirPlaneType,  1f);
		x[5].Init(1f, 12f, new Vector3(0.6f, 0f), (int)GameManager.Instance.eAirPlaneType,  1f);
		x[6].Init(2f, 12f, new Vector3(-0.6f, 0f), (int)GameManager.Instance.eAirPlaneType, 1f);
		// x[7].Init(2f, 12f, new Vector3(0.8f, 0f), (int)GameManager.Instance.eAirPlaneType, 1f);
		// x[8].Init(2f, 12f, new Vector3(-0.8f, 0f), (int)GameManager.Instance.eAirPlaneType, 1f);
		GetBulletCircleRed();
	}

    #endregion

    #region Bullet Circle Red Fire
    	private void FireCcRed(BulletCircleRed[] x)
		{
			x[0].Init(1f, 12f, new Vector3(-0.375f, 0f), (int)GameManager.Instance.eAirPlaneType, 2.75f);
			x[1].Init(1f, 12f, new Vector3(0.375f, 0f), (int)GameManager.Instance.eAirPlaneType, 2.75f);
		}
    #endregion
}
