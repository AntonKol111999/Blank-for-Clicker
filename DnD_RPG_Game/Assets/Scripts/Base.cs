using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Base : MonoBehaviour
{
    // Характеристика героя
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
    public GameObject Hit_Prefab; // префаб объекта, который нужно создать при клике по главной кнопке кликера

    // Характеристика противника
    int Max_Enemy_HP = 12;
    public int Enemy_HP;
    public Slider Enemy_HP_Slider;

    // Спрайты врагов
    public Sprite[] Enemy_Sprite;

    // Прогрес путешествия
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
    public GameObject ShieldButtonPrefab; // Префаб кнопки Shield_Button

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

    // Брасить кубик 4, 6, 8, 10, 12, 20.
    public int Roll_The_Dice(int Max_Value)
    {
        int value = Random.Range(1, Max_Value + 1);
        return value;
    }

    // Расчет модификаторов характеристик героя
    public void Calculation_Of_Modifiers()
    {
        Modifi_Force = (Force - 10) / 2;
        Modifi_Dexterity = (Dexterity - 10) / 2;
        Modifi_Constitution = (Constitution - 10) / 2;
        Modifi_Intelligence = (Intelligence - 10) / 2;
        Modifi_Wisdom = (Wisdom - 10) / 2;
        Modifi_Charisma = (Charisma - 10) / 2;
    }

    // Спасбросок (проверка характеристик)
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

    // Вывести изображение случайного врага (Не может быть 2 одинаковых спрайта подряд)
    int Sprite_Number_1 = 0; // спрайт в этот раз
    int Sprite_Number_2 = -1; // спрайт в прошлый раз
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

    // Начинается сражение
    public void Battle()
    {
        /*
        // Расчет инициативы
        int Hero_Initiative = Roll_The_Dice(20) + Modifi_Dexterity;
        int Enemy_Initiative = Roll_The_Dice(20) + Roll_The_Dice(4);
        // Характеристики врага
        int Enemy_HP = Roll_The_Dice(4) * 3;
        int Enemy_Attack_ = Roll_The_Dice(4);
        int Enemy_Damage = Roll_The_Dice(4);
        int Enemy_Armor_Class = Roll_The_Dice(10);
        // Прочее
        //int Weapon_Dice = 8;
        int Move = 0;

        if (Hero_Initiative > Enemy_Initiative)
        {
            while (HP > 0 || Enemy_HP > 0)
            {
                Move++;
                listBox1.Items.Add("Ход: " + Move);
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
                listBox1.Items.Add("Ход: " + Move);
                Hero_Attack();
                Enemy_Attack();
                Thread.Sleep(2000);
                Update();
            }
        }

        if (HP <= 0) { listBox1.Items.Add("На вас обрущился сокрушительный удар. Последний вздох и в глазах потемнело. Вы погибли..."); }
        if (Enemy_HP <= 0) { listBox1.Items.Add("Враг распластался у ваших ног. Вы победили!"); }
        // Атакует герой
        void Hero_Attack()
        {
            bool Bool = Melee_Weapon_Attack(Enemy_Armor_Class);
            if (Bool == true)
            {
                int Damage = Melee_Weapon_Damage(4);
                Enemy_HP -= Damage;
                listBox1.Items.Add("Вы наносите " + Damage + " урона.");
            }
            else { listBox1.Items.Add("Вы атакеете, но промахиваетесь."); }
        }
        // Атакует враг
        void Enemy_Attack()
        {
            bool Bool = Melee_Weapon_Attack(Armor_Class);
            if (Bool == true)
            {
                int Damage = Melee_Weapon_Damage(4);
                HP -= Damage;
                listBox1.Items.Add("Враг наносит вам " + Damage + " урона.");
            }
            else { listBox1.Items.Add("Враг атакеете, но промахиваетесь."); }
        }
        // Атака рукопашным оружием
        bool Melee_Weapon_Attack(int Required_Hit_Value)
        {
            bool result_Bool = false;
            int result_Int = Roll_The_Dice(20) + Skill_Bonus + Force;
            if (result_Int >= Required_Hit_Value) { result_Bool = true; }
            return result_Bool;
        }
        // Урон рукопашным оружием
        int Melee_Weapon_Damage(int Weapon_Dice)
        {
            int result = Roll_The_Dice(Weapon_Dice) + Force;
            return result;
        }
        */
    }

    // Востановление выносливости (Не работает !!!)
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

    // Нажатие на кнопуку кликера
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

    // Путишествие
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

    // Происхидит событие
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
    void Shield_Appearances() // определят когда появляется щит (атака врага)
    {
        Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
        Shield_Button_Instance = Instantiate(Shield_Button, Click_Button.transform.position + randomPosition, Quaternion.identity); // Создаем экземпляр кнопки Б из префаба
        Debug.Log("Щит появляется");
    }

    public void Destroying_The_Shield() // уничтожение щита
    {
        Destroy(Shield_Button_Instance);
        Debug.Log("Щит уничтожается");
        Shield_Appearances();
    }
    */

    void Shild_Button()
    {

        // Создание кнопки Shield_Button в случайном месте на Click_Button
        Vector3 randomPosition = new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0);
        GameObject shieldButton = Instantiate(ShieldButtonPrefab, Click_Button.transform.position + randomPosition, Quaternion.identity);
        shieldButton.transform.parent = transform; // Установка Click_Button как родительского объекта
    }

    IEnumerator Shield_Coroutine() // определят когда появляется щит (атака врага)
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            Vector3 randomPosition = new Vector3(Random.Range(-4f, 4f), Random.Range(-4f, 5f), 0); // генерируем случайное положение для кнопки Б
            //Shield_Button.transform.position = Click_Button.transform.position + randomPosition; // устанавливаем случайное положение для кнопки Б относительно кнопки А

        }
    }


    // Update is called once per frame
    void Update()
    {
        // Характеристики Героя
        Hero_Char_Text.text = $"" +
            $"Уровень: {Level}\n\n" +
            $"Бунус мастерства {Skill_Bonus}\n\n" +
            $"Хиты {HP}\n" +
            $"Выносливость {Stamina} из {Max_Stamina}\n\n" +
            $"КД {Armor_Class}\n\n" +
            $"Сила {Force}\n" +
            $"Телосложение {Constitution}\n" +
            $"Ловкость {Dexterity}\n" +
            $"Интеллект {Intelligence}\n" +
            $"Мудрость {Wisdom}\n" +
            $"Харизма {Charisma}";
        Calculation_Of_Modifiers();
        // Путишествие        
        Journey_Slider.value = Journey_Way;
        Journey_Text.text = "Пройдено " + Journey_Way + " км.\nДо цели осталось " + (Journey_Way_Max - Journey_Way) + " км.";
        // Опыт героя
        XP_Slider.value = XP;
        if (XP >= Max_XP)
        {
            XP = 0;
            Level++;
        }
        // Здоровье врага
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
