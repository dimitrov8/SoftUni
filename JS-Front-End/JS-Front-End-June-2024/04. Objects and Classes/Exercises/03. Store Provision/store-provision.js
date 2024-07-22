function updateInventory(stockArray, ordersArray) {
  function parseProductData(dataArray) {
    for (let i = 0; i < dataArray.length; i += 2) {
      const name = dataArray[i];
      const quantity = parseInt(dataArray[i + 1], 10);

      if (inventory[name]) {
        inventory[name] += quantity;
      } else {
        inventory[name] = quantity;
      }
    }
  }

  let inventory = {};

  parseProductData(stockArray);
  parseProductData(ordersArray);

  for (const [product, quantity] of Object.entries(inventory)) {
    console.log(`${product} -> ${quantity}`);
  }

  //   Object.keys(inventory).forEach((product) => {
  //     console.log(`${product} -> ${inventory[product]}`);
  //   });
}

updateInventory(
  ['Chips', '5', 'CocaCola', '9', 'Bananas', '14', 'Pasta', '4', 'Beer', '2'],
  ['Flour', '44', 'Oil', '12', 'Pasta', '7', 'Tomatoes', '70', 'Bananas', '30']
);
