function displayEmployeeInfo(inputArray) {
  const employees = {};

  inputArray.forEach((name) => {
    const personalNumber = name.length;
    employees[name] = personalNumber;
  });

  for (let employee in employees) {
    console.log(`Name: ${employee} -- Personal Number: ${employees[employee]}`);
  }
}

displayEmployeeInfo([
  'Silas Butler',
  'Adnaan Buckley',
  'Juan Peterson',
  'Brendan Villarreal',
]);
