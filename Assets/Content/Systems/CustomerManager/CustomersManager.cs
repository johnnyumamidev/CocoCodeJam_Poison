using UnityEngine;
using System.Collections.Generic;

public class CustomersManager : MonoBehaviour
{
    void OnEnable()
    {
        Events.OnCustomerLeaving += TakeOutCurrentCustomer;
        Events.OnCustomerLeft += ResetCustomer;
    }   
    void OnDisable()
    {
        Events.OnCustomerLeaving -= TakeOutCurrentCustomer;
        Events.OnCustomerLeft -= ResetCustomer;
    }   

    [SerializeField] GameObject customerPrefab;
    Customer currentCustomer;
    int minSymptomCount = 0;
    
    [Header("Customer Movement")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform readyPoint;
    [SerializeField] Transform exitPoint;

    void Start()
    {
        //Set max number of possible symptoms for customers
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

    public void SetDifficulty(int difficulty)
    {
        minSymptomCount = difficulty;
    }
    void BringInNewCustomer()
    {
        GameObject newCustomer = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);
        currentCustomer = newCustomer.GetComponent<Customer>();

        currentCustomer.minSymptoms = minSymptomCount;
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
