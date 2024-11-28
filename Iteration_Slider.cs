using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Iteration_Slider : MonoBehaviour
{   
    public Slider _slider1;
    public TextMeshProUGUI _sliderText1;
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
        if (_slider1 != null) {
        _slider1.onValueChanged.AddListener((float value) => {
            _sliderText1.text = value.ToString("0.00");
            // update the iterations
            if (configuration_b != null){
                Debug.Log($"Updating Iterations to: {value}");
                configuration_a.iterations = value;
                configuration_b.iterations = value;
                configuration_c.iterations = value;
                configuration_d.iterations = value;
                configuration_e.iterations = value;
                configuration_f.iterations = value;
                configuration_j.iterations = value;
                configuration_k.iterations = value;
            }
            if (_sliderText1 == null)
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
