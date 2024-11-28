using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Division_Slider : MonoBehaviour
{   
    public Slider _slider3;
    public TextMeshProUGUI _sliderText3;
    public Configuration_b configuration_b; // reference to the script
    
    void Start()
    {   // display change of values
        if (_slider3 != null) {
        _slider3.onValueChanged.AddListener((float value) => {
            _sliderText3.text = value.ToString("0.00");
            // update the iterations
            if (configuration_b != null){
                Debug.Log($"Updating Iterations to: {value}");
                configuration_b.iterations = value;
            }
            if (_sliderText3 == null)
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
