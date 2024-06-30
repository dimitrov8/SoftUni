function solve(fruitType, grams, pricePerKg) {
  let totalPrice = ((grams / 1000) * pricePerKg).toFixed(2);
  let kg = (grams / 1000).toFixed(2);

  console.log(`I need $${totalPrice} to buy ${kg} kilograms ${fruitType}.`);
}

solve('apple', 1563, 2.35);
