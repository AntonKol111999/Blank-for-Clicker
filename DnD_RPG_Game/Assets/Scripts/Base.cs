using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    // �������������� �����
    [SerializeField] int Level = 1;
    public int Max_XP = 300;
    public int XP;
    public int Max_HP = 12;
    public int HP;
    public float Max_Stamina = 10;
    public float Stamina;
    public int Armor_Class = 14;
    public int Skill_Bonus = 2;
    public int Force = 5;
    public int Dexterity = 5;
    public int Constitution = 5;
    public int Intelligence = 5;
    public int Wisdom = 5;
    public int Charisma = 5;
    public int Hero_Damage = 1;
    public Text Hero_Char_Text;
    public Slider XP_Slider;
    public GameObject Hit_Prefab; // ������ �������, ������� ����� ������� ��� ����� �� ������� ������ �������

    // �������������� ����������
    int Max_Enemy_HP = 12;
    public int Enemy_HP;
    public Slider Enemy_HP_Slider;

    // ������� ������
    public Sprite[] Enemy_Sprite;

    // ������� �����������
    public Slider Journey_Slider;
    public int Journey_Way = 0;
    public int Journey_Way_Max = 3650;
    public Text Journey_Text;

    int Modifi_Force;
    int Modifi_Dexterity;
    int Modifi_Constitution;
    int Modifi_Intelligence;
    int Modifi_Wisdom;
    int Modifi_Charisma;

    public GameObject Event_Panel;
    public Button Click_Button;
    public GameObject ShieldButtonPrefab; // ������ ������ Shield_Button

    // Start is called before the first frame update
    void Start()
    {
        //Random rnd = new Random();
        HP = Max_HP;
        Journey_Slider.maxValue = Journey_Way_Max;
        StartCoroutine("Stamina_Coroutine");

        Shild_Button();

        Stamina = Max_Stamina;
        Enemy_HP = Max_Enemy_HP;
        Enemy_HP_Slider.maxValue = Max_Enemy_HP;
        XP = Max_HP;
        XP_Slider.maxValue = Max_XP;
        Random_Enemy_Sprite();
    }

    // ������� ����� 4, 6, 8, 10, 12, 20.
    public int Roll_The_Dice(int Max_Value)
    {
        int value = Random.Range(1, Max_Value + 1);
        return value;
    }

    // ������ ������������� ������������� �����
    public void Calculation_Of_Modifiers()
    {
        Modifi_Force = (Force - 10) / 2;
        Modifi_Dexterity = (Dexterity - 10) / 2;
        Modifi_Constitution = (Constitution - 10) / 2;
        Modifi_Intelligence = (Intelligence - 10) / 2;
        Modifi_Wisdom = (Wisdom - 10) / 2;
        Modifi_Charisma = (Charisma - 10) / 2;
    }

    // ���������� (�������� �������������)
    public bool Checking_The_Characteristics(int Required_Hit_Value, string Characteristic)
    {
        bool result_Boll = false;
        int Modifi_Characteristic = 0;
        if (Characteristic == "Force") { Modifi_Characteristic = Modifi_Force; }
        if (Characteristic == "Dexterity") { Modifi_Characteristic = Modifi_Dexterity; }
        if (Characteristic == "Constitution") { Modifi_Characteristic = Modifi_Constitution; }
        if (Characteristic == "Intelligence") { Modifi_Characteristic = Modifi_Intelligence; }
        if (Characteristic == "Wisdom") { Modifi_Characteristic = Modifi_Wisdom; }
        if (Characteristic == "Charisma") { Modifi_Characteristic = Modifi_Charisma; }
        int result_Int = Roll_The_Dice(20) + Modifi_Characteristic + Skill_Bonus;
        if (result_Int >= Required_Hit_Value) { result_Boll = true; }
        return result_Boll;
    }

    // ������� ����������� ���������� ����� (�� ����� ���� 2 ���������� ������� ������)
    int Sprite_Number_1 = 0; // ������ � ���� ���
    int Sprite_Number_2 = -1; // ������ � ������� ���
    public void Random_Enemy_Sprite()
    {
        Sprite_Number_1 = Random.Range(0, Enemy_Sprite.Length);
        while (Sprite_Number_1 == Sprite_Number_2)
        {
            Sprite_Number_1 = Random.Range(0, Enemy_Sprite.Length);
        }
        Click_Button.image.sprite = Enemy_Sprite[Sprite_Number_1];
        Sprite_Number_2 = Sprite_Number_1;
    }

    // ���������� ��������
    public void Battle()
    {
        /*
        // ������ ����������
        int Hero_Initiative = Roll_The_Dice(20) + Modifi_Dexterity;
        int Enemy_Initiative = Roll_The_Dice(20) + Roll_The_Dice(4);
        // �������������� �����
        int Enemy_HP = Roll_The_Dice(4) * 3;
        int Enemy_Attack_ = Roll_The_Dice(4);
        int Enemy_Damage = Roll_The_Dice(4);
        int Enemy_Armor_Class = Roll_The_Dice(10);
        // ������
        //int Weapon_Dice = 8;
        int Move = 0;

        if (Hero_Initiative > Enemy_Initiative)
        {
            while (HP > 0 || Enemy_HP > 0)
            {
                Move++;
                listBox1.Items.Add("���: " + Move);
                Hero_Attack();
                Enemy_Attack();
                Thread.Sleep(2000);
                Update();
            }
        }
        else
        {
            while (HP > 0 || Enemy_HP > 0)
            {
                Move++;
                listBox1.Items.Add("���: " + Move);
                Hero_Attack();
                Enemy_Attack();
                Thread.Sleep(2000);
                Update();
            }
        }

        if (HP <= 0) { listBox1.Items.Add("�� ��� ��������� �������������� ����. ��������� ����� � � ������ ���������. �� �������..."); }
        if (Enemy_HP <= 0) { listBox1.Items.Add("���� ������������ � ����� ���. �� ��������!"); }
        // ������� �����
        void Hero_Attack()
        {
            bool Bool = Melee_Weapon_Attack(Enemy_Armor_Class);
            if (Bool == true)
            {
                int Damage = Melee_Weapon_Damage(4);
                Enemy_HP -= Damage;
                listBox1.Items.Add("�� �������� " + Damage + " �����.");
            }
            else { listBox1.Items.Add("�� ��������, �� ��������������."); }
        }
        // ������� ����
        void Enemy_Attack()
        {
            bool Bool = Melee_Weapon_Attack(Armor_Class);
            if (Bool == true)
            {
                int Damage = Melee_Weapon_Damage(4);
                HP -= Damage;
                listBox1.Items.Add("���� ������� ��� " + Damage + " �����.");
            }
            else { listBox1.Items.Add("���� ��������, �� ��������������."); }
        }
        // ����� ���������� �������
        bool Melee_Weapon_Attack(int Required_Hit_Value)
        {
            bool result_Bool = false;
            int result_Int = Roll_The_Dice(20) + Skill_Bonus + Force;
            if (result_Int >= Required_Hit_Value) { result_Bool = true; }
            return result_Bool;
        }
        // ���� ���������� �������
        int Melee_Weapon_Damage(int Weapon_Dice)
        {
            int result = Roll_The_Dice(Weapon_Dice) + Force;
            return result;
        }
        */
    }

    // ������������� ������������ (�� �������� !!!)
    IEnumerator Stamina_Coroutine()
    {
        /*
        while (true)
        {
            if (Stamina < Max_Stamina)
            {
                Stamina += 0.1f;
                yield return new WaitForSeconds(0.1f);
            }
        }
        */
        while (Stamina < Max_Stamina)
        {
            Debug.Log("1");
            Stamina += 0.1f;
            yield return new WaitForSeconds(0.1f);
            Debug.Log("2");
        }


    }

    // ������� �� ������� �������
    public void Click_On_Main_Button()
    {
        if (Stamina > 0)
        {
            Force++;
            //Stamina--;
            Click_Button.transform.localScale = new Vector3(1.39f, 1.39f, 3);
            StartCoroutine("Click1");
            Enemy_HP -= Hero_Damage;
        }
    }
    IEnumerator Click1()
    {
        yield return new WaitForSeconds(0.1f);
        Click_Button.transform.localScale = new Vector3(1.4f, 1.4f, 3);
    }

    // �����������
    public void Star_Stop_Journey()
    {
        StartCoroutine("Journey_Coroutine");
    }
    IEnumerator Journey_Coroutine()
    {
        while (Journey_Way < Journey_Way_Max)
        {
            Journey_Way++;
            yield return new WaitForSeconds(0.001f);
            //StartCoroutine("Journey_Coroutine");
        }
    }

    // ���������� �������
    public void Event_Triger()
    {
        int Event_Triger = Journey_Way_Max / 3;
        if (Event_Triger == Journey_Way)
        {
            //Event_Panel.
        }
    }

    GameObject Shield_Button_Instance;
    /*
    void Shield_Appearances() // ��������� ����� ���������� ��� (����� �����)
    {
        Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
        Shield_Button_Instance = Instantiate(Shield_Button, Click_Button.transform.position + randomPosition, Quaternion.identity); // ������� ��������� ������ � �� �������
        Debug.Log("��� ����������");
    }

    public void Destroying_The_Shield() // ����������� ����
    {
        Destroy(Shield_Button_Instance);
        Debug.Log("��� ������������");
        Shield_Appearances();
    }
    */

    void Shild_Button()
    {

        // �������� ������ Shield_Button � ��������� ����� �� Click_Button
        Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
        GameObject shieldButton = Instantiate(ShieldButtonPrefab, Click_Button.transform.position + randomPosition, Quaternion.identity);
        shieldButton.transform.parent = transform; // ��������� Click_Button ��� ������������� �������
    }

    IEnumerator Shield_Coroutine() // ��������� ����� ���������� ��� (����� �����)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Vector3 randomPosition = new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 5f), 0); // ���������� ��������� ��������� ��� ������ �
            //Shield_Button.transform.position = Click_Button.transform.position + randomPosition; // ������������� ��������� ��������� ��� ������ � ������������ ������ �

        }
    }


    // Update is called once per frame
    void Update()
    {
        // �������������� �����
        Hero_Char_Text.text = $"" +
            $"�������: {Level}\n\n" +
            $"����� ���������� {Skill_Bonus}\n\n" +
            $"���� {HP}\n" +
            $"������������ {Stamina} �� {Max_Stamina}\n\n" +
            $"�� {Armor_Class}\n\n" +
            $"���� {Force}\n" +
            $"������������ {Constitution}\n" +
            $"�������� {Dexterity}\n" +
            $"��������� {Intelligence}\n" +
            $"�������� {Wisdom}\n" +
            $"������� {Charisma}";
        Calculation_Of_Modifiers();
        // �����������        
        Journey_Slider.value = Journey_Way;
        Journey_Text.text = "�������� " + Journey_Way + " ��.\n�� ���� �������� " + (Journey_Way_Max - Journey_Way) + " ��.";
        // ���� �����
        XP_Slider.value = XP;
        if (XP >= Max_XP)
        {
            XP = 0;
            Level++;
        }
        // �������� �����
        Enemy_HP_Slider.value = Enemy_HP;
        if (Enemy_HP <= 0)
        {
            Enemy_HP = Max_Enemy_HP;
            XP += 10;
            Random_Enemy_Sprite();            
            if (Random.Range(0, 1) == 0)
            {
                //Event_Panel.SetActive(true);
                //Events.Eve;
            }
        }


    }
}
