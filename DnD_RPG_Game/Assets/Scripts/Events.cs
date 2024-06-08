using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Events : MonoBehaviour
{
    public Base Base;

    public Sprite[] Event_Sprite;
    Event[] Event = new Event[0];


    public Text Event_Name_Text;
    public Sprite Event_Sprite_Image;
    public Text Event_Info_Text;
    public Text Event_Button_1_Text;
    public Text Event_Button_2_Text;
    public Text Event_Button_3_Text;
    public Text Event_Button_4_Text;

    public Button Event_Button_1;
    public Button Event_Button_2;
    public Button Event_Button_3;
    public Button Event_Button_4;

    // Start is called before the first frame update
    void Start()
    {        
        Event[0].Event_Name = "������";
        Event[0].Event_Sprite = Event_Sprite[0];
        Event[0].Event_Info = "�� �������� ������";
        Event[0].Event_Options_Choice_1 = "������� ������";
        Event[0].Event_Options_Choice_2 = "������� ������";
        Event[0].Event_Options_Choice_3 = "����. ���� �� ��� ���������� ������";
    }

    // ������������� �������
    int Event_Number;
    int Result_Number;

    public void Event_Initialization()
    {
        Event_Number = Random.Range(0, Event.Length);        
        switch (Event_Number)
        {
            case 0:
                Event[Event_Number].Event_Name = "������";
                Event[Event_Number].Event_Sprite = Event_Sprite[0];
                Event[Event_Number].Event_Info = "�� �������� ������";
                Event[Event_Number].Event_Options_Choice_1 = "������� ������";
                Event[Event_Number].Event_Options_Choice_2 = "������� ������";
                Event[Event_Number].Event_Options_Choice_3 = "����. ���� �� ��� ���������� ������";
                Event[Event_Number].Event_Options_Choice_4 = null;
                break;
        }
        Event_Name_Text.text = Event[Event_Number].Event_Name;
        Event_Sprite_Image = Event[Event_Number].Event_Sprite;
        Event_Info_Text.text = Event[Event_Number].Event_Info;
        Event_Button_1_Text.text = Event[Event_Number].Event_Options_Choice_1;
        Event_Button_2_Text.text = Event[Event_Number].Event_Options_Choice_2;
        Event_Button_3_Text.text = Event[Event_Number].Event_Options_Choice_3;
        Event_Button_4_Text.text = Event[Event_Number].Event_Options_Choice_4;        
    }

    // ������ �� ������ ������. ������ ��� ������� ������.
    public void Event_Button_1_Click()
    {
        Result_Number = 0;
        Event_Results();
    }
    public void Event_Button_2_Click()
    {
        Result_Number = 1;
        Event_Results();
    }
    public void Event_Button_3_Click()
    {
        Result_Number = 2;
        Event_Results();
    }
    public void Event_Button_4_Click()
    {
        Result_Number = 3;
        Event_Results();
    }

    // ���������� ������� ��� ������� �� ������
    public void Event_Results()
    {
        switch (Event_Number)
        {
            case 0: // �������� ������                
                switch (Result_Number)
                {
                    case 0: // ������� ������                
                        bool Win = Base.Checking_The_Characteristics(Random.Range(5, 15), "Modifi_Dexterity");
                        if (Win == true)
                        {
                            Event_Info_Text.text = "�� ���������� ������.";
                        }
                        else
                        {                            
                            int Damage = Base.Roll_The_Dice(4);
                            Base.HP -= Damage;
                            Event_Info_Text.text = $"�� �������� ������� ������. �� ������ ������������ � ����� �������������. �� �������� �������� ������� �����. �� ��������� {Damage} �����.";
                        }
                        break;                        
                    case 1: // ������� ������                
                        Win = Base.Checking_The_Characteristics(Random.Range(5, 15), "Modifi_Force");
                        if (Win == true)
                        {
                            Event_Info_Text.text = "�� ������� ������ �������.";
                        }
                        else
                        {
                            int Damage = Base.Roll_The_Dice(4);
                            Base.HP -= Damage;
                            Event_Info_Text.text = $"�� ����� ����� � �������������� ����. �� ��������� {Damage} �����.";
                        }
                        break;
                    case 2: // ����                
                        Event_Info_Text.text = $"����� ������� ��������� ��� �������� ������ ����� �� ��� ���. �� �������, ����� ���� �� ������.";
                        break;
                }
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

class Event
{
    public string Event_Name;
    public Sprite Event_Sprite;
    public string Event_Info;
    public string Event_Options_Choice_1;
    public string Event_Options_Choice_2;
    public string Event_Options_Choice_3;
    public string Event_Options_Choice_4;
    public void Event_Result_1()
    {
        
    }
}
