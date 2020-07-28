using System;
using System.ComponentModel;

namespace PropertyChangedEvent
{
public class Person : INotifyPropertyChanged
{
    private string name;
    // Declare the event
    public event PropertyChangedEventHandler PropertyChanged;

    public Person()
    {
    }
    public Person(string value)
    {
        this.name = value;
    }
    public string PersonName
    {
        get { return name; }
        set
        {
            name = value;
            // Call OnPropertyChanged whenever the property is updated
            OnPropertyChanged("PersonName");
        }
    }
    // Create the OnPropertyChanged method to raise the event
    protected void OnPropertyChanged(string name)
    {
        PropertyChangedEventHandler handler = PropertyChanged;
        if (handler != null)
        {
            handler(this, new PropertyChangedEventArgs(name));
        }
    }
}


class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine();
        Console.WriteLine("======TESTE: classe Person PropertyChangedEventHandler=====");

        Person person = new Person();
        person.PropertyChanged += OnPropertyChanged;
        person.PersonName = "Ali";

        Console.ReadKey();
    }

    private static void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
        Person person = (Person)sender;
        Console.WriteLine("Property [{0}] has a new value = [{1}]", e.PropertyName, person.PersonName);
    }
}
}
