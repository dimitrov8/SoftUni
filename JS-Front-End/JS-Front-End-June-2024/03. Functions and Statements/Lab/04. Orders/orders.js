function getTotalPrice(product, quantity) {
  let price;
  if (product === 'coffee') {
    price = 1.5;
  } else if (product === 'water') {
    price = 1.0;
  } else if (product === 'coke') {
    price = 1.4;
  } else if (product === 'snacks') {
    price = 2.0;
  }

  let totalPrice = (price * quantity).toFixed(2);
  console.log(totalPrice);
}

getTotalPrice('coffee', 2);
