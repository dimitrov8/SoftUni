function storeContacts(inputArray) {
  const contacts = {};

  inputArray.forEach((entry) => {
    const [name, phoneNumber] = entry.split(' ');

    contacts[name] = phoneNumber;
  });

  for (let key in contacts) {
    console.log(`${key} -> ${contacts[key]}`);
  }
}

storeContacts([
  'Tim 0834212554',
  'Peter 0877547887',
  'Bill 0896543112',
  'Tim 0876566344',
]);
