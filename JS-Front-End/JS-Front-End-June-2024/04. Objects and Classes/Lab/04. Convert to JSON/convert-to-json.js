function convertToJson(name, lastName, hairColor) {
  const person = {
    name: name,
    lastName: lastName,
    hairColor: hairColor,
  };

  const personJson = JSON.stringify(person);

  console.log(personJson);
}

convertToJson('George', 'Jones', 'Brown');
