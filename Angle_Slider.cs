using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Angle_Slider : MonoBehaviour
{   
    public Slider _slider2;
    public TextMeshProUGUI _sliderText2;
    public Configuration_a configuration_a;
    public Configuration_b configuration_b; // reference to the script
    public Configuration_c configuration_c;
    public Configuration_d configuration_d;
    public Configuration_e configuration_e;
    public Configuration_f configuration_f;
    public PythagorasTree configuration_j;
    public RandomPineTree configuration_k;


    void Start()
    {   // display change of values
        if (_slider2 != null) {
        _slider2.onValueChanged.AddListener((float value2) => {
            _sliderText2.text = value2.ToString("0.00");
            // update the iterations
            if (configuration_b != null){
                Debug.Log($"Updating Angles to: {value2}");
                configuration_a.angleR = value2;
                configuration_b.angleR = value2;
                configuration_c.angleR = value2;
                configuration_d.angleR = value2;
                configuration_e.angleR = value2;
                configuration_f.angleR = value2;
                configuration_j.angleR = value2;
                configuration_k.angleR = value2;
            }
            if (_sliderText2 == null)
            {
            Debug.LogError("Slider Text (TextMeshProUGUI) is not assigned in the Inspector!");
            }
            if (configuration_b == null)
            {
            Debug.LogError("Configuration script reference is missing!");
            }
        });
        
        } else {
        Debug.LogError("Slider is not assigned in the Inspector");
        }

    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
