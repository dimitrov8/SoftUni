function solve() {
  // Attach an event listener to the "Send" button that triggers the onClick function when clicked
  document.querySelector('#btnSend').addEventListener('click', onClick);

  function onClick() {
    // Get the value from the textarea and parse it into an array of restaurant strings
    const inputTextArea = document.querySelector('textarea');
    const restaurantsArray = parseRestaurants(inputTextArea.value);

    // Initialize an object to store restaurant data
    const restaurants = {};

    // Iterate through each restaurant entry
    restaurantsArray.forEach((restaurant) => {
      // Parse each restaurant entry into a name and employee information
      const { restaurantName, employeesInfo } = parseRestaurantData(restaurant);

      // If the restaurant is not already in the object, initialize its data
      if (!restaurants[restaurantName]) {
        restaurants[restaurantName] = {
          employees: [],
          totalSalary: 0,
          bestSalary: 0,
        };
      }

      // Get the current restaurant data
      const restaurantData = restaurants[restaurantName];
      // Iterate through each employee and update the restaurant data
      employeesInfo.forEach((employee) => {
        restaurantData.employees.push(employee);
        restaurantData.totalSalary += employee.salary;
        if (employee.salary > restaurantData.bestSalary) {
          restaurantData.bestSalary = employee.salary;
        }
      });
    });

    // Variables to keep track of the best restaurant and its highest average salary
    let bestRestaurant = null;
    let highestAverageSalary = 0;

    // Find the restaurant with the highest average salary
    for (const [name, data] of Object.entries(restaurants)) {
      const averageSalary = data.totalSalary / data.employees.length;
      if (averageSalary > highestAverageSalary) {
        highestAverageSalary = averageSalary;
        bestRestaurant = {
          restaurantName: name,
          employees: data.employees,
          bestSalary: data.bestSalary,
          averageSalary: averageSalary,
        };
      }
    }

    // If a best restaurant was found, update the output elements
    if (bestRestaurant) {
      // Sort employees by salary in descending order
      bestRestaurant.employees.sort((a, b) => b.salary - a.salary);

      // Update the "bestRestaurant" section with the restaurant information
      document.querySelector('#bestRestaurant p').textContent = `Name: ${
        bestRestaurant.restaurantName
      } Average Salary: ${bestRestaurant.averageSalary.toFixed(
        2
      )} Best Salary: ${bestRestaurant.bestSalary.toFixed(2)}`;

      // Update the "workers" section with the sorted list of employees
      document.querySelector('#workers p').textContent =
        bestRestaurant.employees
          .map((emp) => `Name: ${emp.name} With Salary: ${emp.salary}`)
          .join(' ');
    }

    // Function to parse the input string into an array of restaurant entries
    function parseRestaurants(input) {
      // Remove the enclosing brackets and split by '","'
      return input.slice(2, -2).split('","');
    }

    // Function to parse each restaurant entry into name and employee data
    function parseRestaurantData(restaurant) {
      // Find the index of the '-' character to separate the name from employee data
      const delimiterIndex = restaurant.indexOf('-');
      const restaurantName = restaurant.slice(0, delimiterIndex).trim();
      const employeesInfo = restaurant
        .slice(delimiterIndex + 1)
        .split(',')
        .map((entry) => {
          // Split each employee entry into name and salary
          const [name, salary] = entry.trim().split(' ');
          return { name, salary: Number(salary) };
        });

      return { restaurantName, employeesInfo };
    }
  }
}
