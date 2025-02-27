internal class Program
{
    private static void Main(string[] args)
    {
        IRepository<Animal> animalRepos = new Repository<Animal>();
        IRepository<Car> carRepos = new Repository<Car>();
        IRepository<Person> personRepos = new Repository<Person>();

        while (true)
        {
            Console.WriteLine("--------------Repository Manager--------------");

            Console.Write("1. Animal Repository  |  2. Car Repository  |  3. Person Repository  |  0. Exit\n" +
                "Choose repos. number: ");
            int repoNum;
            int.TryParse(Console.ReadLine(), out repoNum);

            if (repoNum == 0)
            {
                Console.WriteLine("Exit!");
                break;
            }

            if(repoNum < 0 ||  repoNum > 3)
            {
                Console.WriteLine("Wrong repository number! Try again...");
                continue;
            }

            Console.Write("1. Add  |  2. Update  |  3. Delete  |  4. Get by ID  |  5. Get All  | 0. Exit\n" +
                "Choose option: ");
            int option;
            int.TryParse(Console.ReadLine(), out option);

            if (option == 0)
            {
                Console.WriteLine("Exit");
                break;
            }

            switch (option)
            {
                case 1:
                    Console.Write("Input name: ");
                    string name = Console.ReadLine();

                    if (repoNum == 1)
                    {
                        animalRepos.Add(new Animal(name));
                    }
                    else if (repoNum == 2)
                    {
                        carRepos.Add(new Car(name));
                    }
                    else
                    {
                        Console.Write("Figure out ID: ");
                        string id = Console.ReadLine();
                        personRepos.Add(new Person(id, name));
                    }

                    Console.WriteLine("Added!");
                    break;
                case 2:
                    Console.Write("Input ID to find: ");
                    string id2 = Console.ReadLine();

                    if (repoNum == 1)
                    {
                        animalRepos.Show(animalRepos.GetById(id2));
                        Console.Write("Set new name: ");
                        animalRepos.Update(id2, Console.ReadLine());
                    }
                    else if (repoNum == 2)
                    {
                        carRepos.Show(carRepos.GetById(id2));
                        Console.Write("Set new name: ");
                        carRepos.Update(id2, Console.ReadLine());
                    }
                    else
                    {
                        personRepos.Show(personRepos.GetById(id2));
                        Console.Write("Set new name: ");
                        personRepos.Update(id2, Console.ReadLine());
                    }

                    Console.WriteLine("Updated!");
                    break;
                case 3:
                    Console.Write("Input ID to delete from repository: ");
                    string id3 = Console.ReadLine();

                    if (repoNum == 1)
                    {
                        animalRepos.Delete(id3);
                    }
                    else if (repoNum == 2)
                    {
                        carRepos.Delete(id3);
                    }
                    else
                    {
                        personRepos.Delete(id3);
                    }

                    Console.WriteLine("Deleted!");
                    break;
                case 4:
                    Console.Write("Input ID to show details: ");
                    string id4 = Console.ReadLine();

                    if(repoNum == 1)
                    {
                        animalRepos.Show(animalRepos.GetById(id4));
                    }
                    else if(repoNum == 2)
                    {
                        carRepos.Show(carRepos.GetById(id4));
                    }
                    else
                    {
                        personRepos.Show(personRepos.GetById(id4));
                    }
                    break;
                case 5:
                    if(repoNum == 1)
                    {
                        animalRepos.GetAll();
                    }
                    if(repoNum == 2)
                    {
                        carRepos.GetAll();
                    }
                    else
                    {
                        personRepos.GetAll();
                    }
                    break;
                default:
                    break;
            }
        }
    }
}

interface IRepository<T>
{
    void Add(T item);
    void Update(string id, string name);
    void Delete(string id);
    T GetById(string id);
    void Show(T item);
    void GetAll();
}

class Base
{
    public string Id { get; set; }
    public string Name { get; set; }

    public Base(string name)
    {
        Name = name;
    }
}

class Animal : Base
{
    public Animal(string name) : base(name)
    {
        Random random = new Random();
        Id = (random.Next(1, 101) * random.Next(1, 101)).ToString();
    }
}

class Car : Base
{
    public Car(string name) : base(name)
    {
        Id = Guid.NewGuid().ToString();
    }
}

class Person : Base
{
    public Person(string id, string name) : base(name)
    {
        Id = "PsID-" + id;
    }
}

class Repository<T> : IRepository<T> where T : Base
{
    private List<T> _list = new List<T>();
    public void Add(T item)
    {
        _list.Add(item);
    }

    public void Delete(string id)
    {
        _list.Remove(GetById(id));
    }

    public T GetById(string id)
    {
        foreach (T item in _list)
        {
            if (item.Id.ToString() == id)
            {
                return item;
            }
        }
        throw new Exception("Not found!");
    }

    public void GetAll()
    {
        foreach (T item in _list)
        {
            Console.WriteLine($"ID[{item.Id}] Name: {item.Name}");
        }
    }

    public void Update(string id, string name)
    {
        T item = GetById(id);
        item.Name = name;
    }

    public void Show(T item)
    {
        Console.WriteLine($"ID[{item.Id}] Name: {item.Name}");
    }
}