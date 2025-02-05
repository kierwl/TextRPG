using System;
using System.Collections.Generic;

class Map
{
    private Dictionary<string, Location> locations = new Dictionary<string, Location> // locations �ʵ� �߰�
    {
        { "��", new Location("��", "������ ������ ���̴�.", true) },
        { "����", new Location("����", "������ �����̴�.", false) },
        { "����", new Location("����", "�ڰ��� ���� �ʴ´ٸ� ���� �� �� �����ϴ�.", false) },
        { "�縷", new Location("�縷", "�ſ� �߰ſ� �縷�̴�.", true) }
    };

    public void ShowLocations()
    {
        foreach (var loc in locations)
        {
            Console.WriteLine($"- {loc.Key}");
        }
    }

    public Location GetLocation(string name) => locations.ContainsKey(name) ? locations[name] : null; // GetLocation �޼��� �߰�
}
