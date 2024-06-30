function solve(numberOfPeople, groupType, day) {
  var singlePersonPrice;
  var totalPrice;
  if (groupType === 'Students') {
    switch (day) {
      case 'Friday':
        singlePersonPrice = 8.45;
        break;
      case 'Saturday':
        singlePersonPrice = 9.8;
        break;
      case 'Sunday':
        singlePersonPrice = 10.46;
        break;
    }
  } else if (groupType === 'Business') {
    switch (day) {
      case 'Friday':
        singlePersonPrice = 10.9;
        break;
      case 'Saturday':
        singlePersonPrice = 15.6;
        break;
      case 'Sunday':
        singlePersonPrice = 16;
        break;
    }
  } else if (groupType === 'Regular') {
    switch (day) {
      case 'Friday':
        singlePersonPrice = 15;
        break;
      case 'Saturday':
        singlePersonPrice = 20;
        break;
      case 'Sunday':
        singlePersonPrice = 22.5;
        break;
    }
  }

  totalPrice = singlePersonPrice * numberOfPeople;

  if (groupType === 'Students' && numberOfPeople >= 30) {
    totalPrice *= 0.85;
  } else if (groupType === 'Business' && numberOfPeople >= 100) {
    totalPrice -= singlePersonPrice * 10;
  } else if (
    groupType === 'Regular' &&
    numberOfPeople >= 10 &&
    numberOfPeople <= 20
  ) {
    totalPrice *= 0.95;
  }
  console.log(`Total price: ${totalPrice.toFixed(2)}`);
}

solve(30, 'Students', 'Sunday');
