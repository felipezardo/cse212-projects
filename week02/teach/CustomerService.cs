/// <summary>
/// Maintain a Customer Service Queue.  Allows new customers to be 
/// added and allows customers to be serviced.
/// </summary>
public class CustomerService {
    public static void Run() {
        // Example code to see what's in the customer service queue:
        // var cs = new CustomerService(10);
        // Console.WriteLine(cs);

        // Test Cases

        // Test 1
        // Scenario: Try to add more customers than the max size. (Max size is 2).
        // Expected Result: It should let me add 2 customers, but the 3rd time it should show an error message.
        Console.WriteLine("Test 1");
        var cs1 = new CustomerService(2);
        Console.WriteLine("Type anything for the next 2 customers:");
        cs1.AddNewCustomer(); // Customer 1
        cs1.AddNewCustomer(); // Customer 2
        Console.WriteLine("Now trying to add a 3rd customer. You should see an error:");
        cs1.AddNewCustomer(); // Customer 3 - should fail
        // Defect(s) Found: It was using > instead of >=. I change this so it stops when queue is full.

        Console.WriteLine("=================");

        // Test 2
        // Scenario: Serve a customer from a normal queue.
        // Expected Result: It should display the correct customer information that I just typed.
        Console.WriteLine("Test 2");
        var cs2 = new CustomerService(5);
        Console.WriteLine("Type a customer name, id and problem:");
        cs2.AddNewCustomer();
        Console.WriteLine("Now serving the customer:");
        cs2.ServeCustomer();
        // Defect(s) Found: ServeCustomer was removing the customer first, then reading index 0. This is wrong. I fix the order to read first, then remove.

        Console.WriteLine("=================");

        // Test 3
        // Scenario: Serve a customer when the queue is empty.
        // Expected Result: It should show an error message and not crash.
        Console.WriteLine("Test 3");
        var cs3 = new CustomerService(5);
        Console.WriteLine("Trying to serve from empty queue:");
        cs3.ServeCustomer();
        // Defect(s) Found: The code did not check if queue is empty. It just crashed. I add a check to show error message if count is 0.
        
        Console.WriteLine("=================");
    }

    private readonly List<Customer> _queue = new();
    private readonly int _maxSize;

    public CustomerService(int maxSize) {
        if (maxSize <= 0)
            _maxSize = 10;
        else
            _maxSize = maxSize;
    }

    /// <summary>
    /// Defines a Customer record for the service queue.
    /// This is an inner class.  Its real name is CustomerService.Customer
    /// </summary>
    private class Customer {
        public Customer(string name, string accountId, string problem) {
            Name = name;
            AccountId = accountId;
            Problem = problem;
        }

        private string Name { get; }
        private string AccountId { get; }
        private string Problem { get; }

        public override string ToString() {
            return $"{Name} ({AccountId})  : {Problem}";
        }
    }

    /// <summary>
    /// Prompt the user for the customer and problem information.  Put the 
    /// new record into the queue.
    /// </summary>
    private void AddNewCustomer() {
        // Verify there is room in the service queue
        if (_queue.Count >= _maxSize) { // I change > to >= because if it is equal, it is full already.
            Console.WriteLine("Maximum Number of Customers in Queue.");
            return;
        }

        Console.Write("Customer Name: ");
        var name = Console.ReadLine()!.Trim();
        Console.Write("Account Id: ");
        var accountId = Console.ReadLine()!.Trim();
        Console.Write("Problem: ");
        var problem = Console.ReadLine()!.Trim();

        // Create the customer object and add it to the queue
        var customer = new Customer(name, accountId, problem);
        _queue.Add(customer);
    }

    /// <summary>
    /// Dequeue the next customer and display the information.
    /// </summary>
    private void ServeCustomer() {
        // I add this check to prevent crash if queue is empty
        if (_queue.Count <= 0) {
            Console.WriteLine("Queue is empty. No customers to serve.");
            return;
        }

        var customer = _queue[0]; // I move this line up to get the customer before remove
        _queue.RemoveAt(0);       // Now I remove
        Console.WriteLine(customer);
    }

    /// <summary>
    /// Support the WriteLine function to provide a string representation of the
    /// customer service queue object. This is useful for debugging. If you have a 
    /// CustomerService object called cs, then you run Console.WriteLine(cs) to
    /// see the contents.
    /// </summary>
    /// <returns>A string representation of the queue</returns>
    public override string ToString() {
        return $"[size={_queue.Count} max_size={_maxSize} => " + string.Join(", ", _queue) + "]";
    }
}