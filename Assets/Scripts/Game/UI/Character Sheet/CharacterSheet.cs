using UnityEngine;

public class CharacterSheet : MonoBehaviour
{

    public GameObject characterSheet;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && Player.Instance.isFocusedChat == false)
        {
            GameManager.Instance.characterSheetEnabled = !GameManager.Instance.characterSheetEnabled;
            GameManager.Instance.CloseUIWindow();
            Cursor.SetCursor(CustomCursor.Instance.cursorDefault, Vector2.zero, CursorMode.Auto);
        }

        if (GameManager.Instance.characterSheetEnabled == true)
        {
            characterSheet.SetActive(true);
            GameObject characterSheetGo = GameObject.Find("Character Sheet");

            if (characterSheetGo != null)
            {
                // Left Side
                GameObject leftSideCharacterSheet = characterSheetGo.transform.Find("Left Side").gameObject;
                if (leftSideCharacterSheet != null)
                {
                    GameObject container = leftSideCharacterSheet.transform.Find("Container").gameObject;
                    if (container != null)
                    {
                        // Show Level
                        GameObject level = container.transform.Find("Level").gameObject;
                        if (level != null)
                        {
                            GameObject levelValue = level.transform.Find("Value").gameObject;
                            if (levelValue != null)
                            {
                                levelValue.GetComponent<TMPro.TextMeshProUGUI>().text = "Level " + Player.Instance.level;
                            }
                        }

                        // Show Name
                        GameObject name = container.transform.Find("Name").gameObject;
                        if (name != null)
                        {
                            GameObject nameValue = name.transform.Find("Value").gameObject;
                            if (nameValue != null)
                            {
                                nameValue.GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.pseudo;
                            }
                        }

                        // Show Stats Points
                        GameObject statsPoints = container.transform.Find("Stats Points").gameObject;
                        if (statsPoints != null)
                        {
                            GameObject statsPointsValue = statsPoints.transform.Find("Value").gameObject;
                            if (statsPointsValue != null)
                            {
                                statsPointsValue.GetComponent<TMPro.TextMeshProUGUI>().text = (Player.Instance.remainingAttributesPoints - Player.Instance.usedAttributesPointsTemp).ToString();
                            }
                        }

                        // Show Skills Points
                        GameObject skillsPoints = container.transform.Find("Skills Points").gameObject;
                        if (skillsPoints != null)
                        {
                            GameObject skillsPointsValue = skillsPoints.transform.Find("Value").gameObject;
                            if (skillsPointsValue != null)
                            {
                                skillsPointsValue.GetComponent<TMPro.TextMeshProUGUI>().text = (Player.Instance.remainingSkillsPoints - Player.Instance.usedSkillsPointsTemp).ToString();
                            }
                        }

                        // Show All Stats
                        GameObject statistics = container.transform.Find("Statistics").gameObject;
                        if (statistics != null)
                        {
                            // Show Attribute Strenght
                            GameObject strenghtAttribute = statistics.transform.Find("Strenght").gameObject;
                            if (strenghtAttribute != null)
                            {
                                GameObject strenghtAttributeValue = strenghtAttribute.transform.Find("Value").gameObject;
                                if (strenghtAttributeValue != null)
                                {
                                    strenghtAttributeValue.GetComponent<TMPro.TextMeshProUGUI>().text = (Player.Instance.strenghtFinal + Player.Instance.strenghtTemp).ToString() + " / " + (Player.Instance.strenghtFinal + Player.Instance.strenghtTemp).ToString();
                                }
                                GameObject modify = strenghtAttribute.transform.Find("Modify").gameObject;
                                ShowOrHideAttributeModify(modify);
                            }

                            // Show Attribute Endurance
                            GameObject enduranceAttribute = statistics.transform.Find("Endurance").gameObject;
                            if (enduranceAttribute != null)
                            {
                                GameObject enduranceAttributeValue = enduranceAttribute.transform.Find("Value").gameObject;
                                if (enduranceAttributeValue != null)
                                {
                                    enduranceAttributeValue.GetComponent<TMPro.TextMeshProUGUI>().text = (Player.Instance.enduranceFinal + Player.Instance.enduranceTemp).ToString() + " / " + (Player.Instance.enduranceFinal + Player.Instance.enduranceTemp).ToString();
                                }
                                GameObject modify = enduranceAttribute.transform.Find("Modify").gameObject;
                                ShowOrHideAttributeModify(modify);
                            }

                            // Show Attribute Dexterity
                            GameObject dexterityAttribute = statistics.transform.Find("Dexterity").gameObject;
                            if (dexterityAttribute != null)
                            {
                                GameObject dexterityAttributeValue = dexterityAttribute.transform.Find("Value").gameObject;
                                if (dexterityAttributeValue != null)
                                {
                                    dexterityAttributeValue.GetComponent<TMPro.TextMeshProUGUI>().text = (Player.Instance.dexterityFinal + Player.Instance.dexterityTemp).ToString() + " / " + (Player.Instance.dexterityFinal + Player.Instance.dexterityTemp).ToString();
                                }
                                GameObject modify = dexterityAttribute.transform.Find("Modify").gameObject;
                                ShowOrHideAttributeModify(modify);
                            }

                            // Show Attribute Intelect
                            GameObject intelectAttribute = statistics.transform.Find("Intelect").gameObject;
                            if (intelectAttribute != null)
                            {
                                GameObject intelectAttributeValue = intelectAttribute.transform.Find("Value").gameObject;
                                if (intelectAttributeValue != null)
                                {
                                    intelectAttributeValue.GetComponent<TMPro.TextMeshProUGUI>().text = (Player.Instance.intelectFinal + Player.Instance.intelectTemp).ToString() + " / " + (Player.Instance.intelectFinal + Player.Instance.intelectTemp).ToString();
                                }
                                GameObject modify = intelectAttribute.transform.Find("Modify").gameObject;
                                ShowOrHideAttributeModify(modify);
                            }

                            // Show Attribute Wisdom
                            GameObject wisdomAttribute = statistics.transform.Find("Wisdom").gameObject;
                            if (wisdomAttribute != null)
                            {
                                GameObject wisdomAttributeValue = wisdomAttribute.transform.Find("Value").gameObject;
                                if (wisdomAttributeValue != null)
                                {
                                    wisdomAttributeValue.GetComponent<TMPro.TextMeshProUGUI>().text = (Player.Instance.wisdomFinal + Player.Instance.wisdomTemp).ToString() + " / " + (Player.Instance.wisdomFinal + Player.Instance.wisdomTemp).ToString();
                                }
                                GameObject modify = wisdomAttribute.transform.Find("Modify").gameObject;
                                ShowOrHideAttributeModify(modify);
                            }

                            // Show Attribute Armor Class
                            GameObject armorClassAttribute = statistics.transform.Find("Armor Class").gameObject;
                            if (armorClassAttribute != null)
                            {
                                GameObject armorClassAttributeValue = armorClassAttribute.transform.Find("Value").gameObject;
                                if (armorClassAttributeValue != null)
                                {
                                    armorClassAttributeValue.GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.armorClassFinal.ToString();
                                }
                            }

                            // Show Attribute Health
                            GameObject healthAttribute = statistics.transform.Find("Health").gameObject;
                            if (healthAttribute != null)
                            {
                                GameObject healthAttributeValue = healthAttribute.transform.Find("Value").gameObject;
                                if (healthAttributeValue != null)
                                {
                                    healthAttributeValue.GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.currentHealth.ToString() + " / " + Player.Instance.health.ToString();
                                }
                            }

                            // Show Attribute Mana
                            GameObject manaAttribute = statistics.transform.Find("Mana").gameObject;
                            if (manaAttribute != null)
                            {
                                GameObject manaAttributeValue = manaAttribute.transform.Find("Value").gameObject;
                                if (manaAttributeValue != null)
                                {
                                    manaAttributeValue.GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.currentMana.ToString() + " / " + Player.Instance.mana.ToString();
                                }
                            }
                        }

                        // Show Apply & Reset Changes
                        if (Player.Instance.usedAttributesPointsTemp > 0 || Player.Instance.usedSkillsPointsTemp > 0) container.transform.Find("Changes").gameObject.SetActive(true);
                        else container.transform.Find("Changes").gameObject.SetActive(false);
                    }

                    GameObject skills = container.transform.Find("Skills").gameObject;
                    if (skills != null)
                    {
                        // Show Attribute Attack
                        GameObject attackAttribute = skills.transform.Find("Attack").gameObject;
                        if (attackAttribute != null)
                        {
                            GameObject attackAttributeValue = attackAttribute.transform.Find("Value").gameObject;
                            if (attackAttributeValue != null)
                            {
                                attackAttributeValue.GetComponent<TMPro.TextMeshProUGUI>().text = (Player.Instance.attackFinal + Player.Instance.attackTemp).ToString() + " / " + (Player.Instance.attack + Player.Instance.attackTemp).ToString();
                            }
                            GameObject modify = attackAttribute.transform.Find("Modify").gameObject;
                            ShowOrHideSkillModify(modify);
                        }

                        // Show Attribute Archery
                        GameObject archeryAttribute = skills.transform.Find("Archery").gameObject;
                        if (archeryAttribute != null)
                        {
                            GameObject archeryAttributeValue = archeryAttribute.transform.Find("Value").gameObject;
                            if (archeryAttributeValue != null)
                            {
                                archeryAttributeValue.GetComponent<TMPro.TextMeshProUGUI>().text = (Player.Instance.archeryFinal + Player.Instance.archeryTemp).ToString() + " / " + (Player.Instance.archery + Player.Instance.archeryTemp).ToString();
                            }
                            GameObject modify = archeryAttribute.transform.Find("Modify").gameObject;
                            ShowOrHideSkillModify(modify);
                        }

                        // Show Attribute Dodge
                        GameObject dodgeAttribute = skills.transform.Find("Dodge").gameObject;
                        if (dodgeAttribute != null)
                        {
                            GameObject dodgeAttributeValue = dodgeAttribute.transform.Find("Value").gameObject;
                            if (dodgeAttributeValue != null)
                            {
                                dodgeAttributeValue.GetComponent<TMPro.TextMeshProUGUI>().text = (Player.Instance.dodgeFinal + Player.Instance.dodgeTemp).ToString() + " / " + (Player.Instance.dodge + Player.Instance.dodgeTemp).ToString();
                            }
                            GameObject modify = dodgeAttribute.transform.Find("Modify").gameObject;
                            ShowOrHideSkillModify(modify);
                        }
                    }
                }

                // Right Side
                GameObject rightSideCharacterSheet = characterSheetGo.transform.Find("Right Side").gameObject;
                if (rightSideCharacterSheet != null)
                {
                    GameObject container = rightSideCharacterSheet.transform.Find("Container").gameObject;
                    if (container != null)
                    {
                        // Experience Left Score
                        GameObject experienceLeftScore = container.transform.Find("Experience Left Score").gameObject;
                        if (experienceLeftScore != null)
                        {
                            GameObject experienceLeftScoreValue = experienceLeftScore.transform.Find("Value").gameObject;
                            if (experienceLeftScoreValue != null)
                            {
                                experienceLeftScoreValue.GetComponent<TMPro.TextMeshProUGUI>().text = (Player.Instance.leveling.nextLevelExperience - Player.Instance.experience).ToString();
                            }
                        }

                        // Experience Score
                        GameObject experienceScore = container.transform.Find("Experience Score").gameObject;
                        if (experienceScore != null)
                        {
                            GameObject experienceScoreValue = experienceScore.transform.Find("Value").gameObject;
                            if (experienceScoreValue != null)
                            {
                                experienceScoreValue.GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.experience.ToString();
                            }
                        }

                        // Powers
                        GameObject powers = container.transform.Find("Powers").gameObject;
                        if (powers != null)
                        {
                            // Power Fire
                            GameObject powerFire = powers.transform.Find("Power Fire").gameObject;
                            if (powerFire != null)
                            {
                                GameObject powerFireValue = powerFire.transform.Find("Value").gameObject;
                                if (powerFireValue != null)
                                {
                                    powerFireValue.GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.powerFireFinal.ToString() + " / " + Player.Instance.powerFire;
                                }
                            }

                            // Power Earth
                            GameObject powerEarth = powers.transform.Find("Power Earth").gameObject;
                            if (powerEarth != null)
                            {
                                GameObject powerEarthValue = powerEarth.transform.Find("Value").gameObject;
                                if (powerEarthValue != null)
                                {
                                    powerEarthValue.GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.powerEarthFinal.ToString() + " / " + Player.Instance.powerEarth;
                                }
                            }

                            // Power Air
                            GameObject powerAir = powers.transform.Find("Power Air").gameObject;
                            if (powerAir != null)
                            {
                                GameObject powerAirValue = powerAir.transform.Find("Value").gameObject;
                                if (powerAirValue != null)
                                {
                                    powerAirValue.GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.powerAirFinal.ToString() + " / " + Player.Instance.powerAir;
                                }
                            }

                            // Power Water
                            GameObject powerWater = powers.transform.Find("Power Water").gameObject;
                            if (powerWater != null)
                            {
                                GameObject powerWaterValue = powerAir.transform.Find("Value").gameObject;
                                if (powerWaterValue != null)
                                {
                                    powerWaterValue.GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.powerWaterFinal.ToString() + " / " + Player.Instance.powerWater;
                                }
                            }

                            // Power Dark
                            GameObject powerDark = powers.transform.Find("Power Dark").gameObject;
                            if (powerDark != null)
                            {
                                GameObject powerDarkValue = powerDark.transform.Find("Value").gameObject;
                                if (powerDarkValue != null)
                                {
                                    powerDarkValue.GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.powerDarkFinal.ToString() + " / " + Player.Instance.powerDark;
                                }
                            }

                            // Power Light
                            GameObject powerLight = powers.transform.Find("Power Light").gameObject;
                            if (powerLight != null)
                            {
                                GameObject powerLightValue = powerLight.transform.Find("Value").gameObject;
                                if (powerLightValue != null)
                                {
                                    powerLightValue.GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.powerLightFinal.ToString() + " / " + Player.Instance.powerLight;
                                }
                            }
                        }

                        // Resistances
                        GameObject resistances = container.transform.Find("Resistances").gameObject;
                        if (powers != null)
                        {
                            // Resistance Fire
                            GameObject resistanceFire = resistances.transform.Find("Resistance Fire").gameObject;
                            if (resistanceFire != null)
                            {
                                GameObject resistanceFireValue = resistanceFire.transform.Find("Value").gameObject;
                                if (resistanceFireValue != null)
                                {
                                    resistanceFireValue.GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.resistFireFinal.ToString() + " / " + Player.Instance.resistFire;
                                }
                            }

                            // Resistance Earth
                            GameObject resistanceEarth = resistances.transform.Find("Resistance Earth").gameObject;
                            if (resistanceEarth != null)
                            {
                                GameObject resistanceEarthValue = resistanceEarth.transform.Find("Value").gameObject;
                                if (resistanceEarthValue != null)
                                {
                                    resistanceEarthValue.GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.resistEarthFinal.ToString() + " / " + Player.Instance.resistEarth;
                                }
                            }

                            // Resistance Air
                            GameObject resistanceAir = resistances.transform.Find("Resistance Air").gameObject;
                            if (resistanceAir != null)
                            {
                                GameObject resistanceAirValue = resistanceAir.transform.Find("Value").gameObject;
                                if (resistanceAirValue != null)
                                {
                                    resistanceAirValue.GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.resistAirFinal.ToString() + " / " + Player.Instance.resistAir;
                                }
                            }

                            // Resistance Water
                            GameObject resistanceWater = resistances.transform.Find("Resistance Water").gameObject;
                            if (resistanceWater != null)
                            {
                                GameObject resistanceWaterValue = resistanceAir.transform.Find("Value").gameObject;
                                if (resistanceWaterValue != null)
                                {
                                    resistanceWaterValue.GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.resistWaterFinal.ToString() + " / " + Player.Instance.resistWater;
                                }
                            }

                            // Resistance Dark
                            GameObject resistanceDark = resistances.transform.Find("Resistance Dark").gameObject;
                            if (resistanceDark != null)
                            {
                                GameObject resistanceDarkValue = resistanceDark.transform.Find("Value").gameObject;
                                if (resistanceDarkValue != null)
                                {
                                    resistanceDarkValue.GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.resistDarkFinal.ToString() + " / " + Player.Instance.resistDark;
                                }
                            }

                            // Resistance Light
                            GameObject resistanceLight = resistances.transform.Find("Resistance Light").gameObject;
                            if (resistanceLight != null)
                            {
                                GameObject resistanceLightValue = resistanceLight.transform.Find("Value").gameObject;
                                if (resistanceLightValue != null)
                                {
                                    resistanceLightValue.GetComponent<TMPro.TextMeshProUGUI>().text = Player.Instance.resistLightFinal.ToString() + " / " + Player.Instance.resistLight;
                                }
                            }
                        }
                    }
                }
            }
            else characterSheet.SetActive(false);
        }
    }

    private void ShowOrHideAttributeModify(GameObject modify)
    {
        if (modify != null)
        {
            if (Player.Instance.remainingAttributesPoints > 0) modify.transform.Find("Plus").gameObject.SetActive(true);
            else modify.transform.Find("Plus").gameObject.SetActive(false);

            if (Player.Instance.remainingAttributesPoints > 0) modify.transform.Find("Minus").gameObject.SetActive(true);
            else modify.transform.Find("Minus").gameObject.SetActive(false);
        }
    }

    private void ShowOrHideSkillModify(GameObject modify)
    {
        if (modify != null)
        {
            if (Player.Instance.remainingSkillsPoints > 0) modify.transform.Find("Plus").gameObject.SetActive(true);
            else modify.transform.Find("Plus").gameObject.SetActive(false);

            if (Player.Instance.remainingSkillsPoints > 0) modify.transform.Find("Minus").gameObject.SetActive(true);
            else modify.transform.Find("Minus").gameObject.SetActive(false);
        }
    }

    public void ApplyChanges()
    {

        // Flush Used Attributes Points
        Player.Instance.remainingAttributesPoints -= Player.Instance.usedAttributesPointsTemp;
        Player.Instance.usedAttributesPointsTemp = 0;

        // Flush Used Skills Points
        Player.Instance.remainingSkillsPoints -= Player.Instance.usedSkillsPointsTemp;
        Player.Instance.usedSkillsPointsTemp = 0;

        // Strenght
        Player.Instance.strenght += Player.Instance.strenghtTemp;
        Player.Instance.strenghtTemp = 0;

        // Endurance
        Player.Instance.endurance += Player.Instance.enduranceTemp;
        Player.Instance.enduranceTemp = 0;

        // Dexterity
        Player.Instance.dexterity += Player.Instance.dexterityTemp;
        Player.Instance.dexterityTemp = 0;

        // Intelect
        Player.Instance.intelect += Player.Instance.intelectTemp;
        Player.Instance.intelectTemp = 0;

        // Wisdom
        Player.Instance.wisdom += Player.Instance.wisdomTemp;
        Player.Instance.wisdomTemp = 0;

        // Attack
        Player.Instance.attack += Player.Instance.attackTemp;
        Player.Instance.attackTemp = 0;

        // Archery
        Player.Instance.archery += Player.Instance.archeryTemp;
        Player.Instance.archeryTemp = 0;

        // Dodge
        Player.Instance.dodge += Player.Instance.dodgeTemp;
        Player.Instance.dodgeTemp = 0;
    }

    public void ResetChanges()
    {
        // Flush Used Attributes Points
        Player.Instance.usedAttributesPointsTemp = 0;

        // Flush Used Skills Points
        Player.Instance.usedSkillsPointsTemp = 0;

        // Strenght
        Player.Instance.strenghtTemp = 0;

        // Endurance
        Player.Instance.enduranceTemp = 0;

        // Dexterity
        Player.Instance.dexterityTemp = 0;

        // Intelect
        Player.Instance.intelectTemp = 0;

        // Wisdom
        Player.Instance.wisdomTemp = 0;

        // Attack
        Player.Instance.attackTemp = 0;

        // Archery
        Player.Instance.archeryTemp = 0;

        // Dodge
        Player.Instance.dodgeTemp = 0;
    }

    public void AddStrenghTemp()
    {
        if (Player.Instance.usedAttributesPointsTemp < Player.Instance.remainingAttributesPoints)
        {
            Player.Instance.usedAttributesPointsTemp += 1;
            Player.Instance.strenghtTemp += 1;
        }
    }

    public void RemoveStrenghtTemp()
    {
        if (Player.Instance.strenghtTemp > 0)
        {
            Player.Instance.usedAttributesPointsTemp -= 1;
            Player.Instance.strenghtTemp -= 1;
        }
    }

    public void AddEnduranceTemp()
    {
        if (Player.Instance.usedAttributesPointsTemp < Player.Instance.remainingAttributesPoints)
        {
            Player.Instance.usedAttributesPointsTemp += 1;
            Player.Instance.enduranceTemp += 1;
        }
    }

    public void RemoveEnduranceTemp()
    {
        if (Player.Instance.enduranceTemp > 0)
        {
            Player.Instance.usedAttributesPointsTemp -= 1;
            Player.Instance.enduranceTemp -= 1;
        }
    }

    public void AddDexterityTemp()
    {
        if (Player.Instance.usedAttributesPointsTemp < Player.Instance.remainingAttributesPoints)
        {
            Player.Instance.usedAttributesPointsTemp += 1;
            Player.Instance.dexterityTemp += 1;
        }
    }

    public void RemoveDexterityTemp()
    {
        if (Player.Instance.dexterityTemp > 0)
        {
            Player.Instance.usedAttributesPointsTemp -= 1;
            Player.Instance.dexterityTemp -= 1;
        }
    }

    public void AddIntelectTemp()
    {
        if (Player.Instance.usedAttributesPointsTemp < Player.Instance.remainingAttributesPoints)
        {
            Player.Instance.usedAttributesPointsTemp += 1;
            Player.Instance.intelectTemp += 1;
        }
    }

    public void RemoveIntelectTemp()
    {
        if (Player.Instance.intelectTemp > 0)
        {
            Player.Instance.usedAttributesPointsTemp -= 1;
            Player.Instance.intelectTemp -= 1;
        }
    }

    public void AddWisdomTemp()
    {
        if (Player.Instance.usedAttributesPointsTemp < Player.Instance.remainingAttributesPoints)
        {
            Player.Instance.usedAttributesPointsTemp += 1;
            Player.Instance.wisdomTemp += 1;
        }
    }

    public void RemoveWisdomTemp()
    {
        if (Player.Instance.wisdomTemp > 0)
        {
            Player.Instance.usedAttributesPointsTemp -= 1;
            Player.Instance.wisdomTemp -= 1;
        }
    }

    public void AddAttackTemp()
    {
        if (Player.Instance.usedSkillsPointsTemp < Player.Instance.remainingSkillsPoints)
        {
            Player.Instance.usedSkillsPointsTemp += 1;
            Player.Instance.attackTemp += 1;
        }
    }

    public void RemoveAttackTemp()
    {
        if (Player.Instance.attackTemp > 0)
        {
            Player.Instance.usedSkillsPointsTemp -= 1;
            Player.Instance.attackTemp -= 1;
        }
    }

    public void AddArcheryTemp()
    {
        if (Player.Instance.usedSkillsPointsTemp < Player.Instance.remainingSkillsPoints)
        {
            Player.Instance.usedSkillsPointsTemp += 1;
            Player.Instance.archeryTemp += 1;
        }
    }

    public void RemoveArcheryTemp()
    {
        if (Player.Instance.archeryTemp > 0)
        {
            Player.Instance.usedSkillsPointsTemp -= 1;
            Player.Instance.archeryTemp -= 1;
        }
    }

    public void AddDodgeTemp()
    {
        if (Player.Instance.usedSkillsPointsTemp < Player.Instance.remainingSkillsPoints)
        {
            Player.Instance.usedSkillsPointsTemp += 1;
            Player.Instance.dodgeTemp += 1;
        }
    }

    public void RemoveDodgeTemp()
    {
        if (Player.Instance.dodgeTemp > 0)
        {
            Player.Instance.usedSkillsPointsTemp -= 1;
            Player.Instance.dodgeTemp -= 1;
        }
    }
}
