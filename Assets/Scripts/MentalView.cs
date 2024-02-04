using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MentalView : MonoBehaviour
{
    [SerializeField] Image fillGauge;
    [SerializeField] Image exaustGauage;

    private void OnEnable()
    {
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ConsumpMental(float mental)
    {
        SetMental(mental);

    }
    public void RestoreMental(float mental)
    {
        SetMental(mental);
    }

    public void RestoreExaust(float exaust)
    {
        exaustGauage.fillAmount = exaust;
    }

    public void Exaust(float initialExaust)
    {
        exaustGauage.fillAmount = initialExaust;
    }

    public void Revival()
    {
        exaustGauage.fillAmount = 0;
    }

    void SetMental(float mental)
    {
        fillGauge.fillAmount = mental;
    }
}
