function solve() {
  document.querySelector('#btnSend').addEventListener('click', onClick);

  function onClick() {
    const inputTextArea = document.querySelector('textarea');
    const restaurantsDataArray = parseInputString(inputTextArea.value);
    const restaurants = {};

    // Parse and store restaurant data
    parseRestaurantData(restaurantsDataArray);

    // Find the best restaurant based on average salary
    const bestRestaurant = findBestRestaurant(restaurants);

    // Display the results
    displayResults(bestRestaurant);

    // Function to parse the input string into an array of restaurant strings
    function parseInputString(inputTextAreaValue) {
      // Remove the enclosing brackets and split by '","'
      return inputTextAreaValue.slice(2, -2).split('","');
    }

    // Function to parse each restaurant entry and update the restaurants object
    function parseRestaurantData(restaurantsDataArray) {
      restaurantsDataArray.forEach((rd) => {
        const delimeterIndex = rd.indexOf('-');
        const name = rd.slice(0, delimeterIndex).trim();
        const employeesDataArray = rd.slice(delimeterIndex + 1).split(',');

        if (!restaurants[name]) {
          restaurants[name] = {
            employees: [],
            totalSalary: 0,
            bestSalary: 0,
          };
        }

        const restaurantData = restaurants[name];
        employeesDataArray.forEach((empData) => {
          const [name, salary] = empData.trim().split(' ');
          const employee = { name: name, salary: Number(salary) };
          restaurantData.totalSalary += employee.salary;

          restaurantData.employees.push(employee);
          if (employee.salary > restaurantData.bestSalary) {
            restaurantData.bestSalary = employee.salary;
          }
        });
      });
    }

    // Function to find the restaurant with the highest average salary
    function findBestRestaurant(restaurants) {
      let bestRestaurant = null;
      let highestAverageSalary = 0;

      for (const [name, data] of Object.entries(restaurants)) {
        const averageSalary = data.totalSalary / data.employees.length;

        if (averageSalary > highestAverageSalary) {
          highestAverageSalary = averageSalary;
          bestRestaurant = {
            name: name,
            employees: data.employees,
            bestSalary: data.bestSalary,
            averageSalary: averageSalary,
          };
        }
      }

      return bestRestaurant;
    }

    // Function to display the best restaurant and its employees
    function displayResults(bestRestaurant) {
      if (bestRestaurant) {
        // Sort employees by salary in descending order
        bestRestaurant.employees.sort((a, b) => b.salary - a.salary);

        document.querySelector('#bestRestaurant p').textContent = `Name: ${
          bestRestaurant.name
        } Average Salary: ${bestRestaurant.averageSalary.toFixed(
          2
        )} Best Salary: ${bestRestaurant.bestSalary.toFixed(2)}`;

        document.querySelector('#workers p').textContent =
          bestRestaurant.employees
            .map((emp) => `Name: ${emp.name} With Salary: ${emp.salary}`)
            .join(' ');
      }
    }
  }
}
