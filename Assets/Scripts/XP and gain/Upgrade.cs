using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    //Сама прокачка
    public UpgradeData upgrateData;
    public int m_MSCost = 0;
    public int MSMaxlvl = 4;
    public int MSCost { get => 100+ (150*m_MSCost); }
    public void MSUpdate()
    {
        if ((Meta.PlayerXP >= (100+ (150*m_MSCost))) && (m_MSCost < MSMaxlvl))
        {
            Meta.PlayerXP -= 100 + (150*m_MSCost);
            m_MSCost += 1;
            MSUpgrade();
        }
        else return;
    }
    private void MSUpgrade()
    {
        if (Meta.baseMS < 12) Meta.baseMS += 2;
        else return;
    }

    public int m_HPCost = 0;
    public int HPMaxlvl = 4;
    public int HPCost { get => 100+ (100*m_HPCost); }
    public void HPUpdate()
    {
        if ((Meta.PlayerXP >= 100+ (100*m_HPCost)) && (m_HPCost < HPMaxlvl))
        {
            Meta.PlayerXP -= 100+ (100*m_HPCost);
            m_HPCost += 1;
            HPUpgrade();
        }
        else return;
    }
    private void HPUpgrade()
    {
        if (Meta.baseHP < 200) Meta.baseHP += 25;
        else return;
    }

    public int m_BMCost = 0;
    public int BMMaxlvl = 3;
    public int BMCost { get => 250+ (250*m_BMCost); }
    public void BMUpdate()
    {
        if ((Meta.PlayerXP >= 250+ (250*m_BMCost)) && (m_BMCost < BMMaxlvl))
        {
            Meta.PlayerXP -= 250+ (250*m_BMCost);
            m_BMCost += 1;
            BMUpgrade();
        }
        else return;
    }
    private void BMUpgrade()
    {
        if (Meta.baseMoney < 600) Meta.baseMoney += 150;
        else return;
    }

    public void Load()
    {
        UpgradeData Load = JsonManager.UpgradeLoadToJson();
        if (Load != null)
        {
            m_MSCost = Load.MSCost;
            m_HPCost = Load.HPCost;
            m_BMCost = Load.BMCost;
        }
        else
        {
            m_MSCost = 0;
            m_HPCost = 0;
            m_BMCost = 0;
        }
    }
    public void Save()
    {
        upgrateData = new UpgradeData();
        upgrateData.MSCost = m_MSCost;
        upgrateData.HPCost = m_HPCost;
        upgrateData.BMCost = m_BMCost;
        JsonManager.UpgradeSaveToJson(upgrateData);
    }
    public class UpgradeData
    {
        public int MSCost;
        public int HPCost;
        public int BMCost;

    }

    // Взаимодействие с интерфейсом
    public void Start()
    {
        Load();
        JsonManager.LoadFromJson();
        TextSet();
    }

    public void OnDisable()
    {
        Save();
        JsonManager.SaveToJson();
    }

    public void MSUpgradeButton()
    {
        MSUpdate();
        TextSet();
    }
    public void HPUpgradeButton()
    {
        HPUpdate();
        TextSet();
    }
    public void BMUpgradeButton()
    {
        BMUpdate();
        TextSet();
    }
    public void StartButton()
    {
        
    }
    public void StartDemoButton()
    {
        
    }
    public void BackButton()
    {
        Meta.PlayerXP += 100;
        TextSet();
    }

    //Вывод текста
    [SerializeField] protected TextMeshProUGUI ms_cost;
    [SerializeField] protected TextMeshProUGUI hp_cost;
    [SerializeField] protected TextMeshProUGUI bm_cost;
    [SerializeField] protected TextMeshProUGUI xp_value;
    [SerializeField] protected TextMeshProUGUI ms_value;
    [SerializeField] protected TextMeshProUGUI hp_value;
    [SerializeField] protected TextMeshProUGUI bm_value;
    [SerializeField] protected Slider ms_slider;
    [SerializeField] protected Slider hp_slider;
    [SerializeField] protected Slider bm_slider;
    public void TextSet()
    {
        if (MSCost == m_MSCost) ms_cost.text = $"MAX"; else ms_cost.text = $"{MSCost}";
        if (HPCost == m_HPCost) hp_cost.text = $"MAX"; else hp_cost.text = $"{HPCost}";
        if (BMCost == m_BMCost) bm_cost.text = $"MAX"; else bm_cost.text = $"{BMCost}";
        
        xp_value.text = $"Опыт: {Meta.PlayerXP}";
        ms_value.text = $"{Meta.baseMS}";
        hp_value.text = $"{Meta.baseHP}";
        bm_value.text = $"{Meta.baseMoney}";

        ms_slider.value = m_MSCost;
        hp_slider.value = m_HPCost;
        bm_slider.value = m_BMCost;
    }
}
