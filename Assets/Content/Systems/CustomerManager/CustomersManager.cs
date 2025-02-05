using UnityEngine;
using System.Collections.Generic;

public class CustomersManager : MonoBehaviour
{
    void OnEnable()
    {
        Events.OnSymptomsCured += TakeOutCurrentCustomer;
        Events.OnCustomerLeft += ResetCustomer;
    }   
    void OnDisable()
    {
        Events.OnSymptomsCured -= TakeOutCurrentCustomer;
        Events.OnCustomerLeft -= ResetCustomer;
    }   

    [SerializeField] List<GameObject> customerPrefabs = new List<GameObject>();
    Customer currentCustomer;
    
    [Header("Customer Movement")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform readyPoint;
    [SerializeField] Transform exitPoint;

    void Start()
    {
        BringInNewCustomer();
    }
    void Update()
    {
        //check if customer is at the counter
        if(currentCustomer == null)
        {
            BringInNewCustomer();
        }
    }

    void BringInNewCustomer()
    {
        GameObject newCustomer = Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Count)], spawnPoint.position, Quaternion.identity);
        currentCustomer = newCustomer.GetComponent<Customer>();

        currentCustomer.SetMoveTarget(readyPoint.position, false);
    }

    void TakeOutCurrentCustomer()
    {
        //when customer has left, call BRINGINCUSTOMER again
        currentCustomer.SetMoveTarget(exitPoint.position, true);
    }

    void ResetCustomer()
    {
        Destroy(currentCustomer.gameObject);
        currentCustomer = null;
    }
}
