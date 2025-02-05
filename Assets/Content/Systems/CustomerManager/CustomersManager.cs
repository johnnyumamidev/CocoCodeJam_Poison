using UnityEngine;
using System.Collections.Generic;

public class CustomersManager : MonoBehaviour
{
    void OnEnable()
    {
        Events.OnSymptomsCured += TakeOutCurrentCustomer;
    }   
    void OnDisable()
    {
        Events.OnSymptomsCured -= TakeOutCurrentCustomer;
    }   

    [SerializeField] List<GameObject> customerPrefabs = new List<GameObject>();
    
    [Header("Customer Movement")]
    [SerializeField] Transform spawnPoint;
    [SerializeField] Transform readyPoint;
    [SerializeField] Transform exitPoint;

    void Start()
    {
        BringInNewCustomer();
    }

    void BringInNewCustomer()
    {
        GameObject newCustomer = Instantiate(customerPrefabs[Random.Range(0, customerPrefabs.Count)], spawnPoint.position, Quaternion.identity);
        Customer customer = newCustomer.GetComponent<Customer>();

        customer.SetMoveTarget(readyPoint.position);
    }

    void TakeOutCurrentCustomer()
    {
        //when customer has left, call BRINGINCUSTOMER again
    }
}
