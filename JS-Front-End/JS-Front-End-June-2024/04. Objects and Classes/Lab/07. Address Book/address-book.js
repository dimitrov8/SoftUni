function storePersonInfo(inputArray) {
  const peopleInfo = {};

  inputArray.forEach((entry) => {
    const [name, address] = entry.split(':');

    peopleInfo[name] = address;
  });

  Object.keys(peopleInfo)
    .sort()
    .forEach((key) => {
      console.log(`${key} -> ${peopleInfo[key]}`);
    });
}

storePersonInfo([
  'Tim:Doe Crossing',
  'Bill:Nelson Place',
  'Peter:Carlyle Ave',
  'Bill:Ornery Rd',
]);
