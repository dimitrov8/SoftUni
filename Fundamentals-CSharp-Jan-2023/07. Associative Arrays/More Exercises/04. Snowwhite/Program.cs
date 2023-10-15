Dictionary<string, int> dwarves =
    new Dictionary<string, int>();

string input = String.Empty;

while ((input = Console.ReadLine()) != "Once upon a time")
{
    string[] dwarfInfo = input.Split(" <:> ", StringSplitOptions.RemoveEmptyEntries);
    string name = dwarfInfo[0];
    string color = dwarfInfo[1];
    int physics = int.Parse(dwarfInfo[2]);
    string id = name + ":" + color;

    if (!dwarves.ContainsKey(id))
    {
        dwarves.Add(id, physics);
    }
    else
    {
        dwarves[id] = Math.Max(dwarves[id], physics);
    }
}

foreach (var dwarf in dwarves
    .OrderByDescending(x => x.Value)
    .ThenByDescending(x => dwarves
    .Where(y => y.Key.Split(':')[1] == x.Key
    .Split(':')[1])
    .Count()))
{
    Console.WriteLine("({0}) {1} <-> {2}", 
        dwarf.Key.Split(':')[1], dwarf.Key.Split(':')[0], dwarf.Value);
}