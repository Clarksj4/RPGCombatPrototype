
public interface IDefender
{
    string name { get; }
    int MaxHealth { get; }
    int Health { get; set; }
    int Defense { get; }
    int Evasion { get; }
}
