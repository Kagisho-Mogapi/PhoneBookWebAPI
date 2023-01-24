using Newtonsoft.Json;
using PhoneBook.Client;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;

HttpClient client = new();
client.BaseAddress = new Uri("https://localhost:7004");
client.DefaultRequestHeaders.Accept.Clear();
client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

HttpResponseMessage message = await client.GetAsync("api/contacts/All");
message.EnsureSuccessStatusCode();

if(message.IsSuccessStatusCode)
{
    bool run = true;
    int choice = 100;

    while(run)
    {
        Console.WriteLine();
        Console.WriteLine("-----------Contacts client-----------");
        Console.WriteLine("1. View all contact");
        Console.WriteLine("2. Get contact details by Id");
        Console.WriteLine("3. Create a contact");
        Console.WriteLine("4. Update a contact");
        Console.WriteLine("5. Delete a contact");
        Console.WriteLine("100. Close contact client");

        choice = int.Parse(Console.ReadLine());
        Contact contact = new();

        switch(choice)
        {
            case 1:
                await ContactList();
                break;

            case 2:
                await ContactDetails();
                break;

            case 3:
                await CreateContact();
                break;

            case 4:
                await UpdateContact();
                break;

            case 5:
                await DeleteContact();
                break;

            case 100:
                run = false;
                Console.WriteLine("!!!!!! CLIENT CONSOLE IS CLOSED !!!!!!!!");
                break;

            default:
                Console.WriteLine("!!! Wrong choice try again !!!");
                break;
        }

        async Task ContactList()
        {
            message = await client.GetAsync("api/contacts/All");
            message.EnsureSuccessStatusCode();

            var contacts = await message.Content.ReadFromJsonAsync<IEnumerable<Contact>>();

            foreach(Contact contact in contacts)
            {
                Console.WriteLine(contact.Id+". "+contact.Name);
            }
        }

        async Task CreateContact()
        {
            int i = 0;

            Console.Write("Enter name: ");
            contact.Name = Console.ReadLine();

            Console.Write("Enter phone number: ");
            contact.PhoneNumber = Console.ReadLine();

            Console.Write("Do want to enter home address?(n/y): ");
            if(Console.ReadLine()=="y")
            {
                Console.Write("Enter home address: ");
                contact.HomeAddress = Console.ReadLine();
            }


            Console.Write("Enter relationship(1=Friend, 2 = Family, 3 = CoWorker, 4 = Spouse,5 = Acquaintance): ");
            i = Convert.ToInt16(Console.ReadLine());
            contact.Relationship = i - 1 == 0 ? Relationship.Friend
                : i - 1 == 1 ? Relationship.Family
                : i - 1 == 2 ? Relationship.CoWorker
                : i - 1 == 3 ? Relationship.Spouse
                : Relationship.Acquaintance;

            var payload = JsonConvert.SerializeObject(contact);
            var content = new StringContent(payload, Encoding.UTF8,"application/json");

            message = await client.PostAsync("api/contacts/create", content);
            message.EnsureSuccessStatusCode();

            Console.WriteLine("!!! Contact created !!!");

        }

        async Task ContactDetails()
        {
            message = await client.GetAsync("api/contacts/All");
            message.EnsureSuccessStatusCode();
            var contacts = await message.Content.ReadFromJsonAsync<IEnumerable<Contact>>();

            foreach(Contact con in contacts)
            {
                Console.WriteLine(con.Id+". "+con.Name);
            }

            int i = 0;

            Console.WriteLine("Choose contact with id: ");
            i = int.Parse(Console.ReadLine());

            contact = contacts.Where(x => x.Id == i).FirstOrDefault();

            Console.WriteLine("\nName: " + contact.Name +
                "\nPhoneNumber: " + contact.PhoneNumber +
                "\nHomeAddress: " + contact.HomeAddress +
                "\nRelationship: " + contact.Relationship);
        }

        async Task DeleteContact()
        {
            await ContactList();

            int i = 0;

            Console.Write("Choose contact delete by id: ");
            i = int.Parse(Console.ReadLine());

            message = await client.DeleteAsync($"api/contacts/Delete/{i}");
            message.EnsureSuccessStatusCode();

            Console.WriteLine("!!! Contact deleted !!!");
        }

        async Task UpdateContact()
        {
            await ContactList();
            int detail = 0;

            Console.WriteLine();
            Console.Write("Choose contact update by id: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter name: ");
            contact.Name = Console.ReadLine();

            Console.Write("Enter phone number: ");
            contact.PhoneNumber = Console.ReadLine();

            Console.Write("Do want to enter home address?(n/y): ");
            if (Console.ReadLine() == "y")
            {
                Console.Write("Enter home address: ");
                contact.HomeAddress = Console.ReadLine();
            }


            Console.Write("Enter relationship(1=Friend, 2 = Family, 3 = CoWorker, 4 = Spouse,5 = Acquaintance): ");
            detail = Convert.ToInt16(Console.ReadLine());
            contact.Relationship = detail - 1 == 0 ? Relationship.Friend
                : detail - 1 == 1 ? Relationship.Family
                : detail - 1 == 2 ? Relationship.CoWorker
                : detail - 1 == 3 ? Relationship.Spouse
                : Relationship.Acquaintance;

            contact.Id = id;

            var payload = JsonConvert.SerializeObject(contact);
            var content = new StringContent(payload, Encoding.UTF8, "application/json");

            message = await client.PutAsync($"api/contacts/update/{id}", content);
            message.EnsureSuccessStatusCode();

            Console.WriteLine("!!! Contact updated !!!");

        }
    }
}
else
{
    Console.WriteLine(message.StatusCode.ToString());
}

Console.ReadKey();